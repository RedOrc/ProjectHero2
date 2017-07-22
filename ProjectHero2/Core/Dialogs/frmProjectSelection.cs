using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectHero2.Core.Dialogs
{
    public partial class frmProjectSelection : Form
    {
        private IEnumerable<ProjectHero2.Core.ucProjectHero.AvailableProjectNode> _projectCollection = null;

        public frmProjectSelection()
        {
            InitializeComponent();
        }

        public frmProjectSelection(IEnumerable<ProjectHero2.Core.ucProjectHero.AvailableProjectNode> projectCollection)
           : this()
        {
            this._projectCollection = projectCollection;
        }


        private void frmProjectSelection_Load(object sender, EventArgs e)
        {
            listBox1.BeginUpdate();

            foreach (ProjectHero2.Core.ucProjectHero.AvailableProjectNode project in this._projectCollection.OrderBy(i => i.Name))
            {
                listBox1.Items.Add(project);
            }

            listBox1.EndUpdate();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems == null || (listBox1.SelectedItems != null && listBox1.SelectedItems.Count == 0))
            {
                MessageBox.Show("Please choose atleast one project.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<ProjectHero2.Core.ucProjectHero.AvailableProjectNode> selectedProjects = new List<ucProjectHero.AvailableProjectNode>();
            foreach (ProjectHero2.Core.ucProjectHero.AvailableProjectNode project in listBox1.SelectedItems)
            {
                selectedProjects.Add(project);
            }

            this.Tag = selectedProjects;
            this.Close();
        }

        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {

        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {

        }
    }
}
