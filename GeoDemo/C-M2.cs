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
    public partial class C_M2 : Form
    {
        public Font MyFont = SysData.title_font;
        public Color MyColor = SysData.title_color;
        public string MyText = "C_M2";
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
        public string MyXnameText = "X轴";

        public Font MyYnameFont = new Font("宋体", 12, FontStyle.Regular);
        public Color MyYnameColor = Color.Black;
        public string MyYnameText = "Y轴";

        //判断垂直
        public bool MyXCheck = false;
        public bool MyYCheck = false;


        public C_M2()
        {
            InitializeComponent();
        }

        private void C_M2_Paint(object sender, PaintEventArgs e)
        {
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
            Bitmap bit = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bit);
            Pen p = new Pen(MyLineColor1, MyLine1);
            Pen pb = new Pen(MyLineColor2, MyLine2);
            Pen p_Green = new Pen(Color.Green, 2);
            Font myXfont = MyXFont;
            Font myYfont = MyYFont;
            Color myXcolor = MyXBrush;
            Color myYcolor = MyYBrush;


            //画x轴
            double X_multiple = 10;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j <= 5;j++ )
                {
                    g.DrawLine(p, ChangeToX(2 * j * X_multiple), ChangeToY(10000), ChangeToX(2 * j * X_multiple), ChangeToY(2*j*X_multiple));
                }
                X_multiple = X_multiple * 10;
            }
            

            //画10,15,4
            g.DrawLine(p, ChangeToX(4), ChangeToY(10000), ChangeToX(4), ChangeToY(20));
            g.DrawLine(p, ChangeToX(10), ChangeToY(10000), ChangeToX(10), ChangeToY(20)+5);
            g.DrawString(Convert.ToString(10), myXfont, new SolidBrush(myXcolor), ChangeToX(10) - 5, ChangeToY(20) + 10);
            g.DrawLine(p, ChangeToX(15), ChangeToY(10000), ChangeToX(15), ChangeToY(20)+5);
            g.DrawString(Convert.ToString(15), myXfont, new SolidBrush(myXcolor), ChangeToX(15) - 5, ChangeToY(20) + 10);
            //画y轴
            double Y_multiple = 10;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    if (2 * j * Y_multiple <= 1000)
                    {
                        g.DrawLine(p, ChangeToX(4), ChangeToY(j * 2 * Y_multiple), ChangeToX(2 * j * Y_multiple) + 5, ChangeToY(j * 2 * Y_multiple));
                        g.DrawString(Convert.ToString(2 * j * Y_multiple), myYfont, new SolidBrush(myYcolor), ChangeToX(2 * j * Y_multiple) + 10, ChangeToY(j * 2 * Y_multiple) - 7);
                    }
                    else
                    {
                        g.DrawLine(p, ChangeToX(4), ChangeToY(j * 2 * Y_multiple), ChangeToX(1000) + 5, ChangeToY(j * 2 * Y_multiple));
                        g.DrawString(Convert.ToString(2 * j * Y_multiple), myYfont, new SolidBrush(myYcolor), ChangeToX(1000) + 10, ChangeToY(j * 2 * Y_multiple) - 7);
                    }

                }
                Y_multiple = Y_multiple * 10;
            }
            
            //汇左边字
            double Name_multiple = 10;
            for (int i = 0; i < 3;i++ )
            {
                
                g.DrawString(Convert.ToString(10 * Name_multiple), myYfont, new SolidBrush(myYcolor), ChangeToX(4) - 43, ChangeToY(10 * Name_multiple)-5);
                Name_multiple = Name_multiple * 10;
            }


            //g.DrawString("M,μm", new Font("宋体", 20, FontStyle.Regular), new SolidBrush(Color.Black), ChangeToX(6), ChangeToY(20) + 15);
            //Graphics g1 = Graphics.FromImage(bit);
            //g1.RotateTransform(-90);
            //g1.DrawString("C,μm", new Font("宋体", 20, FontStyle.Regular), new SolidBrush(Color.Black), -300,50);

          


            //画出C=M
            g.DrawLine(pb, ChangeToX(20), ChangeToY(20), ChangeToX(1000), ChangeToY(1000));
            //重绘边框
            g.DrawLine(pb, ChangeToX(4), ChangeToY(20), ChangeToX(20), ChangeToY(20));
            g.DrawLine(pb, ChangeToX(4), ChangeToY(20), ChangeToX(4), ChangeToY(10000));
            g.DrawLine(pb, ChangeToX(4), ChangeToY(10000), ChangeToX(1000), ChangeToY(10000));
            g.DrawLine(pb, ChangeToX(1000), ChangeToY(1000), ChangeToX(1000), ChangeToY(10000));


            //绘制特殊部分
            #region 
            //绘制圆3
            g.DrawEllipse(new Pen(Color.Green,2), ChangeToX(4), ChangeToY(60), ChangeToX(10) - ChangeToX(4), ChangeToY(20) - ChangeToY(60));
            
            //绘制2
            PointF P1, P2, P3, P4,P0,P00;
            
            P0 = new PointF(ChangeToX(200), ChangeToY(1100));
            P1 = new PointF(ChangeToX(150), YLine(150, 15, 100));
            P2 = new PointF(XLine(400, 60, 40), ChangeToY(400));
            P3 = new PointF(XLine(20, 60, 40), ChangeToY(20));
            P4 = new PointF(XLine(55, 100, 15), ChangeToY(55));
            P00 = new PointF(P2.X + 5, ChangeToY(800));
            g.DrawLine(p_Green, P1, P4);
            g.DrawLine(p_Green, P2, P3);
            PointF[] Dra_P = { P1,  P0,P00,P2 };
            g.DrawBeziers(p_Green, Dra_P);


            //绘制1
            PointF S1, S2, S3, S4,S0,Q,Q1,Q0,Q00,O1,O2,N1,N2,N3,N4,N5;
            S0 = new PointF(ChangeToX(15), ChangeToY(200));
            S1 = new PointF(ChangeToX(16.5), ChangeToY(240));
            S2 = new PointF(ChangeToX(105), ChangeToY(240));
            S3 = new PointF(ChangeToX(110), YLine(110, 40, 60));
            S4 = new PointF(S1.X,S3.Y);
            Q = new PointF(XLine(400, 240, 105), ChangeToY(400));
            Q1 = new PointF(Q.X, P1.Y);
            O1 = new PointF(ChangeToX(210), ChangeToY(1500));
            Q0 = new PointF(ChangeToX(200),ChangeToY(1440));
            Q00 = new PointF(Q.X, ChangeToY(1400));
            O2 = new PointF(ChangeToX(600), ChangeToY(1550));
            N1 = new PointF(ChangeToX(800), ChangeToY(1900));
            N2 = new PointF(ChangeToX(800), ChangeToY(1400));
            N3 = new PointF(ChangeToX(550), ChangeToY(1050));
            N4 = new PointF(ChangeToX(240), ChangeToY(990));
            N5 = new PointF(ChangeToX(220), YLine(220, 40, 60));

            g.DrawLine(p_Green, S1, S2);
            g.DrawLine(p_Green, S3, S4);
            g.DrawLine(p_Green, S2, Q);
            g.DrawLine(p_Green, Q, Q1);


            PointF[] Dra_Q = { Q1, Q00,Q0, O1 ,O2,N1,N2,N3,N4,N5};
            g.DrawCurve(p_Green, Dra_Q);
            PointF[] Dra_S={S1,S0,S4};
            g.DrawCurve(p_Green,Dra_S);

            panel1.BackgroundImage = bit;
            SysData.PrintBit = bit;
            #endregion
            /////////////////////////////////////////////////////////////////////////绘点
            if (SysData.cm2_dt != null)
            {
                double total_x = 0;
                double total_y = 0;
                for (int i = 1; i < SysData.cm2_dt.Rows.Count; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.Black), ChangeToX(Convert.ToDouble(SysData.cm2_dt.Rows[i][1]) * 1000), ChangeToY(Convert.ToDouble(SysData.cm2_dt.Rows[i][0]) * 1000), 4, 4);
                    total_x = total_x + Convert.ToDouble(SysData.cm2_dt.Rows[i][1]);
                    total_y = total_y + Convert.ToDouble(SysData.cm2_dt.Rows[i][0]);
                }
                double average_x = total_x / (SysData.cm2_dt.Rows.Count - 1);
                double average_y = total_y / (SysData.cm2_dt.Rows.Count - 1);
            }
            #region 绘制xy轴字
            //x
            g.TranslateTransform(this.Width / 2, this.Height - 50);
            if (MyXCheck)
            {
                g.RotateTransform(90);
            }
            g.DrawString(MyXnameText, MyXnameFont, new SolidBrush(MyXnameColor), 0, 0);
            //y
            g.ResetTransform();
            g.TranslateTransform(30, this.Height / 2);
            if (MyYCheck)
            {
                g.RotateTransform(90);
            }
            g.DrawString(MyYnameText, MyYnameFont, new SolidBrush(MyYnameColor), 0, 0);
            #endregion

        }
        /// <summary>
        /// 对应到屏幕x轴的值。根据form的大小确定的放大倍数
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private float ChangeToX(double x)
        {
            double X ;

            X = Math.Log10(x) * 200 * SysData.K_Width;
            
            return Convert.ToSingle(X);
        }
        /// <summary>
        /// 同理上面的ChangeToX,传入的y值是对应的y值除以10以便对应屏幕坐标
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private float ChangeToY(double y)
        {
            double Y ;

            Y = (445 - Math.Log10(y / 10) * 140) * SysData.K_Height;
            
            return Convert.ToSingle(Y);
        }
        /// <summary>
        /// 传入x，x1，y1值返回相应的y值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        private float YLine(double x, double x1, double y1)
        {
            double Slope = 0;//斜率
            Slope = (ChangeToY(1000) - ChangeToY(20)) / (ChangeToX(1000) - ChangeToX(20));
            return Convert.ToSingle(Slope * (ChangeToX(x) - ChangeToX(x1)) + ChangeToY(y1));
        }
        /// <summary>
        /// 返回相应x值
        /// </summary>
        /// <param name="y"></param>
        /// <param name="y1"></param>
        /// <param name="x1"></param>
        /// <returns></returns>
        private float XLine(double y, double y1, double x1)
        {
            double Slope = 0;//斜率
            Slope = (ChangeToY(1000) - ChangeToY(20)) / (ChangeToX(1000) - ChangeToX(20));
            return Convert.ToSingle((ChangeToY(y) - ChangeToY(y1)) / Slope + ChangeToX(x1));
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


        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            if (SysData.cm2_dt == null)
            {
                Data_Special dqsj_form = new Data_Special();
                dqsj_form.ShowDialog();
                this.Refresh();
            }
            else
            {
                Setofline_cm2 setline = new Setofline_cm2(this);
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
            MyObject.MyCurrentFrmName1 = null;
            //复制窗体,把窗体内容序列化
            FormState fs = new FormState(MyText, MyColor, MyFont, MyLineColor1, MyLineColor2, MyLine1, MyLine2, SysData.cm2_dt, MyXBrush, MyYBrush, MyXFont,
                                          MyYFont, MyXnameFont, MyXnameColor, MyXnameText, MyYnameFont, MyYnameColor, MyYnameText, MyXCheck, MyYCheck);
            XmlHelper.SaveForm(fs);
            MyObject.FrmName1 = this.Name;//如果萨胡成因的窗体被点击了，那么就把名字给当前操作窗体的名字
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MyObject.FrmName1 = null;

            this.Close();
        }

        private void C_M2_Load(object sender, EventArgs e)
        {
            this.BringToFront();
           MyObject.FrmName2 = "C_M2";
        }

        private void C_M2_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
            MyObject.FrmName2 = "C_M2";
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
            MyObject.FrmName2 = "C_M2";
        }

        private void C_M2_MouseUp(object sender, MouseEventArgs e)
        {
            MainFrame mf = new MainFrame();
            mf.getMsg();
        }
    }
}
