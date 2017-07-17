/*
 * 岩石三端元图图三
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
    public partial class SanYanShiSanDuanYuan3 : Form
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
        area[] areas = new area[9];//该分类图共有11块
        public float C = 1.7320f;//定义根号3
        public float length = 400.0f;//定义边长
        PointF init_point = new PointF(310, 100);
        public SanYanShiSanDuanYuan3()
        {
            InitializeComponent();
        }

        private void Form6_Paint(object sender, PaintEventArgs e)
        {
            areas[0].p = new PointF[3];
            areas[0].i = 0;//画第一块大三角形
            areas[0].name = "岩石三端元分类图图版二图1";
            areas[0].p[0] = init_point;
            areas[0].p[1] = new PointF((float)(init_point.X + length * 0.5), (float)(init_point.Y + length * C * 0.5));
            areas[0].p[2] = new PointF((float)(init_point.X - length * 0.5), (float)(init_point.Y + length * C * 0.5));
          //  areas[0].pc = Point_Center(areas[0].p);



            areas[1].p = new PointF[3];
            areas[1].i = 1;//画小三角形
            areas[1].name = "石英杂砂岩";
            areas[1].p[0] = init_point;
            areas[1].p[1] = new PointF((float)(init_point.X + length * 0.1 * 0.5), (float)(init_point.Y + length * C * 0.1 * 0.5));
            areas[1].p[2] = new PointF((float)(init_point.X - length * 0.1 * 0.5), (float)(init_point.Y + length * C * 0.1 * 0.5));
            //areas[1].pc = Point_Center(areas[1].p);

            areas[2].p = new PointF[4];
            areas[2].i = 2;//画左等腰梯型
            areas[2].name = "长石质石英杂砂岩";
            areas[2].p[0] = areas[1].p[2];
            areas[2].p[1] = new PointF(areas[1].p[0].X, (float)(init_point.Y + 0.1 * length * C * 0.5));
            areas[2].p[2] = new PointF((float)(init_point.X), (float)(init_point.Y + 0.25 * length * C * 0.5));
            areas[2].p[3] = new PointF((float)(init_point.X - 0.25 * length * 0.5), (float)(init_point.Y + 0.25 * length * C * 0.5));
            //areas[2].pc = Point_Center(areas[2].p);

            areas[3].p = new PointF[4];
            areas[3].i = 3;//画右边等腰梯形
            areas[3].name = "岩屑质石英杂砂岩";
            areas[3].p[0] = areas[2].p[1];
            areas[3].p[1] = areas[1].p[1];
            areas[3].p[2] = new PointF((float)(init_point.X + 0.25 * length * 0.5), (float)(init_point.Y + 0.25 * length * C * 0.5));
            areas[3].p[3] = areas[2].p[2];
            //areas[3].pc = Point_Center(areas[3].p);

            areas[4].p = new PointF[3];
            areas[4].i = 4;//画中间小三角梯形
            areas[4].name = "长石岩屑质石英杂砂岩";
            areas[4].p[0] = areas[2].p[3];
            areas[4].p[1] = areas[3].p[2];
            areas[4].p[2] = new PointF((float)(init_point.X), (float)(init_point.Y + 0.5 * length * C * 0.5));
           
            //areas[4].pc = Point_Center(areas[4].p);

            areas[5].p = new PointF[4];
            areas[5].i = 5;//画左边第一四边角形
            areas[5].name = "长石杂砂岩";
            areas[5].p[0] = areas[4].p[0];
            areas[5].p[1] = new PointF((float)(areas[0].p[0].X - (areas[3].p[2].X - areas[0].p[0].X) * 0.5), (float)(areas[0].p[0].Y + length * 0.375 * C * 0.5));
            areas[5].p[2] = new PointF((float)(init_point.X-length*0.25), (float)(init_point.Y + length * C * 0.5));
            areas[5].p[3] = areas[0].p[2];
            //areas[5].pc = Point_Center(areas[5].p);

            areas[6].p = new PointF[4];
            areas[6].i = 6;//画右边第二个四边形
            areas[6].name = "岩屑质长石杂砂岩";
            areas[6].p[0] = areas[5].p[1];
            areas[6].p[1] = areas[4].p[2];
            areas[6].p[2] = new PointF(init_point.X,(float)(init_point.Y+length*0.5*C));
            areas[6].p[3] = areas[5].p[2];
            //areas[6].pc = Point_Center(areas[6].p);

            areas[7].p = new PointF[4];
            areas[7].i = 7;//画左边第3个多变形
            areas[7].name = "长石质岩屑杂砂岩";
            areas[7].p[0] = areas[4].p[2];
            areas[7].p[1] = new PointF((float)(areas[0].p[0].X + (areas[3].p[2].X - areas[0].p[0].X) * 0.5), (float)(areas[0].p[0].Y + length * 0.375 * C * 0.5));
            areas[7].p[2] = new PointF((float)(areas[0].p[0].X + length * 0.25), (float)(areas[0].p[1].Y));
            areas[7].p[3] = areas[6].p[2];
            //areas[7].pc = Point_Center(areas[7].p);


            areas[8].p = new PointF[4];
            areas[8].i = 8;//画左下第4个多变形
            areas[8].name = "岩屑杂砂岩";
            areas[8].p[0] = areas[7].p[1];
            areas[8].p[1] = areas[4].p[1];
            areas[8].p[2] = areas[0].p[1];
            areas[8].p[3] = areas[7].p[2];
            //areas[8].pc = Point_Center(areas[8].p);


            //绘出各个模块
            Graphics g = e.Graphics;
            foreach (area temp in areas)
            {
                g.DrawPolygon(mypen, temp.p);

            }

            //文字绘制

            g.DrawLine(mypen1, 310, 117,278, 117);
            g.DrawString("石英杂砂岩", myfont, new SolidBrush(Color.Black), 210, 112);
            g.DrawLine(mypen1, 290, 157, 257, 157);
            g.DrawString("长石质石英杂砂岩", myfont, new SolidBrush(Color.Black), 155, 152);
            g.DrawLine(mypen1, 328, 157, 355, 157);
            g.DrawString("岩屑质石英杂砂岩", myfont, new SolidBrush(Color.Black), 360, 152);

            g.DrawLine(mypen1, 310, 212, 389, 212);
            g.DrawString("长石岩屑质石英杂砂岩", myfont, new SolidBrush(Color.Black), 394, 207);

            //绘制麻烦的斜字
            string[] str = new string[5] { "长", "石", "杂", "砂", "岩" };
            int px = 0, py = 0;
            for (int i = 0; i < str.Length; i++)
            {
                px = px - 10;
                py = py + 30;
                g.DrawString(str[i], myfont,new SolidBrush(Color.Black), 253 + px, 224 + py);
            }
            string[] str1 = new string[8] { "岩", "屑", "质", "长", "石", "杂", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                px = px - 5;
                py = py + 20;
                g.DrawString(str1[i], myfont, new SolidBrush(Color.Black), 289 + px, 243 + py);
            }

            string[] str2 = new string[8] { "长", "石", "质", "岩", "屑", "杂", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str2.Length; i++)
            {
                px = px + 5;
                py = py + 20;
                g.DrawString(str2[i], myfont, new SolidBrush(Color.Black), 324 + px, 239 + py);
            }

            string[] str3 = new string[5] { "岩", "屑", "杂", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str3.Length; i++)
            {
                px = px + 10;
                py = py + 30;
                g.DrawString(str3[i], myfont, new SolidBrush(Color.Black), 361 + px, 225 + py);
            }


            g.Dispose();
        
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
            g.DrawLine(mypen1, 310, 117, 278, 117);
            g.DrawString("石英杂砂岩", myfont, new SolidBrush(Color.Black), 210, 112);
            g.DrawLine(mypen1, 290, 157, 257, 157);
            g.DrawString("长石质石英杂砂岩", myfont, new SolidBrush(Color.Black), 155, 152);
            g.DrawLine(mypen1, 328, 157, 355, 157);
            g.DrawString("岩屑质石英杂砂岩", myfont, new SolidBrush(Color.Black), 360, 152);

            g.DrawLine(mypen1, 310, 212, 389, 212);
            g.DrawString("长石岩屑质石英杂砂岩", myfont, new SolidBrush(Color.Black), 394, 207);


            //绘制麻烦的斜字
            string[] str = new string[5] { "长", "石", "杂","砂", "岩" };
            int px = 0, py = 0;
            for (int i = 0; i < str.Length; i++)
            {
                px = px - 10;
                py = py + 30;
                g.DrawString(str[i], myfont, new SolidBrush(Color.Black), 253 + px, 224 + py);
            }
            string[] str1 = new string[8] { "岩", "屑", "质", "长", "石","杂", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                px = px - 5;
                py = py + 20;
                g.DrawString(str1[i], myfont, new SolidBrush(Color.Black), 289 + px, 243 + py);
            }

            string[] str2 = new string[8] { "长", "石", "质", "岩", "屑", "杂","砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str2.Length; i++)
            {
                px = px + 5;
                py = py + 20;
                g.DrawString(str2[i], myfont, new SolidBrush(Color.Black), 324 + px, 239 + py);
            }

            string[] str3 = new string[5] { "岩", "屑","杂", "砂", "岩" };
            px = 0; py = 0;
            for (int i = 0; i < str3.Length; i++)
            {
                px = px + 10;
                py = py + 30;
                g.DrawString(str3[i], myfont, new SolidBrush(Color.Black), 361 + px, 225 + py);
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

        private void Form6_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
            //MyObject.MyCurrentFrmName1 = this.Name;
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

        private void 岩石三端元图图三_Load(object sender, EventArgs e)
        {
            this.BringToFront();
           // this.Name = "岩石三端元图图三";
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

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyObject.My_Chart2 = null;
            //这里是复制
            FormState fs = new FormState(mypen.Color, mybrush.Color, myfont);
            XmlHelper.SaveForm(fs);
            MyObject.MyCurrentFrmName1 = "岩石三端元图图三";
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
