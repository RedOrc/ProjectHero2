using ProjectHero2.Core.Iterators;
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
    public partial class frmFileFolderSelectionDialog : Form
    {
        public frmFileFolderSelectionDialog()
        {
            InitializeComponent();
        }

        public frmFileFolderSelectionDialog(string localPathToScan)
            : this()
        {
            FolderScanner scanner = new FolderScanner();
            SysFileItem rootItem = scanner.ScanFolder(localPathToScan);

            if (rootItem != null)
            {
                listBox1.BeginUpdate();

                foreach (SysFileItem item in rootItem.AssociatedItems)
                {
                    listBox1.Items.Add(item);
                }

                listBox1.EndUpdate();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems == null || (listBox1.SelectedItems != null && listBox1.SelectedItems.Count == 0))
            {
                MessageBox.Show("Please select atleast one item in the list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<SysFileItem> items = new List<SysFileItem>();
            foreach (SysFileItem item in listBox1.SelectedItems)
            {
                items.Add(item);
            }

            this.Tag = items;
            this.Close();
        }

        private void frmFileFolderSelectionDialog_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                SysFileItem item = listBox1.Items[e.Index] as SysFileItem;
                Graphics g = e.Graphics;

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    g.FillRectangle(Brushes.LightBlue, e.Bounds);
                }
                else
                {
                    e.DrawBackground();
                }

                Image imgToDraw = item.FileType == FileType.Directory ? resHero.folder : resHero.file;
                g.DrawImage(imgToDraw, new Rectangle(e.Bounds.X, e.Bounds.Y, 16, 16));

                SizeF szMeasure = g.MeasureString(item.Name, this.Font);

                g.DrawString(item.Name, this.Font, Brushes.Black, e.Bounds.X + imgToDraw.Width + 5, e.Bounds.Y + (e.Bounds.Height / 2 - szMeasure.Height / 2));
                e.DrawFocusRectangle();
            }
        }

        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemWidth = listBox1.Width;
            e.ItemHeight = 20;
        }
    }
}
