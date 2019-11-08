using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Elegant.Ui;
using System.Diagnostics;
using System.Threading;
using FastColoredTextBoxNS;
using System.IO;
using System.Text.RegularExpressions;
namespace TechnoTurn
{
    public partial class Home : Form
    {
        int project_open_flag = 0;
        string static_project_path = "";
        string static_focused_file = "";
        Thread t1_hidewindow;

        TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        AutocompleteMenu popupMenu;
        string[] keywords = { "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while", "add", "alias", "ascending", "descending", "dynamic", "from", "get", "global", "group", "into", "join", "let", "orderby", "partial", "remove", "select", "set", "value", "var", "where", "yield" };
        string[] methods = { };
        string[] snippets = { "if(^)\n{\n;\n}", "if(^)\n{\n;\n}\nelse\n{\n;\n}", "for(^;;)\n{\n;\n}", "while(^)\n{\n;\n}", "do${\n^;\n}while();", "switch(^)\n{\ncase : break;\n}" };
        string[] declarationSnippets = { 
               "public class ^\n{\n}", "private class ^\n{\n}", "internal class ^\n{\n}",
               "public struct ^\n{\n;\n}", "private struct ^\n{\n;\n}", "internal struct ^\n{\n;\n}",
               "public void ^()\n{\n;\n}", "private void ^()\n{\n;\n}", "internal void ^()\n{\n;\n}", "protected void ^()\n{\n;\n}",
               "public ^{ get; set; }", "private ^{ get; set; }", "internal ^{ get; set; }", "protected ^{ get; set; }","<div>\n</div>"
               };

        private void generateAutocompletemenu()
        {
            List<AutocompleteItem> items = new List<AutocompleteItem>();

            foreach (var item in snippets)
                items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 1 });
            foreach (var item in declarationSnippets)
                items.Add(new DeclarationSnippet(item) { ImageIndex = 0 });
            foreach (var item in methods)
                items.Add(new MethodAutocompleteItem(item) { ImageIndex = 2 });
            foreach (var item in keywords)
                items.Add(new AutocompleteItem(item));

            items.Add(new InsertSpaceSnippet());
            items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
            items.Add(new InsertEnterSnippet());

