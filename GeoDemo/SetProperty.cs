/*
 *作者：肖宇博
 *时间：2014/6/19
 * 功能:设置非饼图窗体的属性
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
    public partial class SetProperty : Form
    {

        public SetProperty()
        {
            InitializeComponent();

            #region 绘制属性

            XMaximum.Text = Convert.ToString(MyObject.My_Chart1.ChartAreas[0].AxisX.Maximum);//X轴右刻度
            XMinimum.Text = Convert.ToString(MyObject.My_Chart1.ChartAreas[0].AxisX.Minimum);//X轴左刻度
            YMaximum.Text = Convert.ToString(MyObject.My_Chart1.ChartAreas[0].AxisY.Maximum);//Y轴上刻度
            YMinimum.Text = Convert.ToString(MyObject.My_Chart1.ChartAreas[0].AxisY.Minimum);//Y轴下刻度
            XAxis.SelectedIndex = Convert.ToUInt16(MyObject.My_Chart1.Series[0].XAxisType);//X轴
            YAxis.SelectedIndex = Convert.ToUInt16(MyObject.My_Chart1.Series[0].YAxisType);//Y轴

            #region X轴分区数
            if (MyObject.My_Chart1.ChartAreas[0].AxisX.Interval != 0)
            {
                Xfenqushu.Text = Convert.ToString((MyObject.My_Chart1.ChartAreas[0].AxisX.Maximum - MyObject.My_Chart1.ChartAreas[0].AxisX.Minimum) / MyObject.My_Chart1.ChartAreas[0].AxisX.Interval);
            }
            else
            {
                Xfenqushu.Text = Convert.ToString(0);
            }
            #endregion

            #region Y轴分区数
            if (MyObject.My_Chart1.ChartAreas[0].AxisY.Interval != 0)
            {
                Yfenqushu.Text = Convert.ToString((MyObject.My_Chart1.ChartAreas[0].AxisY.Maximum - MyObject.My_Chart1.ChartAreas[0].AxisY.Minimum) / MyObject.My_Chart1.ChartAreas[0].AxisY.Interval);
            }
            else
            {
                Yfenqushu.Text = Convert.ToString(5);
            }
            #endregion

            #region 是否显示图例项
            if (MyObject.My_Chart1.Legends[0].Enabled)//
            {
                this.comboBox3.SelectedIndex = 0;
            }
            else
            {
                this.comboBox3.SelectedIndex = 1;
            }
            #endregion

            #region 图例项显示方式
            if (MyObject.My_Chart1.Legends[0].Docking == Docking.Top)
            {
                this.comboBox5.SelectedIndex = 0;
            }
            else if (MyObject.My_Chart1.Legends[0].Docking == Docking.Bottom)
            {
                this.comboBox5.SelectedIndex = 1;
            }
            else if (MyObject.My_Chart1.Legends[0].Docking == Docking.Left)
            {
                this.comboBox5.SelectedIndex = 2;
            }
            else
            {
                this.comboBox5.SelectedIndex = 3;
            }
            #endregion

            #endregion


            #region 文本特色

            this.comboBox2.SelectedIndex = -1;//选择文本
            title.Text = MyObject.My_Chart1.Titles[0].Text;

            #region 是否显示数值
            if (MyObject.My_Chart1.Series[0].IsValueShownAsLabel)
            {
                this.comboBox1.SelectedIndex = 0;
            }
            else
            {
                this.comboBox1.SelectedIndex = 1;
            }
            #endregion

            #endregion

            if (MyObject.My_Chart1.Series[0].ChartType == SeriesChartType.Point || MyObject.My_Chart1.Series[0].ChartType == SeriesChartType.Spline || MyObject.My_Chart1.Series[0].ChartType == SeriesChartType.Line)
            {
                ChartStyle.Visible = false;
                PointStyle.Visible = true;
            }
            else
            {
                ChartStyle.Visible = true;
                PointStyle.Visible = false;
            }

            #region 笔刷风格1
            if (ChartStyle.Visible)
            {

                #region 有无网格
                if (MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.Enabled == true && MyObject.My_Chart1.ChartAreas[0].AxisY.MajorGrid.Enabled == true)
                {
                    grid.SelectedIndex = 0;
                }
                else
                {
                    grid.SelectedIndex = 1;
                }
                #endregion

                tick.SelectedIndex = MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth - 1;//网格粗细
                colorPickerButton3.SelectedColor = MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor;//网格颜色

                this.comboBox6.SelectedIndex = 0;//选择对象
                colorPickerButton1.SelectedColor = MyObject.My_Chart1.Series[0].BorderColor;//边框颜色
                BorderSizeCom.SelectedIndex = MyObject.My_Chart1.Series[0].BorderWidth - 1;//边框宽度
                BorderDashStyleCom.SelectedIndex = Convert.ToInt16(MyObject.My_Chart1.Series[0].BorderDashStyle);//边框风格                                                                   

                #region 填充方式
                switch (Convert.ToString(MyObject.My_Chart1.Series[0].BackHatchStyle))
                {
                    case "None": HatchingCom.SelectedIndex = 0; break;
                    case "Cross": HatchingCom.SelectedIndex = 1; break;
                    case "Vertical": HatchingCom.SelectedIndex = 2; break;
                    case "Horizontal": HatchingCom.SelectedIndex = 3; break;
                    case "BackwardDiagonal": HatchingCom.SelectedIndex = 4; break;
                    case "ForwardDiagonal": HatchingCom.SelectedIndex = 5; break;
                    case "OutlinedDiamond": HatchingCom.SelectedIndex = 6; break;
                    case "Weave": HatchingCom.SelectedIndex = 7; break;
                }
                #endregion

                ShadowOffset.SelectedIndex = MyObject.My_Chart1.Series[0].ShadowOffset;//阴影偏移量
                GradientCom.SelectedIndex = Convert.ToInt16(MyObject.My_Chart1.Series[0].BackGradientStyle);//颜色分布
                colorPickerButton2.SelectedColor = MyObject.My_Chart1.Series[0].Color;//颜色

            }



            #endregion


            #region 笔刷风格2

            if (PointStyle.Visible)
            {

                #region 有无网格
                if (MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.Enabled == true && MyObject.My_Chart1.ChartAreas[0].AxisY.MajorGrid.Enabled == true)
                {
                    grid2.SelectedIndex = 0;
                }
                else
                {
                    grid2.SelectedIndex = 1;
                }
                #endregion

                tick2.SelectedIndex = MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth - 1;//网格粗细
                colorPickerButton6.SelectedColor = MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor;//网格颜色

                colorPickerButton7.SelectedColor = MyObject.My_Chart1.Series[0].MarkerBorderColor;//边框颜色
                Bodersize.SelectedIndex = MyObject.My_Chart1.Series[0].MarkerBorderWidth - 1;//边框大小
                colorPickerButton8.SelectedColor = MyObject.My_Chart1.Series[0].MarkerColor;//数据点颜色
                numericUpDown1.Value = MyObject.My_Chart1.Series[0].MarkerSize;//数据点尺寸

                this.comboBox14.SelectedIndex = 0;//选择对象 图形或图例项
                Linesize.SelectedIndex = MyObject.My_Chart1.Series[0].BorderWidth - 1;//线宽
                linestyle.SelectedIndex = Convert.ToInt16(MyObject.My_Chart1.Series[0].BorderDashStyle);//线型
                colorPickerButton5.SelectedColor = MyObject.My_Chart1.Series[0].Color;//柱体颜色
                shadowOffSet2.SelectedIndex = MyObject.My_Chart1.Series[0].ShadowOffset;//阴影偏移量

            }
            #endregion

            
            
        }

            /// <summary>
            /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// </summary>
        Dictionary<string, string> AxisDictionary = new Dictionary<string, string>();
        Dictionary<string, string> ColorDistributeDictionary = new Dictionary<string, string>();
        Dictionary<string, string> HatchDictionary = new Dictionary<string, string>();
        Dictionary<string, string> LineStyleDictionary = new Dictionary<string, string>();
        Dictionary<string, string> MarkerStyleDictionary = new Dictionary<string, string>();
        
        private void btnCancel_Click(object sender, EventArgs e)//当图形属性窗口里面的取消按钮被点击时
        {
            this.Close();
        }
        
        private void btnOK_Click(object sender, EventArgs e)//当图形属性窗口里面的确定按钮被点击时
        {
            OK();//无论是图形属性窗口里面的确定按钮还是格线和文本属性里的确定按钮被点击
            this.Close();//全部设计了之后点击确定按钮并且退出设计窗体
        }

        private void btnPageSet_Click(object sender, EventArgs e)
        {
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
            try
            {
                MyObject.My_Chart1.Printing.Print(ShowPrintDiag.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Chart Control for .NET Framework", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDefaultSet_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(colorPickerButton1.SelectedColor.ToString());
            //回复默认设置
            HatchingCom.SelectedIndex = 0;
            GradientCom.SelectedIndex = 0;
            colorPickerButton1.SelectedColor = Color.Empty;//bug出在这里。。 colorPickerButton1初始值不为Color.FromArgb(0, 0, 0)
            colorPickerButton2.SelectedColor = Color.Empty;
            BorderSizeCom.SelectedIndex = 0;
            BorderDashStyleCom.SelectedIndex = 5;
            ShadowOffset.SelectedIndex = 0;
        }

        private void btnOK2_Click(object sender, EventArgs e)
        {
            OK();
            //全部设计了之后点击确定按钮并且退出设计窗体
            this.Close();
        }

        private void btnChangeFont_Click(object sender, EventArgs e)
        {
            FontDialog diag = new FontDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                this.title.Font = diag.Font;
            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        public void Cancel()
        {

        }
        public void OK()//无论是哪个确定按钮被点击了都调用同一个方法
        {
            if (ChartStyle.Visible)
            {
                if (this.SeriseIndex.SelectedIndex == -1)
                {
                    this.SeriseIndex.SelectedIndex = 0;
                }
            }

            #region 当图形1风格确定按钮被点击的时候

            #region 设置(折线/柱体/数据点）颜色
            if (ChartStyle.Visible)
            {
                for (int i = 0; i < MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].Points.Count; i++)
                {
                    MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].Points[i].Color = colorPickerButton2.SelectedColor;
                }
                MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].Color = colorPickerButton2.SelectedColor;
                if (GradientCom.SelectedItem != null)
                {
                    MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].BackGradientStyle = (GradientStyle)GradientStyle.Parse(typeof(GradientStyle), this.ColorDistributeDictionary[GradientCom.SelectedItem.ToString()]);
                    MyObject.My_Chart1.Legends[0].BackGradientStyle = (GradientStyle)GradientStyle.Parse(typeof(GradientStyle), this.ColorDistributeDictionary[GradientCom.SelectedItem.ToString()]);

                }
                if (this.HatchingCom.SelectedItem != null)
                {
                    MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].BackHatchStyle = (ChartHatchStyle)ChartHatchStyle.Parse(typeof(ChartHatchStyle), this.HatchDictionary[HatchingCom.SelectedItem.ToString()]);
                    MyObject.My_Chart1.Legends[0].BackHatchStyle = (ChartHatchStyle)ChartHatchStyle.Parse(typeof(ChartHatchStyle), this.HatchDictionary[HatchingCom.SelectedItem.ToString()]);
                }
            }
            if (PointStyle.Visible)
            {
                for (int i = 0; i < MyObject.My_Chart1.Series[this.seriesIndex2.SelectedIndex].Points.Count; i++)
                {
                    MyObject.My_Chart1.Series[this.seriesIndex2.SelectedIndex].Points[i].Color = colorPickerButton5.SelectedColor;
                }
                MyObject.My_Chart1.Series[this.seriesIndex2.SelectedIndex].Color = colorPickerButton5.SelectedColor;
                if (this.colorPickerButton8.Enabled)
                {
                    MyObject.My_Chart1.Series[this.seriesIndex2.SelectedIndex].MarkerColor = colorPickerButton8.SelectedColor;
                }


            }


            #endregion

            #region 设置BackGradientStyle


            #endregion

            #region 设置（数据点边框/柱体边框）颜色 数据点边框大小
            if (ChartStyle.Visible)
            {
                if (comboBox6.SelectedIndex >= 0)
                {
                    if (comboBox6.SelectedIndex == 0)
                    {
                        for (int i = 0; i < MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].Points.Count; i++)
                        {
                            MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].Points[i].BorderColor = colorPickerButton1.SelectedColor;
                        }
                        MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].BorderColor = colorPickerButton1.SelectedColor;

                    }
                    else
                    {
                        MyObject.My_Chart1.Legends[0].BorderColor = colorPickerButton1.SelectedColor;
                    }
                }
            }
            if (PointStyle.Visible)
            {
                if (this.Bodersize.Enabled)
                {
                    MyObject.My_Chart1.Series[this.seriesIndex2.SelectedIndex].MarkerBorderColor = colorPickerButton7.SelectedColor;
                    MyObject.My_Chart1.Series[this.seriesIndex2.SelectedIndex].MarkerBorderWidth = Bodersize.SelectedIndex + 1;
                }
            }



            #endregion

            #region 设置BackHatchStyle背景填充方式




            #endregion

            #region 设置（折线/柱体边框/数据点）尺寸
            if (ChartStyle.Visible)
            {
                if (comboBox6.SelectedIndex >= 0)
                {
                    if (comboBox6.SelectedIndex == 0)
                    {
                        for (int i = 0; i < MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].Points.Count; i++)
                        {
                            MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].Points[i].BorderWidth = int.Parse(BorderSizeCom.GetItemText(BorderSizeCom.SelectedItem));
                        }
                        MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].BorderWidth = int.Parse(BorderSizeCom.GetItemText(BorderSizeCom.SelectedItem));

                    }
                    else
                    {
                        MyObject.My_Chart1.Legends[0].BorderWidth = int.Parse(BorderSizeCom.GetItemText(BorderSizeCom.SelectedItem));
                    }
                }
            }
            if (PointStyle.Visible)
            {
                if (this.numericUpDown1.Enabled)
                {
                    MyObject.My_Chart1.Series[this.seriesIndex2.SelectedIndex].MarkerSize = Convert.ToInt32(this.numericUpDown1.Value);
                }
                if (comboBox14.SelectedIndex >= 0)
                {
                    if (comboBox14.SelectedIndex == 0)
                    {
                        MyObject.My_Chart1.Series[this.seriesIndex2.SelectedIndex].BorderWidth = int.Parse(Linesize.GetItemText(Linesize.SelectedItem));

                    }
                    else
                    {
                        MyObject.My_Chart1.Legends[0].BorderWidth = int.Parse(Linesize.GetItemText(Linesize.SelectedItem));
                    }
                }
            }


            #endregion

            #region 设置（折线/柱体边框/数据点）样式
            if (ChartStyle.Visible)
            {
                if (comboBox6.SelectedIndex >= 0)
                {
                    if (comboBox6.SelectedIndex == 0)
                    {
                        for (int i = 0; i < MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].Points.Count; i++)
                        {
                            MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].Points[i].BorderDashStyle = (ChartDashStyle)ChartDashStyle.Parse(typeof(ChartDashStyle), this.LineStyleDictionary[BorderDashStyleCom.SelectedItem.ToString()]);
                        }
                        MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].BorderDashStyle = (ChartDashStyle)ChartDashStyle.Parse(typeof(ChartDashStyle), this.LineStyleDictionary[BorderDashStyleCom.SelectedItem.ToString()]);

                    }
                    else
                    {
                        MyObject.My_Chart1.Legends[0].BorderDashStyle = (ChartDashStyle)ChartDashStyle.Parse(typeof(ChartDashStyle), this.LineStyleDictionary[BorderDashStyleCom.SelectedItem.ToString()]);
                    }
                }
            }
            if (PointStyle.Visible)
            {
                if (this.Pointshape.Enabled)
                {
                    MyObject.My_Chart1.Series[this.seriesIndex2.SelectedIndex].MarkerStyle = (MarkerStyle)MarkerStyle.Parse(typeof(MarkerStyle), this.MarkerStyleDictionary[Pointshape.SelectedItem.ToString()]);
                }
                if (comboBox14.SelectedIndex >= 0)
                {
                    if (comboBox14.SelectedIndex == 0)
                    {
                        MyObject.My_Chart1.Series[this.seriesIndex2.SelectedIndex].BorderDashStyle = (ChartDashStyle)ChartDashStyle.Parse(typeof(ChartDashStyle), this.LineStyleDictionary[linestyle.SelectedItem.ToString()]);

                    }
                    else
                    {
                        MyObject.My_Chart1.Legends[0].BorderDashStyle = (ChartDashStyle)ChartDashStyle.Parse(typeof(ChartDashStyle), this.LineStyleDictionary[linestyle.SelectedItem.ToString()]);
                    }
                }
            }



            #endregion

            #region 阴影偏移量
            if (ChartStyle.Visible)
            {
                if (ShadowOffset.SelectedItem != null)
                {
                    MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].ShadowOffset = int.Parse(ShadowOffset.GetItemText(ShadowOffset.SelectedItem));
                    MyObject.My_Chart1.Legends[0].ShadowOffset = int.Parse(ShadowOffset.GetItemText(ShadowOffset.SelectedItem));
                }
            }
            if (PointStyle.Visible)
            {
                if (shadowOffSet2.SelectedItem != null)
                {
                    MyObject.My_Chart1.Series[this.seriesIndex2.SelectedIndex].ShadowOffset = int.Parse(shadowOffSet2.GetItemText(shadowOffSet2.SelectedItem));
                    MyObject.My_Chart1.Legends[0].ShadowOffset = int.Parse(shadowOffSet2.GetItemText(shadowOffSet2.SelectedItem));
                }
            }


            #endregion


            #endregion




            #region 当格线风格和文本风格确定按钮被点击的时候

            #region 选取有无网格
            if (ChartStyle.Visible && grid.SelectedIndex >= 0)
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

            if (PointStyle.Visible && grid2.SelectedIndex >= 0)
            {
                switch (grid2.SelectedItem.ToString())
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

            #region 选取网格格线的width
            //刻度线的width
            if (ChartStyle.Visible && tick.SelectedIndex >= 0)
            {
                MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = int.Parse(tick.SelectedItem.ToString());
                MyObject.My_Chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = int.Parse(tick.SelectedItem.ToString());
            }

            if (PointStyle.Visible && tick2.SelectedIndex >= 0)
            {
                MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = int.Parse(tick2.SelectedItem.ToString());
                MyObject.My_Chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = int.Parse(tick2.SelectedItem.ToString());
            }
            #endregion

            #region 选取网格颜色
            //网格颜色
            if (ChartStyle.Visible && colorPickerButton3.SelectedColor != Color.Empty)
            {
                MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = colorPickerButton3.SelectedColor;
                MyObject.My_Chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = colorPickerButton3.SelectedColor;
            }

            if (PointStyle.Visible && colorPickerButton6.SelectedColor != Color.Empty)
            {
                MyObject.My_Chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = colorPickerButton6.SelectedColor;
                MyObject.My_Chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = colorPickerButton6.SelectedColor;
            }
            #endregion

            #region 选取Y轴上刻度
            if (YMaximum.Text != string.Empty)
            {
                //判断如果输入的不是数字
                MyObject.My_Chart1.ChartAreas[0].AxisY.Maximum = double.Parse(YMaximum.Text.ToString());
            }
            else
            {
                MessageBox.Show("Y轴上刻度的输入值有误！");
            }
            #endregion
            #region 选取Y轴分区数
            if (Yfenqushu.Text != string.Empty && Yfenqushu.Text != Convert.ToString(0))
            {
                //判断如果输入的不是数字且输入的数不为0
                MyObject.My_Chart1.ChartAreas[0].AxisY.Interval = (MyObject.My_Chart1.ChartAreas[0].AxisY.Maximum - MyObject.My_Chart1.ChartAreas[0].AxisY.Minimum) / double.Parse(Yfenqushu.Text.ToString());
            }
            else
            {
                MessageBox.Show("Y轴分区数的输入值有误！");
            }
            #endregion
            #region 选取Y轴下刻度
            if (YMinimum.Text != string.Empty)
            {
                //判断如果输入的不是数字
                MyObject.My_Chart1.ChartAreas[0].AxisY.Minimum = double.Parse(YMinimum.Text.ToString());
            }
            else
            {
                MessageBox.Show("Y轴下刻度的输入值有误！");
            }
            #endregion
            #region 选取X轴左刻度
            if (XMaximum.Text != string.Empty)
            {
                //判断如果输入的不是数字
                MyObject.My_Chart1.ChartAreas[0].AxisX.Maximum = double.Parse(XMaximum.Text.ToString());
            }
            else
            {
                MessageBox.Show("X轴左刻度的输入值有误！");
            }
            #endregion
            #region 选取X轴分区数
            if (Xfenqushu.Text != string.Empty && Xfenqushu.Text != Convert.ToString(0))
            {
                //判断如果输入的不是数字且输入的数不为0
                MyObject.My_Chart1.ChartAreas[0].AxisX.Interval = (MyObject.My_Chart1.ChartAreas[0].AxisX.Maximum - MyObject.My_Chart1.ChartAreas[0].AxisX.Minimum) / double.Parse(Xfenqushu.Text.ToString());
            }
            else
            {
                MessageBox.Show("X轴分区数的输入值有误！");
            }
            #endregion
            #region 选取X轴右刻度
            if (XMinimum.Text != string.Empty)
            {
                //判断如果输入的不是数字
                MyObject.My_Chart1.ChartAreas[0].AxisY.Minimum = double.Parse(XMinimum.Text.ToString());
            }
            else
            {
                MessageBox.Show("X轴右刻度的输入值有误！");
            }
            #endregion
            #region 设置X轴的属性
            if (ChartStyle.Visible)
            {
                MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].XAxisType = (AxisType)AxisType.Parse(typeof(AxisType), this.AxisDictionary[XAxis.SelectedItem.ToString()]);
                MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].YAxisType = (AxisType)AxisType.Parse(typeof(AxisType), this.AxisDictionary[YAxis.SelectedItem.ToString()]);
            }
            if (PointStyle.Visible)
            {
                MyObject.My_Chart1.Series[this.seriesIndex2.SelectedIndex].XAxisType = (AxisType)AxisType.Parse(typeof(AxisType), this.AxisDictionary[XAxis.SelectedItem.ToString()]);
                MyObject.My_Chart1.Series[this.seriesIndex2.SelectedIndex].YAxisType = (AxisType)AxisType.Parse(typeof(AxisType), this.AxisDictionary[YAxis.SelectedItem.ToString()]);
            }
            #endregion

            #region 设置Y轴的属性

            #endregion

            #region 文本设置
            if (this.comboBox1.SelectedIndex >= 0)
            {//显示数值
                if (this.comboBox1.SelectedIndex == 1)
                {
                    MyObject.My_Chart1.Series[this.SelectSeries.SelectedIndex].IsValueShownAsLabel = false;

                }
                else
                {
                    MyObject.My_Chart1.Series[this.SelectSeries.SelectedIndex].IsValueShownAsLabel = true;
                    //    MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].LabelForeColor = label5.ForeColor;
                    //    MyObject.My_Chart1.Series[this.SeriseIndex.SelectedIndex].Font = label5.Font;
                }
            }
            if (this.comboBox3.SelectedIndex >= 0)
            {//显示图例项
                if (this.comboBox3.SelectedIndex == 1)
                {
                    MyObject.My_Chart1.Legends[0].Enabled = false;

                }
                else
                {
                    MyObject.My_Chart1.Legends[0].Enabled = true;

                }

            }
            if (this.comboBox5.SelectedIndex >= 0)
            {//图例项显示方式
                if (this.comboBox5.SelectedIndex >= 0)
                {//图例项显示方式
                    if (this.comboBox5.SelectedIndex == 0)
                    {
                        MyObject.My_Chart1.Legends[0].LegendStyle = LegendStyle.Table;
                        MyObject.My_Chart1.Legends[0].Docking = Docking.Top;
                    }
                    else if (this.comboBox5.SelectedIndex == 1)
                    {
                        MyObject.My_Chart1.Legends[0].LegendStyle = LegendStyle.Table;
                        MyObject.My_Chart1.Legends[0].Docking = Docking.Bottom;
                    }
                    else if (this.comboBox5.SelectedIndex == 2)
                    {
                        MyObject.My_Chart1.Legends[0].LegendStyle = LegendStyle.Table;
                        MyObject.My_Chart1.Legends[0].Docking = Docking.Left;
                    }
                    else
                    {
                        MyObject.My_Chart1.Legends[0].LegendStyle = LegendStyle.Table;
                        MyObject.My_Chart1.Legends[0].Docking = Docking.Right;
                    }
                }  
            }
            //设置文本
            switch (this.comboBox2.Text)
            {
                case "主标题":
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



                case "数值":
                    MyObject.My_Chart1.Series[this.SelectSeries.SelectedIndex].LabelForeColor = label5.ForeColor;
                    MyObject.My_Chart1.Series[this.SelectSeries.SelectedIndex].Font = label5.Font;
                    break;
                case "图例项":
                    MyObject.My_Chart1.Legends[0].ForeColor = label5.ForeColor;
                    MyObject.My_Chart1.Legends[0].Font = label5.Font;
                    break;
                case "图例项背景":
                    MyObject.My_Chart1.Legends[0].BackColor = label5.ForeColor;
                    break;
            }
            #endregion
            #endregion
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {

            this.Close();
        }
        #region 恢复默认值
        private void btnDefaultSet2_Click(object sender, EventArgs e)
        {
            #region 设置格线和文本属性
            this.grid.SelectedIndex = 0; //初始化默认选择有网格
            this.tick.SelectedIndex = 0;//初始化默认选择刻度线宽度为1
            this.XAxis.SelectedIndex = 0;
            this.YAxis.SelectedIndex = 0;
            this.colorPickerButton3.SelectedColor = Color.Black;
            YMaximum.Text = "600";
            YMinimum.Text = "0";
            Yfenqushu.Text = "5";
            title.Text = "双击图形在属性中修改主标题";
            title.Font = DefaultFont;
            title.ForeColor = DefaultForeColor;
            #endregion
        }
        #endregion
        private void buttonX1_Click(object sender, EventArgs e)
        {
            ColorDialog diag = new ColorDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                this.title.ForeColor = diag.Color;
            }
        }

        public bool isChangeChartStyle()
        {
            if (BorderDashStyleCom.SelectedIndex != -1 || BorderSizeCom.SelectedIndex != -1 || colorPickerButton1.SelectedColor != Color.Empty || colorPickerButton2.SelectedColor != Color.Empty || GradientCom.SelectedIndex != -1 || HatchingCom.SelectedIndex != -1 || ShadowOffset.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        private void SetProperty_Load(object sender, EventArgs e)
        {
            if (ChartStyle .Visible )
            {
                for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                {
                    this.SeriseIndex.Items.Add("序列" + (i + 1));
                }
                this.SeriseIndex.SelectedIndex = 0;
            }
            if (PointStyle .Visible )
	        {
                for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                {
                    this.seriesIndex2.Items.Add("序列" + (i + 1));

                }
                this.seriesIndex2.SelectedIndex = 0;
	        }
               
           
            
            for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
            {
                this.SelectSeries.Items.Add("序列" + (i + 1));
            }
            this.SelectSeries.SelectedIndex = 0;


            //如果是3d图形那么颜色分布和阴影和阴影偏移
            if (MyObject.My_Chart1.ChartAreas[0].Area3DStyle.Enable3D == false)
            {
                this.HatchingCom.Enabled = true;
                this.ShadowOffset.Enabled = true;
                this.GradientCom.Enabled = true;
            }
            else
            {
                this.HatchingCom.Enabled = false;
                this.ShadowOffset.Enabled = false;
                this.GradientCom.Enabled = false;
            }
            if (MyObject .My_Chart1 .Series [0].MarkerStyle!=MarkerStyle .None)
            {
                this.colorPickerButton7.Enabled = true;
                this.colorPickerButton8.Enabled = true;
                this.Bodersize.Enabled = true;
                this.numericUpDown1 .Enabled = true;
                this.Pointshape.Enabled = true;
            }
            else
            {
                this.colorPickerButton7.Enabled = false ;
                this.colorPickerButton8.Enabled = false ;
                this.Bodersize.Enabled = false ;
                this.numericUpDown1 .Enabled = false ;
                this.Pointshape.Enabled = false ;
            }


            //XY轴类型
            this.AxisDictionary.Add("主轴", "Primary");
            this.AxisDictionary.Add("副轴", "Secondary");
            //颜色分布
            this.ColorDistributeDictionary.Add("无", "None");
            this.ColorDistributeDictionary.Add("左右", "LeftRight");
            this.ColorDistributeDictionary.Add("顶底", "TopBottom");
            this.ColorDistributeDictionary.Add("居中", "Center");
            this.ColorDistributeDictionary.Add("水平居中", "HorizontalCenter");
            this.ColorDistributeDictionary.Add("垂直居中", "VerticalCenter");
            //阴影
            this.HatchDictionary.Add("不填充", "None");
            this.HatchDictionary.Add("均匀填充", "Cross");
            this.HatchDictionary.Add("水平填充", "Horizontal");
            this.HatchDictionary.Add("垂直填充", "Vertical");
            this.HatchDictionary.Add("从左上到右下", "ForwardDiagonal");
            this.HatchDictionary.Add("从左下到右上", "BackwardDiagonal");
            this.HatchDictionary.Add("渐变填充", "OutlinedDiamond");
            this.HatchDictionary.Add("岩性填充", "Weave");

            this.LineStyleDictionary.Add("不设置", "NotSet");
            this.LineStyleDictionary.Add("虚线", "Dash");
            this.LineStyleDictionary.Add("虚线点", "DashDot");
            this.LineStyleDictionary.Add("虚线点点", "DashDotDot");
            this.LineStyleDictionary.Add("点", "Dot");
            this.LineStyleDictionary.Add("实线", "Solid");
             //数据点形状
            this.MarkerStyleDictionary.Add("圆形","Circle");
            this.MarkerStyleDictionary.Add("正方形", "Square");
            this.MarkerStyleDictionary.Add("菱形", "Diamond");
            this.MarkerStyleDictionary.Add("三角形", "Triangle");
            this.MarkerStyleDictionary.Add("十字形", "Cross");
            this.MarkerStyleDictionary.Add("十角星", "Star10");
            this.MarkerStyleDictionary.Add("四角星", "Star4");
            this.MarkerStyleDictionary.Add("五角星", "Star5");
            this.MarkerStyleDictionary.Add("六角星", "Star6");
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click_1(object sender, EventArgs e)
        {
            //修改颜色 
            ColorDialog diag = new ColorDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (this.comboBox2.SelectedIndex == 0 || this.comboBox2.SelectedIndex == 2 || this.comboBox2.SelectedIndex == 3)
                {
                    this.title.ForeColor = diag.Color;
                }
                else if (this.comboBox2.SelectedIndex == 1 || this.comboBox2.SelectedIndex == 4 || this.comboBox2.SelectedIndex == 5)
                {
                    this.label5.ForeColor = diag.Color;
                }

            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            //文本风格的确定按钮
            OK();
            this.Close();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            //回复文本原来的字体和颜色
            this.title.Font = DefaultFont;
            this.title.ForeColor = DefaultForeColor;
        }

        private void btnChangeFont_Click_1(object sender, EventArgs e)
        {
            //修改字体
            FontDialog diag = new FontDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (this.comboBox2.SelectedIndex == 0 || this.comboBox2.SelectedIndex == 2 || this.comboBox2.SelectedIndex == 3)
                {
                    this.title.Font = diag.Font;
                }
                else if (this.comboBox2.SelectedIndex == 1 || this.comboBox2.SelectedIndex == 4)
                {
                    this.label5.Font = diag.Font;
                }
            }
        }

        private void buttonX6_Click_1(object sender, EventArgs e)
        {
            OK();
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.title.Text = "主标题";
            if (this.comboBox2.Text.Equals("数值") || this.comboBox2.Text.Equals("图例项") || this.comboBox2.Text.Equals("图例项背景"))
            {
                this.ChangeTitle.Enabled = false;
                if (this.comboBox2 .Text .Equals ("数值"))
                {
                    this.label5.Font = MyObject.My_Chart1.Series[this.SelectSeries.SelectedIndex].Font;
                    this.label5.ForeColor = MyObject.My_Chart1.Series[this.SelectSeries.SelectedIndex].LabelForeColor;
                    this.colorPickerButton4.SelectedColor = MyObject.My_Chart1.Series[this.SelectSeries.SelectedIndex].LabelForeColor;
                }
                if (this.comboBox2 .Text .Equals ("图例项"))
                {
                    this.label5.Font = MyObject.My_Chart1.Legends[0].Font;
                    this.label5.ForeColor = MyObject.My_Chart1.Legends[0].ForeColor;
                    this.colorPickerButton4.SelectedColor = MyObject.My_Chart1.Legends [0].ForeColor;
                }
                if (this.comboBox2 .Text .Equals ("图例项背景"))
                {
                    this.label5.ForeColor = MyObject.My_Chart1.Legends[0].BackColor;
                    this.colorPickerButton4.SelectedColor = MyObject.My_Chart1.Legends [0].BackColor;
                }
            }
            else
            {
                this.ChangeTitle.Enabled = true;
                this.ChangeTitle.TitleText = "修改" + this.comboBox2.Text;
                if (this.comboBox2.Text.Equals("主标题"))
                {
                    this.title.Text = MyObject .My_Chart1 .Titles [0].Text;
                    this.title .Font =MyObject .My_Chart1 .Titles [0].Font ;
                    this.colorPickerButton4.SelectedColor = MyObject.My_Chart1.Titles[0].ForeColor;
                }
                else if (this.comboBox2.Text.Equals("X轴标题"))
                {
                    this.title.Text = MyObject .My_Chart1 .ChartAreas [0].AxisX .Title;
                    this.title.Font = MyObject.My_Chart1.ChartAreas[0].AxisX.TitleFont;
                    this.colorPickerButton4.SelectedColor = MyObject.My_Chart1.ChartAreas[0].AxisX.TitleForeColor;
                }
                else if (this.comboBox2.Text.Equals("Y轴标题"))
                {
                    this.title.Text = MyObject.My_Chart1.ChartAreas[0].AxisY.Title;
                    this.title.Font = MyObject.My_Chart1.ChartAreas[0].AxisY.TitleFont;
                    this.colorPickerButton4.SelectedColor = MyObject.My_Chart1.ChartAreas[0].AxisY.TitleForeColor;
                }

            }
        }


        private void BtnAddSeries_Click(object sender, EventArgs e)
        {

            AddorDelSerise ads = new AddorDelSerise();
            ads.ShowDialog();

            //this.Close();

        }

        private void comboBox4_MouseDown(object sender, MouseEventArgs e)
        {
            //修改字体
            FontDialog diag = new FontDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (this.comboBox2.SelectedIndex == 0 || this.comboBox2.SelectedIndex == 2 || this.comboBox2.SelectedIndex == 3)
                {
                    this.title.Font = diag.Font;
                }
                else if (this.comboBox2.SelectedIndex == 1 || this.comboBox2.SelectedIndex == 4)
                {
                    this.label5.Font = diag.Font;
                }
            }
        }

        private void comboBox7_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void colorPickerButton2_SelectedColorChanged(object sender, EventArgs e)
        {

        }

        private void colorPickerButton4_SelectedColorChanged(object sender, EventArgs e)
        {
            if (this.comboBox2.SelectedIndex == 0 || this.comboBox2.SelectedIndex == 2 || this.comboBox2.SelectedIndex == 3)
            {
                this.title.ForeColor = colorPickerButton4.SelectedColor;
            }
            else if (this.comboBox2.SelectedIndex == 1 || this.comboBox2.SelectedIndex == 4 || this.comboBox2.SelectedIndex == 5)
            {
                this.label5.ForeColor = colorPickerButton4.SelectedColor;
            }
        }

        private void SeriseIndex_SelectedIndexChanged(object sender, EventArgs e)//当选择的序列发生改变时，相应列的属性值也要跟着改变
        {
                    this.colorPickerButton2.SelectedColor = MyObject.My_Chart1.Series[SeriseIndex.SelectedIndex].Points[0].Color;//柱体颜色
                    switch (Convert.ToString(MyObject.My_Chart1.Series[SeriseIndex.SelectedIndex].BackHatchStyle))
                    {
                        case "None": HatchingCom.SelectedIndex = 0; break;
                        case "Cross": HatchingCom.SelectedIndex = 1; break;
                        case "Horizontal": HatchingCom.SelectedIndex = 2; break;
                        case "Vertical": HatchingCom.SelectedIndex = 3; break;
                        case "ForwardDiagonal": HatchingCom.SelectedIndex = 4; break;
                        case "BackwardDiagonal": HatchingCom.SelectedIndex = 5; break;
                        case "OutlinedDiamond": HatchingCom.SelectedIndex = 6; break;
                        case "Weave": HatchingCom.SelectedIndex = 7; break;
                    }
                    this.ShadowOffset.SelectedIndex = MyObject.My_Chart1.Series[SeriseIndex.SelectedIndex].ShadowOffset;//阴影偏移量
                    this.GradientCom.SelectedIndex = Convert.ToInt16(MyObject.My_Chart1.Series[SeriseIndex.SelectedIndex].BackGradientStyle);
                    this.colorPickerButton1.SelectedColor = MyObject.My_Chart1.Series[SeriseIndex.SelectedIndex].BorderColor;//柱体边框颜色
                    this.BorderSizeCom.SelectedIndex = MyObject.My_Chart1.Series[SeriseIndex.SelectedIndex].BorderWidth - 1;//设置柱体边框宽度                                                                                    

                    this.BorderDashStyleCom.SelectedIndex = Convert.ToInt16(MyObject.My_Chart1.Series[SeriseIndex.SelectedIndex].BorderDashStyle);//柱体边框样式
        }

        private void SelectSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox2 .Text .Equals ("数值"))
            {
                this.label5.Font = MyObject.My_Chart1.Series[this.SelectSeries.SelectedIndex].Font;
                this.label5.ForeColor = MyObject.My_Chart1.Series[this.SelectSeries.SelectedIndex].LabelForeColor;
                this.colorPickerButton4.SelectedColor = MyObject.My_Chart1.Series[this.SelectSeries.SelectedIndex].LabelForeColor;
            }
            if (MyObject.My_Chart1.Series[this.SelectSeries.SelectedIndex].IsValueShownAsLabel)
            {
                this.comboBox1.SelectedIndex = 0;
            }
            else
            {
                this.comboBox1.SelectedIndex = 1;
            }
        }

        private void buttonX1_Click_2(object sender, EventArgs e)
        {
            OK();
        }

        private void buttonX11_Click(object sender, EventArgs e)
        {
            OK();
            this.Close();
        }

        private void seriesIndex2_SelectedIndexChanged(object sender, EventArgs e)//带标记的折线图，散点图专用
        {
            this.colorPickerButton5.SelectedColor = MyObject.My_Chart1.Series[seriesIndex2.SelectedIndex].Points[0].Color;//折线颜色

            this.shadowOffSet2.SelectedIndex = MyObject.My_Chart1.Series[seriesIndex2.SelectedIndex].ShadowOffset;//阴影偏移量

            this.colorPickerButton1.SelectedColor = MyObject.My_Chart1.Series[seriesIndex2.SelectedIndex].BorderColor;
            this.Linesize .SelectedIndex = MyObject.My_Chart1.Series[0].BorderWidth - 1;//设置折线宽度                                                                                    

            this.linestyle .SelectedIndex = Convert.ToInt16(MyObject.My_Chart1.Series[0].BorderDashStyle);//折线样式
            this.colorPickerButton7.SelectedColor = MyObject.My_Chart1.Series[seriesIndex2 . SelectedIndex].MarkerBorderColor ;//数据点边框颜色
            //数据点形状
            switch (Convert .ToString(MyObject .My_Chart1 .Series [seriesIndex2 .SelectedIndex ].MarkerStyle) )
	        {
                case "Circle": Pointshape.SelectedIndex = 0; break;
                case "Square": Pointshape.SelectedIndex = 1; break;
                case "Diamond": Pointshape.SelectedIndex = 2; break;
                case "Triangle": Pointshape.SelectedIndex = 3; break;
                case "Cross": Pointshape.SelectedIndex = 4; break;
                case "Star10": Pointshape.SelectedIndex = 5; break;
                case "Star4": Pointshape.SelectedIndex = 6; break;
                case "Star5":Pointshape .SelectedIndex =7;break ;
                case "Star6":Pointshape .SelectedIndex =8;break ;
		        default:break;
	        }
            this.numericUpDown1 .Value = MyObject.My_Chart1.Series[seriesIndex2.SelectedIndex].MarkerSize;
            this.Bodersize.SelectedIndex = MyObject.My_Chart1.Series[seriesIndex2.SelectedIndex].MarkerBorderWidth - 1;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BorderSizeCom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
