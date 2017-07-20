using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using EnvDTE80;
using EnvDTE;
using ProjectHero2.Core.VSEventArgs;
using ProjectHero2.Core.Iterators;

namespace ProjectHero2.Core
{
    public partial class ucProjectHero : VSUserControlBase, IEventModel
    {
        private DTE2 _applicationObject;
        private bool m_buildCancelResetNeeded = false;
        private bool m_isSolutionClosing = false;

        /*
         * Contains the following states.
         * 
         * - Completed
         * - Failed
         * - Skipped
         * - Pending
         * - ProjectCount
         * 
         */
        private Dictionary<string, int> m_StateDictionary;

        private const string CompletedConst = "Completed";
        private const string FailedConst = "Failed";
        private const string SkippedConst = "Skipped";
        private const string PendingConst = "Pending";
        private const string ProjectCountConst = "ProjectCount";

        private new const string DefaultFont = "Calibri";

        internal enum ProjectNodeState : byte
        {
            Idle,
            Building,
            Pending,
            Skipped,
            Done,
            Error
        }

        internal class ProjectNode
        {
            public string Name { get; private set; }
            public string UniqueName { get; private set; }
            public string Platform { get; set; }
            public string ProjectConfig { get; set; }
            public VSProjectType ProjType { get; private set; }
            public ProjectNode ParentNode { get; private set; }
            public DateTime BuildStartTime { get; set; }
            public DateTime BuildEndTime { get; set; }
            public ProjectNodeState State { get; set; }
            public IList<ErrorItem> ErrorCollection { get; private set; }
            public bool IsSelected { get; set; }

            public ProjectNode(string name, VSProjectType projType, ProjectNode parent, string uniqueName = "")
            {
                this.Name = name;
                this.ProjType = projType;
                this.ParentNode = parent;
                this.UniqueName = uniqueName;
                this.ErrorCollection = new List<ErrorItem>();
            }

            public string GetStateAsString()
            {
                StringBuilder builder = new StringBuilder();
                switch (State)
                {
                    case ProjectNodeState.Idle: builder.Append("Idle"); break;
                    case ProjectNodeState.Done: builder.Append("Done"); break;
                    case ProjectNodeState.Error: builder.Append("Error"); break;
                    case ProjectNodeState.Pending: builder.Append("Pending"); break;
                    case ProjectNodeState.Skipped: builder.Append("Skipped"); break;
                }
                return builder.ToString();
            }

            public string GetProjectTypeAsString()
            {
                StringBuilder builder = new StringBuilder();
                switch (ProjType)
                {
                    case VSProjectType.CPlusPlusProject:
                        builder.Append("C++ Project");
                        break;

                    case VSProjectType.CSharpProject:
                    case VSProjectType.SDECSharpProject:
                        builder.Append("C# Project");
                        break;

                    case VSProjectType.FSharpProject:
                        builder.Append("F# Project");
                        break;

                    case VSProjectType.SDEVBProject:
                    case VSProjectType.VBProject:
                        builder.Append("VB Project");
                        break;

                    case VSProjectType.VJSharpProject:
                        builder.Append("J# Project");
                        break;

                    case VSProjectType.Unknown:
                        builder.Append("Unknown");
                        break;
                }
                return builder.ToString();
            }

            public void ResetErrors()
            {
                this.ErrorCollection.Clear();
                this.ErrorCollection = null;
                this.ErrorCollection = new List<ErrorItem>();
            }

            public void AddError(ErrorItem errorItem)
            {
                this.ErrorCollection.Add(errorItem);
            }
        }
        private HashSet<ProjectNode> m_Nodes;

        public ucProjectHero()
        {
            InitializeComponent();
        }

        private void ucProjectHero_Load(object sender, EventArgs e)
        {
        }

        public void Init(DTE2 applicationObject)
        {
            this._applicationObject = applicationObject;
            SetTheme();
        }

        public override void SetTheme()
        {
            base.SetTheme();
        }

        public override void AdjustView(VSToolWindowState state)
        {
            base.AdjustView(state);
        }

