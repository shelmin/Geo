using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plytmf.Net.Bottom;

namespace GeoDemo
{
    public partial class CurvesOfSelectWell : Form
    {
        public float[] Xcur;
        public float[] Ycur;
        public float[] Dpth;
        public static DataTable dtt = new DataTable();
        public static ListViewItem Lvi;
        public static DataTable DT = new DataTable();
        public static double pmin;
        public static double pmax;
        public static double k;

        public CurvesOfSelectWell()
        {
            InitializeComponent();
        }

        private void Init() //初始化一个空表
        {
            DataColumn dc1 = new DataColumn("0");
            DataColumn dc2 = new DataColumn("1");
            DataColumn dc3 = new DataColumn("2");
            DT.Columns.Add(dc1);
            DT.Columns.Add(dc2);
            DT.Columns.Add(dc3);
        }
        private void CurvesOfSelectWell_Load(object sender, EventArgs e)
        {
            if (DT.Columns.Count == 0)
            {
                Init();
            }
            this.listView1.Columns.Add("本井曲线集合", 120, HorizontalAlignment.Left);
            listView1.BeginUpdate();
            foreach (Curve curve in Well_DataBase.well.Curves)                     //添加曲线
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = curve;
                lvi.Text = curve.UniqueName;
                listView1.Items.Add(lvi);
            }
            listView1.EndUpdate();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            if (ReadDataFromDataBase.Xclick)                                       //判断传递给X轴还是Y轴
            {
                ReadDataFromDataBase.XcurveID.Text = listView1.SelectedItems[0].Text;
                Lvi = listView1.SelectedItems[0];
            }
            else if (ReadDataFromDataBase.Yclick)
            {
                if (dtt.Columns.Count != 0)
                {
                    for (int i = 0; i < dtt.Columns.Count; i++)
                    {

                        dtt.Columns.Remove(dtt.Columns[i]);
                    }
                }
                ReadDataFromDataBase.YcurveID.Text = listView1.SelectedItems[0].Text;
                //ReadDataFromDataBase.Yclick = false;
                Curve1D curve = listView1.SelectedItems[0].Tag as Curve1D;
                dtt = DT.Clone();
                Curve1D curve1 = Lvi.Tag as Curve1D;
                k = ((curve.Edep - curve.Sdep) / curve.Rlev);
                pmin = Convert.ToSingle(curve.Sdep);
                pmax = Convert.ToSingle(curve.Edep);
                for (int i = 0; i < k + 1; i++)                                 //将曲线数据存放在dtt里
                {
                    DataRow dr = dtt.NewRow();
                    //SysData.Depth[i] = curve.Sdep + i * curve.Rlev;
                    for (int j = 0; j < 3; j++)
                    {
                        if (j == 0)
                        {
                            dr[j] = curve.Sdep + i * curve.Rlev;
                        }
                        else if (j == 1)
                        {
                            dr[j] = curve1.GetValue(curve1.Sdep + i * curve1.Rlev);
                        }
                        else
                        {
                            dr[j] = curve.GetValue(curve.Sdep + i * curve.Rlev);
                        }
                    }
                    dtt.Rows.Add(dr);
                }

            }
            this.Close();
        }

        private void CurvesOfSelectWell_FormClosed(object sender, FormClosedEventArgs e)
        {
            ReadDataFromDataBase.Xclick = false;
            ReadDataFromDataBase.Yclick = false;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)//暂时并不能有效防止X轴Y轴选取同一条曲线
        {
            //    try
            //    {
            //        if (ReadDataFromDataBase.Xclick)                                       //判断传递给X轴还是Y轴
            //        {
            //            if (ReadDataFromDataBase.YcurveID.Text == listView1.SelectedItems[0].Text)
            //            {
            //                MessageBox.Show("请重新选一条与Y轴曲线不一样的曲线", "温馨提示");
            //                listView1.SelectedItems[0].Selected = false;
            //            }
            //            else
            //            {
            //                listView1.SelectedItems[0].Selected = true;
            //            }

            //        }
            //        else if (ReadDataFromDataBase.Yclick)
            //        {
            //            if (ReadDataFromDataBase.XcurveID.Text == listView1.SelectedItems[0].Text)
            //            {
            //                MessageBox.Show("请重新选一条与X轴曲线不一样的曲线", "温馨提示");
            //                listView1.SelectedItems[0].Selected = false;
            //            }
            //            else
            //            {
            //                listView1.SelectedItems[0].Selected = true;
            //            }

            //        }
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("出错了！");
            //    }

            //}
        }
    }
}
