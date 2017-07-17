using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NPOI.SS.UserModel;
using System.Text.RegularExpressions;

namespace GeoDemo
{
	public delegate void Spot(PointF[] p,string[] str);
    public delegate void paint1();
	public partial class Form_DataSet : Form
	{

		DataSet ds;
        PointF[] points;//存放读取的模块点

		public event Spot oilWaterSpot;
        public event paint1 paint_XY;
        public struct region_Area
        {
            public Region Area;
            public string AreaName;
            public SolidBrush AreaBrush;
        }//用来存放区域和颜色
		public Form_DataSet()
		{
			InitializeComponent();
		}

		private void DataSet_Load(object sender, EventArgs e)
		{
            textBox_K.Enabled = false;
            textBox1.Enabled = false;
		}
        
		private void button_DataLoad_Click(object sender, EventArgs e)
		{
			#region //选取添加文件
			DataTable dT = new DataTable();
			Form_DataSet dS = new Form_DataSet();
			//添加打开文件对象
			OpenFileDialog openFileDialog = new OpenFileDialog();
			//初始指向的地址
			openFileDialog.InitialDirectory = "c:\\";
			//打开excel文件或者文本文件
			openFileDialog.Filter = "xls files(*.xls)|*.xls|xlsx files(*.xlsx)|*.xlsx|txt files (*.txt)|*.txt|All files (*.*)|*.*";
			#endregion

			#region //打开文件显示
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string DataPath = Path.GetExtension(openFileDialog.FileName);
                textBox_DataLoad.Text = DataPath;
                textBox_DataLoad.Enabled = false;

                if (DataPath.Equals(".xls") || DataPath.Equals(".xlsx"))
                {
                    
                    Readexcel(openFileDialog.FileName);
                }
                else if (DataPath.Equals(".txt"))
                {
                    ReadText(openFileDialog.FileName);
                    
                }
                else
                    MessageBox.Show("文件格式错误");
			}
			#endregion
			
		}

       /// <summary>
       /// 读取excel
       /// </summary>
       /// <param name="filePath"></param>
       /// <returns></returns>
		private DataSet Readexcel(string filePath)
		{
			ds =new DataSet();
			using (FileStream fs = File.OpenRead(filePath))
			{
				IWorkbook wb = WorkbookFactory.Create(fs);
				for (int i = 0; i < wb.NumberOfSheets; i++)           //表数
				{
					DataTable dt = new DataTable();
					ISheet iSheet = wb.GetSheetAt(i);
					if (iSheet.LastRowNum == 0)                            //若为空表，跳过下面操作
						continue;
					for (int j = 0; j <= iSheet.LastRowNum; j++)     //行数
					{
						IRow iRow = iSheet.GetRow(j);
						DataRow dr = dt.NewRow();
						for (int k = 0; k < iRow.LastCellNum; k++)    //列数
						{
							//初始化表格
							if (j == 0)
							{
								dt.Columns.Add(iRow.GetCell(k).StringCellValue);
							}
							else
							{
								dr[k] = iRow.GetCell(k).ToString();
							}
						}
						if (j != 0)
							dt.Rows.Add(dr);
					}
					ds.Tables.Add(dt);
				}
			}
			return ds;
		}
        /// <summary>
        /// 读取txt
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private DataSet ReadText(string filePath)
        {
            ds = new DataSet();
            using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
            {
                string txt = sr.ReadToEnd();
                if (string.IsNullOrEmpty(txt))
                {
                    MessageBox.Show("文本为空，请重新选择");
                    return ds;
                }
                //添加表名
               // sheetNameList.Add("sheet1");

                //创建二维数组
                int count1, count2;
                string[,] count_test;
                //double[,] count_testNumber;
                count2 = 0;
                //分割每行
                string[] txtArg = txt.Split('\n');
                count1 = txtArg.Length - 1;//统计行数
                Regex reg = new Regex(@"\S+");
                foreach (Match m in reg.Matches(txtArg[0]))//统计列数
                {

                    count2++;

                }
                //Console.Write(count2);
                count_test = new string[count1, count2];
                //count_testNumber = new double[count1, count2];
                int i = 0;
                //把txt中的所有值以字符串赋给二维数组
                foreach (string a_line in txtArg)
                {
                    int j = 0;
                    foreach (Match m in reg.Matches(a_line))
                    {

                        count_test[i, j] = m.Value;

                        j++;

                    }
                    i++;
                }
                ////把除了第一行的数据转化为double的二维数组
                //for (i = 1; i < count1; i++)
                //{
                //    for (int j = 0; j < count2; j++)
                //    {
                //        count_testNumber[i, j] = Convert.ToDouble(count_test[i, j]);
                //    }
                //}
                //显示到dataGridview中
                DataTable dt = new DataTable();
                for (int n = 0; n < count_test.GetLength(0); n++)
                {
                    DataRow dr = dt.NewRow();
                    for (int m = 0; m < count_test.GetLength(1); m++)
                    {
                        if (n == 0)
                        {
                            dt.Columns.Add(count_test[n, m]);
                           // columnNameList.Add(count_test[n, m]);
                        }
                        else
                            dr[m] = count_test[n, m];
                    }
                    if (n != 0)
                        dt.Rows.Add(dr);
                }

                ds.Tables.Add(dt);
            }
            return ds;
        }