        public void EventMessageProcedure(VSEvents e, object state, out bool callNextProcedure)
        {
            try
            {
                switch (e)
                {
                    case VSEvents.SolutionOpened:
                    case VSEvents.SolutionProjectAdded:
                    case VSEvents.SolutionProjectRemoved:
                    case VSEvents.SolutionProjectRenamed:
                    case VSEvents.SolutionRenamed:
                    case VSEvents.ProjectAdded:
                    case VSEvents.ProjectRemoved:
                    case VSEvents.ProjectRenamed:
                    case VSEvents.ProjectItemAdded:
                    case VSEvents.ProjectItemRemoved:
                    case VSEvents.ProjectItemRenamed:
                        ActivateControl();
                        OnSolutionOpenedOrProjectChanged();
                        break;

                    case VSEvents.SolutionBeforeClosing:
                        ActivateControl();
                        m_isSolutionClosing = true;
                        break;

                    case VSEvents.SolutionAfterClosing:
                        m_isSolutionClosing = false;
                        ActivateControl();
                        OnSolutionOpenedOrProjectChanged();
                        OnSolutionClosed();
                        break;

                    case VSEvents.BuildBegin:
                        ActivateControl();
                        OnBuildBegin();
                        break;

                    case VSEvents.BuildComplete:
                        ActivateControl();
                        OnBuildEnd();
                        break;

                    case VSEvents.BuildProjectConfigBegin:
                        ActivateControl();
                        OnProjectBuildStart(state as BuildProjectConfigEventArg);
                        break;

                    case VSEvents.BuildProjectConfigDone:
                        ActivateControl();
                        OnProjectBuildEnd(state as BuildProjectConfigEventArg);
                        break;

                    case VSEvents.BeforeCommandExecute:
                        // Handle the cancel event from the menu since it wasn't covered in our plugin.
                        CommandExecuteEventArg commandEventArg = state as CommandExecuteEventArg;
                        Command cmd = commandEventArg.Command;

                        if (cmd != null && cmd.Name.Equals("Build.Cancel"))
                            m_buildCancelResetNeeded = true;

                        break;
                }
            }
            catch
            {
                // TODO: Future logging functionality.
            }

            callNextProcedure = false;
        }

        #region Event Methods

        private void ActivateControl()
        {
            Window2 win = VSWindowManager.Manager.GetWindow(VSSettings.vshwnd_ProjectHero);
            if (!win.Visible)
                win.Activate();
        }

        private void OnSolutionClosed()
        {
            ScanSolution2();
            HideProgressElements();
            ResetProgressBar();
            ResetDictionary();
            UpdateDictionaryStatus();

            lvView.Items.Clear();
            lvView.Groups.Clear();

            m_Nodes.Clear();
            m_Nodes = null;

            m_StateDictionary.Clear();
            m_StateDictionary = null;

            rootNode = null;
        }

        private void OnSolutionOpenedOrProjectChanged()
        {
            if (m_isSolutionClosing)
                return;

            // Re-initialize the hashset and the dictionary.
            if (m_Nodes != null)
            {
                m_Nodes.Clear();
                m_Nodes = null;
            }
            m_Nodes = new HashSet<ProjectNode>();

            if (m_StateDictionary != null)
            {
                m_StateDictionary.Clear();
                m_StateDictionary = null;
            }
            m_StateDictionary = new Dictionary<string, int>();

            HideProgressElements();
            ResetProgressBar();
            ResetDictionary();
            UpdateDictionaryStatus();
            ScanSolution2();
        }

        private void OnBuildBegin()
        {
            ShowProgressElements();
            ResetDictionary();
            ResetProgressBar();
            SetProgressBarMax(m_Nodes.Count());

            // Set the state of all nodes to 'Pending'.
            var nodes = m_Nodes.Where(e => e.ParentNode != null);
            foreach (ProjectNode node in nodes)
            {
                node.State = ProjectNodeState.Pending;
                ListViewItem item = lvView.Items[node.Name];
                item.SubItems[3].Text = "Pending";
                item.SubItems[4].Text = string.Empty;
                item.Tag = node;
            }

            // Update state information.
            m_StateDictionary[ProjectCountConst] =
            m_StateDictionary[PendingConst] = nodes.Count();
            UpdateDictionaryStatus();
        }

        private void OnBuildEnd()
        {
            HideProgressElements();
            ResetProgressBar();
            UpdateDictionaryStatus();

            if (m_buildCancelResetNeeded)
            {
                OnSolutionClosed();
                OnSolutionOpenedOrProjectChanged();
                m_buildCancelResetNeeded = false;
            }
            else if (m_StateDictionary[PendingConst] > 0)
            {
                var nodes = m_Nodes.Where(e => e.ParentNode != null && e.State == ProjectNodeState.Pending);
                foreach (ProjectNode node in nodes)
                {
                    node.State = ProjectNodeState.Skipped;
                    ListViewItem item = lvView.Items[node.Name];
                    item.SubItems[3].Text = "Skipped";
                    item.SubItems[4].Text = string.Empty;
                    item.Tag = node;

                    m_StateDictionary[SkippedConst]++;
                }

                m_StateDictionary[PendingConst] = 0;
                lvView.Invalidate();
                UpdateDictionaryStatus();
            }
        }

