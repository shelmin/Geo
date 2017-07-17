/*
 * 岩石分类图图版2图三
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
    public partial class SanYanShiFenLeiTwo3 : Form
    {
        struct area
        {
            public int i;//模块编号
            public string name;//模块的中文名
            public PointF[] p;//构成该区域的边界点
            //public PointF pc;//区域的中心点

        };
        public Pen mypen = new Pen(Color.Black, 2.0f);
        public Pen mypen1 = new Pen(Color.Black, 2.0f);

        public Font myfont = new Font(DefaultFont, FontStyle.Regular);
        public SolidBrush mybrush = new SolidBrush(Color.Yellow);
        area[] areas = new area[11];//该分类图共有11块
        public float C = 1.7320f;//定义根号3
        public float length = 400.0f;//定义边长
        PointF init_point = new PointF(310, 100);
        public SanYanShiFenLeiTwo3()
        {
            InitializeComponent();
        }

      

        private void Form9_Paint(object sender, PaintEventArgs e)
        {
            areas[0].p = new PointF[3];
            areas[0].i = 0;//画第一块大三角形
            //  areas[0].name = "岩石三端元分类图图版1";
            areas[0].p[0] = init_point;
            areas[0].p[1] = new PointF((float)(init_point.X + length * 0.5), (float)(init_point.Y + length * C * 0.5));
            areas[0].p[2] = new PointF((float)(init_point.X - length * 0.5), (float)(init_point.Y + length * C * 0.5));
            //  areas[0].pc = Point_Center(areas[0].p);



            areas[1].p = new PointF[4];
            areas[1].i = 1;//画四边形
            areas[1].name = "石英杂砂岩";
            areas[1].p[0] = init_point;
            areas[1].p[1] = new PointF((float)(init_point.X + length * 0.1 * 0.5), (float)(init_point.Y + length * C * 0.1 * 0.5));
            areas[1].p[2] = new PointF(init_point.X, (float)(init_point.Y + 0.185 * length * 0.5 * C));
            areas[1].p[3] = new PointF((float)(init_point.X - length * 0.1 * 0.5), (float)(init_point.Y + length * C * 0.1 * 0.5));
            //areas[1].pc = Point_Center(areas[1].p);

            areas[2].p = new PointF[4];
            areas[2].i = 2;//画左四边形
            areas[2].name = "长石质石英杂砂岩";
            areas[2].p[0] = areas[1].p[3];
            areas[2].p[1] = areas[1].p[2];
            areas[2].p[2] = new PointF((float)(init_point.X - 0.15 * length * 0.5), (float)((init_point.Y + 0.5 * length * 0.5 * C) * 0.5 + areas[1].p[2].Y * 0.5));
            areas[2].p[3] = new PointF((float)(init_point.X - 0.25 * length * 0.5), (float)(init_point.Y + 0.25 * length * C * 0.5));
            //areas[2].pc = Point_Center(areas[2].p);

            areas[3].p = new PointF[4];
            areas[3].i = 3;//画右四边形
            areas[3].name = "岩屑质石英杂砂岩";
            areas[3].p[0] = areas[1].p[1];
            areas[3].p[1] = new PointF((float)(init_point.X + 0.25 * length * 0.5), (float)(init_point.Y + 0.25 * length * C * 0.5));
            areas[3].p[2] = new PointF((float)(init_point.X + 0.15 * length * 0.5), (float)((init_point.Y + 0.5 * length * 0.5 * C) * 0.5 + areas[1].p[2].Y * 0.5));
            areas[3].p[3] = areas[1].p[2];
            //areas[3].pc = Point_Center(areas[3].p);

            areas[4].p = new PointF[4];
            areas[4].i = 4;
            areas[4].name = "长石岩屑质石英杂砂岩";
            areas[4].p[0] = areas[1].p[2];
            areas[4].p[1] = areas[3].p[2];
            areas[4].p[2] = new PointF(init_point.X, (float)(init_point.Y + 0.5 * length * 0.5 * C));
            areas[4].p[3] = areas[2].p[2];

            //areas[4].pc = Point_Center(areas[4].p);

            areas[5].p = new PointF[4];
            areas[5].i = 5;//画左边第一四边角形
            areas[5].name = "长石杂砂岩";
            areas[5].p[0] = areas[2].p[3];
            areas[5].p[1] = areas[2].p[2];
            areas[5].p[2] = new PointF((float)(init_point.X - length * 0.4), (float)(init_point.Y + length * C * 0.5));
            areas[5].p[3] = areas[0].p[2];
            //areas[5].pc = Point_Center(areas[5].p);

            areas[6].p = new PointF[4];
            areas[6].i = 6;//画右边第二个四边形
            areas[6].name = "岩屑质长石杂砂岩";
            areas[6].p[0] = areas[4].p[3];
            areas[6].p[1] = areas[4].p[2];
            areas[6].p[2] = new PointF((float)(init_point.X - length * 0.25), (float)(init_point.Y + length * C * 0.5));
            areas[6].p[3] = areas[5].p[2];
            //areas[6].pc = Point_Center(areas[6].p);

            areas[7].p = new PointF[3];
            areas[7].i = 7;//画左边第3个多变形
            areas[7].name = "岩屑长石杂砂岩";
            areas[7].p[0] = areas[4].p[2];
            areas[7].p[1] = new PointF((float)(areas[0].p[0].X), (float)(areas[0].p[0].Y + length * C * 0.5));
            areas[7].p[2] = areas[6].p[2];

            //areas[7].pc = Point_Center(areas[7].p);


            areas[8].p = new PointF[3];
            areas[8].i = 8;//画左下第4个多变形
            areas[8].name = "长石岩屑杂砂岩";
            areas[8].p[0] = areas[4].p[2];
            areas[8].p[1] = new PointF((float)(init_point.X + length * 0.25), (float)(init_point.Y + length * C * 0.5));
            areas[8].p[2] = areas[7].p[1];

            //areas[8].pc = Point_Center(areas[8].p);

            areas[9].p = new PointF[4];
            areas[9].i = 9;//画左下第5个多变形
            areas[9].name = "长石质岩屑杂砂岩";
            areas[9].p[0] = areas[4].p[2];
            areas[9].p[1] = areas[4].p[1];
            areas[9].p[2] = new PointF((float)(init_point.X + length * 0.4), (float)(init_point.Y + length * C * 0.5));
            areas[9].p[3] = areas[8].p[1];
            //areas[9].pc = Point_Center(areas[8].p);


            areas[10].p = new PointF[4];
            areas[10].i = 10;//画左下第6个多变形
            areas[10].name = "岩屑杂砂岩";
            areas[10].p[0] = areas[4].p[1];
            areas[10].p[1] = areas[3].p[1];
            areas[10].p[2] = areas[0].p[1];
            areas[10].p[3] = areas[9].p[2];
            //areas[10].pc = Point_Center(areas[10].p);


            //绘出各个模块
            Graphics g = e.Graphics;
            foreach (area temp in areas)
            {
                g.DrawPolygon(mypen, temp.p);

            }

            //文字绘制

            g.DrawLine(mypen1, 310, 125, 338, 125);
            g.DrawString("石英杂砂岩", myfont, new SolidBrush(Color.Black), 351, 120);
            g.DrawLine(mypen1, 287, 169, 252, 169);
            g.DrawString("长石质石英杂砂岩", myfont, new SolidBrush(Color.Black), 140, 162);
            g.DrawLine(mypen1, 336, 164, 367, 164);
            g.DrawString("岩屑质石英杂砂岩", myfont, new SolidBrush(Color.Black), 372, 159);


            g.DrawLine(mypen1, 310, 211, 393, 211);
            g.DrawString("长石岩屑质石英杂砂岩", myfont, new SolidBrush(Color.Black), 398, 206);

            //绘制麻烦的斜字
            //绘制麻烦的斜字
            string[] str = new string[5] { "长", "石", "杂", "砂", "岩" };
            int px = 0, py = 0;
            for (int i = 0; i < str.Length; i++)
            {
                px = px - 14;
                py = py + 30;
                g.DrawString(str[i], myfont, new SolidBrush(Color.Black), 240 + px, 217 + py);
            }
            string[] str1 = new string[8] { "岩", "屑", "质", "长", "石", "杂", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                px = px - 14;
                py = py + 20;
                g.DrawString(str1[i], myfont, new SolidBrush(Color.Black), 290 + px, 242 + py);
            }

            string[] str2 = new string[7] { "岩", "屑", "长", "石", "杂", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str2.Length; i++)
            {
                // px = px + 5;
                py = py + 15;
                g.DrawString(str2[i], myfont, new SolidBrush(Color.Black), 281 + px, 306 + py);
            }


            string[] str3 = new string[7] { "长", "石", "岩", "屑", "杂", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str3.Length; i++)
            {
                // px = px + 10;
                py = py + 15;
                g.DrawString(str3[i], myfont, new SolidBrush(Color.Black), 313 + px, 306 + py);
            }

            string[] str4 = new string[8] { "长", "石", "质", "岩", "屑", "杂", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str4.Length; i++)
            {
                px = px + 10;
                py = py + 20;
                g.DrawString(str4[i], myfont, new SolidBrush(Color.Black), 340 + px, 242 + py);
            }


            string[] str5 = new string[5] { "岩", "屑", "杂", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str5.Length; i++)
            {
                px = px + 10;
                py = py + 20;
                g.DrawString(str5[i], myfont, new SolidBrush(Color.Black), 359 + px, 233 + py);
            }

            //g.Dispose();
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


            g.DrawLine(mypen1, 310, 125, 338, 125);
            g.DrawString("石英杂砂岩", myfont, new SolidBrush(Color.Black), 351, 120);
            g.DrawLine(mypen1, 287, 169, 252, 169);
            g.DrawString("长石质石英杂砂岩", myfont, new SolidBrush(Color.Black), 140, 162);
            g.DrawLine(mypen1, 336, 164, 367, 164);
            g.DrawString("岩屑质石英杂砂岩", myfont, new SolidBrush(Color.Black), 372, 159);


            g.DrawLine(mypen1, 310, 211, 393, 211);
            g.DrawString("长石岩屑质石英杂砂岩", myfont, new SolidBrush(Color.Black), 398, 206);



            //绘制麻烦的斜字
            string[] str = new string[5] { "长", "石", "杂", "砂", "岩" };
            int px = 0, py = 0;
            for (int i = 0; i < str.Length; i++)
            {
                px = px - 14;
                py = py + 30;
                g.DrawString(str[i], myfont, new SolidBrush(Color.Black), 240 + px, 217 + py);
            }
            string[] str1 = new string[8] { "岩", "屑", "质", "长", "石", "杂", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                px = px - 14;
                py = py + 20;
                g.DrawString(str1[i], myfont, new SolidBrush(Color.Black), 290 + px, 242 + py);
            }

            string[] str2 = new string[7] { "岩", "屑", "长", "石", "杂", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str2.Length; i++)
            {
                // px = px + 5;
                py = py + 15;
                g.DrawString(str2[i], myfont, new SolidBrush(Color.Black), 281 + px, 306 + py);
            }

            string[] str3 = new string[7] { "长", "石", "岩", "屑", "杂", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str3.Length; i++)
            {
                // px = px + 10;
                py = py + 15;
                g.DrawString(str3[i], myfont, new SolidBrush(Color.Black), 313 + px, 306 + py);
            }
            string[] str4 = new string[8] { "长", "石", "质", "岩", "屑", "杂", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str4.Length; i++)
            {
                px = px + 10;
                py = py + 20;
                g.DrawString(str4[i], myfont, new SolidBrush(Color.Black), 340 + px, 242 + py);
            }

            string[] str5 = new string[5] { "岩", "屑", "杂", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str5.Length; i++)
            {
                px = px + 10;
                py = py + 20;
                g.DrawString(str5[i], myfont, new SolidBrush(Color.Black), 359 + px, 233 + py);
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

        private void Form9_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
           // MyObject.MyCurrentFrmName1 = this.Name;
            MyObject.MyCurrentFrm1 = this;
            for (int i = 1; i < areas.Length; i++)
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
                    this.label1.Text = "该区域是" + areas[i].name;

                    break;
                }
            }


        }

        private void 岩石分类图图版2图三_MouseMove(object sender, MouseEventArgs e)
        {
            
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
                mybrush.Color= diag.Color;
                this.Refresh();
            }
        }

        private void 岩石分类图图版2图三_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            //this.Name = "岩石分类图图版2图三";
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

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyObject.My_Chart2 = null;
            //这里完成三段元图的复制
            FormState fs = new FormState(mypen.Color, mybrush.Color, myfont);
            XmlHelper.SaveForm(fs);
            MyObject.MyCurrentFrmName1 = "岩石分类图图版2图三";
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
