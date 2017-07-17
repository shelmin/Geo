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
	public partial class OilWater : Form
	{
        public Font MyFont = SysData.title_font;
        public Color MyColor = SysData.title_color;
        public string MyText = "油气水";
        public Color MyLineColor1 = SysData.line_color;
        public Color MyLineColor2 = SysData.line_color2;
        public int MyLine1 = SysData.line1;
        public int MyLine2 = SysData.line2;
        public Color MyXBrush = Color.Green;
        public Color MyYBrush = Color.Green;
        public Font MyXFont = new Font("宋体", 9, FontStyle.Regular);
        public Font MyYFont = new Font("宋体", 9, FontStyle.Regular);
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


        Pen pn ;
        Pen pb;

        int num_Ow = 0;
        List<PointF> ow1 = new List<PointF>();
        List<PointF> ow2 = new List<PointF>();
        List<PointF> ow3 = new List<PointF>();
        List<PointF> ow4 = new List<PointF>();
        List<PointF> ow5 = new List<PointF>();
        //全局结构体
        int num_Area = 0;
        public struct region_Area
        {
            public Region Area;
            public string AreaName;
            public SolidBrush AreaBrush;
        }//用来存放区域和颜色

		private struct regionData
		{
			public List<PointF> points;
			public int num;
			public string type;
		}
        //region_Area[] OW_Area;
		private regionData tData;
		SolidBrush[] brushes;
		private List<regionData> regionDatas;                        //存储每一条区域线条点，num对应区域，brush对应填充颜色
		private bool drawFlag=true;                                       //绘图按钮开关标志
		private bool finishFlag = true;                                    //完成一条区域线条结束标志
		#region//图例绘制
		/// <summary>
		/// 图例绘制
		/// </summary>
		/// 图例1，油水同层，红蓝
		private void OilWaterType(PointF point,Graphics g)
		{
			//g.DrawRectangle(new Pen(Color.Black, 1), point.X - 5f, point.Y - 2.5f, 10.0f, 5.0f);
			PointF point1 = new PointF(point.X - 5.0f, point.Y - 2.5f);
			PointF point2 = new PointF(point.X - 5.0f, point.Y + 2.5f);
			PointF point3 = new PointF(point.X + 5.0f, point.Y - 2.5f);
			PointF point4 = new PointF(point.X - 5.0f, point.Y + 2.5f);
			PointF point5 = new PointF(point.X + 5.0f, point.Y + 2.5f);
			PointF point6 = new PointF(point.X + 5.0f, point.Y - 2.5f);
			PointF[] pRed = { point1, point2, point3 };
			PointF[] pBlue = { point4, point5, point6 };
			SolidBrush redBrush = new SolidBrush(Color.Red);
			SolidBrush blueBrush = new SolidBrush(Color.Blue);
			g.FillPolygon(redBrush, pRed);
			g.FillPolygon(blueBrush, pBlue);
		}
		/// 图例2，水层，蓝
		private void WaterType(PointF point, Graphics g)
		{
			//g.DrawRectangle(new Pen(Color.Black, 1), point.X - 5f, point.Y - 2.5f, 10.0f, 5.0f);
			PointF point1 = new PointF(point.X - 5.0f, point.Y - 2.5f);
			PointF point2 = new PointF(point.X - 5.0f, point.Y + 2.5f);
			PointF point3 = new PointF(point.X + 5.0f, point.Y + 2.5f);
			PointF point4 = new PointF(point.X + 5.0f, point.Y - 2.5f);
			PointF []pBlue={point1,point2,point3,point4};
			SolidBrush blueBrush = new SolidBrush(Color.Blue);
			g.FillPolygon(blueBrush, pBlue);
		}
		/// 图例3，差油层，红
		private void LowOilType(PointF point, Graphics g)
		{
			//g.DrawRectangle(new Pen(Color.Black, 1), point.X - 5f, point.Y - 2.5f, 10.0f, 5.0f);
			PointF point1 = new PointF(point.X - 5.0f, point.Y - 2.5f);
			PointF point2 = new PointF(point.X - 5.0f, point.Y + 2.5f);
			PointF point3 = new PointF(point.X + 5.0f, point.Y + 2.5f);
			PointF point4 = new PointF(point.X + 5.0f, point.Y - 2.5f);
			PointF[] pRed = { point1, point2, point3, point4 };
			SolidBrush blueBrush = new SolidBrush(Color.Red);
			g.FillPolygon(blueBrush, pRed);
		}
		/// 图例4，干层，黄色
		private void DryType(PointF point, Graphics g)
		{
			PointF point1 = new PointF(point.X - 5.0f, point.Y - 2.5f);
			PointF point2 = new PointF(point.X - 5.0f, point.Y + 2.5f);
			PointF point3 = new PointF(point.X + 5.0f, point.Y + 2.5f);
			PointF point4 = new PointF(point.X + 5.0f, point.Y - 2.5f);
			PointF[] pGold = { point1, point2, point3, point4 };
			SolidBrush goldBrush = new SolidBrush(Color.Gold);
			g.FillPolygon(goldBrush, pGold);
		}
		/// 图例5，默认点型，绿
		private void DefaultType(PointF point, Graphics g)
		{
			PointF point1 = new PointF(point.X - 5.0f, point.Y - 2.5f);
			PointF point2 = new PointF(point.X - 5.0f, point.Y + 2.5f);
			PointF point3 = new PointF(point.X + 5.0f, point.Y + 2.5f);
			PointF point4 = new PointF(point.X + 5.0f, point.Y - 2.5f);
			PointF[] pGreen = { point1, point2, point3, point4 };
			SolidBrush greenBrush = new SolidBrush(Color.Green);
			g.FillPolygon(greenBrush, pGreen);
		}
		
		private void panelMark_Paint(object sender, PaintEventArgs e)
		{
			using (Graphics g = panelMark.CreateGraphics())
			{
				g.DrawRectangle(new Pen(Color.Red, 1), 0, 0, 109, 109);
				OilWaterType(new PointF(29.0f, 25.0f), g);
				g.DrawString("油水层", base.Font, new SolidBrush(Color.Black), new PointF(45.0f, 20.0f));
				WaterType(new PointF(29.0f, 40.0f), g);
				g.DrawString("水  层", base.Font, new SolidBrush(Color.Black), new PointF(45.0f, 35.0f));
				LowOilType(new PointF(29.0f, 55.0f), g);
				g.DrawString("差油层", base.Font, new SolidBrush(Color.Black), new PointF(45.0f, 50.0f));
				DryType(new PointF(29.0f, 70.0f), g);
				g.DrawString("干  层", base.Font, new SolidBrush(Color.Black), new PointF(45.0f, 65.0f));
				DefaultType(new PointF(29.0f, 85.0f), g);
				g.DrawString("默认点", base.Font, new SolidBrush(Color.Black), new PointF(45.0f, 80.0f));
			}
		}
		#endregion

		/// <summary>
		/// 坐标轴绘制
		/// </summary>
		//x轴线绘制
		private void XAXis()
		{
			using (Graphics g = this.panel_XY.CreateGraphics())
			{
				g.TranslateTransform(75.0f, 50.0f);
				for (int i = 0; i < 6; i++)
				{
					float x = i * 90.0f;
					g.DrawLine(pn, x, 0.0f, x, 305.0f);
                    float str = 5 * i;//X轴坐标字体
                    float lx;
                    if (i < 2)
                        lx = x - 10;
                    else
                        lx = x - 15;
                    g.DrawString(str.ToString("0.00"), MyXFont, new SolidBrush(MyXBrush), lx, 305);
				}
			}
		}
		//对数坐标轴绘制
		private void LogY()
		{
			using (Graphics g = this.panel_XY.CreateGraphics())
			{
				g.TranslateTransform(75.0f, 50.0f);
				for (int i = 0; i < 10; i++)
				{
					float y=1.0f+1.0f*i;
					PointF p = new PointF(-5.0f, y);
					p = ConvertToLog(p);
					g.DrawLine(pn, p.X, p.Y, p.X + 455.0f, p.Y);
                    g.DrawString(y.ToString(), MyYFont, new SolidBrush(MyYBrush), p.X-15, p.Y-5);//绘字
				}
				for (int i = 0; i < 10; i++)
				{
					float y = 10.0f + 10.0f * i;
					PointF p = new PointF(-5.0f, y);
					p = ConvertToLog(p);
					g.DrawLine(pn, p.X, p.Y, p.X + 455.0f, p.Y);
                    g.DrawString(y.ToString(), MyYFont, new SolidBrush(MyYBrush), p.X - 15, p.Y-5);//绘字
				}
			}
		}
		//对数转换关系
		private PointF ConvertToLog(PointF p)
		{
            float  by;
            by = Convert.ToSingle(300 - 150 * Math.Log10(Convert.ToSingle(p.Y)));
            // float b = Convert.ToSingle(a);
            //ax = 75 + 90 / 5 * p.X;
            PointF pTemp = new PointF(p.X, by);
            return pTemp;
		}
		//1/Rt转换关系
        private PointF ConvertToPow(PointF p,float y)
        {
            float  aY;
            aY = y/*394*/ - Convert.ToSingle(Math.Pow(p.Y, -1 / Oilwater_Data.m)) * 900;
            
            PointF pTemp = new PointF(p.X, aY);
            return pTemp;
        }
        //线性转换关系


        //Den转化公式
        private PointF To_Den(PointF p)
        {
            p.X = Oilwater_Data.K * p.X + Oilwater_Data.C;
            return p;
        }
        //绘制1/Rt坐标轴
        private void PowY()
        {
            float TopY = 0;
            TopY = 50 + Convert.ToSingle(Math.Pow(6, -1 / Oilwater_Data.m)) * 900;
            Oilwater_Data.Top_Y = TopY;
            using (Graphics g = this.panel_XY.CreateGraphics())
            {
                for (int i = 6; i < 20; i++)
                {
                    PointF p = new PointF(70f, Convert.ToSingle(i));
                    p = ConvertToPow(p,TopY);
                    g.DrawLine(pn, p.X, p.Y, p.X + 455f, p.Y);
                }
                for (int i = 2; i <= 10;i++ )
                {
                    float itemp = Convert.ToSingle(i * 10);
                    PointF p = new PointF(70f, itemp);
                    p = ConvertToPow(p,TopY);
                    g.DrawLine(pn, p.X, p.Y, p.X + 455f, p.Y);
                }
                //绘制纵轴字体
                for (int i = 6; i <= 10; i++)
                {
                    PointF p = new PointF(70f, Convert.ToSingle(i));
                    p = ConvertToPow(p, TopY);
                    g.DrawString(i.ToString(), MyYFont, new SolidBrush(MyYBrush), 60, p.Y - 5);
                }
                g.DrawString("20", MyYFont, new SolidBrush(MyYBrush), 60, ConvertToPow(new PointF(70f,20f),TopY).Y - 5);
                g.DrawString("50", MyYFont, new SolidBrush(MyYBrush), 60, ConvertToPow(new PointF(70f, 50f), TopY).Y - 5);
                g.DrawString("100", MyYFont, new SolidBrush(MyYBrush), 60, ConvertToPow(new PointF(70f, 100f), TopY).Y - 5);
            }
        }

		//绘制区域虚线
		private void DrawDashLine()
		{
			using (Graphics g = this.panel_XY.CreateGraphics())
			{
				//g.TranslateTransform(75, 50);
				//虚线画笔
				Pen pen = new Pen(Color.Black, 1);
				float[] dashValues = { 5, 2, 15, 4 };
				pen.DashPattern = dashValues;

				if (tData.points.Count > 1)
				{
					for (int i = 0; i < tData.points.Count - 1; i++)
					{
						g.DrawLine(pen, tData.points[i], tData.points[i + 1]);
					}
				}
			}
		}

		public OilWater()
		{
			InitializeComponent();
            ////
            ////禁用一些button键
            //button_Fill.Enabled = SysData.button_enable_judge;
            //button_Cancel.Enabled = SysData.button_enable_judge;
            //button1.Enabled = SysData.button_enable_judge;
            //button_paint.Enabled = SysData.button_enable_judge;


			//初始化tData变量
			tData.num = 0;
			tData.points = new List<PointF>();
			tData.points.Add(new PointF(525.0f, 350.0f));//初始添加右下角的点
			tData.type = "默认类型";
			//初始化regionDatas变量
			regionDatas = new List<regionData>();
			regionDatas.Add(tData);
			//初始化画笔数组
			brushes = new SolidBrush[6]{new SolidBrush(Color.Pink),new SolidBrush(Color.DarkSlateBlue),new SolidBrush(Color.Orange),new
			  SolidBrush(Color.LightGoldenrodYellow),new SolidBrush(Color.Blue),new SolidBrush(Color.BurlyWood)};
            //OW_Area = new region_Area[5];//结构体
            label1.Text = SysData.title;
            label1.ForeColor = SysData.title_color;
            label1.Font = SysData.title_font;
		}

		private void panel_XY_Paint(object sender, PaintEventArgs e)
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
            pn = new Pen(MyLineColor1, MyLine1);
            pb = new Pen(MyLineColor2, MyLine2);

            //MyXFont = MyXFont;
            //Font myYfont = MyYFont;
            //Color myXcolor = MyXBrush;
            //Color myYcolor = MyYBrush;


            if (Oilwater_Data.m.ToString()!=string.Empty&&Oilwater_Data.Draw_point!=null)
            {
                
                paintXY();
                OilWaterSpot(Oilwater_Data.Draw_point, Oilwater_Data.Draw_str);
            }
            Save_printImage();
            
		}
        //绘制第二横坐标
        private void paint_X()
        {
            using (Graphics g = this.panel_XY.CreateGraphics())
            {
                g.DrawLine(pb, 75, 365, 525, 365);
                for (int i = 0; i < 6; i++)
                {
                    g.DrawLine(pb, 75 + i * 90, 370, 75 + i * 90, 365);
                    int a = Convert.ToInt32((i*5 - Oilwater_Data.C) / Oilwater_Data.K);
                    g.DrawString(Convert.ToString(a), MyYFont, new SolidBrush(MyYBrush), 75 + i * 90, 380);
                }
            }
        }
        private void paintXY()
        {
            
            using (Graphics g = this.panel_XY.CreateGraphics())
            {
                //g.Clear(BackColor);
                //g.TranslateTransform(75.0f, 50.0f);
                //PointF P1=new PointF(75,6);
                //P1=ConvertToPow(P1);
                g.DrawRectangle(pb, 75, 50, 450, 300);
                //tData.points.Add(new PointF(450.0f, P1.Y+300f));
                #region 绘制xy轴字
                //x
                g.TranslateTransform(this.panel_XY.Width / 2, this.panel_XY.Height - 20);
                if (MyXCheck)
                {
                    g.RotateTransform(90);
                }
                g.DrawString(MyXnameText, MyXnameFont, new SolidBrush(MyXnameColor), 0, 0);
                //y
                g.ResetTransform();
                g.TranslateTransform(5, this.panel_XY.Height / 2);
                if (MyYCheck)
                {
                    g.TranslateTransform(10, 0);
                    g.RotateTransform(90);
                }
                g.DrawString(MyYnameText, MyYnameFont, new SolidBrush(MyYnameColor), 0, 0);
                g.ResetTransform();
                #endregion
            }
            if (regionDatas.Count > 1) FillRegion();
            if (Oilwater_Data.str_Y == "对数")
            {
                LogY();
            }
            if (Oilwater_Data.str_Y == "Rt^(-1/m)")
            {
                PowY();
                paint_Sw();
            }
            XAXis();
            DrawDashLine();
            if (Oilwater_Data.str_X == "密度")
            {
                paint_X();
            }
            //this.panel_XY.Update();
        }
        /// <summary>
        /// 绘制Sw
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void paint_Sw()
        {
            PointF Zore=new PointF(75,350);
            PointF Sw1=new PointF(0,6);//计算Rt为6时，对应屏幕的Y坐标
            float Y6 = ConvertToPow(Sw1, Oilwater_Data.Top_Y).Y;
            using (Graphics g = this.panel_XY.CreateGraphics())
            {
                Pen PSw=new Pen(Color.Red, 2);
                g.DrawLine(PSw, Zore.X, Zore.Y, 75f+18* Archie_Kong(1f, 6f), 50);
                g.DrawString("Sw=100%", new Font("宋体", 9f), Brushes.Red, 75f + 18 * Archie_Kong(1f, 6f), 50);//绘字
                //Console.Write(Archie_Kong(1, 6));
                g.DrawLine(PSw, Zore.X, Zore.Y, 75 + 18 * Archie_Kong(0.7f, 6f), Y6);
                g.DrawString("Sw=70%", new Font("宋体", 9f), Brushes.Red, 75 + 18 * Archie_Kong(0.7f, 6f), Y6);//绘字
                g.DrawLine(PSw, Zore.X, Zore.Y, 75 + 90 / 5 * 25, Archie_Rt(50f, 25f));
                g.DrawString("Sw=50%", new Font("宋体", 9f), Brushes.Red, 75 + 90 / 5 * 25, Archie_Rt(50f, 25f));//绘字
                g.DrawLine(PSw, Zore.X, Zore.Y, 75 + 90 / 5 * 25, Archie_Rt(0.3f, 0.25f));
                g.DrawString("Sw=30%", new Font("宋体", 9f), Brushes.Red, 75 + 90 / 5 * 25, Archie_Rt(0.3f, 0.25f));//绘字
            }
        }
        //阿尔奇公式
        private float Archie_Kong(float Sw, float Rt)//已知rt求φ
        {
            float k;
            k = Convert.ToSingle(Math.Pow((Oilwater_Data.a * Oilwater_Data.b * Oilwater_Data.Rw) / Sw, Oilwater_Data.n / Oilwater_Data.m))*Convert.ToSingle(Math.Pow(Rt,-1/Oilwater_Data.m));
            return k;
        }
        private float Archie_Rt(float Sw, float k)//已知φ求Rt
        {
            float rt,rt1;
            rt = Convert.ToSingle(Math.Pow((Oilwater_Data.a * Oilwater_Data.b * Oilwater_Data.Rw) / Sw, Oilwater_Data.n) * Math.Pow(k, -Oilwater_Data.m));
            //rt1 = oilwater_Data.Top_Y - Convert.ToSingle(Math.Pow(rt, -1 / oilwater_Data.m)) * 900;
            return rt;
        }





		private void button_paint_MouseClick(object sender, MouseEventArgs e)
		{
			if (drawFlag == true)
			{
				this.panel_XY.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_XY_MouseClick);
				drawFlag = false;
                button_paint.Text = "停止";
               
            }
			else
			{
				this.panel_XY.MouseClick -= new System.Windows.Forms.MouseEventHandler(this.panel_XY_MouseClick);
				drawFlag = true;
                button_paint.Text = "绘制";
                
                
                #region 绘制区域

                finishFlag = true;
                if (Isvalid(tData.points))
                {
                    regionDatas.Add(tData);
                    panel_XY.Refresh();
                    //if (regionDatas.Count > 1) FillRegion();
                }
                #endregion
			}
			
		}

		private void panel_XY_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (finishFlag == true)
				{
					tData.points = new List<PointF>();
					tData.num++;
					finishFlag = false;
				}
				PointF p = new PointF(Convert.ToSingle(e.X ), Convert.ToSingle(e.Y ));
				tData.points.Add(p);
				DrawDashLine();
               
			}
            //if (e.Button == MouseButtons.Right)
            //{
            //    finishFlag = true;
            //    if (Isvalid(tData.points))
            //    {
            //        regionDatas.Add(tData);
            //        panel_XY.Refresh();
            //        //if (regionDatas.Count > 1) FillRegion();
            //    }
            //}
		}

		private void FillRegion()
		{
			using (Graphics g = panel_XY.CreateGraphics())
			{
				//g.TranslateTransform(75.0f, 50.0f);
				//SolidBrush s = new SolidBrush(Color.Gold);
				List<PointF> temp = new List<PointF>();
				for (int i = 0; i < regionDatas.Count - 1; i++)
				{
					temp.Clear();
					temp.AddRange(regionDatas[i + 1].points);
					regionDatas[i].points.Reverse();
					temp.AddRange(regionDatas[i].points);
					regionDatas[i].points.Reverse();
					
					g.FillPolygon(brushes[regionDatas[i+1].num-1], temp.ToArray());
                    GraphicsPath ztpath = new GraphicsPath();//表示一系列的线围成的图
                    ztpath.AddPolygon(temp.ToArray());//向该路径添加z多边形
                    Oilwater_Data.OW_Area[num_Area].Area = new Region(ztpath);//添加区域
                    Oilwater_Data.OW_Area[num_Area].AreaBrush = brushes[regionDatas[i + 1].num - 1];//添加填充颜色
                    if (num_Area == 3)
                    {
                        temp.Clear();
                        //PointF[] three_p = new PointF[] { new PointF(525.0f, 50.0f), new PointF(75.0f, 50.0f), new PointF(75.0f, 350.0f) };
                        PointF[] three_p = new PointF[] { new PointF(75.0f, 350.0f), new PointF(75.0f, 50.0f),new PointF(525.0f, 50.0f)  };
                        //temp.Add(new PointF(525.0f, 50.0f));
                        //temp.Add(new PointF(75.0f, 50.0f));
                        //temp.Add(new PointF(75.0f, 350.0f));
                        temp.AddRange(three_p);
                        regionDatas[i+1].points.Reverse();
                        temp.AddRange(regionDatas[i+1].points);
                        regionDatas[i+1].points.Reverse();
                        g.FillPolygon(brushes[regionDatas[i + 1].num], temp.ToArray());
                        GraphicsPath ztpath5 = new GraphicsPath();//表示一系列的线围成的图
                        ztpath5.AddPolygon(temp.ToArray());//向该路径添加z多边形
                        Oilwater_Data.OW_Area[num_Area + 1].Area = new Region(ztpath5);//添加区域
                        Oilwater_Data.OW_Area[num_Area+1].AreaBrush = brushes[regionDatas[i + 1].num];//添加填充颜色
                        this.panel_XY.MouseClick -= new System.Windows.Forms.MouseEventHandler(this.panel_XY_MouseClick);
                        drawFlag = true;
                        this.button_paint.Enabled = false;
                    }
				}
			}
            if (num_Area < 3)
                num_Area++;
		}

		//检测数据是否是从下方，到上方，并且合法
		private bool Isvalid(List<PointF> p)
		{
			bool flag;
			int num = p.Count - 1;
			if (IsUnder(p[0]) && IsRight(p[num]))
			{
				int i = 0, j = 0;
				foreach (PointF m in p)
				{
					if (IsUnder(m))
						i++;
					if (IsRight(m))
						j++;
				}
				if (i == 1 && j == 1)
					flag = true;
				else
					flag = false;
			}
			else
				flag = false;

            ///////////////////////////////////////需要修改的和矩形下边和右边交点的变化/////////////////////////////////////////////
			if (flag == true)
			{
				PointF temp=new PointF(( p[0].X - (p[0].X - p[1].X) / (p[0].Y - p[1].Y) * (p[0].Y - 350.0f)),350.0f);
				p[0] = temp;
				temp = new PointF(525.0f, (p[num].Y - (p[num].X - 525.0f) * (p[num].Y - p[num - 1].Y) / (p[num].X - p[num - 1].X)));
				p[num] = temp;
			}
			return flag;
		}

		//分布区域检测，位于矩形区域下方
		private bool IsUnder(PointF p)
		{
			bool flag;
			if (p.X < 525f && p.X >= 0.0f && p.Y > 350.0f)
				flag = true;
			else
				flag = false;
			return flag;
		}

		//分布区域检测，位于矩形区域右方
		private bool IsRight(PointF p)
		{
			bool flag;
			if (p.X > 525.0f && p.Y > 0.0f && p.Y < 350.0f)
				flag = true;
			else
				flag = false;
			return flag;
		}

		//响应填充刷新界面
		private void refresh(SolidBrush[] b,string[] str )
		{
			for (int i = 0; i < b.Length; i++)
			{
				brushes[i] = b[i];
			}
			this.panel_XY.Refresh();
		}

		private void button_Fill_Click(object sender, EventArgs e)
		{
			OilWaterColorSelect colorSelect=new OilWaterColorSelect();
			colorSelect.refreshForm += refresh;
			colorSelect.ShowDialog();
		}
       
		private void OilWaterSpot(PointF[] p,string[] strs)//投点
		{
            //
            //禁用一些button键
            button_Fill.Enabled = SysData.button_enable_judge;
            button_Cancel.Enabled = SysData.button_enable_judge;
            button1.Enabled = SysData.button_enable_judge;
            button_paint.Enabled = SysData.button_enable_judge;
            
			using(Graphics g=this.panel_XY.CreateGraphics())
			{
				//g.TranslateTransform(75.0f, 50.0f);
				for (int i = 0; i < p.Length; i++)
				{
					switch (strs[i])
					{
						case "油水同层":
							{
								//p[i].X = p[i].X * 18;
								OilWaterType(p[i], g);
                                if(num_Ow==0)
                                ow1.Add(p[i]);
                                
                                    break;
							}
						case "水层":
							{
								//p[i].X = p[i].X * 18;
								WaterType(p[i], g);
                                if (num_Ow == 0)
                                ow2.Add(p[i]);
								break;
							}
						case "差油层":
							{
								//p[i].X = p[i].X * 18;
								LowOilType(p[i], g);
                                if (num_Ow == 0)
                                ow3.Add(p[i]);
								break;
							}
						case "干层":
							{
								//p[i].X = p[i].X * 18;
								DryType(p[i], g);
                                if (num_Ow == 0)
                                ow4.Add(p[i]);
								break;
							}
						default:
							{
								//p[i].X = p[i].X * 18;
								DefaultType(p[i], g);
                                if (num_Ow == 0)
                                ow5.Add(p[i]);
								break;
							}
					}
                }
               
			}
            num_Ow++;
		}
        private void NewSpot(PointF[] p)
        {

            using (Graphics g = this.panel_XY.CreateGraphics())
            {
                for(int i=0;i<p.Length;i++)
                {
                    g.FillRectangle(new SolidBrush(Color.Black), p[i].X, p[i].Y, 4, 4);
                }
               
            }
        }
		public void button_Set_Click(object sender, EventArgs e)
		{
			Form_DataSet form_DataSet = new Form_DataSet();
			form_DataSet.oilWaterSpot += OilWaterSpot;
            form_DataSet.paint_XY += paintXY;
            form_DataSet.ShowDialog();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            //regionDatas.Clear();
            //tData.points.Clear();
            //tData.points.Add(new PointF(450.0f, 300.0f));//初始添加右下角的点
            //regionDatas.Add(tData);
            this.panel_XY.Controls.Clear();
            num_Area = 0;//肖宇博
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Oilwater_Data.OW_Area != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (Oilwater_Data.OW_Area[i].Area.IsVisible(ow1[1]))

                    {
                        Oilwater_Data.OW_Area[i].AreaName = "油水同层";
                    }
                    if (Oilwater_Data.OW_Area[i].Area.IsVisible(ow2[1]))
                    { 
                        Oilwater_Data.OW_Area[i].AreaName = "水层";
                    }
                    if (Oilwater_Data.OW_Area[i].Area.IsVisible(ow3[1]))
                    { 
                        Oilwater_Data.OW_Area[i].AreaName = "差油层";
                    }
                    if (Oilwater_Data.OW_Area[i].Area.IsVisible(ow4[1]))
                    { 
                        Oilwater_Data.OW_Area[i].AreaName = "干层";
                    }
                    if (Oilwater_Data.OW_Area[i].Area.IsVisible(ow5[1]))
                    { 
                        Oilwater_Data.OW_Area[i].AreaName = "默认"; 
                    }
                    Console.Write(Oilwater_Data.OW_Area[i].AreaName);
                }
            }
            DataUnknow_OilWater f1 = new DataUnknow_OilWater();
            f1.draw_point += NewSpot;
            f1.ShowDialog();
        }
        /// <summary>
        /// 截屏函数
        /// </summary>
        private void Save_printImage()
        {
            Graphics g1 = this.CreateGraphics();              //获得窗体图形对象 
            Bitmap MyImage = new Bitmap(this.panel_XY.Width, this.panel_XY.Height, g1);
            SysData.PrintBit = MyImage;
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

        private void panel_XY_DoubleClick(object sender, EventArgs e)
        {
            Setofline_oilwater setline = new Setofline_oilwater(this);
            setline.paint_refresh += selfrefresh;
            setline.ShowDialog();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //复制窗体,把窗体内容序列化
            FormState fs = new FormState(MyText, MyColor, MyFont, MyLineColor1, MyLineColor2, MyLine1, MyLine2, SysData.dt, MyXBrush, MyYBrush, MyXFont,
                                         MyYFont, MyXnameFont, MyXnameColor, MyXnameText, MyYnameFont, MyYnameColor, MyYnameText, MyXCheck, MyYCheck);
            XmlHelper.SaveForm(fs);
            MyObject.FrmName1 = this.Name;//如果萨胡成因的窗体被点击了，那么就把名字给当前操作窗体的名字
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MyObject.FrmName1 = null;
            this.Close();
        }

        private void 油气水_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            MyObject.FrmName2 = "油气水";
        }

        private void OilWater_MouseClick(object sender, MouseEventArgs e)
        {
            MyObject.FrmName2 = "油气水";
            this.BringToFront();
        }

        private void panel_XY_Click(object sender, EventArgs e)
        {
            MyObject.FrmName2 = "油气水";
            this.BringToFront();
        }

        private void OilWater_MouseUp(object sender, MouseEventArgs e)
        {
            MainFrame mf = new MainFrame();
            mf.getMsg();
        }

       
	}

}
