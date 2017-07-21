namespace ProjectHero2
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;
    using ProjectHero2.Core;
    using System.Windows.Forms;
    using EnvDTE80;
    using EnvDTE;

    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("a82bd635-fef3-4cef-bf8b-df99c8f8063e")]
    public class ProjectHeroToolWindow : ToolWindowPane
    {
        public ProjectHeroToolWindow() : base(null)
        {
            this.Caption = "Project Hero 2";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = null;
        }

        override public IWin32Window Window
        {
            get
            {
                return (IWin32Window)ProjectHeroFactory.SharedInstance.HeroControl;
            }
        }
    }
}