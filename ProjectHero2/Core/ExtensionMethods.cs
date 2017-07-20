using EnvDTE;
using EnvDTE80;
using ProjectHero2.Core.Iterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core
{
    internal static class ExtensionMethods
    {
        #region Project Extensions

        public static string GetProjectName(this Project project)
        {
            if (project == null || string.IsNullOrEmpty(project.Name))
                return string.Empty;

            string name = string.Empty;

            if (!project.Name.Contains("\\"))
                name = project.Name;
            else
                name = project.Name.Split('\\').Reverse().First();

            return name
                // C# project file
                .Replace(".csproj", string.Empty)
                // Visual Basic project file
                .Replace(".vbp", string.Empty)
                .Replace(".vip", string.Empty)
                .Replace(".vbproj", string.Empty)
                // Visual Studio deployment project file
                .Replace(".vdproj", string.Empty)
                // Visual Studio macro project file
                .Replace(".vmx", string.Empty)
                // The utility project file.
                .Replace(".vup", string.Empty)
                // Visual C++ project file.
                .Replace(".vcxproj", string.Empty);
        }

        public static bool IsValidProject(this Project solutionItem)
        {
            if (solutionItem == null)
                return false;

            if (VSUtils.IsValidSolutionProject(solutionItem.Kind))
                return true;

            return (solutionItem.Object != null && solutionItem.Object is Project &&
                VSUtils.IsValidSolutionProject((solutionItem.Object as Project).Kind)) ? true : false;
        }

        public static bool HasProjects(this Project solutionItem)
        {
            if (solutionItem == null || solutionItem.ProjectItems == null || solutionItem.ProjectItems.Count == 0)
                return false;

            foreach (ProjectItem item in solutionItem.ProjectItems)
            {
                if (item.IsValidProject())
                    return true;
            }

            return false;
        }

        public static bool IsSolutionFolder(this Project solutionItem)
        {
            if (solutionItem == null)
                return false;

            return ((solutionItem.Object == null &&
                !VSUtils.IsValidSolutionProject(solutionItem.Kind)) ||
                !(solutionItem.Object is Project));
        }

        public static VSProjectType GetProjectType(this Project project)
        {
            return VSUtils.DetermineProjectType(project.Kind);
        }

        public static bool DoesSolutionItemHaveProjects(this Project solutionItem)
        {
            if (solutionItem == null || solutionItem.ProjectItems.Count == 0)
                return false;

            bool hasValidProjects = false;
            foreach (ProjectItem projectItem in solutionItem.ProjectItems)
            {
                if (projectItem.Object is Project)
                {
                    Project thisProject = projectItem.Object as Project;
                    VSProjectType vsProjType = VSUtils.DetermineProjectType(thisProject.Kind);

                    switch (vsProjType)
                    {
                        case VSProjectType.VBProject:
                        case VSProjectType.VJSharpProject:
                        case VSProjectType.CPlusPlusProject:
                        case VSProjectType.CSharpProject:
                        case VSProjectType.FSharpProject:
                        case VSProjectType.SDECSharpProject:
                        case VSProjectType.SDEVBProject:
                            return true;
                    }
                }
            }

            return hasValidProjects;
        }

        #endregion Project Extensions

        #region ProjectItem Extensions

        public static string GetFullyQualifiedName(this ProjectItem item)
        {
            StringBuilder builder = new StringBuilder();
            Stack<string> m_stack = new Stack<string>();

            if (item.IsSolutionFolder())
            {
                m_stack.Push(item.Name);
                SolutionFolder folder = item.Object as SolutionFolder;
                if (!folder.Hidden && folder.Parent != null)
                {
                    m_stack.Push(folder.Parent.GetProjectName());
                }

                string n = string.Empty;
                while ((n = m_stack.Pop()) != null)
                    builder.Append(n + ".");

                builder = builder.Remove(builder.Length - 1, 1);
                return builder.ToString();
            }
            else if (item.IsValidProject())
                return ((Project)item.Object).GetProjectName();

            return string.Empty;
        }

        public static bool IsValidProject(this ProjectItem solutionItem)
        {
            if (solutionItem == null)
                return false;

            if (VSUtils.IsValidSolutionProject(solutionItem.Kind))
                return true;

            return (solutionItem.Object != null && solutionItem.Object is Project &&
                VSUtils.IsValidSolutionProject((solutionItem.Object as Project).Kind)) ? true : false;
        }

        public static bool HasProjects(this ProjectItem solutionItem)
        {
            if (solutionItem == null || solutionItem.ProjectItems == null || solutionItem.ProjectItems.Count == 0)
                return false;

            foreach (ProjectItem item in solutionItem.ProjectItems)
            {
                if (item.IsValidProject())
                    return true;
            }

            return false;
        }

        public static bool IsSolutionFolder(this ProjectItem solutionItem)
        {
            if (solutionItem == null)
                return false;

            return (solutionItem.Object is Project) ? false :
                VSUtils.DetermineProjectType(solutionItem.Kind) == VSProjectType.SolutionItems;
        }

        #endregion ProjectItem Extensions

        #region Solution Extensions

        public static string GetSolutionName(this Solution solution)
        {
            if (solution == null || string.IsNullOrEmpty(solution.FullName))
                return string.Empty;

            string name = solution.FullName.Split('\\').Reverse().First();
            return name
                .Replace(".sln", string.Empty)
                .Replace(".suo", string.Empty);
        }

        public static ISolution ScanSolution(this Solution solution)
        {
            ISolutionIterator iterator = new VSSolutionIterator();
            return iterator.ScanSolution(solution);
        }

        #endregion Solution Extensions
    }
}