        private void OnProjectBuildStart(BuildProjectConfigEventArg e)
        {
            ProjectNode node = m_Nodes.FirstOrDefault(i => i.UniqueName == e.Project);
            if (node == null)
                return;

            node.ResetErrors();
            node.Platform = e.Platform;
            node.ProjectConfig = e.ProjectConfig;
            node.State = ProjectNodeState.Building;
            node.BuildStartTime = DateTime.Now;

            ListViewItem item = lvView.Items[node.Name];
            item.Tag = node;
            item.SubItems[2].Text = node.ProjectConfig;
            item.SubItems[3].Text = "Building";

            // Update state information.
            m_StateDictionary[PendingConst]--;
        }

        private void OnProjectBuildEnd(BuildProjectConfigEventArg e)
        {
            ProjectNode node = m_Nodes.FirstOrDefault(i => i.UniqueName == e.Project);
            if (node == null)
                return;

            node.State = (e.Success) ? ProjectNodeState.Done : ProjectNodeState.Error;
            node.BuildEndTime = DateTime.Now;

            ListViewItem item = lvView.Items[node.Name];
            item.SubItems[3].Text = e.Success ? "Done" : "Error";

            // Calculate the difference in build time.
            TimeSpan tsDiff = node.BuildEndTime.Subtract(node.BuildStartTime);
            string outputString = tsDiff.Seconds > 0 ?
                string.Format("{0}s {1}ms", tsDiff.Seconds, tsDiff.Milliseconds) :
                string.Format("{0}ms", tsDiff.Milliseconds);

            item.SubItems[4].Text = outputString;

            if (e.Success)
            {
                // It's possible that the project was skipped and we'll know from 
                // the time delta :-).
                if (tsDiff.Seconds == 0 && tsDiff.Milliseconds == 0)
                {
                    m_StateDictionary[SkippedConst]++;
                    node.State = ProjectNodeState.Skipped;
                    item.SubItems[3].Text = "Skipped";
                }
                else
                    m_StateDictionary[CompletedConst]++;
            }
            else
            {
                // Let's find out why this project failed to build.
                m_StateDictionary[FailedConst]++;
            }

            // Update the status bar.
            progBar.Value++;
            progBar.Invalidate();

            UpdateDictionaryStatus();

            // Check for any errors associated with this project.
            if (m_StateDictionary[FailedConst] > 0)
            {
                ErrorItems errorItems = _applicationObject.ToolWindows.ErrorList.ErrorItems;
                for (int i = 0; i < errorItems.Count; i++)
                {
                    ErrorItem errorItem = errorItems.Item(i + 1);
                    if (errorItem.Project.Equals(node.UniqueName))
                        node.AddError(errorItem);
                }

                // Cover both cases here.
                if (!e.Success)
                {
                    if (node.ErrorCollection.Count > 0)
                        item.SubItems[3].Text = string.Format("{0} {1}", node.ErrorCollection.Count.ToString(), node.ErrorCollection.Count == 1 ? "Error" : "Errors");
                    else
                        item.SubItems[3].Text = "Error";
                }
            }

            // Update the list view item node with all data.
            item.Tag = node;
        }

        #endregion Event Methods

        #region Private Methods

        private void ShowProgressElements()
        {
            tsSep1.Visible = tsSep2.Visible = true;
            btnCancelBuild.Visible = progBar.Visible = true;
        }

        private void HideProgressElements()
        {
            tsSep1.Visible = tsSep2.Visible = false;
            btnCancelBuild.Visible = progBar.Visible = false;
        }

        private void ResetProgressBar()
        {
            progBar.Value = 0;
            progBar.Maximum = 100;
        }

        private void SetProgressBarMax(int maximum)
        {
            progBar.Value = 0;
            progBar.Minimum = 0;
            progBar.Maximum = maximum;
        }

        private void ResetDictionary()
        {
            m_StateDictionary[CompletedConst] = 0;
            m_StateDictionary[FailedConst] = 0;
            m_StateDictionary[SkippedConst] = 0;
            m_StateDictionary[PendingConst] = 0;
            m_StateDictionary[ProjectCountConst] = 0;
        }

