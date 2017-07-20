using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core
{
    public class VSTheme : IDisposable
    {
        private readonly float _fontSize;

        public string ThemeName
        {
            get;
            private set;
        }

        public VSThemes VSThemeType
        {
            get;
            private set;
        }

        private readonly Color _backgroundBrushColor = Color.Empty;
        private SolidBrush _backgroundBrush = null;
        public SolidBrush BackgroundBrush
        {
            get
            {
                return this._backgroundBrush;
            }
        }

        private readonly Color _foregroundBrushColor = Color.Empty;
        private SolidBrush _foregroundBrush = null;
        public SolidBrush ForegroundBrush
        {
            get
            {
                return this._foregroundBrush;
            }
        }

        public string FontFamilyName
        {
            get;
            private set;
        }

        public Font OptimalFont
        {
            get;
            private set;
        }

        public string ThemeGuid
        {
            get;
            private set;
        }

        public VSTheme(string themeName, VSThemes vsThemeType, Color backgroundColor, Color foregroundColor,
                       string fontFamilyName, string themeGuid = "", float fontSize = 12.0f)
        {
            this.ThemeName = themeName;
            this.VSThemeType = vsThemeType;
            this._backgroundBrushColor = backgroundColor;
            this._foregroundBrushColor = foregroundColor;
            this.FontFamilyName = fontFamilyName;
            this._fontSize = fontSize;
            this.ThemeGuid = themeGuid;

            // Initialize all resources now.
            this.OptimalFont = new Font(fontFamilyName, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
            this._backgroundBrush = new SolidBrush(this._backgroundBrushColor);
            this._foregroundBrush = new SolidBrush(this._foregroundBrushColor);
        }

        public void Dispose()
        {
            if (this.OptimalFont != null)
            {
                this.OptimalFont.Dispose();
                this.OptimalFont = null;
            }

            if (this._backgroundBrush != null)
            {
                this._backgroundBrush.Dispose();
                this._backgroundBrush = null;
            }

            if (this._foregroundBrush != null)
            {
                this._foregroundBrush.Dispose();
                this._foregroundBrush = null;
            }
        }
    }
}
