/* 
 * 作者;肖宇博
 * 日期：2014/6/22
 * 功能：这是我自己写的和鼠标交互的类，可以把所以与鼠标交互的函数都封装在该类中
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;

namespace GeoDemo
{
    class MouseEvent
    {

        private enum EnumMousePointPosition
        {
            MouseSizeNone = 0, //'无
            MouseSizeRight = 1, //'拉伸右边框
            MouseSizeLeft = 2, //'拉伸左边框
            MouseSizeBottom = 3, //'拉伸下边框
            MouseSizeTop = 4, //'拉伸上边框
            MouseSizeTopLeft = 5, //'拉伸左上角
            MouseSizeTopRight = 6, //'拉伸右上角
            MouseSizeBottomLeft = 7, //'拉伸左下角
            MouseSizeBottomRight = 8, //'拉伸右下角
            MouseDrag = 9   // '鼠标拖动
        }//枚举鼠标可能在图上移动的位置

        const int Band = 5;//定义鼠标移动到的边界。即距离边界5个像素单位时候鼠标Curse变形
        const int MinWidth = 250;//定义最小的长和宽
        const int MinHeight = 250;
        private static EnumMousePointPosition m_MousePointPosition;
        private static Point p, p1;
        

        //MouseEvent.MyMouseDoubleClick(sender, e, chart28, ref id, ref r)
        public static void MyMouseDoubleClick(Object sender, MouseEventArgs e, Chart[] chart, ref int id, ref int r)
        {
            try
            {
                //调用设置属性的窗体并且把要设置的对象传过去！！！
                Control ctrl = (Control)sender;
                id = (int)ctrl.Tag;//获得了鼠标点击的那个chart表的id
                MyObject.My_Chart1 = chart[id];//把鼠标点击的那个chart表传过去

                HitTestResult result = MyObject.My_Chart1.HitTest(e.X, e.Y);
                //        dddddddddddddddddddddddddddddddddddddddddddddddd序列
                
                if ( MyObject.My_Chart1.Name != "生产开发曲线")
                {
                    ToOperateTheChart.DoubleClickOnTheChart(MyObject.My_Chart1, result, ref r);
                }
                else if ( MyObject.My_Chart1.Name == "生产开发曲线")
                {
                    //生产开发曲线专用
                    ToOperateTheChart.DoubleClickOnTheChart4(MyObject.My_Chart1, result, ref r);
                }
                
            }
            catch
            {
                return;
            }
        }

       
        public static void MyMouseClick(Object sender,MouseEventArgs e,Chart [] chart,ref int id,ref int r)
        {
            try
            {
                //MessageBox.Show(MyObject.My_Chart1.Series.Count.ToString());
                //调用设置属性的窗体并且把要设置的对象传过去！！！
                Control ctrl = (Control)sender;
                id = (int)ctrl.Tag;//获得了鼠标点击的那个chart表的id
                MyObject.My_Chart1 = chart[id];//把鼠标点击的那个chart表传过去
                //只让当前的图显示在最上方
                chart[id].BringToFront();
                //只让当前的图边框显示出来
                //ToOperateTheChart.ChangeTheChartBoder(MyObject.My_Chart1, Color.WhiteSmoke, BorderSkinStyle.FrameThin1);
                HitTestResult result = MyObject.My_Chart1.HitTest(e.X, e.Y);  //result存储的是每次单击的图标元素（如果有）
                if (MyObject.My_Chart1.Series.Count == 1)
                {
                    ToOperateTheChart.ClickDataPoint(MyObject.My_Chart1, result, ref r);
                }
                else if (MyObject.My_Chart1.Series.Count == 2)
                {
                    if (MyObject.My_Chart1.Name == "递减曲线")
                    {
                        //只用改变柱形的就可以了
                        ToOperateTheChart.ClickDataPoint(MyObject.My_Chart1, result, ref r);
                    }
                    else
                    {
                        ToOperateTheChart.ClickDataPoint2(MyObject.My_Chart1, result, ref r);
                    }
                }
                else if (MyObject.My_Chart1.Series.Count == 3 && MyObject.My_Chart1.Name.Equals("相渗曲线"))//专为相渗
                {
                    ToOperateTheChart.ClickDataPoint3(MyObject.My_Chart1, result, ref r);
                }

                else if (MyObject.My_Chart1.Name.Equals("生产开发曲线"))
                {
                    //专为生产开发曲线单写一个函数，因为他有可能是4个以上

                    ToOperateTheChart.ClickDataPoint4(MyObject.My_Chart1, result, ref r);
                }

                else
                {
                    ToOperateTheChart.ClickDataPointUsedAll(MyObject.My_Chart1, result, ref r);//这个可以用于无论有几个序列
                    

                }

                // 如果单击的是Titles，将Title的边框显示出来
                //MainFrame mf = new MainFrame();
                //if (result.ChartElementType == ChartElementType.Title)//图题区域
                //{
                //    ToOperateTheChart.ChangeTheChartTitleBoder(MyObject.My_Chart1, Color.Black, ChartDashStyle.Solid);
                //    MyObject.MyTitle1 = chart[id].Titles[0];
                //}
                //else if (result.ChartElementType == ChartElementType.PlottingArea)//绘图区
                //{
                //    //MyObject.My_Chart1.ChartAreas[0].ShadowOffset = 10;
                //    //MyObject.My_Chart1.ChartAreas[0].ShadowColor = Color.Gray;
                //    ToOperateTheChart.ChangePlottingAreaBoder(MyObject.My_Chart1, Color.Gray);
                    
                //}
                //else if (result.ChartElementType == ChartElementType.DataPoint)//数据区
                //{
                //    for (int i = 0; i < MyObject .My_Chart1 .Series .Count ; i++)
                //    {
                //        if (result.Series == MyObject.My_Chart1.Series[i])
                //        {
                //            //for (int j = 0; j < MyObject.My_Chart1.Series[i].Points.Count; j++)
                //            //{
                //            //    MyObject.My_Chart1.Series[i].Points[j].BorderColor = Color.Black;
                //            //    MyObject.My_Chart1.Series[i].Points[j].BorderDashStyle = ChartDashStyle.Solid;
                //            //    MyObject.My_Chart1.Series[i].Points[j].BorderWidth = 2;
                //            //}
                //            //MyObject.My_Chart1.Series[i].BorderColor = Color.Black;
                //            //MyObject.My_Chart1.Series[i].BorderDashStyle = ChartDashStyle.Solid;
                //            //MyObject.My_Chart1.Series[i].BorderWidth = 2;
                //            MyObject.My_Chart1.Series[i].ShadowColor = Color.Gray;
                //            MyObject.My_Chart1.Series[i].ShadowOffset = 5;
                //        }
                //    }
                //}
                //else if (result.ChartElementType == ChartElementType.DataPoint && result.Series == MyObject.My_Chart1.Series[1])//数据区
                //{
                //    MyObject.My_Chart1.Series[1].BorderColor = Color.Black;
                //    MyObject.My_Chart1.Series[1].BorderDashStyle = ChartDashStyle.Solid;
                //}
                //else if (result.ChartElementType == ChartElementType.AxisLabels && result.Axis == MyObject.My_Chart1.ChartAreas[0].AxisY)//垂直轴区
                //{
                //    ToOperateTheChart.ChangeTheChartTitleBoder(MyObject.My_Chart1, Color.Black, ChartDashStyle.Solid);
                //}
                //else if (result.ChartElementType == ChartElementType.AxisLabels && result.Axis == MyObject.My_Chart1.ChartAreas[0].AxisX)//水平轴区
                //{
                //    //MyObject.My_Chart1.ChartAreas[0].AxisX.LineColor = Color.Red;
                //    //MyObject.My_Chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Red;
                //    //MyObject.My_Chart1.ChartAreas[0].AxisX.LineDashStyle = ChartDashStyle.Solid;
                    
                //    ToOperateTheChart.ChangeTheChartTitleBoder(MyObject.My_Chart1, Color.Black, ChartDashStyle.Solid);
                //}
                //else if (result.ChartElementType == ChartElementType.AxisTitle && result.Axis == MyObject.My_Chart1.ChartAreas[0].AxisX)//水平轴区题目
                //{
                //    MyObject.My_Chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Yellow;
                //    ToOperateTheChart.ChangeTheChartTitleBoder(MyObject.My_Chart1, Color.Black, ChartDashStyle.Solid);
                //}
                //else if (result.ChartElementType == ChartElementType.AxisTitle && result.Axis == MyObject.My_Chart1.ChartAreas[0].AxisY)//垂直轴区题目
                //{
                //    ToOperateTheChart.ChangeTheChartTitleBoder(MyObject.My_Chart1, Color.Black, ChartDashStyle.Solid);
                //}
                //else if (result.ChartElementType == ChartElementType.LegendItem)//图例区
                //{
                //    ToOperateTheChart.ChangeLegendItemBoder(MyObject.My_Chart1, Color.Gray);
                //}
                
            }
            catch
            {
                return;
            }
              
           
        }


        public static void MyMouseDown(Object sender,MouseEventArgs e,Chart [] chart,ref int MouseDownID,ref int id,ref int r,ref Point[] startp)
        {
            try
            {
                Control ctrl = (Control)sender;
                MouseDownID = (int)ctrl.Tag;//获得了鼠标点击的那个chart表的id
                startp[id] = e.Location;//记录图形移动前的位置
                MyObject.My_Chart1 = chart[MouseDownID];//把鼠标点击的那个chart表传过去
                //HitTestResult result = MyObject.My_Chart1.HitTest(e.X, e.Y);  //result存储的是每次单击的图标元素（如果有）
                //int x = e.X + MyObject.My_Chart1.Location.X;
                //int y = e.Y + MyObject.My_Chart1.Location.Y;
                //if (e.Button == MouseButtons.Right)
                //{
                //    MainFrame mf = new MainFrame();
                //    if (result.ChartElementType == ChartElementType.LegendItem)
                //    {
                //        mf.toolStripComboBox1.Text = "图例区";
                //    }
                //    else if (result.ChartElementType == ChartElementType.PlottingArea)
                //    {
                //        mf.toolStripComboBox1.Text = "绘图区";
                //    }
                //    else if (result.ChartElementType == ChartElementType.DataPoint)
                //    {
                //        for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                //        {
                //            if (result.Series == MyObject.My_Chart1.Series[i])
                //            {
                //                mf.toolStripComboBox1.Text = "序列" + (i + 1);
                //            }
                //        }

                //    }
                //    else if (result.ChartElementType == ChartElementType.AxisLabels)
                //    {
                //        for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                //        {
                //            if (result.Axis == MyObject.My_Chart1.ChartAreas[i].AxisY)
                //            {
                //                mf.toolStripComboBox1.Text = "垂直轴区";
                //            }
                //            else if (result.Axis == MyObject.My_Chart1.ChartAreas[0].AxisX)
                //            {
                //                mf.toolStripComboBox1.Text = "水平轴区";
                //            }
                //        }
                //    }
                //    else
                //    {
                //        mf.toolStripComboBox1.Text = "图表区";
                //    }
                //    if (mf.toolStrip1.Visible == false)
                //    {
                //        mf.toolStrip1.Visible = true;
                //    }

                //    if ((mf.panel1.Size.Height - y) < mf.contextMenuStrip1.Size.Height)
                //    {
                //        int y1 = mf.contextMenuStrip1.Size.Height - (mf.panel1.Size.Height - y);
                //        mf.toolStrip1.Location = new Point(x, y - 25 - y1);
                //    }
                //    else
                //    {
                //        mf.toolStrip1.Location = new Point(e.X + MyObject.My_Chart1.Location.X, e.Y + MyObject.My_Chart1.Location.Y - 25);
                //    }
                //    mf.toolStrip1.BringToFront();
                //}
                p.X = e.X;
                p.Y = e.Y;
                p1.X = e.X;
                p1.Y = e.Y;
            }
            catch
            {
                return;
            }
 
        }

       
        
        public static void MyMouseMove(Object sender, MouseEventArgs e,Chart[] chart,ref int id,ref Point[] startp,ref Point[] endp)
        {
            try
            {
                Control lCtrl = (Control)sender;
                id = (int)lCtrl.Tag;//获得了鼠标移动过的那个chart表的id

                if (e.Button == MouseButtons.Left)
                {
                    switch (m_MousePointPosition)
                    {
                        case EnumMousePointPosition.MouseDrag:

                            //使chart图不能移动到左边和上边
                            if ((p.X - e.X) > lCtrl.Left)
                            { lCtrl.Left = 0; }
                            else
                            {
                                lCtrl.Left = lCtrl.Left + e.X - p.X;
                            }
                            if ((p.Y - e.Y) > lCtrl.Top)
                            { lCtrl.Top = 0; }
                            else
                            {
                                lCtrl.Top = lCtrl.Top + e.Y - p.Y;
                            }
                            #region 晁  修改
                            Savelocation.value_x = lCtrl.Left;
                            Savelocation.value_y = lCtrl.Top;
                            #endregion 晁  修改
                            break;

                        case EnumMousePointPosition.MouseSizeBottom://实现向下拖拽边框
                            lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                            p1.X = e.X;
                            p1.Y = e.Y; //'记录光标拖动的当前点
                            break;
                        case EnumMousePointPosition.MouseSizeBottomRight:
                            lCtrl.Width = lCtrl.Width + e.X - p1.X;
                            lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                            p1.X = e.X;
                            p1.Y = e.Y; //'记录光标拖动的当前点
                            break;
                        case EnumMousePointPosition.MouseSizeRight:
                            lCtrl.Width = lCtrl.Width + e.X - p1.X;
                            //       lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                            p1.X = e.X;
                            p1.Y = e.Y; //'记录光标拖动的当前点
                            break;
                        case EnumMousePointPosition.MouseSizeTop:
                            lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                            lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                            break;
                        case EnumMousePointPosition.MouseSizeLeft:
                            lCtrl.Left = lCtrl.Left + e.X - p.X;
                            lCtrl.Width = lCtrl.Width - (e.X - p.X);
                            break;
                        case EnumMousePointPosition.MouseSizeBottomLeft:
                            lCtrl.Left = lCtrl.Left + e.X - p.X;
                            lCtrl.Width = lCtrl.Width - (e.X - p.X);
                            lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                            p1.X = e.X;
                            p1.Y = e.Y; //'记录光标拖动的当前点
                            break;
                        case EnumMousePointPosition.MouseSizeTopRight:
                            lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                            lCtrl.Width = lCtrl.Width + (e.X - p1.X);
                            lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                            p1.X = e.X;
                            p1.Y = e.Y; //'记录光标拖动的当前点
                            break;
                        case EnumMousePointPosition.MouseSizeTopLeft:
                            lCtrl.Left = lCtrl.Left + e.X - p.X;
                            lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                            lCtrl.Width = lCtrl.Width - (e.X - p.X);
                            lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                            break;
                        default:
                            break;
                    }
                    if (lCtrl.Width < MinWidth) lCtrl.Width = MinWidth;
                    if (lCtrl.Height < MinHeight) lCtrl.Height = MinHeight;
                }
                else
                {
                    ChangeCurse(sender, e);
                }
            }
            catch
            {
                return;
            }
        }

        public static void MyMouseDown(Object sender, MouseEventArgs e)
        {
            p.X = e.X;
            p.Y = e.Y;
            p1.X = e.X;
            p1.Y = e.Y;
        }  //为三端元图所写

        public static void MyMouseMove(Object sender, MouseEventArgs e)
        {
            Control lCtrl = (Control)sender;
            if (e.Button == MouseButtons.Left)
            {
                switch (m_MousePointPosition)
                {
                    case EnumMousePointPosition.MouseDrag:
                        //使特殊图不能移动到左边和上边
                        if ((p.X - e.X) > lCtrl.Left)
                        { lCtrl.Left = 0; }
                        else
                        {
                            lCtrl.Left = lCtrl.Left + e.X - p.X;
                        }
                        if ((p.Y - e.Y) > lCtrl.Top)
                        { lCtrl.Top = 0; }
                        else
                        {
                            lCtrl.Top = lCtrl.Top + e.Y - p.Y;
                        }
                        break;//这个case实现picturebox的移动

                    case EnumMousePointPosition.MouseSizeBottom://实现向下拖拽边框
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点
                        break;
                    case EnumMousePointPosition.MouseSizeBottomRight:
                        lCtrl.Width = lCtrl.Width + e.X - p1.X;
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点
                        break;
                    case EnumMousePointPosition.MouseSizeRight:
                        lCtrl.Width = lCtrl.Width + e.X - p1.X;
                        //       lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点
                        break;
                    case EnumMousePointPosition.MouseSizeTop:
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        break;
                    case EnumMousePointPosition.MouseSizeLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        break;
                    case EnumMousePointPosition.MouseSizeBottomLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点
                        break;
                    case EnumMousePointPosition.MouseSizeTopRight:
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Width = lCtrl.Width + (e.X - p1.X);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点
                        break;
                    case EnumMousePointPosition.MouseSizeTopLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        break;
                    default:
                        break;
                }
                if (lCtrl.Width < MinWidth) lCtrl.Width = MinWidth;
                if (lCtrl.Height < MinHeight) lCtrl.Height = MinHeight;
            }
            else
            {
                ChangeCurse(sender, e);
            }
        }//为 了除三端元以外的图写的和MyMouseDown达到移动控件改变控件大小的功能

        public static void MyMouseMove2(Object sender, MouseEventArgs e)//给TextBox放大缩小专用的
        {

            Control lCtrl = (Control)sender;
            if (e.Button == MouseButtons.Left)
            {
                switch (m_MousePointPosition)
                {
                    case EnumMousePointPosition.MouseDrag:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Top = lCtrl.Top + e.Y - p.Y;
                        break;//这个case实现picturebox的移动

                    case EnumMousePointPosition.MouseSizeBottom://实现向下拖拽边框
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点
                        break;
                    case EnumMousePointPosition.MouseSizeBottomRight:
                        lCtrl.Width = lCtrl.Width + e.X - p1.X;
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点
                        break;
                    case EnumMousePointPosition.MouseSizeRight:
                        lCtrl.Width = lCtrl.Width + e.X - p1.X;
                        //       lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点
                        break;
                    case EnumMousePointPosition.MouseSizeTop:
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        break;
                    case EnumMousePointPosition.MouseSizeLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        break;
                    case EnumMousePointPosition.MouseSizeBottomLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点
                        break;
                    case EnumMousePointPosition.MouseSizeTopRight:
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Width = lCtrl.Width + (e.X - p1.X);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点
                        break;
                    case EnumMousePointPosition.MouseSizeTopLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        break;
                    default:
                        break;
                }
             
            }
            else
            {
                ChangeCurse2(sender, e);
            }
        }

      public static void MySanDuanYuanMouseMove(Object sender, MouseEventArgs e)//只可以同时放大长和宽，不可以只放大长或者只放大宽
        {
            Control lCtrl = (Control)sender;
            if (e.Button == MouseButtons.Left)
            {
                //使三段元图不能移动到左边和上边
                if ((p.X - e.X) > lCtrl.Left)
                { lCtrl.Left = 0; }
                else
                {
                    lCtrl.Left = lCtrl.Left + e.X - p.X;
                }
                if ((p.Y - e.Y) > lCtrl.Top)
                { lCtrl.Top = 0; }
                else
                {
                    lCtrl.Top = lCtrl.Top + e.Y - p.Y;
                } 
            }
            else
            {
                lCtrl.Cursor = Cursors.Hand;      //'四方向
            }
        }

        public static void ChangeCurse(object sender, MouseEventArgs e)
        {
            Control lCtrl = (sender as Control);
            m_MousePointPosition = MousePointPosition(lCtrl.Size, e);   //'判断光标的位置状态lCtrl.Size就是picturebox的大小
            switch (m_MousePointPosition)   //'改变光标
            {
                case EnumMousePointPosition.MouseSizeNone:
                    lCtrl.Cursor = Cursors.Arrow;        //'箭头
                    break;
                case EnumMousePointPosition.MouseDrag:
                    lCtrl.Cursor = Cursors.Hand;      //'四方向
                    break;
                case EnumMousePointPosition.MouseSizeBottom://底
                    lCtrl.Cursor = Cursors.SizeNS;       //'南北
                    break;
                case EnumMousePointPosition.MouseSizeTop://顶
                    lCtrl.Cursor = Cursors.SizeNS;       //'南北
                    break;
                case EnumMousePointPosition.MouseSizeLeft://左边
                    lCtrl.Cursor = Cursors.SizeWE;       //'东西
                    break;
                case EnumMousePointPosition.MouseSizeRight://右边
                    lCtrl.Cursor = Cursors.SizeWE;       //'东西
                    break;
                case EnumMousePointPosition.MouseSizeBottomLeft://左边底部
                    lCtrl.Cursor = Cursors.SizeNESW;     //'东北到南西
                    break;
                case EnumMousePointPosition.MouseSizeBottomRight://右边底部
                    lCtrl.Cursor = Cursors.SizeNWSE;     //'东南到西北
                    break;
                case EnumMousePointPosition.MouseSizeTopLeft://左上
                    lCtrl.Cursor = Cursors.SizeNWSE;     //'东南到西北
                    break;
                case EnumMousePointPosition.MouseSizeTopRight://右上
                    lCtrl.Cursor = Cursors.SizeNESW;     //'东北到南西
                    break;
                default:
                    break;
            }
        }

        public static void ChangeCurse2(object sender, MouseEventArgs e)
        {
            Control lCtrl = (sender as Control);
            m_MousePointPosition = MousePointPosition(lCtrl.Size, e);   //'判断光标的位置状态lCtrl.Size就是picturebox的大小
            switch (m_MousePointPosition)   //'改变光标
            {
                case EnumMousePointPosition.MouseSizeNone:
                    lCtrl.Cursor = Cursors.Arrow;        //'箭头
                    break;
                case EnumMousePointPosition.MouseDrag:
                    lCtrl.Cursor = Cursors.SizeAll;      //'四方向
                    break;
                case EnumMousePointPosition.MouseSizeBottom://底
                    lCtrl.Cursor = Cursors.SizeNS;       //'南北
                    break;
                case EnumMousePointPosition.MouseSizeTop://顶
                    lCtrl.Cursor = Cursors.SizeNS;       //'南北
                    break;
                case EnumMousePointPosition.MouseSizeLeft://左边
                    lCtrl.Cursor = Cursors.SizeWE;       //'东西
                    break;
                case EnumMousePointPosition.MouseSizeRight://右边
                    lCtrl.Cursor = Cursors.SizeWE;       //'东西
                    break;
                case EnumMousePointPosition.MouseSizeBottomLeft://左边底部
                    lCtrl.Cursor = Cursors.SizeNESW;     //'东北到南西
                    break;
                case EnumMousePointPosition.MouseSizeBottomRight://右边底部
                    lCtrl.Cursor = Cursors.SizeNWSE;     //'东南到西北
                    break;
                case EnumMousePointPosition.MouseSizeTopLeft://左上
                    lCtrl.Cursor = Cursors.SizeNWSE;     //'东南到西北
                    break;
                case EnumMousePointPosition.MouseSizeTopRight://右上
                    lCtrl.Cursor = Cursors.SizeNESW;     //'东北到南西
                    break;
                default:
                    break;
            }
        }

        private static EnumMousePointPosition MousePointPosition(Size size, System.Windows.Forms.MouseEventArgs e)
        {


            if (e.X < Band)//左边整个区域，包括左上，左下，左边
            {
                if (e.Y < Band) //左上
                {
                    return EnumMousePointPosition.MouseSizeTopLeft;
                }
                else if (e.Y > -1 * Band + size.Height) //左下
                {

                    return EnumMousePointPosition.MouseSizeBottomLeft;
                }
                else//左边
                {
                    return EnumMousePointPosition.MouseSizeLeft;
                }

            }//end左边





            else if (e.X > -1 * Band + size.Width) //右边整个区域
            {
                if (e.Y < Band)//右上角
                {
                    return EnumMousePointPosition.MouseSizeTopRight;
                }
                else if (e.Y > -1 * Band + size.Height)
                {

                    return EnumMousePointPosition.MouseSizeBottomRight;//右下角
                }
                else//右边
                {
                    return EnumMousePointPosition.MouseSizeRight;
                }
            }


            else//上面或者下面
            {
                if (e.Y < Band)//上
                {
                    return EnumMousePointPosition.MouseSizeTop;
                }
                else if (e.Y > -1 * Band + size.Height)//下
                {

                    return EnumMousePointPosition.MouseSizeBottom;


                }
                else //最中间
                {
                    return EnumMousePointPosition.MouseDrag;
                }
            }
            //

        }

    }
}
