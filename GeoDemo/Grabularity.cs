using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace GeoDemo
{
    public partial class Grabularity : Form
    {
        public Font MyFont = SysData.title_font;
        public Color MyColor = SysData.title_color;
        public string MyText = "粒度概率累计曲线";
        public Color MyLineColor1 = SysData.line_color;
        public Color MyLineColor2 = SysData.line_color2;
        public int MyLine1 = SysData.line1;
        public int MyLine2 = SysData.line2;
        public Color MyXBrush = Color.Black;
        public Color MyYBrush = Color.Black;
        public Font MyXFont = new Font("宋体", 12, FontStyle.Regular);
        public Font MyYFont = new Font("宋体", 12, FontStyle.Regular);
        //轴名
        public Font MyXnameFont = new Font("宋体", 12, FontStyle.Regular);
        public Color MyXnameColor = Color.Black;
        

        public Font MyYnameFont = new Font("宋体", 12, FontStyle.Regular);
        public Color MyYnameColor = Color.Black;
        




        public Grabularity()
        {
            InitializeComponent();
            //this.comboBox1.SelectedIndex = 0;
           
        }
        double PI = 3.14;
        float Average_Size;//平均粒径
        public float Middle_Value;//中值
        double Standard_Deviation;//标准偏差
        double Sorting_Coefficient;//分选系数
        double Skewness;//偏度
        double Kurtosis;//峰度


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            label13.Text = MyText;
            if (SysData.IsDouble)
            {
                label13.Font = MyFont;
                label13.ForeColor = MyColor;

            }
            else
            {
                label13.Font = SysData.title_font;
                label13.ForeColor = SysData.title_color;
            }
            //float[] y = new float[] { 0.53f, 2.46f, 17.75f, 30.04f, 38.92f, 49.1f, 69.85f, 73.29f, 93.08f, 99.01f, 99.53f, 99.68f, 99.74f, 99.82f, 99.89f, 99.99f};
            //float[] x = new float[] { 0.2f, 0.4f, 0.72f, 1f, 1.2f, 1.3f, 1.75f, 2f, 2.32f, 2.72f, 3f, 3.3f, 3.5f, 3.75f, 4f, 5f};

            //////////////////////////////////////////////////////////////////////////label控件的移动
            label1.Location = new Point(Convert.ToInt32(651* SysData.K_Width), Convert.ToInt32(99 * SysData.K_Height));
            label2.Location = new Point(Convert.ToInt32(651* SysData.K_Width), Convert.ToInt32(131 * SysData.K_Height));
            label3.Location = new Point(Convert.ToInt32(651 * SysData.K_Width), Convert.ToInt32(163* SysData.K_Height));
            label4.Location = new Point(Convert.ToInt32(542 * SysData.K_Width), Convert.ToInt32(99 * SysData.K_Height));
            label5.Location = new Point(Convert.ToInt32(542 * SysData.K_Width), Convert.ToInt32(131 * SysData.K_Height));
            label6.Location = new Point(Convert.ToInt32(542 * SysData.K_Width), Convert.ToInt32(163 * SysData.K_Height));
            label7.Location = new Point(Convert.ToInt32(649 * SysData.K_Width), Convert.ToInt32(199 * SysData.K_Height));
            label8.Location = new Point(Convert.ToInt32(542 * SysData.K_Width), Convert.ToInt32(199 * SysData.K_Height));
            label9.Location = new Point(Convert.ToInt32(649 * SysData.K_Width), Convert.ToInt32(236 * SysData.K_Height));
            label10.Location = new Point(Convert.ToInt32(649 * SysData.K_Width), Convert.ToInt32(269 * SysData.K_Height));
            label11.Location = new Point(Convert.ToInt32(542 * SysData.K_Width), Convert.ToInt32(236 * SysData.K_Height));
            label12.Location = new Point(Convert.ToInt32(542* SysData.K_Width), Convert.ToInt32(269 * SysData.K_Height));




            Font myXfont = MyXFont;
            Font myYfont = MyYFont;
            Color myXcolor = MyXBrush;
            Color myYcolor = MyYBrush;





            Bitmap bit = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bit);
            Pen p = new Pen(MyLineColor2, MyLine2);
            g.DrawLine(p, ChangeToX(0), ChangeToScreem(0.0001), ChangeToX(5), ChangeToScreem(0.0001));//下横线
            g.DrawLine(p, ChangeToX(0), ChangeToScreem(0.9999), ChangeToX(5), ChangeToScreem(0.9999));//上横线
            g.DrawLine(p, ChangeToX(5), ChangeToScreem(0.0001), ChangeToX(5), ChangeToScreem(0.9999));//右Y
            PointF a1 = new PointF(ChangeToX(0), ChangeToScreem(0.0001));
            PointF a2 = new PointF(ChangeToX(0), ChangeToScreem(0.5));

            PointF a3 = new PointF(ChangeToX(0), ChangeToScreem(0.3));
            PointF a4 = new PointF(ChangeToX(0), ChangeToScreem(0.1));
            PointF a5 = new PointF(ChangeToX(0), ChangeToScreem(0.01));
            PointF a6 = new PointF(ChangeToX(0), ChangeToScreem(0.001));
            PointF a7 = new PointF(ChangeToX(0), ChangeToScreem(0.0001));

            PointF a8 = new PointF(ChangeToX(0), ChangeToScreem(0.7));
            PointF a9 = new PointF(ChangeToX(0), ChangeToScreem(0.9));
            PointF a10 = new PointF(ChangeToX(0), ChangeToScreem(0.99));
            PointF a11 = new PointF(ChangeToX(0), ChangeToScreem(0.999));
            PointF a12 = new PointF(ChangeToX(0), ChangeToScreem(0.9999));
          
            g.DrawLine(p, a1, a12);
            g.DrawLine(p, a2.X, a2.Y, ChangeToX(0)-5, a2.Y);
            g.DrawLine(p, a3.X, a3.Y, ChangeToX(0)-5, a3.Y);
            g.DrawLine(p, a4.X, a4.Y, ChangeToX(0)-5, a4.Y);
            g.DrawLine(p, a5.X, a5.Y, ChangeToX(0)-5, a5.Y);
            g.DrawLine(p, a6.X, a6.Y, ChangeToX(0)-5, a6.Y);
            g.DrawLine(p, a7.X, a7.Y, ChangeToX(0)-5, a7.Y);
            g.DrawLine(p, a8.X, a8.Y, ChangeToX(0)-5, a8.Y);
            g.DrawLine(p, a9.X, a9.Y, ChangeToX(0)-5, a9.Y);
            g.DrawLine(p, a10.X, a10.Y, ChangeToX(0)-5, a10.Y);
            g.DrawLine(p, a11.X, a11.Y, ChangeToX(0)-5, a11.Y);
            g.DrawLine(p, a12.X, a12.Y, ChangeToX(0)-5, a12.Y);

            g.DrawString("99.99", myYfont, new SolidBrush(myYcolor), a12.X - 50, a12.Y - 5);
            g.DrawString("99.9", myYfont, new SolidBrush(myYcolor), a11.X - 50, a11.Y - 5);
            g.DrawString("99", myYfont, new SolidBrush(myYcolor), a10.X - 50, a10.Y - 5);
            g.DrawString("90", myYfont, new SolidBrush(myYcolor), a9.X - 50, a9.Y - 5);
            g.DrawString("70", myYfont, new SolidBrush(myYcolor), a8.X - 50, a8.Y - 5);
            g.DrawString("50", myYfont, new SolidBrush(myYcolor), a2.X - 50, a2.Y - 5);
            g.DrawString("30", myYfont, new SolidBrush(myYcolor), a3.X - 50, a3.Y - 5);
            g.DrawString("10", myYfont, new SolidBrush(myYcolor), a4.X - 50, a4.Y - 5);
            g.DrawString("1", myYfont, new SolidBrush(myYcolor), a5.X - 50, a5.Y - 5);
            g.DrawString("0.1", myYfont, new SolidBrush(myYcolor), a6.X - 50, a6.Y - 5);
            g.DrawString("0.01", myYfont, new SolidBrush(myYcolor), a7.X - 50, a7.Y - 5);

            //算术百分比刻度线
            for (int i = 0; i <= 10;i++ )
            {
                
                g.DrawLine(p, ChangeToX(5), ChangeToScreem(0.0001) - (ChangeToScreem(0.0001) - ChangeToScreem(0.9999))*i/10, ChangeToX(5)+5, ChangeToScreem(0.0001) - (ChangeToScreem(0.0001) - ChangeToScreem(0.9999))*i/10);
                g.DrawString(Convert.ToString(i * 10), myYfont, new SolidBrush(myYcolor), ChangeToX(5)+5, ChangeToScreem(0.0001) - 5 - (ChangeToScreem(0.0001) - ChangeToScreem(0.9999)) * i / 10);
            }

            //横坐标刻度线
            for (int i = 0; i < 6; i++ )
            {
                g.DrawLine(p, Convert.ToSingle(ChangeToX(0) + 500 / 5 * i*SysData.K_Width), ChangeToScreem(0.0001), Convert.ToSingle(ChangeToX(0) + 500 / 5 * i*SysData.K_Width), ChangeToScreem(0.0001)+5f);
                g.DrawString(Convert.ToString(i), myXfont, new SolidBrush(myXcolor), ChangeToX(0)-5 + 500 / 5 * i*SysData.K_Width, ChangeToScreem(0.0001) + 10f);
                g.DrawString(Convert.ToString(1/Math.Pow(2,i)), myXfont, new SolidBrush(myXcolor), ChangeToX(0) - 5 + 500 / 5 * i * SysData.K_Width, ChangeToScreem(0.0001) + 23f);
            }

            //两个Y轴的标题
            Graphics g1 = Graphics.FromImage(bit);
            //Random rdn;
             Font myFont = new Font("宋体", 18);
            string s1 = "概率百分比%";
            g1.ResetTransform();
            SizeF size = g1.MeasureString(s1, myFont);
            g1.TranslateTransform(ChangeToX(-0.8), ChangeToScreem(0.3));
            g1.RotateTransform(-90); //旋转
            g1.DrawString(s1, MyYnameFont, new SolidBrush(MyYnameColor), new PointF(-size.Width/2, -size.Height/2));
           // g1.RotateTransform(-90);

            Graphics g2 = Graphics.FromImage(bit);
            //Random rdn;
            
            string s2 = "算术百分比%";
            g2.ResetTransform();
            SizeF size2 = g1.MeasureString(s2, myFont);
            g2.TranslateTransform(ChangeToX(5.8), ChangeToScreem(0.3));
            g2.RotateTransform(-90); //旋转
            g2.DrawString(s2, MyYnameFont, new SolidBrush(MyYnameColor), new PointF(-size.Width / 2, -size.Height / 2));
            //x轴轴名
            g.DrawString("Φ值", MyXnameFont, new SolidBrush(MyXnameColor), ChangeToX(0) - 5 + 500 / 5 * 5 * SysData.K_Width+15, ChangeToScreem(0.0001) + 10f);
            g.DrawString("mm", MyXnameFont, new SolidBrush(MyXnameColor), ChangeToX(0) - 5 + 500 / 5 * 5 * SysData.K_Width + 60, ChangeToScreem(0.0001) + 23f);

            if (SysData.ldglljqx_dt != null)
            {

                #region 数据的处理
                float[] y1 = new float[SysData.ldglljqx_dt.Rows.Count];//概率（频率）
                float[] x = new float[SysData.ldglljqx_dt.Rows.Count];
                for (int i = 0; i < y1.Length-1; i++)
                {
                    if (SysData.ldglljqx_dt.Rows[i][0].ToString() == ">4")
                    {
                        SysData.ldglljqx_dt.Rows[i][0] = 4;
                    }
                    if (SysData.ldglljqx_dt.Rows[i][0].ToString() == ">1")
                    {
                        SysData.ldglljqx_dt.Rows[i][0] = 1.0001;
                    }
                    if (SysData.ldglljqx_dt.Rows[i][0].ToString() == "<0.06")
                    {
                        SysData.ldglljqx_dt.Rows[i][0] = 0.0625;
                    }
                    x[i] = Convert.ToSingle(SysData.ldglljqx_dt.Rows[i][0]);
                    if (SysData.com_tab == "mm")
                    {
                        x[i] =Convert.ToSingle( -Math.Log(Convert.ToDouble(x[i]),2));
                    }
                    if (SysData.ldglljqx_dt.Rows[i][1].ToString() == "0")
                    { y1[i] = 0.01f; }
                    else
                    {
                        y1[i] = Convert.ToSingle(SysData.ldglljqx_dt.Rows[i][1]);
                    }
                }
                float[] y = new float[y1.Length];//频率Y值
                for (int i = 0; i < x.Length; i++)
                {
                    if (i == 0)
                        y[i] = y1[i];
                    else
                        y[i] = y[i - 1] + y1[i];
                }

                #endregion

                PointF[] Dpoint = new PointF[y.Length];
                PointF[] DpointR = new PointF[y.Length];
                PointF[] DpointQ = new PointF[y.Length];
                PointF[] DrawPointQ = new PointF[7];
                DrawPointQ[0].X = ChangeToX(0.0);
                DrawPointQ[0].Y = ChangeToY_Right(0);
                //为绘制.DrawBeziers曲线，增加的第7个点（因为.DrawBeziers曲线是用3的倍数加1个点绘制的）
                DrawPointQ[6].X = ChangeToX(5);
                DrawPointQ[6].Y = DrawPointQ[0].Y;



                for (int i = 0; i < x.Length; i++)
                {
                    Dpoint[i].X = ChangeToX(x[i]);
                    Dpoint[i].Y = ChangeToScreem(Convert.ToDouble(y[i] / 100.0));

                }
                for (int i = 0; i < x.Length; i++)
                {
                    DpointR[i].X = ChangeToX(x[i]);
                    DpointR[i].Y = ChangeToY_Right(y[i]);
                }

                for (int i = 0; i < x.Length; i++)
                {
                    DpointQ[i].X = DpointR[i].X;
                    DpointQ[i].Y = ChangeToY_Right(y1[i]);
                }

                //寻找频率曲线中点的最大值
                int[] i_count = new int[6];
                i_count[0] = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] > 0f && x[i] <= 1f)
                        i_count[1] = i;
                    if (x[i] > 1f && x[i] <= 2f)
                        i_count[2] = i;
                    if (x[i] > 2f && x[i] <= 3f)
                        i_count[3] = i;
                    if (x[i] > 3f && x[i] <= 4f)
                        i_count[4] = i;
                    if (x[i] > 4f && x[i] <= 5f)
                        i_count[5] = i;

                }
                for (int j = 1; j < 6; j++)
                {
                    DrawPointQ[j].X = ChangeToX(j - 0.5);
                    DrawPointQ[j].Y = Max(DpointQ, i_count[j - 1], i_count[j]);

                }

                #region

                //

                //舍弃不在规律上的点的新的frequency点集
                //
                /*/ PointF frequency=new 
                 int count_frequency = 0;
                 float DpointQ_min_X = DpointQ[Min(DpointQ)].X;
                 for (int i = 0; i < y.Length-1; i++)
                 {
                     int count_new = 0;
                
                     if (DpointQ[i].X < DpointQ_min_X)
                     {
                         for (int j = i + 1; j < y.Length; )
                         {
                             if (DpointQ[i].Y < DpointQ[j].Y)
                             {
                                 count_frequency++;
                                 count_new++;
                                 j++;
                             }
                             else
                                 break;
                         }
                         if (count_new != 0)
                         {
                             for (int k = i + 1; (k+count_new) < y.Length; k++)
                             {
                                 DpointQ[k] = DpointQ[k + count_new];
                             }
                             for (int k1 = 0; k1 < count_new; k1++)
                             {
                                 DpointQ[y.Length - 1 - k1].X = 0f;
                                 DpointQ[y.Length - 1 - k1].Y = ChangeToY_Right(0);
                             }
                         }
                         //count_frequency = count_frequency + count_new;
                     }
                     else
                     {
                         for (int n = i + 1; n < y.Length; )
                         {
                             if (DpointQ[i].Y > DpointQ[n].Y)
                             {
                                 count_new++;
                                 count_frequency++;
                                 n++;
                             }
                             else
                                 break;
                         }
                         if (count_new != 0)
                         {
                             for (int m = i + 1; (m+count_new) < y.Length; m++)
                             {
                                 DpointQ[m] = DpointQ[m + count_new];
                             }
                             for (int k1 = 0; k1 < count_new; k1++)
                             {
                                 DpointQ[y.Length- count_frequency- 1 + k1].X = 0f;
                                 DpointQ[y.Length-count_frequency- 1 + k1].Y = ChangeToY_Right(0);
                             }
                         }
                         //count_frequency = count_frequency + count_new;

                     }
                 }
                 PointF[] frequency = new PointF[y.Length-count_frequency-1];
                 for (int i = 0; i < y.Length - count_frequency-1; i++)
                 {
                     frequency[i] = DpointQ[i];
                 }
            
                 g.DrawCurve(new Pen(Color.Gold, 2), frequency);*/



                /*//
                //
                //链接峰值，来作图
                //
                int count_frequency = 0;
                int remember_count_new = 0;
                float DpointQ_min_X = DpointQ[Min(DpointQ)].X;
                for (int i = 0; i < y.Length - 1; i++)
                {
                    int count_new = 0;

                    if (DpointQ[i].X < DpointQ_min_X)
                    {
                        for (int j = i + 1; j < y.Length; )
                        {
                            if (DpointQ[i].Y < DpointQ[j].Y)
                            {
                                count_frequency++;
                                count_new++;
                                j++;
                            }
                            else
                                break;
                        }
                        if (count_new != 0)
                        {
                            for (int k = i + 1; (k + count_new) < y.Length; k++)
                            {
                                DpointQ[k] = DpointQ[k + count_new];
                            }
                            for (int k1 = 0; k1 < count_new; k1++)
                            {
                                //DpointQ[y.Length - 1 - k1].X = ChangeToX(x[x.Length-1-k1]);
                                DpointQ[y.Length - 1 - k1].Y = ChangeToY_Right(0);
                            }
                            remember_count_new = remember_count_new + count_new;
                        }
                        //count_frequency = count_frequency + count_new;
                    }
                    else
                        break;
                }
                for (int i = y.Length - 1-remember_count_new; i > 0;i-- )//最大值右边的曲线
                {
                    int count_new = 0;
                    if (DpointQ[i].X > DpointQ_min_X)
                    {
                        for (int j = i - 1; j > 0; j--)
                        {
                            if (DpointQ[i].Y < DpointQ[j].Y)
                            {
                                count_frequency++;
                                count_new++;
                                //j++;
                            }
                            else
                                break;
                        }
                        if (count_new != 0)
                        {
                            for (int k = i ; (k + count_new) < y.Length; k++)
                            {
                                DpointQ[k-count_new] = DpointQ[k];
                            }
                        }
                    }
                    else
                        break;
                }
                PointF[] frequency = new PointF[y.Length - count_frequency ];
                for (int i = 0; i < y.Length - count_frequency ; i++)
                {
                    frequency[i] = DpointQ[i];
                }

                g.DrawCurve(new Pen(Color.Gold, 2), frequency);
                */
                #endregion
                //绘制曲线
                g.DrawCurve(new Pen(Color.Red, 1), Dpoint);//绘制3-累积概率曲线
                g.DrawString("累积概率曲线", new Font("宋体",12), new SolidBrush(Color.Red), Dpoint[x.Length - 1]);
                g.DrawCurve(new Pen(Color.Black), DpointR);//绘制2-累积曲线
                g.DrawString("累积曲线", new Font("宋体", 12), new SolidBrush(Color.Black), DpointR[x.Length - 1]);
                g.DrawCurve(new Pen(Color.Blue, 2), DpointQ);//绘制1-频率曲线
                g.DrawString("频率曲线", new Font("宋体", 12), new SolidBrush(Color.Blue), DpointQ[x.Length - 1]);







                //50%的粒径  16%的粒径   84%的粒径  除以3得出的 平均粒径
                Average_Size = ((Calculate(DpointR[Search(50f, y) - 1], DpointR[Search(50f, y) + 1], DpointR[Search(50f, y)], 50).X - 100*SysData.K_Width) + (Calculate(DpointR[Search(16f, y) - 1], DpointR[Search(16f, y) + 1], DpointR[Search(16f, y)], 16).X - 100*SysData.K_Width) + (Calculate(DpointR[Search(84f, y) - 1], DpointR[Search(84f, y) + 1], DpointR[Search(84f, y)], 84).X - 100*SysData.K_Width)) /(500*SysData.K_Width)*5 / 3;
                label1.Text = Convert.ToString(Average_Size) + "φ";
                //50%的粒径 （中值Md）
                Middle_Value = (Calculate(DpointR[Search(50f, y) - 1], DpointR[Search(50f, y) + 1], DpointR[Search(50f, y)], 50).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5;
                label2.Text = Convert.ToString(Middle_Value) + "φ";
                //标准偏差   （φ84-φ16）/4+(φ95-φ5)/6.6
                Standard_Deviation = ((Calculate(DpointR[Search(84f, y) - 1], DpointR[Search(84f, y) + 1], DpointR[Search(84f, y)], 84).X - 100*SysData.K_Width) - (Calculate(DpointR[Search(16f, y) - 1], DpointR[Search(16f, y) + 1], DpointR[Search(16f, y)], 16).X - 100*SysData.K_Width)) / (500*SysData.K_Width) * 5 / 4 + ((Calculate(DpointR[Search(95f, y) - 1], DpointR[Search(95f, y) + 1], DpointR[Search(95f, y)], 95).X - 100*SysData.K_Width) - (Calculate(DpointR[Search(5f, y) - 1], DpointR[Search(5f, y) + 1], DpointR[Search(5f, y)], 5).X - 100*SysData.K_Width)) / (500*SysData.K_Width )* 5 / 6.6;
                label3.Text = Convert.ToString(Convert.ToSingle(Standard_Deviation));
                //分选系数
                Sorting_Coefficient = Math.Pow(2, -((Calculate(DpointR[Search(25f, y) - 1], DpointR[Search(25f, y) + 1], DpointR[Search(25f, y)], 25).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5)) / Math.Pow(2, -((Calculate(DpointR[Search(75f, y) - 1], DpointR[Search(75f, y) + 1], DpointR[Search(75f, y)], 75).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5));
                label7.Text = Convert.ToString(Convert.ToSingle(Sorting_Coefficient));
                //偏度
                Skewness = ((Calculate(DpointR[Search(16f, y) - 1], DpointR[Search(16f, y) + 1], DpointR[Search(16f, y)], 16).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5 + (Calculate(DpointR[Search(84f, y) - 1], DpointR[Search(84f, y) + 1], DpointR[Search(84f, y)], 84).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5 - 2 * (Calculate(DpointR[Search(50f, y) - 1], DpointR[Search(50f, y) + 1], DpointR[Search(50f, y)], 50).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5) / (2 * ((Calculate(DpointR[Search(84f, y) - 1], DpointR[Search(84f, y) + 1], DpointR[Search(84f, y)], 84).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5) - (Calculate(DpointR[Search(16f, y) - 1], DpointR[Search(16f, y) + 1], DpointR[Search(16f, y)], 16).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5) +
                    ((Calculate(DpointR[Search(5f, y) - 1], DpointR[Search(5f, y) + 1], DpointR[Search(5f, y)], 5).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5 + (Calculate(DpointR[Search(50f, y) - 1], DpointR[Search(95f, y) + 1], DpointR[Search(95f, y)], 95).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5 - 2 * (Calculate(DpointR[Search(50f, y) - 1], DpointR[Search(50f, y) + 1], DpointR[Search(50f, y)], 50).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5) / (2 * ((Calculate(DpointR[Search(95f, y) - 1], DpointR[Search(95f, y) + 1], DpointR[Search(95f, y)], 95).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5 - (Calculate(DpointR[Search(5f, y) - 1], DpointR[Search(5f, y) + 1], DpointR[Search(5f, y)], 5).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5));
                label9.Text = Convert.ToString(Convert.ToSingle(Skewness));
                //峰度
                Kurtosis = ((Calculate(DpointR[Search(95f, y) - 1], DpointR[Search(95f, y) + 1], DpointR[Search(95f, y)], 95).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5 - (Calculate(DpointR[Search(5f, y) - 1], DpointR[Search(5f, y) + 1], DpointR[Search(5f, y)], 5).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5) /
                    (2.44 * ((Calculate(DpointR[Search(75f, y) - 1], DpointR[Search(75f, y) + 1], DpointR[Search(75f, y)], 75).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5 - (Calculate(DpointR[Search(25f, y) - 1], DpointR[Search(25f, y) + 1], DpointR[Search(25f, y)], 25).X - 100*SysData.K_Width) / (500*SysData.K_Width) * 5));
                label10.Text = Convert.ToString(Convert.ToSingle(Kurtosis));
            }
            this.panel1.BackgroundImage = bit;
            SysData.PrintBit = bit;
        }

        private double Normal(double y)//正态分布函数反求x
        {
            double x = Math.Abs(2 * PI) / 4 * Math.Log(y / (1 - y)); 
             return x;
            
        }

        private float ChangeToScreem(double a)//调整对应左Y轴的值
        {
            float b = Convert.ToSingle((240 - Normal(a) * 13.9)*SysData.K_Height);//调整输入数字到屏幕上的距离
            return b;
        }

        private float ChangeToX(double x)//x轴转换适应屏幕
        {
            
            //if (SysData.com_tab == "mm")
            //{
            //    x = -Math.Log(2, x);
            //}
            float re_x= Convert.ToSingle((100 + x * 500/5 )*SysData.K_Width);
                return re_x;
        }

        private float ChangeToY_Right(float y)//调整对应右Y轴的值
        {
            return ChangeToScreem(0.0001) - y * (ChangeToScreem(0.0001) - ChangeToScreem(0.9999)) / 100;
        }

        private float Max(PointF[] point,int i_m, int i_number)//找每一段中最大的Y值(实际屏幕坐标的最小值)
        {
            float max = point[i_m].Y;
            for (int j = i_m; j < i_number; j++)
            {
                if (max > point[j].Y)
                    max = point[j].Y;
            }
            return max;
        }

        //计算线上的点
        private PointF Calculate(PointF startpoint, PointF endpoint, PointF controlpoint, int T)
        {
            float t =Convert.ToSingle( T) / 100f;
            PointF Repoint = new PointF(0f,0f);
            Repoint.X = (1 - t) * (1 - t) * startpoint.X + 2 * t * (1 - t) * controlpoint.X + t * t * endpoint.X;
            Repoint.Y = (1 - t) * (1 - t) * startpoint.Y + 2 * t * (1 - t) * controlpoint.Y + t * t * endpoint.Y;
            return Repoint;
        }
       
        //折半查找数组中最接近的数，返回最接近的array【i】的i值
        private int Search(float key,float[] S)
        {
            int low = 0;
            int high = S.Length;
            int mid;
            while (low < high)
            {
                mid = (low + high)/2;
                if (key == S[mid])
                    return mid;
                else if (key < S[mid])
                    high = mid - 1;
                else low = mid + 1;

            }
            return high;
        }
        
        //找频率曲线对应屏幕Y值最小的点的i值
        private int Min(PointF[] min)
        {
            int i=0;
            float a_min = min[0].Y;
            for (int j = 0; j < min.Length; j++)
            {
                if (a_min > min[j].Y)
                {
                    a_min = min[j].Y;
                    i = j;
                }

            }
            return i;
        }

        private void label13_DoubleClick(object sender, EventArgs e)
        {
            FontDialog diag = new FontDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                this.label13.Font = diag.Font;
                MyFont = diag.Font;
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
        //x轴字体颜色
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
        //y轴字体颜色
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
        //x轴字体样式
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
        //y轴字体样式
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
        #endregion



        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            if (SysData.ldglljqx_dt == null)
            {
                ldglljqx读取数据 ldsj = new ldglljqx读取数据();
                ldsj.ShowDialog();
                this.Refresh();
            }
            else
            {
                Setofline_粒度 setline = new Setofline_粒度(this);
                setline.paint_refresh += selfrefresh;
                setline.ShowDialog();
            }
        }
        public void selfrefresh()
        {
            this.Refresh();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //复制窗体,把窗体内容序列化
            FormState fs = new FormState(MyText, MyColor, MyFont, MyLineColor1, MyLineColor2, MyLine1, MyLine2, SysData.ldglljqx_dt,MyXBrush,MyYBrush,MyXFont,MyYFont,MyXnameFont,MyXnameColor,MyYnameFont,MyYnameColor);
            XmlHelper.SaveForm(fs);
            MyObject.FrmName1 = this.Name;//如果萨胡成因的窗体被点击了，那么就把名字给当前操作窗体的名字
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MyObject.FrmName1 = null; 
            this.Close();
            //SysData.ldglljqx_dt.Clear();
        }

        private void 粒度概率累计曲线_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            MyObject.FrmName2 = "粒度概率累计曲线";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Grabularity_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
            MyObject.FrmName2 = "粒度概率累计曲线";
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            this.BringToFront();
            MyObject.FrmName2 = "粒度概率累计曲线";
        }

        private void Grabularity_MouseUp(object sender, MouseEventArgs e)
        {
            MainFrame mf = new MainFrame();
            mf.getMsg();
        }

    }
      
   

}
