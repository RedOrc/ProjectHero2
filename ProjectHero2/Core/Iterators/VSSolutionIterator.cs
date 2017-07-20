using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.Iterators
{
    internal class VSSolutionIterator : ISolutionIterator
    {
        private ISolution _solution;
        public ISolution ScanSolution(EnvDTE.Solution solution)
        {
            if (solution == null || solution.Projects == null || solution.Projects.Count == 0)
                return null;

            this._solution = new VSSolution(solution.GetSolutionName());
            foreach (Project project in solution.Projects)
            {
                // Check if this is actually a valid project type.
                if (project.IsValidProject())
                {
                    if (project.Object == null)
                        continue;

                    ISolutionProject solutionProject = new VSSolutionProject(this._solution, project);
                    this._solution.AddProject(solutionProject);
                }
                else if (project.ProjectItems != null && project.ProjectItems.Count > 0)
                    NavigateProjectFolder(project.ProjectItems);
            }

            return this._solution;
        }

        private void NavigateProjectFolder(ProjectItems itemCollection)
        {
            if (itemCollection == null)
                return;

            foreach (ProjectItem item in itemCollection)
            {
                if (item.SubProject != null)
                {
                    if (VSUtils.IsValidSolutionProject(item.SubProject.Kind))
                    {
                        if (item.SubProject == null)
                            continue;
                        else if (item.SubProject is Project)
                        {
                            ISolutionProject subProject = new VSSolutionProject(this._solution, item.SubProject);
                            this._solution.AddProject(subProject);
                        }
                    }
                    else
                        NavigateProjectFolder(item.SubProject.ProjectItems);
                }
                else
                {
                    if (item.Object == null)
                        continue;
                    else if (item.Object is Project)
                    {
                        ISolutionProject solutionProject = new VSSolutionProject(this._solution, item.Object as Project);
                        this._solution.AddProject(solutionProject);
                    }
                }
            }
        }
    }
}
