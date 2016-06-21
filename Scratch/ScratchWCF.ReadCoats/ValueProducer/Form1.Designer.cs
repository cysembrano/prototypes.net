namespace ValueProducer
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.digestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timestampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signatureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signatureInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linarizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBinarySecretServer = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.digestToolStripMenuItem,
            this.signatureToolStripMenuItem,
            this.xMLToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1136, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // digestToolStripMenuItem
            // 
            this.digestToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timestampToolStripMenuItem});
            this.digestToolStripMenuItem.Name = "digestToolStripMenuItem";
            this.digestToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.digestToolStripMenuItem.Text = "&Digest";
            // 
            // timestampToolStripMenuItem
            // 
            this.timestampToolStripMenuItem.Name = "timestampToolStripMenuItem";
            this.timestampToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.timestampToolStripMenuItem.Text = "&SHA1Hash";
            this.timestampToolStripMenuItem.Click += new System.EventHandler(this.timestampToolStripMenuItem_Click);
            // 
            // signatureToolStripMenuItem
            // 
            this.signatureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.signatureInfoToolStripMenuItem});
            this.signatureToolStripMenuItem.Name = "signatureToolStripMenuItem";
            this.signatureToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.signatureToolStripMenuItem.Text = "&Signature";
            // 
            // signatureInfoToolStripMenuItem
            // 
            this.signatureInfoToolStripMenuItem.Name = "signatureInfoToolStripMenuItem";
            this.signatureInfoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.signatureInfoToolStripMenuItem.Text = "&HMACSHA1";
            this.signatureInfoToolStripMenuItem.Click += new System.EventHandler(this.signatureInfoToolStripMenuItem_Click);
            // 
            // xMLToolStripMenuItem
            // 
            this.xMLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.linarizeToolStripMenuItem});
            this.xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            this.xMLToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.xMLToolStripMenuItem.Text = "XML";
            // 
            // linarizeToolStripMenuItem
            // 
            this.linarizeToolStripMenuItem.Name = "linarizeToolStripMenuItem";
            this.linarizeToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.linarizeToolStripMenuItem.Text = "Linarize";
            this.linarizeToolStripMenuItem.Click += new System.EventHandler(this.linarizeToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.23172F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.76828F));
            this.tableLayoutPanel1.Controls.Add(this.txtInput, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtOutput, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.67188F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.32813F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1124, 256);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(3, 3);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtInput.Size = new System.Drawing.Size(659, 220);
            this.txtInput.TabIndex = 0;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(3, 229);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(659, 20);
            this.txtOutput.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.43929F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.56071F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtBinarySecretServer, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(668, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.54839F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.45161F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(453, 62);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Binary Secret";
            // 
            // txtBinarySecretServer
            // 
            this.txtBinarySecretServer.Location = new System.Drawing.Point(81, 3);
            this.txtBinarySecretServer.Name = "txtBinarySecretServer";
            this.txtBinarySecretServer.Size = new System.Drawing.Size(369, 20);
            this.txtBinarySecretServer.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 306);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Value Producer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem digestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timestampToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signatureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signatureInfoToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBinarySecretServer;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linarizeToolStripMenuItem;
    }
}

