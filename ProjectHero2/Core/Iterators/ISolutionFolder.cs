using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.Iterators
{
    internal interface ISolutionFolder : IDisposable
    {
        void AddProject(ISolutionProject solutionProject);
        ISolution ParentSolution { get; }
        IList<ISolutionProject> SolutionProjectCollection { get; }
        string Name { get; }
    }
}
