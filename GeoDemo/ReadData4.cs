﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using NPOI.SS.UserModel;

namespace GeoDemo
{
    public partial class ReadData4 : Form
    {
        public ReadData4()
        {
            InitializeComponent();
        }

        //存储excel中表名
        List<string> sheetNameList = new List<string>();
        //列名
        List<string> columnNameList = new List<string>();
        //合并表展示最终需要数据
        DataTable dataTables = new DataTable();

        public static string AxisX_SelectIndex;//combox中选择绑定哪一项为X值
        public static string AxisY1_SelectIndex;//combox中选择绑定哪一项为Y1值
        public static string AxisY2_SelectIndex;//combox中选择绑定哪一项为Y2值
        public static string AxisY3_SelectIndex;//combox中选择绑定哪一项为Y3值
        public static string AxisY4_SelectIndex;//combox中选择绑定哪一项为Y4值

        private void button_Select_Click(object sender, EventArgs e)
        {
            String DebugPath = Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("\\"));//这里把\\Debug移除了
            //String binPath = Application.StartupPath.Substring(0, DebugPath.LastIndexOf("\\"));//这里把\\bin移除了
            //String Path1 = Application.StartupPath.Substring(0, binPath.LastIndexOf("\\"));//再向上寻址
            //String Path2 = Application.StartupPath.Substring(0, Path1.LastIndexOf("\\"));//再向上寻址

            #region //选取添加文件
            DataTable dT = new DataTable();
            DataSet dS = new DataSet();
            //添加打开文件对象
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //初始指向的地址
            openFileDialog.InitialDirectory = DebugPath;
            //打开excel文件或者文本文件
            openFileDialog.Filter = "All files (*.*)|*.*|txt files (*.txt)|*.txt|xls files(*.xls)|*.xls|xlsx files(*.xlsx)|*.xlsx";
            #endregion

            #region 打开文件显示
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //打开新的文件时，清空旧文件内容
                sheetNameList.Clear();
                comboBox_Value1.Items.Clear();
                comboBox_Value2.Items.Clear();
                comboBox_Value3.Items.Clear();
                comboBox_Value4.Items.Clear();
                comboBox_Value5.Items.Clear();
                columnNameList.Clear();
                dataTables.Columns.Clear();

                DataPath.Text = openFileDialog.FileName;
                //获取文件后缀名，判断调用函数
                string extent = Path.GetExtension(openFileDialog.FileName);

                if (extent.Equals(".xls") || extent.Equals(".xlsx"))
                {
                    // MessageBox.Show("读取excel");
                    dS = ReadExcel(openFileDialog.FileName);
                }
                else if (extent.Equals(".txt"))
                {
                    dS = ReadText(openFileDialog.FileName);
                    //MessageBox.Show("读取txt");
                }
                else
                    MessageBox.Show("文件格式错误");

                DataGridViewMaker(dS);
            }
            else
            {
                return;
            }
            #endregion

            #region 列名绑定到Combox上
            foreach (string str in columnNameList)
            {
                comboBox_Value1.Items.Add(str);
                comboBox_Value2.Items.Add(str);
                comboBox_Value3.Items.Add(str);
                comboBox_Value4.Items.Add(str);
                comboBox_Value5.Items.Add(str);
            }
            #endregion

