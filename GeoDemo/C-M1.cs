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
    public partial class C_M1 : Form
    {
        

        //标题
        public Font MyFont = SysData.title_font;
        public Color MyColor = SysData.title_color;
        public string MyText = "C_M1";
        

        public Color MyLineColor1 = SysData.line_color;
        public Color MyLineColor2 = SysData.line_color2;
        public int MyLine1 = SysData.line1;
        public int MyLine2 = SysData.line2;


        public DataTable MyDatatable = SysData.cm1_dt;
        
        //刻度
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

        public C_M1()
        {
            InitializeComponent();
           
            //label1.Location = new Point(this.Width / 2, 10);
        }




        private void Form3_Paint(object sender, PaintEventArgs e)
        {
            //MyFont = SysData.title_font;
            //MyColor = SysData.title_color;
            //MyText = SysData.title;
            //MyLineColor1 = SysData.line_color;
            //MyLineColor2 = SysData.line_color2;
            //MyLine1 = SysData.line1;
            //MyLine2 = SysData.line2;
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
            //if (SysData.MainFont != new Font("宋体", 12, FontStyle.Regular))
            //{
            //    label1.Font = SysData.MainFont;
            //}

            Bitmap bit = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bit);
            //Graphics g = this.pictureBox1.CreateGraphics();
            //先画4-9
            for (int i = 4; i < 10; i++)
            {
                g.DrawLine(p, ChangeToX(i), ChangeToY(10), ChangeToX(i), ChangeToY(2000));
            }
            //再画剩下的
            //画出x轴的线
            double X_multiple = 10;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    g.DrawLine(p, ChangeToX(X_multiple * j), ChangeToY(X_multiple * j), ChangeToX(X_multiple * j), ChangeToY(2000));
                }
                X_multiple = X_multiple * 10;
            }
            //画出Y轴的线
            double Y_multiple = 10;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    g.DrawLine(p, ChangeToX(4), ChangeToY(Y_multiple * j), ChangeToX(Y_multiple * j), ChangeToY(Y_multiple * j));
                }
                Y_multiple = Y_multiple * 10;
            }
            //连接C=M的直线
            g.DrawLine(p, ChangeToX(10), ChangeToY(10), ChangeToX(1000), ChangeToY(1000));
            //画出四个边框
            g.DrawLine(Pw, ChangeToX(4), ChangeToY(10), ChangeToX(1000), ChangeToY(10));
            g.DrawLine(Pw, ChangeToX(4), ChangeToY(10), ChangeToX(4), ChangeToY(2000));
            g.DrawLine(Pw, ChangeToX(4), ChangeToY(2000), ChangeToX(1000), ChangeToY(2000));
            g.DrawLine(Pw, ChangeToX(1000), ChangeToY(10), ChangeToX(1000), ChangeToY(2000));

            /////////绘制XY轴字
            ////x
            //Graphics gx = this.pictureBox1.CreateGraphics();
            //gx.TranslateTransform(this.pictureBox1.Width / 2, this.pictureBox1.Height - 10);
            //gx.DrawString(MyXnameText, MyXnameFont, new SolidBrush(MyXnameColor), 0, 0);


            /////////
            ////绘制x轴文字
            X_multiple = 10;
            for (int i = 0; i < 2; i++)
            {
                g.DrawString(Convert.ToString(X_multiple), myXfont, new SolidBrush(myXcolor), ChangeToX(X_multiple) - 5, ChangeToY(10) + 5);
                g.DrawString(Convert.ToString(X_multiple*2), myXfont, new SolidBrush(myXcolor), ChangeToX(X_multiple*2) - 5, ChangeToY(10) + 5);
                g.DrawString(Convert.ToString(X_multiple*3), myXfont, new SolidBrush(myXcolor), ChangeToX(X_multiple*3) - 5, ChangeToY(10) + 5);
                g.DrawString(Convert.ToString(X_multiple * 5), myXfont, new SolidBrush(myXcolor), ChangeToX(X_multiple * 5) - 5, ChangeToY(10) + 5);
               
                X_multiple = X_multiple * 10.0;
            }
            g.DrawString(Convert.ToString(1000), myXfont, new SolidBrush(myXcolor), ChangeToX(1000) - 5, ChangeToY(10) + 5);
            g.DrawString(Convert.ToString(5), myXfont, new SolidBrush(myXcolor), ChangeToX(5) - 5, ChangeToY(10) + 5);
            g.DrawString(Convert.ToString(6), myXfont, new SolidBrush(myXcolor), ChangeToX(6) - 5, ChangeToY(10) + 5);
            g.DrawString(Convert.ToString(8), myXfont, new SolidBrush(myXcolor), ChangeToX(8) - 5, ChangeToY(10) + 5);
            //绘制y轴文字
            Y_multiple = 10;
            for (int i = 0; i < 2; i++)
            {
                g.DrawString(Convert.ToString(Y_multiple*2), myYfont, new SolidBrush(myYcolor), ChangeToX(4) - 35, ChangeToY(Y_multiple*2) - 8);
                g.DrawString(Convert.ToString(Y_multiple * 3), myYfont, new SolidBrush(myYcolor), ChangeToX(4) - 35, ChangeToY(Y_multiple * 3) - 8);
                g.DrawString(Convert.ToString(Y_multiple * 4), myYfont, new SolidBrush(myYcolor), ChangeToX(4) - 35, ChangeToY(Y_multiple * 4) - 8);
                g.DrawString(Convert.ToString(Y_multiple * 6), myYfont, new SolidBrush(myYcolor), ChangeToX(4) - 35, ChangeToY(Y_multiple * 6) - 8);
                g.DrawString(Convert.ToString(Y_multiple * 8), myYfont, new SolidBrush(myYcolor), ChangeToX(4) - 35, ChangeToY(Y_multiple * 8) - 8);
                g.DrawString(Convert.ToString(Y_multiple * 10), myYfont, new SolidBrush(myYcolor), ChangeToX(4) - 35, ChangeToY(Y_multiple * 10) - 8);
                Y_multiple = Y_multiple * 10.0;
            }
            g.DrawString(Convert.ToString(2000), myYfont, new SolidBrush(myYcolor), ChangeToX(4) - 35, ChangeToY(2000) - 8);
           
            
            
            
            //4个像素点来绘制一个点
           // g.FillRectangle(new SolidBrush(Color.Black), 1, 1, 4, 4);
            if (SysData.cm1_dt != null)
            {


                double total_x = 0;
                double total_y = 0;
                for (int i = 1; i < SysData.cm1_dt.Rows.Count; i++)
                {
                    g.FillRectangle(new SolidBrush(Color.Black), ChangeToX(Convert.ToDouble(SysData.cm1_dt.Rows[i][1]) * 1000), ChangeToY(Convert.ToDouble(SysData.cm1_dt.Rows[i][0]) * 1000), 4, 4);
                    total_x = total_x + Convert.ToDouble(SysData.cm1_dt.Rows[i][1]);
                    total_y = total_y + Convert.ToDouble(SysData.cm1_dt.Rows[i][0]);
                }
                double average_x = total_x / (SysData.cm1_dt.Rows.Count - 1);
                double average_y = total_y / (SysData.cm1_dt.Rows.Count - 1);

                double k1 = (ChangeToY(1000) - ChangeToY(10)) / (ChangeToX(1000) - ChangeToX(10));

                //画出红色的中线
                g.DrawLine(new Pen(Color.Red, 2), ChangeToX(10), Convert.ToSingle(k1 * (ChangeToX(10) - ChangeToX(average_x * 1000)) + ChangeToY(average_y * 1000)), ChangeToX(300), Convert.ToSingle(k1 * (ChangeToX(300) - ChangeToX(average_x * 1000)) + ChangeToY(average_y * 1000)));

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
            g.TranslateTransform( 30, this.Height / 2);
            if ( MyYCheck)
            {
                g.RotateTransform(90);
            }
            g.DrawString(MyYnameText, MyYnameFont, new SolidBrush(MyYnameColor), 0, 0);
            #endregion

            pictureBox1.BackgroundImage = bit;
            SysData.PrintBit = bit;
            
        }
        /// <summary>
        /// 对应到屏幕x轴的值。根据form的大小确定的放大倍数
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private float ChangeToX(double x)
        {
            double X;

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

            Y = (380 - Math.Log10(y / 10) * 150) * SysData.K_Height;
            
            return Convert.ToSingle(Y);
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


        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (SysData.cm1_dt == null)
            {
                Data_Special dqsj_form = new Data_Special();
                dqsj_form.ShowDialog();
                this.Refresh();
            }
            else
            {
                Setofline_cm1 setline = new Setofline_cm1(this);
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
        public void selfrefresh()
        {
            this.Refresh();
        }

      

        private void C_M1_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            MyObject.FrmName2 = "C_M1";
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyObject.MyCurrentFrmName1 = null;
            //复制窗体,把窗体内容序列化
            FormState fs = new FormState(MyText, MyColor, MyFont, MyLineColor1, MyLineColor2, MyLine1, MyLine2, MyDatatable,MyXBrush,MyYBrush,MyXFont,
                                         MyYFont,MyXnameFont,MyXnameColor,MyXnameText,MyYnameFont,MyYnameColor,MyYnameText,MyXCheck,MyYCheck);
            XmlHelper.SaveForm(fs);
            MyObject.FrmName1 = this.Name;//如果萨胡成因的窗体被点击了，那么就把名字给当前操作窗体的名字
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MyObject.FrmName1 = null;

            this.Close();
          
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void C_M1_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
            MyObject.FrmName2 = "C_M1";
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
            MyObject.FrmName2 = "C_M1";
        }

        private void C_M1_MouseUp(object sender, MouseEventArgs e)
        {
            MainFrame mf = new MainFrame();
            mf.getMsg();
        }

    }
}
