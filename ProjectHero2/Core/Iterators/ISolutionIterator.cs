using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.Iterators
{
    internal interface ISolutionIterator
    {
        ISolution ScanSolution(Solution solution);
    }
}
