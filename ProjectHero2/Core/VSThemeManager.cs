using Microsoft.Win32;
using ProjectHero2.Core.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core
{
    public sealed class VSThemeManager : IObjectLifecycle
    {
        private static VSThemeManager _manager = null;
        public static VSThemeManager Manager
        {
            get
            {
                if (_manager == null)
                    _manager = new VSThemeManager();

                return _manager;
            }
        }

        private readonly List<VSTheme> _themeCollection;

        public VSTheme CurrentTheme
        {
            get;
            private set;
        }

        public int ThemeCount
        {
            get;
            private set;
        }

        private VSThemeManager()
        {
            // Let's initialize with a very low memory footprint.
            this._themeCollection = new List<VSTheme>(15);
        }

        public void ChangeTheme(VSThemes vsTheme)
        {
            // Our pointer to the list items never changes but only gets swapped.
            // So let's re-point the current theme object to the new list item
            // and invoke a win32 message to all child windows of Visual Studio.
            //
            // This is a very efficient and extremely fast way of communicating
            // without the need to manage event delegates.
            VSTheme currentTheme = CurrentTheme;
            CurrentTheme = this._themeCollection.First(theme => theme.VSThemeType == vsTheme);
            Win32ProcessMessageInvoker.SendSimpleMessage((UInt32)Win32ProjectHeroMessages.WM_ACEMSG_THEME_CHANGED, IntPtr.Zero, IntPtr.Zero);
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void Init()
        {
            const string themesRegistryPath = "Software\\Microsoft\\VisualStudio\\12.0_Config\\Themes";
            const string currentThemeRegistryPath = "Software\\Microsoft\\VisualStudio\\12.0\\General";

            RegistryKey regThemesKey = null;
            HashSet<Tuple<string, string>> regThemeKeyHashSet = new HashSet<Tuple<string, string>>();

            try
            {
                // Open up the parent key and grab the sub key names for iteration.
                regThemesKey = Registry.CurrentUser.OpenSubKey(themesRegistryPath);
                string[] childKeyNames = regThemesKey.GetSubKeyNames();

                // Store each key (COM Guid) and its theme name in a tuple
                // which we'll use later to associate to our initialized themes.
                foreach (string childKey in childKeyNames)
                {
                    RegistryKey regChildKey = regThemesKey.OpenSubKey(childKey);
                    Tuple<string, string> m_tuple =
                        new Tuple<string, string>(
                            // Replace the COM Guid braces.
                            childKey.Replace("{", "").Replace("}", ""),
                            Convert.ToString(regChildKey.GetValue("", ""))
                    );
                    regThemeKeyHashSet.Add(m_tuple);

                    regChildKey.Close();
                    regChildKey.Dispose();
                    regChildKey = null;
                }
            }
            finally
            {
                if (regThemesKey != null)
                {
                    regThemesKey.Close();
                    regThemesKey.Dispose();
                    regThemesKey = null;
                }
            }

            const string defaultFontFamilyName = "Consolas";

            this._themeCollection.Clear();
            this._themeCollection.AddRange(
                new VSTheme[] {
                    new VSTheme("Blue", VSThemes.Blue, Color.FromArgb(214, 219, 233), Color.Black, defaultFontFamilyName, regThemeKeyHashSet.Where(e=> e.Item2=="Blue").Count()>0? regThemeKeyHashSet.FirstOrDefault(i => i.Item2 == "Blue").Item1:string.Empty),
                    new VSTheme("Dark", VSThemes.Dark, Color.FromArgb(45, 45, 48), Color.White, defaultFontFamilyName, regThemeKeyHashSet.Where(e=> e.Item2=="Dark").Count()>0? regThemeKeyHashSet.FirstOrDefault(i => i.Item2 == "Dark").Item1:string.Empty),
                    new VSTheme("Dark With Light Editor", VSThemes.DarkWithLightEditor, Color.FromArgb(45, 45, 48), Color.White, defaultFontFamilyName, regThemeKeyHashSet.Where(e=> e.Item2 =="Dark With Light Editor").Count()>0?regThemeKeyHashSet.FirstOrDefault(i => i.Item2 == "Dark With Light Editor").Item1:string.Empty),
                    new VSTheme("Green", VSThemes.Green, Color.FromArgb(207, 221, 208), Color.Black, defaultFontFamilyName, regThemeKeyHashSet.Where(e=> e.Item2 == "Green").Count() >0? regThemeKeyHashSet.FirstOrDefault(i=> i.Item2 == "Green").Item1:string.Empty),
                    new VSTheme("Light", VSThemes.Light, Color.FromArgb(238, 238, 242), Color.Black, defaultFontFamilyName, regThemeKeyHashSet.Where(e=> e.Item2 =="Light").Count()>0? regThemeKeyHashSet.FirstOrDefault(i => i.Item2 == "Light").Item1:string.Empty),
                    new VSTheme("Light With Dark Editor", VSThemes.LightWithDarkEditor, Color.FromArgb(238, 238, 242), Color.Black, defaultFontFamilyName, regThemeKeyHashSet.Where(e=> e.Item2 =="Light With Dark Editor").Count()>0? regThemeKeyHashSet.FirstOrDefault(i=> i.Item2 == "Light With Dark Editor").Item1:string.Empty),
                    new VSTheme("Purple", VSThemes.Purple, Color.FromArgb(221, 202, 226), Color.Black, defaultFontFamilyName, regThemeKeyHashSet.Where(e=> e.Item2 == "Purple").Count()>0? regThemeKeyHashSet.FirstOrDefault(i=> i.Item2 == "Purple").Item1:string.Empty),
                    new VSTheme("Red", VSThemes.Red, Color.FromArgb(226, 202, 202), Color.Black, defaultFontFamilyName, regThemeKeyHashSet.Where(e=> e.Item2 == "Red").Count()>0? regThemeKeyHashSet.FirstOrDefault(i=> i.Item2 == "Red").Item1:string.Empty),
                    new VSTheme("Solarized (Dark)", VSThemes.SolarizedDark, Color.FromArgb(0, 30, 38), Color.White, defaultFontFamilyName, regThemeKeyHashSet.Where(e=> e.Item2 == "Solarized (Dark)").Count()>0? regThemeKeyHashSet.FirstOrDefault(i=> i.Item2 == "Solarized (Dark)").Item1:string.Empty),
                    new VSTheme("Solarized (Light)", VSThemes.SolarizedLight, Color.FromArgb(230, 221, 193), Color.Black, defaultFontFamilyName, regThemeKeyHashSet.Where(e=> e.Item2 == "Solarized (Light)").Count()>0 ? regThemeKeyHashSet.FirstOrDefault(i=> i.Item2 == "Solarized (Light)").Item1:string.Empty),
                    new VSTheme("Tan", VSThemes.Tan, Color.FromArgb(226, 220, 202), Color.Black, defaultFontFamilyName, regThemeKeyHashSet.Where(e=> e.Item2 =="Tan").Count()>0? regThemeKeyHashSet.FirstOrDefault(i=> i.Item2 == "Tan").Item1:string.Empty)
            });

            // Since we need to select one, the "Blue" theme will be the default.
            this.CurrentTheme = this._themeCollection.First();

            // What's the current theme?
            RegistryKey regCurrentThemeKey = null;
            try
            {
                regCurrentThemeKey = Registry.CurrentUser.OpenSubKey(currentThemeRegistryPath);
                string currentThemeValue = Convert.ToString(regCurrentThemeKey.GetValue("CurrentTheme", ""))
                                                  .Replace("{", string.Empty)
                                                  .Replace("}", string.Empty);

                if (!string.IsNullOrEmpty(currentThemeValue))
                {
                    VSTheme selectedTheme = this._themeCollection.FirstOrDefault(i => i.ThemeGuid == currentThemeValue);
                    this.CurrentTheme = selectedTheme;
                }
            }
            finally
            {
                if (regCurrentThemeKey != null)
                {
                    regCurrentThemeKey.Close();
                    regCurrentThemeKey.Dispose();
                    regCurrentThemeKey = null;
                }
            }
        }

        public void Destroy()
        {
            if (_themeCollection != null)
            {
                // Destroy each theme accordingly to prevent any memory leaks.
                for (int themeIndex = 0; themeIndex < _themeCollection.Count; themeIndex++)
                {
                    VSTheme currentTheme = this._themeCollection[themeIndex];
                    currentTheme.Dispose();
                    currentTheme = null;
                }

                this._themeCollection.Clear();
            }

            if (_manager != null)
                _manager = null;
        }
    }
}
