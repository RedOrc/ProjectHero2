using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.VSEventArgs
{
    internal class BuildProjectConfigEventArg
    {
        public string Project { get; set; }
        public string ProjectConfig { get; set; }
        public string Platform { get; set; }
        public string SolutionConfig { get; set; }
        public bool Success { get; set; }

        public BuildProjectConfigEventArg(string project, string projectConfig, string platform, string solutionConfig, bool success)
        {
            this.Project = project;
            this.ProjectConfig = projectConfig;
            this.Platform = platform;
            this.SolutionConfig = solutionConfig;
            this.Success = success;
        }
    }
}
