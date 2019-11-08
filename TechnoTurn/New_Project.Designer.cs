namespace TechnoTurn
{
    partial class New_Project
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(New_Project));
            this.formFrameSkinner = new Elegant.Ui.FormFrameSkinner();
            this.label1 = new System.Windows.Forms.Label();
            this.Pr_name = new Elegant.Ui.TextBox();
            this.pr_type = new Elegant.Ui.ComboBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pr_path = new Elegant.Ui.TextBox();
            this.button1 = new Elegant.Ui.Button();
            this.button2 = new Elegant.Ui.Button();
            this.button3 = new Elegant.Ui.Button();
            this.pr_path_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // formFrameSkinner
            // 
            this.formFrameSkinner.Form = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(40, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Project Name :";
            // 
            // Pr_name
            // 
            this.Pr_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pr_name.Id = "ad6de486-023d-4f78-9c6f-556a179b8b2c";
            this.Pr_name.Location = new System.Drawing.Point(192, 28);
            this.Pr_name.Name = "Pr_name";
            this.Pr_name.Size = new System.Drawing.Size(167, 28);
            this.Pr_name.TabIndex = 3;
            this.Pr_name.TextEditorWidth = 161;
            // 
            // pr_type
            // 
            this.pr_type.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pr_type.FormattingEnabled = false;
            this.pr_type.Id = "697091bc-bd43-44ab-b557-aa2948fd9f69";
            this.pr_type.Items.AddRange(new object[] {
            "Empty Project",
            "Java",
            "Html",
            "Php",
            "Visual basic ",
            "C Sharp"});
            this.pr_type.Location = new System.Drawing.Point(192, 89);
            this.pr_type.Name = "pr_type";
            this.pr_type.Size = new System.Drawing.Size(167, 28);
            this.pr_type.TabIndex = 4;
            this.pr_type.TextEditorWidth = 148;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(40, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Project Type :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(40, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "Project Path :";
            // 
            // pr_path
            // 
            this.pr_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pr_path.Id = "d8729efa-f785-46a5-99d6-93defbfcadef";
            this.pr_path.Location = new System.Drawing.Point(192, 152);
            this.pr_path.Name = "pr_path";
            this.pr_path.Size = new System.Drawing.Size(129, 24);
            this.pr_path.TabIndex = 7;
            this.pr_path.TextEditorWidth = 123;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Id = "5f98b191-d474-40ce-89a8-f4011f9b88bb";
            this.button1.Location = new System.Drawing.Point(234, 196);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 32);
            this.button1.TabIndex = 8;
            this.button1.Text = "&Create";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Id = "2dd5e6ae-d409-4729-8a56-3711faf44255";
            this.button2.Location = new System.Drawing.Point(322, 196);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 32);
            this.button2.TabIndex = 9;
            this.button2.Text = "C&ancel";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Id = "330870ef-8e0e-45d1-8778-055ed4660ad8";
            this.button3.Location = new System.Drawing.Point(322, 150);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(37, 28);
            this.button3.TabIndex = 10;
            this.button3.Text = "...";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pr_path_label
            // 
            this.pr_path_label.AutoSize = true;
            this.pr_path_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pr_path_label.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.pr_path_label.Location = new System.Drawing.Point(31, 226);
            this.pr_path_label.Name = "pr_path_label";
            this.pr_path_label.Size = new System.Drawing.Size(0, 13);
            this.pr_path_label.TabIndex = 11;
            // 
            // New_Project
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(436, 250);
            this.Controls.Add(this.pr_path_label);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pr_path);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pr_type);
            this.Controls.Add(this.Pr_name);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "New_Project";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New_Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Elegant.Ui.FormFrameSkinner formFrameSkinner;
        private Elegant.Ui.Button button3;
        private Elegant.Ui.Button button2;
        private Elegant.Ui.Button button1;
        private Elegant.Ui.TextBox pr_path;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Elegant.Ui.ComboBox pr_type;
        private Elegant.Ui.TextBox Pr_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label pr_path_label;

    }
}