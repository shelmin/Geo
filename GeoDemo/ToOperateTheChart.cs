/*
 * 作者：肖宇博
 * 时间：2014/6/24
 * 功能：将对图的一些操作封装起来，便于主界面调用
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Windows.Forms;

namespace GeoDemo
{
    class ToOperateTheChart
    {
        static int k;
        //写一个静态方法，对表的边框进行处理
        public static void ChangeTheChartBoder(Chart[] chart,int NUM,BorderSkinStyle style)
        {
            for (int i = 0; i < NUM; i++)
            {
                //chart[i].BorderlineColor = color;
                chart[i].BorderSkin.SkinStyle = style;
            }
        }
        public static void ChangeTheChartTitleBoder(Chart[] chart, int NUM, Color color, ChartDashStyle style)
        {
            for (int i = 0; i < NUM; i++)
            {
                chart[i].Titles[0].BorderColor = color;
                chart[i].Titles[0].BorderDashStyle = style;
            }
        }
        public static void ChangeLengendItemBoder(Chart[] chart, int NUM, Color color, ChartDashStyle style)
        {
            for (int i = 0; i < NUM; i++)
            {
                chart[i].Legends[0].ShadowColor = color;
                //chart[i].Legends[0].BorderDashStyle = style;
                chart[i].Legends[0].ShadowOffset = 0;
            }
        }
        public static void ChangePlottingAreaBoder(Chart[] chart, int NUM, Color color)
        {
            for (int i = 0; i < NUM; i++)
            {
                for (int j = 0; j < chart[i].ChartAreas.Count; j++)
                {
                    chart[i].ChartAreas[j].ShadowColor = color;
                    chart[i].ChartAreas[j].ShadowOffset = 0;
                    //chart[i].ChartAreas[j].BorderWidth = 1;
                }
            }
        }
        public static void ChangeTheChartBoder(Chart chart,Color color, BorderSkinStyle style)
        {
            //图表区域边框
            chart.BorderSkin.BackColor = color;
            chart.BorderSkin.SkinStyle = style;
        }
        public static void ChangeTheChartTitleBoder(Chart chart, Color color, ChartDashStyle style)
        {
            //图题区边框
            chart.Titles[0].BorderColor = color;
            chart.Titles[0].BorderDashStyle = style;
        }
        public static void ChangeLegendItemBoder(Chart chart, Color color)
        { 
            //图例区域边框
            chart.Legends[0].ShadowColor = color;
            chart.Legends[0].ShadowOffset = 5;
            chart.Legends[0].BorderDashStyle = ChartDashStyle.Solid;
            chart.Legends[0].BorderWidth = 1;
            chart.Legends[0].BorderColor = Color.White;
        }
        public static void ChangePlottingAreaBoder(Chart chart, Color color)
        { 
            //绘图区边框
            for (int i = 0; i < chart.ChartAreas.Count; i++)
            {
                chart.ChartAreas[i].BorderDashStyle = ChartDashStyle.Solid;
                chart.ChartAreas[i].BorderWidth = 1;
                chart.ChartAreas[i].BorderColor = Color.White;
                chart.ChartAreas[i].ShadowColor = color;
                chart.ChartAreas[i].ShadowOffset = 10;
            }
        }
        

        //写一个静态方法用于实现点击到图的DataPoints时
        public static void ClickDataPoint(Chart chart,HitTestResult result,ref int r)
        {
            #region 单击DataPoint时，改变边框。
            if (result.ChartElementType == ChartElementType.DataPoint)//如果单击的是图的某一块
            {

                r = result.PointIndex;//代表鼠标点击的是哪一块，用变量r存储

                //这里加一段当点击chart的datapoints时候，让联动的窗体的哪一个格子高亮显
                //这里注释掉，解决了边框颜色设置后一点就消失的bug
                //for (int i = 0; i < chart.Series[0].Points.Count; i++)
                //{
                //    chart.Series[0].Points[i].BorderColor = Color.Empty;//先循环一圈让所有的边框都变白

                //}
            #endregion
                #region  晁    修改
                //将其注释掉
                /*
                foreach (DataPoint point in chart.Series[0].Points)
                {
                    point["Exploded"] = "false";  //每次单击的时候都让饼图初始合在一起
                }
                 */
                #endregion  晁  修改
                #region  晁修改(不需要)
                //k = 0;
                //for (int i = 0; i < chart.Series[0].Points.Count; i++)
                //{
                //    if (chart.Series[0].Points[i]["Exploded"] == "true")
                //    {
                //        k++;
                //    }
                //}
                #endregion  晁修改
                for (int i = 0; i < chart.Series[0].Points.Count; i++)
                {

                    #region  原版
                    
                    if (r == i)//如果循环到的DataPoints是我点击的那个
                    {
                        //chart.Series[0].Points[i].BorderColor = Color.Black;
                        if (chart.Series[0].Points[i]["Exploded"] == "true")
                        {
                            chart.Series[0].Points[i]["Exploded"] = "flase";
                        }
                        else
                        {
                            chart.Series[0].Points[i]["Exploded"] = "true";
                        }
                    }
                    
                    #endregion  原版
                    #region     晁  修改(不需要)
                    //if (k == 0)
                    //{
                    //    if (r == i)//如果循环到的DataPoints是我点击的那个
                    //    {
                    //        if (chart.Series[0].Points[i]["Exploded"] == "true")
                    //        {
                    //            chart.Series[0].Points[i]["Exploded"] = "flase";
                    //        }
                    //        else
                    //        {
                    //            chart.Series[0].Points[i]["Exploded"] = "true";
                    //        }
                    //        break;
                    //    }
                    //}
                    //else if (k == 1 && ((Control.ModifierKeys & Keys.Control) == Keys.Control))
                    //{
                    //    if (r == i)//如果循环到的DataPoints是我点击的那个
                    //    {
                    //        if (chart.Series[0].Points[i]["Exploded"] == "true")
                    //        {
                    //            chart.Series[0].Points[i]["Exploded"] = "flase";
                    //        }
                    //        else
                    //        {
                    //            chart.Series[0].Points[i]["Exploded"] = "true";
                    //        }
                    //        break;
                    //    }
                    //}
                    //else if (k == 1)
                    //{
                    //    if (r == i)//如果循环到的DataPoints是我点击的那个
                    //    {
                    //        if (chart.Series[0].Points[i]["Exploded"] == "true")
                    //        {
                    //            chart.Series[0].Points[i]["Exploded"] = "flase";
                    //        }
                    //        // else
                    //        // {
                    //        //      chart.Series[0].Points[i]["Exploded"] = "true";
                    //        //   }
                    //        break;
                    //    }
                    //}
                    //else if (k > 1 && k <= chart.Series[0].Points.Count && ((Control.ModifierKeys & Keys.Control) == Keys.Control))
                    //{
                    //    if (r == i)//如果循环到的DataPoints是我点击的那个
                    //    {
                    //        if (chart.Series[0].Points[i]["Exploded"] == "true")
                    //        {
                    //            chart.Series[0].Points[i]["Exploded"] = "flase";
                    //        }
                    //        else
                    //        {
                    //            chart.Series[0].Points[i]["Exploded"] = "true";
                    //        }
                    //        break;
                    //    }
                    //}
                    //else
                    //{

                    //}
                    #endregion  晁  修改

                }
            }
        }
        public static void ClickDataPoint2(Chart chart, HitTestResult result, ref int r)
        {
            #region 单击DataPoint时，弹出颜色对话框选择颜色
            if (result.ChartElementType == ChartElementType.DataPoint)//如果单击的是图的某一块
            {
                r = result.PointIndex;//代表鼠标点击的是哪一块，用变量r存储
                //这里注释掉，解决了边框颜色设置后一点就消失的bug
                //for (int i = 0; i < chart.Series[0].Points.Count; i++)
                //{
                //    //先循环让所有的边框都变白
                //    chart.Series[0].Points[i].BorderColor = Color.Empty;
                //}
                //for (int i = 0; i < chart.Series[1].Points.Count; i++)
                //{
                //    //先循环让所有的边框都变白
                //    chart.Series[1].Points[i].BorderColor = Color.Empty;
                //}
                //for (int i = 0; i < chart.Series.Count; i++)
                //{
                //    for (int j = 0; j < chart.Series[0].Points.Count; j++)
                //    {
                //        chart.Series[i].Points[j].BorderColor = Color.Empty;
                //    }
                //}
                //for (int i = 0; i < chart.Series[0].Points.Count; i++)
                //{

                //    if (r == i)//如果循环到的DataPoints是我点击的那个
                //    {
                //        chart.Series[result.Series.Name].Points[i].BorderColor = Color.Black;
                //        //if (result.Series.Name.Equals("Series1"))
                //        //{
                //        //    chart.Series[0].Points[i].BorderColor = Color.Black;
                //        //}
                //        //else if (result.Series.Name.Equals("Series2"))
                //        //{
                //        //    chart.Series[1].Points[i].BorderColor = Color.Black;
                //        //}

                      
                //    }
                //    else//如果还没有循环到我点击的那个
                //    {
                //        chart.Series[result.Series.Name].Points[i].BorderColor = Color.Empty;
                //        //if (result.Series.Name.Equals("Series1"))
                //        //{
                //        //    chart.Series[0].Points[i].BorderColor = Color.Empty;
                //        //}
                //        //else if (result.Series.Name.Equals("Series2"))
                //        //{
                //        //    chart.Series[1].Points[i].BorderColor = Color.Empty;
                //        //}
                //    }
                //}

            }

            #endregion
        }

        public static void ClickDataPoint3(Chart chart, HitTestResult result, ref int r)
        {
            #region 单击DataPoint时，弹出颜色对话框选择颜色
            if (result.ChartElementType == ChartElementType.DataPoint)//如果单击的是图的某一块
            {
                r = result.PointIndex;//代表鼠标点击的是哪一块，用变量r存储
                //这里注释掉，解决了边框颜色设置后一点就消失的bug
                //for (int i = 0; i < chart.Series[0].Points.Count; i++)
                //{
                //    //先循环让所有的边框都变白
                //    chart.Series[0].Points[i].BorderColor = Color.Empty;
                //}
                //for (int i = 0; i < chart.Series[1].Points.Count; i++)
                //{
                //    //先循环让所有的边框都变白
                //    chart.Series[1].Points[i].BorderColor = Color.Empty;
                //}
                //for (int i = 0; i < chart.Series[2].Points.Count; i++)
                //{
                //    //先循环让所有的边框都变白
                //    chart.Series[2].Points[i].BorderColor = Color.Empty;
                //}



                //for (int i = 0; i < chart.Series[0].Points.Count; i++)
                //{

                //    if (r == i)//如果循环到的DataPoints是我点击的那个
                //    {
                //        if (result.Series.Name.Equals("Series1"))
                //        {
                //            chart.Series[0].Points[i].BorderColor = Color.Black;
                //        }
                //        else if (result.Series.Name.Equals("Series2"))
                //        {
                //            chart.Series[1].Points[i].BorderColor = Color.Black;
                //        }
                //        else if (result.Series.Name.Equals("Series3"))
                //        {
                //            chart.Series[2].Points[i].BorderColor = Color.Black;
                //        }

                //    }
                //    else//如果还没有循环到我点击的那个
                //    {
                //        if (result.Series.Name.Equals("Series1"))
                //        {
                //            chart.Series[0].Points[i].BorderColor = Color.Empty;
                //        }
                //        else if (result.Series.Name.Equals("Series2"))
                //        {
                //            chart.Series[1].Points[i].BorderColor = Color.Empty;
                //        }
                //        else if (result.Series.Name.Equals("Series3"))
                //        {
                //            chart.Series[2].Points[i].BorderColor = Color.Empty;
                //        }
                //    }
                //}

            }

            #endregion
        }

        public static void ClickDataPoint4(Chart chart, HitTestResult result, ref int r)
        {
            #region 单击DataPoint时，改变边框颜色显示选中状态
            if (result.ChartElementType == ChartElementType.DataPoint)//如果单击的是图的某一块
            {
                r = result.PointIndex;//代表鼠标点击的是哪一块，用变量r存储
                //这里注释掉，解决了边框颜色设置后一点就消失的bug
                //for (int a = 0; a < chart.Series.Count; a++)
                //{
                //   for (int i = 0; i < chart.Series[0].Points.Count; i++)
                //    {
                //      //先循环让所有的边框都变白
                //      chart.Series[a].Points[i].BorderColor = Color.Empty;
                //    }
                //}
              
                //    for (int i = 0; i < chart.Series[0].Points.Count; i++)
                //    {

                //        if (r == i)//如果循环到的DataPoints是我点击的那个
                //        {
                //            for (int a = 0; a < chart.Series.Count; a++)
                //            {
                //                if (result.Series.Name.Equals("Series" + (a + 1)))//如果点击的是相应的序列相应的数据点
                //                {
                //                    chart.Series[a].Points[i].BorderColor = Color.Black;
                //                }
                //            }
                //        }
                //        else//如果还没有循环到我点击的那个
                //        {
                //            if (result.Series.Name.Equals("Series1"))
                //            {
                //                chart.Series[0].Points[i].BorderColor = Color.Empty;
                //            }
                //            else if (result.Series.Name.Equals("Series2"))
                //            {
                //                chart.Series[1].Points[i].BorderColor = Color.Empty;
                //            }
                //            else if (result.Series.Name.Equals("Series3"))
                //            {
                //                chart.Series[2].Points[i].BorderColor = Color.Empty;
                //            }
                //        }
                //    }
                

            }

            #endregion
        }
        public static void ClickDataPointUsedAll(Chart chart, HitTestResult result, ref int r)
        {
            #region 单击DataPoint时，改变边框颜色显示选中状态
            if (result.ChartElementType == ChartElementType.DataPoint)//如果单击的是图的某一块
            {
                r = result.PointIndex;//代表鼠标点击的是哪一块，用变量r存储
                //这里注释掉，解决了边框颜色设置后一点就消失的bug
                //for (int a = 0; a < chart.Series.Count; a++)
                //{
                //    for (int i = 0; i < chart.Series[0].Points.Count; i++)
                //    {
                //        //先循环让所有的边框都变白
                //        chart.Series[a].Points[i].BorderColor = Color.Empty;
                //    }
                //}

                //for (int i = 0; i < chart.Series[0].Points.Count; i++)
                //{

                //    if (r == i)//如果循环到的DataPoints是我点击的那个
                //    {
                       
                //     chart.Series[result.Series.Name].Points[i].BorderColor = Color.Black;
                            
                //    }
                //    else//如果还没有循环到我点击的那个
                //    {
                //        chart.Series[result.Series.Name].Points[i].BorderColor = Color.Empty;
                //        //if (result.Series.Name.Equals("Series1"))
                //        //{
                //        //    chart.Series[0].Points[i].BorderColor = Color.Empty;
                //        //}
                //        //else if (result.Series.Name.Equals("Series2"))
                //        //{
                //        //    chart.Series[1].Points[i].BorderColor = Color.Empty;
                //        //}
                //        //else if (result.Series.Name.Equals("Series3"))
                //        //{
                //        //    chart.Series[2].Points[i].BorderColor = Color.Empty;
                //        //}
                //    }
                //}


            }

            #endregion
        }
     
        public static void ChangeDataPointsBoderWhite(Chart [] chart,int NUM)
        {
            
            try
            {
                for (int i = 0; i < NUM; i++)
                {
                    for (int j = 0; j < chart[i].Series.Count; j++)
                    {
                        //for (int k = 0; k < chart[i].Series [j].Points .Count ; k++)
                        //{
                        //    chart[i].Series[j].Points[k].BorderColor = Color.Empty;
                        //}
                        //把chart1[i]的datapoints的边框去掉
                        chart[i].Series[j].ShadowOffset = 0;
                       
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void DoubleClickOnTheChart(Chart chart,HitTestResult result,ref int r)//专为一个序列的（部分普通图和递减曲线）写的双击事件
        {
           //当双击该图的时候
            if (result.ChartElementType == ChartElementType.Title)//如果双击的是图题，弹出字体对话框改变字体
            {
                //弹出字体对话框
                FontDialog diag = new FontDialog();
                if (diag.ShowDialog() == DialogResult.OK)
                {
                    chart.Titles[0].Font = diag.Font;
                }
            }
            else if (result.ChartElementType == ChartElementType.DataPoint)//如果双击的是数据点，改变数据点的颜色
            {
                    for (int j = 0; j < chart.Series[0].Points.Count; j++)
                    {

                        if (r == j)//如果循环到的DataPoints是我点击的那个
                        {

                            ColorDialog diag = new ColorDialog();
                            if (diag.ShowDialog() == DialogResult.OK)
                            {
                                    chart.Series[result.Series.Name].Points[j].Color = diag.Color;
                            }
                            break;
                        }
                    }
            }
            else if (result.ChartElementType == ChartElementType.AxisLabels)//如果单击的是图的坐标轴
            {
                if (result.ChartElementType == ChartElementType.AxisLabels && result.Axis == chart.ChartAreas[0].AxisY)//单击Y轴
                {
                    //改变区域0 的Y轴的颜色
                    ColorDialog diag = new ColorDialog();
                    if (diag.ShowDialog() == DialogResult.OK)
                    {
                        chart.ChartAreas[0].AxisY.TitleForeColor = diag.Color;
                        //chart.ChartAreas[0].AxisY.LineColor = diag.Color;
                        chart.ChartAreas[0].AxisY.LabelStyle.ForeColor = diag.Color;
                    }
                }
                else if (result.ChartElementType == ChartElementType.AxisLabels && result.Axis == chart.ChartAreas[0].AxisX)
                {
                    //改变区域0 的X轴的颜色
                    ColorDialog diag = new ColorDialog();
                    if (diag.ShowDialog() == DialogResult.OK)
                    {
                        chart.ChartAreas[0].AxisX.TitleForeColor = diag.Color;
                        //chart.ChartAreas[0].AxisX.LineColor = diag.Color;
                        chart.ChartAreas[0].AxisX.LabelStyle.ForeColor = diag.Color;
                    }
                }
            }//如果单击的是图的坐标轴。。。结束
            else if (result.ChartElementType == ChartElementType.LegendItem)//如果单击的是图例
            {
                AddorDelSerise ad = new AddorDelSerise();
                ad.ShowDialog();
            }
            else//如果单击不是图题不是坐标轴，而是其他空白的部分，就显示属性设置的对话框
            {
                if (chart.Series[0].ChartType == SeriesChartType.Pie || chart.Series[0].ChartType == SeriesChartType.Doughnut)
                {
                    SetProperty2 sp2 = new SetProperty2();//专为普通图中的圆环图和饼图
                    sp2.ShowDialog();
                }
                else if (chart.Name.Equals("递减曲线"))//专为递减曲线，因为有可能递减曲线最开始的时候只有一个序列
                {
                    SetDiJianQuXianProperity sd = new SetDiJianQuXianProperity();
                    sd.ShowDialog();
                }
                else if (chart.Name.Equals("相渗曲线"))
                {
                    SetXiangshenQuXianProperity sd = new SetXiangshenQuXianProperity();
                    sd.ShowDialog();
                }
                else
                {
                    SetProperty sp = new SetProperty();
                    sp.ShowDialog();//专为普通图中非圆环图和饼图
                }
            }
        }

        public static void DoubleClickOnTheChart4(Chart chart, HitTestResult result, ref int r)
        {


            if (result.ChartElementType == ChartElementType.Title)
            {
                //弹出字体对话框
                FontDialog diag = new FontDialog();
                if (diag.ShowDialog() == DialogResult.OK)
                {
                    chart.Titles[0].Font = diag.Font;

                }
            }
            else if (result.ChartElementType == ChartElementType.DataPoint)
            {

                
                for (int i = 0; i < chart.Series[0].Points.Count; i++)
                {

                    if (result.PointIndex == i)//如果循环到的DataPoints是我点击的那个
                    {
                        ColorDialog diag = new ColorDialog();
                        if (diag.ShowDialog() == DialogResult.OK)
                        {
                            for (int a = 0; a <chart.Series.Count; a++)
                            {
                                if (result.Series.Name.Equals("Series" + (a + 1)))//如果点击的是相应的序列相应的数据点
                                {
                                    chart.Series[a].Points[i].Color = diag.Color;
                                }
                            }
                           
                        }
                    }
                }
            }
            else if (result.ChartElementType == ChartElementType.AxisLabels)
            {
                if (result.ChartElementType == ChartElementType.AxisLabels && result.Axis == chart.ChartAreas[0].AxisY)
                {
                    //改变区域0 的Y轴的颜色
                    ColorDialog diag = new ColorDialog();
                    if (diag.ShowDialog() == DialogResult.OK)
                    {
                        chart.ChartAreas[0].AxisY.TitleForeColor = diag.Color;
                        chart.ChartAreas[0].AxisY.LineColor = diag.Color;
                        chart.ChartAreas[0].AxisY.LabelStyle.ForeColor = diag.Color;
                    }
                }
                else if (result.ChartElementType == ChartElementType.AxisLabels && result.Axis == chart.ChartAreas[1].AxisY)
                {
                    //改变区域0 的Y轴的颜色
                    ColorDialog diag = new ColorDialog();
                    if (diag.ShowDialog() == DialogResult.OK)
                    {
                        chart.ChartAreas[1].AxisY.TitleForeColor = diag.Color;
                        //chart.ChartAreas[1].AxisY.LineColor = diag.Color;
                        chart.ChartAreas[1].AxisY.LabelStyle.ForeColor = diag.Color;
                    }
                }
                else if (result.ChartElementType == ChartElementType.AxisLabels && result.Axis == chart.ChartAreas[2].AxisY)
                {
                    //改变区域0 的Y轴的颜色
                    ColorDialog diag = new ColorDialog();
                    if (diag.ShowDialog() == DialogResult.OK)
                    {
                        chart.ChartAreas[2].AxisY.TitleForeColor = diag.Color;
                        //chart.ChartAreas[2].AxisY.LineColor = diag.Color;
                        chart.ChartAreas[2].AxisY.LabelStyle.ForeColor = diag.Color;
                    }
                }
                else if (result.ChartElementType == ChartElementType.AxisLabels && result.Axis == chart.ChartAreas[3].AxisX)
                {
                    //改变区域0 的Y轴的颜色
                    ColorDialog diag = new ColorDialog();
                    if (diag.ShowDialog() == DialogResult.OK)
                    {
                        chart.ChartAreas[2].AxisX.TitleForeColor = diag.Color;
                        //chart.ChartAreas[2].AxisX.LineColor = diag.Color;
                        chart.ChartAreas[2].AxisX.LabelStyle.ForeColor = diag.Color;
                    }
                }
            }
            else if (result.ChartElementType == ChartElementType.LegendItem)//如果单击的是图例
            {
                AddorDelSerise ad = new AddorDelSerise();
                ad.ShowDialog();
            }
            else
            {
                if (chart.Series.Count!=0)
                {
                    if (chart.Series[0].ChartType == SeriesChartType.Pie || chart.Series[0].ChartType == SeriesChartType.Doughnut)
                    {
                        SetProperty2 sp2 = new SetProperty2();
                        sp2.ShowDialog();
                    }
                    else if (chart.Name.Equals("递减曲线"))
                    {
                        SetDiJianQuXianProperity sd = new SetDiJianQuXianProperity();
                        sd.ShowDialog();
                    }
                    else if (chart.Name.Equals("相渗曲线"))
                    {
                        SetXiangshenQuXianProperity sd = new SetXiangshenQuXianProperity();
                        sd.ShowDialog();
                    }
                    else if (chart.Name.Equals("生产开发曲线"))
                    {
                        SetShengChanKaiFaQuXianProperity sd = new SetShengChanKaiFaQuXianProperity();
                        sd.ShowDialog();
                    }
                    else
                    {
                        SetProperty sp = new SetProperty();
                        sp.ShowDialog();
                    }
                }
            }
        }//专为生产开发曲线而写的，因为
        //他的序列有能超过3个



    }
}
