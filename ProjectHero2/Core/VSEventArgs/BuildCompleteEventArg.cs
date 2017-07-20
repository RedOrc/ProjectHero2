using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.VSEventArgs
{
    internal class BuildCompleteEventArg
    {
        public vsBuildScope Scope { get; private set; }
        public vsBuildAction Action { get; private set; }

        public BuildCompleteEventArg(vsBuildScope scope, vsBuildAction action)
        {
            this.Scope = scope;
            this.Action = action;
        }
    }
}
