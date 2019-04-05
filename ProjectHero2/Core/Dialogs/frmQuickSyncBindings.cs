using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectHero2.Core.Dialogs
{
    public partial class frmQuickSyncBindings : Form
    {
        private IEnumerable<ProjectHero2.Core.ucProjectHero.AvailableProjectNode> _projectNodes;

        public frmQuickSyncBindings()
        {
            InitializeComponent();
        }

        public frmQuickSyncBindings(IEnumerable<ProjectHero2.Core.ucProjectHero.AvailableProjectNode> projectNodes)
            : this()
        {
            this._projectNodes = projectNodes;
        }

        private void frmQuickSyncBindings_Load(object sender, EventArgs e)
        {
            if (this._projectNodes == null)
            {
                btnAddProject.Enabled = false;
            }

            LoadListBox();
            LoadTreeView();
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (treeProjects.Nodes.Count == 0)
                return;

            if (ProjectHeroSettingManager.Manager.PluginSettings.QuickSyncAssociations == null)
                ProjectHeroSettingManager.Manager.PluginSettings.QuickSyncAssociations = new List<SourceProjectBinding>();

            ProjectHeroSettingManager.Manager.SaveSettings();

            lblStatus.Text = string.Format("Changes successfully saved. Last updated on {0}", DateTime.Now.ToString("G"));
        }

        private void btnAddProject_Click(object sender, EventArgs e)
        {
            using (frmProjectSelection frmProjectSelect = new frmProjectSelection(_projectNodes))
            {
                frmProjectSelect.ShowDialog();

                if (frmProjectSelect.Tag == null)
                    return;

                List<ProjectHero2.Core.ucProjectHero.AvailableProjectNode> projectCollection = frmProjectSelect.Tag as List<ProjectHero2.Core.ucProjectHero.AvailableProjectNode>;
                foreach (ProjectHero2.Core.ucProjectHero.AvailableProjectNode project in projectCollection)
                {
                    if (ProjectHeroSettingManager.Manager.PluginSettings.QuickSyncAssociations.Where(i => i.MD5Hash.Equals(project.Md5Hash)).Count() > 0)
                        continue;

                    SourceProjectBinding binding = new SourceProjectBinding();
                    binding.MD5Hash = project.Md5Hash;
                    binding.ProjectFilePath = project.FilePath;
                    binding.ProjectName = project.Name;
                    binding.ProjectNickname = string.Empty;

                    ProjectHeroSettingManager.Manager.PluginSettings.QuickSyncAssociations.Add(binding);
                }

                LoadTreeView();
            }
        }

        private void btnRemoveAllProjects_Click(object sender, EventArgs e)
        {
            if (treeProjects.Nodes.Count == 0)
                return;

            if (MessageBox.Show("Are you sure you want to remove all projects?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                lbFolders.BeginUpdate();
                lbFolders.Items.Clear();
                lbFolders.EndUpdate();

                foreach (TreeNode node in treeProjects.Nodes)
                {
                    DisposeOfNode(node);

                    SourceProjectBinding binding = node.Tag as SourceProjectBinding;
                    ProjectHeroSettingManager.Manager.PluginSettings.QuickSyncAssociations.Remove(binding);
                }

                treeProjects.BeginUpdate();
                treeProjects.Nodes.Clear();
                treeProjects.EndUpdate();

                propGrid.SelectedObject = null;

                btnSaveChanges_Click(null, null);
                LoadTreeView();
            }
        }

        private void treeProjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = treeProjects.SelectedNode;

            if (selectedNode.Tag is SourceProjectBinding)
            {
                SourceProjectBinding binding = selectedNode.Tag as SourceProjectBinding;
                if (binding.DestinationFolderCollection.Count > 0)
                {
                    lbFolders.BeginUpdate();

                    lbFolders.Items.Clear();

                    foreach (DestinationFolderBinding folderBinding in binding.DestinationFolderCollection.OrderBy(i => i.FolderName))
                        lbFolders.Items.Add(folderBinding);

                    lbFolders.ContextMenuStrip.Enabled = true;

                    lbFolders.EndUpdate();
                }
                else
                {
                    lbFolders.Items.Clear();
                    lbFolders.ContextMenuStrip.Enabled = false;
                }

                if (selectedNode.Nodes.Count > 0)
                {
                    selectedNode.ContextMenuStrip.Items["removeAllAssociations"].Enabled = true;

                    bool isSelected = false;
                    foreach (TreeNode childNode in selectedNode.Nodes)
                    {
                        if (childNode.Checked)
                        {
                            isSelected = true;
                            break;
                        }
                    }
                }
                else
                {
                    selectedNode.ContextMenuStrip.Items["removeAllAssociations"].Enabled = false;
                }

                propGrid.SelectedObject = binding;
            }
            else
            {
                lbFolders.BeginUpdate();
                lbFolders.Items.Clear();
                lbFolders.EndUpdate();
                lbFolders.ContextMenuStrip.Enabled = false;

                propGrid.SelectedObject = selectedNode.Tag as SysFileItem;
            }
        }

        private int GetImageIndexForProjType(VSProjectType projType)
        {
            int imageIndex;
            switch (projType)
            {
                case VSProjectType.CPlusPlusProject: imageIndex = 0; break;
                case VSProjectType.CSharpProject:
                case VSProjectType.SDECSharpProject: imageIndex = 1; break;
                case VSProjectType.FSharpProject: imageIndex = 2; break;
                case VSProjectType.VBProject:
                case VSProjectType.SDEVBProject: imageIndex = 4; break;
                default: imageIndex = 5; break;
            }
            return imageIndex;
        }

        private void DisposeOfNode(TreeNode n)
        {
            if (n.ContextMenuStrip != null)
            {
                n.ContextMenuStrip.Dispose();
                n.ContextMenuStrip = null;
            }

            if (n.Nodes.Count > 0)
            {
                foreach (TreeNode childNode in n.Nodes)
                    DisposeOfNode(childNode);
            }
        }

        private ContextMenuStrip GetChildLevelNodeContextMenu()
        {
            ContextMenuStrip ctxMenu = new ContextMenuStrip();

            // ================================================================================
            // Provide the ability to remove this node.
            // ================================================================================
            ctxMenu.Items.Add("Remove item", null, new EventHandler(delegate (object sender, EventArgs e)
            {
                if (MessageBox.Show("Are you sure you want to remove this item?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    treeProjects.BeginUpdate();

                    TreeNode selectedNode = treeProjects.SelectedNode;
                    SysFileItem item = selectedNode.Tag as SysFileItem;

                    TreeNode parentNode = selectedNode.Parent;
                    SourceProjectBinding binding = parentNode.Tag as SourceProjectBinding;

                    binding.SyncAssociationCollection.Remove(item);
                    parentNode.Tag = binding;

                    DisposeOfNode(selectedNode);
                    parentNode.Nodes.Remove(selectedNode);

                    treeProjects.EndUpdate();
                }
            }));

            return ctxMenu;
        }

        private ContextMenuStrip GetTopLevelNodeContextMenu()
        {
            ContextMenuStrip ctxMenu = new ContextMenuStrip();
            ToolStripItem tsItem = null;

            // ================================================================================
            // Provide the ability to add files and folders for the selected project.
            // ================================================================================
            tsItem = ctxMenu.Items.Add("Add Files/Folders", null, new EventHandler(delegate (object sender, EventArgs e)
            {
                if (treeProjects.SelectedNode == null)
                    return;

                TreeNode selectedNode = treeProjects.SelectedNode;
                SourceProjectBinding binding = selectedNode.Tag as SourceProjectBinding;

                using (frmFileFolderSelectionDialog ofd = new frmFileFolderSelectionDialog(binding.ProjectFilePath))
                {
                    ofd.ShowDialog();

                    if (ofd.Tag != null && ofd.Tag is List<SysFileItem>)
                    {
                        treeProjects.BeginUpdate();

                        List<SysFileItem> fileList = ofd.Tag as List<SysFileItem>;
                        foreach (SysFileItem file in fileList)
                        {
                            if (binding.SyncAssociationCollection.Count > 0 && binding.SyncAssociationCollection.Where(i => i.Name.ToLower().Equals(file.Name.ToLower())).Count() > 0)
                                continue;

                            int childImageIndex = (file.FileType == FileType.Directory) ? 7 : 6;
                            TreeNode childNode = new TreeNode(file.Name, childImageIndex, childImageIndex);
                            childNode.Tag = file;
                            childNode.ContextMenuStrip = GetChildLevelNodeContextMenu();

                            selectedNode.Nodes.Add(childNode);
                            binding.SyncAssociationCollection.Add(file);
                        }

                        selectedNode.Tag = binding;

                        treeProjects.EndUpdate();
                    }
                }
            }));
            tsItem.Name = "addFolder";

            // ================================================================================
            // Provide the ability to add source folders.
            // ================================================================================
            tsItem = ctxMenu.Items.Add("Add Sync Folder", null, new EventHandler(delegate (object sender, EventArgs e)
            {
                if (treeProjects.SelectedNode == null)
                    return;

                TreeNode selectedNode = treeProjects.SelectedNode;
                SourceProjectBinding binding = selectedNode.Tag as SourceProjectBinding;

                if (binding == null)
                    return;

                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    fbd.Description = "Select a folder to sync files to.";
                    fbd.ShowNewFolderButton = true;

                    fbd.ShowDialog();

                    if (!string.IsNullOrEmpty(fbd.SelectedPath))
                    {
                        string dirName = fbd.SelectedPath.Split('\\').Reverse().First();
                        if (binding.DestinationFolderCollection.Count > 0)
                        {
                            DestinationFolderBinding destFolderBinding = binding.DestinationFolderCollection.FirstOrDefault(i => i.FolderPath.ToLower().Equals(fbd.SelectedPath.ToLower().Trim()));

                            if (destFolderBinding != null)
                                return;
                        }

                        DestinationFolderBinding newFolderBinding = new DestinationFolderBinding(dirName, fbd.SelectedPath);

                        binding.DestinationFolderCollection.Add(newFolderBinding);
                        lbFolders.Items.Add(newFolderBinding);
                        lbFolders.ContextMenuStrip.Enabled = true;

                        selectedNode.Tag = binding;
                    }
                }
            }));
            tsItem.Name = "addSyncFolder";

            // ================================================================================
            // Provide the ability to remove all associations for the selected project.
            // ================================================================================
            tsItem = ctxMenu.Items.Add("Remove all associations", null, new EventHandler(delegate (object sender, EventArgs e)
            {
                if (treeProjects.SelectedNode == null)
                    return;

                TreeNode selectedNode = treeProjects.SelectedNode;
                SourceProjectBinding binding = selectedNode.Tag as SourceProjectBinding;

                if (selectedNode.Nodes.Count == 0)
                    return;

                if (MessageBox.Show("Are you sure you want to remove all associations?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    treeProjects.BeginUpdate();

                    if (selectedNode.Nodes.Count > 0)
                    {
                        foreach (TreeNode childNode in selectedNode.Nodes)
                            DisposeOfNode(childNode);
                    }

                    selectedNode.Nodes.Clear();

                    binding.SyncAssociationCollection.Clear();
                    binding.DestinationFolderCollection.Clear();

                    lbFolders.Items.Clear();

                    selectedNode.Tag = binding;

                    treeProjects.EndUpdate();
                }
            }));
            tsItem.Name = "removeAllAssociations";

            // ================================================================================
            // Provide the ability to remove selected items only.
            // ================================================================================
            tsItem = ctxMenu.Items.Add("Remove selected items", null, new EventHandler(delegate (object sender, EventArgs e)
            {
                if (treeProjects.SelectedNode == null)
                    return;

                TreeNode selectedNode = treeProjects.SelectedNode;
                SourceProjectBinding binding = selectedNode.Tag as SourceProjectBinding;

                if (selectedNode.Nodes.Count == 0)
                    return;

                bool hasItemsSelected = false;
                foreach (TreeNode childNode in selectedNode.Nodes)
                {
                    if (childNode.Checked)
                    {
                        hasItemsSelected = true;
                        break;
                    }
                }

                if (!hasItemsSelected)
                    return;

                if (MessageBox.Show("Are you sure you want to remove the selected items?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    treeProjects.BeginUpdate();

                    ArrayList tmpNodeList = new ArrayList();
                    foreach (TreeNode childNode in selectedNode.Nodes)
                    {
                        if (childNode.Checked)
                        {
                            SysFileItem item = childNode.Tag as SysFileItem;
                            binding.SyncAssociationCollection.Remove(item);

                            DisposeOfNode(childNode);

                            tmpNodeList.Add(childNode);
                        }
                    }

                    foreach (TreeNode node in tmpNodeList)
                        selectedNode.Nodes.Remove(node);

                    selectedNode.Tag = binding;

                    treeProjects.EndUpdate();
                }
            }));
            tsItem.Name = "removeSelectedItems";

            // ================================================================================
            // Provide the ability to remove the selected node.
            // ================================================================================
            tsItem = ctxMenu.Items.Add("Remove Project", null, new EventHandler(delegate (object sender, EventArgs e)
            {
                if (treeProjects.SelectedNode == null)
                    return;

                TreeNode selectedNode = treeProjects.SelectedNode;
                SourceProjectBinding binding = selectedNode.Tag as SourceProjectBinding;

                if (MessageBox.Show("Are you sure you want to remove this project?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    DisposeOfNode(selectedNode);
                    ProjectHeroSettingManager.Manager.PluginSettings.QuickSyncAssociations.Remove(binding);

                    treeProjects.Nodes.Remove(selectedNode);
                    propGrid.SelectedObject = null;

                    LoadTreeView();
                }
            }));
            tsItem.Name = "removeProject";

            return ctxMenu;
        }

        private void LoadListBox()
        {
            ContextMenuStrip ctxMenu = new ContextMenuStrip();
            ctxMenu.Items.Add("Remove folder", null, new EventHandler(delegate (object sender, EventArgs e)
            {
                if (lbFolders.SelectedItems == null || lbFolders.SelectedItems.Count == 0)
                    return;

                if (MessageBox.Show("Are you sure you want to remove the selected folders?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    ArrayList tmpList = new ArrayList();

                    lbFolders.BeginUpdate();

                    TreeNode selectedNode = treeProjects.SelectedNode;
                    SourceProjectBinding binding = selectedNode.Tag as SourceProjectBinding;

                    foreach (DestinationFolderBinding folder in lbFolders.Items)
                    {
                        tmpList.Add(folder);
                        binding.DestinationFolderCollection.Remove(folder);
                    }

                    foreach (DestinationFolderBinding folder in tmpList)
                        lbFolders.Items.Remove(folder);

                    selectedNode.Tag = binding;

                    lbFolders.EndUpdate();
                }
            }));

            lbFolders.ContextMenuStrip = ctxMenu;
        }

        private void LoadTreeView()
        {
            if (this._projectNodes == null)
                return;

            // ================================================================================
            // Let's properly dispose of the context menu's that currently exist.
            // ================================================================================
            if (treeProjects.Nodes.Count > 0)
            {
                foreach (TreeNode n in treeProjects.Nodes)
                    DisposeOfNode(n);

                treeProjects.Nodes.Clear();
            }

            if (lbFolders.Items.Count > 0)
                lbFolders.Items.Clear();

            treeProjects.BeginUpdate();

            if (ProjectHeroSettingManager.Manager.PluginSettings != null && ProjectHeroSettingManager.Manager.PluginSettings.QuickSyncAssociations != null)
            {
                foreach (SourceProjectBinding sourceBinding in ProjectHeroSettingManager.Manager.PluginSettings.QuickSyncAssociations.OrderBy(i => i.ProjectName))
                {
                    ProjectHero2.Core.ucProjectHero.AvailableProjectNode projNode = this._projectNodes.FirstOrDefault(i => i.Md5Hash.Equals(sourceBinding.MD5Hash));
                    if (projNode != null)
                    {
                        int imageIndex = GetImageIndexForProjType(projNode.ProjType);
                        TreeNode node = new TreeNode(
                            !string.IsNullOrEmpty(sourceBinding.ProjectNickname) ?
                            sourceBinding.ProjectNickname : sourceBinding.ProjectName,
                            imageIndex, imageIndex
                        );
                        sourceBinding.ProjectFilePath = projNode.FilePath;
                        node.Tag = sourceBinding;
                        node.ContextMenuStrip = GetTopLevelNodeContextMenu();

                        treeProjects.Nodes.Add(node);

                        if (sourceBinding.SyncAssociationCollection != null && sourceBinding.SyncAssociationCollection.Count > 0)
                        {
                            foreach (SysFileItem item in sourceBinding.SyncAssociationCollection.OrderBy(i => i.Name))
                            {
                                int childImageIndex = (item.FileType == FileType.Directory) ? 7 : 6;
                                TreeNode childNode = new TreeNode(item.Name, childImageIndex, childImageIndex);
                                childNode.Tag = item;
                                childNode.ContextMenuStrip = GetChildLevelNodeContextMenu();

                                node.Nodes.Add(childNode);
                            }
                        }
                    }
                }
            }

            treeProjects.EndUpdate();
        }

        private void lbFolders_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (lbFolders.Items.Count > 0 && lbFolders.SelectedItems != null && lbFolders.SelectedItems.Count > 0)
                {
                    if (MessageBox.Show("Are you sure you want to remove the selected folders?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        TreeNode selectedNode = treeProjects.SelectedNode;
                        SourceProjectBinding binding = selectedNode.Tag as SourceProjectBinding;

                        lbFolders.BeginUpdate();

                        ArrayList tmpItemsToRemove = new ArrayList();
                        foreach (DestinationFolderBinding folderBinding in lbFolders.SelectedItems)
                        {
                            tmpItemsToRemove.Add(folderBinding);
                            binding.DestinationFolderCollection.Remove(folderBinding);
                        }

                        foreach (DestinationFolderBinding folderBinding in tmpItemsToRemove)
                            lbFolders.Items.Remove(folderBinding);

                        selectedNode.Tag = binding;

                        lbFolders.EndUpdate();
                    }
                }
            }
        }

        private void propGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            TreeNode selectedNode = treeProjects.SelectedNode;
            SourceProjectBinding binding = selectedNode.Tag as SourceProjectBinding;

            treeProjects.BeginUpdate();

            selectedNode.Text = !string.IsNullOrEmpty(binding.ProjectNickname) ? binding.ProjectNickname : binding.ProjectName;

            treeProjects.EndUpdate();

            if (binding.DestinationFolderCollection.Count == 0)
                lbFolders.Items.Clear();
            else
            {
                lbFolders.BeginUpdate();
                lbFolders.Items.Clear();

                foreach (DestinationFolderBinding folder in binding.DestinationFolderCollection.OrderBy(i => i.FolderName))
                {
                    lbFolders.Items.Add(folder);
                }

                lbFolders.EndUpdate();
            }
        }
    }
}