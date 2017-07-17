/* 
 * 作者;肖宇博
 * 日期：2014/7/14
 * 功能：这是设置生产开发曲线的属性的窗体
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace GeoDemo
{
    public partial class SetShengChanKaiFaQuXianProperity : Form
    {
        int Y_value;//Y轴偏移
        //
        int sum;//计数功能 添加曲线道按钮点击了几次 删除曲线道按钮点击了几次 
        public string r;//记录要改变哪个曲线道
        public int value;//记录X轴格式对应的序列号
        public SetShengChanKaiFaQuXianProperity()
        {
            InitializeComponent();
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 1; i <= MyObject.My_Chart1.Series[0].Points.Count; i++)
                {
                    this.cmbYOffset.Items.Add(i);
                }
            }
            cmbCtArea.SelectedIndex = 0;
            cmbSeries.SelectedIndex = 0;
        }
        
    
        private void SetShengChanKaiFaQuXianProperity_Load(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1.Series.Count < 4)
            {
                this.btnDelSeries.Enabled = false;
            }
            if (MyObject.My_Chart1.ChartAreas.Count < 4)
            {
                this.btnDelCtArea.Enabled = false;
            }
            int i = MyObject.My_Chart1.ChartAreas.Count;
            for (int k = 0; k < i-3; k++)
            {
                this.cmbCtArea.Items.Add("曲线道" + (4+k));
            }
            int b = MyObject.My_Chart1.Series.Count;
            for (int j = 0; j < b-3; j++)
            {
                this.cmbSeries.Items.Add("曲线"+(4+j));
            }
            this.dictionary.Add("实线","Solid");
            this.dictionary.Add("虚线", "Dash");
            this.dictionary2.Add("圆形","Circle");
            this.dictionary2.Add("方形", "Square");
            this.dictionary2.Add("菱形","Diamond");
            this.dictionary2.Add("三角形", "Triangle");
            this.dictionary3.Add("样条图", "Spline");
            this.dictionary3.Add("点图", "Point");
            this.dictionary3.Add("折线图", "Line");
            this.dictionary3.Add("柱形图", "Column");
            this.dictionary4.Add("d", 0);
            this.dictionary4.Add("M", 1);
            this.dictionary4.Add("Y", 2);
            this.dictionary4.Add("ddd,d", 3);
            this.dictionary4.Add("D", 4);
            this.dictionary4.Add("G", 5);


        }
        public static DateTime XDatetime;
        public static DateTimeFormatInfo dtfi;
        private string GetDateItem(int item)
        {
            string format;
            switch (item)
            {
                case 0:
                    format = "d";
                    break;
                case 1:
                    format = "M";
                    break;
                case 2:
                    format = "Y";
                    break;
                case 3:
                    format = "ddd,d";
                    break;
                case 4:
                    format = "D";
                    break;
                case 5:
                    format = "G";
                    break;
                default:
                    format = "";
                    break;
            }
            return format;
        }
       private System.Windows.Forms.DataVisualization.Charting.SeriesChartType GetSeriesStyle(int item)
        {
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType format = SeriesChartType.Spline;
            switch (item)
            {
                case 0:
                    format = SeriesChartType.Spline;
                    break;
                case 1:
                    format = SeriesChartType.Point;
                    break;
                case 2:
                    format = SeriesChartType.Line;
                    break;
                case 3:
                    format = SeriesChartType.Column;
                    break;
            }
            return format;

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            //确定按钮
            ok();
            this.Close();
        }


        private Dictionary<string, string> dictionary = new Dictionary<string, string>();//申明一个字典类型 线的样式
        private Dictionary<string, string> dictionary2 = new Dictionary<string, string>();//申明一个字典类型 数据点样式
        private Dictionary<string, string> dictionary3 = new Dictionary<string, string>();//申明一个字典类型 曲线类型
        private Dictionary<string, int> dictionary4 = new Dictionary<string, int>();//申明一个字典类型 X轴格式

        public void ok()
        {
            if (MyObject.My_Chart1 != null)
            {
                #region 改变X轴时间显示的格式
               // XDatetime=DateTime.ParseExact(MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisX.LabelStyle.ToString(), "yyyyMMdd", dtfi);
             //   XDatetime.ToString(GetDateItem(this.cmbXStyle.SelectedIndex), dtfi);
              //  MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisX.LabelStyle.ToString
                
             
                #endregion

                //ChartDashStyle style = (ChartDashStyle)Enum.Parse(typeof(ChartDashStyle), this.dictionary[this.cmbLineStyle.SelectedItem.ToString()]);
                MarkerStyle pointstyle = (MarkerStyle)Enum.Parse(typeof(MarkerStyle), this.dictionary2[this.comboBox6.SelectedItem.ToString()]);
                SeriesChartType seriesstyle = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), this.dictionary3[this.cmbSeriesStyle.SelectedItem.ToString()]);

                //改变Y轴的偏移量
                switch (cmbYOffset.SelectedItem.ToString())
                {
                    case "Max":
                        MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisX.Crossing = Double.MaxValue;
                        break;
                    case "Min":
                        MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisX.Crossing = Double.MinValue;
                        break;
                    default:
                        MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisX.Crossing = Double.Parse(this.cmbYOffset.SelectedItem.ToString());
                        break;
                }

                if (this.txtYInterval.Text != string.Empty)
                {
                    //改变Y轴刻度
                    MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisY.Interval = double.Parse(this.txtYInterval.Text.ToString());
                }
                //颜色
                MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisX.MajorGrid.LineColor = cpbtnGrid.SelectedColor;
                MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisY.MajorGrid.LineColor = cpbtnGrid.SelectedColor;

                if (chkIsGridShown.Checked == true)
                {
                    MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisX.Enabled = AxisEnabled.True;
                    MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisX.MajorGrid.Enabled = true;
                    MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisY.MajorGrid.Enabled = true;
                }
                else
                {
                    MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisX.Enabled = AxisEnabled.False;
                    MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisX.MajorGrid.Enabled = false;
                    MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisY.MajorGrid.Enabled = false;
                }

                if (chkIsValueShown.Checked == true)
                {
                    MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].IsValueShownAsLabel = true;
                }
                else
                {
                    MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].IsValueShownAsLabel = false;
                }

                if (cmbSeriesStyle.SelectedIndex == 0|| cmbSeriesStyle.SelectedIndex == 2)   //样条图||折线图           
                {
                    this.cmbLineStyle.Enabled = true;
                    cmbLineWidth.Enabled = true;
                    cpbtnLine.Enabled = true;
                    this.comboBox6.Enabled = true;
                    switch (this.cmbLineStyle.SelectedIndex)
                    {
                        case 0:
                            MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].BorderDashStyle = ChartDashStyle.Solid;
                            break;
                        case 1:
                            MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].BorderDashStyle = ChartDashStyle.Dash;
                            break;
                    }
                    
                    MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].BorderWidth = this.cmbLineWidth.SelectedIndex + 1;
                    MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].Color = cpbtnLine.SelectedColor;
                    MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].MarkerStyle = pointstyle;
                    MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].ChartType = GetSeriesStyle(cmbSeriesStyle.SelectedIndex);
                                
                }
                else if(cmbSeriesStyle.SelectedIndex == 1) //点图
                {
                    this.cmbLineStyle.Enabled = false;
                    cmbLineWidth.Enabled = false;
                    cpbtnLine.Enabled = false;
                    this.comboBox6.Enabled = true;
                    MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].MarkerStyle = pointstyle;
                    MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].ChartType = GetSeriesStyle(cmbSeriesStyle.SelectedIndex);
                }

                else if(cmbSeriesStyle.SelectedIndex == 3)//柱形图
                {
                    this.cmbLineStyle.Enabled = false;
                    cmbLineWidth.Enabled = false;
                    cpbtnLine.Enabled = false;
                    this.comboBox6.Enabled = false;
                    MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].ChartType = GetSeriesStyle(cmbSeriesStyle.SelectedIndex);
                }        
              /*  else
                {
                    MessageBox.Show("未设置曲线类型！");
                }*/
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //取消按钮
            this.Close();
        }
        public static  int i = 3;
        private void btnAddSeries_Click(object sender, EventArgs e)
        {
            //增加一条线
            i++;
            Series series = new Series();
            series.Name = "Series" + i;
            MyObject.My_Chart1.Series.Add(series);
            //把该图形设为浮雕形
            MyObject.My_Chart1.Series[series.Name]["DrawingStyle"] = "Emboss";//绘图风格默认的是方形，此时可以改变成圆柱型

            series.ChartArea = MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].Name;
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 2;

            //这里便要读取数据
            //double[] y = new double[] { 11, 133, 121.5, 212.3, 327.4, 111.2, 121.3, 164.9, 201.2, 44.9 };
            //string[] x = new string[] { "20060101", "20060301", "20060501", "20060701", "20060901", "20061101", "20070101", "20070301", "20070501", "20070701" };

            if (MessageBox.Show("您点击了添加一条线到" + r + "，那么请你请先读入一组数据", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK) 
            {
                ReadData f = new ReadData();
                f.ShowDialog();
                this.Close();
            }
        }

        private void btnDelSeries_Click(object sender, EventArgs e)
        {
            //删除一条线
            if (MyObject.My_Chart1.Series.Count < 4)
            {
                this.btnDelSeries.Enabled = false;
            }
            else
            {
                MyObject.My_Chart1.Series.RemoveAt(MyObject.My_Chart1.Series.Count - 1);
            }
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       //应用
        private void btnApply_Click(object sender, EventArgs e)
        {
            ok();
        }

        private void btnAddCtArea_Click(object sender, EventArgs e)
        {
            sum = MyObject.My_Chart1.ChartAreas.Count ;
            sum++;
            //增加一条曲线道
            ChartArea ca = new ChartArea();
            ca.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisY.MajorGrid.Enabled = false;
            //ca.Position.Auto = false;
            //ca.Position.Height = 20F;
            //ca.Position.Width = 90F;
            //ca.Position.X = 4.5F;
            //ca.Position.Y = (70f+sum*26f);
            MyObject.My_Chart1.ChartAreas.Add(ca);
            //MyObject.My_Chart1.Height += sum * 26; 
            cmbCtArea.Items.Add("曲线道" + (sum));
        }

        private void cmbCtArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            r = cmbCtArea.Text;//用来添加曲线那的文本
            //默认的曲线道是不能删除的 
            if (cmbCtArea.SelectedIndex == 0 || cmbCtArea.SelectedIndex == 1 || cmbCtArea.SelectedIndex == 2)
            {
                btnDelCtArea.Enabled = false;
            }
            else
            {
                btnDelCtArea.Enabled = true;
            }
            //X轴格式
            dictionary4.TryGetValue(MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisX.LabelStyle.Format, out value);
            cmbXStyle.SelectedIndex = value;

            //Y轴刻度
            txtYInterval.Text = Convert.ToString(MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisY.Interval);

            //Y轴偏移
            try
            {
                Y_value = Convert.ToInt32(MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisX.Crossing);
                if (Y_value == MyObject.My_Chart1.Series[0].Points.Count)
                {
                    this.cmbYOffset.SelectedIndex = 1; 
                }
                else
                {
                    if (Y_value == 0)
                    {
                        this.cmbYOffset.SelectedIndex = 0;
                    }
                    else
                    {
                        this.cmbYOffset.SelectedIndex = Y_value + 1;
                    }
                }
            }
            catch
            {
                this.cmbYOffset.SelectedIndex = 0;
            }
                    
            #region 有无网格
            if (MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisX.MajorGrid.Enabled == true && MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisY.MajorGrid.Enabled == true)
            {
                chkIsGridShown.Checked = true;
            }
            else
            {
                chkIsGridShown.Checked = false;
            }
            #endregion

            //网格颜色
            this.cpbtnGrid.SelectedColor = MyObject.My_Chart1.ChartAreas[cmbCtArea.SelectedIndex].AxisX.MajorGrid.LineColor;
        }

        private void btnDelCtArea_Click(object sender, EventArgs e)
        {//删除曲线道
            if (sum > 0)
            {
                sum--;
            }

            MyObject.My_Chart1.ChartAreas.RemoveAt(cmbCtArea.SelectedIndex);
            cmbCtArea.Items.RemoveAt(cmbCtArea.SelectedIndex);
            cmbCtArea.SelectedIndex = 0;
        }

        private void cmbSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region 是否显示数值
            if (MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].IsValueShownAsLabel == true)
            {
                chkIsValueShown.Checked = true;
            }
            else
            {
                chkIsValueShown.Checked = false;
            }
            #endregion

            if (MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].ChartType == SeriesChartType.Column)
            {
                this.cmbLineStyle.Enabled = false;
                cmbLineWidth.Enabled = false;
                cpbtnLine.Enabled = false;
                this.comboBox6.Enabled = false;
                cmbSeriesStyle.SelectedIndex = 3;
            }
            else if (MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].ChartType == SeriesChartType.Point)
            {
                this.cmbLineStyle.Enabled = false;
                cmbLineWidth.Enabled = false;
                cpbtnLine.Enabled = false;
                this.comboBox6.Enabled = true;
                this.comboBox6.SelectedIndex = Convert.ToInt32(MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].MarkerStyle) - 1;//数据点样式
                cmbSeriesStyle.SelectedIndex = 1;
            }
            
            else if(MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].ChartType == SeriesChartType.Spline|| MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].ChartType == SeriesChartType.Line)
            {
                this.cmbLineStyle.Enabled = true;
                cmbLineWidth.Enabled = true;
                cpbtnLine.Enabled = true;
                this.comboBox6.Enabled = true;
                #region 线的样式
                if (Convert.ToInt32(MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].BorderDashStyle) == 5)
                {
                    this.cmbLineStyle.SelectedIndex = 0;
                }
                else
                {
                    this.cmbLineStyle.SelectedIndex = 1;
                }
                #endregion

                cmbLineWidth.SelectedIndex = MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].BorderWidth - 1;//线的宽度
                cpbtnLine.SelectedColor = MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].Color;//线的颜色
                this.comboBox6.SelectedIndex = Convert.ToInt32(MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].MarkerStyle) - 1;//数据点样式
                if (MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].ChartType == SeriesChartType.Spline) cmbSeriesStyle.SelectedIndex = 0;
                else cmbSeriesStyle.SelectedIndex = 2;
                //cmbSeriesStyle.SelectedIndex = Convert.ToInt32(MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].ChartType) - 1;//曲线类型
            }
             
        }

        private void cmbXStyle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbYOffset_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtYInterval_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbSeriesStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSeriesStyle.SelectedIndex == 0 || cmbSeriesStyle.SelectedIndex == 2)   //样条图||折线图           
            {
                this.cmbLineStyle.Enabled = true;
                if (MyObject .My_Chart1 .Series [cmbSeries.SelectedIndex].BorderDashStyle ==ChartDashStyle.Dash )
                {
                    this.cmbLineStyle.SelectedIndex = 1;
                }
                else
                {
                    this.cmbLineStyle.SelectedIndex = 0;
                }
                
                cmbLineWidth.Enabled = true;
                cmbLineWidth.SelectedIndex = MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].BorderWidth-1;
                cpbtnLine.Enabled = true;
                cpbtnLine.SelectedColor = MyObject.My_Chart1.Series[cmbSeries.SelectedIndex].Color;
                this.comboBox6.Enabled = true;
            }
            else if (cmbSeriesStyle.SelectedIndex == 1) //点图
            {
                this.cmbLineStyle.Enabled = false;
                cmbLineWidth.Enabled = false;
                cpbtnLine.Enabled = false;
                this.comboBox6.Enabled = true;
            }

            else if (cmbSeriesStyle.SelectedIndex == 3)//柱形图
            {
                this.cmbLineStyle.Enabled = false;
                cmbLineWidth.Enabled = false;
                cpbtnLine.Enabled = false;
                this.comboBox6.Enabled = false;
            }
        }

        private void grpChgSeries_Enter(object sender, EventArgs e)
        {

        }
    }
}
