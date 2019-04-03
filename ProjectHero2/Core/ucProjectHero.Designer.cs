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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnDone = new System.Windows.Forms.ToolStripButton();
            this.btnFailed = new System.Windows.Forms.ToolStripButton();
            this.btnSkipped = new System.Windows.Forms.ToolStripButton();
            this.btnPending = new System.Windows.Forms.ToolStripButton();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelBuild = new System.Windows.Forms.ToolStripButton();
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.progBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblProjectCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblCompletionTime = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnManageQuickSyncBindings = new System.Windows.Forms.ToolStripButton();
            this.lblQuickSync = new System.Windows.Forms.ToolStripLabel();
            this.lvView = new ProjectHero2.Core.OptimizedListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colConfiguration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCompleted = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.lblProjectCount,
            this.toolStripSeparator2,
            this.lblCompletionTime,
            this.toolStripSeparator3,
            this.btnManageQuickSyncBindings,
            this.lblQuickSync});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(832, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnDone
            // 
            this.btnDone.Image = global::ProjectHero2.resHero.tick_green;
            this.btnDone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(64, 22);
            this.btnDone.Text = "0 Done";
            this.btnDone.ToolTipText = "How many projects were built successfully.";
            // 
            // btnFailed
            // 
            this.btnFailed.Image = global::ProjectHero2.resHero.error2;
            this.btnFailed.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFailed.Name = "btnFailed";
            this.btnFailed.Size = new System.Drawing.Size(67, 22);
            this.btnFailed.Text = "0 Failed";
            this.btnFailed.ToolTipText = "How many projects failed to build.";
            // 
            // btnSkipped
            // 
            this.btnSkipped.Image = global::ProjectHero2.resHero.skip;
            this.btnSkipped.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSkipped.Name = "btnSkipped";
            this.btnSkipped.Size = new System.Drawing.Size(78, 22);
            this.btnSkipped.Text = "0 Skipped";
            this.btnSkipped.ToolTipText = "How many projects have been skipped.";
            // 
            // btnPending
            // 
            this.btnPending.Image = global::ProjectHero2.resHero.pending;
            this.btnPending.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPending.Name = "btnPending";
            this.btnPending.Size = new System.Drawing.Size(80, 22);
            this.btnPending.Text = "0 Pending";
            this.btnPending.ToolTipText = "How projects are currently pending to be built.";
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(6, 25);
            this.tsSep1.Visible = false;
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
            this.btnCancelBuild.Click += new System.EventHandler(this.btnCancelBuild_Click);
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6, 25);
            this.tsSep2.Visible = false;
            // 
            // progBar
            // 
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(100, 22);
            this.progBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progBar.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lblProjectCount
            // 
            this.lblProjectCount.Name = "lblProjectCount";
            this.lblProjectCount.Size = new System.Drawing.Size(58, 22);
            this.lblProjectCount.Text = "0 Projects";
            this.lblProjectCount.ToolTipText = "The number of projects in your solution.";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // lblCompletionTime
            // 
            this.lblCompletionTime.Name = "lblCompletionTime";
            this.lblCompletionTime.Size = new System.Drawing.Size(42, 22);
            this.lblCompletionTime.Text = "Ready.";
            this.lblCompletionTime.ToolTipText = "The total completion time of the build.";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnManageQuickSyncBindings
            // 
            this.btnManageQuickSyncBindings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnManageQuickSyncBindings.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageQuickSyncBindings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnManageQuickSyncBindings.Name = "btnManageQuickSyncBindings";
            this.btnManageQuickSyncBindings.Size = new System.Drawing.Size(117, 22);
            this.btnManageQuickSyncBindings.Text = "Manage Sync Bindings";
            this.btnManageQuickSyncBindings.ToolTipText = "Manage your quick sync bindings.";
            this.btnManageQuickSyncBindings.Click += new System.EventHandler(this.btnManageQuickSyncBindings_Click);
            // 
            // lblQuickSync
            // 
            this.lblQuickSync.Image = global::ProjectHero2.resHero.lightning;
            this.lblQuickSync.Name = "lblQuickSync";
            this.lblQuickSync.Size = new System.Drawing.Size(117, 22);
            this.lblQuickSync.Text = "Quick Sync Ready";
            this.lblQuickSync.ToolTipText = "Quick Sync will synchronize successfully built projects to designated folders.";
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
            this.lvView.Size = new System.Drawing.Size(832, 139);
            this.lvView.SmallImageList = this.imageList1;
            this.lvView.TabIndex = 3;
            this.lvView.UseCompatibleStateImageBehavior = false;
            this.lvView.View = System.Windows.Forms.View.Details;
            this.lvView.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lvView_ColumnWidthChanged);
            this.lvView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvView_DrawColumnHeader);
            this.lvView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvView_DrawItem);
            this.lvView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvView_DrawSubItem);
            this.lvView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvView_MouseMove);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 200;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            // 
            // colConfiguration
            // 
            this.colConfiguration.Text = "Configuration";
            this.colConfiguration.Width = 100;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            // 
            // colCompleted
            // 
            this.colCompleted.Text = "Completed";
            this.colCompleted.Width = 100;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1417086282_arrow-skip.png");
            this.imageList1.Images.SetKeyName(1, "1417087947_tick_circle.png");
            this.imageList1.Images.SetKeyName(2, "CPPProject_SolutionExplorerNode_.png");
            this.imageList1.Images.SetKeyName(3, "CSharpProject_SolutionExplorerNode.png");
            this.imageList1.Images.SetKeyName(4, "exclamation-red-frame.png");
            this.imageList1.Images.SetKeyName(5, "FSharpProject_SolutionExplorerNode.png");
            this.imageList1.Images.SetKeyName(6, "hourglass.png");
            this.imageList1.Images.SetKeyName(7, "PYProject_SolutionExplorerNode.png");
            this.imageList1.Images.SetKeyName(8, "RBProject_SolutionExplorerNode.png");
            this.imageList1.Images.SetKeyName(9, "Solution_8308.png");
            this.imageList1.Images.SetKeyName(10, "VBProject_SolutionExplorerNode.png");
            this.imageList1.Images.SetKeyName(11, "");
            // 
            // ucProjectHero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvView);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ucProjectHero";
            this.Size = new System.Drawing.Size(832, 164);
            this.Load += new System.EventHandler(this.ucProjectHero_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnDone;
        private System.Windows.Forms.ToolStripButton btnFailed;
        private System.Windows.Forms.ToolStripButton btnSkipped;
        private System.Windows.Forms.ToolStripButton btnPending;
        private System.Windows.Forms.ToolStripSeparator tsSep1;
        private System.Windows.Forms.ToolStripButton btnCancelBuild;
        private System.Windows.Forms.ToolStripSeparator tsSep2;
        private System.Windows.Forms.ToolStripProgressBar progBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblProjectCount;
        private OptimizedListView lvView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colConfiguration;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colCompleted;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblCompletionTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel lblQuickSync;
        private System.Windows.Forms.ToolStripButton btnManageQuickSyncBindings;
    }
}
