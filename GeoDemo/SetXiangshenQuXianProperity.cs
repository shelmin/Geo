/* 
 * 作者;肖宇博
 * 日期：2014/7/12
 * 功能：这是设置相渗曲线属性的窗体
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GeoDemo
{
    public partial class SetXiangshenQuXianProperity : Form
    {
        static double uw=0;
        static double uo=0;
        public SetXiangshenQuXianProperity()
        {
            InitializeComponent();
            #region 晁  修改
            #region  笔刷风格
            
            this.colorPickerButton1.SelectedColor = MyObject.My_Chart1.Series[0].Color;
            switch ((MyObject.My_Chart1.Series[0].BorderDashStyle).ToString())
            {
                case "Dash": this.comboBox1.SelectedIndex = 1;
                    break;
                case "Solid": this.comboBox1.SelectedIndex = 0;
                    break;
                default:
                    break;
            }
            #endregion
            this.comboBox2.SelectedIndex = -1;//将0  修改为-1
            this.radioButton1.Checked = true;
            this.comboBox3.SelectedIndex = 1;
            #region  绘制属性
            if (!((uw == 0) && (uo == 0)))//记录uw,uo 的值
            {
                this.textBox3.Text = Convert.ToString(uw);
                this.textBox4.Text = Convert.ToString(uo);
            }
            //  this.grid.SelectedIndex =Convert.ToInt32(MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.Enabled)-1;//选取有网格
            #region 有无网络
            if (MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.Enabled == true)
            {
                this.grid.SelectedIndex = 0;//选取有网络
            }
            else
            {
                this.grid.SelectedIndex = 1;
            }
            #endregion 有无网络
            this.tick.SelectedIndex = MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth - 1;//选取格线粗细
            this.colorPickerButton2.SelectedColor = MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor;
            #endregion
            #endregion  晁  修改
        }


       
        private void buttonX1_Click(object sender, EventArgs e)
        {
            //确定
            OK();
            this.Close();
        }
        Dictionary<string,string> dictionary =new Dictionary<string,string>();
        public void OK()
        {

            #region 功能

            if (MyObject.My_Chart1 == null)
            {
                return;
            }
            //每次点击前要把上一次的信息给清除
            MyObject.My_Chart1.Series[2].Points.Clear();
           //读取到Uw和Uo的值并将曲线fw画出来
            if (string.IsNullOrWhiteSpace(this.textBox3.Text) || string.IsNullOrWhiteSpace(this.textBox4.Text))
            {
                MessageBox.Show("Uw或Uo文本框不能为空！");
                return;
            }
           
            else
            {
                try
                {
                    
                     uw = Convert.ToDouble(this.textBox3.Text);
                     uo = Convert.ToDouble(this.textBox4.Text);
                    for (int i = 0; i < MyObject.My_Chart1.Series[0].Points.Count; i++)
                    {
                       double temp = FW(uw,uo,Kro[i],Krw[i]);
                       double temp1 = Convert.ToDouble(temp.ToString("0.00"));
                       MyObject.My_Chart1.Series[2].Points.AddXY(Sw[i],temp1);
                       MyObject.My_Chart1.Series[2].Points[i].ToolTip = "Sw: " + Sw[i].ToString() + "\r\n" + "fw: " + Kro[i].ToString();
                       MyObject.My_Chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                    }
                }
                catch
                {
                    MessageBox.Show("您的输入有误！");
                    return;
                }
            }

            #endregion

            #region 图形风格
            Color color = this.colorPickerButton1.SelectedColor;
            ChartDashStyle style = (ChartDashStyle)Enum.Parse(typeof(ChartDashStyle),this.dictionary[this.comboBox1.Text.ToString()]);
            if (radioButton1.Checked)
            {
                MyObject.My_Chart1.Series[0].Color = color;
                MyObject.My_Chart1.Series[0].BorderDashStyle = style;
            }
            if (radioButton2.Checked)
            {
                MyObject.My_Chart1.Series[1].Color = color;
                MyObject.My_Chart1.Series[1].BorderDashStyle = style;
            }
            if (radioButton3.Checked)
            {
                MyObject.My_Chart1.Series[2].Color = color;
                MyObject.My_Chart1.Series[2].BorderDashStyle = style;
            }


            #endregion 

            #region 有无网格
         
            if (grid.SelectedIndex >= 0)
            {
                switch (grid.SelectedItem.ToString())
                {
                    case "有网格":
                        MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                        MyObject.My_Chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                        break;
                    case "无网格":
                        MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                        MyObject.My_Chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                        break;
                }
            }
           
            #endregion

            #region 选取网格颜色
            //网格颜色
            if (colorPickerButton2.SelectedColor != Color.Empty)
            {
                MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = colorPickerButton2.SelectedColor;
                MyObject.My_Chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = colorPickerButton2.SelectedColor;
            }
            #endregion

            #region 选取网格格线的width
            //刻度线的width
            if (tick.SelectedIndex >= 0)
            {
                MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = int.Parse(tick.SelectedItem.ToString());
                MyObject.My_Chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = int.Parse(tick.SelectedItem.ToString());
            }
            #endregion

            //是否显示数值  肖宇博
            if (this.comboBox3.SelectedIndex >= 0)
            {
                if (this.comboBox3.SelectedIndex == 0)
                {
                    if (radioButton1.Checked)
                        MyObject.My_Chart1.Series[0].IsValueShownAsLabel = true;
                    else if (radioButton2.Checked)
                        MyObject.My_Chart1.Series[1].IsValueShownAsLabel = true;
                    else if (radioButton3.Checked)
                        MyObject.My_Chart1.Series[2].IsValueShownAsLabel = true;

                }
                else
                {
                    if (radioButton1.Checked)
                        MyObject.My_Chart1.Series[0].IsValueShownAsLabel = false;
                    else if (radioButton2.Checked)
                        MyObject.My_Chart1.Series[1].IsValueShownAsLabel = false;
                    else if (radioButton3.Checked)
                        MyObject.My_Chart1.Series[2].IsValueShownAsLabel = false;
                }

            }


            if (this.comboBox1.SelectedIndex >= 0)
            {
                if (radioButton1.Checked)
                {
                    
                        MyObject.My_Chart1.Series[0].LabelForeColor = label5.ForeColor;
                        MyObject.My_Chart1.Series[0].Font = label5.Font;
                    
                }
                else if (radioButton2.Checked)
                {
                    if (this.comboBox1.SelectedIndex >= 0)
                    {
                        
                            MyObject.My_Chart1.Series[1].LabelForeColor = label5.ForeColor;
                            MyObject.My_Chart1.Series[1].Font = label5.Font;
                        //}

                    }
                }
                else
                {
                    if (this.comboBox1.SelectedIndex >= 0)
                    {
                            MyObject.My_Chart1.Series[2].LabelForeColor = label5.ForeColor;
                            MyObject.My_Chart1.Series[2].Font = label5.Font;
                        //}

                    }
                }

            }

            //设置图题

            //设置文本
            switch (this.comboBox2.Text)
            {
                case "图题":
                    MyObject.My_Chart1.Titles[0].ForeColor = title.ForeColor;
                    MyObject.My_Chart1.Titles[0].Font = title.Font;
                    MyObject.My_Chart1.Titles[0].Text = title.Text;
                    break;
                case "X轴标题":
                    MyObject.My_Chart1.ChartAreas[0].AxisX.TitleForeColor = title.ForeColor;
                    MyObject.My_Chart1.ChartAreas[0].AxisX.TitleFont = title.Font;
                    MyObject.My_Chart1.ChartAreas[0].AxisX.Title = title.Text;
                    break;
                case "Y轴标题":
                    MyObject.My_Chart1.ChartAreas[0].AxisY.TitleForeColor = title.ForeColor;
                    MyObject.My_Chart1.ChartAreas[0].AxisY.TitleFont = title.Font;
                    MyObject.My_Chart1.ChartAreas[0].AxisY.Title = title.Text;
                    break;
            }



      
        }

        //fw的计算公式
        public double FW(double Uw, double Uo,double kro,double krw)
        {
            double m=(kro*Uw)/(krw*Uo);
            double fw = 1 / (1 + m);
            return fw;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            OK();
            this.Close();
        }

   
        double[] Sw,Kro,Krw;

        private void SetXiangshenQuXianProperity_Load(object sender, EventArgs e)
        {
            this.dictionary.Add("实线","Solid");
            this.dictionary.Add("虚线","Dash");
            ReadDataFromDatatable r = new ReadDataFromDatatable();
            int count = r.getCounx();
            Sw = new double[count];
            Kro = new double[count];
            Krw = new double[count];
            r.readDataService(ref Sw,ref Kro,ref Sw,ref Krw);

        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            //改变格线的风格
            OK();
            this.Close();
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPageSet_Click(object sender, EventArgs e)
        {
            //页面设置
            try
            {
                MyObject.My_Chart1.Printing.PageSetup();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Chart Control for .NET Framework", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            //预览
            try
            {
                MyObject.My_Chart1.Printing.PrintPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Chart Control for .NET Framework", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //打印
            try
            {
                MyObject.My_Chart1.Printing.Print(ShowPrintDiag.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Chart Control for .NET Framework", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControlPanel2_Click(object sender, EventArgs e)
        {

        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.title.Text = "";
            if (this.comboBox2.Text.Equals("数值"))
            {
                this.ChangeTitle.Enabled = false;
            }
            else
            {
                this.ChangeTitle.Enabled = true;
                this.ChangeTitle.TitleText = "修改" + this.comboBox2.Text;
                if (this.comboBox2.Text.Equals("图题"))
                {
                    this.title.Text = "图题";

                }
                else if (this.comboBox2.Text.Equals("X轴标题"))
                {
                    this.title.Text = "X轴标题";
                }
                else if (this.comboBox2.Text.Equals("Y轴标题"))
                {
                    this.title.Text = "Y轴标题";
                }

            }
        }

        private void buttonX11_Click(object sender, EventArgs e)
        {
            //修改颜色 
            ColorDialog diag = new ColorDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (this.comboBox2.SelectedIndex == 0 || this.comboBox2.SelectedIndex == 2 || this.comboBox2.SelectedIndex == 3)
                {
                    this.title.ForeColor = diag.Color;
                }
                else if (this.comboBox2.SelectedIndex == 1)
                {
                    this.label5.ForeColor = diag.Color;
                }

            }
        }

        private void btnChangeFont_Click(object sender, EventArgs e)
        {
            FontDialog diag = new FontDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (this.comboBox2.SelectedIndex == 0 || this.comboBox2.SelectedIndex == 2 || this.comboBox2.SelectedIndex == 3)
                {
                    this.title.Font = diag.Font;
                }
                else if (this.comboBox2.SelectedIndex == 1)
                {
                    this.label5.Font = diag.Font;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)//当选择的曲线改变时，相应的属性值也跟着改变
        {
            if (radioButton1.Checked)
            {
                this.colorPickerButton1.SelectedColor = MyObject.My_Chart1.Series[0].Color;
                switch ((MyObject.My_Chart1.Series[0].BorderDashStyle).ToString())
                {
                    case "Dash": this.comboBox1.SelectedIndex = 1;
                        break;
                    case "Solid": this.comboBox1.SelectedIndex = 0;
                        break;
                    default:
                        break;
                }
                if (MyObject.My_Chart1.Series[0].IsValueShownAsLabel == true)
                {
                    this.comboBox3.SelectedIndex = 0;
                }
                else
                {
                    this.comboBox3.SelectedIndex = 1;
                }
            }
            else if (radioButton2.Checked)
            {
                this.colorPickerButton1.SelectedColor = MyObject.My_Chart1.Series[1].Color;
                switch ((MyObject.My_Chart1.Series[1].BorderDashStyle).ToString())
                {
                    case "Dash": this.comboBox1.SelectedIndex = 1;
                        break;
                    case "Solid": this.comboBox1.SelectedIndex = 0;
                        break;
                    default:
                        break;
                }
                if (MyObject.My_Chart1.Series[1].IsValueShownAsLabel == true)
                {
                    this.comboBox3.SelectedIndex = 0;
                }
                else
                {
                    this.comboBox3.SelectedIndex = 1;
                }
            }
            else
            {
                this.colorPickerButton1.SelectedColor = MyObject.My_Chart1.Series[2].Color;
                switch ((MyObject.My_Chart1.Series[2].BorderDashStyle).ToString())
                {
                    case "Dash": this.comboBox1.SelectedIndex = 1;
                        break;
                    case "Solid": this.comboBox1.SelectedIndex = 0;
                        break;
                    default:
                        break;
                }
                if (MyObject.My_Chart1.Series[2].IsValueShownAsLabel == true)
                {
                    this.comboBox3.SelectedIndex = 0;
                }
                else
                {
                    this.comboBox3.SelectedIndex = 1;
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)//当选择的曲线改变时，线型和线的颜色也跟着改变
        {
            if (radioButton1.Checked)
            {
                this.colorPickerButton1.SelectedColor = MyObject.My_Chart1.Series[0].Color;
                switch ((MyObject .My_Chart1 .Series [0].BorderDashStyle ).ToString ())
                {
                    case "Dash": this.comboBox1.SelectedIndex = 1;
                        break;
                    case "Solid":this .comboBox1 .SelectedIndex =0;
                        break;
                    default:
                        break;
                }
                if (MyObject.My_Chart1.Series[0].IsValueShownAsLabel == true)
                {
                    this.comboBox3.SelectedIndex = 0;
                }
                else
                {
                    this.comboBox3.SelectedIndex = 1;
                }
            }
            else if (radioButton2.Checked)
            {
                this.colorPickerButton1.SelectedColor = MyObject.My_Chart1.Series[1].Color;
                switch ((MyObject.My_Chart1.Series[1].BorderDashStyle).ToString())
                {
                    case "Dash": this.comboBox1.SelectedIndex = 1;
                        break;
                    case "Solid": this.comboBox1.SelectedIndex = 0;
                        break;
                    default:
                        break;
                }
                if (MyObject.My_Chart1.Series[1].IsValueShownAsLabel == true)
                {
                    this.comboBox3.SelectedIndex = 0;
                }
                else
                {
                    this.comboBox3.SelectedIndex = 1;
                }
            }
            else
            {
                this.colorPickerButton1.SelectedColor = MyObject.My_Chart1.Series[2].Color;
                switch ((MyObject.My_Chart1.Series[2].BorderDashStyle).ToString())
                {
                    case "Dash": this.comboBox1.SelectedIndex = 1;
                        break;
                    case "Solid": this.comboBox1.SelectedIndex = 0;
                        break;
                    default:
                        break;
                }
                if (MyObject.My_Chart1.Series[2].IsValueShownAsLabel == true)
                {
                    this.comboBox3.SelectedIndex = 0;
                }
                else
                {
                    this.comboBox3.SelectedIndex = 1;
                }
            }
        }
    }
}
