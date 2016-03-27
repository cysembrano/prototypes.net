namespace Convergys.Assist.Sched
{
    partial class FrmAgentSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAgentSchedule));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpEmpList = new System.Windows.Forms.GroupBox();
            this.btnLoadEmpIds = new System.Windows.Forms.Button();
            this.txtEmpIds = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnLoadByTeam = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.reloadTeamsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpEmpList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.reloadTeamsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(891, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 367);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(891, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtResult);
            this.splitContainer1.Size = new System.Drawing.Size(891, 343);
            this.splitContainer1.SplitterDistance = 441;
            this.splitContainer1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.grpEmpList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.17784F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.82216F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(441, 343);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grpEmpList
            // 
            this.grpEmpList.Controls.Add(this.btnLoadEmpIds);
            this.grpEmpList.Controls.Add(this.txtEmpIds);
            this.grpEmpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEmpList.Location = new System.Drawing.Point(3, 3);
            this.grpEmpList.MinimumSize = new System.Drawing.Size(430, 244);
            this.grpEmpList.Name = "grpEmpList";
            this.grpEmpList.Size = new System.Drawing.Size(435, 244);
            this.grpEmpList.TabIndex = 0;
            this.grpEmpList.TabStop = false;
            this.grpEmpList.Text = "By CSV Emp Ids";
            // 
            // btnLoadEmpIds
            // 
            this.btnLoadEmpIds.Location = new System.Drawing.Point(3, 203);
            this.btnLoadEmpIds.Name = "btnLoadEmpIds";
            this.btnLoadEmpIds.Size = new System.Drawing.Size(110, 23);
            this.btnLoadEmpIds.TabIndex = 1;
            this.btnLoadEmpIds.Text = "Load Schedule";
            this.btnLoadEmpIds.UseVisualStyleBackColor = true;
            this.btnLoadEmpIds.Click += new System.EventHandler(this.btnLoadEmpIds_Click);
            // 
            // txtEmpIds
            // 
            this.txtEmpIds.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtEmpIds.Location = new System.Drawing.Point(3, 16);
            this.txtEmpIds.MaxLength = 1000;
            this.txtEmpIds.Multiline = true;
            this.txtEmpIds.Name = "txtEmpIds";
            this.txtEmpIds.Size = new System.Drawing.Size(429, 181);
            this.txtEmpIds.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.btnLoadByTeam);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 253);
            this.groupBox1.MinimumSize = new System.Drawing.Size(430, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 87);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "By Team";
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 16);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(429, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // btnLoadByTeam
            // 
            this.btnLoadByTeam.Location = new System.Drawing.Point(0, 58);
            this.btnLoadByTeam.Name = "btnLoadByTeam";
            this.btnLoadByTeam.Size = new System.Drawing.Size(110, 23);
            this.btnLoadByTeam.TabIndex = 2;
            this.btnLoadByTeam.Text = "Load Schedule";
            this.btnLoadByTeam.UseVisualStyleBackColor = true;
            this.btnLoadByTeam.Click += new System.EventHandler(this.btnLoadByTeam_Click);
            // 
            // txtResult
            // 
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Location = new System.Drawing.Point(0, 0);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(446, 343);
            this.txtResult.TabIndex = 0;
            // 
            // reloadTeamsToolStripMenuItem
            // 
            this.reloadTeamsToolStripMenuItem.Name = "reloadTeamsToolStripMenuItem";
            this.reloadTeamsToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.reloadTeamsToolStripMenuItem.Text = "Reload Teams";
            this.reloadTeamsToolStripMenuItem.Click += new System.EventHandler(this.reloadTeamsToolStripMenuItem_Click);
            // 
            // FrmAgentSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 389);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(907, 427);
            this.Name = "FrmAgentSchedule";
            this.Text = "Convergys Assist Agent Schedule Loader";
            this.Load += new System.EventHandler(this.FrmAgentSchedule_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpEmpList.ResumeLayout(false);
            this.grpEmpList.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpEmpList;
        private System.Windows.Forms.Button btnLoadEmpIds;
        private System.Windows.Forms.TextBox txtEmpIds;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnLoadByTeam;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem reloadTeamsToolStripMenuItem;
    }
}