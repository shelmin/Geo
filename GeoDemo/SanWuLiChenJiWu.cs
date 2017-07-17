/*
 * 无砾沉积物三角形分类图
*/
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
    public partial class SanWuLiChenJiWu : Form
    {
        //常量根号3
        float C = 1.7320f;
        //常量画笔
       public  Pen cP = new Pen(Color.Black, 2.0f);
       public SolidBrush MyBrush = new SolidBrush(Color.Yellow);
       public Font LabelFont = Label.DefaultFont;

        //三角形边长
        private float length = 400.0f;
        //顶点坐标
        private PointF point = new PointF(315.0f, 100.0f);
        //储存模块数据结构体，顶点坐标，模块名称，模块编号，模块中心点坐标
        private struct Points
        {
            public PointF[] p;
            public string str;
            public string name;
            public int i;
            public PointF pointC;
        }
        //结构体数组
        private Points[] points = new Points[11];
        public SanWuLiChenJiWu()
        {
            InitializeComponent();
            this.MouseClick+=new MouseEventHandler(Form2_MouseClick);
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //大三角形
            points[0].p = new PointF[3];
            points[0].p[0] = point;
            points[0].p[1] = new PointF(point.X - length / 2, point.Y + length * C / 2.0f);
            points[0].p[2] = new PointF(point.X + length / 2, point.Y + length / 2 * C);
            points[0].str ="";
            points[0].name = "无砾沉积物三角形分类图";
            points[0].i = 0;
            //points[0].pointC = new PointF((points[0].p[0].X + points[0].p[1].X + points[0].p[2].X) / 3.0f, (points[0].p[0].Y + points[0].p[1].Y + points[0].p[2].Y) / 3.0f);
            points[0].pointC = new PointF(point.X, point.Y + length * C * 0.5f + 20.0f);

            //模块1数据
            points[1].p = new PointF[3];
            points[1].p[0] = point;
            points[1].p[1] = new PointF(point.X - 0.1f * length * 0.5f, point.Y + 0.1f * length * C * 0.5f);
            points[1].p[2] = new PointF(point.X + 0.1f * length * 0.5f, point.Y + 0.1f * length * C * 0.5f);
            points[1].str = "S";
            points[1].name = "砂";
            points[1].i = 1;
            points[1].pointC = Point_Center(points[1].p);

            //模块2数据
            points[2].p = new PointF[4];
            points[2].p[0] = points[1].p[1];
            points[2].p[1] = new PointF(points[1].p[1].X + length * 0.1f / 3, points[2].p[0].Y);
            points[2].p[2] = new PointF(point.X - 0.25f * length + length * 0.5f / 3, point.Y + length * 0.5f * C * 0.5f);
            points[2].p[3] = new PointF(point.X - length * 0.25f, points[2].p[2].Y);
            points[2].str = "cS";
            points[2].name = "黏土质砂";
            points[2].i = 2;
            points[2].pointC = Point_Center(points[2].p);

            //模块3数据
            points[3].p = new PointF[4];
            points[3].p[0] = points[2].p[1];
            points[3].p[1] = new PointF(points[2].p[1].X + length * 0.1f / 3, points[3].p[0].Y);
            points[3].p[2] = new PointF(points[2].p[2].X + length * 0.5f / 3, points[2].p[2].Y);
            points[3].p[3] = points[2].p[2];
            points[3].str = "mS";
            points[3].name = "泥质砂";
            points[3].i = 3;
            points[3].pointC = Point_Center(points[3].p);

            //模块4数据
            points[4].p = new PointF[4];
            points[4].p[0] = points[3].p[1];
            points[4].p[1] = points[1].p[2];
            points[4].p[2] = new PointF(point.X + length * 0.25f, points[3].p[2].Y);
            points[4].p[3] = points[3].p[2];
            points[4].str = "zS";
            points[4].name = "粉砂质砂";
            points[4].i = 4;
            points[4].pointC = Point_Center(points[4].p);

            //模块5数据
            points[5].p = new PointF[4];
            points[5].p[0] = points[2].p[3];
            points[5].p[1] = points[2].p[2];
            points[5].p[2] = new PointF(point.X - length * 0.9f * 0.5f + length * 0.9f / 3, point.Y + length * 0.9f * 0.5f * C);
            points[5].p[3] = new PointF(point.X - length * 0.9f * 0.5f, point.Y + length * 0.9f * 0.5f * C);
            points[5].str = "sC";
            points[5].name = "砂质黏土";
            points[5].i = 5;
            points[5].pointC = Point_Center(points[5].p);

            //模块6数据
            points[6].p = new PointF[4];
            points[6].p[0] = points[5].p[1];
            points[6].p[1] = new PointF(points[6].p[0].X + length * 0.5f / 3, points[6].p[0].Y);
            points[6].p[2] = new PointF(points[5].p[2].X + length * 0.9f / 3, points[5].p[2].Y);
            points[6].p[3] = points[5].p[2];
            points[6].str = "sM";
            points[6].name = "砂质泥";
            points[6].i = 6;
            points[6].pointC = Point_Center(points[6].p);

            //模块7数据
            points[7].p = new PointF[4];
            points[7].p[0] = points[6].p[1];
            points[7].p[1] = new PointF(points[6].p[1].X + length * 0.5f / 3, points[6].p[1].Y);
            points[7].p[2] = new PointF(points[6].p[2].X + length * 0.9f / 3, points[6].p[2].Y);
            points[7].p[3] = points[6].p[2];
            points[7].str = "sZ";
            points[7].name = "砂质粉砂";
            points[7].i = 7;
            points[7].pointC = Point_Center(points[7].p);

            //模块8数据
            points[8].p = new PointF[4];
            points[8].p[0] = points[5].p[3];
            points[8].p[1] = points[5].p[2];
            points[8].p[2] = new PointF(point.X - length * 0.5f + length / 3, point.Y + length * 0.5f * C);
            points[8].p[3] = new PointF(point.X - length * 0.5f, point.Y + length * 0.5f * C);
            points[8].str = "C";
            points[8].name = "黏土";
            points[8].i = 8;
            points[8].pointC = Point_Center(points[8].p);

            //模块9数据
            points[9].p = new PointF[4];
            points[9].p[0] = points[8].p[1];
            points[9].p[1] = points[6].p[2];
            points[9].p[2] = new PointF(points[8].p[2].X + length / 3, points[8].p[2].Y);
            points[9].p[3] = points[8].p[2];
            points[9].str = "M";
            points[9].name = "泥";
            points[9].i = 9;
            points[9].pointC = Point_Center(points[9].p);

            //模块10数据
            points[10].p = new PointF[4];
            points[10].p[0] = points[9].p[1];
            points[10].p[1] = points[7].p[2];
            points[10].p[2] = new PointF(points[8].p[3].X + length, points[9].p[2].Y);
            points[10].p[3] = points[9].p[2];
            points[10].str = "Z";
            points[10].name = "粉砂";
            points[10].i = 10;
            points[10].pointC = Point_Center(points[10].p);
            Paints();


        }
        //绘制图形
        private void Paints()
        {
            Graphics g = this.CreateGraphics();
            //模块绘制
            foreach (Points temp in points)
            {
                if (temp.str == null)
                    break;
                g.DrawPolygon(cP, temp.p);
            }
            //文字绘制
            foreach (Points temp in points)
            {
                if (temp.str == null)
                    break;
                if (temp.i > 7)
                {
                    base.Font = new Font(base.Font.Name, base.Font.Size, base.Font.Style);
                }
                g.DrawString(temp.str, base.Font, new SolidBrush(base.ForeColor), temp.pointC);
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
            //PointF p1 = new PointF(x,y);
            //g.DrawLine(cP,p1,p1);
            g.DrawEllipse(cP, x, y, 2, 2);
            //Rectangle r = new Rectangle(,basePoint.Y-b*length*0.5,10,10);


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
        ////跳转按钮
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    this.Hide();
        //    含砾沉积物三角形分类图 old = new 含砾沉积物三角形分类图();
        //    old.Show();
        //}
        //区域检测算法
        public bool IsPtIn(PointF p1, PointF[] p2)
        {
            if (p2.Length < 3)
                return false;
            int intSum = 0;
            int count = p2.Length;
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
                    x0 = p2[i].X;
                    y0 = p2[i].Y;
                    x1 = p2[i + 1].X;
                    y1 = p2[i + 1].Y;
                }
                if ((p1.Y > y0 && p1.Y < y1) || (p1.Y < y0 && p1.Y > y1))
                {
                    if ((y0 - y1) != 0.0f)
                    {
                        x = x0 - (x0 - x1) * (y0 - p1.Y) / (y0 - y1);
                        if (x < p1.X)
                            intSum++;
                    }
                }
            }
            if ((intSum % 2) == 0)
                return false;
            else
                return true;
        }

        private bool abs()
        {
            throw new NotImplementedException();
        }

        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            //MyObject.MyCurrentFrmName1 = this.Name;
            MyObject.MyCurrentFrm1 = this;
            Graphics g1;
            g1 = this.CreateGraphics();
            if (IsPtIn(e.Location, points[0].p))
            {
                foreach (Points temp in points)
                {
                    if ((IsPtIn(e.Location, temp.p)) && (temp.i != 0))
                    {
                        g1.Clear(BackColor);
                       
                        this.label1.Text ="该区域是：" + temp.name;
                        g1.FillPolygon(MyBrush, temp.p);
                        Paints();
                        g1.Dispose();
                        break;
                    }
                }
            }
            else
            {
                this.label1.Text = points[0].name;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            //this.Name = "无砾沉积物三角形分类图";
            foreach (Control c in this.Controls)
            {
                if (c is Label)
                {
                    c.Font = LabelFont;
                    this.Refresh();
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void 更改画笔颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog diag = new ColorDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
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

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyObject.Sum1 = 0;
            //MyObject.MyCurrentFrmName1 = null;
            this.Close();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyObject.My_Chart2 = null;
            FormState fs = new FormState( cP.Color, MyBrush.Color, LabelFont);
            XmlHelper.SaveForm(fs);
            MyObject.MyCurrentFrmName1 = "无砾沉积物三角形分类图";
            MyObject.FrmName1 = null;
            MyObject.MyCurrentFrm1 = this;
        }

        private void 添加数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            //在这里调用datatable
            DataForSanDuanYuan dfsdy = new DataForSanDuanYuan();
            dfsdy.ShowDialog();
            Graphics g = this.CreateGraphics();
            for (int i = 0; i < MyObject.Sum1; i++)
            {
                TouDian(g, MyObject.A1[i], MyObject.B1[i], MyObject.C1[i]);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SanWuLiChenJiWu_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
        }
    }
    
}