        private void UpdateDictionaryStatus()
        {
            btnDone.Text = string.Format("{0} Done", m_StateDictionary[CompletedConst].ToString());
            btnFailed.Text = string.Format("{0} Failed", m_StateDictionary[FailedConst].ToString());
            btnSkipped.Text = string.Format("{0} Skipped", m_StateDictionary[SkippedConst].ToString());
            btnPending.Text = string.Format("{0} Pending", m_StateDictionary[PendingConst].ToString());
            lblProjectCount.Text = string.Format("{0} Project{1}", m_StateDictionary[ProjectCountConst].ToString(), m_StateDictionary[ProjectCountConst] == 1 ? "" : "s");
        }

        ProjectNode rootNode = null;

        private void ScanSolution2()
        {
            Solution masterSolution = this._applicationObject.Solution;
            Projects solutionProjects = masterSolution.Projects;

            try
            {
                if (solutionProjects.Count > 0)
                {
                    // Clear all existing nodes out since this could be a rebuild of the list.
                    lvView.Items.Clear();
                    m_Nodes.Clear();

                    // Add the solution node as the root node.
                    rootNode = new ProjectNode(masterSolution.GetSolutionName(), VSProjectType.Solution, null);
                    m_Nodes.Add(rootNode);

                    ISolution solution = masterSolution.ScanSolution();
                    if (solution.HasProjects)
                    {
                        foreach (ISolutionProject project in solution.SolutionProjectCollection.OrderBy(i => i.Name))
                        {
                            ProjectNode node = new ProjectNode(project.Name, project.ProjectType, rootNode, project.UniqueName);
                            m_Nodes.Add(node);
                        }
                    }

                    // Build the nodes out into the list view for visual representation.
                    BuildListViewItems();

                    // Update the status of the state dictionary and status buttons.
                    ResetDictionary();
                    m_StateDictionary[ProjectCountConst] = m_Nodes.Count(i => i.ParentNode != null);
                    UpdateDictionaryStatus();

                    solution.Dispose();
                }
            }
            catch
            {
                // Un-comment to see any errors.
                //MessageBox.Show(ex.Message);
            }
        }

        private void NavigateProject(Project project)
        {
            ProjectNode childNode = new ProjectNode(project.GetProjectName(), VSProjectType.SolutionItems, null);
            m_Nodes.Add(childNode);

            NavigateProjectItems(project.ProjectItems, childNode, project);
        }

        private void NavigateProjectItems(ProjectItems colProjectItems, ProjectNode parentNode, Project project)
        {
            if (colProjectItems == null)
                return;

            foreach (ProjectItem projItem in colProjectItems)
            {
                if (projItem.Object is Project)
                {
                    Project thisProject = projItem.Object as Project;
                    VSProjectType projType = VSUtils.DetermineProjectType(thisProject.Kind);
                    switch (projType)
                    {
                        case VSProjectType.CPlusPlusProject:
                        case VSProjectType.CSharpProject:
                        case VSProjectType.SDECSharpProject:
                        case VSProjectType.FSharpProject:
                        case VSProjectType.VBProject:
                        case VSProjectType.SDEVBProject:
                        case VSProjectType.VJSharpProject:
                            ProjectNode node = new ProjectNode(projItem.Name, projType, parentNode, thisProject.UniqueName);
                            m_Nodes.Add(node);
                            break;
                    }
                }
                else
                {
                    if (projItem.SubProject != null)
                        NavigateProject(projItem.SubProject);
                }
            }
        }

