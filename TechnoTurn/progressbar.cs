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
    public partial class progressbar : Form
    {
        string path;
        string name;
        public progressbar()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                System.Threading.Thread.Sleep(100);
                progressBar1.Value = i;
            }
            new Home().openfile(path + "/" + name);
            this.Dispose();
        }
        public void setvalues(string pathtemp, string filetemp)
        {
            path = pathtemp;
            name = filetemp;
        }
    }
}
