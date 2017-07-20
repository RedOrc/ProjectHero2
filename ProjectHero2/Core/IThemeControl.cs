using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core
{
    /// <summary>
    /// A user control inheriting from this interface is made
    /// aware of updates to the Visual Studio Skin so that adjustments
    /// can be made to the look and feel to keep all user controls
    /// looking consistently the same.
    /// </summary>
    internal interface IThemeControl
    {
        void SetTheme();
        void AdjustView(VSToolWindowState state);
    }
}