        private void ScanSolution()
        {
            Solution masterSolution = this._applicationObject.Solution;
            Projects solutionProjects = masterSolution.Projects;

            if (solutionProjects.Count > 0)
            {
                // Clear all existing nodes out since this could be a rebuild of the list.
                lvView.Items.Clear();
                m_Nodes.Clear();

                // Add the solution node as the root node.
                ProjectNode rootNode = new ProjectNode(masterSolution.GetSolutionName(), VSProjectType.Solution, null);
                m_Nodes.Add(rootNode);

                // Scan through all of the projects to find all of the necessary projects and project items.
                foreach (Project project in solutionProjects)
                {
                    VSProjectType projType = VSUtils.DetermineProjectType(project);
                    switch (projType)
                    {
                        case VSProjectType.SolutionItems:
                            if (project.DoesSolutionItemHaveProjects())
                            {
                                ProjectNode childNode = new ProjectNode(project.GetProjectName(), VSProjectType.SolutionItems, null);
                                m_Nodes.Add(childNode);

                                // Scan the depth of the solution item for more projects.
                                //ScanProject(project, childNode);
                            }
                            break;

                        // Note: For the unsuspecting culprit projects these aren't quite identified well.
                        case VSProjectType.Unmodeled:
                        case VSProjectType.CPlusPlusProject:
                        case VSProjectType.CSharpProject:
                        case VSProjectType.SDECSharpProject:
                        case VSProjectType.FSharpProject:
                        case VSProjectType.VBProject:
                        case VSProjectType.SDEVBProject:
                        case VSProjectType.VJSharpProject:
                            ProjectNode solutionChildProject = new ProjectNode(project.GetProjectName(), projType, rootNode, project.UniqueName);
                            m_Nodes.Add(solutionChildProject);
                            break;
                    }
                }

                // Build the nodes out into the list view for visual representation.
                BuildListViewItems();

                // Update the status of the state dictionary and status buttons.
                ResetDictionary();
                m_StateDictionary[ProjectCountConst] = m_Nodes.Count(i => i.ParentNode != null);
                UpdateDictionaryStatus();
            }
        }

        private void BuildListViewItems()
        {
            // Nothing was found so don't process.
            if (m_Nodes.Count == 0)
                return;

            // Prevent drawing from happening.
            lvView.BeginUpdate();

            // Create the solution group.
            ProjectNode solutionNode = m_Nodes.First(e => e.ProjType == VSProjectType.Solution);
            ListViewGroup solutionGroup = new ListViewGroup(solutionNode.Name, solutionNode.Name);
            solutionGroup.Tag = solutionNode;
            lvView.Groups.Add(solutionGroup);

            // Find all nodes that fall directly under the current solution group.
            foreach (ProjectNode node in m_Nodes.Where(e => e.ParentNode == solutionNode))
            {
                ListViewItem viewItem = new ListViewItem(
                    new string[] { node.Name, node.GetProjectTypeAsString(), string.Empty, node.GetStateAsString(), string.Empty },
                    GetImageKeyForProjectType(node.ProjType),
                    solutionGroup
                );
                viewItem.Name = node.Name;
                viewItem.Tag = node;
                lvView.Items.Add(viewItem);
            }

            // Find every other top level node so we can list it.
            foreach (ProjectNode node in m_Nodes.Where(e => e.ProjType == VSProjectType.SolutionItems))
            {
                ListViewGroup childGroup = new ListViewGroup(node.Name, node.Name);
                childGroup.Tag = node;
                lvView.Groups.Add(childGroup);

                // Find all nodes that relate to this grouped node so we can assign it accordingly.
                foreach (ProjectNode projectNode in m_Nodes.Where(e => e.ParentNode == node))
                {
                    ListViewItem viewItem = new ListViewItem(
                        new string[] { projectNode.Name, projectNode.GetProjectTypeAsString(), string.Empty, projectNode.GetStateAsString(), string.Empty },
                        GetImageKeyForProjectType(projectNode.ProjType),
                        childGroup
                    );
                    viewItem.Name = projectNode.Name;
                    viewItem.Tag = projectNode;
                    lvView.Items.Add(viewItem);
                }
            }

            // Allow all changes to be rendered.
            lvView.EndUpdate();
        }

        private string GetImageKeyForProjectType(VSProjectType projType)
        {
            return string.Empty;
        }

        private Bitmap GetImageProjectState(ProjectNodeState state)
        {
            Bitmap bmp = null;
            switch (state)
            {
                case ProjectNodeState.Building:
                    bmp = resHero.waiting;
                    break;

                case ProjectNodeState.Done:
                    bmp = resHero.Check;
                    break;

                case ProjectNodeState.Error:
                    bmp = resHero.exclamation_red;
                    break;

                case ProjectNodeState.Pending:
                    bmp = resHero.pending;
                    break;

                case ProjectNodeState.Skipped:
                    bmp = resHero.skip;
                    break;
            }
            return bmp;
        }

        private Bitmap GetImageForProjectType(VSProjectType projType)
        {
            Bitmap bmp = null;
            switch (projType)
            {
                case VSProjectType.CPlusPlusProject:
                    bmp = resHero.cppproject_node;
                    break;

                case VSProjectType.CSharpProject:
                case VSProjectType.SDECSharpProject:
                    bmp = resHero.csharpproject_node;
                    break;

                case VSProjectType.SDEVBProject:
                case VSProjectType.VBProject:
                    bmp = resHero.vbproject_node;
                    break;

                case VSProjectType.VJSharpProject:
                    bmp = resHero.fsharpproject_node;
                    break;

                default:
                    // Do nothing here
                    bmp = resHero.rbproject_node;
                    break;
            }
            return bmp;
        }

