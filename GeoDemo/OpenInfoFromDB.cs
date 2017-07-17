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

namespace GeoDemo
{
    public partial class OpenInfoFromDB : Form
    {
        //private System.Windows.Forms.Button btnCancel;
        ////private System.ComponentModel.Container components = null;
        private ArrayList items;
        //private System.Windows.Forms.Button btnDel;
        private bool succeed;
        private ProjectInfo ProjectInfo;
        public static string filename;
        public static bool Isclose = false;
        public static byte[] dd;
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

        public OpenInfoFromDB()
        {
            this.items = Ymhdo.Project.GetInfoes(ShareInfo.GeoAuxMapNew);
            InitializeComponent();
        }

        private void listItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listItems.SelectedIndex;
            if (index >= 0)
            {
                string plotInfoName = this.listItems.SelectedItem.ToString();
                foreach (ProjectInfo projectInfo in this.items)
                {
                    if (0 == string.Compare(plotInfoName, projectInfo.Name, true))
                    {
                        this.ProjectInfo = projectInfo;
                        this.textBox1.Text = projectInfo.Name;
                        OpenPlotInfo(projectInfo);
                        return;
                    }
                }
            }
        }

        private void OpenInfoFromDB_Load(object sender, EventArgs e)
        {
            if (items.Count == 0)
            {
                MessageBox.Show("本项目还没有地质插图信息!");
                this.Close();
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    ProjectInfo projectInfo = items[i] as ProjectInfo;
                    this.listItems.Items.Add(projectInfo.Name);
                    if (0 == i)
                    {
                        this.ProjectInfo = items[i] as ProjectInfo;
                    }
                }
                this.listItems.SelectedIndex = 0;
            }
            succeed = false;
        }

        private void OpenPlotInfo(ProjectInfo projectinfo)
        {
            MyObject.pfo1 = projectinfo;
            dd = projectinfo.Info;
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
        public ProjectInfo Projectinfo 
        {
            get 
            {
                return this.ProjectInfo;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Isclose = true;
            this.Close();
        }
    }
}
