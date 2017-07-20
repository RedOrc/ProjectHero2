using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectHero2.Core
{
    public class OptimizedListView : ListView
    {
        public OptimizedListView()
            : base()
        {
            this.DoubleBuffered = true;
        }
    }
}