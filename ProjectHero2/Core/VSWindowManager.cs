using EnvDTE;
using EnvDTE80;
using ProjectHero2.Core.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core
{
    public sealed class VSWindowManager : IObjectLifecycle
    {
        private static VSWindowManager _manager = null;
        public static VSWindowManager Manager
        {
            get
            {
                if (_manager == null)
                    _manager = new VSWindowManager();

                return _manager;
            }
        }

        private readonly SortedList<string, VSWindowHandle> _vsHandleCollection;
        private DTE2 _applicationObject;
        private AddIn _addIn;

        private VSWindowManager()
        {
            this._vsHandleCollection = new SortedList<string, VSWindowHandle>();
        }

        /// <summary>
        /// Reserved for internal usage only.
        /// </summary>
        /// <param name="applicationObject"></param>
        /// <param name="addIn"></param>
        public void InitApplicationContext(ref DTE2 applicationObject, ref AddIn addIn)
        {
            this._applicationObject = applicationObject;
            this._addIn = addIn;
        }

        /// <summary>
        /// Returns true if the specified window title is registered in the system.
        /// </summary>
        /// <param name="windowTitle">The title of the toolbox window to look for.</param>
        /// <returns></returns>
        public bool IsRegisteredWindow(string windowTitle)
        {
            foreach (KeyValuePair<string, VSWindowHandle> pair in this._vsHandleCollection)
            {
                if (pair.Value.VSWindowTitle.ToLower().Trim().Equals(windowTitle.ToLower().Trim()))
                    return true;
            }
            return false;
        }

        public void RegisterSysWindow(string commandName, string vsWindowTitle, string fullControlClass)
        {
            // Go ahead and add our newly created COM Window to our collection
            // so we can manage its lifespan and prevent COM issues when the window
            // is closed pre-maturely by the user.
            VSWindowHandle wndHandle = new VSWindowHandle(
                fullControlClass,
                vsWindowTitle,
                IntPtr.Zero,
                null
            );

            // Fortunately for us we won't need to remove this until the IDE closes.
            this._vsHandleCollection.Add(commandName, wndHandle);
        }

        private IntPtr CreateVSWindowInternal2(string commandName, ref VSWindowHandle handle, DTE2 applicationObject, AddIn addIn)
        {
            return IntPtr.Zero;

            string hostAssembly = Assembly.GetCallingAssembly().Location;
            
            // +Alphonso (8/14/2014)
            //
            // Note: 
            // 'DocumentSite' can only be set once so if we utilize the same 
            // guid for each window creation it in essence is referring to 
            // the same object that has already been created and registered 
            // as a COM Instance.
            //
            // The myth is that the COM Guid needs to be the same as the assembly
            // guid but I believe this is a Microsoft bug. For that reason I'm
            // commenting out this code and replacing it with a new Guid each time which
            // mimics having a separate assembly where the COM visible Guid's are different.
            //
            // Bug Fix:
            // 1 Assembly + 1 Control = Single COM+ Guid();
            // 1 Assembly + N number of Controls = New COM+ Guids();
            //const string hostAssemblyComGuid = "{F494D23E-B31E-45AB-BCA4-28865DFC2E60}";
            string hostAssemblyComGuid = Guid.NewGuid().ToString();

            object m_managedControl = null;
            IntPtr m_controlHandle = IntPtr.Zero;

            // Create the window and then store it in our collection.
            Windows2 hwndWindows = (Windows2)applicationObject.Windows;
            Window2 hwndWindowControl = (Window2)hwndWindows.CreateToolWindow2(
                addIn,
                hostAssembly,
                handle.ClassName,
                handle.VSWindowTitle,
                hostAssemblyComGuid,
                ref m_managedControl
            );
            hwndWindowControl.Visible = true;
            hwndWindowControl.Width = m_stack.Pop();  // width
            hwndWindowControl.Height = m_stack.Pop(); // height

            // Grab the managed handle of the control so we can register it
            // to receive our unmanaged window messages whenever events are raised.
            // Without this we won't be able to capture specific window events :(.
            m_controlHandle = ((System.Windows.Forms.Control)m_managedControl).Handle;
            ((IThemeControl)m_managedControl).SetTheme();

            handle.ControlHandle = m_controlHandle;
            handle.Window = hwndWindowControl;
            handle.Control = m_managedControl as System.Windows.Forms.Control;

            return handle.ControlHandle;
        }

        private Stack<int> m_stack = new Stack<int>(2);

        public IntPtr ShowWindow(string commandName, int width = 400, int height = 250)
        {
            VSWindowHandle handle = null;

            foreach (KeyValuePair<string, VSWindowHandle> pair in this._vsHandleCollection)
            {
                if (pair.Key.ToLower().Trim().Equals(commandName.ToLower().Trim()))
                {
                    handle = pair.Value;
                    break;
                }
            }

            if (handle != null && handle.Window != null)
            {
                try
                {
                    handle.Window.Visible = true;
                    handle.Window.Width = width;
                    handle.Window.Height = height;
                }
                catch { }

                return handle.ControlHandle;
            }

            // Push the dimensions on the stack.
            m_stack.Clear();
            m_stack.Push(height);
            m_stack.Push(width);

            return CreateVSWindowInternal2(commandName, ref handle, this._applicationObject, this._addIn);
        }

        public Window2 GetWindow(string commandName)
        {
            Window2 window = null;
            IntPtr hwndHandle = ShowWindow(commandName);

            if (hwndHandle == IntPtr.Zero)
                return null;

            foreach (KeyValuePair<string, VSWindowHandle> pair in this._vsHandleCollection)
            {
                if (pair.Key.ToLower().Trim().Equals(commandName.ToLower().Trim()))
                {
                    window = pair.Value.Window;
                    break;
                }
            }

            return window;
        }

        public void SendMessageToAllWindows(uint msg, IntPtr wParam, IntPtr lParam)
        {
            foreach (KeyValuePair<string, VSWindowHandle> pair in this._vsHandleCollection)
            {
                VSWindowHandle handle = pair.Value;
                if (handle.ControlHandle != IntPtr.Zero)
                    Win32ProcessMessageInvoker.SendSimpleMessage(handle.ControlHandle, msg, wParam, lParam);
            }
        }

        public void SendMessageToAllWindows(VSEvents e, object state)
        {
            bool shouldCallNextProcedure = true;
            foreach (KeyValuePair<string, VSWindowHandle> pair in this._vsHandleCollection)
            {
                VSWindowHandle handle = pair.Value;

                if (handle.ControlHandle != IntPtr.Zero && handle.Control is IEventModel)
                {
                    ((IEventModel)handle.Control).EventMessageProcedure(e, state, out shouldCallNextProcedure);
                    if (!shouldCallNextProcedure)
                        break;
                }
            }
        }

        public void Init()
        {
            /* Do nothing */
        }

        public void Destroy()
        {
            if (this._vsHandleCollection != null)
                this._vsHandleCollection.Clear();
        }
    }
}