            try
            {
                dataTables = dS.Tables[0];

                for (int j = 0; j < dS.Tables[0].Columns.Count; j++)
                {
                    dgvData.Columns[j].HeaderText = columnNameList[j];
                }
                dS.Tables.Clear();
            }
            catch
            {
                return;
            }
        }

        #region excel文件读取
        private DataSet ReadExcel(string filePath)
        {
            try
            {
                DataSet ds = new DataSet();
                using (FileStream fs = File.OpenRead(filePath))
                {
                    IWorkbook wb = WorkbookFactory.Create(fs);
                    for (int i = 0; i < wb.NumberOfSheets; i++)           //表数
                    {
                        DataTable dt = new DataTable();
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
            catch
            {
                MessageBox.Show("文件读取失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;
            }

        }

        #endregion

        #region 读取txt文件
        private DataSet ReadText(string filePath)
        {
            DataSet ds = new DataSet();

            int RowCount = 0;//统计行数
            int ColumnCount = 0;//统计列数
            getRowCountAndColumnCount(filePath, ref RowCount, ref ColumnCount);//得到文件的行列值
            String[,] WordsArray = new string[RowCount, ColumnCount];//用文件的行列值申请一个二维数组
            ConvertToStrArray(filePath, ref WordsArray);//此处把从文件中读取到的数据变成了一个二维数组
            SetDataGridViewValue(ref ds, WordsArray);//把二维数组复制给datagridview

            return ds;
        }

        public void getRowCountAndColumnCount(string filePath, ref int RowCount, ref int ColumnCount)
        {
            using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
            {
                //添加表名
                sheetNameList.Add("sheet1");

                int j = 0;//列值用来循环的局部变量
                while (true)
                {
                    string EachLinetxt = sr.ReadLine();//读了一行数据出来

                    if (EachLinetxt == null || EachLinetxt.Equals(""))//如果读完了。就是说如果下一行读出来的是空值，就跳出循环不读数据了
                    {
                        break;
                    }

                    //这里的EachLinetxt处理一下把\t去掉,统计行列的数目，设个断点看起来很直观
                    Regex reg = new Regex(@"\S+");
                    foreach (Match m in reg.Matches(EachLinetxt))
                    {
                        j++;//改行列的数目加一
                    }
                    RowCount++;//一行结束了，行数要加一
                    ColumnCount = j;//一行结束了，把该列的值复制给ColoumCount表示该行有多少列
                    j = 0;//每一行循环完了，就把列值还原为0
                }

            }

        }
        public bool isErrorShow = false;
        public void ConvertToStrArray(string filePath, ref string[,] WordsArray)//把从txt中读取的内容保存为一个二维数组
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
                {
                    int j = 0;//列值用来循环的局部变量
                    int i = 0;//行值用来循环的局部变量
                    while (true)
                    {

                        string EachLinetxt = sr.ReadLine();//读了一行数据出来
                        if (i == 0 && (EachLinetxt == null || EachLinetxt.Equals("")))
                        {
                            MessageBox.Show("第一行不可以为空", "出错", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            this.Close();
                            return;
                        }
                        if (EachLinetxt == null || EachLinetxt.Equals(""))//如果读完了。就是说如果下一行读出来的是空值，就跳出循环不读数据了
                        {
                            break;
                        }

                        //这里的EachLinetxt处理一下把\t去掉,统计行列的数目，设个断点看起来很直观
                        Regex reg = new Regex(@"\S+");

                        foreach (Match m in reg.Matches(EachLinetxt))
                        {

                            WordsArray[i, j++] = m.Value;
                        }
                        i++; //一行循环玩了开始下一行
                        j = 0;//每一行循环完了，就把列值还原为0
                    }
                }
            }
            catch
            {
                isErrorShow = true;
                this.button_Bind.Enabled = false;
                MessageBox.Show("数据读取过程中出错，请检查是否有非法数据！", "出错", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public void SetDataGridViewValue(ref DataSet dataset, string[,] WordsArray)
        {
            //显示到dataGridview中
            try
            {
                DataTable dt = new DataTable();
                int row = WordsArray.GetLength(0);
                int col = WordsArray.GetLength(1);
                string[,] ContentArray = new string[row - 1, col];//将第一行去掉保存在一个字符串数组中
                for (int i = 1; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        ContentArray[i - 1, j] = WordsArray[i, j];
                    }
                }
                for (int j = 0; j < col; j++)
                    columnNameList.Add(WordsArray[0, j]);//columnNameList代表列名

                dt = ConvertToDataTable(ContentArray);//将字符串数组变为DataTable
                dataset.Tables.Add(dt);
            }
            catch
            {
                if (isErrorShow == false)
                {
                    MessageBox.Show("数据显示出错，请检查是否有非法数据！", "出错", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                return;
            }
        }

        public DataTable ConvertToDataTable(string[,] arr)//将字符串数组变为DataTable
        {

            DataTable dataSouce = new DataTable();
            for (int i = 0; i < arr.GetLength(1); i++)
            {
                DataColumn newColumn = new DataColumn(i.ToString(), arr[0, 0].GetType());
                dataSouce.Columns.Add(newColumn);
            }
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                DataRow newRow = dataSouce.NewRow();
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    newRow[j.ToString()] = arr[i, j];
                }
                dataSouce.Rows.Add(newRow);
            }
            return dataSouce;

        }

        #endregion 

        //表格绘制
        DataGridView dgvData = new DataGridView();
        private void DataGridViewMaker(DataSet tdS)
        {
            try
            {

                //绘制表格前，将tab控件中内容清空
                tabControl_Data.TabPages.Clear();
                //绘制
                for (int i = 0; i < tdS.Tables.Count; i++)
                {
                    dgvData.Dock = DockStyle.Fill;
                    dgvData.DataSource = tdS.Tables[i];
                    dgvData.Width = tabControl_Data.Width - 10;
                    dgvData.Height = tabControl_Data.Height - 20;
                    dgvData.ScrollBars = ScrollBars.Both;
                    TabPage tabPage = new TabPage();
                    tabPage.Text = sheetNameList[i];
                    tabPage.Controls.Add(dgvData);
                    tabControl_Data.TabPages.Add(tabPage);

                }
            }
            catch
            {
                return;
            }


            //给DataGridView列名赋值

        }

        private void button_Bind_Click(object sender, EventArgs e)
        {
            //绘制表格
            if (this.comboBox_Value1.SelectedIndex == -1 || this.comboBox_Value2.SelectedIndex == -1 || this.comboBox_Value3.SelectedIndex == -1 || this.comboBox_Value4.SelectedIndex == -1 || this.comboBox_Value5.SelectedIndex == -1)
            {
                MessageBox.Show("X轴或者Y轴必须要绑定数据，不得为空！");
                return;
            }
            try
            {
                DataSet newDataSet = new DataSet();
                newDataSet.Tables.Add(dataTables);

                DataGridViewMaker(newDataSet);

                MyObject.Dataset = newDataSet;
                AxisX_SelectIndex = this.comboBox_Value1.SelectedItem.ToString();
                AxisY1_SelectIndex = this.comboBox_Value2.SelectedItem.ToString();
                AxisY2_SelectIndex = this.comboBox_Value3.SelectedItem.ToString();
                AxisY3_SelectIndex = this.comboBox_Value4.SelectedItem.ToString();
                AxisY4_SelectIndex = this.comboBox_Value5.SelectedItem.ToString();
                string strX = this.comboBox_Value1.SelectedItem.ToString();
                string strY1 = this.comboBox_Value2.SelectedItem.ToString();
                string strY2 = this.comboBox_Value3.SelectedItem.ToString();
                string strY3 = this.comboBox_Value4.SelectedItem.ToString();
                string strY4 = this.comboBox_Value5.SelectedItem.ToString();
                //int X = this.comboBox_Value1.SelectedIndex;
                //int Y = this.comboBox_Value2.SelectedIndex;
                for (int i = 0; i < dataTables.Columns.Count; i++)
                {
                    dataTables.Columns[i].ColumnName = columnNameList[i];//给每一列一个列名和读取的txt中文件的列名一样
                }

                if (strX.Equals(strY1) || strX.Equals(strY2) || strX.Equals(strY3) || strX.Equals(strY4) || strY1.Equals(strY2) || strY1.Equals(strY3) || strY1.Equals(strY4) || strY2.Equals(strY3) || strY2.Equals(strY4) || strY4.Equals(strY3))
                {
                    return;
                }
                DataTable dat = dataTables.DefaultView.ToTable(false, new string[] { strX, strY1, strY2, strY3,strY4 });
                newDataSet.Tables.Clear();
                newDataSet.Tables.Add(dat);

                DataGridViewMaker(newDataSet);



                MessageBox.Show("绑定成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch
            {
                MessageBox.Show("绑定失败,请仔细核对", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                this.Close();
            }
        }

        private void DataPath_TextChanged(object sender, EventArgs e)
        {
            if (DataPath.Text == string.Empty)
            {
                button_Bind.Enabled = false;
            }
            else
            {
                button_Bind.Enabled = true;
            }
        }

        private void comboBox_Value1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = this.comboBox_Value1.SelectedItem.ToString();
            this.textBox2.Text = this.comboBox_Value1.SelectedItem.ToString();
            this.textBox3.Text = this.comboBox_Value1.SelectedItem.ToString();
        }

        private void ReadData4_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (MyObject.Dataset != null)//已经绑定了数据
                {
                    //if (this.comboBox_Value2.SelectedItem.ToString().Equals(this.comboBox_Value3.SelectedItem.ToString()) || this.comboBox_Value2.SelectedItem.ToString().Equals(this.comboBox_Value4.SelectedItem.ToString()) || this.comboBox_Value3.SelectedItem.ToString().Equals(this.comboBox_Value4.SelectedItem.ToString()))
                    //{
                    //    MessageBox.Show("绑定的Y轴有重复项，请重新绑定！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //    return;
                    //}

                    ReadDataFromDatatable readdatafromdatatable = new ReadDataFromDatatable();
                    int countx = readdatafromdatatable.getCounx();//从datatable中读取有多少行数据
                    string[] x = new string[countx];
                    double[] y = new double[countx];
                    double[] yy = new double[countx];
                    double[] yyy = new double[countx];
                    double[] yyyy = new double[countx];
                    readdatafromdatatable.readDataService(ref x, ref y, ref x, ref yy, ref x, ref yyy,ref x,ref yyyy);
                    MyObject.My_Chart1.Series[0].Points.DataBindXY(x, y);//将油田1的横纵坐标绑定绘出柱形图
                    MyObject.My_Chart1.Series[1].Points.DataBindXY(x, yy);//将油田1的横纵坐标绑定绘出柱形图
                    MyObject.My_Chart1.Series[2].Points.DataBindXY(x, yyy);//将油田1的横纵坐标绑定绘出柱形图
                    MyObject.My_Chart1.Series[3].Points.DataBindXY(x, yyy);//将油田1的横纵坐标绑定绘出柱形图

                    MyObject.My_Chart1.Titles[0].Text = "双击图形在属性中修改主标题";

                    MyObject.My_Chart1.ChartAreas[0].Name = this.comboBox_Value2.SelectedItem.ToString();
                    MyObject.My_Chart1.ChartAreas[1].Name = this.comboBox_Value3.SelectedItem.ToString();
                    MyObject.My_Chart1.ChartAreas[2].Name = this.comboBox_Value4.SelectedItem.ToString();
                    MyObject.My_Chart1.ChartAreas[3].Name = this.comboBox_Value5.SelectedItem.ToString();
                    MyObject.My_Chart1.ChartAreas[0].AxisY.Title = this.comboBox_Value2.SelectedItem.ToString();//后期这里的ChartArea.Name就是从TXT中读取的列名
                    MyObject.My_Chart1.ChartAreas[1].AxisY.Title = this.comboBox_Value3.SelectedItem.ToString();//后期这里的ChartArea.Name就是从TXT中读取的列名
                    MyObject.My_Chart1.ChartAreas[2].AxisY.Title = this.comboBox_Value4.SelectedItem.ToString();//后期这里的ChartArea.Name就是从TXT中读取的列名
                    MyObject.My_Chart1.ChartAreas[3].AxisY.Title = this.comboBox_Value5.SelectedItem.ToString();//后期这里的ChartArea.Name就是从TXT中读取的列名
                }
            }
            catch
            {
                MessageBox.Show("绑定失败,请仔细核对", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void ReadData4_Load(object sender, EventArgs e)
        {
            //每一次读取数据窗体显示出来的时候就将上次读取的内容清楚
            MyObject.Dataset = null;
        }

        #region  王旭峰写的实现复制粘贴功能的代码段

        private void tabControl_Data_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Y < this.tabControl_Data.GetTabRect(0).Bottom) && (e.Button == MouseButtons.Right))
            {
                contextMenuStrip1.Show(this.tabControl_Data, e.X, e.Y);//让右键菜单显示出来
            }
        }


        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button_Bind.Enabled = true;
            columnNameList.Clear();
            dataTables.Columns.Clear();
            //同时把combox中的类容清楚
            this.comboBox_Value1.Items.Clear();

            this.comboBox_Value2.Items.Clear();

            sheetNameList.Add("sheet1");
            DataSet ds = new DataSet();
            try
            {
                // 获取剪切板的内容，并按行分割  
                string pasteText = "";
                pasteText = Clipboard.GetText();

                if (string.IsNullOrEmpty(pasteText))//判断文件是否为空
                {
                    MessageBox.Show("剪切板内容为空");
                    return;
                }

                if (pasteText == "pasteText")//这句话是干嘛？
                {
                    return;
                }
                int tnum = 0;
                int nnum = 0;
                //获得当前剪贴板内容的行、列数
                for (int i = 0; i < pasteText.Length; i++)
                {
                    if (pasteText.Substring(i, 1) == "\t")//从第i开始截取1个字符，此处相当于遍历了整个剪切板的内容
                    {
                        //如果遇到一个\t那么就是列值加1
                        tnum++;
                    }
                    if (pasteText.Substring(i, 1) == "\n")
                    {
                        //如果遇到一个\n那么就是行值加1
                        nnum++;
                    }
                } //最终获得当前剪贴板内容的行nnum,注意此时的tnum是6

                string[,] data;
                //粘贴板上的数据来自于EXCEL时，每行末都有\n，在DATAGRIDVIEW内复制时，最后一行末没有\n
                if (pasteText.Substring(pasteText.Length - 1, 1) == "\n")
                {
                    nnum = nnum - 1;//去掉最后一行的 空列
                }
                tnum = tnum / (nnum + 1);//到这里才可以得出除去第一行的行列值
                data = new string[nnum + 1, tnum + 1];//定义一个二维数组

                String rowstr;
                rowstr = "";
                //MessageBox.Show(pasteText.IndexOf("B").ToString());
                //对数组赋值
                for (int i = 0; i < (nnum + 1); i++)
                {
                    for (int colIndex = 0; colIndex < (tnum + 1); colIndex++)
                    {
                        //是否是某一行中的最后一列
                        if (colIndex == tnum && pasteText.IndexOf("\r") != -1)
                        {
                            rowstr = pasteText.Substring(0, pasteText.IndexOf("\r"));
                        }
                        //最后一行的最后一列
                        if (colIndex == tnum && pasteText.IndexOf("\r") == -1)
                        {
                            rowstr = pasteText.Substring(0);
                        }
                        //其他行列
                        if (colIndex != tnum)
                        {
                            rowstr = pasteText.Substring(0, pasteText.IndexOf("\t"));//从0开始截取到\t符号，就是一个单元格的内容
                            pasteText = pasteText.Substring(pasteText.IndexOf("\t") + 1);//然后将pasteText从第二个单元格开始，第一个单元格的内容不要了
                        }
                        data[i, colIndex] = rowstr;//把每个单元格的内容给data数组
                    }
                    //截取下一行数据
                    pasteText = pasteText.Substring(pasteText.IndexOf("\n") + 1);
                }

                //到此次上述单元格的内容全部被复制给 了data数组
                DataTable dt = new DataTable();
                for (int n = 0; n < data.GetLength(0); n++)
                {
                    DataRow dr = dt.NewRow();
                    for (int m = 0; m < data.GetLength(1); m++)
                    {
                        if (n == 0)
                        {
                            dt.Columns.Add(data[n, m]);
                            columnNameList.Add(data[n, m]);//把第一行中的列名字赋值
                        }
                        else
                            dr[m] = data[n, m];
                    }
                    if (n != 0)
                        dt.Rows.Add(dr);
                }

                ds.Tables.Add(dt);
                DataGridViewMaker(ds);
                #region 列名绑定到Combox上
                foreach (string str in columnNameList)
                {

                    comboBox_Value1.Items.Add(str);
                    comboBox_Value2.Items.Add(str);
                }
                #endregion
                try
                {
                    dataTables = ds.Tables[0];

                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)//列名给datagridview
                    {
                        dgvData.Columns[j].HeaderText = columnNameList[j];
                    }
                    ds.Tables.Clear();
                }
                catch
                {
                    return;
                }
            }
            catch
            {
                Clipboard.Clear();
                MessageBox.Show("错误！");
                return;
            }
        }


        #endregion 

    }
}
