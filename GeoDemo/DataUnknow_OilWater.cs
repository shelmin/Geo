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
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel; 
using Microsoft.Office.Interop;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace GeoDemo
{
    public delegate void new_spot(PointF[] p);
    public partial class DataUnknow_OilWater : Form
    {
        public event new_spot draw_point;
        //全局的文件
        OpenFileDialog abovefile = new OpenFileDialog();

        //全局datatable
        System.Data.DataTable Excel_DT;

        //全局dataset
        DataSet allds, myDataSet;
        //存储excel中表名
        List<string> sheetNameList = new List<string>();
        //列名
        List<string> columnNameList = new List<string>();
        //全局临时变量
        List<string> templist = new List<string>();
        public DataUnknow_OilWater()
        {
            InitializeComponent();
        }

        private void button_DataLoad_Click(object sender, EventArgs e)
        {

            #region //选取添加文件
            System.Data.DataTable dT = new System.Data.DataTable();
            //DataSet dS = new DataSet();


            //添加打开文件对象
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //初始指向的地址
            openFileDialog.InitialDirectory = "c:\\";
            //打开excel文件或者文本文件
            openFileDialog.Filter = "xls files(*.xls)|*.xls|xlsx files(*.xlsx)|*.xlsx|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            //string outline = openFileDialog.FileNames;

            #endregion

            abovefile = openFileDialog;
           


            #region //打开文件显示
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //打开新的文件时，清空旧文件内容
                sheetNameList.Clear();
                comboBox4.Items.Clear();
                comboBox6.Items.Clear();
                columnNameList.Clear();
                
                templist.Clear();

                textBox_DataLoad.Text = openFileDialog.FileName;
                //获取文件后缀名，判断调用函数
                string extent = Path.GetExtension(openFileDialog.FileName);

                if (extent.Equals(".xls") || extent.Equals(".xlsx"))
                {
                    
                    allds = ReadExcel(openFileDialog.FileName);
                }
                else if (extent.Equals(".txt"))
                {
                    allds = ReadText(openFileDialog.FileName);
                    
                }
                else
                    MessageBox.Show("文件格式错误");

               

            }
            #endregion

            #region //列名绑定到Combox上
            
            //foreach (string str in columnNameList)
            //{
            //    comboBox_Value1.Items.Add(str);
            //    comboBox_Value2.Items.Add(str);
            //    comboBox_Value3.Items.Add(str);

            //}
            #endregion



            ///把列名赋值到全局区
            for (int i = 0; i < columnNameList.Count; i++)
            {
                templist.Add(columnNameList[i]);
            }




        }
        //excel文件读取
        private DataSet ReadExcel(string filePath)
        {
            DataSet ds = new DataSet();
            using (FileStream fs = File.OpenRead(filePath))
            {
                IWorkbook wb = WorkbookFactory.Create(fs);
                for (int i = 0; i < wb.NumberOfSheets; i++)           //表数
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
                    ISheet iSheet = wb.GetSheetAt(i);
                    sheetNameList.Add(wb.GetSheetName(i));         //保存表名
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
                                columnNameList.Add(iRow.GetCell(k).StringCellValue);                   //获取列名
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

        //读取txt文件
        private DataSet ReadText(string filePath)
        {
            DataSet ds = new DataSet();
            using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
            {
                string txt = sr.ReadToEnd();
                if (string.IsNullOrEmpty(txt))
                {
                    MessageBox.Show("文本为空，请重新选择");
                    return ds;
                }
                //添加表名
                //sheetNameList.Add("sheet1");

                //创建二维数组
                int count1, count2;
                string[,] count_test;
                double[,] count_testNumber;
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
                count_testNumber = new double[count1, count2];
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
                //把除了第一行的数据转化为double的二维数组
                for (i = 1; i < count1; i++)
                {
                    for (int j = 0; j < count2; j++)
                    {
                        count_testNumber[i, j] = Convert.ToDouble(count_test[i, j]);
                    }
                }
                //显示到dataGridview中
                System.Data.DataTable dt = new System.Data.DataTable();
                for (int n = 0; n < count_test.GetLength(0); n++)
                {
                    DataRow dr = dt.NewRow();
                    for (int m = 0; m < count_test.GetLength(1); m++)
                    {
                        if (n == 0)
                        {
                            dt.Columns.Add(count_test[n, m]);
                            columnNameList.Add(count_test[n, m]);
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

        //不加列数的Excel读取
        private DataSet Readexcel(string filePath)
        {
            DataSet ds = new DataSet();
            using (FileStream fs = File.OpenRead(filePath))
            {
                IWorkbook wb = WorkbookFactory.Create(fs);
                for (int i = 0; i < wb.NumberOfSheets; i++)           //表数
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
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
    
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
      //  //创建一个数据链接
      //      string strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = .\\分层界限.xlsx;Extended Properties=Excel 8.0";
      //      OleDbConnection myConn = new OleDbConnection(strCon);
      //      string strCom = " SELECT * FROM [Sheet1$] ";
      //      myConn.Open();
      ////打开数据链接，得到一个数据集
      //      OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);
      // // 创建一个 DataSet对象
      //      myDataSet = new DataSet();
      //  //得到自己的DataSet对象
      //      myCommand.Fill(myDataSet, "[Sheet1$]");
      // // 关闭此数据链接
      //      myConn.Close();

            #region //选取添加文件
           // DataTable dT = new DataTable();
            //DataSet dS = new DataSet();


            //添加打开文件对象
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //初始指向的地址
            openFileDialog.InitialDirectory = ".\\";
            //打开excel文件或者文本文件
            openFileDialog.Filter = "xls files(*.xls)|*.xls|xlsx files(*.xlsx)|*.xlsx|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            //string outline = openFileDialog.FileNames;

            #endregion

            #region //打开文件显示
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //获取文件后缀名，判断调用函数
                string extent = Path.GetExtension(openFileDialog.FileName);

                if (extent.Equals(".xls") || extent.Equals(".xlsx"))
                {

                    myDataSet = Readexcel(openFileDialog.FileName);
                }
                else if (extent.Equals(".txt"))
                {
                    myDataSet = ReadText(openFileDialog.FileName);

                }
                else
                    MessageBox.Show("文件格式错误");



            }
            #endregion
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (string str in columnNameList)
            {
                comboBox4.Items.Add(str);
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (string str in columnNameList)
            {
                comboBox6.Items.Add(str);
            }
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
            if (Oilwater_Data.str_X == "密度")
            {
                p = To_Den(p);
            }
            float ax, by;
            by = Convert.ToSingle(300 - 150 * Math.Log10(Convert.ToSingle(p.Y)));
            // float b = Convert.ToSingle(a);
            ax = 75 + 90 / 5 * p.X;
            PointF pTemp = new PointF(ax, by);
            return pTemp;
        }
        //1/Rt转换关系
        private PointF ConvertToPow(PointF p, float y)
        {
            if (Oilwater_Data.str_X == "密度")
            {
                p = To_Den(p);
            }
            float aX, aY;
            aY = y/*394*/ - Convert.ToSingle(Math.Pow(p.Y, -1 / Oilwater_Data.m)) * 900;
            aX = 75 + 90 / 5 * p.X;
            PointF pTemp = new PointF(aX, aY);
            return pTemp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<float> Top_depth = new List<float>();//存放分层顶深
            List<float> depth_hight = new List<float>();//存放分层深度
            List<float> num1_value = new List<float>();//存放横坐标的对应深度的值
            List<float> num2_value = new List<float>();//存放纵坐标的对应深度的值

            int column_num1=0, column_num2=0;//记录选中列的值
            System.Data.DataTable mydatatable=myDataSet.Tables[0];//分层数据
            System.Data.DataTable alldt=allds.Tables[0];
            //Excel列名
            
            Excel_DT = new System.Data.DataTable();
            Excel_DT.Columns.Add("井名");
            Excel_DT.Columns.Add("起始深度");
            Excel_DT.Columns.Add("测厚");
            Excel_DT.Columns.Add("结果");
            
            //Excel_DT.Columns.Add(mydatatable.Rows[0][0].ToString(), System.Type.GetType("System.String"));
            //Excel_DT.Columns.Add(mydatatable.Rows[0][2].ToString(), System.Type.GetType("System.String"));
            //Excel_DT.Columns.Add(mydatatable.Rows[0][3].ToString(), System.Type.GetType("System.String"));
            //Excel_DT.Columns.Add("结果", System.Type.GetType("System.String"));
            //Excel_DT.Rows[0][0] = mydatatable.Rows[0][0];
            //Excel_DT.Rows[0][1] = mydatatable.Rows[0][2];
            //Excel_DT.Rows[0][2] = mydatatable.Rows[0][3];
            //Excel_DT.Rows[0][3] = "结果";

            //分层划分
            for (int i = 0; i < mydatatable.Rows.Count; i++)
            {
                if (Equals(System.IO.Path.GetFileNameWithoutExtension(abovefile.FileName), mydatatable.Rows[i][0].ToString()))
                {
                    DataRow dr = Excel_DT.NewRow();
                    dr["井名"] = mydatatable.Rows[i][0].ToString();
                    dr["起始深度"] = mydatatable.Rows[i][2].ToString();
                    dr["测厚"] = mydatatable.Rows[i][3].ToString();


                    //Excel_DT.Columns.Add(mydatatable.Rows[i][2].ToString());
                    //Excel_DT.Columns.Add(mydatatable.Rows[i][3].ToString());

                    ////Excel_DT.Rows[i][0] = mydatatable.Rows[i][0];
                    ////Excel_DT.Rows[i][1] = mydatatable.Rows[i][2];
                    ////Excel_DT.Rows[i][2] = mydatatable.Rows[i][3];
                    Excel_DT.Rows.Add(dr);
                    Top_depth.Add( Convert.ToSingle(mydatatable.Rows[i][2]));
                    depth_hight.Add( Convert.ToSingle(mydatatable.Rows[i][3]));
                    
                }
            }
            //Console.Write(abovefile.FileName);
            for (int i = 0; i < columnNameList.Count; i++)
            {
                if (Equals(columnNameList[i] , comboBox4.Text.ToString()))
                {
                    column_num1 = i;//横坐标
                }
                if (Equals(columnNameList[i],comboBox6.Text.ToString()))
                {
                    column_num2 = i;//纵坐标
                }
            }

            PointF[] num_XY = new PointF[Top_depth.Count];

            switch (comboBox2.Text.ToString())
            {
                case"最大值":
                    {
                        for (int i = 0; i < Top_depth.Count; i++)
                        {
                            for (int j = 0; j < alldt.Rows.Count; j++)
                            {
                                if (Convert.ToSingle(alldt.Rows[j][0]) >= Top_depth[i] && Convert.ToSingle(alldt.Rows[j][0]) < Top_depth[i] + depth_hight[i])
                                {
                                    num1_value.Add(Convert.ToSingle(alldt.Rows[j][column_num1]));
                                    num2_value.Add(Convert.ToSingle(alldt.Rows[j][column_num2]));
                                }
                            }
                            num_XY[i].X= num1_value.Max();
                            num_XY[i].Y = num2_value.Max();

                            if (Oilwater_Data.str_Y == "Rt^(-1/m)")
                            {
                                num_XY[i] = ConvertToPow(num_XY[i], Oilwater_Data.Top_Y);
                            }
                            if (Oilwater_Data.str_Y == "对数")
                            {
                                num_XY[i] = ConvertToLog(num_XY[i]);
                            }
                        }
                        draw_point(num_XY);//投点
                        break;
                    }
                case "最小值":
                    {
                        for (int i = 0; i < Top_depth.Count; i++)
                        {
                            for (int j = 0; j < alldt.Rows.Count; j++)
                            {
                                if (Convert.ToSingle(alldt.Rows[j][0]) >= Top_depth[i] && Convert.ToSingle(alldt.Rows[j][0]) < Top_depth[i] + depth_hight[i])
                                {
                                    num1_value.Add(Convert.ToSingle(alldt.Rows[j][column_num1]));
                                    num2_value.Add(Convert.ToSingle(alldt.Rows[j][column_num2]));
                                }
                            }
                            num_XY[i].X = num1_value.Min();
                            num_XY[i].Y = num2_value.Min();

                            if (Oilwater_Data.str_Y == "Rt^(-1/m)")
                            {
                                num_XY[i] = ConvertToPow(num_XY[i], Oilwater_Data.Top_Y);
                            }
                            if (Oilwater_Data.str_Y == "对数")
                            {
                                num_XY[i] = ConvertToLog(num_XY[i]);
                            }
                        }
                        draw_point(num_XY);//投点
                        break;
                    }
                case "算术平均值":
                    {
                        for (int i = 0; i < Top_depth.Count; i++)
                        {
                            for (int j = 0; j < alldt.Rows.Count; j++)
                            {
                                if (Convert.ToSingle(alldt.Rows[j][0]) >= Top_depth[i] && Convert.ToSingle(alldt.Rows[j][0]) < Top_depth[i] + depth_hight[i])
                                {
                                    num1_value.Add(Convert.ToSingle(alldt.Rows[j][column_num1]));
                                    num2_value.Add(Convert.ToSingle(alldt.Rows[j][column_num2]));
                                }
                            }
                            num_XY[i].X = num1_value.Average();
                            num_XY[i].Y = num2_value.Average();

                            if (Oilwater_Data.str_Y == "Rt^(-1/m)")
                            {
                                num_XY[i] = ConvertToPow(num_XY[i], Oilwater_Data.Top_Y);
                            }
                            if (Oilwater_Data.str_Y == "对数")
                            {
                                num_XY[i] = ConvertToLog(num_XY[i]);
                            }
                        }
                        draw_point(num_XY);//投点
                        break;
                    }
                default:
                    {
                        MessageBox.Show("请选择数据提取方式");
                        break;
                    }
            }
            //////////////////////////////////////////////////////////////////////////输出Excel
            for (int i = 0; i < num_XY.Length; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (Oilwater_Data.OW_Area[j].Area.IsVisible(num_XY[i]))
                    {
                        //Console.Write(oilwater_Data.OW_Area[j].AreaName);
                        Excel_DT.Rows[i]["结果"] = Oilwater_Data.OW_Area[j].AreaName;
                        //DataRow dr1 = Excel_DT.NewRow();
                        //dr1["结果"] = oilwater_Data.OW_Area[j].AreaName;
                        //Excel_DT.Rows.InsertAt(dr1, i + 1);
                        //Excel_DT.Rows[i][3] = oilwater_Data.OW_Area[j].AreaName;
                    }
                }
            }
            ////////
            //ExportExcel(Excel_DT);
            DataSet excel_ds = new DataSet();
            excel_ds.Tables.Add(Excel_DT);
            ExportToExcel(excel_ds, "c:\\out");
            this.Close();
        }
        /// <summary>
        /// 到出Excel
        /// </summary>
        /// <param name="dt"></param>
        //protected void ExportExcel(System.Data.DataTable dt)
        //{
        //    if (dt == null || dt.Rows.Count == 0) return;
        //    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

        //    if (xlApp == null)
        //    {
        //        return;
        //    }
        //    System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
        //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        //    Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
        //    Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
        //    Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
        //    Microsoft.Office.Interop.Excel.Range range;
        //    long totalCount = dt.Rows.Count;
        //    long rowRead = 0;
        //    float percent = 0;
        //    for (int i = 0; i < dt.Columns.Count; i++)
        //    {
        //        worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
        //        range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
        //        range.Interior.ColorIndex = 15;
        //        range.Font.Bold = true;
        //    }
        //    for (int r = 0; r < dt.Rows.Count; r++)
        //    {
        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i].ToString();
        //        }
        //        rowRead++;
        //        percent = ((float)(100 * rowRead)) / totalCount;
        //    }
        //    xlApp.Visible = false;
        //    xlApp.ActiveWorkbook.SaveAs("c:\\", Excel.XlFileFormat.xlWorkbookNormal, Type.Missing,
        //Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive,
        //Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        //}






        public static void ExportToExcel(DataSet dataSet, string outputPath)
        {

            Excel.ApplicationClass excel = new Excel.ApplicationClass();
            Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);
            int sheetIndex = 0;
            foreach (System.Data.DataTable dt in dataSet.Tables)
            {
                object[,] data = new object[dt.Rows.Count + 1, dt.Columns.Count];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    data[0, j] = dt.Columns[j].ColumnName;
                }
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        data[i + 1, j] = dt.Rows[i][j];
                    }
                }
                string finalColLetter = string.Empty;

                string colCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                int colCharsetLen = colCharset.Length;
                if (dt.Columns.Count > colCharsetLen)
                {
                    finalColLetter = colCharset.Substring(
                        (dt.Columns.Count - 1) / colCharsetLen - 1, 1);
                }
                finalColLetter += colCharset.Substring(
                        (dt.Columns.Count - 1) % colCharsetLen, 1);

                Excel.Worksheet sheet = (Excel.Worksheet)workbook.Sheets.Add(
                    workbook.Sheets.get_Item(++sheetIndex),
                    Type.Missing, 1, Excel.XlSheetType.xlWorksheet);
                sheet.Name = dt.TableName;
                string range = string.Format("A1:{0}{1}", finalColLetter, dt.Rows.Count + 1);
                sheet.get_Range(range, Type.Missing).Value2 = data;
                ((Excel.Range)sheet.Rows[1, Type.Missing]).Font.Bold = true;
            }
            workbook.SaveAs(outputPath, Excel.XlFileFormat.xlWorkbookNormal, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            workbook.Close(true, Type.Missing, Type.Missing);
            workbook = null;
            excel.Quit();
            KillSpecialExcel(excel);
            excel = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);

        static void KillSpecialExcel(Excel.Application app)
        {
            try
            {
                if (app != null)
                {
                    int processId;
                    GetWindowThreadProcessId(new IntPtr(app.Hwnd), out processId);
                    System.Diagnostics.Process.GetProcessById(processId).Kill();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
