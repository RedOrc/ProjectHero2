using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.Iterators
{
    internal interface ISolutionProject : IDisposable
    {
        ISolutionFolder ParentSolutionFolder { get; }
        ISolution ParentSolution { get; }
        Project Project { get; }
        VSProjectType ProjectType { get; }
        string Name { get; }
        string UniqueName { get; }
        string FilePath { get; }
        string MD5HashCode { get; }
    }
}
