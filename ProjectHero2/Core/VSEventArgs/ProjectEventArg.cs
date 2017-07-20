using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.VSEventArgs
{
    internal class ProjectEventArg
    {
        public Project Project { get; private set; }
        public string OldName { get; private set; }

        public ProjectEventArg(Project project, string oldName = "")
        {
            this.Project = project;
            this.OldName = oldName;
        }
    }
}
