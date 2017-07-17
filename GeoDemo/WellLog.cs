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
    public delegate void PaintZT();
    public partial class WellLog : Form
    {
        public Font MyFont = SysData.title_font;
        public Color MyColor = SysData.title_color;
        public string MyText = "测井曲线";
        public Color MyLineColor1 = SysData.line_color;
        public Color MyLineColor2 = SysData.line_color2;
        public int MyLine1 = SysData.line1;
        public int MyLine2 = SysData.line2;
        public Color MyXBrush = Color.Black;
        public Color MyYBrush = Color.Black;
        public Font MyXFont = new Font("宋体", 7, FontStyle.Regular);
        public Font MyYFont = new Font("宋体", 8, FontStyle.Regular);
        //轴名
        public Font MyXnameFont = new Font("宋体", 8, FontStyle.Regular);
        public Color MyXnameColor = Color.Black;


        public Font MyYnameFont = new Font("宋体", 24, FontStyle.Regular);
        public Color MyYnameColor = Color.Black;


        List<int> start_count = new List<int>(); //绘制红线
        
        List<int> end_count = new List<int>();
        public WellLog()
        {
            InitializeComponent();
        }
        public event PaintZT Panel_refresh;
        float MinX, MinY, cjKx, cjKy;
        List<float> tempdtX = new List<float>();//用来排序
        List<float> tempdtY = new List<float>();
        #region 绘图区域的点

        PointF P1 = new PointF(5, 5);
        PointF P2 = new PointF(295, 5);
        PointF P3 = new PointF(5, 54);
        PointF P4 = new PointF(130, 54);
        PointF P5 = new PointF(170, 54);
        PointF P6 = new PointF(295, 54);
        PointF P7 = new PointF(5, 103);
        PointF P8 = new PointF(295, 103);
        PointF P9 = new PointF(5, 495);
        PointF P10 = new PointF(295, 495);
        #endregion
        private void 测井曲线_Paint(object sender, PaintEventArgs e)
        {
            label1.Text = MyText;
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
            Pen pn = new Pen(MyLineColor1, MyLine1);
            Pen pb = new Pen(MyLineColor2, MyLine2);
            Font myXfont = MyXFont;
            Font myYfont = MyYFont;
            Color myXcolor = MyXBrush;
            Color myYcolor = MyYBrush;





            Graphics g = this.CreateGraphics();
            SysData.textbox1 = textBox1.Text;
            SysData.textbox2 = textBox2.Text;


            #region 选中合并曲线道



            if (checkBox1.Checked)
            {
                //边框
                g.DrawLine(pb, P1, P2);
                g.DrawLine(pb, P1, P9);
                g.DrawLine(pb, P2, P10);
                g.DrawLine(pb, P9, P10);
                g.DrawLine(pb, P3, P6);
                g.DrawLine(pb, P7, P8);
                g.DrawLine(pb, P5.X, P5.Y, P5.X, P9.Y);
                //曲线道 网格横线（灰色）
                for (int i = 1; i < 20; i++)
                {
                    g.DrawLine(pn, P7.X, P7.Y + (P9.Y - P7.Y) / 20 * i, P5.X, P7.Y + (P9.Y - P7.Y) / 20 * i);
                }
                //曲线道 网格纵线（灰色）
                for (int i = 1; i < 5; i++)
                {
                    g.DrawLine(pn, P9.X + (P5.X - P3.X) / 5 * i, P9.Y, P9.X + (P5.X - P3.X) / 5 * i, P7.Y);
                }
                //深度道 刻度（黑色）
                for (int i = 1; i < 40; i++)
                {
                    g.DrawLine(new Pen(Color.Black, 1), P5.X, P8.Y + (P9.Y - P7.Y) / 40 * i, P5.X + 6, P8.Y + (P9.Y - P7.Y) / 40 * i);
                }
                for (int i = 1; i < 4; i++)
                {
                    g.DrawLine(new Pen(Color.Black, 1), P5.X, P8.Y + (P9.Y - P7.Y) / 4 * i, P5.X + 9, P8.Y + (P9.Y - P7.Y) / 4 * i);
                    g.DrawLine(new Pen(Color.Black, 2), P7.X, P7.Y + (P9.Y - P7.Y) / 4 * i, P5.X, P7.Y + (P9.Y - P7.Y) / 4 * i);//网格中的粗线
                }
                #region 绘字
                g.DrawString(System.IO.Path.GetFileNameWithoutExtension(SysData.abovefile.FileName), MyYnameFont, new SolidBrush(MyYnameColor), P1.X, P1.Y + (P3.Y - P1.Y) / 3);
                g.DrawString("曲线道X", MyXnameFont, new SolidBrush(MyXnameColor), P3.X + (P5.X - P3.X) / 3, P3.Y + (P7.Y - P3.Y) / 4);
                g.DrawString("曲线道Y", MyXnameFont, new SolidBrush(Color.Green), P3.X + (P5.X - P3.X) / 3, P3.Y + (P7.Y - P3.Y) / 2);
                g.DrawString("深度", MyXnameFont, new SolidBrush(MyXnameColor), P5.X + (P6.X - P5.X) / 3, P4.Y + (P7.Y - P3.Y) / 3);

                #endregion
                //数据处理
                if (SysData.dt != null)
                {
                    for (int i = 1; i < SysData.dt.Rows.Count; i++)
                    {
                        if (Convert.ToSingle(SysData.dt.Rows[i][1]) > -5000 && Convert.ToSingle(SysData.dt.Rows[i][2]) > -5000)
                        {
                            tempdtX.Add(Convert.ToSingle(SysData.dt.Rows[i][1]));
                            tempdtY.Add(Convert.ToSingle(SysData.dt.Rows[i][2]));
                        }

                    }
                    tempdtX.Sort();
                    tempdtY.Sort();

                    //Console.Write(tempdtX[tempdtX.Count - 1]);
                    ////////给测井曲线的数据
                    cjKx = (P5.X - P3.X) / (tempdtX[tempdtX.Count - 1] - tempdtX[0]);
                    cjKy = (P5.X - P3.X) / (tempdtY[tempdtY.Count - 1] - tempdtY[0]);

                    MinX = tempdtX[0];

                    MinY = tempdtY[0];

                    #region 曲线道绘字
                    for (int i = 0; i < 6; i++)
                    {
                        g.DrawString((MinX + (tempdtX[tempdtX.Count - 1] - tempdtX[0]) / 5 * i).ToString("F1"), myXfont, new SolidBrush(myXcolor), P7.X + (P5.X - P3.X) / 5 * i, P7.Y - 10);
                        g.DrawString((MinY + (tempdtY[tempdtY.Count - 1] - tempdtY[0]) / 5 * i).ToString("F1"), myXfont, new SolidBrush(myXcolor), P7.X + (P5.X - P3.X) / 5 * i, P7.Y + 10);
                    }
                    #endregion

                    #region 绘制有效的深度测井曲线
                    if (textBox1.Text == string.Empty)
                    {
                        int Starti = 0;
                        for (int i = 1; i < SysData.dt.Rows.Count; i++)
                        {
                            if (Convert.ToSingle(SysData.dt.Rows[i][1]) > 0 - 5000 && Convert.ToSingle(SysData.dt.Rows[i][2]) > -5000)//要保证横纵坐标是对应的（成对的坐标）
                            {
                                Starti = i;
                                break;
                            }
                        }
                        int end_i = 0;
                        for (int i = Starti; i < SysData.dt.Rows.Count; i++)
                        {
                            if (Convert.ToSingle(SysData.dt.Rows[i][0]) == (Convert.ToSingle(SysData.dt.Rows[Starti][0]) + 40))
                            {
                                end_i = i;
                                break;
                            }
                        }
                        int Temp_i = Starti;//用来替代start_i

                        PointF[] Left_road = new PointF[end_i - Starti + 1];
                        PointF[] Right_road = new PointF[end_i - Starti + 1];
                        for (int i = 0; i < end_i - Starti + 1; i++)
                        {
                            Left_road[i].X = P3.X + (Convert.ToSingle(SysData.dt.Rows[Temp_i][1]) - MinX) * cjKx;
                            Right_road[i].X = P3.X + (Convert.ToSingle(SysData.dt.Rows[Temp_i][2]) - MinY) * cjKy;
                            Right_road[i].Y = Left_road[i].Y = P7.Y + (Convert.ToSingle(SysData.dt.Rows[Temp_i][0]) - Convert.ToSingle(SysData.dt.Rows[Starti][0])) * ((P9.Y - P7.Y) / 40);
                            Temp_i = Temp_i + 1;
                        }
                        g.DrawLines(new Pen(Color.Black, 1), Left_road);
                        g.DrawLines(new Pen(Color.Green, 1), Right_road);



                        //绘制刻度字
                        for (int i = 1; i < 4; i++)
                        {
                            //10为每个大格所代表深度（每个小刻度代表1）,10=40/4，40是所有深度，4为四个大格
                            g.DrawString((Convert.ToSingle(SysData.dt.Rows[Starti][0]) + i * 10).ToString("F0"), myYfont, new SolidBrush(myYcolor), P5.X + (P6.X - P5.X) / 3, P7.Y - 5 + i * 100);
                        }
                    }
                    #endregion


                    #region 绘制对应刻度测井曲线
                    else
                    {
                        // Draw_cjqx_lines();
                        // Graphics g = this.CreateGraphics();
                        int start_i = 0;
                        for (int i = 0; i < SysData.dt.Rows.Count; i++)
                        {
                            if (Convert.ToSingle(SysData.dt.Rows[i][0]) == Convert.ToSingle(textBox1.Text))
                            {
                                start_i = i;
                                break;
                            }
                        }

                        int end_i1 = 0;
                        for (int i = start_i; i < SysData.dt.Rows.Count; i++)
                        {
                            if (Convert.ToSingle(SysData.dt.Rows[i][0]) == (Convert.ToSingle(textBox1.Text) + Convert.ToSingle(textBox2.Text)))
                            {
                                end_i1 = i;
                                break;
                            }
                        }

                        int temp_i1 = start_i;//用来替代start_i

                        //PointF[] Left_road=new PointF[SysData.dt.Rows.Count-start_i];
                        //PointF[] Right_road = new PointF[SysData.dt.Rows.Count - start_i];
                        PointF[] Left_road = new PointF[end_i1 - start_i + 1];
                        PointF[] Right_road = new PointF[end_i1 - start_i + 1];
                        for (int i = 0; i < end_i1 - start_i + 1; i++)
                        {
                            Left_road[i].X = P3.X + (Convert.ToSingle(SysData.dt.Rows[temp_i1][1]) - MinX) * cjKx;
                            Right_road[i].X = P3.X + (Convert.ToSingle(SysData.dt.Rows[temp_i1][2]) - MinY) * cjKy;
                            Right_road[i].Y = Left_road[i].Y = P7.Y + (Convert.ToSingle(SysData.dt.Rows[temp_i1][0]) - Convert.ToSingle(SysData.dt.Rows[start_i][0])) * ((P9.Y - P7.Y) / Convert.ToSingle(textBox2.Text));
                            temp_i1 = temp_i1 + 1;
                        }
                        g.DrawLines(new Pen(Color.Black, 1), Left_road);
                        g.DrawLines(new Pen(Color.Green, 1), Right_road);
                        //绘制刻度字
                        for (int i = 1; i < 4; i++)
                        {
                            g.DrawString((Convert.ToSingle(SysData.dt.Rows[start_i][0]) + i * Convert.ToSingle(textBox2.Text)/4).ToString("F0"), myYfont, new SolidBrush(myYcolor), P5.X + (P6.X - P5.X) / 3, P7.Y - 5 + i * 100);
                        }
                    }

                    #endregion

                    //主图 zt=new 主图();
                    //联动红线
                    if (start_count != null && end_count != null && SysData.button_cancel == false)
                    {
                        for (int i = 0; i < start_count.Count; i++)
                        {
                            g.DrawLine(new Pen(Color.Red, 1), P7.X, P7.Y + (Convert.ToSingle(SysData.dt.Rows[end_count[i]][0]) - Convert.ToSingle(SysData.dt.Rows[start_count[i]][0])) * ((P9.Y - P7.Y) / 40), P5.X, P7.Y + (Convert.ToSingle(SysData.dt.Rows[end_count[i]][0]) - Convert.ToSingle(SysData.dt.Rows[start_count[i]][0])) * ((P9.Y - P7.Y) / 40));
                            //g.DrawLine(new Pen(Color.Red, 1), P5.X, P7.Y + (Convert.ToSingle(SysData.dt.Rows[end_count[i]][0]) - Convert.ToSingle(SysData.dt.Rows[start_count[i]][0])) * ((P9.Y - P7.Y) / 40), P8.X, P7.Y + (Convert.ToSingle(SysData.dt.Rows[end_count[i]][0]) - Convert.ToSingle(SysData.dt.Rows[start_count[i]][0])) * ((P9.Y - P7.Y) / 40));

                        }

                    }
                    if (SysData.button_cancel)
                    {
                        start_count.Clear();
                        end_count.Clear();
                    }
                }
            }
            #endregion


            #region 不合并曲线道

            else
            {
                #region 画网格以及边框



                g.DrawLine(pb, P1, P2);
                g.DrawLine(pb, P1, P9);
                g.DrawLine(pb, P2, P10);
                g.DrawLine(pb, P9, P10);


                g.DrawLine(pb, P3, P6);
                g.DrawLine(pb, P4.X, P4.Y, P4.X, P9.Y);
                g.DrawLine(pb, P5.X, P5.Y, P5.X, P9.Y);
                g.DrawLine(pb, P7, P8);
                //曲线道 网格横线（灰色）
                for (int i = 1; i < 20; i++)
                {
                    g.DrawLine(pn, P7.X, P7.Y + (P9.Y - P7.Y) / 20 * i, P4.X, P7.Y + (P9.Y - P7.Y) / 20 * i);
                    g.DrawLine(pn, P5.X, P7.Y + (P9.Y - P7.Y) / 20 * i, P8.X, P7.Y + (P9.Y - P7.Y) / 20 * i);

                }
                //曲线道 网格纵线（灰色）
                for (int i = 1; i < 5; i++)
                {
                    g.DrawLine(pn, P9.X + (P4.X - P3.X) / 5 * i, P9.Y, P9.X + (P4.X - P3.X) / 5 * i, P7.Y);
                    g.DrawLine(pn, P5.X + (P4.X - P3.X) / 5 * i, P9.Y, P5.X + (P4.X - P3.X) / 5 * i, P7.Y);
                }
                //深度道 刻度（黑色）
                for (int i = 1; i < 40; i++)
                {
                    g.DrawLine(new Pen(Color.Black, 1), P5.X, P8.Y + (P9.Y - P7.Y) / 40 * i, P5.X - 6, P8.Y + (P9.Y - P7.Y) / 40 * i);
                }
                for (int i = 1; i < 4; i++)
                {
                    g.DrawLine(new Pen(Color.Black, 1), P5.X, P8.Y + (P9.Y - P7.Y) / 4 * i, P5.X - 9, P8.Y + (P9.Y - P7.Y) / 4 * i);
                    g.DrawLine(new Pen(Color.Black, 2), P7.X, P7.Y + (P9.Y - P7.Y) / 4 * i, P4.X, P7.Y + (P9.Y - P7.Y) / 4 * i);
                    g.DrawLine(new Pen(Color.Black, 2), P5.X, P7.Y + (P9.Y - P7.Y) / 4 * i, P8.X, P7.Y + (P9.Y - P7.Y) / 4 * i);
                }
                #endregion


                #region 绘字
                g.DrawString(System.IO.Path.GetFileNameWithoutExtension(SysData.abovefile.FileName), MyYnameFont, new SolidBrush(MyYnameColor), P1.X, P1.Y + (P3.Y - P1.Y) / 3);
                g.DrawString("曲线道X", MyXnameFont, new SolidBrush(MyXnameColor), P3.X + (P4.X - P3.X) / 4, P3.Y + (P7.Y - P3.Y) / 3);
                g.DrawString("曲线道Y", MyXnameFont, new SolidBrush(MyXnameColor), P5.X + (P6.X - P5.X) / 4, P3.Y + (P7.Y - P3.Y) / 3);
                g.DrawString("深度", MyXnameFont, new SolidBrush(MyXnameColor), P4.X + (P5.X - P4.X) / 6, P4.Y + (P7.Y - P3.Y) / 3);

                #endregion

                //数据处理
                if (SysData.dt != null)
                {
                    for (int i = 1; i < SysData.dt.Rows.Count; i++)
                    {
                        if (Convert.ToSingle(SysData.dt.Rows[i][1]) > -5000 && Convert.ToSingle(SysData.dt.Rows[i][2]) > -5000)
                        {
                            tempdtX.Add(Convert.ToSingle(SysData.dt.Rows[i][1]));
                            tempdtY.Add(Convert.ToSingle(SysData.dt.Rows[i][2]));
                        }

                    }
                    tempdtX.Sort();
                    tempdtY.Sort();

                    //Console.Write(tempdtX[tempdtX.Count - 1]);
                    ////////给测井曲线的数据
                    cjKx = (P4.X - P3.X) / (tempdtX[tempdtX.Count - 1] - tempdtX[0]);
                    cjKy = (P6.X - P5.X) / (tempdtY[tempdtY.Count - 1] - tempdtY[0]);

                    MinX = tempdtX[0];

                    MinY = tempdtY[0];

                    #region 曲线道绘字
                    for (int i = 0; i < 6; i++)
                    {
                        g.DrawString((MinX + (tempdtX[tempdtX.Count - 1] - tempdtX[0]) / 5 * i).ToString("F1"), myXfont, new SolidBrush(myXcolor), P7.X + (P4.X - P3.X) / 5 * i, P7.Y - 10);
                        g.DrawString((MinY + (tempdtY[tempdtY.Count - 1] - tempdtY[0]) / 5 * i).ToString("F1"), myXfont, new SolidBrush(myXcolor), P5.X + (P6.X - P5.X) / 5 * i, P7.Y - 10);
                    }
                    #endregion

                    #region 绘制有效的深度测井曲线
                    if (textBox1.Text == string.Empty)
                    {
                        int Starti = 0;
                        for (int i = 1; i < SysData.dt.Rows.Count; i++)
                        {
                            if (Convert.ToSingle(SysData.dt.Rows[i][1]) > 0 && Convert.ToSingle(SysData.dt.Rows[i][2]) > 0)//要保证横纵坐标是对应的（成对的坐标）
                            {
                                Starti = i;
                                break;
                            }
                        }
                        int end_i = 0;
                        for (int i = Starti; i < SysData.dt.Rows.Count; i++)
                        {
                            if (Convert.ToSingle(SysData.dt.Rows[i][0]) == (Convert.ToSingle(SysData.dt.Rows[Starti][0]) + 40))
                            {
                                end_i = i;
                                break;
                            }
                        }
                        int Temp_i = Starti; ;//用来替代start_i

                        PointF[] Left_road = new PointF[end_i - Starti + 1];
                        PointF[] Right_road = new PointF[end_i - Starti + 1];
                        for (int i = 0; i < end_i - Starti + 1; i++)
                        {
                            Left_road[i].X = P3.X + (Convert.ToSingle(SysData.dt.Rows[Temp_i][1]) - MinX) * cjKx;
                            Right_road[i].X = P5.X + (Convert.ToSingle(SysData.dt.Rows[Temp_i][2]) - MinY) * cjKy;
                            Right_road[i].Y = Left_road[i].Y = P7.Y + (Convert.ToSingle(SysData.dt.Rows[Temp_i][0]) - Convert.ToSingle(SysData.dt.Rows[Starti][0])) * ((P9.Y - P7.Y) / 40);
                            Temp_i = Temp_i + 1;
                        }
                        g.DrawLines(new Pen(Color.Black, 1), Left_road);
                        g.DrawLines(new Pen(Color.Black, 1), Right_road);



                        //绘制刻度字
                        for (int i = 1; i < 4; i++)
                        {
                            //10为每个大格所代表深度（每个小刻度代表1）,10=40/4，40是所有深度，4为四个大格
                            g.DrawString((Convert.ToSingle(SysData.dt.Rows[Starti][0]) + i * 10).ToString("F0"), myYfont, new SolidBrush(myYcolor), P4.X + (P5.X - P4.X) / 5, P7.Y - 5 + i * 100);
                        }
                    }
                    #endregion


                    #region 绘制对应刻度测井曲线
                    else
                    {
                        // Draw_cjqx_lines();
                        // Graphics g = this.CreateGraphics();
                        int start_i = 0;
                        for (int i = 0; i < SysData.dt.Rows.Count; i++)
                        {
                            if (Convert.ToSingle(SysData.dt.Rows[i][0]) == Convert.ToSingle(textBox1.Text))
                            {
                                start_i = i;
                                break;
                            }
                        }

                        int end_i1 = 0;
                        for (int i = start_i; i < SysData.dt.Rows.Count; i++)
                        {
                            if (Convert.ToSingle(SysData.dt.Rows[i][0]) == (Convert.ToSingle(textBox1.Text) + Convert.ToSingle(textBox2.Text)))
                            {
                                end_i1 = i;
                                break;
                            }
                        }

                        int temp_i1 = start_i; ;//用来替代start_i

                        //PointF[] Left_road=new PointF[SysData.dt.Rows.Count-start_i];
                        //PointF[] Right_road = new PointF[SysData.dt.Rows.Count - start_i];
                        PointF[] Left_road = new PointF[end_i1 - start_i + 1];
                        PointF[] Right_road = new PointF[end_i1 - start_i + 1];
                        for (int i = 0; i < end_i1 - start_i + 1; i++)
                        {
                            Left_road[i].X = P3.X + (Convert.ToSingle(SysData.dt.Rows[temp_i1][1]) - MinX) * cjKx;
                            Right_road[i].X = P5.X + (Convert.ToSingle(SysData.dt.Rows[temp_i1][2]) - MinY) * cjKy;
                            Right_road[i].Y = Left_road[i].Y = P7.Y + (Convert.ToSingle(SysData.dt.Rows[temp_i1][0]) - Convert.ToSingle(SysData.dt.Rows[start_i][0])) * ((P9.Y - P7.Y) / Convert.ToSingle(textBox2.Text));
                            temp_i1 = temp_i1 + 1;
                        }
                        g.DrawLines(new Pen(Color.Black, 1), Left_road);
                        g.DrawLines(new Pen(Color.Black, 1), Right_road);
                        //绘制刻度字
                        for (int i = 1; i < 4; i++)
                        {
                            g.DrawString((Convert.ToSingle(SysData.dt.Rows[start_i][0]) + i * Convert.ToSingle(textBox2.Text) / 4).ToString("F0"), myYfont, new SolidBrush(myYcolor), P4.X + (P5.X - P4.X) / 5, P7.Y - 5 + i * 100);
                        }
                    }

                    #endregion

                    //主图 zt=new 主图();
                    //联动红线
                    if (start_count != null && end_count != null && SysData.button_cancel == false)
                    {
                        for (int i = 0; i < start_count.Count; i++)
                        {
                            g.DrawLine(new Pen(Color.Red, 1), P7.X, P7.Y + (Convert.ToSingle(SysData.dt.Rows[end_count[i]][0]) - Convert.ToSingle(SysData.dt.Rows[start_count[i]][0])) * ((P9.Y - P7.Y) / 40), P4.X, P7.Y + (Convert.ToSingle(SysData.dt.Rows[end_count[i]][0]) - Convert.ToSingle(SysData.dt.Rows[start_count[i]][0])) * ((P9.Y - P7.Y) / 40));
                            g.DrawLine(new Pen(Color.Red, 1), P5.X, P7.Y + (Convert.ToSingle(SysData.dt.Rows[end_count[i]][0]) - Convert.ToSingle(SysData.dt.Rows[start_count[i]][0])) * ((P9.Y - P7.Y) / 40), P8.X, P7.Y + (Convert.ToSingle(SysData.dt.Rows[end_count[i]][0]) - Convert.ToSingle(SysData.dt.Rows[start_count[i]][0])) * ((P9.Y - P7.Y) / 40));

                        }

                    }
                    if (SysData.button_cancel)
                    {
                        start_count.Clear();
                        end_count.Clear();
                    }
                }
            }
            #endregion


            
            Save_printImage();

        }
        #region 最小二乘法计算拟合方程
        ///<summary>
        ///用最小二乘法拟合二元多次曲线
        ///例如y=ax+b
        ///其中MultiLine将返回a，b两个参数。
        ///a对应MultiLine[1]
        ///b对应MultiLine[0]
        ///</summary>
        ///<param name="arrX">已知点的x坐标集合</param>
        ///<param name="arrY">已知点的y坐标集合</param>
        ///<param name="length">已知点的个数</param>
        ///<param name="dimension">方程的最高次数</param>
        public double[] MultiLine(double[] arrX, double[] arrY, int length, int dimension)//二元多次线性方程拟合曲线
        {
            int n = dimension + 1;                  //dimension次方程需要求 dimension+1个 系数
            double[,] Guass = new double[n, n + 1];      //高斯矩阵 例如：y=a0+a1*x+a2*x*x
            for (int i = 0; i < n; i++)
            {
                int j;
                for (j = 0; j < n; j++)
                {
                    Guass[i, j] = SumArr(arrX, j + i, length);
                }
                Guass[i, j] = SumArr(arrX, i, arrY, 1, length);
            }

            return ComputGauss(Guass, n);

        }
        private double SumArr(double[] arr, int n, int length) //求数组的元素的n次方的和
        {
            double s = 0;
            for (int i = 0; i < length; i++)
            {
                if (arr[i] != 0 || n != 0)
                    s = s + Math.Pow(arr[i], n);
                else
                    s = s + 1;
            }
            return s;
        }
        private double SumArr(double[] arr1, int n1, double[] arr2, int n2, int length)
        {
            double s = 0;
            for (int i = 0; i < length; i++)
            {
                if ((arr1[i] != 0 || n1 != 0) && (arr2[i] != 0 || n2 != 0))
                    s = s + Math.Pow(arr1[i], n1) * Math.Pow(arr2[i], n2);
                else
                    s = s + 1;
            }
            return s;

        }
        private double[] ComputGauss(double[,] Guass, int n)
        {
            int i, j;
            int k, m;
            double temp;
            double max;
            double s;
            double[] x = new double[n];

            for (i = 0; i < n; i++) x[i] = 0.0;//初始化


            for (j = 0; j < n; j++)
            {
                max = 0;

                k = j;
                for (i = j; i < n; i++)
                {
                    if (Math.Abs(Guass[i, j]) > max)
                    {
                        max = Guass[i, j];
                        k = i;
                    }
                }



                if (k != j)
                {
                    for (m = j; m < n + 1; m++)
                    {
                        temp = Guass[j, m];
                        Guass[j, m] = Guass[k, m];
                        Guass[k, m] = temp;

                    }
                }

                if (0 == max)
                {
                    // "此线性方程为奇异线性方程" 

                    return x;
                }


                for (i = j + 1; i < n; i++)
                {

                    s = Guass[i, j];
                    for (m = j; m < n + 1; m++)
                    {
                        Guass[i, m] = Guass[i, m] - Guass[j, m] * s / (Guass[j, j]);

                    }
                }


            }//结束for (j=0;j<n;j++)


            for (i = n - 1; i >= 0; i--)
            {
                s = 0;
                for (j = i + 1; j < n; j++)
                {
                    s = s + Guass[i, j] * x[j];
                }

                x[i] = (Guass[i, n] - s) / Guass[i, i];

            }

            return x;
        }//返回值是函数的系数

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("请输入起始深度");
            }

            else
            {
                update_draw();


                //更新主图
                Panel_refresh();

                selfrefresh();
                //Draw_cjqx_lines();
            }
           

           
            

        }


        /// <summary>
        /// 联动
        /// </summary>
        /// <param name="un_i"></param>
        public void DrawRedline(int un_i,int start_i)
        {
            start_count.Add(start_i);
            end_count.Add(un_i);
            Graphics g = this.CreateGraphics();
            g.DrawLine(new Pen(Color.Red, 1), P7.X, P7.Y + (Convert.ToSingle(SysData.dt.Rows[un_i][0]) - Convert.ToSingle(SysData.dt.Rows[start_i][0])) * ((P9.Y - P7.Y) / 40), P4.X, P7.Y + (Convert.ToSingle(SysData.dt.Rows[un_i][0]) - Convert.ToSingle(SysData.dt.Rows[start_i][0])) * ((P9.Y - P7.Y) / 40));
            g.DrawLine(new Pen(Color.Red, 1), P5.X, P7.Y + (Convert.ToSingle(SysData.dt.Rows[un_i][0]) - Convert.ToSingle(SysData.dt.Rows[start_i][0])) * ((P9.Y - P7.Y) / 40), P8.X, P7.Y + (Convert.ToSingle(SysData.dt.Rows[un_i][0]) - Convert.ToSingle(SysData.dt.Rows[start_i][0])) * ((P9.Y - P7.Y) / 40));
        }
        public void update_draw()
        {
            this.Invalidate();//清除已绘制的
            this.Update();//重新绘制
        }
        public void selfrefresh()
        {
            this.Refresh();
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

        private void 测井曲线_DoubleClick(object sender, EventArgs e)
        {
            if (SysData.dt == null)
            {
                Way_DataLoad wdl = new Way_DataLoad();
                wdl.ShowDialog();
                if (SysData.dt != null)
                {
                    this.Refresh();
                    Panel_refresh();
                    //cjsj.Show();
                }
            }
            else
            {
                Setofline_welllog setline = new Setofline_welllog(this);
                setline.paint_refresh += selfrefresh;
                setline.ShowDialog();
            }
        }

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
            MyObject.FrmName2 = "测井曲线";
           // 复制窗体,把窗体内容序列化
             FormState fs = new FormState(MyText, MyColor, MyFont, MyLineColor1, MyLineColor2, MyLine1, MyLine2, SysData.dt,MyXBrush,MyYBrush,MyXFont,MyYFont,MyXnameFont,MyXnameColor,MyYnameFont,MyYnameColor);
            XmlHelper.SaveForm(fs);
            MyObject.FrmName1 = "测井曲线";//如果萨胡成因的窗体被点击了，那么就把名字给当前操作窗体的名字
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MyObject.FrmName1 = null;
            this.Close();
        }

        private void 测井曲线_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            MyObject.FrmName2 = "测井曲线";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            selfrefresh();
        }

        private void WellLog_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
        }

        private void WellLog_MouseUp(object sender, MouseEventArgs e)
        {
            MainFrame mf = new MainFrame();
            mf.getMsg();
        }
        
    }
}
