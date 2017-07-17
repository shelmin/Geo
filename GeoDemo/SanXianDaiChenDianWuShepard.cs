/*
 * 线代沉积物Shepard分类图
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
    public partial class SanXianDaiChenDianWuShepard : Form
    {
        public static float leng = 400.0f;
        public static float heightx = 310.0f;
        //常量根号3
        float C = 1.7320f;
       
        //常量画笔
       public Pen cP = new Pen(Color.Black, 2.0f);
       public Pen dp = new Pen(Color.Black,2.0f);
        public SolidBrush MyBrush = new SolidBrush(Color.Yellow);
        public Font LabelFont = Label.DefaultFont;

        //大三角形边长
        private float length = leng;
       //小三角形边长
        private float smaleng =leng/3 ;
        //大三角形顶点坐标
        private PointF point = new PointF(heightx, 100.0f);
        //小三角形的顶点坐标
         private PointF smallpoint = new PointF(heightx, 240.0f);
        //储存模块数据结构体，顶点坐标，模块名称，模块编号，模块中心点坐标
        private struct Points
        {
            public PointF[] p;
          //  public string str;
            public string name;
            public int i;
            public PointF pointC;
        }
        //结构体数组
         Points[] points = new Points[11];

        public SanXianDaiChenDianWuShepard()
        {
            InitializeComponent();
            this.MouseClick += new MouseEventHandler(Form3_MouseClick);
        }
      
      private void Form3_Paint(object sender, PaintEventArgs e)
       {
           dp.DashStyle = DashStyle.Dot;
           Paints();
        }

        //绘制图形
        private void Paints()
        {
            Graphics g = this.CreateGraphics();
           
            #region 给点赋值的过程
            //大三角形 绘点的顺序是顺时针
            points[0].p = new PointF[3];
            points[0].p[0] = point;
            points[0].p[1] = new PointF(point.X + length / 2, point.Y + length / 2 * C);
            points[0].p[2] = new PointF(point.X - length / 2, point.Y + length * C / 2.0f);

            //  points[0].str = "现代沉淀物Shepard分类图";
            points[0].name = "现代沉淀物Shepard分类图";
            points[0].i = 0;
            points[0].pointC = new PointF(point.X, point.Y + length * C * 0.5f + 20.0f);

            //模块1数据 绘点的顺序是顺时针
            points[1].p = new PointF[3];
            points[1].p[0] = point;
            points[1].p[1] = new PointF(point.X + 0.25f * length * 0.5f, point.Y + 0.25f * length * C * 0.5f);
            points[1].p[2] = new PointF(point.X - 0.25f * length * 0.5f, point.Y + 0.25f * length * C * 0.5f);

            //   points[1].str = "G";
            points[1].name = "黏土";
            points[1].i = 1;
            points[1].pointC = Point_Center(points[1].p);

            //模块2数据  绘点的顺序是顺时针
            points[2].p = new PointF[5];
            points[2].p[0] = points[1].p[2];
            points[2].p[1] = new PointF(point.X, points[2].p[0].Y);
            points[2].p[2] = new PointF(smallpoint.X, smallpoint.Y);
            points[2].p[3] = new PointF((float)(smallpoint.X - 0.5 * smaleng * 0.5), (float)(smallpoint.Y + 0.5 * smaleng * C * 0.5));
            points[2].p[4] = new PointF((float)(point.X - 0.5 * length * 0.5), (float)(point.Y + 0.5 * length * C * 0.5));

            //   points[2].str = "mG";
            points[2].name = "砂质黏土";
            points[2].i = 2;
            points[2].pointC = Point_Center(points[2].p);

            //模块3数据 绘点的顺序是顺时针
            points[3].p = new PointF[5];
            points[3].p[0] = points[2].p[1];
            points[3].p[1] = points[1].p[1];
            points[3].p[2] = new PointF(point.X + length * 0.5f * 0.5f, points[2].p[4].Y);
            points[3].p[3] = new PointF((float)(smallpoint.X + 0.5 * smaleng * 0.5), (float)(smallpoint.Y + 0.5 * smaleng * C * 0.5));
            points[3].p[4] = new PointF(smallpoint.X, smallpoint.Y);
            //points[3].str = "msG";
            points[3].name = "粉砂质黏土";
            points[3].i = 3;
            points[3].pointC = Point_Center(points[3].p);

            //模块4数据
            points[4].p = new PointF[5];
            points[4].p[0] = points[2].p[4];
            points[4].p[1] = points[2].p[3];
            points[4].p[2] = new PointF((float)(smallpoint.X - 0.5 * smaleng), (float)(smallpoint.Y + smaleng * C * 0.5));
            points[4].p[3] = new PointF((float)(point.X - 0.875 * length * 0.5 + 0.125 * length), (float)(point.Y + 0.875 * length * C * 0.5));
            points[4].p[4] = new PointF((float)(point.X - 0.75 * length * 0.5), (float)(point.Y + 0.75 * length * C * 0.5));
            // points[4].str = "sG";
            points[4].name = "黏土质砂";
            points[4].i = 4;
            points[4].pointC = Point_Center(points[4].p);

            //模块5数据
            points[5].p = new PointF[5];
            points[5].p[0] = points[3].p[3];
            points[5].p[1] = points[3].p[2];
            points[5].p[2] = new PointF((float)(point.X + 0.75 * length * 0.5), (float)(point.Y + 0.75 * length * C * 0.5));
            points[5].p[3] = new PointF((float)(point.X + 0.875 * length * 0.5 - 0.125 * length), (float)(point.Y + 0.875 * length * C * 0.5));
            points[5].p[4] = new PointF((float)(smallpoint.X + 0.5 * smaleng), (float)(smallpoint.Y + smaleng * C * 0.5));
            // points[5].str = "gM";
            points[5].name = "黏土质粉砂";
            points[5].i = 5;
            points[5].pointC = Point_Center(points[5].p);

            //模块6数据
            points[6].p = new PointF[5];
            points[6].p[0] = new PointF((float)(smallpoint.X - 0.5 * smaleng), (float)(smallpoint.Y + smaleng * C * 0.5));
            points[6].p[1] = new PointF(point.X, (float)(smallpoint.Y + smaleng * C * 0.5));
            points[6].p[2] = new PointF(point.X, (float)(point.Y + 0.5 * length * C));
            points[6].p[3] = new PointF((float)(points[0].p[2].X + 0.25 * length), points[0].p[2].Y);
            points[6].p[4] = points[4].p[3];
            //  points[6].str = "gmS";
            points[6].name = "粉砂质砂";
            points[6].i = 6;
            points[6].pointC = Point_Center(points[6].p);

            //模块7数据
            points[7].p = new PointF[5];
            points[7].p[0] = points[6].p[1];
            points[7].p[1] = points[5].p[4];
            points[7].p[2] = points[5].p[3];
            points[7].p[3] = new PointF((float)(points[0].p[1].X - 0.25 * length), points[0].p[1].Y);
            points[7].p[4] = points[6].p[2];
            // points[7].str = "gS";
            points[7].name = "砂质砂粉";
            points[7].i = 7;
            points[7].pointC = Point_Center(points[7].p);

            //模块8数据
            points[8].p = new PointF[3];
            points[8].p[0] = points[4].p[4];
            points[8].p[1] = points[6].p[3];
            points[8].p[2] = points[0].p[2];

            //  points[8].str = "(g)M";
            points[8].name = "砂";
            points[8].i = 8;
            points[8].pointC = Point_Center(points[8].p);

            //模块9数据
            points[9].p = new PointF[3];
            points[9].p[0] = points[5].p[2];
            points[9].p[1] = points[0].p[1];
            points[9].p[2] = points[7].p[3];

            //  points[9].str = "(g)mS";
            points[9].name = "粉砂";
            points[9].i = 9;
            points[9].pointC = Point_Center(points[9].p);

            //模块10数据
            points[10].p = new PointF[3];
            points[10].p[0] = new PointF(smallpoint.X, smallpoint.Y);
            points[10].p[1] = points[5].p[4];
            points[10].p[2] = points[4].p[2];

            // points[10].str = "(g)S";
            points[10].name = "砂-粉砂-黏土";
            points[10].i = 10;
            points[10].pointC = Point_Center(points[10].p);

            #endregion 

  
            //模块绘制
            //foreach (Points temp in points)
            //{
            //    if (temp.name == null)
            //        break;
            //    g.DrawPolygon(cP, temp.p);
            //}
            g.DrawPolygon(cP,points[0].p);
            g.DrawPolygon(cP, points[1].p);
            g.DrawPolygon(cP, points[8].p);
            g.DrawPolygon(cP, points[9].p);
            g.DrawPolygon(cP, points[10].p);
            dp.DashStyle = DashStyle.Dot;
            g.DrawLine(dp,points[2].p[1],points[10].p[0]);
            g.DrawLine(dp, points[2].p[4], points[2].p[3]);
            g.DrawLine(dp, points[10].p[2], points[4].p[3]);
            g.DrawLine(dp, points[6].p[1], points[6].p[2]);
            g.DrawLine(dp, points[10].p[1], points[7].p[2]);
            g.DrawLine(dp, points[3].p[2], points[3].p[3]);
            //文字绘制
            g.DrawString("砂-粉砂-黏土",LabelFont, new SolidBrush(Color.Black), 263, 313);

            g.TranslateTransform(241,261);
            g.RotateTransform(-60);
            g.DrawString("砂质黏土", LabelFont, new SolidBrush(Color.Black), 0, 0);
            g.ResetTransform();

            g.TranslateTransform(191, 358);
            g.RotateTransform(-60);
            g.DrawString("黏土质砂", LabelFont, new SolidBrush(Color.Black), 0, 0);
            g.ResetTransform();

            g.TranslateTransform(333, 201);
            g.RotateTransform(60);
            g.DrawString("粉砂质黏土", LabelFont, new SolidBrush(Color.Black), 0, 0);
            g.ResetTransform();

            g.TranslateTransform(392, 300);
            g.RotateTransform(60);
            g.DrawString("黏土质粉砂", LabelFont, new SolidBrush(Color.Black), 0, 0);
            g.ResetTransform();

            foreach (Points temp in points)
            {
                if (temp.i == 1 || temp.i == 8 || temp.i == 9 || temp.i == 6 || temp.i == 7)
                {
                    g.DrawString(temp.name, LabelFont, new SolidBrush(base.ForeColor), temp.pointC.X - 20, temp.pointC.Y);
                }
            }
            for (int i = 0; i < MyObject.Sum1; i++)
            {
                TouDian(g, MyObject.A1[i], MyObject.B1[i], MyObject.C1[i]);
            }
            g.Dispose();
        }

        public void TouDian(Graphics g, int a, int b, int c)//投点算法
        {
            PointF basePoint = new PointF(point.X - length / 2, point.Y + length * C / 2.0f);//定义三角形左边那个角的点为原点；
            float x = basePoint.X + (c + a / 2) * 4;
            float y = basePoint.Y - 2 * a * C;
            g.DrawEllipse(cP, x, y, 2, 2);
        }

        //求中心点坐标函数
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


        //区域检测算法
        public bool IsPtIn(PointF p1, PointF[] p2)//参数1代表的是鼠标点击所在的点，参数2代表的是某个区域的边界点
        {
           
            if (p2.Length < 3)//如果构成区域的边界点数小于3，那么就不能构成一个封闭的区域，return false
                return false;
            int intSum = 0;
            int count = p2.Length;  //构成区域的边界点的个数
            float y0, y1, x0, x1;
            float x;
            for (int i = 0; i < count; i++)
            {
                if (i == count - 1)
                {
                    x0 = p2[count - 1].X;
                    y0 = p2[count - 1].Y;
                    x1 = p2[0].X;
                    y1 = p2[0].Y;
                }
                else
                {
                    x0 = p2[i].X;//x0被赋值给多变型第I个点的横坐标
                    y0 = p2[i].Y;//y0被赋值给多变型第I个点的纵坐标
                    x1 = p2[i + 1].X;//x0被赋值给多变型第I+1个点的横坐标
                    y1 = p2[i + 1].Y;//y0被赋值给多变型第I+1个点的纵坐标
                }
                if ((p1.Y > y0 && p1.Y < y1) || (p1.Y < y0 && p1.Y > y1))//p1代表的是鼠标点击的位置，这句话是说鼠标点击点的纵坐标总是在多边形最高的点和多边形最低的点的范围内
                {
                    if ((y0 - y1) != 0.0f)
                    {
                        x = x0 - (x0 - x1) * (y0 - p1.Y) / (y0 - y1);//这句话是几个意思啊？
                        if (x < p1.X)
                            intSum++;
                    }
                }
            }
            if ((intSum % 2) == 0)//明白了！！明白了。判断点是否在多边形的区域内有个算法，从点击的点向左方（或者右方）作出射线，从左往右数交点
                return false;//如果交点是偶数则不在区域内，否则在该区域内
            else
                return true;
        }


        private void Form3_MouseClick(object sender, MouseEventArgs e)
        {
           // MyObject.MyCurrentFrmName1 = this.Name;
            MyObject.MyCurrentFrm1 = this;
            
            Graphics g1;
            g1 = this.CreateGraphics();
          // MessageBox.Show(e.Location.ToString());
          
            if (IsPtIn(e.Location, points[0].p))
            {
                foreach (Points temp in points)
                {
                    if ((IsPtIn(e.Location, temp.p)) && (temp.i != 0))
                    {
                        g1.Clear(BackColor);
                        g1.FillPolygon(MyBrush, temp.p);
                        Paints();
                        this.label1.Text = "该区域是" + temp.name;
                      //  g1.Dispose();
                        break;
                    }
                }
            }
            else
            {
                this.label1.Text = points[0].name;
            }
            
        }

      

        private void Form3_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            //this.Name = "现代沉淀物Shepard分类图";
           
            foreach (Control c in this.Controls)
            {
                if (c is Label)
                {
                    c.Font = LabelFont;
                    this.Refresh();
                }
            }
        }

        private void 更改画笔颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog diag = new ColorDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                dp.Color = diag.Color;
                cP.Color = diag.Color;
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
                        LabelFont = diag.Font;
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
                MyBrush.Color = diag.Color;
                this.Refresh();
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyObject.Sum1 = 0;
            //MyObject.MyCurrentFrmName1 = null;
            this.Close();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyObject.My_Chart2 = null;
            //这里是复制!!!!!!!名字取错了
            FormState fs = new FormState(cP.Color, MyBrush.Color, LabelFont);
            XmlHelper.SaveForm(fs);
            MyObject.MyCurrentFrmName1 = "现代沉淀物Shepard分类图";
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

        private void SanXianDaiChenDianWuShepard_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
        }
       
    }
}
