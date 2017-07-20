using EnvDTE80;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectHero2.Core
{
    /// <summary>
    /// Holds a reference to a Visual Studio COM ToolWindow and the
    /// underlying control that is stored within the COM ToolWindow.
    /// 
    /// Visual Studio Extensions that utilize any sort of embedded UI
    /// that is dockable must reside in a User Control. 
    /// For that reason, whenever a window is created that hosts a 
    /// control, a reference will be exposed at creation time. 
    /// 
    /// We hold on to this reference so we may manipulate the control
    /// whenever necessary.
    /// </summary>
    public class VSWindowHandle
    {
        public IntPtr ControlHandle
        {
            get;
            set;
        }

        public Window2 Window
        {
            get;
            set;
        }

        public string ClassName
        {
            get;
            private set;
        }

        public string VSWindowTitle
        {
            get;
            private set;
        }

        public Control Control
        {
            get;
            set;
        }

        public VSWindowHandle(string className, string vsWindowTitle, IntPtr controlHandle, Window2 window)
        {
            this.ClassName = className;
            this.VSWindowTitle = vsWindowTitle;
            this.ControlHandle = controlHandle;
            this.Window = window;
        }
    }
}
