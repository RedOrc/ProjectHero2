using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectHero2.Core
{
    public partial class VSUserControlBase : UserControl, IThemeControl
    {
        public VSUserControlBase() :
            base()
        {

        }

        public virtual void SetTheme()
        {
            // Definition defined in derived user control class.
        }

        public virtual void AdjustView(VSToolWindowState state)
        {
            // Definition in derived control class.
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            // If the message captured is a custom message then we can safely
            // tell the derived control to execute its repaint routine.
            switch ((Win32ProjectHeroMessages)m.Msg)
            {
                case Win32ProjectHeroMessages.WM_ACEMSG_THEME_CHANGED:
                    SetTheme();
                    break;
            }

            base.WndProc(ref m);
        }
    }
}
