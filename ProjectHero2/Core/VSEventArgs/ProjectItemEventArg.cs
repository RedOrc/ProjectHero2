using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.VSEventArgs
{
    internal class ProjectItemEventArg
    {
        public ProjectItem ProjectItem { get; private set; }
        public string OldName { get; private set; }

        public ProjectItemEventArg(ProjectItem projectItem, string oldName = "")
        {
            this.ProjectItem = projectItem;
            this.OldName = oldName;
        }
    }
}