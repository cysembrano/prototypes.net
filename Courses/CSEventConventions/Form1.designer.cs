namespace CpEventSamples20
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
         this.MoveFileButton = new System.Windows.Forms.Button();
         this.RenameFileButton = new System.Windows.Forms.Button();
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.Results = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // MoveFileButton
         // 
         this.MoveFileButton.Location = new System.Drawing.Point(178, 12);
         this.MoveFileButton.Name = "MoveFileButton";
         this.MoveFileButton.Size = new System.Drawing.Size(75, 23);
         this.MoveFileButton.TabIndex = 0;
         this.MoveFileButton.Text = "Move File";
         this.MoveFileButton.UseVisualStyleBackColor = true;
         this.MoveFileButton.Click += new System.EventHandler(this.MoveFileButton_Click);
         // 
         // RenameFileButton
         // 
         this.RenameFileButton.Location = new System.Drawing.Point(178, 42);
         this.RenameFileButton.Name = "RenameFileButton";
         this.RenameFileButton.Size = new System.Drawing.Size(75, 23);
         this.RenameFileButton.TabIndex = 1;
         this.RenameFileButton.Text = "Rename File";
         this.RenameFileButton.UseVisualStyleBackColor = true;
         this.RenameFileButton.Click += new System.EventHandler(this.RenameFileButton_Click);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(8, 16);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(161, 13);
         this.label1.TabIndex = 3;
         this.label1.Text = "Raise and Handle Custom Event";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(8, 46);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(166, 13);
         this.label2.TabIndex = 4;
         this.label2.Text = "Handle FileSystemWatcher Event";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(8, 68);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(42, 13);
         this.label3.TabIndex = 5;
         this.label3.Text = "Results";
         // 
         // Results
         // 
         this.Results.BackColor = System.Drawing.Color.White;
         this.Results.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.Results.Location = new System.Drawing.Point(11, 82);
         this.Results.Name = "Results";
         this.Results.Size = new System.Drawing.Size(242, 61);
         this.Results.TabIndex = 6;
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(264, 151);
         this.Controls.Add(this.Results);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.RenameFileButton);
         this.Controls.Add(this.MoveFileButton);
         this.Name = "Form1";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Form1";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button MoveFileButton;
      private System.Windows.Forms.Button RenameFileButton;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label Results;
   }
}

