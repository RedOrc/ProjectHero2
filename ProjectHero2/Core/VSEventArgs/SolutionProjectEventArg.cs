using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.VSEventArgs
{
    internal class SolutionProjectEventArg
    {
        public Project Project { get; set; }
        public string OldName { get; set; }

        public SolutionProjectEventArg(Project project, string oldName = "")
        {
            this.Project = project;
            this.OldName = oldName;
        }
    }
}