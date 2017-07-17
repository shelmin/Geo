/* 
 * 作者;肖宇博
 * 日期：2014/7/18
 * 功能：该类主要实现读取数据的功能，将从datatable中的数据赋值给数组，供chart图绑定用
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace GeoDemo
{
    class ReadDataFromDatatable
    {
        private int countx = 0;

        public void readDataService(ref string[] x, ref double[] y)//供只有一个序列的图使用
        {
            #region 从datatable中读入数据
            //选择读去哪2列？就是之前在Combox中选择的那两列
            string cx = ReadData.AxisX_SelectIndex;
            string cy = ReadData.AxisY_SelectIndex;

            try
            {
                DataSet dataset = MyObject.Dataset;
                countx = dataset.Tables[0].Rows.Count;

                int colnum = dataset.Tables[0].Columns.Count;
                for (int i = 0; i < countx; i++)
                {
                    y[i] = Convert.ToDouble(dataset.Tables[0].Rows[i][cy].ToString());
                }

                for (int j = 0; j < countx; j++)
                {
                    x[j] = dataset.Tables[0].Rows[j][cx].ToString();
                }
            }
            catch
            {
                MessageBox.Show("您读取表的方式有误，请重新核对选择！");
                //把x,j都置为0
                for (int i = 0; i < countx; i++)
                {
                    y[i] = 0;


                    x[i] = "";
                }
            }

            #endregion
        }
        public void readDataService(ref double[] x, ref double[] y)//供只有一个序列的图使用
        {
            #region 从datatable中读入数据
            //选择读去哪2列？就是之前在Combox中选择的那两列
            string cx = ReadData.AxisX_SelectIndex;
            string cy = ReadData.AxisY_SelectIndex;

            try
            {
                DataSet dataset = MyObject.Dataset;
                countx = dataset.Tables[0].Rows.Count;

                int colnum = dataset.Tables[0].Columns.Count;
                for (int i = 0; i < countx; i++)
                {
                    y[i] = Convert.ToDouble(dataset.Tables[0].Rows[i][cy].ToString());
                }

                for (int j = 0; j < countx; j++)
                {
                    x[j] = Convert.ToDouble(dataset.Tables[0].Rows[j][cx].ToString());
                }
            }
            catch
            {
                MessageBox.Show("您读取表的方式有误，请重新核对选择！");
                //把x,j都置为0
                for (int i = 0; i < countx; i++)
                {
                    y[i] = 0;


                    x[i] = 0;
                }
            }

            #endregion
        }



        public void readDataService(ref string[] x, ref double[] y, ref string[] xx, ref double[] yy)//供只有两个序列的图使用
        {

            #region 从datatable中读入数据
            //选择读去哪2列？就是之前在Combox中选择的那两列
            string cx = ReadData2.AxisX_SelectIndex;
            string cy1 = ReadData2.AxisY1_SelectIndex;
            string cy2 = ReadData2.AxisY2_SelectIndex;
            try
            {
                DataSet dataset = MyObject.Dataset;
                countx = dataset.Tables[0].Rows.Count;

                int colnum = dataset.Tables[0].Columns.Count;//date value1 value2

                for (int i = 0; i < countx; i++)
                {
                    y[i] = Convert.ToDouble(dataset.Tables[0].Rows[i][cy1].ToString());
                }
                for (int i = 0; i < countx; i++)
                {
                    yy[i] = Convert.ToDouble(dataset.Tables[0].Rows[i][cy2].ToString());
                }

                for (int j = 0; j < countx; j++)
                {
                    x[j] = dataset.Tables[0].Rows[j][cx].ToString();
                }
            }
            catch
            {
                MessageBox.Show("您读取表的方式有误，请重新核对选择！");

                for (int i = 0; i < countx; i++)
                {
                    y[i] = 0;
                    yy[i] = 0;

                    x[i] = "";
                }

            }

            #endregion

        }


        public void readDataService(ref double[] x, ref double[] y, ref double[] xx, ref double[] yy)//专为相渗曲线
        {
            //因为相渗曲线横纵坐标皆为double类型
            #region 从datatable中读入数据
            //选择读去哪2列？就是之前在Combox中选择的那两列
            string cx = ReadData2.AxisX_SelectIndex;
            string cy1 = ReadData2.AxisY1_SelectIndex;
            string cy2 = ReadData2.AxisY2_SelectIndex;
            try
            {
                DataSet dataset = MyObject.Dataset;
                countx = dataset.Tables[0].Rows.Count;

                int colnum = dataset.Tables[0].Columns.Count;//date value1 value2

                for (int i = 0; i < countx; i++)
                {
                    y[i] = Convert.ToDouble(dataset.Tables[0].Rows[i][cy1].ToString());
                }
                for (int i = 0; i < countx; i++)
                {
                    yy[i] = Convert.ToDouble(dataset.Tables[0].Rows[i][cy2].ToString());
                }

                for (int j = 0; j < countx; j++)
                {
                    x[j] = Convert.ToDouble(dataset.Tables[0].Rows[j][cx].ToString());
                }
                
            }
            catch
            {
                MessageBox.Show("数据读取有误，请仔细检查数据！");
            }
            #endregion

        }

        public void readDataService(ref string[] x, ref double[] y, ref string[] xx, ref double[] yy, ref string[] xxx, ref double[] yyy)
        {
            //专为生产开发曲线设计，因为他要读入三个以上的序列
            #region 从datatable中读入数据
            //选择读去哪2列？就是之前在Combox中选择的那两列
            string cx = ReadData3.AxisX_SelectIndex;
            string cy1 = ReadData3.AxisY1_SelectIndex;
            string cy2 = ReadData3.AxisY2_SelectIndex;
            string cy3 = ReadData3.AxisY3_SelectIndex;
            try
            {
                DataSet dataset = MyObject.Dataset;
                countx = dataset.Tables[0].Rows.Count;

                int colnum = dataset.Tables[0].Columns.Count;//date value1 value2

                for (int i = 0; i < countx; i++)
                {
                    y[i] = Convert.ToDouble(dataset.Tables[0].Rows[i][cy1].ToString());
                }
                for (int i = 0; i < countx; i++)
                {
                    yy[i] = Convert.ToDouble(dataset.Tables[0].Rows[i][cy2].ToString());
                }
                for (int i = 0; i < countx; i++)
                {
                    yyy[i] = Convert.ToDouble(dataset.Tables[0].Rows[i][cy3].ToString());
                }

                for (int j = 0; j < countx; j++)
                {
                    x[j] = dataset.Tables[0].Rows[j][cx].ToString();
                }
            }
            catch
            {
                MessageBox.Show("您读取表的方式有误，请重新核对选择！");
                //Application.Restart();
                for (int i = 0; i < countx; i++)
                {
                    y[i] = 0;
                    yy[i] = 0;
                    yyy[i] = 0;
                    x[i] = "";
                }

            }

            #endregion

        }

        public void readDataService(ref string[] x, ref double[] y, ref string[] xx, ref double[] yy, ref string[] xxx, ref double[] yyy, ref string[] xxxx, ref double[] yyyy)
        {
            //专为水井注水曲线设计，因为他要读入四个以上的序列
            #region 从datatable中读入数据
            //选择读去哪2列？就是之前在Combox中选择的那两列
            string cx = ReadData4.AxisX_SelectIndex;
            string cy1 = ReadData4.AxisY1_SelectIndex;
            string cy2 = ReadData4.AxisY2_SelectIndex;
            string cy3 = ReadData4.AxisY3_SelectIndex;
            string cy4 = ReadData4.AxisY4_SelectIndex;
            try
            {
                DataSet dataset = MyObject.Dataset;
                countx = dataset.Tables[0].Rows.Count;

                int colnum = dataset.Tables[0].Columns.Count;//date value1 value2

                for (int i = 0; i < countx; i++)
                {
                    y[i] = Convert.ToDouble(dataset.Tables[0].Rows[i][cy1].ToString());
                }
                for (int i = 0; i < countx; i++)
                {
                    yy[i] = Convert.ToDouble(dataset.Tables[0].Rows[i][cy2].ToString());
                }
                for (int i = 0; i < countx; i++)
                {
                    yyy[i] = Convert.ToDouble(dataset.Tables[0].Rows[i][cy3].ToString());
                }
                for (int i = 0; i < countx; i++)
                {
                    yyyy[i] = Convert.ToDouble(dataset.Tables[0].Rows[i][cy4].ToString());
                }

                for (int j = 0; j < countx; j++)
                {
                    x[j] = dataset.Tables[0].Rows[j][cx].ToString();
                }
            }
            catch
            {
                MessageBox.Show("您读取表的方式有误，请重新核对选择！");
                //Application.Restart();
                for (int i = 0; i < countx; i++)
                {
                    y[i] = 0;
                    yy[i] = 0;
                    yyy[i] = 0;
                    yyyy[i] = 0;
                    x[i] = "";
                }

            }

            #endregion
        }

        public int getCounx()
        {
            DataSet dataset = MyObject.Dataset;
            countx = dataset.Tables[0].Rows.Count;
            return countx;
        }   //得到该datatable有多少行


        public void readDataService(ref DateTime[] x, ref double[] y)//供递减曲线使用
        {
            #region 从datatable中读入数据
            //选择读去哪2列？就是之前在Combox中选择的那两列
            string cx = ReadData.AxisX_SelectIndex;
            string cy = ReadData.AxisY_SelectIndex;

            try
            {
                DataSet dataset = MyObject.Dataset;
                countx = dataset.Tables[0].Rows.Count;

                int colnum = dataset.Tables[0].Columns.Count;
                for (int i = 0; i < countx; i++)
                {
                    y[i] = Convert.ToDouble(dataset.Tables[0].Rows[i][cy].ToString());
                }

                for (int j = 0; j < countx; j++)
                {
                    x[j] = Convert.ToDateTime(dataset.Tables[0].Rows[j][cx].ToString());
                }
            }
            catch
            {
                MessageBox.Show("您读取表的方式有误，请重新核对选择！");
                //for (int i = 0; i < countx; i++)
                //{
                //    y[i] = 0.0;
                //    x[i] = System.DateTime.Now.AddDays(i);
                //}
            }

            #endregion
        }

        public void readDataService(ref string []x,ref double [,]y,ref string []z) 
        {
            int h = MyObject.O;
            int t = 1;
            try
            {
                foreach (DataGridViewAutoFilterTextBoxColumn  col in ReadDataAll .dgvData .Columns)
                {
                    if (h != 0||t!=0)
                    {
                        switch (col.HeaderText)
                        {
                            case "水平轴":
                                for (int i = 0; i < MyObject.U; i++)
                                {
                                    x[i] = ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString();
                                    z[0] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                }
                                t--;
                                break;
                            case "序列1":
                                for (int i = 0; i < MyObject.U; i++)
                                {
                                    y[0, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                    z[1] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                }
                                h--;
                                break;
                            case "序列2":
                                if (MyObject .O<2)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[0, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[1] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[1, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[2] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列3":
                                if (MyObject .O <3)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O-1, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[2, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[3] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列4":
                                if (MyObject.O < 4)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[3, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[4] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列5":
                                if (MyObject.O < 5)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[4, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[5] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列6":
                                if (MyObject.O < 6)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[5, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[6] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列7":
                                if (MyObject.O < 7)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[6, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[7] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列8":
                                if (MyObject.O < 8)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[7, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[8] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch 
            {
            }
        }

        public void readDataService(ref double [] x, ref double[,] y, ref string[] z)
        {
            int h = MyObject.O;
            try
            {
                foreach (DataGridViewAutoFilterTextBoxColumn col in ReadDataAll.dgvData.Columns)
                {
                    if (h != 0)
                    {
                        switch (col.HeaderText)
                        {
                            case "水平轴":
                                for (int i = 0; i < MyObject.U; i++)
                                {
                                    x[i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                    z[0] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                }
                                break;
                            case "序列1":
                                for (int i = 0; i < MyObject.U; i++)
                                {
                                    y[0, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                    z[1] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                }
                                h--;
                                break;
                            case "序列2":
                                if (MyObject.O < 2)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[0, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[1] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[1, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[2] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列3":
                                if (MyObject.O < 3)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[2, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[3] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列4":
                                if (MyObject.O < 4)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[3, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[4] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列5":
                                if (MyObject.O < 5)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[4, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[5] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列6":
                                if (MyObject.O < 6)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[5, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[6] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列7":
                                if (MyObject.O < 7)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[6, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[7] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列8":
                                if (MyObject.O < 8)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[7, i] = Convert.ToDouble(ReadDataAll.dgvData.Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[8] = ReadDataAll.dgvData.Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }    
}