        #endregion Private Methods

        private void btnCancelBuild_Click(object sender, EventArgs e)
        {
            _applicationObject.ExecuteCommand("Build.Cancel");
            m_buildCancelResetNeeded = true;
        }

        private void lvView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            Graphics g = e.Graphics;
            using (StringFormat stringFormat = new StringFormat())
            {
                // The text drawn for the header should conform accordingly to the
                // header column specifications for rendered text so it looks good.
                switch (e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        stringFormat.Alignment = StringAlignment.Center;
                        break;

                    case HorizontalAlignment.Right:
                        stringFormat.Alignment = StringAlignment.Far;
                        break;
                }

                // Draw the background.
                e.DrawBackground();

                // Draw the header text now with high quality compositing mode.
                using (Font headerFont = new Font(DefaultFont, 12, FontStyle.Bold, GraphicsUnit.Pixel))
                {
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    g.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds, stringFormat);
                }
            }
        }

        private void lvView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            Graphics g = e.Graphics;
            VSTheme theme = VSThemeManager.Manager.CurrentTheme;
            ProjectNode node = e.Item.Tag as ProjectNode;

            if ((e.State & ListViewItemStates.Selected) != 0)
            {
                using (SolidBrush brush = new SolidBrush(VSColors.StatusBarNormal))
                {
                    g.FillRectangle(brush, e.Bounds);
                    e.DrawFocusRectangle();
                    node.IsSelected = true;
                }
            }
            else
            {
                e.DrawBackground();
                node.IsSelected = false;
            }

            if (lvView.View != View.Details)
                e.DrawText();
        }

        private void lvView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Graphics g = e.Graphics;
            ProjectNode node = e.Item.Tag as ProjectNode;
            Font fFont = new Font(DefaultFont, 12.0f, FontStyle.Regular, GraphicsUnit.Pixel);
            Bitmap bmp = GetImageForProjectType(node.ProjType);
            SolidBrush brush = new SolidBrush(node.IsSelected ? Color.White : Color.Black);

            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            switch (e.ColumnIndex)
            {
                // Name
                case 0:
                    g.DrawImageUnscaled(bmp, e.Bounds.X, e.Bounds.Y);
                    g.DrawString(e.Item.Text, fFont, brush, new PointF(bmp.Width + 2, e.Bounds.Y));
                    break;

                // Type
                case 1:
                    g.DrawString(e.SubItem.Text ?? string.Empty, fFont, brush, new PointF(e.Bounds.X, e.Bounds.Y));
                    break;

                // Configuration
                case 2:
                    g.DrawString(e.SubItem.Text ?? string.Empty, fFont, brush, new PointF(e.Bounds.X, e.Bounds.Y));
                    break;

                // Status
                case 3:
                    if (node.State != ProjectNodeState.Idle)
                    {
                        Bitmap tmpBmp = GetImageProjectState(node.State);
                        g.DrawImageUnscaled(tmpBmp, e.Bounds.X, e.Bounds.Y);

                        // If the node is currently in an error state we'll need to treat it differently.
                        if (node.State == ProjectNodeState.Error)
                        {
                            using (SolidBrush errorBrush = new SolidBrush(Color.Red))
                            {
                                using (Font errorFont = new Font(DefaultFont, 12.0f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Pixel))
                                {
                                    g.DrawString(e.SubItem.Text ?? string.Empty, errorFont, errorBrush, new PointF(e.Bounds.X + tmpBmp.Width + 3, e.Bounds.Y));
                                }
                            }
                        }
                        else
                            g.DrawString(e.SubItem.Text ?? string.Empty, fFont, brush, new PointF(e.Bounds.X + tmpBmp.Width + 3, e.Bounds.Y));
                    }
                    else
                        g.DrawString(e.SubItem.Text ?? string.Empty, fFont, brush, new PointF(e.Bounds.X, e.Bounds.Y));
                    break;

                // Completed
                case 4:
                    g.DrawString(e.SubItem.Text ?? string.Empty, fFont, brush, new PointF(e.Bounds.X, e.Bounds.Y));
                    break;
            }

            // Dispose of all resources temporarily used here.
            fFont.Dispose();
            fFont = null;

            brush.Dispose();
            brush = null;
        }

        private void lvView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            lvView.Invalidate();
        }
    }
}
