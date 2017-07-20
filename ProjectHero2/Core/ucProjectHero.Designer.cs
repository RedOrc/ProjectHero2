namespace ProjectHero2.Core
{
    partial class ucProjectHero
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucProjectHero));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colConfiguration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblProjectCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.progBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelBuild = new System.Windows.Forms.ToolStripButton();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPending = new System.Windows.Forms.ToolStripButton();
            this.btnSkipped = new System.Windows.Forms.ToolStripButton();
            this.btnFailed = new System.Windows.Forms.ToolStripButton();
            this.btnDone = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lvView = new ProjectHero2.Core.OptimizedListView();
            this.colCompleted = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1417086230_clock.png");
            this.imageList1.Images.SetKeyName(1, "1417086282_arrow-skip.png");
            this.imageList1.Images.SetKeyName(2, "1417087947_tick_circle.png");
            this.imageList1.Images.SetKeyName(3, "CPPProject_SolutionExplorerNode_.png");
            this.imageList1.Images.SetKeyName(4, "CSharpProject_SolutionExplorerNode.png");
            this.imageList1.Images.SetKeyName(5, "exclamation-red-frame.png");
            this.imageList1.Images.SetKeyName(6, "FSharpProject_SolutionExplorerNode.png");
            this.imageList1.Images.SetKeyName(7, "hourglass.png");
            this.imageList1.Images.SetKeyName(8, "PYProject_SolutionExplorerNode.png");
            this.imageList1.Images.SetKeyName(9, "RBProject_SolutionExplorerNode.png");
            this.imageList1.Images.SetKeyName(10, "Solution_8308.png");
            this.imageList1.Images.SetKeyName(11, "VBProject_SolutionExplorerNode.png");
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            // 
            // colConfiguration
            // 
            this.colConfiguration.Text = "Configuration";
            this.colConfiguration.Width = 100;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 200;
            // 
            // lblProjectCount
            // 
            this.lblProjectCount.Name = "lblProjectCount";
            this.lblProjectCount.Size = new System.Drawing.Size(58, 22);
            this.lblProjectCount.Text = "0 Projects";
            this.lblProjectCount.ToolTipText = "The number of projects in your solution.";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // progBar
            // 
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(100, 22);
            this.progBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progBar.Visible = false;
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6, 25);
            this.tsSep2.Visible = false;
            // 
            // btnCancelBuild
            // 
            this.btnCancelBuild.Image = global::ProjectHero2.resHero.hand;
            this.btnCancelBuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelBuild.Name = "btnCancelBuild";
            this.btnCancelBuild.Size = new System.Drawing.Size(63, 22);
            this.btnCancelBuild.Text = "Cancel";
            this.btnCancelBuild.ToolTipText = "Cancel the current build.";
            this.btnCancelBuild.Visible = false;
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(6, 25);
            this.tsSep1.Visible = false;
            // 
            // btnPending
            // 
            this.btnPending.Checked = true;
            this.btnPending.CheckOnClick = true;
            this.btnPending.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnPending.Image = global::ProjectHero2.resHero.pending;
            this.btnPending.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPending.Name = "btnPending";
            this.btnPending.Size = new System.Drawing.Size(80, 22);
            this.btnPending.Text = "0 Pending";
            this.btnPending.ToolTipText = "How projects are currently pending to be built.";
            // 
            // btnSkipped
            // 
            this.btnSkipped.Checked = true;
            this.btnSkipped.CheckOnClick = true;
            this.btnSkipped.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnSkipped.Image = global::ProjectHero2.resHero.skip;
            this.btnSkipped.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSkipped.Name = "btnSkipped";
            this.btnSkipped.Size = new System.Drawing.Size(78, 22);
            this.btnSkipped.Text = "0 Skipped";
            this.btnSkipped.ToolTipText = "How many projects have been skipped.";
            // 
            // btnFailed
            // 
            this.btnFailed.Checked = true;
            this.btnFailed.CheckOnClick = true;
            this.btnFailed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnFailed.Image = global::ProjectHero2.resHero.exclamation_red;
            this.btnFailed.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFailed.Name = "btnFailed";
            this.btnFailed.Size = new System.Drawing.Size(67, 22);
            this.btnFailed.Text = "0 Failed";
            this.btnFailed.ToolTipText = "How many projects failed to build.";
            // 
            // btnDone
            // 
            this.btnDone.Checked = true;
            this.btnDone.CheckOnClick = true;
            this.btnDone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnDone.Image = global::ProjectHero2.resHero.Check;
            this.btnDone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(64, 22);
            this.btnDone.Text = "0 Done";
            this.btnDone.ToolTipText = "How many projects were built successfully.";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDone,
            this.btnFailed,
            this.btnSkipped,
            this.btnPending,
            this.tsSep1,
            this.btnCancelBuild,
            this.tsSep2,
            this.progBar,
            this.toolStripSeparator1,
            this.lblProjectCount});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(695, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lvView
            // 
            this.lvView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colType,
            this.colConfiguration,
            this.colStatus,
            this.colCompleted});
            this.lvView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvView.FullRowSelect = true;
            this.lvView.Location = new System.Drawing.Point(0, 25);
            this.lvView.MultiSelect = false;
            this.lvView.Name = "lvView";
            this.lvView.OwnerDraw = true;
            this.lvView.ShowItemToolTips = true;
            this.lvView.Size = new System.Drawing.Size(695, 169);
            this.lvView.SmallImageList = this.imageList1;
            this.lvView.TabIndex = 3;
            this.lvView.UseCompatibleStateImageBehavior = false;
            this.lvView.View = System.Windows.Forms.View.Details;
            this.lvView.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lvView_ColumnWidthChanged);
            this.lvView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvView_DrawColumnHeader);
            this.lvView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvView_DrawItem);
            this.lvView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvView_DrawSubItem);
            // 
            // colCompleted
            // 
            this.colCompleted.Text = "Completed";
            this.colCompleted.Width = 100;
            // 
            // ucProjectHero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvView);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ucProjectHero";
            this.Size = new System.Drawing.Size(695, 194);
            this.Load += new System.EventHandler(this.ucProjectHero_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colConfiguration;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ToolStripLabel lblProjectCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripProgressBar progBar;
        private System.Windows.Forms.ToolStripSeparator tsSep2;
        private System.Windows.Forms.ToolStripButton btnCancelBuild;
        private System.Windows.Forms.ToolStripSeparator tsSep1;
        private System.Windows.Forms.ToolStripButton btnPending;
        private System.Windows.Forms.ToolStripButton btnSkipped;
        private System.Windows.Forms.ToolStripButton btnFailed;
        private System.Windows.Forms.ToolStripButton btnDone;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ColumnHeader colCompleted;
        private OptimizedListView lvView;
    }
}
