namespace ProjectHero2.Core.Dialogs
{
    partial class frmSettings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkOverrideVSOutputWindow = new System.Windows.Forms.CheckBox();
            this.chkDisplayOnSolutionAddRemove = new System.Windows.Forms.CheckBox();
            this.chkDisplayOnBuildStart = new System.Windows.Forms.CheckBox();
            this.chkDisplayOnStartup = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkEnableQuickSync = new System.Windows.Forms.CheckBox();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkOverrideVSOutputWindow);
            this.groupBox1.Controls.Add(this.chkDisplayOnSolutionAddRemove);
            this.groupBox1.Controls.Add(this.chkDisplayOnBuildStart);
            this.groupBox1.Controls.Add(this.chkDisplayOnStartup);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 121);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Display Settings";
            // 
            // chkOverrideVSOutputWindow
            // 
            this.chkOverrideVSOutputWindow.AutoSize = true;
            this.chkOverrideVSOutputWindow.Checked = true;
            this.chkOverrideVSOutputWindow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOverrideVSOutputWindow.Location = new System.Drawing.Point(6, 88);
            this.chkOverrideVSOutputWindow.Name = "chkOverrideVSOutputWindow";
            this.chkOverrideVSOutputWindow.Size = new System.Drawing.Size(200, 17);
            this.chkOverrideVSOutputWindow.TabIndex = 7;
            this.chkOverrideVSOutputWindow.Text = "Override Output Window during build";
            this.chkOverrideVSOutputWindow.UseVisualStyleBackColor = true;
            // 
            // chkDisplayOnSolutionAddRemove
            // 
            this.chkDisplayOnSolutionAddRemove.AutoSize = true;
            this.chkDisplayOnSolutionAddRemove.Checked = true;
            this.chkDisplayOnSolutionAddRemove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplayOnSolutionAddRemove.Location = new System.Drawing.Point(6, 65);
            this.chkDisplayOnSolutionAddRemove.Name = "chkDisplayOnSolutionAddRemove";
            this.chkDisplayOnSolutionAddRemove.Size = new System.Drawing.Size(268, 17);
            this.chkDisplayOnSolutionAddRemove.TabIndex = 6;
            this.chkDisplayOnSolutionAddRemove.Text = "Display Project Hero Solution Project Add/Removal";
            this.chkDisplayOnSolutionAddRemove.UseVisualStyleBackColor = true;
            // 
            // chkDisplayOnBuildStart
            // 
            this.chkDisplayOnBuildStart.AutoSize = true;
            this.chkDisplayOnBuildStart.Checked = true;
            this.chkDisplayOnBuildStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplayOnBuildStart.Location = new System.Drawing.Point(6, 42);
            this.chkDisplayOnBuildStart.Name = "chkDisplayOnBuildStart";
            this.chkDisplayOnBuildStart.Size = new System.Drawing.Size(229, 17);
            this.chkDisplayOnBuildStart.TabIndex = 5;
            this.chkDisplayOnBuildStart.Text = "Display Project Hero on Solution Build Start";
            this.chkDisplayOnBuildStart.UseVisualStyleBackColor = true;
            // 
            // chkDisplayOnStartup
            // 
            this.chkDisplayOnStartup.AutoSize = true;
            this.chkDisplayOnStartup.Checked = true;
            this.chkDisplayOnStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplayOnStartup.Location = new System.Drawing.Point(6, 19);
            this.chkDisplayOnStartup.Name = "chkDisplayOnStartup";
            this.chkDisplayOnStartup.Size = new System.Drawing.Size(238, 17);
            this.chkDisplayOnStartup.TabIndex = 4;
            this.chkDisplayOnStartup.Text = "Display Project Hero on Visual Studio Startup";
            this.chkDisplayOnStartup.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkEnableQuickSync);
            this.groupBox2.Location = new System.Drawing.Point(12, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 45);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quick Sync Settings";
            // 
            // chkEnableQuickSync
            // 
            this.chkEnableQuickSync.AutoSize = true;
            this.chkEnableQuickSync.Location = new System.Drawing.Point(6, 20);
            this.chkEnableQuickSync.Name = "chkEnableQuickSync";
            this.chkEnableQuickSync.Size = new System.Drawing.Size(117, 17);
            this.chkEnableQuickSync.TabIndex = 0;
            this.chkEnableQuickSync.Text = "Enable Quick Sync";
            this.chkEnableQuickSync.UseVisualStyleBackColor = true;
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveChanges.Location = new System.Drawing.Point(107, 190);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(140, 23);
            this.btnSaveChanges.TabIndex = 7;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 218);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Hero Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkOverrideVSOutputWindow;
        private System.Windows.Forms.CheckBox chkDisplayOnSolutionAddRemove;
        private System.Windows.Forms.CheckBox chkDisplayOnBuildStart;
        private System.Windows.Forms.CheckBox chkDisplayOnStartup;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkEnableQuickSync;
        private System.Windows.Forms.Button btnSaveChanges;
    }
}