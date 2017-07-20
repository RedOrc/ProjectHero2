using EnvDTE;
using EnvDTE80;
using ProjectHero2.Core.VSEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core
{
    /// <summary>
    /// Responsible for managing all Visual Studio events and routing
    /// them to the appropriate windows.
    /// </summary>
    internal class VSEventManager
    {
        private WindowVisibilityEvents _windowVisibilityEvents;
        private BuildEvents _buildEvents;
        private SolutionEvents _solutionEvents;
        private ProjectsEvents _projectEvents;
        private ProjectItemsEvents _projectItemEvents;
        private CommandEvents _commandEvents;

        private DTE2 _applicationObject;
        private VSWindowManager _winManager = VSWindowManager.Manager;

        public VSEventManager(DTE2 applicationObject)
        {
            this._applicationObject = applicationObject;
            Init();
        }

        private void Init()
        {
            Events2 events2 = (Events2)_applicationObject.Events;

            // Tool window visibility
            this._windowVisibilityEvents = events2.get_WindowVisibilityEvents();
            this._windowVisibilityEvents.WindowShowing += _windowVisibilityEvents_WindowShowing;
            this._windowVisibilityEvents.WindowHiding += _windowVisibilityEvents_WindowHiding;

            // Build Events
            this._buildEvents = events2.BuildEvents;
            this._buildEvents.OnBuildBegin += _buildEvents_OnBuildBegin;
            this._buildEvents.OnBuildDone += _buildEvents_OnBuildDone;
            this._buildEvents.OnBuildProjConfigBegin += _buildEvents_OnBuildProjConfigBegin;
            this._buildEvents.OnBuildProjConfigDone += _buildEvents_OnBuildProjConfigDone;

            // Solution Events
            this._solutionEvents = events2.SolutionEvents;
            this._solutionEvents.AfterClosing += _solutionEvents_AfterClosing;
            this._solutionEvents.BeforeClosing += _solutionEvents_BeforeClosing;
            this._solutionEvents.Opened += _solutionEvents_Opened;
            this._solutionEvents.ProjectAdded += _solutionEvents_ProjectAdded;
            this._solutionEvents.ProjectRemoved += _solutionEvents_ProjectRemoved;
            this._solutionEvents.ProjectRenamed += _solutionEvents_ProjectRenamed;
            this._solutionEvents.QueryCloseSolution += _solutionEvents_QueryCloseSolution;
            this._solutionEvents.Renamed += _solutionEvents_Renamed;

            // Project Events
            this._projectEvents = events2.ProjectsEvents;
            this._projectEvents.ItemAdded += _projectEvents_ItemAdded;
            this._projectEvents.ItemRemoved += _projectEvents_ItemRemoved;
            this._projectEvents.ItemRenamed += _projectEvents_ItemRenamed;

            // Project Item Events
            this._projectItemEvents = events2.ProjectItemsEvents;
            this._projectItemEvents.ItemAdded += ProjectItemsEvents_ItemAdded;
            this._projectItemEvents.ItemRemoved += _projectItemEvents_ItemRemoved;
            this._projectItemEvents.ItemRenamed += _projectItemEvents_ItemRenamed;

            // Command Events
            this._commandEvents = events2.CommandEvents;
            this._commandEvents.BeforeExecute += _commandEvents_BeforeExecute;
            this._commandEvents.AfterExecute += _commandEvents_AfterExecute;
        }

        public void Destroy()
        {
            // Cleanup window visibility events.
            this._windowVisibilityEvents.WindowShowing -= _windowVisibilityEvents_WindowShowing;
            this._windowVisibilityEvents.WindowHiding -= _windowVisibilityEvents_WindowHiding;

            // Cleanup build events.
            this._buildEvents.OnBuildBegin -= _buildEvents_OnBuildBegin;
            this._buildEvents.OnBuildDone -= _buildEvents_OnBuildDone;
            this._buildEvents.OnBuildProjConfigBegin -= _buildEvents_OnBuildProjConfigBegin;
            this._buildEvents.OnBuildProjConfigDone -= _buildEvents_OnBuildProjConfigDone;

            // Cleanup solution events.
            this._solutionEvents.AfterClosing -= _solutionEvents_AfterClosing;
            this._solutionEvents.BeforeClosing -= _solutionEvents_BeforeClosing;
            this._solutionEvents.Opened -= _solutionEvents_Opened;
            this._solutionEvents.ProjectAdded -= _solutionEvents_ProjectAdded;
            this._solutionEvents.ProjectRemoved -= _solutionEvents_ProjectRemoved;
            this._solutionEvents.ProjectRenamed -= _solutionEvents_ProjectRenamed;
            this._solutionEvents.QueryCloseSolution -= _solutionEvents_QueryCloseSolution;
            this._solutionEvents.Renamed -= _solutionEvents_Renamed;

            // Cleanup project events.
            this._projectEvents.ItemAdded -= _projectEvents_ItemAdded;
            this._projectEvents.ItemRemoved -= _projectEvents_ItemRemoved;
            this._projectEvents.ItemRenamed -= _projectEvents_ItemRenamed;

            // Cleanup project item events.
            this._projectItemEvents.ItemAdded -= ProjectItemsEvents_ItemAdded;
            this._projectItemEvents.ItemRemoved -= _projectItemEvents_ItemRemoved;
            this._projectItemEvents.ItemRenamed -= _projectItemEvents_ItemRenamed;
        }

        #region Command Events

        void _commandEvents_AfterExecute(string Guid, int ID, object CustomIn, object CustomOut)
        {
            CommandExecuteEventArg e = new CommandExecuteEventArg(Guid, ID, CustomIn, CustomOut, VSUtils.GetCommandByGuidAndID(_applicationObject, Guid, ID));
            this._winManager.SendMessageToAllWindows(VSEvents.AfterCommandExecute, e);
        }

        void _commandEvents_BeforeExecute(string Guid, int ID, object CustomIn, object CustomOut, ref bool CancelDefault)
        {
            CommandExecuteEventArg e = new CommandExecuteEventArg(Guid, ID, CustomIn, CustomOut, ref CancelDefault, VSUtils.GetCommandByGuidAndID(_applicationObject, Guid, ID));
            this._winManager.SendMessageToAllWindows(VSEvents.BeforeCommandExecute, e);
        }

        #endregion Command Events

        #region Project Item Events

        void _projectItemEvents_ItemRenamed(ProjectItem ProjectItem, string OldName)
        {
            ProjectItemEventArg e = new ProjectItemEventArg(ProjectItem, OldName);
            this._winManager.SendMessageToAllWindows(VSEvents.ProjectItemRenamed, e);
        }

        void _projectItemEvents_ItemRemoved(ProjectItem ProjectItem)
        {
            ProjectItemEventArg e = new ProjectItemEventArg(ProjectItem);
            this._winManager.SendMessageToAllWindows(VSEvents.ProjectItemRemoved, e);
        }

        void ProjectItemsEvents_ItemAdded(ProjectItem ProjectItem)
        {
            ProjectItemEventArg e = new ProjectItemEventArg(ProjectItem);
            this._winManager.SendMessageToAllWindows(VSEvents.ProjectItemAdded, e);
        }

        #endregion Project Item Events

        #region Project Events

        void _projectEvents_ItemRenamed(Project Project, string OldName)
        {
            ProjectEventArg e = new ProjectEventArg(Project, OldName);
            this._winManager.SendMessageToAllWindows(VSEvents.ProjectRenamed, e);
        }

        void _projectEvents_ItemRemoved(Project Project)
        {
            ProjectEventArg e = new ProjectEventArg(Project);
            this._winManager.SendMessageToAllWindows(VSEvents.ProjectRemoved, e);
        }

        void _projectEvents_ItemAdded(Project Project)
        {
            ProjectEventArg e = new ProjectEventArg(Project);
            this._winManager.SendMessageToAllWindows(VSEvents.ProjectAdded, e);
        }

        #endregion Project Events

        #region Solution Events

        void _solutionEvents_Renamed(string OldName)
        {
            SolutionProjectEventArg e = new SolutionProjectEventArg(null, OldName);
            this._winManager.SendMessageToAllWindows(VSEvents.SolutionRenamed, e);
        }

        void _solutionEvents_QueryCloseSolution(ref bool fCancel)
        {
            /* Not needed yet */
        }

        void _solutionEvents_ProjectRenamed(Project Project, string OldName)
        {
            SolutionProjectEventArg e = new SolutionProjectEventArg(Project, OldName);
            this._winManager.SendMessageToAllWindows(VSEvents.SolutionProjectRenamed, e);
        }

        void _solutionEvents_ProjectRemoved(Project Project)
        {
            SolutionProjectEventArg e = new SolutionProjectEventArg(Project);
            this._winManager.SendMessageToAllWindows(VSEvents.SolutionProjectRemoved, e);
        }

        void _solutionEvents_ProjectAdded(Project Project)
        {
            SolutionProjectEventArg e = new SolutionProjectEventArg(Project);
            this._winManager.SendMessageToAllWindows(VSEvents.SolutionProjectAdded, e);
        }

        void _solutionEvents_Opened()
        {
            this._winManager.SendMessageToAllWindows(VSEvents.SolutionOpened, null);
        }

        void _solutionEvents_BeforeClosing()
        {
            this._winManager.SendMessageToAllWindows(VSEvents.SolutionBeforeClosing, null);
        }

        void _solutionEvents_AfterClosing()
        {
            this._winManager.SendMessageToAllWindows(VSEvents.SolutionAfterClosing, null);
        }

        #endregion Solution Events

        #region Build Events

        // Will fire when a build operation completes. 
        // This event fires only once for a full solution or multiproject build operation.
        void _buildEvents_OnBuildDone(vsBuildScope Scope, vsBuildAction Action)
        {
            BuildCompleteEventArg e = new BuildCompleteEventArg(Scope, Action);
            this._winManager.SendMessageToAllWindows(VSEvents.BuildComplete, e);
        }

        // Will fire when any build operation is fired from the IDE. 
        // It fires only once for a full solution or multiproject build operation.
        void _buildEvents_OnBuildBegin(vsBuildScope Scope, vsBuildAction Action)
        {
            BuildBeginEventArg e = new BuildBeginEventArg(Scope, Action);
            this._winManager.SendMessageToAllWindows(VSEvents.BuildBegin, e);
        }

        // Will fire when a project build completes. This event is used to catch the completion
        // of each project build within a solution or multiproject build operation.
        void _buildEvents_OnBuildProjConfigDone(string Project, string ProjectConfig, string Platform, string SolutionConfig, bool Success)
        {
            BuildProjectConfigEventArg e = new BuildProjectConfigEventArg(Project, ProjectConfig, Platform, SolutionConfig, Success);
            this._winManager.SendMessageToAllWindows(VSEvents.BuildProjectConfigDone, e);
        }

        // Will fire when a project build begins. This event is used to catch each project
        // build event within a solution or multiproject build operation.
        void _buildEvents_OnBuildProjConfigBegin(string Project, string ProjectConfig, string Platform, string SolutionConfig)
        {
            BuildProjectConfigEventArg e = new BuildProjectConfigEventArg(Project, ProjectConfig, Platform, SolutionConfig, false);
            this._winManager.SendMessageToAllWindows(VSEvents.BuildProjectConfigBegin, e);
        }

        #endregion Build Events

        #region Window Visibility Events

        private void _windowVisibilityEvents_WindowHiding(EnvDTE.Window Window)
        {
            if (this._winManager.IsRegisteredWindow(Window.Caption))
            {
                if (Window is IThemeControl)
                    ((IThemeControl)Window.Object).AdjustView(VSToolWindowState.VS_HIDDEN);
            }
        }

        private void _windowVisibilityEvents_WindowShowing(EnvDTE.Window Window)
        {
            if (this._winManager.IsRegisteredWindow(Window.Caption))
            {
                if (Window.Object is IThemeControl)
                    ((IThemeControl)Window.Object).AdjustView(VSToolWindowState.VS_SHOWN);
            }
        }

        #endregion Window Visibility Events
    }
}
