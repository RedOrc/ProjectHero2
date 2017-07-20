using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core
{
    /// <summary>
    /// Provides basic mechanisms by which an object lifecycle occurs.
    /// First is the Init() stage and later is the Destroy() stage.
    /// Implementers will perform necessary initialization during the Init cycle
    /// and clean up work during the destruction cycle.
    /// </summary>
    public interface IObjectLifecycle
    {
        void Init();
        void Destroy();
    }
}
