using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using ProjectHero2.Core;

namespace ProjectHero2
{
    public sealed class ProjectHeroFactory
    {
        private static readonly ProjectHeroFactory factoryInstance = new ProjectHeroFactory();
        public static ProjectHeroFactory SharedInstance
        {
            get
            {
                return factoryInstance;
            }
        }

        private Package pluginPackage;
        private DTE2 applicationObject;
        private ucProjectHero heroControl;

        public Package PluginPackage
        {
            get
            {
                return this.pluginPackage;
            }
        }

        public DTE2 GetApplicationObject
        {
            get
            {
                return this.applicationObject;
            }
        }

        public ucProjectHero HeroControl
        {
            get
            {
                if (heroControl == null)
                {
                    heroControl = new ucProjectHero();
                    if (applicationObject != null)
                    {
                        heroControl.Init(this.applicationObject, true);
                        VSEventManager.SharedManager.AddSubscriber(heroControl);
                    }
                }
                return this.heroControl;
            }
        }

        public void InitApplicationObject(DTE2 appObject)
        {
            this.applicationObject = appObject;
        }

        public void InitPluginPackage(Package package)
        {
            this.pluginPackage = package;
        }
    }
}