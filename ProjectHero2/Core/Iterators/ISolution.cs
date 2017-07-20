using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.Iterators
{
    internal interface ISolution : IDisposable
    {
        string Name { get; }
        bool HasProjects { get; }
        IList<ISolutionFolder> SolutionFolderCollection { get; }
        IList<ISolutionProject> SolutionProjectCollection { get; }
        void AddProject(ISolutionProject solutionProject);
        void AddSolutionFolder(ISolutionFolder solutionFolder);
    }
}
