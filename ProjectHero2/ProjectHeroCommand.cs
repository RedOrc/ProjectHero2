/**
 * Copyright (c) 2017 Alphonso Turner
 * All Rights Reserved
 */
 
using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.Text;
using ProjectHero2.Core;

namespace ProjectHero2
{
    internal sealed class ProjectHeroCommand
    {
        public const int ShowProjectHeroCommandId = 0x0100;
        public const int ShowProjectHeroSettingsCommandId = 0x0200;
        public const int ShowProjectHeroAboutCommandId = 0x0300;

        private const string ABOUT_ASCII = "✌(◕‿-)✌";

        // Command menu group
        public static readonly Guid CommandSet = new Guid("785834e7-b99f-45ae-b043-3c8db402b65a");

        // VS Package that provides this command, not null.
        private readonly Package package;

        // Tool Window that contains our user control
        private ProjectHeroToolWindow window;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectHeroCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private ProjectHeroCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                commandService.AddCommand(new MenuCommand(delegate (object sender, EventArgs e) {
                    window = (ProjectHeroToolWindow)this.package.FindToolWindow(typeof(ProjectHeroToolWindow), 0, true);
                    if ((window == null) || (window.Frame == null))
                    {
                        throw new NotSupportedException("Cannot create tool window for Project Hero");
                    }

                    IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
                    Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
                }, new CommandID(CommandSet, ShowProjectHeroCommandId)));

                commandService.AddCommand(new MenuCommand(delegate (object sender, EventArgs e)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine("Project Hero v1.0");
                    builder.AppendLine();
                    builder.AppendLine("Coded by Alphonso T.");
                    builder.AppendLine();
                    builder.AppendLine(ABOUT_ASCII);
                    builder.AppendLine();
                    builder.AppendLine("Copyright (C) 2017. All Rights Reserved");

                    VsShellUtilities.ShowMessageBox(
                        this.ServiceProvider,
                        builder.ToString(),
                        "About Project Hero 2",
                        OLEMSGICON.OLEMSGICON_INFO,
                        OLEMSGBUTTON.OLEMSGBUTTON_OK,
                        OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST
                    );

                }, new CommandID(CommandSet, ShowProjectHeroAboutCommandId)));
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static ProjectHeroCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new ProjectHeroCommand(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
        }
    }
}
