using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32;
using System.Globalization;
using Plytmf.Net.Bottom;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;

namespace GeoDemo
{
    public partial class OpentemplateFromDB : Form
    {
        //private System.Windows.Forms.Button btnCancel;
        ////private System.ComponentModel.Container components = null;
        private ArrayList items;
        //private System.Windows.Forms.Button btnDel;
        private bool succeed;
        //private ProjectInfo projectInfo;
        private ProjectTemplate projectTemplate;
        public static string filename;
        public   static bool Isclose = false;
        public static byte[] bb;
        public static int num;
        //private CrossPlotDoc doc;
        //private System.Windows.Forms.Panel panelBottom;
        //private System.Windows.Forms.Button btnEnsure;
        //private GroupBox groupPropty;
        //private Label labelSeq;
        //private TextBox txtSeq;
        //private Label labelCreateTime;
        //private TextBox txtCreateTime;
        //private Label labelName;
        //private TextBox txtName;
        //private Panel panelUp;
        //private GroupBox groupFilter;
        //private Panel bodyPanel;
        //private GroupBox groupItems;
        //private GroupBox groupKey;
        //private Label label1;
        //private TextBox txtKey;
        //private Button btnUpdate;

        private Image newBitmap;

        public OpentemplateFromDB()
        {
            this.items = Ymhdo.Project.GetTemplates(ShareInfo.GeoAuxMapNew);
            InitializeComponent();
        }

        private void OpentemplateFromDB_Load(object sender, EventArgs e)
        {
            if (items.Count == 0)
            {
                MessageBox.Show("本库还没有地质插图模板!");
                this.Close();
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    ProjectTemplate projectTemplate = items[i] as ProjectTemplate;
                    this.listItems.Items.Add(projectTemplate.Name);
                    if (0 == i)
                    {
                        this.projectTemplate = items[i] as ProjectTemplate;
                    }
                }
                this.listItems.SelectedIndex = 0;
            }
            succeed = false;
        }

        private void listItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listItems.SelectedIndex;
            if (index >= 0)
            {
                string plotInfoName = this.listItems.SelectedItem.ToString();
                foreach (ProjectTemplate projectTemplate in this.items)
                {
                    if (0 == string.Compare(plotInfoName, projectTemplate.Name, true))
                    {
                        this.projectTemplate = projectTemplate;
                        this.textBox1.Text = projectTemplate.Name;
                        OpenPlotTemplate(projectTemplate);
                        return;
                    }
                }
            }
        }

        private void OpenPlotTemplate(ProjectTemplate projectTemplate) 
        {
            MyObject.pro1 = projectTemplate;
            bb = projectTemplate.Data;
        }

        public bool Succeed
        {
            get
            {
                return this.succeed;
            }
        }

        /// <summary>
        /// 目标项目模板
        /// </summary>
        public ProjectTemplate ProjectTemplate
        {
            get
            {
                return this.projectTemplate;
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            Isclose = true;
            this.Close();
        }

        
    }
}
