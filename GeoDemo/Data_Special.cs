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
    public partial class Data_Special : Form
    {
        //存储excel中表名
        List<string> sheetNameList = new List<string>();
        //列名
        List<string> columnNameList = new List<string>();
        //合并表展示最终需要数据
        DataTable dataTables = new DataTable();
        //全局临时变量
        List<string> templist = new List<string>();


        public Data_Special()
        {
            InitializeComponent();
           // this.comboBox_Value1.SelectedIndex = 0;
          //  this.comboBox_Value2.SelectedIndex = 0;
           // ContextMenuStrip contextMenuStrip1 = new ContextMenuStrip();
           // ToolStripMenuItem 复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
           // ToolStripMenuItem 粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
           // 复制ToolStripMenuItem.Text = "复制";
           // 粘贴ToolStripMenuItem.Text = "粘贴";
           // contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
           //     复制ToolStripMenuItem,
           //     粘贴ToolStripMenuItem});
           //// 复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
           // 粘贴ToolStripMenuItem.Click += new EventHandler(粘贴ToolStripMenuItem_Click);
           // //tabControl_Data.ContextMenuStrip = contextMenuStrip1;
        }
      

        //选取加载文件
        private void button_Select_Click(object sender, EventArgs e)
        {
            try
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

                #region //打开文件显示
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //打开新的文件时，清空旧文件内容
                    sheetNameList.Clear();
                    comboBox_Value1.Items.Clear();
                    comboBox_Value2.Items.Clear();
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

                #region //列名绑定到Combox上
                foreach (string str in columnNameList)
                {

                    comboBox_Value1.Items.Add(str);
                    comboBox_Value2.Items.Add(str);
                }
                #endregion
               
                
                ///把列名赋值到全局区
                for (int i = 0; i < columnNameList.Count; i++)
                {
                    templist.Add(columnNameList[i]);
                }

                if (dS.Tables.Count > 1)
                {
                    for (int i = 1; i < dS.Tables.Count; i++)
                    {
                        string strT = "dataTables";
                        dataTables = UniteDataTable2(dS.Tables[0], dS.Tables[i], strT);
                    }
                }
                else
                {
                    dataTables = dS.Tables[0];
                    dS.Tables.Clear();
                }
            }
            catch(Exception se)
            {
                MessageBox.Show(se.Message);
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

        //读取txt文件
        private DataSet ReadText(string filePath)
        {
            DataSet ds = new DataSet(); 
            try
            {
              
                using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default))
                {
                    string txt = sr.ReadToEnd();
                    if (string.IsNullOrEmpty(txt))
                    {
                        MessageBox.Show("文本为空，请重新选择");
                        return ds;
                    }
                    //添加表名
                    sheetNameList.Add("sheet1");

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
                    DataTable dt = new DataTable();
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

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
                return ds;
           


        }
        //表格绘制
        private void DataGridViewMaker(DataSet tdS)
        {
            

            //绘制表格前，将tab控件中内容清空
            tabControl_Data.TabPages.Clear();
            //绘制
            for (int i = 0; i < tdS.Tables.Count; i++)
            {
                DataGridView dgvData = new DataGridView();
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

        //数据绑定
        private void button_Bind_Click(object sender, EventArgs e)
        {

            // MessageBox.Show(common.Isbtn_click.ToString());
            //存储从combobox中读取的数据
            List<string> str = new List<string>();
            //存储非combobox中的数据
            List<string> Temp = new List<string>();

            #region    //读取combobox中选定的数据
            foreach (Control ctr in groupBox_Bind.Controls)
            {
                if (ctr.GetType() == typeof(ComboBox))
                {
                    if (string.IsNullOrEmpty(ctr.Text))
                    {
                        MessageBox.Show("绑定数据不能为空，请选择绑定的数据");
                        return;
                    }
                    else
                    {
                        str.Add(ctr.Text);
                    }
                }
            }
            #endregion

            #region //根据combobox中读取的数据选择datatable中的数据，创建新的datatable
            foreach (string str1 in str)
            {
                foreach (string str2 in columnNameList)
                {
                    if (str1 == str2)
                    {
                        columnNameList.Remove(str1);
                        break;
                    }
                }
            }
            foreach (string str3 in columnNameList)
            {
                dataTables.Columns.Remove(str3);
            }
            #endregion

            try
            {
            //绘制表格
            DataSet newDataSet = new DataSet();
            newDataSet.Tables.Add(dataTables);
            if (MyObject.FrmName2 == "C_M1")
            {
                SysData.cm1_dt = dataTables.Clone();
                SysData.cm1_dt.Clear();
                for (int i = 0; i < dataTables.Rows.Count; i++)
                {
                    SysData.cm1_dt.Rows.Add(dataTables.Rows[i].ItemArray);
                }
            }
            else
            {
                SysData.cm2_dt = dataTables.Clone();
                SysData.cm2_dt.Clear();
                for (int i = 0; i < dataTables.Rows.Count; i++)
                {
                    SysData.cm2_dt.Rows.Add(dataTables.Rows[i].ItemArray);
                }
            }
            DataGridViewMaker(newDataSet);

            #region 进行XY轴选中的判断，使sysdata.dt中的第二列为X轴，第三列为Y轴
            int numX = 0;
            int numY = 0;
            for (int i = 0; i < templist.Count; i++)
            {
                if (comboBox_Value1.SelectedItem.Equals(templist[i]))
                {
                    numX = i;
                }
                if (comboBox_Value2.SelectedItem.Equals(templist[i]))
                {
                    numY = i;
                }
            }
            if (numX > numY)
            {
                if (MyObject.FrmName2 == "C_M1")
                {
                    SysData.cm1_dt.Columns[1].SetOrdinal(0);
                }
                else {
                    SysData.cm2_dt.Columns[1].SetOrdinal(0);
                }
            }
            #endregion
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

        #region /// 将两个列不同(结构不同)的DataTable合并成一个新的DataTable
        /// </summary> 
        /// <param name="DataTable1">表1</param> 
        /// <param name="DataTable2">表2</param> 
        /// <param name="DTName">合并后新的表名</param> 
        /// <returns>合并后的新表</returns> 
        private DataTable UniteDataTable2(DataTable DataTable1, DataTable DataTable2, string DTName)
        {
            DataTable newDataTable = new DataTable();
            if (DataTable1.Rows.Count > DataTable2.Rows.Count)
            {
                newDataTable = FillData(DataTable1, DataTable2);
            }
            else
            {
                newDataTable = FillData(DataTable2, DataTable1);
            }

            newDataTable.TableName = DTName; //设置DT的名字 
            return newDataTable;
        }
        private DataTable FillData(DataTable dt1, DataTable dt2)
        {
            //克隆DataTable1的结构
            DataTable newDataTable = dt1.Clone();
            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                //再向新表中加入DataTable2的列结构
                newDataTable.Columns.Add(dt2.Columns[i].ColumnName);
            }
            object[] obj = new object[newDataTable.Columns.Count];
            //添加DataTable1的数据
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dt1.Rows[i].ItemArray.CopyTo(obj, 0);
                newDataTable.Rows.Add(obj);
            }
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                for (int j = 0; j < dt2.Columns.Count; j++)
                {
                    newDataTable.Rows[i][j + dt1.Columns.Count] = dt2.Rows[i][j].ToString();
                }
            }
            if (MyObject.FrmName2 == "C_M1")
            {
                SysData.cm1_dt = newDataTable.Clone();
                SysData.cm1_dt.Clear();
                for (int i = 0; i < newDataTable.Rows.Count; i++)
                {
                    SysData.cm1_dt.Rows.Add(newDataTable.Rows[i].ItemArray);
                }
            }
            else
            {
                SysData.cm2_dt = newDataTable.Clone();
                SysData.cm2_dt.Clear();
                for (int i = 0; i < newDataTable.Rows.Count; i++)
                {
                    SysData.cm2_dt.Rows.Add(newDataTable.Rows[i].ItemArray);
                }
            }
                return newDataTable;
        }
        #endregion

        private void button_Draw_Click(object sender, EventArgs e)
        {
          
            this.Close();
        }



        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void 读取数据_Load(object sender, EventArgs e)
        {
           
        }

        private void DataPath_TextChanged(object sender, EventArgs e)
        {
            //if (DataPath.Text == string.Empty)
            //{
            //    button_Bind.Enabled = false;
            //}
            //else
            //{
            //    button_Bind.Enabled = true;
            //}
        }






        //////////粘贴

       
         private void tabControl_Data_MouseUp(object sender, MouseEventArgs e)
         {
             if ((e.Y < this.tabControl_Data.GetTabRect(0).Bottom) && (e.Button == MouseButtons.Right))
             {
                 contextMenuStrip1.Show(this.tabControl_Data, e.X, e.Y);
             }
         }

         private void 粘贴ToolStripMenuItem_Click_1(object sender, EventArgs e)
         {
             columnNameList.Clear();
             dataTables.Columns.Clear();


             sheetNameList.Add("sheet1");
             DataSet ds = new DataSet();
             try
             {
                 // 获取剪切板的内容，并按行分割  
                 string pasteText = "";
                 pasteText = Clipboard.GetText();

                 if (string.IsNullOrEmpty(pasteText))
                     return;
                 if (pasteText == "pasteText")
                 {
                     return;
                 }
                 int tnum = 0;
                 int nnum = 0;
                 //获得当前剪贴板内容的行、列数
                 for (int i = 0; i < pasteText.Length; i++)
                 {
                     if (pasteText.Substring(i, 1) == "\t")
                     {
                         tnum++;
                     }
                     if (pasteText.Substring(i, 1) == "\n")
                     {
                         nnum++;
                     }
                 }
                 string[,] data;
                 //粘贴板上的数据来自于EXCEL时，每行末都有\n，在DATAGRIDVIEW内复制时，最后一行末没有\n
                 if (pasteText.Substring(pasteText.Length - 1, 1) == "\n")
                 {
                     nnum = nnum - 1;
                 }
                 tnum = tnum / (nnum + 1);
                 data = new string[nnum + 1, tnum + 1];//定义一个二维数组

                 String rowstr;
                 rowstr = "";
                 //MessageBox.Show(pasteText.IndexOf("B").ToString());
                 //对数组赋值
                 for (int i = 0; i < (nnum + 1); i++)
                 {
                     for (int colIndex = 0; colIndex < (tnum + 1); colIndex++)
                     {
                         //一行中的最后一列
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
                             rowstr = pasteText.Substring(0, pasteText.IndexOf("\t"));
                             pasteText = pasteText.Substring(pasteText.IndexOf("\t") + 1);
                         }
                         data[i, colIndex] = rowstr;
                     }
                     //截取下一行数据
                     pasteText = pasteText.Substring(pasteText.IndexOf("\n") + 1);
                 }
                 DataTable dt = new DataTable();
                 for (int n = 0; n < data.GetLength(0); n++)
                 {
                     DataRow dr = dt.NewRow();
                     for (int m = 0; m < data.GetLength(1); m++)
                     {
                         if (n == 0)
                         {
                             dt.Columns.Add(data[n, m]);
                             columnNameList.Add(data[n, m]);
                         }
                         else
                             dr[m] = data[n, m];
                     }
                     if (n != 0)
                         dt.Rows.Add(dr);
                 }

                 ds.Tables.Add(dt);
                 DataGridViewMaker(ds);
                 #region //列名绑定到Combox上
                 foreach (string str in columnNameList)
                 {

                     comboBox_Value1.Items.Add(str);
                     comboBox_Value2.Items.Add(str);
                 }
                 #endregion
                 if (ds.Tables.Count > 1)
                 {
                     for (int i = 1; i < ds.Tables.Count; i++)
                     {
                         string strT = "dataTables";
                         dataTables = UniteDataTable2(ds.Tables[0], ds.Tables[i], strT);
                     }
                 }
                 else
                 {
                     dataTables = ds.Tables[0];
                     ds.Tables.Clear();
                 }
             }
             catch
             {
                 Clipboard.Clear();
                 MessageBox.Show("错误！");
                 return;
             }
         }



    }
}
