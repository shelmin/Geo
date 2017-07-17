using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeoDemo
{
    public partial class ReadDataFromDataBase : Form
    {
        public static bool Xclick=false ;//用来判断读取的是那条轴的曲线
        public static bool Yclick=false ;
        public static string Sdps;
        public static string Edps;
      
        public ReadDataFromDataBase()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ReadDataFromDataBase_Load(object sender, EventArgs e)
        {
            //if (WellPath .Text =="")
            //{
                btnXcurve.Enabled = false;
                btnYcurve.Enabled = false;
                Sdepth.ReadOnly = true;
                Edepth.ReadOnly = true;
                 btnBind.Enabled = false  ;
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Xclick = true;
            CurvesOfSelectWell Xcurve = new CurvesOfSelectWell();
            Xcurve.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Well_DataBase Well = new Well_DataBase();
            Well.ShowDialog();
            //this.Close();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Yclick = true;
            CurvesOfSelectWell Ycurve = new CurvesOfSelectWell();
            Ycurve.ShowDialog();
        }

        private void WellPath_TextChanged(object sender, EventArgs e)//读取井数据之前不能读取曲线数据
        {
            //WellPath.Text = Wellname;
            if (WellPath.Text != "")
            {
                btnXcurve.Enabled = true;
                //btnYcurve.Enabled = true ;
            }
        }

        private void btnXcurve_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void btnYcurve_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //SysData.SdepthValue = this.Sdepth.Text;
            //SysData.EdepthValue = this.Edepth.Text;
            OK();
            this.Close();
        }

        private void Sdepth_TextChanged(object sender, EventArgs e)
        {
            SysData .textbox1=this.Sdepth.Text;
            
        }

        private void Edepth_TextChanged(object sender, EventArgs e)
        {
            if (Edepth .Text =="0"||Convert.ToString(Convert.ToDouble(Edepth.Text) - Convert.ToDouble(Sdepth.Text))=="0")
            {
                MessageBox.Show("有效深度不能为0，请重新设置","温馨提示");
            }
        }

        private void OK() 
        {
            WellLog.textBox1.Text = Sdepth.Text;
            WellLog.textBox2.Text = Convert.ToString(Convert.ToDouble(Edepth.Text) - Convert.ToDouble(Sdepth.Text));
            if (CurvesOfSelectWell.dtt != null)
            {
                SysData.dt = CurvesOfSelectWell.dtt.Clone();
                for (int i = 0; i < CurvesOfSelectWell.dtt.Rows.Count; i++)
                {
                    SysData.dt.Rows.Add(CurvesOfSelectWell.dtt.Rows[i].ItemArray);
                }
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            //SysData.textbox1 = Sdepth.Text;
            //SysData.textbox2 =Convert .ToString ( Convert .ToDouble( Edepth.Text)-Convert .ToDouble (Sdepth .Text));
            OK();
        }

        private void XcurveID_TextChanged(object sender, EventArgs e)
        {
            //XcurveID.Text = Xname;
            if (XcurveID .Text !="")
            {
                btnYcurve.Enabled = true;
            }
        }

        private void YcurveID_TextChanged(object sender, EventArgs e)
        {
            //YcurveID.Text = Yname;
            if (YcurveID .Text !="")
            {
                btnBind.Enabled = true;
                 Sdepth.ReadOnly =false ;
                Edepth.ReadOnly = false ;
                Sdepth.Text = CurvesOfSelectWell .pmin.ToString ();
                Edepth.Text = 2000.ToString ();
            }
        }
    }
}
