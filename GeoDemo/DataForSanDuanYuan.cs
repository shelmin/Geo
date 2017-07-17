/* 
 * 作者;肖宇博
 * 日期：2014/7/13
 * 功能：这里本打算实现三端元投点功能，然而公司这边没有给出具体投点的相关算法，所以这个功能暂时没有实现
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using NPOI.SS.UserModel;
using System.Windows.Forms.DataVisualization.Charting;

namespace GeoDemo
{
    public partial class DataForSanDuanYuan : Form
    {
        public DataForSanDuanYuan()
        {
            InitializeComponent();

        }

        
        public static int aVal;
        public static int bVal;
        public static int cVal;
        public static int[] a = new int[100];
        public static int[] b = new int[100];
        public static int[] c = new int[100];
        
        public int k = 0;
        public static int sum=0;
        List<string> columnNameList = new List<string>();
        private void btnShow_Click(object sender, EventArgs e)
        {
            //在这里检查数据的合法性
            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                //Check(Convert.ToInt32(this.dataGridView1.Rows[i].Cells[0].Value),Convert.ToInt32( this.dataGridView1.Rows[i].Cells[1].Value),Convert.ToInt32( this.dataGridView1.Rows[i].Cells[2].Value));
                aVal = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[0].Value);
                bVal = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[1].Value);
                cVal = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[2].Value);
                a[i] = aVal;
                b[i] = bVal;
                c[i] = cVal;
                

                if (Check(aVal, bVal, cVal))
                {
                    sum++;
                }
                else
                {
                    MessageBox.Show("数据不合法");
                }

            }
            MyObject.A1 = a;
            MyObject.B1 = b;
            MyObject.C1 = c;
            MyObject.Sum1 = sum;
            //Check(Convert.ToInt32(this.dataGridView1.Rows[0].Cells[0].Value),Convert.ToInt32( this.dataGridView1.Rows[0].Cells[1].Value),Convert.ToInt32( this.dataGridView1.Rows[0].Cells[2].Value));
            //aVal =Convert.ToInt32(this.dataGridView1.Rows[0].Cells[0].Value);
            //bVal = Convert.ToInt32(this.dataGridView1.Rows[0].Cells[1].Value);
            //cVal = Convert.ToInt32(this.dataGridView1.Rows[0].Cells[2].Value);


            this.Close();
        }
        
        public bool Check(int a, int b, int c)
        {
            if (a < 0 || b < 0 || c < 0)
                return false;
            if (a + b + c != 100)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private void btnRead_Click(object sender, EventArgs e)
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
                //sheetNameList.Clear();
                //comboBox_Value1.Items.Clear();
                //comboBox_Value2.Items.Clear();
                //columnNameList.Clear();
                //dataTables.Columns.Clear();

                //DataPath.Text = openFileDialog.FileName;
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
            try
            {
                //dataTables = dS.Tables[0];

                for (int j = 0; j < dS.Tables[0].Columns.Count; j++)
                {
                    dataGridView1.Columns[j].HeaderText = columnNameList[j];
                }
                dS.Tables.Clear();
            }
            catch
            {
                return;
            }
        }

            #endregion
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
                        //sheetNameList.Add(wb.GetSheetName(i));         //保存表名
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
                                    dt.Columns.Add(iRow.GetCell(k).ToString());
                                    //columnNameList.Add(iRow.GetCell(k).ToString());                   //获取列名
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
                //sheetNameList.Add("sheet1");

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
                //this.button_Bind.Enabled = false;
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
                //for (int j = 0; j < col; j++)
                //{
                //    dataGridView1.Columns[j].HeaderText = WordsArray[0, j];
                //}
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
        //DataGridView dgvData = new DataGridView();
        private void DataGridViewMaker(DataSet tdS)
        {


            //绘制表格前，将tab控件中内容清空
            //tabControl_Data.TabPages.Clear();
            //绘制
            for (int i = 0; i < tdS.Tables.Count; i++)
            {


                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.DataSource = tdS.Tables[i];
                //dgvData.Width = tabControl_Data.Width - 10;
                //dgvData.Height = tabControl_Data.Height - 20;
                dataGridView1.ScrollBars = ScrollBars.Both;
                //TabPage tabPage = new TabPage();
                //tabPage.Text = sheetNameList[i];
                //tabPage.Controls.Add(dgvData);
                //tabControl_Data.TabPages.Add(tabPage);

            }


            //给DataGridView列名赋值

        }


        private void DataForSanDuanYuan_Load(object sender, EventArgs e)
        {
            // 设置用户不能手动给 DataGridView1 添加新行
            dataGridView1.AllowUserToAddRows = false;

        }
        //DataTable dataTables = new DataTable();

        //private void btnRead_Click(object sender, EventArgs e)
        //{
        //    //this.dataGridView1.Rows.Add();
        //}

        //#endregion 

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            // 删除前的用户确认。  
            if (MessageBox.Show("确认要删除该行数据吗？", "删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {

                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    if (!r.IsNewRow)
                    {
                        dataGridView1.Rows.Remove(r);
                    }
                }
                // 如果不是 OK，则取消。  
                e.Cancel = true;
            }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            //删除行
            // 删除前的用户确认。  
            if (MessageBox.Show("确认要删除该行数据吗？", "删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    if (!r.IsNewRow)
                    {
                        dataGridView1.Rows.Remove(r);
                    }
                }

            }
        }

        private void DataForSanDuanYuan_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MyObject.My_Datagridview1.DataSource = this.dataGridView1;
            //MyObject.A1 =a;
            //MyObject.B1 = b;
            //MyObject.C1 = c;
            //MyObject.Sum1 = sum;
            MainFrame frm = new MainFrame();





        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
      