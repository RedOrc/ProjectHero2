using System;
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
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            if (ProjectHeroSettingManager.Manager.PluginSettings != null)
            {
                ProjectHeroSettings settings = ProjectHeroSettingManager.Manager.PluginSettings;
                chkDisplayOnStartup.Checked = settings.DisplayOnStartup;
                chkDisplayOnSolutionAddRemove.Checked = settings.DisplayOnSolutionChange;
                chkDisplayOnBuildStart.Checked = settings.DisplayOnBuildStart;
                chkOverrideVSOutputWindow.Checked = settings.OverrideVSOutputWindow;
                chkEnableQuickSync.Checked = settings.EnableQuickSync;
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            ProjectHeroSettingManager.Manager.PluginSettings.DisplayOnBuildStart = chkDisplayOnBuildStart.Checked;
            ProjectHeroSettingManager.Manager.PluginSettings.DisplayOnSolutionChange = chkDisplayOnSolutionAddRemove.Checked;
            ProjectHeroSettingManager.Manager.PluginSettings.DisplayOnStartup = chkDisplayOnStartup.Checked;
            ProjectHeroSettingManager.Manager.PluginSettings.OverrideVSOutputWindow = chkOverrideVSOutputWindow.Checked;
            ProjectHeroSettingManager.Manager.PluginSettings.EnableQuickSync = chkEnableQuickSync.Checked;

            ProjectHeroSettingManager.Manager.SaveSettings();
            this.Close();
        }
    }
}
