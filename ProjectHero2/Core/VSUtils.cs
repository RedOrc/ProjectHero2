using EnvDTE;
using EnvDTE80;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core
{
    internal static class VSUtils
    {
        private const string prjKindSDECSharpProject = "{20D4826A-C6FA-45db-90F4-C717570B9F32}";
        private const string prjKindSDEVBProject = "{CB4CE8C6-1BDB-4dc7-A4D3-65A1999772F8}";
        private const string prjKindVJSharpProject = "{E6FDF86B-F3D1-11D4-8576-0002A516ECE8}";
        private const string prjKindEnterpriseProject = "{7D353B21-6E36-11D2-B35A-0000F81F0C06}";
        private const string prjKindCplusPlusProject = "{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}";
        private const string prjKindVSNetSetupProject = "{54435603-DBB4-11D2-8724-00A0C9A8B90C}";

        private const string prjKindSolutionItems2 = "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}";
        public const string vsProjectItemKindSolutionItems = "{66A26722-8FB5-11D2-AA7E-00C04F688DDE}";
        public const string vsProjectKindSolutionFolder = "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}";


        public static VSProjectType DetermineProjectType(string kind)
        {
            VSProjectType projType = VSProjectType.Unknown;
            string K = kind.ToUpper();

            if (K.Equals(VSLangProj.PrjKind.prjKindVBProject.ToUpper()))
                projType = VSProjectType.VBProject;
            else if (K.Equals(VSLangProj.PrjKind.prjKindCSharpProject.ToUpper()))
                projType = VSProjectType.CSharpProject;
            else if (K.Equals(prjKindVJSharpProject))
                projType = VSProjectType.VJSharpProject;
            else if (K.Equals(prjKindSDEVBProject))
                projType = VSProjectType.SDEVBProject;
            else if (K.Equals(prjKindSDECSharpProject))
                projType = VSProjectType.SDECSharpProject;
            else if (K.Equals(EnvDTE.Constants.vsProjectItemKindMisc.ToUpper()))
                projType = VSProjectType.Misc;
            else if (K.Equals(EnvDTE.Constants.vsProjectItemKindSolutionItems.ToUpper()) || K.Equals(prjKindSolutionItems2) ||
                     K.Equals(vsProjectKindSolutionFolder.ToUpper()))
                projType = VSProjectType.SolutionItems;
            else if (K.Equals(EnvDTE.Constants.vsProjectKindUnmodeled.ToUpper()))
                projType = VSProjectType.Unmodeled;
            else if (K.Equals(VSLangProj.PrjKind.prjKindVSAProject.ToUpper()))
                projType = VSProjectType.VSAProject;
            else if (K.Equals(prjKindEnterpriseProject))
                projType = VSProjectType.EnterpriseProject;
            else if (K.Equals(prjKindCplusPlusProject))
                projType = VSProjectType.CPlusPlusProject;
            else if (K.Equals(prjKindVSNetSetupProject))
                projType = VSProjectType.SetupProject;

            return projType;
        }

        public static VSProjectType DetermineProjectType(Project project)
        {
            return project == null ? VSProjectType.Unknown : DetermineProjectType(project.Kind);
        }

        public static bool IsValidSolutionProject(string kind)
        {
            if (string.IsNullOrEmpty(kind))
                return false;

            VSProjectType projType = DetermineProjectType(kind);
            switch (projType)
            {
                case VSProjectType.CPlusPlusProject:
                case VSProjectType.CSharpProject:
                case VSProjectType.SDECSharpProject:
                case VSProjectType.FSharpProject:
                case VSProjectType.VBProject:
                case VSProjectType.SDEVBProject:
                case VSProjectType.VJSharpProject:
                    return true;
            }

            return false;
        }

        public static Command GetCommandByGuidAndID(DTE2 applicationObject, string guid, int id)
        {
            return applicationObject.Commands.Item(guid, id);
        }
    }
}
