/*
 * 含砾沉积物三角形分类图
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
    public partial class SanHanLiChenJiWu : Form
    {
        //常量根号3
        float C = 1.7320f;
        //存放投点区域的字符数组
        public static string[] d = new string[100];
        //常量画笔
        public  Pen cP = new Pen(Color.Black, 2.0f);
        public  SolidBrush MyBrush = new SolidBrush(Color.Yellow);

        public Font LabelFont = Label.DefaultFont;
        //三角形边长
        private float length = 400.0f;
        //顶点坐标
        private PointF point = new PointF(310.0f, 100.0f);
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
        private Points[] points=new Points[15];
        public SanHanLiChenJiWu()
        {
            InitializeComponent();
            this.MouseClick+=new MouseEventHandler(Form1_MouseClick);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            #region 点的赋值
            //大三角形
            points[0].p = new PointF[3];
            points[0].p[0] = point;
            points[0].p[1] = new PointF(point.X - length / 2, point.Y + length * C/ 2.0f);
            points[0].p[2] = new PointF(point.X + length / 2, point.Y + length / 2 * C);
            points[0].str = "含砾沉积物三角形分类图";
            points[0].name = "含砾沉积物三角形分类图";
            points[0].i = 0;
            //points[0].pointC = new PointF((points[0].p[0].X + points[0].p[1].X + points[0].p[2].X) / 3.0f, (points[0].p[0].Y + points[0].p[1].Y + points[0].p[2].Y) / 3.0f);
            points[0].pointC = new PointF(point.X, point.Y + length * C * 0.5f + 20.0f);

            //模块1数据
            points[1].p = new PointF[3];
            points[1].p[0] = point;
            points[1].p[1] = new PointF(point.X - 0.2f * length * 0.5f, point.Y + 0.2f * length * C * 0.5f);
            points[1].p[2] = new PointF(point.X + 0.2f * length * 0.5f, point.Y + 0.2f * length * C * 0.5f);
            points[1].str = "G";
            points[1].name = "砾";
            points[1].i = 1;
            points[1].pointC = Point_Center(points[1].p);
            
            //模块2数据
            points[2].p = new PointF[4];
            points[2].p[0] = points[1].p[1];
            points[2].p[1] = new PointF(point.X, points[2].p[0].Y);
            points[2].p[2] = new PointF(point.X, point.Y + length * 0.7f * C * 0.5f);
            points[2].p[3] = new PointF(point.X - length * 0.7f * 0.5f, points[2].p[2].Y);
            points[2].str = "mG";
            points[2].name = "泥质砾";
            points[2].i = 2;
            points[2].pointC = Point_Center(points[2].p);

            //模块3数据
            points[3].p = new PointF[4];
            points[3].p[0] = points[2].p[1];
            points[3].p[1] = new PointF(point.X + length * 0.2f * 0.4f, points[3].p[0].Y);
            points[3].p[2] = new PointF(point.X + length * 0.7f * 0.4f, points[2].p[2].Y);
            points[3].p[3] = points[2].p[2];
            points[3].str = "msG";
            points[3].name = "泥质砂质砾";
            points[3].i = 3;
            points[3].pointC = Point_Center(points[3].p);

            //模块4数据
            points[4].p = new PointF[4];
            points[4].p[0] = points[3].p[1];
            points[4].p[1] = points[1].p[2];
            points[4].p[2] = new PointF(point.X + length * 0.7f * 0.5f, point.Y + length * 0.7f * C*0.5f);
            points[4].p[3] = points[3].p[2];
            points[4].str = "sG";
            points[4].name = "砂质砾";
            points[4].i = 4;
            points[4].pointC = Point_Center(points[4].p);

            //模块5数据
            points[5].p = new PointF[4];
            points[5].p[0] = new PointF(point.X - length * 0.7f * 0.5f, point.Y + length * 0.7f * 0.5f * C);
            points[5].p[1] = new PointF(point.X, point.Y + length * 0.7f * 0.5f * C);
            points[5].p[2] = new PointF(point.X, point.Y + length * 0.95f * 0.5f * C);
            points[5].p[3] = new PointF(point.X - length * 0.95f * 0.5f, point.Y + length * 0.95f * 0.5f * C);
            points[5].str = "gM";
            points[5].name = "砾质泥";
            points[5].i = 5;
            points[5].pointC = Point_Center(points[5].p);

            //模块6数据
            points[6].p = new PointF[4];
            points[6].p[0] = new PointF(point.X, point.Y + length * 0.7f * 0.5f * C);
            points[6].p[1] = new PointF(point.X + length * 0.7f * 0.4f, point.Y + length * 0.7f * 0.5f * C);
            points[6].p[2] = new PointF(point.X + length * 0.95f * 0.4f, point.Y + length * 0.95f * 0.5f * C);
            points[6].p[3] = new PointF(point.X, point.Y + length * 0.95f * 0.5f * C);
            points[6].str = "gmS";
            points[6].name = "砾质泥质砂";
            points[6].i = 6;
            points[6].pointC = Point_Center(points[6].p);

            //模块7数据
            points[7].p = new PointF[4];
            points[7].p[0] = new PointF(point.X + length * 0.7f * 0.4f, point.Y + length * 0.7f * 0.5f * C);
            points[7].p[1] = new PointF(point.X + length * 0.7f * 0.5f, point.Y + length * 0.7f * 0.5f * C);
            points[7].p[2] = new PointF(point.X + length * 0.95f * 0.5f, point.Y + length * 0.95f * 0.5f * C);
            points[7].p[3] = new PointF(point.X + length * 0.95f * 0.4f, point.Y + length * 0.95f * 0.5f * C);
            points[7].str = "gS";
            points[7].name = "砾质砂";
            points[7].i = 7;
            points[7].pointC = Point_Center(points[7].p);

            //模块8数据
            points[8].p = new PointF[4];
            points[8].p[0] = new PointF(point.X - length * 0.95f * 0.5f, point.Y + length * 0.95f * 0.5f * C);
            points[8].p[1] = new PointF(point.X, point.Y + length * 0.95f * 0.5f * C);
            points[8].p[2] = new PointF(point.X, point.Y + length * 0.99f * 0.5f * C);
            points[8].p[3] = new PointF(point.X - length * 0.99f * 0.5f, point.Y + length * 0.99f * 0.5f * C);
            points[8].str = "(g)M";
            points[8].name = "含砾泥";
            points[8].i = 8;
            points[8].pointC = Point_Center(points[8].p);

            //模块9数据
            points[9].p = new PointF[4];
            points[9].p[0] = new PointF(point.X, point.Y + length * 0.95f * 0.5f * C);
            points[9].p[1] = new PointF(point.X + length * 0.95f * 0.4f, point.Y + length * 0.95f * 0.5f * C);
            points[9].p[2] = new PointF(point.X + length * 0.99f * 0.4f, point.Y + length * 0.99f * 0.5f * C);
            points[9].p[3] = new PointF(point.X, point.Y + length * 0.99f * 0.5f * C);
            points[9].str = "(g)mS";
            points[9].name = "含砾泥质砂";
            points[9].i = 9;
            points[9].pointC = Point_Center(points[9].p);

            //模块10数据
            points[10].p = new PointF[4];
            points[10].p[0] = new PointF(point.X + length * 0.95f * 0.4f, point.Y + length * 0.95f * 0.5f * C);
            points[10].p[1] = new PointF(point.X + length * 0.95f * 0.5f, point.Y + length * 0.95f * 0.5f * C);
            points[10].p[2] = new PointF(point.X + length * 0.99f * 0.5f, point.Y + length * 0.99f * 0.5f * C);
            points[10].p[3] = new PointF(point.X + length * 0.99f * 0.4f, point.Y + length * 0.99f * 0.5f * C);
            points[10].str = "(g)S";
            points[10].name = "含砾砂";
            points[10].i = 10;
            points[10].pointC = Point_Center(points[10].p);

            //模块11数据
            points[11].p = new PointF[4];
            points[11].p[0] = new PointF(point.X - length * 0.99f * 0.5f, point.Y + length * 0.99f * 0.5f * C);
            points[11].p[1] = new PointF(point.X - length * 0.99f * 0.4f, point.Y + length * 0.99f * 0.5f * C);
            points[11].p[2] = new PointF(point.X - length * 0.4f, point.Y + length * 0.5f * C);
            points[11].p[3] = new PointF(point.X - length * 0.5f, point.Y+length * 0.5f * C);
            points[11].str = "M";
            points[11].name = "泥";
            points[11].i = 11;
            points[11].pointC = Point_Center(points[11].p);

            //模块12数据
            points[12].p = new PointF[4];
            points[12].p[0] = new PointF(point.X - length * 0.99f * 0.4f, point.Y + length * 0.99f * 0.5f * C);
            points[12].p[1] = new PointF(point.X, point.Y + length * 0.99f * 0.5f * C);
            points[12].p[2] = new PointF(point.X, point.Y + length * 0.5f * C);
            points[12].p[3] = new PointF(point.X - length * 0.4f, point.Y + length * 0.5f * C);
            points[12].str = "sM";
            points[12].name = "砂质泥";
            points[12].i = 12;
            points[12].pointC = Point_Center(points[12].p);

            //模块13数据
            points[13].p = new PointF[4];
            points[13].p[0] = new PointF(point.X, point.Y + length * 0.99f * 0.5f * C);
            points[13].p[1] = new PointF(point.X + length * 0.99f * 0.4f, point.Y + length * 0.99f * 0.5f * C);
            points[13].p[2] = new PointF(point.X + length * 0.4f, point.Y + length * 0.5f * C);
            points[13].p[3] = new PointF(point.X, point.Y + length * 0.5f * C);
            points[13].str = "mS";
            points[13].name = "泥质砂";
            points[13].i = 13;
            points[13].pointC = Point_Center(points[13].p);

            //模块14数据
            points[14].p = new PointF[4];
            points[14].p[0] = new PointF(point.X + length * 0.99f * 0.4f, point.Y + length * 0.99f * 0.5f * C);
            points[14].p[1] = new PointF(point.X + length * 0.99f * 0.5f, point.Y + length * 0.99f * 0.5f * C);
            points[14].p[2] = new PointF(point.X + length * 0.5f, point.Y + length * 0.5f * C);
            points[14].p[3] = new PointF(point.X + length * 0.4f, point.Y + length * 0.5f * C);
            points[14].str = "S";
            points[14].name = "砂";
            points[14].i = 14;
            points[14].pointC = Point_Center(points[14].p);
            #endregion 

            Paints();
        }
        //绘制图形
        private void Paints()
        {
            
            Graphics g = this.CreateGraphics();
            //DataForSanDuanYuan DD = new DataForSanDuanYuan();
            
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
               
                g.DrawString(temp.str, base.Font, new SolidBrush(base.ForeColor), temp.pointC);
            }
           //在这写一个函数用来投点
            
            //dt.Columns.Add("投点所在区域");
            for (int i = 0; i <MyObject .Sum1  ; i++)
            {
                TouDian(g, MyObject .A1[i],MyObject .B1 [i],MyObject .C1 [i] );
            }
            g.Dispose();
        }
        public void TouDian(Graphics g,int a,int b,int c)//投点算法
        {
            PointF  basePoint =new PointF(point.X - length / 2, point.Y + length * C/ 2.0f);//定义三角形左边那个角的点为原点；
            float  x = basePoint.X + (c + a / 2)*4;
            float y = basePoint.Y - 2*a*C;
            //PointF p1 = new PointF(x,y);
            //g.DrawLine(cP,p1,p1);
            g.DrawEllipse(cP, x, y, 2, 2);
            //Rectangle r = new Rectangle(,basePoint.Y-b*length*0.5,10,10);
           
          
        }

        public string TouDianFenLeiPanBie(int a, int b, int c) //判别投点落在哪个区域的算法
        {
            string y = "";
            if (a >80)
            {
                y = "砾";
                
                
            }
            else if (a <= 80 && a >30)
            {
                if ((b / b + c) > 0.5)
                {
                    y = "泥质砾"; 
                }
                else if ((b / b + c) <= 1 / 9)
                {
                    y = "砂质砾";
                }
                else
                {
                    y = "泥质砂质砾";
                }
            }
            else if (a > 5 && a <= 30)
            {
                if ((b / b + c) > 0.5)
                {
                    y = "励志泥";
 
                }
                else if ((b / b + c) <= 1 / 9)
                {
                    y = "gS";
                }
                else
                {
                    y = "gmS";
                }
            }
            return y;
            //else 
            //{
            //    if ((b / b + c) > 0.5)
            //    {
            //        y = "";
            //    }
            //    else if ((b / b + c) < 1 / 9)
            //    {

            //    }
            //    else
            //    {

            //    }

            //}
        }

        //求中心点坐标函数
        public PointF Point_Center(PointF []p)
        {
            float X=0.0f, Y=0.0f;
            PointF temp=new PointF(0.0f,0.0f);
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
        //跳转按钮
  
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

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //MyObject.MyCurrentFrmName1 = this.Name;
            MyObject.MyCurrentFrm1 = this;
            Graphics g1;
            g1 = this.CreateGraphics();
           //MessageBox.Show(e.Location.ToString());
            if (IsPtIn(e.Location, points[0].p))
            {
                foreach (Points temp in points)
                {
                    if ((IsPtIn(e.Location, temp.p)) && (temp.i != 0))
                    {
                        g1.Clear(BackColor);
                        //Paints();
                        g1.FillPolygon(MyBrush, temp.p);
                        Paints();
                        this.label1.Text = "该区域是" + temp.name;
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

        private void 含砾沉积物三角形分类图_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            //this.Name = "含砾沉积物三角形分类图";
            //MyObject.MyPen1 = cP;
            //MyObject.MyBrush1 = new SolidBrush(Color.Yellow);
            foreach (Control c in this.Controls)
            {
                if (c is Label)
                {
                    c.Font=LabelFont ;
                    this.Refresh();
                }
            }
        }

        private void 含砾沉积物三角形分类图_DoubleClick(object sender, EventArgs e)
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

        private void 含砾沉积物三角形分类图_MouseMove(object sender, MouseEventArgs e)
        {

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

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyObject.Sum1 = 0;
            //MyObject.MyCurrentFrmName1 = null;
            this.Close();
            
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //三端元图的复制
            //复制窗体,把窗体内容序列化
            MyObject.My_Chart2 = null;
            FormState fs = new FormState( cP.Color,  MyBrush.Color,LabelFont);
            XmlHelper.SaveForm(fs);
            MyObject.MyCurrentFrmName1 = "含砾沉积物三角形分类图";
            MyObject.FrmName1 = null;
            MyObject.MyCurrentFrm1 = this;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void 投点ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void SanHanLiChenJiWu_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
        }

    }
}
