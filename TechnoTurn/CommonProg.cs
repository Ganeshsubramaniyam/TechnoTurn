using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace TechnoTurn
{
    public class CommonProg
    {
        public void writeerrorlog(string message)
        {
            FileStream fs = new FileStream("ErrorLog.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(message);
            System.Windows.Forms.MessageBox.Show(message, "TechnoTurn", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
        }
       
        public string fileextension(int type)
        {
            string extn="";
            switch (type.ToString())
            {
                case "1" : extn = "java"; break;
                case "2": extn = "html"; break;
                case "3": extn = "php"; break;
                case "4": extn = "vb"; break;
                case "5": extn = "cs"; break;
                default: extn = "txt"; break;
            }
            return extn;
        }
        
        public int projecttype(string prextn)
        {
            int index;
            switch (prextn)
            {
                case ".tnjava": index = 1; break;
                case ".tnhtml": index =2; break;
                case ".tnphp": index = 3; break;
                case ".tnvb": index = 4; break;
                case ".tncs": index = 5; break;
                default: index = 6; break;
            }
            return index;
        }
        
        public string getfilename(string name)
        {
            return name.Substring(0, name.LastIndexOf("."));
        }
        
        public int new_project_create(string path,string name,int typeval)
        {
            Directory.CreateDirectory(path + "/" + name);
            Directory.CreateDirectory(path + "/" + name + "/Resources");
            Directory.CreateDirectory(path + "/" + name + "/SourceFiles");
            File.Create(path + "/" + name + "/" + name + ".tn" + fileextension(typeval)).Dispose();
            File.Create(path+"/"+name+"/SourceFiles/"+"Main."+fileextension(typeval)).Dispose();
            return 1;

        }
        
        public int new_file_create(string path, String filename)
        {
            File.Create(path + "/" + filename).Dispose();
            return 1;
        }
    }
}
