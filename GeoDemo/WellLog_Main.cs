using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace GeoDemo
{
    public delegate void drawred(int m,int n);
    public delegate void updata();
    public delegate void paintSelfrefresh();
    public partial class WellLog_Main : Form
    {
        #region 肖宇博


        public Font MyFont = SysData.title_font;
        public Color MyColor = SysData.title_color;
        public string MyText = "主图";
        public Color MyLineColor1 = SysData.line_color;
        public Color MyLineColor2 = SysData.line_color2;
        public int MyLine1 = SysData.line1;
        public int MyLine2 = SysData.line2;
        public Color MyXBrush = Color.Black;
        public Color MyYBrush = Color.Black;
        public Font MyXFont = new Font("宋体", 8, FontStyle.Regular);
        public Font MyYFont = new Font("宋体", 8, FontStyle.Regular);
        //轴名
        public Font MyXnameFont = new Font("宋体", 12, FontStyle.Regular);
        public Color MyXnameColor = Color.Black;
        public string MyXnameText = "X轴";

        public Font MyYnameFont = new Font("宋体", 12, FontStyle.Regular);
        public Color MyYnameColor = Color.Black;
        public string MyYnameText = "Y轴";

        //判断垂直
        public bool MyXCheck = false;
        public bool MyYCheck = false;

        #endregion 
        public WellLog_Main()
        {
            InitializeComponent();
        }
        public event drawred DrawRedline;
        public event updata Update_draw;
        public event paintSelfrefresh Selfrefresh;

        float mul, add = 0;//拟合系数a，b值

        int starti = 0;
        int endi = 0;
        float kX = 0;//兑换屏幕坐标的比例
        float kY = 0;//同上
        List<float> tempdtX_F = new List<float>();//用来排序
        List<float> tempdtY_F = new List<float>();
        bool button_done = true;//判断点击按钮
        List<PointF> Area_point = new List<PointF>();//区域点集
        

        private void WellLog_Main_Paint(object sender, PaintEventArgs e)
        { 
            if (SysData.dt != null)
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            if (SysData.IsDouble)
            {
                label1.Font = MyFont;
                label1.ForeColor = MyColor;
            }
            else
            {
                label1.Font = SysData.title_font;
                label1.ForeColor = SysData.title_color;
            }
            label1.Text = MyText;
            Pen p = new Pen(MyLineColor1, MyLine1);
            Pen Pw = new Pen(MyLineColor2, MyLine2);
            Font myXfont = MyXFont;
            Font myYfont = MyYFont;
            Color myXcolor = MyXBrush;
            Color myYcolor = MyYBrush;

            //this.Invalidate();//清除已绘制的
            //this.Update();//重新绘制

            Graphics g = this.CreateGraphics();


            #region 画网格和横纵轴
            //0116////确定四个边框的点
            PointF P1 = new PointF(60, 20);
            PointF P2 = new PointF(60, 460);
            PointF P3 = new PointF(500, 460);
            PointF P4 = new PointF(500, 20);

            //
            
            for (int i = 0; i < 12; i++)
            {
                //0116// g.DrawLine(p, 60, 20 + 40 * i, 500, 20 + 40 * i);//横坐标范围60—500
                //0116// g.DrawLine(p, 60 + i * 40, 20, 60 + i * 40, 460);//纵坐标范围20—460
                g.DrawLine(p, P1.X, P1.Y + (P2.Y - P1.Y) / 11 * i, P4.X, P1.Y + (P2.Y - P1.Y) / 11 * i);//横坐标范围60—500
                g.DrawLine(p, P1.X + (P4.X - P1.X) / 11 * i, P1.Y, P1.X + (P4.X - P1.X) / 11 * i, P2.Y);//纵坐标范围20—460
            }
            g.DrawLine(Pw, P1,P2);
            g.DrawLine(Pw, P2,P3);
            #endregion


            #region 画数据点
 
            if (SysData.dt != null)
            {
                ///////////////////////////////////////////////////////////////////////根据附图处理数据

                if (SysData.textbox1 == string.Empty)
                {
                    for (int i = 1; i < SysData.dt.Rows.Count; i++)
                    {
                        if (Convert.ToSingle(SysData.dt.Rows[i][1]) > -5000 && Convert.ToSingle(SysData.dt.Rows[i][2]) > -5000)
                        {
                            //tempdtX_F.Add(Convert.ToSingle(SysData.dt.Rows[i][1]));
                            //tempdtY_F.Add(Convert.ToSingle(SysData.dt.Rows[i][2]));
                            starti = i;
                            break;
                        }

                    }
                    for (int i = starti; i < SysData.dt.Rows.Count; i++)
                    {
                        if (Convert.ToSingle(SysData.dt.Rows[i][0]) == (Convert.ToSingle(SysData.dt.Rows[starti][0]) + 40))//40指深度范围，如需改动可换为变量
                        {
                            endi = i;
                            break;
                        }
                    }
                    for (int i = starti; i < endi + 1; i++)
                    {
                        tempdtX_F.Add(Convert.ToSingle(SysData.dt.Rows[i][1]));
                        tempdtY_F.Add(Convert.ToSingle(SysData.dt.Rows[i][2]));
                    }
                    tempdtX_F.Sort();
                    tempdtY_F.Sort();
                    //0116//kX = 440 / (tempdtX_F[tempdtX_F.Count - 1] - tempdtX_F[0]);
                    //0116//kY = 440 / (tempdtY_F[tempdtY_F.Count - 1] - tempdtY_F[0]);
                    kX = (P4.X-P1.X) / (tempdtX_F[tempdtX_F.Count - 1] - tempdtX_F[0]);
                    kY = (P2.Y-P1.Y) / (tempdtY_F[tempdtY_F.Count - 1] - tempdtY_F[0]);
                }
                else
                {
                    for (int i = 1; i < SysData.dt.Rows.Count; i++)
                    {
                        if (Convert.ToSingle(SysData.dt.Rows[i][0]) == Convert.ToSingle(SysData.textbox1))
                        {

                            starti = i;
                            break;
                        }

                    }
                    for (int i = starti; i < SysData.dt.Rows.Count; i++)
                    {
                        if (Convert.ToSingle(SysData.dt.Rows[i][0]) == (Convert.ToSingle(SysData.dt.Rows[starti][0]) + Convert.ToSingle(SysData.textbox2)))
                        {
                            endi = i;
                            break;
                        }
                    }
                    for (int i = starti; i < endi + 1; i++)
                    {
                        tempdtX_F.Add(Convert.ToSingle(SysData.dt.Rows[i][1]));
                        tempdtY_F.Add(Convert.ToSingle(SysData.dt.Rows[i][2]));
                    }
                    tempdtX_F.Sort();
                    tempdtY_F.Sort();
                    //kX = 440 / (tempdtX_F[tempdtX_F.Count - 1] - tempdtX_F[0]);
                    //kY = 440 / (tempdtY_F[tempdtY_F.Count - 1] - tempdtY_F[0]);
                    kX = (P4.X - P1.X) / (tempdtX_F[tempdtX_F.Count - 1] - tempdtX_F[0]);
                    kY = (P2.Y - P1.Y) / (tempdtY_F[tempdtY_F.Count - 1] - tempdtY_F[0]);
                }

                //////////////////////////////////////////////////////////////////////////

                //for (int i = 1; i < SysData.dt.Rows.Count; i++)
                //{
                //    if (Convert.ToSingle(SysData.dt.Rows[i][1]) > 0 && Convert.ToSingle(SysData.dt.Rows[i][2]) > 0)//要保证横纵坐标是对应的（成对的坐标）
                //    {
                //        g.FillRectangle(new SolidBrush(Color.Black), 60 + (Convert.ToSingle(SysData.dt.Rows[i][1]) - tempdtX[0]) * kX, 520 - (60 + (Convert.ToSingle(SysData.dt.Rows[i][2]) - tempdtY[0]) * kY), 2, 2);
                //    }
                //}
                for (int i = starti; i < endi + 1; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.Black), P1.X + (Convert.ToSingle(SysData.dt.Rows[i][1]) - tempdtX_F[0]) * kX, P2.Y - (Convert.ToSingle(SysData.dt.Rows[i][2]) - tempdtY_F[0]) * kY, 2, 2);

                }

                #region 根据数据点计算mul和add的值

               
                //y = ax + b计算a与b
                
                float xmean = 0, ymean = 0; //x与y的平均值
                int number = 0;
                bool succeed;
                for (int i = 0; i < tempdtX_F.Count;i++ )
                {
                    float tempx=tempdtX_F[i];
                    float tempy=tempdtY_F[i];
                   

                    tempx = GetYFitValue(tempx, out succeed);
                    if (!succeed)
                    {
                        continue;
                    }
                    xmean += tempx;
                    ymean += tempy;
                    number++;
                }

                if (number <= 1)
                {
                    this.add = 0;
                    this.mul = 0;
                    return;
                }

                xmean /= number;
                ymean /= number;

                float sumxy = 0, sumxx = 0, sumyy = 0;
                for (int i = 0; i < tempdtX_F.Count; i++)
                {
                    float valx = tempdtX_F[i];
                    float valy = tempdtY_F[i];
                    valx = GetYFitValue(valx, out succeed);
                    if (!succeed)
                    {
                        continue;
                    }
                    sumxx += (valx - xmean) * (valx - xmean);
                    sumxy += (valx - xmean) * (valy - ymean);
                    sumyy += (valy - ymean) * (valy - ymean);
                }

                mul = sumxy / sumxx;
                add = ymean - mul * xmean;
                double q = 0; //偏差平方和
                double u = 0; //偏差平均值

                for (int i = 0; i < tempdtX_F.Count; i++)
                {
                    float valx = tempdtX_F[i];
                    float valy = tempdtY_F[i];
                  
                    valx = GetYFitValue(valx, out succeed);
                    if (!succeed)
                    {
                        continue;
                    }
                    q += (valy - mul * valx - add) * (valy - mul * valx - add);
                    u += (valy - mul * valx - add);
                }
                double s = Math.Sqrt(q / number); //平均标准偏差
                double r = Math.Sqrt(1 - q / sumyy);   //相关系数
                u /= number;    //偏差平均值
               // SetResultStr(r, s);

                #endregion


                //标数据刻度

                for (int i = 0; i < 12; i++)
                {
                    g.DrawString((tempdtX_F[0] + ((tempdtX_F[tempdtX_F.Count - 1] - tempdtX_F[0]) / 11) * i).ToString("F3"), myXfont, new SolidBrush(myXcolor), P2.X - 15 + (P3.X - P2.X) / 11 * i, P2.Y + 10);
                    g.DrawString((tempdtY_F[0] + ((tempdtY_F[tempdtY_F.Count - 1] - tempdtY_F[0]) / 11) * i).ToString("F3"), myYfont, new SolidBrush(myYcolor), P1.X - 40, P2.Y - (P2.Y - P1.Y) / 11 * i);
                }

                #region 根据mul和add绘制拟合曲线
                if (SysData.IsDrawLine)
                {
                    try
                    {
                        switch (SysData.comboxselected)
                        {
                            case 0:
                                {
                                    PointF a = new PointF(P1.X, P2.Y - (mul * (tempdtX_F[0] - tempdtX_F[0]) + add) * kY);
                                    PointF b = new PointF(P1.X + (tempdtX_F[tempdtX_F.Count - 1] - tempdtX_F[0]) * kX, P2.Y - ((mul * tempdtX_F[tempdtX_F.Count - 1] + add) - tempdtY_F[0]) * kY);
                                    g.DrawLine(new Pen(Color.Blue, 2), a, b);
                                    break;
                                }
                            case 1:
                                {
                                    PointF[] a = new PointF[12];
                                    for (int i = 0; i < 12; i++)
                                    {
                                        a[i].X = P1.X + ((tempdtX_F[tempdtX_F.Count - 1] - tempdtX_F[0]) / 11) * i * kX;
                                        a[i].Y = P2.Y - ((mul * Convert.ToSingle(Math.Log10(tempdtX_F[0] + ((tempdtX_F[tempdtX_F.Count - 1] - tempdtX_F[0]) / 11) * i)) + add) - tempdtY_F[0]) * kY;
                                    }
                                    g.DrawCurve(new Pen(Color.Blue, 2), a);
                                    break;
                                }
                            case 2:
                                {
                                    PointF[] a = new PointF[12];
                                    for (int i = 0; i < 12; i++)
                                    {
                                        a[i].X = P1.X + ((tempdtX_F[tempdtX_F.Count - 1] - tempdtX_F[0]) / 11) * i * kX;
                                        a[i].Y = P2.Y - ((mul * Convert.ToSingle(Math.Exp(tempdtX_F[0] + ((tempdtX_F[tempdtX_F.Count - 1] - tempdtX_F[0]) / 11) * i)) + add) - tempdtY_F[0]) * kY;
                                    }
                                    g.DrawCurve(new Pen(Color.Blue, 2), a);
                                    break;
                                }
                            case 3:
                                {
                                    PointF[] a = new PointF[12];
                                    for (int i = 0; i < 12; i++)
                                    {
                                        a[i].X = P1.X + ((tempdtX_F[tempdtX_F.Count - 1] - tempdtX_F[0]) / 11) * i * kX;
                                        a[i].Y = P2.Y - ((mul / Convert.ToSingle(Math.Pow((tempdtX_F[0] + ((tempdtX_F[tempdtX_F.Count - 1] - tempdtX_F[0]) / 11) * i), 2)) + add) - tempdtY_F[0]) * kY;
                                    }
                                    g.DrawCurve(new Pen(Color.Blue, 2), a);
                                    break;
                                }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("数据溢出,请仔细核对", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                  
                }

                if (SysData.IsDrawWords)
                {
                    g.DrawString("平均标准差:", MyXnameFont, new SolidBrush(MyColor), P4.X + 10, P4.Y + 50);
                    g.DrawString(s.ToString("F6"), MyXnameFont, new SolidBrush(MyColor), P4.X + 10, P4.Y + 70);
                    g.DrawString("相关系数:", MyXnameFont, new SolidBrush(MyColor), P4.X + 10, P4.Y + 100);
                    g.DrawString(r.ToString("F6"), MyXnameFont, new SolidBrush(MyColor), P4.X + 10, P4.Y + 120);
                }
                #endregion

            }

            #endregion

            if (Area_point.Count > 0)
            {
                PointF[] DrawArea = new PointF[Area_point.Count];
                for (int i = 0; i < Area_point.Count; i++)
                {
                    DrawArea[i] = Area_point[i];
                }
                GraphicsPath ztpath = new GraphicsPath();//表示一系列的线围成的图
                ztpath.AddPolygon(DrawArea);//向该路径添加z多边形
                Region ztregion = new Region(ztpath);//创建每个多边形的区域
                g.DrawPath(new Pen(Color.Green, 2), ztpath);

            }
            #region 绘制xy轴字
            //x
            g.TranslateTransform(this.Width / 3, this.Height - 20);
            if (MyXCheck)
            {
                g.RotateTransform(90);
            }
            g.DrawString(MyXnameText, MyXnameFont, new SolidBrush(MyXnameColor), 0, 0);
            //y
            g.ResetTransform();
            g.TranslateTransform(5, this.Height / 2);
            if (MyYCheck)
            {
                g.RotateTransform(90);
            }
            g.DrawString(MyYnameText, MyYnameFont, new SolidBrush(MyYnameColor), 0, 0);
            g.ResetTransform();
            
            #endregion

            Save_printImage();
        }

        #region 绘制拟合曲线

        /// <summary>
        /// 开始拟合计算
        /// </summary>
        //public override void StartFit()
        //{
        //    Curve xCurve = log2DCrossInfo.XCurve;
        //    Curve yCurve = log2DCrossInfo.YCurve;
        //    if (log2DCrossInfo.IsZPlot || xCurve == null || yCurve == null) //是Z值图 或X曲线或Y曲线不存在
        //    {
        //        return;
        //    }

        //    xmax = (log2DCrossInfo.XRight > log2DCrossInfo.XLeft) ? log2DCrossInfo.XRight : log2DCrossInfo.XLeft;
        //    xmin = (log2DCrossInfo.XRight < log2DCrossInfo.XLeft) ? log2DCrossInfo.XRight : log2DCrossInfo.XLeft;
        //    ymax = (log2DCrossInfo.YUp > log2DCrossInfo.YDown) ? log2DCrossInfo.YUp : log2DCrossInfo.YDown;
        //    ymin = (log2DCrossInfo.YUp < log2DCrossInfo.YDown) ? log2DCrossInfo.YUp : log2DCrossInfo.YDown;

        //    double sdep = log2DCrossInfo.Sdep;
        //    double edep = log2DCrossInfo.Edep;
        //    //y = ax + b计算a与b
        //    double rlev = xCurve.Rlev > yCurve.Rlev ? xCurve.Rlev : yCurve.Rlev;
        //    double xmean = 0, ymean = 0; //x与y的平均值
        //    int number = 0;
        //    bool succeed;
        //    for (double dep = sdep; dep < edep; dep += rlev)
        //    {
        //        double valx = xCurve.GetValue(dep);
        //        double valy = yCurve.GetValue(dep);
        //        if (valx < xmin || valx > xmax)
        //        {
        //            continue;
        //        }
        //        if (valy < ymin || valy > ymax)
        //        {
        //            continue;
        //        }

        //        valx = GetYFitValue(valx, out succeed);
        //        if (!succeed)
        //        {
        //            continue;
        //        }
        //        xmean += valx;
        //        ymean += valy;
        //        number++;
        //    }

        //    if (number <= 1)
        //    {
        //        this.add = 0;
        //        this.mul = 0;
        //        return;
        //    }

        //    xmean /= number;
        //    ymean /= number;

        //    double sumxy = 0, sumxx = 0, sumyy = 0;
        //    for (double dep = sdep; dep < edep; dep += rlev)
        //    {
        //        double valx = xCurve.GetValue(dep);
        //        double valy = yCurve.GetValue(dep);
        //        if (valx < xmin || valx > xmax)
        //        {
        //            continue;
        //        }
        //        if (valy < ymin || valy > ymax)
        //        {
        //            continue;
        //        }
        //        valx = GetYFitValue(valx, out succeed);
        //        if (!succeed)
        //        {
        //            continue;
        //        }
        //        sumxx += (valx - xmean) * (valx - xmean);
        //        sumxy += (valx - xmean) * (valy - ymean);
        //        sumyy += (valy - ymean) * (valy - ymean);
        //    }

        //    mul = sumxy / sumxx;
        //    add = ymean - mul * xmean;
        //    double q = 0; //偏差平方和
        //    double u = 0; //偏差平均值

        //    for (double dep = sdep; dep < edep; dep += rlev)
        //    {
        //        double valx = xCurve.GetValue(dep);
        //        double valy = yCurve.GetValue(dep);
        //        if (valx < xmin || valx > xmax)
        //        {
        //            continue;
        //        }
        //        if (valy < ymin || valy > ymax)
        //        {
        //            continue;
        //        }
        //        valx = GetYFitValue(valx, out succeed);
        //        if (!succeed)
        //        {
        //            continue;
        //        }
        //        q += (valy - mul * valx - add) * (valy - mul * valx - add);
        //        u += (valy - mul * valx - add);
        //    }
        //    double s = Math.Sqrt(q / number); //平均标准偏差
        //    double r = Math.Sqrt(1 - q / sumyy);   //相关系数
        //    u /= number;    //偏差平均值
        //    SetResultStr(r, s);
        //}


        /// <summary>
        /// 根据X的值采用目标函数计算Y的值
        /// </summary>
        /// <param name="xCurveVal"></param>
        /// <param name="succeed"></param>
        /// <returns></returns>
        private float GetYFitValue(float xCurveVal, out bool succeed)
        {
            //拟合方程标志:0、 用 "Y = a * X + b"； 1、"Y = a * Log10(X) + b"； 2、"Y = a * Exp(X) + b"；3、"Y = a / Pow(X, power) + b		
            if (SysData.comboxselected == 0)
            {
                succeed = true;
                return xCurveVal;
            }
            else if (SysData.comboxselected == 1)
            {
                succeed = (xCurveVal > 0);
                if (succeed)
                {
                    return Convert.ToSingle(Math.Log10(xCurveVal));
                }
                return xCurveVal;
            }
            else if (SysData.comboxselected == 2)
            {
                succeed = true;
                return Convert.ToSingle(Math.Exp(xCurveVal));
            }
            else
            {
                succeed = true;
                return Convert.ToSingle(Math.Pow(xCurveVal, 2));
            }
        }
        #endregion

    #region panel控件上绘制的图
        /*
        public void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (SysData.dt != null)
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            if (SysData.IsDouble)
            {
                label1.Font = MyFont;
                label1.ForeColor = MyColor;

            }
            else
            {
                label1.Font = SysData.title_font;
                label1.ForeColor = SysData.title_color;
            }
            label1.Text = MyText;
            Pen p = new Pen(MyLineColor1, MyLine1);
            Pen Pw = new Pen(MyLineColor2, MyLine2);
            Font myXfont = MyXFont;
            Font myYfont = MyYFont;
            Color myXcolor = MyXBrush;
            Color myYcolor = MyYBrush;
           
            //this.Invalidate();//清除已绘制的
            //this.Update();//重新绘制
            using (Graphics g = this.panel1.CreateGraphics())
            {
                


                #region 画网格和横纵轴
                for (int i = 0; i < 12; i++)
                {
                    g.DrawLine(p, 60, 20 + 40 * i, 500, 20 + 40 * i);//横坐标范围60—500
                    g.DrawLine(p, 60 + i * 40, 20, 60 + i * 40, 460);//纵坐标范围20—460
                }
                g.DrawLine(Pw, 60, 20, 60, 460);
                g.DrawLine(Pw, 60, 460, 500, 460);
                #endregion




                #region 画数据点

             
                if(SysData.dt!=null)
                {

                   



                    ///////////////////////////////////////////////////////////////////////根据附图处理数据

                    
                    
                    if (SysData.textbox == string.Empty)
                    {
                        for (int i = 1; i < SysData.dt.Rows.Count; i++)
                        {
                            if (Convert.ToSingle(SysData.dt.Rows[i][1]) > 0 && Convert.ToSingle(SysData.dt.Rows[i][2]) > 0)
                            {
                                //tempdtX_F.Add(Convert.ToSingle(SysData.dt.Rows[i][1]));
                                //tempdtY_F.Add(Convert.ToSingle(SysData.dt.Rows[i][2]));
                                starti = i;
                                break;
                            }

                        }
                        for (int i = starti; i < SysData.dt.Rows.Count; i++)
                        {
                            if (Convert.ToSingle(SysData.dt.Rows[i][0]) == (Convert.ToSingle(SysData.dt.Rows[starti][0]) + 40))
                            {
                                endi = i;
                                break;
                            }
                        }
                        for (int i = starti; i < endi + 1; i++)
                        {
                            tempdtX_F.Add(Convert.ToSingle(SysData.dt.Rows[i][1]));
                            tempdtY_F.Add(Convert.ToSingle(SysData.dt.Rows[i][2]));
                        }
                        tempdtX_F.Sort();
                        tempdtY_F.Sort();
                        kX = 440 / (tempdtX_F[tempdtX_F.Count - 1] - tempdtX_F[0]);
                        kY = 440 / (tempdtY_F[tempdtY_F.Count - 1] - tempdtY_F[0]);

                    }
                    else
                    {
                        for (int i = 1; i < SysData.dt.Rows.Count; i++)
                        {
                            if (Convert.ToSingle(SysData.dt.Rows[i][0]) == Convert.ToSingle(SysData.textbox))
                            {

                                starti = i;
                                break;
                            }

                        }
                        for (int i = starti; i < SysData.dt.Rows.Count; i++)
                        {
                            if (Convert.ToSingle(SysData.dt.Rows[i][0]) == (Convert.ToSingle(SysData.dt.Rows[starti][0]) + 40))
                            {
                                endi = i;
                                break;
                            }
                        }
                        for (int i = starti; i < endi + 1; i++)
                        {
                            tempdtX_F.Add(Convert.ToSingle(SysData.dt.Rows[i][1]));
                            tempdtY_F.Add(Convert.ToSingle(SysData.dt.Rows[i][2]));
                        }
                        tempdtX_F.Sort();
                        tempdtY_F.Sort();
                        kX = 440 / (tempdtX_F[tempdtX_F.Count - 1] - tempdtX_F[0]);
                        kY = 440 / (tempdtY_F[tempdtY_F.Count - 1] - tempdtY_F[0]);
                    }

                    //////////////////////////////////////////////////////////////////////////

                    //for (int i = 1; i < SysData.dt.Rows.Count; i++)
                    //{
                    //    if (Convert.ToSingle(SysData.dt.Rows[i][1]) > 0 && Convert.ToSingle(SysData.dt.Rows[i][2]) > 0)//要保证横纵坐标是对应的（成对的坐标）
                    //    {
                    //        g.FillRectangle(new SolidBrush(Color.Black), 60 + (Convert.ToSingle(SysData.dt.Rows[i][1]) - tempdtX[0]) * kX, 520 - (60 + (Convert.ToSingle(SysData.dt.Rows[i][2]) - tempdtY[0]) * kY), 2, 2);
                    //    }
                    //}
                    for (int i = starti; i < endi + 1; i++)
                    {

                        g.FillRectangle(new SolidBrush(Color.Black), 60 + (Convert.ToSingle(SysData.dt.Rows[i][1]) - tempdtX_F[0]) * kX, 520 - (60 + (Convert.ToSingle(SysData.dt.Rows[i][2]) - tempdtY_F[0]) * kY), 2, 2);

                    }




                    //标数据刻度
                   
                    for (int i = 0; i < 12; i++)
                    {
                        g.DrawString((tempdtX_F[0] + ((tempdtX_F[tempdtX_F.Count - 1] - tempdtX_F[0]) / 11) * i).ToString("F3"), myXfont, new SolidBrush(myXcolor), 45 + 40 * i, 470);
                        g.DrawString((tempdtY_F[0] + ((tempdtY_F[tempdtY_F.Count - 1] - tempdtY_F[0]) / 11) * i).ToString("F3"), myYfont, new SolidBrush(myYcolor), 20, 457 - 40 * i);
                    }
                }

                #endregion



                if (Area_point.Count > 0)
                {
                    PointF[] DrawArea = new PointF[Area_point.Count];
                    for (int i = 0; i < Area_point.Count; i++)
                    {
                        DrawArea[i] = Area_point[i];
                    }
                    GraphicsPath ztpath = new GraphicsPath();//表示一系列的线围成的图
                    ztpath.AddPolygon(DrawArea);//向该路径添加z多边形
                    Region ztregion = new Region(ztpath);//创建每个多边形的区域
                    g.DrawPath(new Pen(Color.Green, 2), ztpath);

                }
                #region 绘制xy轴字
                //x
                g.TranslateTransform(this.panel1.Width / 2, this.Height - 20);
                if (MyXCheck)
                {
                    g.RotateTransform(90);
                }
                g.DrawString(MyXnameText, MyXnameFont, new SolidBrush(MyXnameColor), 0, 0);
                //y
                g.ResetTransform();
                g.TranslateTransform(5, this.Height / 2);
                if (MyYCheck)
                {
                    g.RotateTransform(90);
                }
                g.DrawString(MyYnameText, MyYnameFont, new SolidBrush(MyYnameColor), 0, 0);
                g.ResetTransform();
                #endregion
            }
            Save_printImage();
        }
        */
    #endregion
        public void Panel_refresh()
        {
            this.Refresh();
        }

         private void button1_Click(object sender, EventArgs e)
         {
             
                 SysData.button_cancel = false;
                 if (button_done)
                 {
                     this.MouseClick += new MouseEventHandler(wellmain_MouseClick);
                     button_done = false;
                     button1.Text = "停止";
                 }
                 else
                 {
                     this.MouseClick -= new MouseEventHandler(wellmain_MouseClick);
                     button_done = true;

                     button1.Text = "点击绘图";

                     PointF[] DrawArea = new PointF[Area_point.Count];
                     for (int i = 0; i < Area_point.Count; i++)
                     {
                         DrawArea[i] = Area_point[i];
                     }



                     GraphicsPath ztpath = new GraphicsPath();//表示一系列的线围成的图
                     if (Area_point.Count != 0)
                     {
                         ztpath.AddPolygon(DrawArea);//向该路径添加z多边形
                         Region ztregion = new Region(ztpath);//创建每个多边形的区域

                         using (Graphics g = this.CreateGraphics())
                         {
                             //g.FillPolygon(new SolidBrush(Color.AntiqueWhite), DrawArea);
                             g.DrawPath(new Pen(Color.Green, 2), ztpath);
                         }
                         for (int i = starti; i <= endi; i++)
                         {
                             if (ztregion.IsVisible(60 + (Convert.ToSingle(SysData.dt.Rows[i][1]) - tempdtX_F[0]) * kX, 520 - (60 + (Convert.ToSingle(SysData.dt.Rows[i][2]) - tempdtY_F[0]) * kY)))
                             {
                                 DrawRedline(i, starti);
                                 //单独窗体数据联动
                                 //SysData.dgv_Data.Rows[i].Selected = true;
                                 //SysData.dgv_Data.FirstDisplayedScrollingRowIndex = i;
                             }

                         }
                         //Selfrefresh();
                     }

                 }
             
            
         }

         private void button2_Click(object sender, EventArgs e)
         {

             this.MouseClick -= new MouseEventHandler(wellmain_MouseClick);
             
             Update_draw();
             Area_point.Clear();
             SysData.button_cancel = true;
             Selfrefresh();
             if (SysData.dt != null)
             {
                 if (SysData .dgv_Data !=null)
                 {
                     SysData.dgv_Data.ClearSelection();
                 }
                 
             }
             
             this.Refresh();
         }


        /// <summary>
        /// 联动区域
        /// </summary>
         

         private void DrawDashLine()
         {
             using (Graphics g = this.CreateGraphics())
             {
                 //虚线画笔
                 Pen pen = new Pen(Color.Black, 1);
                 float[] dashValues = { 5, 2, 15, 4 };
                 pen.DashPattern = dashValues;

                 if (Area_point.Count > 1)
                 {
                     for (int i = 0; i < Area_point.Count - 1; i++)
                     {
                         g.DrawLine(pen, Area_point[i], Area_point[i + 1]);
                     }
                 }
             }
         }
         private void wellmain_MouseClick(object sender, MouseEventArgs e)
         {
             if (e.Button == MouseButtons.Left)
             {
                 PointF p = new PointF(Convert.ToSingle(e.X), Convert.ToSingle(e.Y));
                 Area_point.Add(p);
                 DrawDashLine();
             }
             //if (e.Button == MouseButtons.Right)
             //{
             //    PointF[] DrawArea = new PointF[Area_point.Count];
             //    for (int i = 0; i < Area_point.Count; i++)
             //    {
             //        DrawArea[i] = Area_point[i];
             //    }
                 


             //    GraphicsPath ztpath = new GraphicsPath();//表示一系列的线围成的图
             //    ztpath.AddPolygon(DrawArea);//向该路径添加z多边形
             //    Region ztregion = new Region(ztpath);//创建每个多边形的区域
                 
             //    using (Graphics g = this.panel1.CreateGraphics())
             //    {
             //        //g.FillPolygon(new SolidBrush(Color.AntiqueWhite), DrawArea);
             //        g.DrawPath(new Pen(Color.Green, 2), ztpath);
             //    }
             //    for (int i = starti; i <= endi; i++)
             //    {
             //        if (ztregion.IsVisible(60 + (Convert.ToSingle(SysData.dt.Rows[i][1]) - tempdtX_F[0]) * kX, 520 - (60 + (Convert.ToSingle(SysData.dt.Rows[i][2]) - tempdtY_F[0]) * kY)))
             //        {
             //            DrawRedline(i, starti);
             //            SysData.dgv_Data.Rows[i].Selected = true;
             //            SysData.dgv_Data.FirstDisplayedScrollingRowIndex = i;
             //        }
                     
             //    }
             //}
         }
        /// <summary>
        /// 截屏函数
        /// </summary>
         private void Save_printImage()
         {
             Graphics g1 = this.CreateGraphics();              //获得窗体图形对象 
             Bitmap MyImage = new Bitmap(this.Width, this.Height, g1);
             SysData.PrintBit = MyImage;
         }

        
        //联动刷新
         private void selfrefresh()
         {
             this.Refresh();
         }

         private void label1_Click(object sender, EventArgs e)
         {
             FontDialog diag = new FontDialog();
             if (diag.ShowDialog() == DialogResult.OK)
             {
                 this.label1.Font = diag.Font;
             }
         }

         #region 窗体传值


         //label文字
         public string mytext
         {
             get
             {
                 return MyText;
             }
             set
             {
                 MyText = value;
                 //this.label1.Text = MyText;
             }
         }
         //label字样
         public Font myfont
         {
             get
             {
                 return MyFont;
             }
             set
             {
                 MyFont = value;
                 //this.label1.Font = MyFont;
             }
         }
         //label颜色
         public Color mycolor
         {
             get
             {
                 return MyColor;
             }
             set
             {
                 MyColor = value;
                 //this.label1.ForeColor = MyColor;
             }
         }
         //line1粗细
         public int myline1
         {
             get
             {
                 return MyLine1;
             }
             set
             {
                 MyLine1 = value;
             }
         }
         //line2粗细
         public int myline2
         {
             get
             {
                 return MyLine2;
             }
             set
             {
                 MyLine2 = value;
             }
         }
         //line1颜色
         public Color mylinecolor1
         {
             get
             {
                 return MyLineColor1;
             }
             set
             {
                 MyLineColor1 = value;
             }
         }
         //line2颜色
         public Color mylinecolor2
         {
             get
             {
                 return MyLineColor2;
             }
             set
             {
                 MyLineColor2 = value;
             }
         }
         //x轴刻度字体颜色
         public Color myxbrush
         {
             get
             {
                 return MyXBrush;
             }
             set
             {
                 MyXBrush = value;
             }
         }
         //y轴刻度字体颜色
         public Color myybrush
         {
             get
             {
                 return MyYBrush;
             }
             set
             {
                 MyYBrush = value;
             }
         }
         //x轴刻度字体样式
         public Font myxfont
         {
             get
             {
                 return MyXFont;
             }
             set
             {
                 MyXFont = value;
             }
         }
         //y轴刻度字体样式
         public Font myyfont
         {
             get
             {
                 return MyYFont;
             }
             set
             {
                 MyYFont = value;
             }
         }
         //x轴名字体样式
         public Font myxnamefont
         {
             get
             {
                 return MyXnameFont;
             }
             set
             {
                 MyXnameFont = value;
             }
         }
         //y轴名字体样式
         public Font myynamefont
         {
             get
             {
                 return MyYnameFont;
             }
             set
             {
                 MyYnameFont = value;
             }
         }
         //x轴名字体颜色
         public Color myxnamecolor
         {
             get
             {
                 return MyXnameColor;
             }
             set
             {
                 MyXnameColor = value;
             }
         }
         //y轴名字体颜色
         public Color myynamecolor
         {
             get
             {
                 return MyYnameColor;
             }
             set
             {
                 MyYnameColor = value;
             }
         }
         //x轴文字
         public string myxnametext
         {
             get
             {
                 return MyXnameText;
             }
             set
             {
                 MyXnameText = value;

             }
         }
         //y轴文字
         public string myynametext
         {
             get
             {
                 return MyYnameText;
             }
             set
             {
                 MyYnameText = value;

             }
         }
         //checkboxX
         public bool myxcheck
         {
             get
             {
                 return MyXCheck;
             }
             set
             {
                 MyXCheck = value;
             }
         }
         //checkboxY
         public bool myycheck
         {
             get
             {
                 return MyYCheck;
             }
             set
             {
                 MyYCheck = value;
             }
         }
         #endregion

        /*
         private void panel1_DoubleClick(object sender, EventArgs e)
         {
             
             if (SysData.dt == null)
             {
                 Data_WellLog cjsj = new Data_WellLog();
                 cjsj.ShowDialog();
                 if (SysData.dt != null)
                 {
                     this.Refresh();
                     Selfrefresh();
                     //cjsj.Show();
                 }
             }
             else
             {
                 Setofline_wLogMain setline = new Setofline_wLogMain(this);
                 setline.paint_refresh += selfrefresh;
                 setline.ShowDialog();
             }
         }
        */
         private void label1_DoubleClick(object sender, EventArgs e)
         {
             SysData.IsDouble = true;
             FontDialog diag = new FontDialog();
             if (diag.ShowDialog() == DialogResult.OK)
             {
                 this.label1.Font = diag.Font;
                 MyFont = diag.Font;
             }
         }

         private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             //复制窗体,把窗体内容序列化
             FormState fs = new FormState(MyText, MyColor, MyFont, MyLineColor1, MyLineColor2, MyLine1, MyLine2, SysData.dt, MyXBrush, MyYBrush, MyXFont,
                                          MyYFont, MyXnameFont, MyXnameColor, MyXnameText, MyYnameFont, MyYnameColor, MyYnameText, MyXCheck, MyYCheck);
             XmlHelper.SaveForm(fs);
             MyObject.FrmName1 = "主图";//如果萨胡成因的窗体被点击了，那么就把名字给当前操作窗体的名字
         }

         private void 主图_Load(object sender, EventArgs e)
         {
             this.BringToFront();
             button1.Enabled = false;
             button2.Enabled = false;
             MyObject.FrmName2 = "主图";
           
         }

         private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
         {
             //MyObject.FrmName1 = null;
             this.Close();
         }

         private void WellLog_Main_DoubleClick(object sender, EventArgs e)
         {
             if (SysData.dt == null)
             {
                 Way_DataLoad wdl = new Way_DataLoad();
                 wdl.ShowDialog ();
                 if (SysData.dt != null)
                 {
                     this.Refresh();
                     Selfrefresh();
                     //cjsj.Show();
                 }
             }
             else
             {
                 Setofline_wLogMain setline = new Setofline_wLogMain(this);
                 setline.paint_refresh += selfrefresh;
                 setline.ShowDialog();
             }
         }

         private void WellLog_Main_MouseClick(object sender, MouseEventArgs e)
         {
             MyObject.FrmName2 = "主图";
             this.BringToFront();
         }

         private void WellLog_Main_MouseUp(object sender, MouseEventArgs e)
         {
             MainFrame mf = new MainFrame();
             mf.getMsg();
         }

         
    }



   
}
