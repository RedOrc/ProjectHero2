using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core
{
    public delegate void CrossThreadInvoker();
    public delegate void CrossThreadInvoker2(object data);

    public enum VSThemes : byte
    {
        Blue = 0,
        Dark,
        DarkWithLightEditor,
        Green,
        HighContrast,
        Light,
        LightWithDarkEditor,
        Purple,
        Red,
        SolarizedDark,
        SolarizedLight,
        Tan
    }

    /// <summary>
    /// Windows Messages
    /// Defined specifically for 
    /// Visual Studio 2013 interprocess messaging.
    /// </summary>
    public enum Win32ProjectHeroMessages : uint
    {
        ///<summary>
        /// High-Order message defined as the win32 base message for our current message.
        /// Please look at the WParam and LParam variables accordingly to receive
        /// more detailed information related to a message.
        ///</summary>
        WM_ACEMSG_THEME_CHANGED = (Win32.Win32Messages.WM_USER + 0x10)
    }

    public enum VSWindowHandles : byte
    {
        VS_BASE_HANDLE = 0x50,
        HWND_SOLUTION_DESC = VS_BASE_HANDLE + 0x1
    }

    public enum VSToolWindowState : byte
    {
        VS_SHOWN = 0,
        VS_HIDDEN
    }

    public enum VSEvents
    {
        BuildComplete,
        BuildBegin,
        WindowShown,
        WindowHidden,

        SolutionOpened,
        SolutionClosed,
        SolutionRenamed,
        SolutionBeforeClosing,
        SolutionAfterClosing,

        SolutionProjectAdded,
        SolutionProjectRemoved,
        SolutionProjectRenamed,

        ProjectAdded,
        ProjectRemoved,
        ProjectRenamed,

        ProjectItemAdded,
        ProjectItemRemoved,
        ProjectItemRenamed,

        BuildProjectConfigBegin,
        BuildProjectConfigDone,

        BeforeCommandExecute,
        AfterCommandExecute
    }

    public enum VSProjectType
    {
        VBProject,
        CSharpProject,
        VJSharpProject,
        SDEVBProject,
        SDECSharpProject,
        SolutionItems,
        VSAProject,
        EnterpriseProject,
        CPlusPlusProject,
        SetupProject,
        FSharpProject,
        Unmodeled,
        Misc,
        Solution,
        Unknown
    }

    public enum FileType
    {
        Directory,
        File
    }
}