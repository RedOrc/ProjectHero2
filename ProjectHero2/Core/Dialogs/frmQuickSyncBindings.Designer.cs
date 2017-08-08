namespace ProjectHero2.Core.Dialogs
{
    partial class frmQuickSyncBindings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuickSyncBindings));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeProjects = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lbFolders = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.propGrid = new System.Windows.Forms.PropertyGrid();
            this.btnRemoveAllProjects = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnAddProject = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 56);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeProjects);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbFolders);
            this.splitContainer1.Size = new System.Drawing.Size(696, 167);
            this.splitContainer1.SplitterDistance = 335;
            this.splitContainer1.TabIndex = 9;
            // 
            // treeProjects
            // 
            this.treeProjects.CheckBoxes = true;
            this.treeProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeProjects.HideSelection = false;
            this.treeProjects.ImageKey = "csharp";
            this.treeProjects.ImageList = this.imageList1;
            this.treeProjects.Location = new System.Drawing.Point(0, 0);
            this.treeProjects.Name = "treeProjects";
            this.treeProjects.SelectedImageIndex = 0;
            this.treeProjects.Size = new System.Drawing.Size(335, 167);
            this.treeProjects.TabIndex = 0;
            this.treeProjects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeProjects_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "cpp");
            this.imageList1.Images.SetKeyName(1, "csharp");
            this.imageList1.Images.SetKeyName(2, "fsharp");
            this.imageList1.Images.SetKeyName(3, "python");
            this.imageList1.Images.SetKeyName(4, "vb");
            this.imageList1.Images.SetKeyName(5, "randomproj");
            this.imageList1.Images.SetKeyName(6, "file");
            this.imageList1.Images.SetKeyName(7, "folder");
            // 
            // lbFolders
            // 
            this.lbFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFolders.FormattingEnabled = true;
            this.lbFolders.Location = new System.Drawing.Point(0, 0);
            this.lbFolders.Name = "lbFolders";
            this.lbFolders.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbFolders.Size = new System.Drawing.Size(357, 167);
            this.lbFolders.Sorted = true;
            this.lbFolders.TabIndex = 0;
            this.lbFolders.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbFolders_KeyUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 477);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(720, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 13;
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(38, 17);
            this.lblStatus.Text = "Ready";
            // 
            // propGrid
            // 
            this.propGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propGrid.Location = new System.Drawing.Point(12, 229);
            this.propGrid.Name = "propGrid";
            this.propGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propGrid.Size = new System.Drawing.Size(696, 205);
            this.propGrid.TabIndex = 11;
            this.propGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propGrid_PropertyValueChanged);
            // 
            // btnRemoveAllProjects
            // 
            this.btnRemoveAllProjects.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveAllProjects.Location = new System.Drawing.Point(133, 6);
            this.btnRemoveAllProjects.Name = "btnRemoveAllProjects";
            this.btnRemoveAllProjects.Size = new System.Drawing.Size(147, 26);
            this.btnRemoveAllProjects.TabIndex = 16;
            this.btnRemoveAllProjects.Text = "&Remove All Projects";
            this.btnRemoveAllProjects.UseVisualStyleBackColor = true;
            this.btnRemoveAllProjects.Click += new System.EventHandler(this.btnRemoveAllProjects_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(348, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Destination Sync Folders";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Configured Projects";
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveChanges.Location = new System.Drawing.Point(590, 440);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(118, 28);
            this.btnSaveChanges.TabIndex = 12;
            this.btnSaveChanges.Text = "&Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnAddProject
            // 
            this.btnAddProject.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddProject.Location = new System.Drawing.Point(12, 6);
            this.btnAddProject.Name = "btnAddProject";
            this.btnAddProject.Size = new System.Drawing.Size(115, 26);
            this.btnAddProject.TabIndex = 10;
            this.btnAddProject.Text = "&Add Project(s)";
            this.btnAddProject.UseVisualStyleBackColor = true;
            this.btnAddProject.Click += new System.EventHandler(this.btnAddProject_Click);
            // 
            // frmQuickSyncBindings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 499);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.propGrid);
            this.Controls.Add(this.btnRemoveAllProjects);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.btnAddProject);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmQuickSyncBindings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quick Sync Binding Configuration";
            this.Load += new System.EventHandler(this.frmQuickSyncBindings_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeProjects;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListBox lbFolders;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.PropertyGrid propGrid;
        private System.Windows.Forms.Button btnRemoveAllProjects;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnAddProject;
    }
}