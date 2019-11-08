using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechnoTurn
{
    public partial class New_Project : Form
    {
        Home parent;
        public New_Project(Home param)
        {
            InitializeComponent();
            parent = param;
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pr_path.Text = folderBrowserDialog1.SelectedPath;
                pr_path_label.Text = pr_path.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CommonProg pgcls = new CommonProg();
            try
            {
               int retval = pgcls.new_project_create(pr_path.Text, Pr_name.Text,pr_type.SelectedIndex);
               if (retval == 1)
               {
                   //new Home().createnewtab_project();
                   //new Home().Show();
                   //this.Dispose();
                   parent.openfile(pr_path.Text+"/"+Pr_name.Text+"/main."+pgcls.fileextension(pr_type.SelectedIndex));
                   this.Dispose();
               }
            }
            catch (Exception ex)
            {
                pgcls.writeerrorlog(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
