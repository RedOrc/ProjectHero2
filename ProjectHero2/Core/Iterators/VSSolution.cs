using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.Iterators
{
    internal class VSSolution : ISolution
    {
        public string Name
        {
            get;
            private set;
        }

        public bool HasProjects
        {
            get
            {
                return (SolutionFolderCollection != null && SolutionFolderCollection.Count() > 0) ||
                       (SolutionProjectCollection != null && SolutionProjectCollection.Count() > 0);
            }
        }

        public IList<ISolutionFolder> SolutionFolderCollection
        {
            get;
            private set;
        }

        public IList<ISolutionProject> SolutionProjectCollection
        {
            get;
            private set;
        }

        public VSSolution(string name)
        {
            this.SolutionFolderCollection = new List<ISolutionFolder>();
            this.SolutionProjectCollection = new List<ISolutionProject>();
            this.Name = name;
        }

        public void AddProject(ISolutionProject solutionProject)
        {
            this.SolutionProjectCollection.Add(solutionProject);
        }

        public void AddSolutionFolder(ISolutionFolder solutionFolder)
        {
            this.SolutionFolderCollection.Add(solutionFolder);
        }

        public void Dispose()
        {
            if (this.SolutionFolderCollection != null)
            {
                foreach (ISolutionFolder folder in this.SolutionFolderCollection)
                    ((IDisposable)folder).Dispose();

                this.SolutionFolderCollection.Clear();
                this.SolutionFolderCollection = null;
            }

            if (this.SolutionProjectCollection != null)
            {
                foreach (ISolutionProject project in this.SolutionProjectCollection)
                    ((IDisposable)project).Dispose();

                this.SolutionProjectCollection.Clear();
                this.SolutionProjectCollection = null;
            }
        }
    }
}