            //set as autocomplete source
            popupMenu.Items.SetAutocompleteItems(items);
        }

        /// <summary>
        /// This item appears when any part of snippet text is typed
        /// </summary>
        class DeclarationSnippet : SnippetAutocompleteItem
        {
            public DeclarationSnippet(string snippet)
                : base(snippet)
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                var pattern = Regex.Escape(fragmentText);
                if (Regex.IsMatch(Text, "\\b" + pattern, RegexOptions.IgnoreCase))
                    return CompareResult.Visible;
                return CompareResult.Hidden;
            }
        }

        /// <summary>
        /// Divides numbers and words: "123AND456" -> "123 AND 456"
        /// Or "i=2" -> "i = 2"
        /// </summary>
        class InsertSpaceSnippet : AutocompleteItem
        {
            string pattern;

            public InsertSpaceSnippet(string pattern)
                : base("")
            {
                this.pattern = pattern;
            }

            public InsertSpaceSnippet()
                : this(@"^(\d+)([a-zA-Z_]+)(\d*)$")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                if (Regex.IsMatch(fragmentText, pattern))
                {
                    Text = InsertSpaces(fragmentText);
                    if (Text != fragmentText)
                        return CompareResult.Visible;
                }
                return CompareResult.Hidden;
            }

            public string InsertSpaces(string fragment)
            {
                var m = Regex.Match(fragment, pattern);
                if (m == null)
                    return fragment;
                if (m.Groups[1].Value == "" && m.Groups[3].Value == "")
                    return fragment;
                return (m.Groups[1].Value + " " + m.Groups[2].Value + " " + m.Groups[3].Value).Trim();
            }

            public override string ToolTipTitle
            {
                get
                {
                    return Text;
                }
            }
        }

        /// <summary>
        /// Inerts line break after '}'
        /// </summary>
        class InsertEnterSnippet : AutocompleteItem
        {
            Place enterPlace = Place.Empty;

            public InsertEnterSnippet()
                : base("[Line break]")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                var r = Parent.Fragment.Clone();
                while (r.Start.iChar > 0)
                {
                    if (r.CharBeforeStart == '}')
                    {
                        enterPlace = r.Start;
                        return CompareResult.Visible;
                    }

                    r.GoLeftThroughFolded();
                }

                return CompareResult.Hidden;
            }

            public override string GetTextForReplace()
            {
                //extend range
                Range r = Parent.Fragment;
                Place end = r.End;
                r.Start = enterPlace;
                r.End = r.End;
                //insert line break
                return Environment.NewLine + r.Text;
            }

            public override void OnSelected(AutocompleteMenu popupMenu, SelectedEventArgs e)
            {
                base.OnSelected(popupMenu, e);
                if (Parent.Fragment.tb.AutoIndent)
                    Parent.Fragment.tb.DoAutoIndent();
            }

            public override string ToolTipTitle
            {
                get
                {
                    return "Insert line break after '}'";
                }
            }
        }
    

        public string defaultprojectpath;

        public string defaultfileformat;

        public Home()
        {
            InitializeComponent();
        }
        
        public Home(int retval)
        {

        }
        
        public void hidewindow()
        {
            //System.Threading.Thread.Sleep(8000);
            while (true)
            {
                try
                {
                    foreach (Form frm in Application.OpenForms)
                    {

                        if (frm.Text == "Elegant UI")
                        {
                            int i;
                            // frm.Hide();
                            frm.BeginInvoke(new MethodInvoker(frm.Close));
                            t1_hidewindow.Abort();
                        }

                    }
                }
                catch (Exception e)
                {

                }

            }
        }

        private void InitStylesPriority(FastColoredTextBox fstemp)
        {
            //add this style explicitly for drawing under other styles
            fstemp.AddStyle(SameWordsStyle);
        }
        
        //private void fstemp_AutoIndentNeeded(object sender, AutoIndentEventArgs args)
        //{
        //    //block {}
        //    if (Regex.IsMatch(args.LineText, @"^[^""']*\{.*\}[^""']*$"))
        //        return;
        //    //start of block {}
        //    if (Regex.IsMatch(args.LineText, @"^[^""']*\{"))
        //    {
        //        args.ShiftNextLines = args.TabLength;
        //        return;
        //    }
        //    //end of block {}
        //    if (Regex.IsMatch(args.LineText, @"}[^""']*$"))
        //    {
        //        args.Shift = -args.TabLength;
        //        args.ShiftNextLines = -args.TabLength;
        //        return;
        //    }
        //    //label
        //    if (Regex.IsMatch(args.LineText, @"^\s*\w+\s*:\s*($|//)") &&
        //        !Regex.IsMatch(args.LineText, @"^\s*default\s*:"))
        //    {
        //        args.Shift = -args.TabLength;
        //        return;
        //    }
        //    //some statements: case, default
        //    if (Regex.IsMatch(args.LineText, @"^\s*(case|default)\b.*:\s*($|//)"))
        //    {
        //        args.Shift = -args.TabLength / 2;
        //        return;
        //    }
        //    //is unclosed operator in previous line ?
        //    if (Regex.IsMatch(args.PrevLineText, @"^\s*(if|for|foreach|while|[\}\s]*else)\b[^{]*$"))
        //        if (!Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)"))//operator is unclosed
        //        {
        //            args.Shift = args.TabLength;
        //            return;
        //        }
           
        //}

        //private void ontextchanged(FastColoredTextBox fstemp)
        //{
        //    if (fstemp.Text.Trim().StartsWith("<?xml"))
        //    {
        //        fstemp.Language = Language.XML;

        //        fstemp.ClearStylesBuffer();
        //        fstemp.Range.ClearStyle(StyleIndex.All);
        //        InitStylesPriority(fstemp);
        //        fstemp.AutoIndentNeeded -= fstemp_AutoIndentNeeded;

        //        //fstemp.OnSyntaxHighlight(new TextChangedEventArgs(fctb.Range));
        //    }
        //}

        private void fileread(FastColoredTextBox fstemp,string filename)
        {
           fstemp.Text = File.ReadAllText(filename);
            //FileStream fs = new FileStream(filename, FileMode.Open);
            //StreamReader stread = new StreamReader(fs);
            //fstemp.Text = stread.ReadToEnd();
        }

        public void opennewtab()
        {
            //System.Windows.Forms.MessageBox.Show(tabControl1.TabPages.Count.ToString());
            FastColoredTextBox fst = new FastColoredTextBox();
            Elegant.Ui.TabPage tabpg = new Elegant.Ui.TabPage();
            tabpg.TabIndex = tabControl1.TabPages.Count;
            tabControl1.TabPages.AddRange(new Elegant.Ui.TabPage[] { tabpg });
            tabControl1.SelectNextTab();
            tabControl1.SelectedTabPage.Text = "New tab" + (tabControl1.TabPages.Count).ToString();
            tabControl1.SelectedTabPage.Controls.Add(fst);
            fst.Dock = DockStyle.Fill;
            //System.Windows.Forms.MessageBox.Show(tabControl1.TabPages.Count.ToString());
            fst.Focus();
            tabControl1.SelectedTabPage.Tag = "";
        }
       
        public void openfile(string filename)
        {
            int tempstatus=0;
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (tabControl1.TabPages[i].Tag == filename)
                {
                    Elegant.Ui.MessageBox.Show("The Selected File is already open in the Editor");
                    tempstatus = 1;
                }
            }
            if (tempstatus == 0)
            {
                FastColoredTextBox fst = new FastColoredTextBox();
                Elegant.Ui.TabPage tabpg = new Elegant.Ui.TabPage();
                tabpg.TabIndex = tabControl1.TabPages.Count;
                tabControl1.TabPages.AddRange(new Elegant.Ui.TabPage[] { tabpg });
                tabControl1.SelectNextTab();
                FileInfo fio = new FileInfo(filename);
                tabControl1.SelectedTabPage.Text = fio.Name;
                tabControl1.SelectedTabPage.Tag = filename;
                tabControl1.SelectedTabPage.Controls.Add(fst);
                fst.Dock = DockStyle.Fill;
                // fst.AutoIndentNeeded += fstemp_AutoIndentNeeded;

                FileInfo fi = new FileInfo(filename);
                textboxlanguage(fst, fi.Extension);
                fileread(fst, filename);
                fst.SelectAll();
                fst.DoAutoIndent();
                fst.Text = fst.Text + "";
                static_focused_file = filename;
                if(filename.Contains(".java"))
                {
                    tb_compile.Enabled = true;
                    tb_run.Enabled = true;
                    tb_compileandrun.Enabled = true;
                }
            }
                    }

        public void createproject()
        {
            try
            {
                if (System.Windows.Forms.MessageBox.Show("Creating Project will close all the open documents. Click Ok to continue or cancel to save the open documents..", "Technoturn - New project", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                saveFileDialog1.Filter = "Java Project (*.tnjava)|*.tnjava|Html Project (*.tnhtml)|*.tnhtml|Php Project (*.tnphp)|*.tnphp|Visual basic Project (*.tnvb)|*.tnvb|CSharp Project (*.tncs)|*.tncs";
                saveFileDialog1.Title = "Create New Project...";
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileInfo fio = new FileInfo(saveFileDialog1.FileName);
                    int retval = new CommonProg().new_project_create(fio.DirectoryName, new CommonProg().getfilename(fio.Name), new CommonProg().projecttype(fio.Extension));
                    if (retval == 1)
                    {
                        openfile(fio.DirectoryName + "/" + new CommonProg().getfilename(fio.Name) + "\\SourceFiles\\Main." + new CommonProg().fileextension(new CommonProg().projecttype(fio.Extension)));
                        this.defaultprojectpath = fio.DirectoryName+"\\"+new CommonProg().getfilename(fio.Name);
                        this.defaultfileformat= new CommonProg().fileextension(new CommonProg().projecttype(fio.Extension));
                    }
                    static_project_path = fio.DirectoryName;
                }

            }
            }
            catch (Exception exp)
            {
                throw;
            }
        }

        public void createfile()
        {
            try
            {
                saveFileDialog1.Filter = "Java File (*.java)|*.java|Html File (*.html)|(*.html)|Javascript File (*.js)|(*.js)|XML File (*.xml)|(*.xml)|Php File (*.php)|(*.php) | Visual Basic File (*.vb)|(*.vb)|CSharp File (*.cs)|(*.cs)|All Files (*.*)|(*.*)";
                saveFileDialog1.Title = "Create New File...";
                saveFileDialog1.DefaultExt = "*." + this.defaultfileformat;
                saveFileDialog1.InitialDirectory = this.defaultprojectpath;
                saveFileDialog1.FileName = "";
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileInfo fio = new FileInfo(saveFileDialog1.FileName);
                    int retval = new CommonProg().new_file_create(fio.DirectoryName, fio.Name);
                    if (retval == 1)
                    {
                        openfile(fio.DirectoryName + "/" + fio.Name);
                    }

                }
            }
            catch (Exception exp)
            {
                throw;
            }
        }

        public void openproject()
        {
            try
            {
                openFileDialog1.FileName = "";
                if (System.Windows.Forms.MessageBox.Show("Opening Project will close all the open documents. Click Ok to continue or cancel to save the open documents..", "Technoturn - New project", System.Windows.Forms.MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    //openFileDialog1.Filter = "Java Project (*.tnjava)|*.tnjava|Html Project (*.tnhtml)|*.tnhtml|Php Project (*.tnphp)|*.tnphp|Visual basic Project (*.tnvb)|*.tnvb|CSharp Project (*.tncs)|*.tncs";
                    openFileDialog1.Filter = "Technoturn Project (*.tn*)|*.tn*";
                    openFileDialog1.Title = "Open a Project...";
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        FileInfo fio = new FileInfo(openFileDialog1.FileName);
                        //openfile(fio.DirectoryName + "\\SourceFiles\\Main." + new CommonProg().fileextension(new CommonProg().projecttype(fio.Extension)));
                        this.defaultprojectpath = fio.DirectoryName + "\\" + new CommonProg().getfilename(fio.Name);
                        this.defaultfileformat = new CommonProg().fileextension(new CommonProg().projecttype(fio.Extension));
                        treelist(fio.DirectoryName);
                        Explore_panel.Visible = true;
                        project_open_flag = 1;
                        static_project_path = fio.DirectoryName;
                    }
                }
            }
            catch (Exception exp)
            {
                throw;
            }
        }

        public void closeproject()
        {
            try
            {
                int count = tabControl1.TabPages.Count;
                for (int i = 0; i < count-1; i++)
                {
                    tabControl1.TabPages.RemoveAt(0);
                }
                static_focused_file = "";
                static_project_path = "";
            }
            catch (Exception exp)
            {
                throw;
            }
        }

        public void treelist(string path)
        {
            treeView1.Nodes.Clear();
            DirectoryInfo dirinfo = new DirectoryInfo(path);
            TreeNode parentnode = new TreeNode();
            parentnode = foldernodecreation(dirinfo);
            parentnode.Text = parentnode.Text+"(Solution  )";
            parentnode.ToolTipText = "Folder";
            parentnode.NodeFont = new System.Drawing.Font("Arial Black", 10, FontStyle.Bold);
            parentnode.Nodes.RemoveAt(parentnode.Nodes.Count - 1);
            treeView1.Nodes.Add(parentnode);
        }
       
        public TreeNode foldernodecreation(DirectoryInfo dirinf)
        {
            TreeNode foldernode = new TreeNode(dirinf.Name);
            foreach (var folder in dirinf.GetDirectories())
            {
                TreeNode folnode = new TreeNode();
                folnode = foldernodecreation(folder);
                folnode.ToolTipText = "Folder";
                folnode.NodeFont = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
                foldernode.Nodes.Add(folnode);
            }
            foreach (var file_val in dirinf.GetFiles())
            {
                TreeNode tnode=new TreeNode(file_val.Name);
                tnode.ToolTipText = dirinf.FullName + "\\" + file_val;
                foldernode.Nodes.Add(tnode);
            }
            return foldernode;
        }

        public void addnodes(string foldername)
        {
        }

        public void compile_Action(FileInfo fiot,int status,int flag)
        {
            save_File();
            tabControl1.SelectedTabPage.Controls.Add(Outputpanel);
            Outputpanel.Dock = DockStyle.Bottom;
            Outputpanel.Visible = true;
            OutputBox.Text = "";
            if (fiot.Extension.ToString() == ".java")
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startinfo = new System.Diagnostics.ProcessStartInfo();
                startinfo.WindowStyle = ProcessWindowStyle.Hidden;
                startinfo.UseShellExecute = false;
                startinfo.RedirectStandardOutput = true;
                startinfo.RedirectStandardInput = true;
                startinfo.RedirectStandardError = true;
                startinfo.WorkingDirectory=fiot.DirectoryName.Replace(@"\\",@"\");
                startinfo.FileName = "CMD.exe";
                if (status == 0)
                {
                    startinfo.Arguments = @"/c cd " + fiot.DirectoryName.Replace(@"\\", @"\") + " & javac " + fiot.Name;
                }
                else if (status == 1)
                {
                    startinfo.Arguments = @"/c cd " + fiot.DirectoryName.Replace(@"\\", @"\") + " & java " + fiot.Name.Replace(".java","");
                }
                process.StartInfo = startinfo;
                process.Start();
                String output = process.StandardOutput.ReadToEnd();
                String error = process.StandardError.ReadToEnd();
                if(error.Equals(""))
                {
                    if(status==0)
                    {
                        OutputBox.Text = "Compilation Successfull of "+fiot.Name;
                    }
                    else
                    {
                        OutputBox.Text = output;
                    }
                    
                }
                else
                {
                    OutputBox.Text = error;
                }
                process.WaitForExit();
            }
            else if(fiot.Extension.ToString()==".cs")
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startinfo = new System.Diagnostics.ProcessStartInfo();
                startinfo.WindowStyle = ProcessWindowStyle.Hidden;
                startinfo.UseShellExecute = false;
                startinfo.RedirectStandardOutput = true;
                startinfo.RedirectStandardInput = true;
                startinfo.RedirectStandardError = true;
                startinfo.WorkingDirectory = fiot.DirectoryName.Replace(@"\\", @"\");
                startinfo.FileName = "CMD.exe";
                if (status == 0)
                {
                    startinfo.Arguments = @"/c cd " + fiot.DirectoryName + " & csc " + fiot.Name;
                }
                else
                {
                    startinfo.Arguments = @"/c cd " + fiot.DirectoryName + " & " + fiot.Name+".exe";
                }
                process.StartInfo = startinfo;
                process.Start();
                String output = process.StandardOutput.ReadToEnd();
                String error = process.StandardError.ReadToEnd();
                if (error.Equals(""))
                {
                    if (status == 0)
                    {
                        OutputBox.Text = "Compilation Successfull of " + fiot.Name;
                    }
                    else
                    {
                        OutputBox.Text = output;
                    }

                }
                else
                {
                    OutputBox.Text = error;
                }
                process.WaitForExit();

            }
            else if(fiot.Extension.ToString()==".vb")
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startinfo = new System.Diagnostics.ProcessStartInfo();
                startinfo.WindowStyle = ProcessWindowStyle.Hidden;
                startinfo.UseShellExecute = false;
                startinfo.RedirectStandardOutput = true;
                startinfo.RedirectStandardInput = true;
                startinfo.RedirectStandardError = true;
                startinfo.WorkingDirectory = fiot.DirectoryName.Replace(@"\\", @"\");
                startinfo.FileName = "CMD.exe";
                if (status == 0)
                {
                    startinfo.Arguments = @"/c cd " + fiot.DirectoryName + " & vbc " + fiot.Name;
                }
                else
                {
                    startinfo.Arguments = @"/c cd " + fiot.DirectoryName + " & vbc " + fiot.Name;
                }
                process.StartInfo = startinfo;
                process.Start();
                String output = process.StandardOutput.ReadToEnd();
                String error = process.StandardError.ReadToEnd();
                if (error.Equals(""))
                {
                    if (status == 0)
                    {
                        OutputBox.Text = "Compilation Successfull of " + fiot.Name;
                    }
                    else
                    {
                        OutputBox.Text = output;
                    }

                }
                else
                {
                    OutputBox.Text = error;
                }
                process.WaitForExit();

            }
            else
            {
                System.Diagnostics.Process.Start(fiot.FullName);
            }
            
        }

        public void project_Actions_visiblitity()
        {
            if (tabControl1.SelectedTabPage.Tag.ToString() == "")
            {
                tb_compile.Enabled = false;
                tb_run.Enabled = false;
                tb_compileandrun.Enabled = false;
            }
            else
            {
                FileInfo fiio = new FileInfo(tabControl1.SelectedTabPage.Tag.ToString());
                if (fiio.Extension.ToString() == ".java" || fiio.Extension.ToString() == ".cs" || fiio.Extension.ToString() == ".vb")
                {
                    tb_compile.Enabled = true;
                    tb_run.Enabled = true;
                    tb_compileandrun.Enabled = true;
                }
                else
                {
                    tb_compile.Enabled = false;
                    tb_run.Enabled = false;
                    tb_compileandrun.Enabled = false;
                }
            }
        }

        public void textboxlanguage(FastColoredTextBox fstemp, string extension)
        {
            switch (extension)
            {
                case ".java": fstemp.Language = Language.JAVA; break;
                case ".vb": fstemp.Language = Language.VB; break;
                case ".cs": fstemp.Language = Language.CSharp; break;
                case ".html": fstemp.Language = Language.HTML; break;
                case ".js": fstemp.Language = Language.JS; break;
                case ".php": fstemp.Language = Language.PHP; break;
                case ".sql": fstemp.Language = Language.SQL; break;
                case ".xml": fstemp.Language = Language.XML; break;
                default: break;
            }
        }

        private void backstageViewButton1_Click(object sender, EventArgs e)
        {
            t1_hidewindow.Abort();
            this.Dispose();
            Application.Exit();
        }

        private void tb_about_Click(object sender, EventArgs e)
        {
            
        }

        private void tb_projectexplorer_Click(object sender, EventArgs e)
        {
            projectExplorer_View();
        }

        private void tb_new_project_Click(object sender, EventArgs e)
        {
            createproject();
            project_Actions_visiblitity();
        }

        public void createnewtab_project()
        {
            opennewtab();
        }

        private void tb_new_file_Click(object sender, EventArgs e)
        {
            createfile();
            project_Actions_visiblitity();
        }
      
        public void createnewtab_file()
        {
            opennewtab();
        }

        private void Home_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
            FastColoredTextBox ft = new FastColoredTextBox();
            tabControl1.SelectedTabPage.Controls.Add(ft);
            tabControl1.SelectedTabPage.Text = "Blank TextFile" + tabControl1.TabPages.Count.ToString();
            ft.Dock = DockStyle.Fill;
            t1_hidewindow = new Thread(new ThreadStart(this.hidewindow));
            t1_hidewindow.Start();
            // System.Windows.Forms.MessageBox.Show(tabControl1.TabPages.Count.ToString());
            ft.Language = Language.CSharp;
            
            Explore_panel.Visible = false;
            tableLayoutPanel1.ColumnStyles.Clear();
            popupMenu = new AutocompleteMenu(ft);
            popupMenu.Items.ImageList = imageList1;
            popupMenu.SearchPattern = @"[\w\.:=!<>]";
            popupMenu.AllowTabKey = true;
            generateAutocompletemenu();
            tabControl1.SelectedTabPage.Tag = "";
            static_focused_file = "";
            tb_compile.Enabled = false;
            tb_run.Enabled = false;
            tb_compileandrun.Enabled = false;
            Outputpanel.Visible = false;
        }

        private void save_file_actions(string path , FastColoredTextBox ftval)
        {
            try
            {
                File.WriteAllText(path, ftval.Text);
            }
            catch (Exception exp)
            {
                
                throw;
            }
        }
        private void save_as_file_actions(FastColoredTextBox ftval)
        {
            try
            {
                saveFileDialog1.Filter = "Java File (*.java)|*.java|Html File (*.html)|*.html|Javascript File (*.js)|*.js|XML File (*.xml)|*.xml|Php File (*.php)|*.php| Visual Basic File (*.vb)|*.vb|CSharp File (*.cs)|*.cs|All Files (*.*)|*.*";
                saveFileDialog1.Title = "Save As File...";
                saveFileDialog1.DefaultExt = "*." + this.defaultfileformat;
                saveFileDialog1.InitialDirectory = this.defaultprojectpath;
                saveFileDialog1.FileName = "";
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog1.FileName,ftval.Text);
                }
            }
            catch (Exception exp)
            {
                throw;
            }
        }
        private void close_current_tab()
        {
            string tabname = tabControl1.SelectedTabPage.Text;
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (tabControl1.TabPages[i].Text.Equals(tabname, StringComparison.OrdinalIgnoreCase))
                {
                    if (tabControl1.TabPages.Count == 1)
                    {
                        opennewtab();
                        tabControl1.TabPages.RemoveAt(0);
                        break;
                    }
                    tabControl1.TabPages.RemoveAt(i);
                    break;
                }
            }
        }
        public void save_notification_on_tab(object sender, AutoIndentEventArgs args)
        {
            string tempfilename = tabControl1.SelectedTabPage.Text;
            tabControl1.SelectedTabPage.Text = tempfilename + "*";
        }
        private void tb_new_tab_Click(object sender, EventArgs e)
        {
            opennewtab();
        }

        private void tb_open_file_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                openfile(openFileDialog1.FileName);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            FastColoredTextBox ft = new FastColoredTextBox();
            tabControl1.SelectedTabPage.Controls.Add(ft);
            ft.DoAutoIndent();
        }

        private void tb_open_project_Click(object sender, EventArgs e)
        {
            openproject();
            project_Actions_visiblitity();
        }

        private void Menu_new_project_Click(object sender, EventArgs e)
        {
            backstageView1.Visible = false;
            createproject();
        }

        private void Menu_new_file_Click(object sender, EventArgs e)
        {
            createfile();
        }

        private void Menu_open_project_Click(object sender, EventArgs e)
        {
            openproject();
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            t1_hidewindow.Abort();
            this.Dispose();
            Application.Exit();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.ToolTipText != "Folder")
            {
                openfile(treeView1.SelectedNode.ToolTipText);
            }
            
        }

        private void c_add_tab_Click(object sender, EventArgs e)
        {
            opennewtab();
        }

        private void c_remove_tab_Click(object sender, EventArgs e)
        {
            close_current_tab();
            static_focused_file = tabControl1.SelectedTabPage.Tag.ToString();
        }

        private void Menu_close_project_Click(object sender, EventArgs e)
        {

        }

        private void tb_save_file_Click(object sender, EventArgs e)
        {
            save_File();
        }

        private void tb_save_as_Click(object sender, EventArgs e)
        {
            FastColoredTextBox ftsf = new FastColoredTextBox();
            ftsf = (FastColoredTextBox)tabControl1.SelectedTabPage.Controls[0];
            save_as_file_actions(ftsf);
        }

        private void tb_close_file_Click(object sender, EventArgs e)
        {
            close_current_tab();
            static_focused_file = tabControl1.SelectedTabPage.Tag.ToString();
        }

        private void tb_compile_Click(object sender, EventArgs e)
        {
            FileInfo fioc = new FileInfo(tabControl1.SelectedTabPage.Tag.ToString());
            compile_Action(fioc,0,0);
       }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            project_Actions_visiblitity(); 
        }

        private void tb_run_Click(object sender, EventArgs e)
        {
            FileInfo fioc = new FileInfo(tabControl1.SelectedTabPage.Tag.ToString());
            compile_Action(fioc, 1,0);
        }

        private void tb_compileandrun_Click(object sender, EventArgs e)
        {
            FileInfo fioc = new FileInfo(tabControl1.SelectedTabPage.Tag.ToString());
            compile_Action(fioc, 0,0);
            compile_Action(fioc, 1, 0);
        }

        private void tb_developer_Click(object sender, EventArgs e)
        {
            new New_file().Show();
        }

        private void tb_close_project_Click(object sender, EventArgs e)
        {
            closeproject();
            projectExplorer_View();
            opennewtab();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Outputpanel.Visible = false;
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            t1_hidewindow.Abort();
            this.Dispose();
            Application.Exit();
        }

        public void save_File()
        {
            FastColoredTextBox ftsf = new FastColoredTextBox();
            ftsf = (FastColoredTextBox)tabControl1.SelectedTabPage.Controls[0];
            if (static_focused_file == "")
            {
                saveFileDialog1.Filter = "Java File (*.java)|*.java|Html File (*.html)|*.html|Javascript File (*.js)|*.js|XML File (*.xml)|*.xml|Php File (*.php)|*.php| Visual Basic File (*.vb)|*.vb|CSharp File (*.cs)|*.cs|All Files (*.*)|*.*";
                saveFileDialog1.Title = "Save As File...";
                saveFileDialog1.FileName = "";
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    static_focused_file = saveFileDialog1.FileName;
                    save_file_actions(static_focused_file, ftsf);
                }
            }
            else
            {
                save_file_actions(static_focused_file, ftsf);
            }
        }

        public void projectExplorer_View()
        {
            if (project_open_flag == 1)
            {
                if (Explore_panel.Visible == true)
                {
                    Explore_panel.Visible = false;
                    tableLayoutPanel1.ColumnStyles.Clear();
                    project_open_flag = 0;
                }
                else
                {
                    Explore_panel.Visible = true;
                }
            }
            else
            {
                Elegant.Ui.MessageBox.Show("No Projects is alive in Scope. Please Open a Project and try again... :(", "Technoturn", Elegant.Ui.MessageBoxButtons.OK);
            }
        }
    }
}
