using EnvDTE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.Iterators
{
    internal class VSSolutionProject : ISolutionProject, IDisposable
    {
        public ISolutionFolder ParentSolutionFolder
        {
            get;
            private set;
        }

        public ISolution ParentSolution
        {
            get;
            private set;
        }

        public EnvDTE.Project Project
        {
            get;
            private set;
        }

        public VSProjectType ProjectType
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string UniqueName
        {
            get;
            private set;
        }

        public string FilePath
        {
            get;
            private set;
        }

        public string MD5HashCode
        {
            get;
            private set;
        }

        public VSSolutionProject(ISolutionFolder folder, Project project)
        {
            this.ParentSolutionFolder = folder;
            this.Project = project;
            Init();
        }

        public VSSolutionProject(ISolution solution, Project project)
        {
            this.ParentSolution = solution;
            this.Project = project;
            Init();
        }

        private void Init()
        {
            this.Name = this.Project.GetProjectName();
            this.UniqueName = this.Project.UniqueName;
            this.ProjectType = this.Project.GetProjectType();
                                    
            if (!string.IsNullOrEmpty(this.Project.FullName))
                this.FilePath = Path.GetDirectoryName(this.Project.FullName);

            this.MD5HashCode = Crypto.GenerateMD5Hash(this.UniqueName);
        }

        public void Dispose()
        {
            this.ParentSolution = null;
            this.ParentSolutionFolder = null;
            this.Project = null;
        }
    }
}
