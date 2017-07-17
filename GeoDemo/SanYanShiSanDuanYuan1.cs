/*
 * 岩石三端元图图一
*/
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
    public partial class SanYanShiSanDuanYuan1 : Form
    {
        struct area
        {
            public int i;//模块编号
            public string name;//模块的中文名
            public PointF[] p;//构成该区域的边界点
            public PointF pc;//区域的中心点

        };
        public Pen mypen = new Pen(Color.Black,2.0f);
        public Pen mypen1 = new Pen(Color.Black, 2.0f);
        public Font myfont = new Font(DefaultFont, FontStyle.Regular);
        public SolidBrush mybrush = new SolidBrush(Color.Yellow);
        area [] areas = new area[11];//该分类图共有11块
        public float C = 1.7320f;//定义根号3
        public float length = 400.0f;//定义边长
        PointF init_point = new PointF(310, 100);
        public SanYanShiSanDuanYuan1()
        {
            InitializeComponent();
           
        }

        private void Form4_Paint(object sender, PaintEventArgs e)
        {
            areas[0].p = new PointF[3];
            areas[0].i = 0;//画第一块大三角形
            areas[0].name = "岩石三端元分类图版一图1";
            areas[0].p[0] = init_point;
            areas[0].p[1] = new PointF((float)(init_point.X+length*0.5),(float)(init_point.Y+length*C*0.5));
            areas[0].p[2] = new PointF((float)(init_point.X - length * 0.5), (float)(init_point.Y + length * C * 0.5));
            areas[0].pc = Point_Center(areas[0].p);
          


            areas[1].p = new PointF[3];
            areas[1].i = 1;//画小三角形
            areas[1].name = "纯石英砂岩";
            areas[1].p[0] = init_point;
            areas[1].p[1] = new PointF((float)(init_point.X + length * 0.05*0.5), (float)(init_point.Y + length * C * 0.05*0.5));
            areas[1].p[2] = new PointF((float)(init_point.X - length * 0.05 * 0.5), (float)(init_point.Y + length * C * 0.05 * 0.5));
            areas[1].pc = Point_Center(areas[1].p);

            areas[2].p = new PointF[4];
            areas[2].i = 2;//画小等腰梯形
            areas[2].name = "石英砂岩";
            areas[2].p[0] = areas[1].p[2];
            areas[2].p[1] = areas[1].p[1];
            areas[2].p[2] = new PointF((float)(init_point.X+0.1*length*0.5), (float)(init_point.Y+0.1*length*C*0.5));
            areas[2].p[3] = new PointF((float)(init_point.X - 0.1 * length * 0.5), (float)(init_point.Y + 0.1 * length * C * 0.5));
            areas[2].pc = Point_Center(areas[2].p);

            areas[3].p = new PointF[4];
            areas[3].i = 3;//画左边梯形
            areas[3].name = "长石石英砂岩";
            areas[3].p[0] = areas[2].p[3];
            areas[3].p[1] = new PointF((float)(init_point.X), areas[2].p[2].Y);
            areas[3].p[2] = new PointF((float)(init_point.X), (float)(init_point.Y + length * C * 0.5*0.25));
            areas[3].p[3] = new PointF((float)(init_point.X-0.25*length*0.5),(float)(init_point .Y+0.25*length*C*0.5));
            areas[3].pc = Point_Center(areas[3].p);

            areas[4].p = new PointF[4];
            areas[4].i = 4;//画右边梯形
            areas[4].name = "岩屑石英砂岩";
            areas[4].p[0] = areas[3].p[1];
            areas[4].p[1] = areas[2].p[2];
            areas[4].p[2] = new PointF((float)(init_point.X + 0.25 * length * 0.5), (float)(init_point.Y + 0.25 * length * C * 0.5));
            areas[4].p[3] = areas[3].p[2];
            areas[4].pc = Point_Center(areas[4].p);

            areas[5].p = new PointF[3];
            areas[5].i = 5;//画左边三角形
            areas[5].name = "次长石砂岩";
            areas[5].p[0] = areas[3].p[3];
            areas[5].p[1] = areas[3].p[2];
            areas[5].p[2] = new PointF((float)(init_point.X), (float)(init_point.Y + 0.5 * length * C * 0.5));
            areas[5].pc = Point_Center(areas[5].p);

            areas[6].p = new PointF[3];
            areas[6].i = 6;//画右边三角形
            areas[6].name = "次岩屑砂岩";
            areas[6].p[0] = areas[4].p[3];
            areas[6].p[1] = areas[4].p[2];
            areas[6].p[2] = areas[5].p[2];
            areas[6].pc = Point_Center(areas[6].p);

            areas[7].p = new PointF[4];
            areas[7].i = 7;//画左下第一个多变形
            areas[7].name = "长石砂岩";
            areas[7].p[0] = areas[5].p[0];
            areas[7].p[1] = new PointF((float)(areas[0].p[0].X-(areas[4].p[2].X-areas[0].p[0].X)*0.5),(float)(areas[0].p[0].Y+length*0.375*C*0.5));
            areas[7].p[2] = new PointF((float)(areas[0].p[0].X - length * 0.25), (float)(areas[0].p[2].Y));
            areas[7].p[3] = areas[0].p[2];
            areas[7].pc = Point_Center(areas[7].p);


            areas[8].p = new PointF[4];
            areas[8].i = 8;//画左下第2个多变形
            areas[8].name = "岩屑长石砂岩";
            areas[8].p[0] = areas[7].p[1];
            areas[8].p[1] = new PointF(areas[0].p[0].X,(float)(areas[0].p[0].Y+0.5*length*C*0.5));
            areas[8].p[2] = new PointF(areas[0].p[0].X, (float)(areas[0].p[0].Y + length * C * 0.5));
            areas[8].p[3] = areas[7].p[2];
            areas[8].pc = Point_Center(areas[8].p);



            areas[9].p = new PointF[4];
            areas[9].i = 9;//画左下第3个多变形
            areas[9].name = "长石岩屑砂岩";
            areas[9].p[0] = areas[8].p[1];
            areas[9].p[1] = new PointF((float)(areas[0].p[0].X + (areas[4].p[2].X-areas[0].p[0].X) * 0.5), (float)(areas[0].p[0].Y + length * 0.375 * C * 0.5));
            areas[9].p[2] = new PointF((float)(areas[0].p[0].X+length*0.25),areas[0].p[1].Y);
            areas[9].p[3] = areas[8].p[2];
            areas[9].pc = Point_Center(areas[9].p);

            areas[10].p = new PointF[4];
            areas[10].i = 10;//画左下第3个多变形
            areas[10].name = "岩屑砂岩";
            areas[10].p[0] = areas[9].p[1];
            areas[10].p[1] = areas[6].p[1];
            areas[10].p[2] = areas[0].p[1];
            areas[10].p[3] = areas[9].p[2];
            areas[10].pc = Point_Center(areas[10].p);

            //绘出各个模块
            Graphics g = e.Graphics;
            foreach (area temp in areas)
            {
                g.DrawPolygon(mypen, temp.p);

            }
            //文字绘制
            g.DrawLine(mypen1, 310, 110, 330, 110);
            g.DrawString("纯石英砂岩", myfont, new SolidBrush(Color.Black), 335, 106);
            g.DrawLine(mypen1, 310, 125, 265, 125);
            g.DrawString("石英砂岩", myfont, new SolidBrush(Color.Black), 212, 120);
            g.DrawLine(mypen1, 295, 162, 237, 162);
            g.DrawString("长石石英砂岩", myfont, new SolidBrush(Color.Black), 160, 157);
            g.DrawLine(mypen1, 332, 162, 385, 162);
            g.DrawString("岩屑石英砂岩", myfont, new SolidBrush(Color.Black), 395, 157);
            g.DrawLine(mypen1, 287,205,228,205);
            g.DrawString("次长石砂岩", myfont, new SolidBrush(Color.Black), 160, 200);
            g.DrawLine(mypen1, 328, 205, 388, 205);
            g.DrawString("次岩屑砂岩", myfont, new SolidBrush(Color.Black), 395, 200);
            //绘制麻烦的斜字
            string[] str = new string[4] { "长", "石", "砂", "岩" };
            int px = 0, py = 0;
            for (int i = 0; i < str.Length; i++)
            {
                px = px - 10;
                py = py + 30;
                g.DrawString(str[i], myfont, new SolidBrush(Color.Black), 252 + px, 237 + py);
            }
            string[] str1 = new string[6] { "岩", "屑", "长", "石", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                px = px - 5;
                py = py + 30;
                g.DrawString(str1[i], myfont, new SolidBrush(Color.Black), 286 + px, 250 + py);
            }

            string[] str2 = new string[6] { "长", "石", "岩", "屑", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str2.Length; i++)
            {
                px = px + 5;
                py = py + 30;
                g.DrawString(str2[i], myfont, new SolidBrush(Color.Black), 320 + px, 250 + py);
            }

            string[] str3 = new string[4] { "岩", "屑", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str3.Length; i++)
            {
                px = px + 10;
                py = py + 30;
                g.DrawString(str3[i], myfont, new SolidBrush(Color.Black), 371 + px, 237 + py);
            }

            for (int i = 0; i < MyObject.Sum1; i++)
            {
                TouDian(g, MyObject.A1[i], MyObject.B1[i], MyObject.C1[i]);
            }

            g.Dispose();

        }

        public void TouDian(Graphics g, int a, int b, int c)//投点算法
        {
            PointF basePoint = new PointF(init_point.X - length / 2, init_point.Y + length * C / 2.0f);//定义三角形左边那个角的点为原点；
            float x = basePoint.X + (c + a / 2) * 4;
            float y = basePoint.Y - 2 * a * C;
            g.DrawEllipse(mypen, x, y, 2, 2);
        }

        public PointF Point_Center(PointF[] p)
        {
            float X = 0.0f, Y = 0.0f;
            PointF temp = new PointF(0.0f, 0.0f);
            foreach (PointF t in p)
            {
                X = X + t.X;
                Y = Y + t.Y;
            }
            X = X / p.Length;
            Y = Y / p.Length;
            temp.X = X;
            temp.Y = Y;
            return temp;
        }
     

        //绘制图形
        private void Paints()
        {
            Graphics g = this.CreateGraphics();
            //模块绘制
            foreach (area temp in areas)
            {
                
                g.DrawPolygon(mypen, temp.p);
            }
            //文字绘制

            g.DrawLine(mypen1, 310, 110, 330, 110);
            g.DrawString("纯石英砂岩", myfont, new SolidBrush(Color.Black), 335, 106);
            g.DrawLine(mypen1, 310, 125, 265, 125);
            g.DrawString("石英砂岩", myfont, new SolidBrush(Color.Black), 212, 120);
            g.DrawLine(mypen1, 295, 162, 237, 162);
            g.DrawString("长石石英砂岩", myfont, new SolidBrush(Color.Black), 160, 157);
            g.DrawLine(mypen1,332,162,385,162);
            g.DrawString("岩屑石英砂岩", myfont, new SolidBrush(Color.Black), 395, 157);
            g.DrawLine(mypen1, 287, 205, 228, 205);
            g.DrawString("次长石砂岩", myfont, new SolidBrush(Color.Black), 160, 200);
            g.DrawLine(mypen1, 328, 205, 388, 205);
            g.DrawString("次岩屑砂岩", myfont, new SolidBrush(Color.Black), 395, 200);

            Graphics g1 = this.CreateGraphics();
            g1.RotateTransform(-60);
            g1.DrawString("长石砂岩", myfont, new SolidBrush(Color.Black), areas[5].pc);
            //绘制麻烦的斜字
            string[] str = new string[4] { "长", "石", "砂", "岩" };
            int px = 0, py = 0;
            for (int i = 0; i < str.Length; i++)
            {
                px = px - 10;
                py = py + 30;
                g.DrawString(str[i], myfont, new SolidBrush(Color.Black), 252 + px, 237 + py);
            }
            string[] str1 = new string[6] { "岩", "屑", "长", "石", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                px = px - 5;
                py = py + 30;
                g.DrawString(str1[i], myfont, new SolidBrush(Color.Black), 286 + px, 250 + py);
            }

            string[] str2 = new string[6] { "长", "石", "岩", "屑", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str2.Length; i++)
            {
                px = px + 5;
                py = py + 30;
                g.DrawString(str2[i], myfont, new SolidBrush(Color.Black), 320 + px, 250 + py);
            }

            string[] str3 = new string[4] { "岩", "屑", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str3.Length; i++)
            {
                px = px + 10;
                py = py + 30;
                g.DrawString(str3[i], myfont, new SolidBrush(Color.Black), 371 + px, 237 + py);
            }



        }
        private void Form4_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
            //MyObject.MyCurrentFrmName1 = this.Name;
            MyObject.MyCurrentFrm1 = this;
            for (int i = 1; i < areas.Length;i++ )
            {
                Graphics g = this.CreateGraphics();//创建一个画布
                GraphicsPath mypath = new GraphicsPath();//表示一系列的线围成的图
                mypath.AddPolygon(areas[i].p);//向该路径添加z多边形
                Region myregion = new Region(mypath);//创建每个多边形的区域
                if (myregion.IsVisible(e.X, e.Y))//判断鼠标点击的点是否在该区域内
                {
                    g.Clear(BackColor);

                    g.FillPolygon(mybrush, areas[i].p);
                    Paints();
                    this.label1.Text = "该区域是：" + areas[i].name;

                    break;
                }
                else
                {
                    this.label1.Text =areas[0].name;

                }
                
            }
           
           

        }

        private void 更改画笔颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog diag = new ColorDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                mypen1.Color = diag.Color;
                mypen.Color = diag.Color;
                this.Refresh();
            }
        }

        private void 更改字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog diag = new FontDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                foreach (Control c in this.Controls)
                {
                    if (c is Label)
                    {
                        myfont = diag.Font;
                        c.Font = diag.Font;
                        this.Refresh();
                    }
                }
            }
        }

        private void 更改区域块颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog diag = new ColorDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                mybrush.Color = diag.Color;
                this.Refresh();
            }
        }

        private void 岩石三端元图图一_Load(object sender, EventArgs e)
        {
            this.BringToFront();
           // this.Name = "岩石三端元图图一";
            foreach (Control c in this.Controls)
            {
                if (c is Label)
                {
                    c.Font = myfont;
                    this.Refresh();
                }
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MyObject.MyCurrentFrmName1 = null;
            MyObject.Sum1 = 0;
            this.Close();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyObject.My_Chart2 = null;
            //这里是复制
            FormState fs = new FormState(mypen.Color, mybrush.Color, myfont);
            XmlHelper.SaveForm(fs);
            MyObject.MyCurrentFrmName1 = "岩石三端元图图一";
            MyObject.FrmName1 = null;
            MyObject.MyCurrentFrm1 = this;
        }

        private void 添加数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataForSanDuanYuan dfsdy = new DataForSanDuanYuan();
            dfsdy.ShowDialog();
            Graphics g = this.CreateGraphics();
            for (int i = 0; i < MyObject.Sum1; i++)
            {
                TouDian(g, MyObject.A1[i], MyObject.B1[i], MyObject.C1[i]);
            }
        }

      
         

      
     


    }
}
