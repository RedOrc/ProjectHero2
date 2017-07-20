using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.Iterators
{
    internal class VSSolutionFolder : ISolutionFolder
    {
        public ISolution ParentSolution
        {
            get;
            private set;
        }

        public IList<ISolutionProject> SolutionProjectCollection
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public VSSolutionFolder(ISolution parentSolution, string name)
        {
            this.ParentSolution = parentSolution;
            this.Name = name;
            this.SolutionProjectCollection = new List<ISolutionProject>();
        }

        public void AddProject(ISolutionProject solutionProject)
        {
            this.SolutionProjectCollection.Add(solutionProject);
        }

        public void Dispose()
        {
            if (this.SolutionProjectCollection != null)
            {
                foreach (ISolutionProject project in this.SolutionProjectCollection)
                    ((IDisposable)project).Dispose();

                this.SolutionProjectCollection.Clear();
                this.SolutionProjectCollection = null;
            }

            this.ParentSolution = null;
        }
    }
}
