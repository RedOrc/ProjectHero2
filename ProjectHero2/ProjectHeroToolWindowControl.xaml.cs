
namespace ProjectHero2
{
    using ProjectHero2.Core;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;

    public partial class ProjectHeroToolWindowControl : UserControl
    {
        private ucProjectHero control;
        
        public ProjectHeroToolWindowControl() : base()
        {
            this.InitializeComponent();
            
            if (control == null)
            {
                control = new ucProjectHero();
            }
            this.Content = control;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }
    }
}