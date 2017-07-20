using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.Win32
{
    /// <summary>
    /// This class is responsible for sending custom Win32 messages to user controls. 
    /// This was utilized for a set of private bundled tools and was extracted since 
    /// it's needed in this plugin.
    /// </summary>
    public static class Win32ProcessMessageInvoker
    {
        public static void SendSimpleMessage(UInt32 msg, IntPtr wParam, IntPtr lParam)
        {
            // Get the handle of the current main window handle to Visual Studio.
            Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            IntPtr mainWindowHandle = currentProcess.MainWindowHandle;
            Win32Api.SendMessage(mainWindowHandle, msg, wParam, lParam);
        }

        public static void SendSimpleMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam)
        {
            // Can't send this message without a proper window handle.
            if (hWnd == IntPtr.Zero)
                return;

            Win32Api.SendMessage(hWnd, msg, wParam, lParam);
        }

        public static void SendComplexMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam, object structure)
        {
            if (hWnd == IntPtr.Zero)
                return;

            // Note: Receiving control must manually cleanup unmanaged memory!
            IntPtr m_unmanagedBlock = Marshal.AllocHGlobal(Marshal.SizeOf(structure));
            Marshal.StructureToPtr(structure, m_unmanagedBlock, false);
            Win32Api.SendMessage(hWnd, msg, m_unmanagedBlock, lParam);
        }
    }
}
