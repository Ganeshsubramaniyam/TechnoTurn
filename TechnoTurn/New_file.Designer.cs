namespace TechnoTurn
{
    partial class New_file
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
            this.formFrameSkinner = new Elegant.Ui.FormFrameSkinner();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.File_path_label = new System.Windows.Forms.Label();
            this.label1 = new Elegant.Ui.Label();
            this.label2 = new Elegant.Ui.Label();
            this.label3 = new Elegant.Ui.Label();
            this.label4 = new Elegant.Ui.Label();
            this.SuspendLayout();
            // 
            // formFrameSkinner
            // 
            this.formFrameSkinner.Form = this;
            // 
            // File_path_label
            // 
            this.File_path_label.AutoSize = true;
            this.File_path_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.File_path_label.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.File_path_label.Location = new System.Drawing.Point(24, 113);
            this.File_path_label.Name = "File_path_label";
            this.File_path_label.Size = new System.Drawing.Size(0, 13);
            this.File_path_label.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "Name :";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(118, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 20);
            this.label2.TabIndex = 23;
            this.label2.Text = "Ganesh Subramaniyam";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 20);
            this.label3.TabIndex = 24;
            this.label3.Text = "Email :";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(118, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(250, 20);
            this.label4.TabIndex = 25;
            this.label4.Text = "ganeshsubramaniyamv@gmail.com";
            // 
            // New_file
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(398, 124);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.File_path_label);
            this.Name = "New_file";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Developer - TechnoTurn";
            this.Load += new System.EventHandler(this.New_file_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Elegant.Ui.FormFrameSkinner formFrameSkinner;
        private System.Windows.Forms.Label File_path_label;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private Elegant.Ui.Label label1;
        private Elegant.Ui.Label label2;
        private Elegant.Ui.Label label4;
        private Elegant.Ui.Label label3;
    }
}