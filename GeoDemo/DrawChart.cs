/* 
 * 作者;肖宇博
 * 日期：2014/6/19
 * 功能：这是画图的工具类，可以把所有要画的图都封装在这里
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace GeoDemo
{
    [Serializable ]
    class DrawChart 
    {

        private int initPointX;

        public int InitPointX
        {
            get { return initPointX; }
            set { initPointX = value; }
        }
        private int initPointY;

        public int InitPointY
        {
            get { return initPointY; }
            set { initPointY = value; }
        }
        private int initWidth;

        public int InitWidth
        {
            get { return initWidth; }
            set { initWidth = value; }
        }

        private int initHeight;

        public int InitHeight
        {
            get { return initHeight; }
            set { initHeight = value; }
        }

     

        public void InitChart(Chart[] chart,int NUM)
        {
            for (int i = 0; i < NUM; i++)
            {
                chart[i] = new Chart();
                chart[i].Tag = i;
            }
        }

        //public void  CreateAnnotions(Chart chart)
        //{
            //RectangleAnnotation[] annotation = new RectangleAnnotation[3];
            //for (int i = 0; i < annotation.Length; i++)
            //{
            //    //annotation[i].X = 20;
            //    //annotation[i].Y = 20;
            //    annotation[i].Width = 20;
            //    annotation[i].Height = 10;
            //    annotation[i].IsMultiline = true;
                
            //    annotation[i].AllowSelecting = true;
            //    annotation[i].AllowMoving = true;
            //    annotation[i].AllowResizing = true;
            //    annotation[i].AllowTextEditing = true;
            //    annotation[i].LineDashStyle = ChartDashStyle.NotSet;
            //    chart.Annotations.Add(annotation[i]);
            //}

            //标题
            //RectangleAnnotation title = new RectangleAnnotation();
            //title.X = chart.Titles[0].Position.X;
            //title.Y = chart.Titles[0].Position.Y;
            //title.Text = "点击此处编辑标题！";
            //title.Width = 20;
            //title.Height = 10;
            //title.IsMultiline = true;
            //title.AllowSelecting = true;
            //title.AllowMoving = true;
            //title.AllowResizing = true;
            //title.AllowTextEditing = true;
            //title.LineDashStyle = ChartDashStyle.NotSet;
            //chart.Annotations.Add(title);
          
        //}
        /////////////////////////////////////////下面开始普通图//////////////////////////////////////////////////
        #region 普通图
        public void Draw_Cuzhuangzhuxingtu(Chart[] chart1,int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            
            #region 动态创建一个表
            //先动态创建一个chart表 
            ChartArea charArea = new ChartArea();
            chart1[times].ChartAreas.Add(charArea);
            Series s1 = new Series();
            chart1[times].Series.Add(s1);
            chart1[times].Name = "chart1";
            s1.XValueType = ChartValueType.String;
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
         
            chart1[times].Legends.Add(legend1);
            
           
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart1[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart1[times].Width = initWidth;//默认的初始宽值
            chart1[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();

            title1.Text = "双击图形在属性中修改主标题  ";
            chart1[times].Titles.Add(title1);
           
            
            #endregion

            #region 将数据用数组保存时的情况，测试时用的
            //string[] x = new string[] { "2013-01", "2013-02", "2013-03", "2013-04", "2013-04", "2013-04", "2013-04", "2013-04" };//纵坐标的数据代表日期

            //int[] y = new int[] { 46, 44, 63, 64, 120, 44, 273, 123 };//横坐标的数据代表此时的油田2的各个时期收集到的数据
            
            #endregion 
          
                //如果没有绑定
            title1.Text = "请双击绑定数据";

            #region 绘簇状柱形图并给出初始属性
            //chart1[times].Legends.Clear();
            chart1[times].Series[0]["PointWidth"] = "0.6";
            chart1[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart1[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            chart1[times].ChartAreas[0].AxisX.Interval = 1;
            
            chart1[times].Series[0].ChartType = SeriesChartType.Column;//绘图类型为簇状柱形图
            chart1[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart1[times].BorderlineColor = Color.Black;
            chart1[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart1[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart1[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion

        }

        public void Draw_Duijizhuxingtu(Chart[] chart2, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart2[times].ChartAreas.Add(charArea);
            chart2[times].Name = "chart2";
            Series s1 = new Series();
            //Series s2 = new Series();
            chart2[times].Series.Add(s1);
            //chart2[times].Series.Add(s2);
            s1.XValueType = ChartValueType.String;
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart2[times].Legends.Add(legend1);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart2[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart2[times].Width = initWidth;//默认的初始宽值
            chart2[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();

            title1.Text = "双击图形在属性中修改图题";
            chart2[times].Titles.Add(title1);


            #endregion
            //下面这两个也可以封装成函数
            #region 将数据用数组保存时的情况，测试时用的
            //string[] x = new string[] { "20.01", "2.02", "13.03", "22.04", "13.04", "32.04", "21.04", "12.4" };//纵坐标的数据代表日期

            //string[] y = new string[] { "46.23", "44.321", "63.123", "64.54", "120.123", "44.32", "273.4", "123.2" };//横坐标的数据代表此时的油田2的各个时期收集到的数据
            //int[] yy = new int[] { 123, 63, 13, 164, 10, 224, 273, 140 };//横坐标的数据代表此时的油田2的各个时期收集到的数据
            #endregion 


            #region 从datatable中读取数据
          
           //如果没有绑定
             title1.Text = "请双击绑定数据";
          
            #endregion


            #region 绘簇状柱形图并给出初始属性
            //chart2[times].Legends.Clear();
            chart2[times].Series[0]["PointWidth"] = "0.6";
            //chart2[times].Series[1]["PointWidth"] = "0.6";
            chart2[times].ChartAreas[0].AxisX.Interval = 1;
            chart2[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            //chart2[times].Series[1].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart2[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            //chart2[times].Series[1]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
               //chart2[times].Series[1].Points.DataBindXY(x, y);//将油田1的横纵坐标绑定绘出柱形图
            chart2[times].Series[0].ChartType = SeriesChartType.StackedColumn;//绘图类型为堆积柱形图
            //chart2[times].Series[1].ChartType = SeriesChartType.StackedColumn;//绘图类型为堆积柱形图
            chart2[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart2[times].BorderlineColor = Color.Black;
            chart2[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart2[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart2[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_baifenbiduijizhuxing(Chart[] chart3, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart3[times].ChartAreas.Add(charArea);
            chart3[times].Name = "chart3";
            Series s1 = new Series();
            Series s2 = new Series();
            chart3[times].Series.Add(s1);
            chart3[times].Series.Add(s2);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart3[times].Legends.Add(legend1);
            s1.XValueType = ChartValueType.String;
            s2.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart3[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart3[times].Width = initWidth;//默认的初始宽值
            chart3[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();

            title1.Text = "双击图形在属性中修改图题";
            chart3[times].Titles.Add(title1);


            #endregion

              //如果没有绑定
              title1.Text = "请双击绑定数据";
            
            #region 绘簇状柱形图并给出初始属性
            //chart3[times].Legends.Clear();
            chart3[times].Series[0]["PointWidth"] = "0.6";
            chart3[times].Series[1]["PointWidth"] = "0.6";
            chart3[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart3[times].Series[1].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart3[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            chart3[times].Series[1]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            chart3[times].ChartAreas[0].AxisX.Interval = 1;
            chart3[times].Series[0].ChartType = SeriesChartType.StackedColumn100;//绘图类型为堆积柱形图
            chart3[times].Series[1].ChartType = SeriesChartType.StackedColumn100;//绘图类型为堆积柱形图
            chart3[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            chart3[times].Series[1].Color = Color.FromArgb(210, 100, 255, 100);//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart3[times].BorderlineColor = Color.Black;
            chart3[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart3[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart3[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_sanweicuzhuangzhuxing(Chart[] chart4, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart4[times].ChartAreas.Add(charArea);
            chart4[times].Name = "chart4";
            Series s1 = new Series();
            Series s2 = new Series();
            chart4[times].Series.Add(s1);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart4[times].Legends.Add(legend1);
            //chart4[times].Series.Add(s2);
            s1.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart4[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart4[times].Width = initWidth;//默认的初始宽值
            chart4[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();

            title1.Text = "双击图形在属性中修改图题";
            chart4[times].Titles.Add(title1);


            #endregion

            title1.Text = "请双击绑定数据";

            #region 绘三维簇状柱形图并给出初始属性
            //chart4[times].Legends.Clear();
            chart4[times].ChartAreas[0].Area3DStyle.Enable3D = true;//柱体的显示风格为3D
            chart4[times].Series[0]["PointWidth"] = "0.6";
            chart4[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart4[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            chart4[times].ChartAreas[0].AxisX.Interval = 1;
            chart4[times].Series[0].ChartType = SeriesChartType.Column;//绘图类型为堆积柱形图
            chart4[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart4[times].BorderlineColor = Color.Black;
            chart4[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart4[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart4[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_sanweiduijizhuxing(Chart[] chart5, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart5[times].ChartAreas.Add(charArea);
            chart5[times].Name = "chart5";
            Series s1 = new Series();
            Series s2 = new Series();
            chart5[times].Series.Add(s1);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart5[times].Legends.Add(legend1);
            //chart5[times].Series.Add(s2);
            s1.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart5[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart5[times].Width = initWidth;//默认的初始宽值
            chart5[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();

            title1.Text = "双击图形在属性中修改图题";
            chart5[times].Titles.Add(title1);


            #endregion

            title1.Text = "请双击绑定数据";

            #region 绘三维簇状柱形图并给出初始属性
            //chart5[times].Legends.Clear();
            chart5[times].ChartAreas[0].Area3DStyle.Enable3D = true;//柱体的显示风格为3D
            chart5[times].Series[0]["PointWidth"] = "0.6";
            chart5[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart5[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            chart5[times].ChartAreas[0].AxisX.Interval = 1;
            chart5[times].Series[0].ChartType = SeriesChartType.StackedColumn;//绘图类型为堆积柱形图
            chart5[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart5[times].BorderlineColor = Color.Black;
            chart5[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart5[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart5[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_sanweibaifenbiduijizhuxing(Chart[] chart6, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart6[times].ChartAreas.Add(charArea);
            chart6[times].Name = "chart6";
            Series s1 = new Series();
            Series s2 = new Series();
            chart6[times].Series.Add(s1);
            chart6[times].Series.Add(s2);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart6[times].Legends.Add(legend1);
            s1.XValueType = ChartValueType.String;
            s2.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart6[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart6[times].Width = initWidth;//默认的初始宽值
            chart6[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();

            title1.Text = "双击图形在属性中修改图题";
            chart6[times].Titles.Add(title1);


            #endregion

            title1.Text = "请双击绑定数据";

            #region 绘簇状柱形图并给出初始属性
            //chart6[times].Legends.Clear();
            chart6[times].Series[0]["PointWidth"] = "0.6";
            chart6[times].Series[1]["PointWidth"] = "0.6";
            chart6[times].ChartAreas[0].Area3DStyle.Enable3D = true;//柱体的显示风格为3D
            chart6[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart6[times].Series[1].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart6[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            chart6[times].Series[1]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            chart6[times].ChartAreas[0].AxisX.Interval = 1;
            chart6[times].Series[0].ChartType = SeriesChartType.StackedColumn100;//绘图类型为三维百分比堆积柱形图
            chart6[times].Series[1].ChartType = SeriesChartType.StackedColumn100;//绘图类型为三维百分比堆积柱形图
            chart6[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            chart6[times].Series[1].Color = Color.FromArgb(210, 100, 255, 100);//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart6[times].BorderlineColor = Color.Black;
            chart6[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart6[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart6[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_sanweizhuxing(Chart[] chart7, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart7[times].ChartAreas.Add(charArea);
            chart7[times].Name = "chart7";
            Series s1 = new Series();
            Series s2 = new Series();
            chart7[times].Series.Add(s1);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart7[times].Legends.Add(legend1);
            //chart7[times].Series.Add(s2);
            s1.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart7[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart7[times].Width = initWidth;//默认的初始宽值
            chart7[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();

            title1.Text = "双击图形在属性中修改图题";
            chart7[times].Titles.Add(title1);


            #endregion

            title1.Text = "请双击绑定数据";
       
            #region 绘簇状柱形图并给出初始属性
            //chart7[times].Legends.Clear();
            chart7[times].Series[0]["PointWidth"] = "0.6";
            chart7[times].ChartAreas[0].Area3DStyle.Enable3D = true;//柱体的显示风格为3D
            chart7[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart7[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            chart7[times].ChartAreas[0].AxisX.Interval = 1;
            chart7[times].Series[0].ChartType = SeriesChartType.StackedColumn;//绘图类型为三维百分比堆积柱形图
            chart7[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart7[times].BorderlineColor = Color.Black;
            chart7[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart7[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart7[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_cuzhuangyuanzhu(Chart[] chart8, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart8[times].ChartAreas.Add(charArea);
            chart8[times].Name = "chart8";
            Series s1 = new Series();
            Series s2 = new Series();
            chart8[times].Series.Add(s1);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart8[times].Legends.Add(legend1);
            //chart8[times].Series.Add(s2);
            s1.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart8[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart8[times].Width = initWidth;//默认的初始宽值
            chart8[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();

            title1.Text = "双击图形在属性中修改图题";
            chart8[times].Titles.Add(title1);


            #endregion

            title1.Text = "请双击绑定数据";
       
            #region 绘簇状柱形图并给出初始属性
            //chart8[times].Legends.Clear();
            chart8[times].Series[0]["PointWidth"] = "0.6";
            chart8[times].Series[0]["DrawingStyle"] = "Cylinder";//绘图风格默认的是方形，此时可以改变成圆柱型
            //chart8[times].ChartAreas[0].Area3DStyle.Enable3D = true;//柱体的显示风格为3D
            chart8[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            //chart8[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            chart8[times].ChartAreas[0].AxisX.Interval = 1;
            chart8[times].Series[0].ChartType = SeriesChartType.Column;//绘图类型为三维百分比堆积柱形图
            chart8[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart8[times].BorderlineColor = Color.Black;
            chart8[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart8[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart8[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_duijiyuanzhu(Chart[] chart9, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart9[times].ChartAreas.Add(charArea);
            chart9[times].Name = "chart9";
            Series s1 = new Series();
            Series s2 = new Series();
            chart9[times].Series.Add(s1);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart9[times].Legends.Add(legend1);
            //chart9[times].Series.Add(s2);
            s1.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart9[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart9[times].Width = initWidth;//默认的初始宽值
            chart9[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();

            title1.Text = "双击图形在属性中修改图题";
            chart9[times].Titles.Add(title1);

            title1.Text = "请双击绑定数据";
            #endregion

            #region 绘簇状柱形图并给出初始属性
            //chart9[times].Legends.Clear();
            chart9[times].Series[0]["PointWidth"] = "0.6";
            chart9[times].Series[0]["DrawingStyle"] = "Cylinder";//绘图风格默认的是方形，此时可以改变成圆柱型
            //chart8[times].ChartAreas[0].Area3DStyle.Enable3D = true;//柱体的显示风格为3D
            chart9[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            //chart9[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            chart9[times].ChartAreas[0].AxisX.Interval = 1;
            chart9[times].Series[0].ChartType = SeriesChartType.StackedColumn;//绘图类型为三维百分比堆积柱形图
            chart9[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart9[times].BorderlineColor = Color.Black;
            chart9[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart9[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart9[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion

        }

        public void Draw_baifenbiduijiyuanzhu(Chart[] chart10, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart10[times].ChartAreas.Add(charArea);
            chart10[times].Name = "chart10";
            Series s1 = new Series();
            Series s2 = new Series();
            chart10[times].Series.Add(s1);
            chart10[times].Series.Add(s2);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart10[times].Legends.Add(legend1);
            s1.XValueType = ChartValueType.String;
            s2.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart10[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart10[times].Width = initWidth;//默认的初始宽值
            chart10[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();

            title1.Text = "双击图形在属性中修改图题";
            chart10[times].Titles.Add(title1);

            title1.Text = "请双击绑定数据";
            #endregion

            #region 绘簇状柱形图并给出初始属性
            //chart10[times].Legends.Clear();
            chart10[times].Series[0]["PointWidth"] = "0.6";
            chart10[times].Series[1]["PointWidth"] = "0.6";
            chart10[times].Series[0]["DrawingStyle"] = "Cylinder";//绘图风格默认的是方形，此时可以改变成圆柱型
            chart10[times].Series[1]["DrawingStyle"] = "Cylinder";//绘图风格默认的是方形，此时可以改变成圆柱型
           
            chart10[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart10[times].Series[1].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart10[times].ChartAreas[0].AxisX.Interval = 1;
            chart10[times].Series[0].ChartType = SeriesChartType.StackedColumn100;//绘图类型为三维百分比堆积柱形图
            chart10[times].Series[1].ChartType = SeriesChartType.StackedColumn100;//绘图类型为三维百分比堆积柱形图
            chart10[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            chart10[times].Series[1].Color = Color.FromArgb(210, 100, 255, 100);//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart10[times].BorderlineColor = Color.Black;
            chart10[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart10[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart10[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_sanweiyuanzhu(Chart[] chart11, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart11[times].ChartAreas.Add(charArea);
            chart11[times].Name = "chart11";
            Series s1 = new Series();
            Series s2 = new Series();
            chart11[times].Series.Add(s1);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart11[times].Legends.Add(legend1);
            //chart11[times].Series.Add(s2);
            s1.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart11[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart11[times].Width = initWidth;//默认的初始宽值
            chart11[times].Height = initHeight;//默认的初始高值
           #endregion

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart11[times].Titles.Add(title1);
            #endregion
            title1.Text = "请双击绑定数据";
         
            #region 绘簇状柱形图并给出初始属性
            //chart11[times].Legends.Clear();
            chart11[times].Series[0]["PointWidth"] = "0.6";
            chart11[times].ChartAreas[0].Area3DStyle.Enable3D = true;//柱体的显示风格为3D
            chart11[times].Series[0]["DrawingStyle"] = "Cylinder";//绘图风格默认的是方形，此时可以改变成圆柱型
            chart11[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart11[times].ChartAreas[0].AxisX.Interval = 1;
            chart11[times].Series[0].ChartType = SeriesChartType.Column;//绘图类型为三维百分比堆积柱形图
            chart11[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart11[times].BorderlineColor = Color.Black;
            chart11[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart11[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart11[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_cuzhuangtiaoxing(Chart[] chart12, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart12[times].ChartAreas.Add(charArea);
            chart12[times].Name = "chart12";
            Series s1 = new Series();
            Series s2 = new Series();
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart12[times].Legends.Add(legend1);
            //s2.ChartArea = charArea;
            chart12[times].Series.Add(s1);//千万注意此处只可以添加一个序列！！！！！！！！！！
            //chart12[times].Series.Add(s2);
            s1.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart12[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart12[times].Width = initWidth;//默认的初始宽值
            chart12[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart12[times].Titles.Add(title1);
            #endregion

            title1.Text = "请双击绑定数据";

            #region 绘簇状柱形图并给出初始属性
            //chart12[times].Legends.Clear();
            chart12[times].Series[0]["PointWidth"] = "0.6";
            chart12[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart12[times].Series[0].ChartType = SeriesChartType.Bar;//绘图类型为三维百分比堆积柱形图
           
            chart12[times].ChartAreas[0].AxisX.Interval = 1;
            chart12[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart12[times].BorderlineColor = Color.Black;
            chart12[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart12[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart12[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_duijitiaoxing(Chart[] chart13, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart13[times].ChartAreas.Add(charArea);
            chart13[times].Name = "chart13";
            Series s1 = new Series();
            Series s2 = new Series();
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart13[times].Legends.Add(legend1);
            //s2.ChartArea = charArea;
            chart13[times].Series.Add(s1);//千万注意此处只可以添加一个序列！！！！！！！！！！
            //chart12[times].Series.Add(s2);
            s1.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart13[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart13[times].Width = initWidth;//默认的初始宽值
            chart13[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart13[times].Titles.Add(title1);
            #endregion

            title1.Text = "请双击绑定数据";
            #region 绘簇状柱形图并给出初始属性
            //chart13[times].Legends.Clear();
            chart13[times].Series[0]["PointWidth"] = "0.6";
            chart13[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart13[times].Series[0].ChartType = SeriesChartType.StackedBar;//绘图类型为三维百分比堆积柱形图
            chart13[times].ChartAreas[0].AxisX.Interval = 1;
            chart13[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart13[times].BorderlineColor = Color.Black;
            chart13[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart13[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart13[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_baifenbiduijitiaoxing(Chart[] chart14, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart14[times].ChartAreas.Add(charArea);
            chart14[times].Name = "chart14";
            Series s1 = new Series();
            Series s2 = new Series();
            //s2.ChartArea = charArea;
            chart14[times].Series.Add(s1);
            chart14[times].Series.Add(s2);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart14[times].Legends.Add(legend1);
            s1.XValueType = ChartValueType.String;
            s2.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart14[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart14[times].Width = initWidth;//默认的初始宽值
            chart14[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart14[times].Titles.Add(title1);
            #endregion

            title1.Text = "请双击绑定数据";
            #region 绘簇状柱形图并给出初始属性
            //chart14[times].Legends.Clear();
            chart14[times].Series[0]["PointWidth"] = "0.6";
            chart14[times].Series[1]["PointWidth"] = "0.6";
          
            chart14[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart14[times].Series[1].IsValueShownAsLabel = true;//数据值显示在圆柱体上

            chart14[times].ChartAreas[0].AxisX.Interval = 1;
            chart14[times].Series[0].ChartType = SeriesChartType.StackedBar100;//绘图类型为三维百分比堆积柱形图
            chart14[times].Series[1].ChartType = SeriesChartType.StackedBar100;//绘图类型为三维百分比堆积柱形图
            chart14[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            chart14[times].Series[1].Color = Color.FromArgb(210, 100, 255, 100);//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart14[times].BorderlineColor = Color.Black;
            chart14[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart14[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart14[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_sanweicuzhuangtiaoxing(Chart[] chart15, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart15[times].ChartAreas.Add(charArea);
            chart15[times].Name = "chart15";
            Series s1 = new Series();
            Series s2 = new Series();
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart15[times].Legends.Add(legend1);
            //s2.ChartArea = charArea;
            chart15[times].Series.Add(s1);//千万注意此处只可以添加一个序列！！！！！！！！！！
            //chart12[times].Series.Add(s2);
            s1.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart15[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart15[times].Width = initWidth;//默认的初始宽值
            chart15[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart15[times].Titles.Add(title1);
            #endregion

            title1.Text = "请双击绑定数据";
            #region 绘簇状柱形图并给出初始属性
            //chart15[times].Legends.Clear();
            chart15[times].ChartAreas[0].Area3DStyle.Enable3D = true;//柱体的显示风格为3D
            chart15[times].Series[0]["PointWidth"] = "0.6";
            chart15[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart15[times].Series[0].ChartType = SeriesChartType.Bar;//绘图类型为三维百分比堆积柱形图
            chart15[times].ChartAreas[0].AxisX.Interval = 1;
            chart15[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart15[times].BorderlineColor = Color.Black;
            chart15[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart15[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart15[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_sanweiduijitiaoxing(Chart[] chart16, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart16[times].ChartAreas.Add(charArea);
            chart16[times].Name = "chart16";
            Series s1 = new Series();
            Series s2 = new Series();
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart16[times].Legends.Add(legend1);
            //s2.ChartArea = charArea;
            chart16[times].Series.Add(s1);//千万注意此处只可以添加一个序列！！！！！！！！！！
            //chart12[times].Series.Add(s2);
            s1.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart16[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart16[times].Width = initWidth;//默认的初始宽值
            chart16[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart16[times].Titles.Add(title1);
            #endregion

            title1.Text = "请双击绑定数据";
            #region 绘簇状柱形图并给出初始属性
            //chart16[times].Legends.Clear();
            chart16[times].ChartAreas[0].Area3DStyle.Enable3D = true;//柱体的显示风格为3D
            chart16[times].Series[0]["PointWidth"] = "0.6";
            chart16[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart16[times].Series[0].ChartType = SeriesChartType.StackedBar;//绘图类型为三维百分比堆积柱形图
            chart16[times].ChartAreas[0].AxisX.Interval = 1;
            chart16[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart16[times].BorderlineColor = Color.Black;
            chart16[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart16[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart16[times].BorderSkin.BackColor = Color.WhiteSmoke;

            #endregion
        }

        public void Draw_sanweibaifenbiduijitiaoxing(Chart[] chart17, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart17[times].ChartAreas.Add(charArea);
            chart17[times].Name = "chart17";
            Series s1 = new Series();
            Series s2 = new Series();
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart17[times].Legends.Add(legend1);
            //s2.ChartArea = charArea;
            chart17[times].Series.Add(s1);//千万注意此处只可以添加一个序列！！！！！！！！！！
            chart17[times].Series.Add(s2);
            s1.XValueType = ChartValueType.String;
            s2.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart17[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart17[times].Width = initWidth;//默认的初始宽值
            chart17[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart17[times].Titles.Add(title1);
            #endregion

            title1.Text = "请双击绑定数据";
            #region 绘簇状柱形图并给出初始属性
            //chart17[times].Legends.Clear();
            chart17[times].Series[0]["PointWidth"] = "0.6";
            chart17[times].Series[1]["PointWidth"] = "0.6";
            chart17[times].ChartAreas[0].Area3DStyle.Enable3D = true;//柱体的显示风格为3D
            chart17[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart17[times].Series[1].IsValueShownAsLabel = true;//数据值显示在圆柱体上

            chart17[times].ChartAreas[0].AxisX.Interval = 1;
            chart17[times].Series[0].ChartType = SeriesChartType.StackedBar100;//绘图类型为三维百分比堆积柱形图
            chart17[times].Series[1].ChartType = SeriesChartType.StackedBar100;//绘图类型为三维百分比堆积柱形图
            chart17[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            chart17[times].Series[1].Color = Color.FromArgb(210, 100, 255, 100);//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart17[times].BorderlineColor = Color.Black;
            chart17[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart17[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart17[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_cuzhuangshuipingyuanzhu(Chart[] chart18, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart18[times].ChartAreas.Add(charArea);
            chart18[times].Name = "chart18";
            Series s1 = new Series();
            Series s2 = new Series();
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart18[times].Legends.Add(legend1);
            //s2.ChartArea = charArea;
            chart18[times].Series.Add(s1);//千万注意此处只可以添加一个序列！！！！！！！！！！
            //chart12[times].Series.Add(s2);
            s1.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart18[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart18[times].Width = initWidth;//默认的初始宽值
            chart18[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart18[times].Titles.Add(title1);
            #endregion

            title1.Text = "请双击绑定数据";
            #region 绘簇状柱形图并给出初始属性
            //chart18[times].Legends.Clear();
            chart18[times].Series[0]["DrawingStyle"] = "Cylinder";//绘图风格默认的是方形，此时可以改变成圆柱型
            chart18[times].Series[0]["PointWidth"] = "0.6";
            chart18[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart18[times].Series[0].ChartType = SeriesChartType.Bar;//绘图类型为三维百分比堆积柱形图
            chart18[times].ChartAreas[0].AxisX.Interval = 1;
            chart18[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart18[times].BorderlineColor = Color.Black;
            chart18[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart18[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart18[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_duijishuipingyuanzhu(Chart[] chart19, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart19[times].ChartAreas.Add(charArea);
            chart19[times].Name = "chart19";
            Series s1 = new Series();
            Series s2 = new Series();
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart19[times].Legends.Add(legend1);
            //s2.ChartArea = charArea;
            chart19[times].Series.Add(s1);//千万注意此处只可以添加一个序列！！！！！！！！！！
            //chart12[times].Series.Add(s2);
            s1.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart19[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart19[times].Width = initWidth;//默认的初始宽值
            chart19[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart19[times].Titles.Add(title1);
            #endregion

            title1.Text = "请双击绑定数据";
            #region 绘簇状柱形图并给出初始属性
            //chart19[times].Legends.Clear();
            chart19[times].Series[0]["DrawingStyle"] = "Cylinder";//绘图风格默认的是方形，此时可以改变成圆柱型
            chart19[times].Series[0]["PointWidth"] = "0.6";
            chart19[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart19[times].Series[0].ChartType = SeriesChartType.StackedBar;//绘图类型为三维百分比堆积柱形图
            chart19[times].ChartAreas[0].AxisX.Interval = 1;
            chart19[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart19[times].BorderlineColor = Color.Black;
            chart19[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart19[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart19[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }

        public void Draw_baifenbiduijishuipingyuanzhu(Chart[] chart20, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart20[times].ChartAreas.Add(charArea);
            chart20[times].Name = "chart20";
            Series s1 = new Series();
            Series s2 = new Series();
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart20[times].Legends.Add(legend1);
            //s2.ChartArea = charArea;
            chart20[times].Series.Add(s1);//千万注意此处只可以添加一个序列！！！！！！！！！！
            chart20[times].Series.Add(s2);
            s1.XValueType = ChartValueType.String;
            s2.XValueType = ChartValueType.String;
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart20[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart20[times].Width = initWidth;//默认的初始宽值
            chart20[times].Height = initHeight;//默认的初始高值
            #endregion

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart20[times].Titles.Add(title1);
            #endregion

            title1.Text = "请双击绑定数据";
            #region 绘簇状柱形图并给出初始属性
            //chart20[times].Legends.Clear();
            chart20[times].Series[0]["PointWidth"] = "0.6";
            chart20[times].Series[1]["PointWidth"] = "0.6";
            chart20[times].Series[0]["DrawingStyle"] = "Cylinder";//绘图风格默认的是方形，此时可以改变成圆柱型
            chart20[times].Series[1]["DrawingStyle"] = "Cylinder";//绘图风格默认的是方形，此时可以改变成圆柱型
            chart20[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart20[times].Series[1].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart20[times].ChartAreas[0].AxisX.Interval = 1;
            
            chart20[times].Series[0].ChartType = SeriesChartType.StackedBar100;//绘图类型为三维百分比堆积柱形图
            chart20[times].Series[1].ChartType = SeriesChartType.StackedBar100;//绘图类型为三维百分比堆积柱形图
            chart20[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            chart20[times].Series[1].Color = Color.FromArgb(210, 100, 255, 100);//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart20[times].BorderlineColor = Color.Black;
            chart20[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart20[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart20[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion
        }




        public void Draw_PieChart(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "PieChart";
            Series s1 = new Series();
            //Series s2 = new Series();
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            //chart[times].Series.Add(s2);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion
            s1.XValueType = ChartValueType.String;
            #endregion

            title1.Text = "请双击绑定数据";
            chart[times].Series[0].ChartType = SeriesChartType.Pie;//选择图的类型为饼图
            //chart[times].ChartAreas[0].AxisX.Interval = 1;
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }


        public void Draw_DivPieChart(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "DivPieChart";
            Series s1 = new Series();
            //Series s2 = new Series();
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            //chart[times].Series.Add(s2);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion
            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].ChartAreas[0].AxisX.Interval = 1;

            chart[times].Series[0].ChartType = SeriesChartType.Pie;//选择图的类型为饼图
            //在这里就把所有的模块分离开


            //foreach (DataPoint point in chart[times].Series[0].Points)
            //{
            //    point["Exploded"] = "true";  //每次单击的时候都让饼图初始合在一起
            //}
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_3DPieChart(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "3DPieChart";
            Series s1 = new Series();
            //Series s2 = new Series();
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            //chart[times].Series.Add(s2);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion
            s1.XValueType = ChartValueType.String;

            title1.Text = "请双击绑定数据";
            chart[times].Series[0].ChartType = SeriesChartType.Pie;//选择图的类型为饼图
            chart[times].ChartAreas[0].AxisX.Interval = 1;
            chart[times].ChartAreas[0].Area3DStyle.Enable3D = true;
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_3DDivPieChart(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "3DDivPieChart";
            Series s1 = new Series();
            //Series s2 = new Series();
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            //chart[times].Series.Add(s2);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion
            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";

            chart[times].Series[0].ChartType = SeriesChartType.Pie;//选择图的类型为饼图
            chart[times].ChartAreas[0].AxisX.Interval = 1;
            chart[times].ChartAreas[0].Area3DStyle.Enable3D = true;
            foreach (DataPoint point in chart[times].Series[0].Points)
            {
                point["Exploded"] = "true";  //每次单击的时候都让饼图初始合在一起
            }
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_RingChart(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "RingChart";
            Series s1 = new Series();
            //Series s2 = new Series();
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            //chart[times].Series.Add(s2);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion

            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].Series[0].ChartType = SeriesChartType.Doughnut;//选择图的类型为饼图
            chart[times].ChartAreas[0].AxisX.Interval = 1;
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_DivRingChart(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "DivRingChart";
            Series s1 = new Series();
            //Series s2 = new Series();
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            //chart[times].Series.Add(s2);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion
            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].ChartAreas[0].AxisX.Interval = 1;
            chart[times].Series[0].ChartType = SeriesChartType.Doughnut;//选择图的类型为饼图
          
            foreach (DataPoint point in chart[times].Series[0].Points)
            {
                point["Exploded"] = "true";  //每次单击的时候都让饼图初始合在一起
            }
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_AreaChart(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
          
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "chart27";
            Series s1 = new Series();
            //Series s2 = new Series();
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            //chart[times].Series.Add(s2);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion

            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].ChartAreas[0].AxisX.Interval = 1;

            chart[times].Series[0].ChartType = SeriesChartType.Area;//选择图的类型为饼图
        
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_StackAreaChart(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "chart28";
            Series s1 = new Series();
            //Series s2 = new Series();
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            //chart[times].Series.Add(s2);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion

            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].Series[0].ChartType = SeriesChartType.StackedArea;//选择图的类型为饼图

            chart[times].ChartAreas[0].AxisX.Interval = 1;
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_baifenbiStackAreaChart(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "chart29";
            Series s1 = new Series();
            Series s2 = new Series();
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            chart[times].Series.Add(s2);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion
            s1.XValueType = ChartValueType.String;
            s2.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].ChartAreas[0].AxisX.Interval = 1;
            chart[times].Series[0].ChartType = SeriesChartType.StackedArea100;//选择图的类型为饼图
            chart[times].Series[1].ChartType = SeriesChartType.StackedArea100;//选择图的类型为饼图
         
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart[times].Series[1].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            chart[times].Series[1].Color = Color.FromArgb(210, 100, 255, 100);//定义绘出的柱体的颜色
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_sanweiAreaChart(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "chart30";
            Series s1 = new Series();
            Series s2 = new Series();
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            //chart[times].Series.Add(s2);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion
            s1.XValueType = ChartValueType.String;
            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].ChartAreas[0].AxisX.Interval = 1;
            chart[times].Series[0].ChartType = SeriesChartType.Area;//选择图的类型为饼图
        
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            chart[times].ChartAreas[0].Area3DStyle.Enable3D = true;
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_sanweiStackAreaChart(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "chart31";
            Series s1 = new Series();
            Series s2 = new Series();
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            //chart[times].Series.Add(s2);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion
            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].ChartAreas[0].AxisX.Interval = 1;

            chart[times].Series[0].ChartType = SeriesChartType.StackedArea;//选择图的类型为饼图
        
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            chart[times].ChartAreas[0].Area3DStyle.Enable3D = true;
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_sanweibaifenbiStackAreaChart(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "chart32";
            Series s1 = new Series();
            Series s2 = new Series();
            //s2.ChartArea = charArea;
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            chart[times].Series.Add(s1);
            chart[times].Series.Add(s2);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion
            s1.XValueType = ChartValueType.String;
            s2.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].ChartAreas[0].AxisX.Interval = 1;
            
            chart[times].Series[0].ChartType = SeriesChartType.StackedArea100;//选择图的类型为饼图
            chart[times].Series[1].ChartType = SeriesChartType.StackedArea100;//选择图的类型为饼图
           
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart[times].Series[1].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            chart[times].Series[1].Color = Color.FromArgb(210, 100, 255, 100);//定义绘出的柱体的颜色
            chart[times].ChartAreas[0].Area3DStyle.Enable3D = true;
            chart[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            chart[times].Series[1]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_zhexiantu(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "chart33";
            Series s1 = new Series();
            Series s2 = new Series();
            //s2.ChartArea = charArea;
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            chart[times].Series.Add(s1);
            //chart[times].Series.Add(s2);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion

            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].ChartAreas[0].AxisX.Interval = 1;
            
            chart[times].Series[0].ChartType = SeriesChartType.Line;//选择图的类型为饼图
        
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
         
            chart[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
       
           
            chart[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型

            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }


        public void Draw_daishujubiaojidezhexiantu(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "chart34";
            Series s1 = new Series();
            Series s2 = new Series();
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            //chart[times].Series.Add(s2);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion
            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].ChartAreas[0].AxisX.Interval = 1;
            

            chart[times].Series[0].ChartType = SeriesChartType.Line;//选择图的类型为饼图

        

            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上

            chart[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色

            chart[times].Series[0].MarkerStyle = MarkerStyle.Diamond;//设置数据点的形状
            chart[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型

            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_sanweizhexiantu(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "chart35";
            Series s1 = new Series();
            Series s2 = new Series();
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
          
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion
            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].ChartAreas[0].AxisX.Interval = 1;
            

            chart[times].Series[0].ChartType = SeriesChartType.Line;//选择图的类型为饼图

           

            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上

            chart[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            chart[times].ChartAreas[0].Area3DStyle.Enable3D = true;
            //chart[times].Series[0].MarkerStyle = MarkerStyle.Diamond;//设置数据点的形状
            chart[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型

            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_jindaishujubiaojidesandiantu(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "chart36";
            Series s1 = new Series();
            Series s2 = new Series();
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            //chart[times].Series.Add(s2);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion
            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].ChartAreas[0].AxisX.Interval = 1;

            chart[times].Series[0].ChartType = SeriesChartType.Point;//选择图的类型为饼图
            chart[times].Series[0].MarkerStyle = MarkerStyle.Diamond;//选择散点图的点的类型（形状）
           
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上

            chart[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //chart[times].ChartAreas[0].Area3DStyle.Enable3D = true;
            //chart[times].Series[0].MarkerStyle = MarkerStyle.Diamond;//设置数据点的形状
            chart[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型

            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_daipinghuaxianheshujubiaojidesandiantu(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "chart37";
            Series s1 = new Series();
            Series s2 = new Series();
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            //chart[times].Series.Add(s2);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion
            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].ChartAreas[0].AxisX.Interval = 1;

            chart[times].Series[0].ChartType = SeriesChartType.Spline;//
            chart[times].Series[0].MarkerStyle = MarkerStyle.Diamond;//选择散点图的点的类型（形状）
         
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上

            chart[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //chart[times].ChartAreas[0].Area3DStyle.Enable3D = true;
            //chart[times].Series[0].MarkerStyle = MarkerStyle.Diamond;//设置数据点的形状
            chart[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_daipinghuaxiansandiantu(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "chart38";
            Series s1 = new Series();
            Series s2 = new Series();
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            //chart[times].Series.Add(s2);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion

            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].ChartAreas[0].AxisX.Interval = 1;
            chart[times].Series[0].ChartType = SeriesChartType.Spline;//
            //chart[times].Series[0].MarkerStyle = MarkerStyle.Diamond;//选择散点图的点的类型（形状）
            
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上

            chart[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //chart[times].ChartAreas[0].Area3DStyle.Enable3D = true;
            //chart[times].Series[0].MarkerStyle = MarkerStyle.Diamond;//设置数据点的形状
            chart[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }


        public void Draw_daizhixianheshujubiaojidesandiantu(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "chart39";
            Series s1 = new Series();
            Series s2 = new Series();
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            //chart[times].Series.Add(s2);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion
            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].ChartAreas[0].AxisX.Interval = 1;
            chart[times].Series[0].ChartType = SeriesChartType.Line;
            chart[times].Series[0].MarkerStyle = MarkerStyle.Diamond;//选择散点图的点的类型（形状）
         
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上

            chart[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //chart[times].ChartAreas[0].Area3DStyle.Enable3D = true;
            //chart[times].Series[0].MarkerStyle = MarkerStyle.Diamond;//设置数据点的形状
            chart[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型

            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }

        public void Draw_daizhixiansandiantu(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            #region 动态创建一个表
            //先动态创建一个chart表
            ChartArea charArea = new ChartArea();
            chart[times].ChartAreas.Add(charArea);
            chart[times].Name = "chart40";
            Series s1 = new Series();
            Series s2 = new Series();
            //s2.ChartArea = charArea;
            chart[times].Series.Add(s1);
            Legend legend1 = new Legend();
            legend1.Name = "Legend1";
            chart[times].Legends.Add(legend1);
            ////chart[times].Series.Add(s2);
            #region 定义chart表的初始位置和初始的宽和高，可以封装成方法
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);//给才显示出来的chart表一个初始的位置
            chart[times].Width = initWidth;//默认的初始宽值
            chart[times].Height = initHeight;//默认的初始高值

            Title title1 = new Title();
            title1.Text = "双击图形在属性中修改图题";
            chart[times].Titles.Add(title1);
            #endregion

            #endregion
            s1.XValueType = ChartValueType.String;
            title1.Text = "请双击绑定数据";
            chart[times].ChartAreas[0].AxisX.Interval = 1;
            chart[times].Series[0].ChartType = SeriesChartType.Line;
            //chart[times].Series[0].MarkerStyle = MarkerStyle.Diamond;//选择散点图的点的类型（形状）
        
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上

            chart[times].Series[0].Color = Color.Red;//定义绘出的柱体的颜色
            //chart[times].ChartAreas[0].Area3DStyle.Enable3D = true;
            //chart[times].Series[0].MarkerStyle = MarkerStyle.Diamond;//设置数据点的形状
            chart[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型

            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
        }
        #endregion

        /////////////////////////////////////////下面开始画递减曲线///////////////////////////////////////////////////
        public void Draw_dijianquxian(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            ChartArea chartArea1 = new ChartArea();
            chartArea1.Name = "Default";
            chart[times].ChartAreas.Add(chartArea1);
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);
            chart[times].Name = "递减曲线";
            Series series1 = new Series();
            Series series2 = new Series();
            series1.Name = "Series1";
            //series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            //series1.ChartType = SeriesChartType.Column;
            series2.Name = "Series2";
            series1.XValueType = ChartValueType.DateTime;
            series2.XValueType = ChartValueType.DateTime;
            chart[times].Series.Add(series1);
            chart[times].Series.Add(series2);
            chart[times].Size = new System.Drawing.Size(initWidth, initHeight);
            


            Title title1 = new Title();
            title1.Text = "请双击绑定数据";
            chart[times].Titles.Add(title1);
            //#region 读取数据
            ////日期变量读取
            //String[] dt2 = { "2000/8/1", "2000/9/1", "2000/10/1", "2000/11/1", "2000/12/1", "2001/1/1", "2001/2/1","2001/3/1", "2001/4/1", "2001/5/1", "2001/6/1", "2001/7/1" };
           
            ////产量数据读取
            //int[] p2 = { 95, 98, 102, 106, 108, 118, 119, 114, 110, 106, 100, 95 };

            //#endregion

            #region //绘制柱状填充图
            chart[times].Legends.Clear();
            chart[times].Series[0]["PointWidth"] = "0.6";
            chart[times].Series[0].IsValueShownAsLabel = true;//数据值显示在圆柱体上
            chart[times].Series[0]["DrawingStyle"] = "Emboss";//绘图风格为浮雕型
            chart[times].Series[1].ChartType = SeriesChartType.Spline;
            chart[times].Series[0].ChartType = SeriesChartType.Column;
            //chart[times].Series[0].Points.DataBindXY(dt2, p2);
            chart[times].ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chart[times].Series[0]["BarLabelStyle"] = "Center";
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
            #endregion 


        }


        /////////////////////////////////////////下面开始画相渗曲线///////////////////////////////////////////////////

        public void Draw_xiangshenquxian(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            ChartArea chartArea1 = new ChartArea();
            chartArea1.Name = "Default";
            chart[times].ChartAreas.Add(chartArea1);


            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);
            chart[times].Name = "相渗曲线";
            Series series1 = new Series();
            Series series2 = new Series();
            Series series3 = new Series();
            series1.Name = "Series1";
            series2.Name = "Series2";
            series3.Name = "Series3";
           
            chart[times].Series.Add(series1);
            chart[times].Series.Add(series2);
            chart[times].Series.Add(series3);
            chart[times].Size = new System.Drawing.Size(initWidth, initHeight);
            chart[times].Series[0].Color = Color.Red;
            chart[times].Series[1].Color = Color.Yellow;
            chart[times].Series[2].Color = Color.Blue;

            //XY坐标轴属性设置
            chart[times].ChartAreas["Default"].AxisX.Minimum = 0;
            chart[times].ChartAreas["Default"].AxisX.Maximum = 1;
            chart[times].ChartAreas["Default"].AxisX.Interval = 0.04;
            chart[times].ChartAreas["Default"].AxisX.MajorTickMark.Enabled = false;


            chart[times].ChartAreas["Default"].AxisY.Minimum = 0;
            chart[times].ChartAreas["Default"].AxisY.Maximum = 1;
            chart[times].ChartAreas["Default"].AxisY.Interval = 0.04;
            chart[times].ChartAreas["Default"].AxisY.MajorTickMark.Enabled = false;


            Title title1 = new Title();
            title1.Text = "请绑定数据";
            chart[times].Titles.Add(title1);
            //#region 读取数据
            //double[] Sw = { 0.32, 0.352, 0.384, 0.416, 0.448, 0.48, 0.512, 0.544, 0.576, 0.608, 0.64, 0.672, 0.704, 0.736, 0.768, 0.8 };
            //double[] Kro = { 0.65, 0.5861, 0.5244, 0.4651, 0.4082, 0.3538, 0.3021, 0.2532, 0.2072, 0.1644, 0.1251, 0.0895, 0.0581, 0.0316, 0.0112, 0 };
            //double[] Krw = { 0, 0.0017, 0.0059, 0.012, 0.0198, 0.0292, 0.0402, 0.0527, 0.0666, 0.0818, 0.0984, 0.1162, 0.1353, 0.1557, 0.1773, 0.2 };
            //#endregion


            //#region //绘制柱状填充图
            //for (int i = 0; i < Sw.Length; i++)
            //    chart[times].Series["Series1"].Points.AddXY(Sw[i], Kro[i]);
            //    chart[times].Series["Series1"].Points[i].ToolTip = "Sw: " + Sw[i].ToString() + "\r\n" + "Kro: " + Kro[i].ToString();
            //    chart[times].Series["Series2"].Points.AddXY(Sw[i], Krw[i]);
            //    chart[times].Series["Series2"].Points[i].ToolTip = "Sw: " + Sw[i].ToString() + "\r\n" + "Krw: " + Krw[i].ToString();
            //}
            chart[times].Series[0].ChartType = SeriesChartType.Spline;
            chart[times].Series[1].ChartType = SeriesChartType.Spline;
            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
            //#endregion


        }

        /////////////////////////////////////////下面开始画生产开发曲线///////////////////////////////////////////////////
        public void Draw_shengchankaifaquxian(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            ChartArea chartArea1 = new ChartArea();
            ChartArea chartArea2 = new ChartArea();
            ChartArea chartArea3 = new ChartArea();
            //ChartArea chartArea4 = new ChartArea();
            Series series1 = new Series();
            Series series2 = new Series();
            Series series3 = new Series();
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisX.LineWidth = 0;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            //chartArea1.Position.Auto = false;
            //chartArea1.Position.Height = 20F;
            //chartArea1.Position.Width = 90F;
            //chartArea1.Position.X = 4.5F;
            //chartArea1.Position.Y = 18F;

            chartArea2.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisY.MajorGrid.Enabled = false;
            //chartArea2.Position.Auto = false;
            //chartArea2.Position.Height = 20F;
            //chartArea2.Position.Width = 90F;
            //chartArea2.Position.X = 4.5F;
            //chartArea2.Position.Y = 44F;
         
            chartArea3.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea3.AxisX.MajorGrid.Enabled = false;
            chartArea3.AxisY.MajorGrid.Enabled = false;
            //chartArea3.Position.Auto = false;
            //chartArea3.Position.Height = 20F;
            //chartArea3.Position.Width = 90f;
            //chartArea3.Position.X = 4.5f;
            //chartArea3.Position.Y = 70f;

          
            chart[times].ChartAreas.Add(chartArea1);
            chart[times].ChartAreas.Add(chartArea2);
            chart[times].ChartAreas.Add(chartArea3);
            //chart[times].ChartAreas.Add(chartArea4);
            //chart[times].ChartAreas[0].AlignmentOrientation = AreaAlignmentOrientations.Vertical;
            //chart[times].ChartAreas[1].AlignmentOrientation = AreaAlignmentOrientations.Vertical;
            //chart[times].ChartAreas[2].AlignmentOrientation = AreaAlignmentOrientations.Vertical;
            //chart[times].ChartAreas[3].AlignmentOrientation = AreaAlignmentOrientations.Vertical;

            chart[times].Location = new System.Drawing.Point(147, 3);
            chart[times].Name = "生产开发曲线";
            series1.ChartArea = chartArea1.Name;
            //series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = chartArea2.Name;
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            //series2.Legend = "Legend1";
            series2.Name = "Series2";
            series3.ChartArea = chartArea3.Name;
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            //series3.Legend = "Legend1";
            series3.Name = "Series3";
            chart[times].Series.Add(series1);
            chart[times].Series.Add(series2);
            chart[times].Series.Add(series3);

            series1.XValueType = ChartValueType.String;
            series2.XValueType = ChartValueType.String;
            series3.XValueType = ChartValueType.String;
            //chartArea1.Name = "开油井";//后期这里的ChartArea.Name就是从TXT中读取的列名
            //chartArea2.Name = "日油水平";//后期这里的ChartArea.Name就是从TXT中读取的列名
            //chartArea3.Name = "日液水平";//后期这里的ChartArea.Name就是从TXT中读取的列名
            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);
            chart[times].Name = "生产开发曲线";

         

            chart[times].Size = new System.Drawing.Size(initWidth, initHeight);

            Title title1 = new Title();
            title1.Text = "请双击绑定数据";
            chart[times].Titles.Add(title1);
        

            chart[times].Series[0].IsValueShownAsLabel = true;
            chart[times].Series[1].IsValueShownAsLabel = true;
            chart[times].Series[2].IsValueShownAsLabel = true;
           
            //chart[times].ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
            //chart[times].ChartAreas[1].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
            //chart[times].ChartAreas[2].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
            chart[times].ChartAreas[0].AxisY.Title = chartArea1.Name;//后期这里的ChartArea.Name就是从TXT中读取的列名
            chart[times].ChartAreas[1].AxisY.Title = chartArea2.Name;//后期这里的ChartArea.Name就是从TXT中读取的列名
            chart[times].ChartAreas[2].AxisY.Title = chartArea3.Name;//后期这里的ChartArea.Name就是从TXT中读取的列名
            //#region 读取数据
            //DateTime[] dt = new DateTime[] { Convert.ToDateTime("2006/1/1"), Convert.ToDateTime("2006/3/1"), Convert.ToDateTime("2006/5/1"), Convert.ToDateTime("2006/7/1"), Convert.ToDateTime("2006/9/1"), Convert.ToDateTime("2006/11/1"), Convert.ToDateTime("2007/1/1"), Convert.ToDateTime("2007/3/1"), Convert.ToDateTime("2007/5/1"), Convert.ToDateTime("2007/7/1") };
            //string[] x = new string[] { "20060101", "20060301", "20060501", "20060701", "20060901", "20061101", "20070101", "20070301", "20070501", "20070701" };
            //double[] y1 = new double[] { 27.1, 23.2, 11.1, 4.1, 27.1, 55.1, 11.1, 24.1, 10.1, 64.1 };
            //double[] y2 = new double[] { 17.3, 33.3, 21.1, 41.1, 37.1, 15.1, 21.1, 64.1, 30.1, 84.1 };
            //double[] y3 = new double[] { 171, 233, 21.5, 412.3, 327.4, 151.2, 21.3, 64.9, 301.2, 84.9 };
        

           #region //绘制柱状填充图
            chart[times].Series[0].Color = Color.Red;
          
            chart[times].Series[0].MarkerStyle = MarkerStyle.Diamond;//选择点的类型（形状）
            chart[times].Series[1].MarkerStyle = MarkerStyle.Circle;//选择点的类型（形状）
            chart[times].Series[2].MarkerStyle = MarkerStyle.Square;//选择点的类型（形状）

            chart[times].Series["Series1"].ChartType = SeriesChartType.Spline;  //图的形状
            chart[times].Series["Series2"].ChartType = SeriesChartType.Point;   //图的形状
            chart[times].Series["Series3"].ChartType = SeriesChartType.Column;  //图的形状

            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
            //chart[times].Series[0].Points.DataBindXY(x, y1);
            //chart[times].Series[1].Points.DataBindXY(x, y2);
            //chart[times].Series[2].Points.DataBindXY(x, y3);
        
            #endregion


        }

        //////////////////////////////////////////下面开始画油井注水曲线图///////////////////////////////////////////////
        public void Draw_shuijinzhushui(Chart[] chart, int times, int initPointX, int initPointY, int initWidth, int initHeight)
        {
            ChartArea chartArea1 = new ChartArea();
            ChartArea chartArea2 = new ChartArea();
            ChartArea chartArea3 = new ChartArea();
            ChartArea chartArea4 = new ChartArea();
            Series series1 = new Series();
            Series series2 = new Series();
            Series series3 = new Series();
            Series series4 = new Series();
            //Series series5 = new Series();
            //Series series6 = new Series();
            //Series series7 = new Series();
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisX.LineWidth = 0;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;

            chartArea2.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisY.MajorGrid.Enabled = false;

            chartArea3.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea3.AxisX.MajorGrid.Enabled = false;
            chartArea3.AxisY.MajorGrid.Enabled = false;

            chartArea4.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea4.AxisX.MajorGrid.Enabled = false;
            chartArea4.AxisY.MajorGrid.Enabled = false;


            chart[times].ChartAreas.Add(chartArea1);
            chart[times].ChartAreas.Add(chartArea2);
            chart[times].ChartAreas.Add(chartArea3);
            chart[times].ChartAreas.Add(chartArea4);

            //chart[times].ChartAreas[0].AlignmentOrientation = AreaAlignmentOrientations.Vertical;
            //chart[times].ChartAreas[1].AlignmentOrientation = AreaAlignmentOrientations.Vertical;
            //chart[times].ChartAreas[2].AlignmentOrientation = AreaAlignmentOrientations.Vertical;
            //chart[times].ChartAreas[3].AlignmentOrientation = AreaAlignmentOrientations.Vertical;

            //chart[times].Location = new System.Drawing.Point(147, 3);
            //chart[times].Name = "水井注水曲线";

            series1.ChartArea = chartArea1.Name;
            //series1.Legend = "Legend1";
            series1.Name = "Series1";

            series2.ChartArea = chartArea2.Name;
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            //series2.Legend = "Legend1";
            series2.Name = "Series2";

            series3.ChartArea = chartArea3.Name;
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            //series3.Legend = "Legend1";
            series3.Name = "Series3";

            series4.ChartArea = chartArea4.Name;
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            //series3.Legend = "Legend1";
            series4.Name = "Series4";

            //series5.ChartArea = chartArea3.Name;
            //series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            ////series3.Legend = "Legend1";
            //series5.Name = "Series5";

            //series6.ChartArea = chartArea3.Name;
            //series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            ////series3.Legend = "Legend1";
            //series6.Name = "Series6";

            //series7.ChartArea = chartArea4.Name;
            //series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            ////series3.Legend = "Legend1";
            //series7.Name = "Series7";

            chart[times].Series.Add(series1);
            chart[times].Series.Add(series2);
            chart[times].Series.Add(series3);
            chart[times].Series.Add(series4);
            //chart[times].Series.Add(series5);
            //chart[times].Series.Add(series6);
            //chart[times].Series.Add(series7);

            series1.XValueType = ChartValueType.DateTime;
            series2.XValueType = ChartValueType.DateTime;
            series3.XValueType = ChartValueType.DateTime;
            series4.XValueType = ChartValueType.DateTime;
            //series5.XValueType = ChartValueType.DateTime;
            //series6.XValueType = ChartValueType.DateTime;
            //series7.XValueType = ChartValueType.DateTime;

            //chartArea1.Name = "开油井";//后期这里的ChartArea.Name就是从TXT中读取的列名
            //chartArea2.Name = "日油水平";//后期这里的ChartArea.Name就是从TXT中读取的列名
            //chartArea3.Name = "日液水平";//后期这里的ChartArea.Name就是从TXT中读取的列名
            //chartArea4.Name = "日水水平";//后期这里的ChartArea.Name就是从TXT中读取的列名

            chart[times].Location = new System.Drawing.Point(initPointX, initPointY);
            chart[times].Name = "水井注水曲线";
            chart[times].Size = new System.Drawing.Size(initWidth, initHeight);

            Title title1 = new Title();
            title1.Text = "请双击绑定数据";
            chart[times].Titles.Add(title1);


            chart[times].Series[0].IsValueShownAsLabel = true;
            chart[times].Series[1].IsValueShownAsLabel = true;
            chart[times].Series[2].IsValueShownAsLabel = true;
            chart[times].Series[3].IsValueShownAsLabel = true;
            //chart[times].Series[4].IsValueShownAsLabel = true;
            //chart[times].Series[5].IsValueShownAsLabel = true;
            //chart[times].Series[6].IsValueShownAsLabel = true;

            //chart[times].ChartAreas[0].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
            //chart[times].ChartAreas[1].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
            //chart[times].ChartAreas[2].AxisX.Interval = 1;   //设置X轴坐标的间隔为1
            //chart[times].ChartAreas[0].AxisY.Title = chartArea1.Name;//后期这里的ChartArea.Name就是从TXT中读取的列名
            //chart[times].ChartAreas[1].AxisY.Title = chartArea2.Name;//后期这里的ChartArea.Name就是从TXT中读取的列名
            //chart[times].ChartAreas[2].AxisY.Title = chartArea3.Name;//后期这里的ChartArea.Name就是从TXT中读取的列名
            //chart[times].ChartAreas[3].AxisY.Title = chartArea4.Name;//后期这里的ChartArea.Name就是从TXT中读取的列名

            #region 读取数据 测试
            //DateTime[] dt = new DateTime[] { Convert.ToDateTime("2006/1/1"), Convert.ToDateTime("2006/3/1"), Convert.ToDateTime("2006/5/1"), Convert.ToDateTime("2006/7/1"), Convert.ToDateTime("2006/9/1"), Convert.ToDateTime("2006/11/1"), Convert.ToDateTime("2007/1/1"), Convert.ToDateTime("2007/3/1"), Convert.ToDateTime("2007/5/1"), Convert.ToDateTime("2007/7/1") };
            ////string[] x = new string[] { "20060101", "20060301", "20060501", "20060701", "20060901", "20061101", "20070101", "20070301", "20070501", "20070701" };
            //double[] y1 = new double[] { 27.1, 23.2, 11.1, 4.1, 27.1, 55.1, 11.1, 24.1, 10.1, 64.1 };
            //double[] y2 = new double[] { 17.3, 33.3, 21.1, 41.1, 37.1, 15.1, 21.1, 64.1, 30.1, 84.1 };
            //double[] y3 = new double[] { 11, 33, 21.5, 2.3, 32.4, 151.2, 21.3, 64.9, 301.2, 84.9 };
            //double[] y4 = new double[] { 171, 233, 21.5, 412.3, 37.4, 15.2, 21.3, 64.9, 31.2, 84.9 };
            //double[] y5 = new double[] { 171, 233, 21.5, 412.3, 37.4, 151.2, 21.3, 64.9, 301.2, 4.9 };
            //double[] y6 = new double[] { 171, 233, 21.5, 42.3, 327.4, 151.2, 21.3, 64.9, 301.2, 84.9 };
            //double[] y7 = new double[] { 171, 233, 21.5, 412.3, 37.4, 151.2, 21.3, 64.9, 301.2, 84.9 };
            #endregion

            #region //绘制柱状填充图
            chart[times].Series[0].Color = Color.Red;

            chart[times].Series[0].MarkerStyle = MarkerStyle.Diamond;//选择点的类型（形状）
            chart[times].Series[1].MarkerStyle = MarkerStyle.Circle;//选择点的类型（形状）
            chart[times].Series[2].MarkerStyle = MarkerStyle.Square;//选择点的类型（形状）
            chart[times].Series[3].MarkerStyle = MarkerStyle.Square;//选择点的类型（形状）
            //chart[times].Series[4].MarkerStyle = MarkerStyle.Square;//选择点的类型（形状）
            //chart[times].Series[5].MarkerStyle = MarkerStyle.Square;//选择点的类型（形状）
            //chart[times].Series[6].MarkerStyle = MarkerStyle.Square;//选择点的类型（形状）

            chart[times].Series["Series1"].ChartType = SeriesChartType.Spline;
            chart[times].Series["Series2"].ChartType = SeriesChartType.Point;
            chart[times].Series["Series3"].ChartType = SeriesChartType.Column;
            chart[times].Series["Series4"].ChartType = SeriesChartType.Column;

            //使得该图一出现的时候就突显状态
            chart[times].BorderlineColor = Color.Black;
            chart[times].BorderlineDashStyle = ChartDashStyle.Solid;
            chart[times].BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
            chart[times].BorderSkin.BackColor = Color.WhiteSmoke;
            //chart[times].Series["Series5"].ChartType = SeriesChartType.Column;
            //chart[times].Series["Series6"].ChartType = SeriesChartType.Column;
            //chart[times].Series["Series7"].ChartType = SeriesChartType.Column;


            //chart[times].Series[0].Points.DataBindXY(dt, y1);
            //chart[times].Series[1].Points.DataBindXY(dt, y2);
            //chart[times].Series[2].Points.DataBindXY(dt, y3);
            //chart[times].Series[3].Points.DataBindXY(dt, y4);
            //chart[times].Series[4].Points.DataBindXY(dt, y5);
            //chart[times].Series[5].Points.DataBindXY(dt, y6);
            //chart[times].Series[6].Points.DataBindXY(dt, y7);

            #endregion

        }
    }
}