        //Den转化公式
        private PointF To_Den(PointF p)
        {
            p.X = Oilwater_Data.K * p.X + Oilwater_Data.C;
            return p;
        }

        //对数转换关系
        private PointF ConvertToLog(PointF p)
        {
            if (comboBox1.Text.ToString() == "密度")
            {
                p = To_Den(p);
            }
            float ax,by;
            by = Convert.ToSingle(300 - 150 * Math.Log10(Convert.ToSingle(p.Y)));
           // float b = Convert.ToSingle(a);
            ax = 75 + 90 / 5 * p.X; 
            PointF pTemp = new PointF(ax, by);
            return pTemp;
        }
        //1/Rt转换关系
        private PointF ConvertToPow(PointF p, float y)
        {
            if (comboBox1.Text.ToString() == "密度")
            {
                p = To_Den(p);
            }
            float aX,aY;
            aY = y/*394*/ - Convert.ToSingle(Math.Pow(p.Y, -1 / Oilwater_Data.m)) * 900;
            aX = 75 + 90 / 5 * p.X; 
            PointF pTemp = new PointF(aX, aY);
            return pTemp;
        }

		private void button_OK_Click(object sender, EventArgs e)
        {
            try
            {

                //参数pag
                Oilwater_Data.a = Convert.ToSingle(textBox2.Text);
                Oilwater_Data.b = Convert.ToSingle(textBox3.Text);
                Oilwater_Data.Rw = Convert.ToSingle(textBox4.Text);
                Oilwater_Data.m = Convert.ToSingle(textBox5.Text);
                Oilwater_Data.n = Convert.ToSingle(textBox6.Text);
                paint_XY();
                if (comboBox1.Text.ToString() == "密度")
                {
                    Oilwater_Data.C = Convert.ToSingle(textBox1.Text);
                    Oilwater_Data.K = Convert.ToSingle(textBox_K.Text);
                }

                points = new PointF[ds.Tables[0].Rows.Count];
                string[] strs = new string[ds.Tables[0].Rows.Count];
                Oilwater_Data.Draw_point = new PointF[points.Length];
                Oilwater_Data.Draw_str = new string[strs.Length];
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string str = dt.Columns[i].ColumnName.ToString();
                    if (str != "RILD" && str != "孔隙度" && str != "油层性质")
                    {
                        dt.Columns.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    points[i].Y = Convert.ToSingle(dt.Rows[i][0]);
                    //////////////////////////////////////////////////////////////////////////预留处理横坐标是密度情况
                    points[i].X = Convert.ToSingle(dt.Rows[i][1]);
                    Oilwater_Data.Draw_str[i] = strs[i] = dt.Rows[i][2].ToString();
                    if (Oilwater_Data.str_Y == "Rt^(-1/m)")
                    {
                        Oilwater_Data.Draw_point[i] = points[i] = ConvertToPow(points[i], Oilwater_Data.Top_Y);
                    }
                    if (Oilwater_Data.str_Y == "对数")
                    {
                        Oilwater_Data.Draw_point[i] = points[i] = ConvertToLog(points[i]);
                    }
                    if (Oilwater_Data.str_Y == string.Empty)
                    {
                        MessageBox.Show("请选择Y轴类型");
                    }
                }
                //for(inti=0;)


                SysData.button_enable_judge = true;//将button键设置为可以使用
                oilWaterSpot(points, strs);

                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作出错!");
                return;
            }
		}
        /// <summary>
        /// 参数设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;

            ////oilwater_Data.a = Convert.ToSingle(textBox2.Text);
            ////oilwater_Data.b = Convert.ToSingle(textBox3.Text);
            ////oilwater_Data.Rw = Convert.ToSingle(textBox4.Text);
            //oilwater_Data.m = Convert.ToSingle(textBox5.Text);
            ////oilwater_Data.n = Convert.ToSingle(textBox6.Text);
            //paint_XY();
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region combo传值
           // 
            Oilwater_Data.str_Y = Convert.ToString(comboBox2.Text);

            #endregion
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox_K.Enabled = false;
            Oilwater_Data.C = 0;
            Oilwater_Data.K = 1;
            Oilwater_Data.str_X = Convert.ToString(comboBox1.Text);
            if (comboBox1.Text.ToString() == "密度")
            {
                textBox1.Enabled = true;
                textBox_K.Enabled = true;
                //oilwater_Data.C = Convert.ToSingle(textBox1.Text);
                //oilwater_Data.K = Convert.ToSingle(textBox_K.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
        }
        
 


	}
}
