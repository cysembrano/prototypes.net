namespace WindowsFormsApplication1
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
            this.buttonShowAsyncMessage = new System.Windows.Forms.Button();
            this.btnShowSyncMessage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonShowAsyncMessage
            // 
            this.buttonShowAsyncMessage.Location = new System.Drawing.Point(12, 25);
            this.buttonShowAsyncMessage.Name = "buttonShowAsyncMessage";
            this.buttonShowAsyncMessage.Size = new System.Drawing.Size(108, 23);
            this.buttonShowAsyncMessage.TabIndex = 0;
            this.buttonShowAsyncMessage.Text = "Async Message";
            this.buttonShowAsyncMessage.UseVisualStyleBackColor = true;
            this.buttonShowAsyncMessage.Click += new System.EventHandler(this.buttonShowAsyncMessage_Click);
            // 
            // btnShowSyncMessage
            // 
            this.btnShowSyncMessage.Location = new System.Drawing.Point(139, 25);
            this.btnShowSyncMessage.Name = "btnShowSyncMessage";
            this.btnShowSyncMessage.Size = new System.Drawing.Size(109, 23);
            this.btnShowSyncMessage.TabIndex = 1;
            this.btnShowSyncMessage.Text = "Sync Message";
            this.btnShowSyncMessage.UseVisualStyleBackColor = true;
            this.btnShowSyncMessage.Click += new System.EventHandler(this.btnShowSyncMessage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Wait for 10 Seconds for Message box";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 60);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnShowSyncMessage);
            this.Controls.Add(this.buttonShowAsyncMessage);
            this.Name = "Form1";
            this.Text = "Async and Sync";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonShowAsyncMessage;
        private System.Windows.Forms.Button btnShowSyncMessage;
        private System.Windows.Forms.Label label1;
    }
}

