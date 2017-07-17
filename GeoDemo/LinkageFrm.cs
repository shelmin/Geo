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
    public partial class LinkageFrm : Form
    {
        DataTable dataTables = new DataTable();
        public static int p0=0,p1=0,p2=0,p3=0;
        public LinkageFrm()
        {
            InitializeComponent();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button_Select_Click(object sender, EventArgs e)
        {

        }
        public void LinkageFrm_Load(object sender, EventArgs e)
        {
            //每一次初始的时候默认显示读取普通图
            //每一次初始的时候都将上次读取的数据清空
            MyObject.Dataset = null;
            if (MyObject.DT != null && MyObject.My_Chart1.Series[0].Points.Count != 0)
            {
                MyObject.Columns = MyObject.DT.Columns.Count;
                this.dataGridView1.Dock = DockStyle.Fill;
                this.dataGridView1.ScrollBars = ScrollBars.Both;
                this.dataGridView1.DataSource = MyObject.DT;
                this.tabPage1.Text = MyObject.My_Chart1.Name;
            }
            this.Left = Savelocation.value_x - this.Width;
            this.Top = Savelocation.value_y;
        }

       
        private void tabControl_Data_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Y < this.tabControl_Data.GetTabRect(0).Bottom) && (e.Button == MouseButtons.Right))
            {
                contextMenuStrip1.Show(this.tabControl_Data, e.X, e.Y);//让右键菜单显示出来
            }
        }

        public void readDataService2(ref string[] x, ref double[,] y, ref string[] z)
        {
            int h = MyObject.O;
            try
            {
                foreach (DataGridViewTextBoxColumn col in this.dataGridView1.Columns)
                {
                    if (h != 0)
                    {
                        switch (col.HeaderText) 
                        {
                            case "水平轴":
                                for (int i = 0; i < MyObject.U; i++)
                                {
                                    x[i] = this.dataGridView1.Rows[i + 1].Cells[col.Index].Value.ToString();
                                    z[0] = this.dataGridView1.Rows[0].Cells[col.Index].Value.ToString();
                                }
                                break;
                            case "序列1":
                                for (int i = 0; i < MyObject.U; i++)
                                {
                                    y[0, i] = Convert.ToDouble(this.dataGridView1 .Rows[i + 1].Cells[col.Index].Value.ToString());
                                    z[1] = this.dataGridView1 .Rows[0].Cells[col.Index].Value.ToString();
                                }
                                h--;
                                break;
                            case "序列2":
                                if (MyObject.O < 2)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[0, i] = Convert.ToDouble(this.dataGridView1 .Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[1] = this.dataGridView1 .Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[1, i] = Convert.ToDouble(this.dataGridView1 .Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[2] = this.dataGridView1 .Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列3":
                                if (MyObject.O < 3)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(this.dataGridView1 .Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = this.dataGridView1 .Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[2, i] = Convert.ToDouble(this.dataGridView1 .Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[3] = this.dataGridView1 .Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列4":
                                if (MyObject.O < 4)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(this.dataGridView1 .Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = this.dataGridView1 .Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[3, i] = Convert.ToDouble(this.dataGridView1 .Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[4] = this.dataGridView1 .Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列5":
                                if (MyObject.O < 5)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(this.dataGridView1 .Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = this.dataGridView1 .Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[4, i] = Convert.ToDouble(this.dataGridView1 .Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[5] = this.dataGridView1 .Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列6":
                                if (MyObject.O < 6)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(this.dataGridView1 .Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = this.dataGridView1 .Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[5, i] = Convert.ToDouble(this.dataGridView1 .Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[6] = this.dataGridView1 .Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列7":
                                if (MyObject.O < 7)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(this.dataGridView1 .Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = this.dataGridView1 .Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[6, i] = Convert.ToDouble(this.dataGridView1 .Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[7] = this.dataGridView1 .Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                h--;
                                break;
                            case "序列8":
                                if (MyObject.O < 8)
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[MyObject.O - 1, i] = Convert.ToDouble(this.dataGridView1 .Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[MyObject.O] = this.dataGridView1 .Rows[0].Cells[col.Index].Value.ToString();
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < MyObject.U; i++)
                                    {
                                        y[7, i] = Convert.ToDouble(this.dataGridView1 .Rows[i + 1].Cells[col.Index].Value.ToString());
                                        z[8] = this.dataGridView1 .Rows[0].Cells[col.Index].Value.ToString();
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

        private void Nice()
        {
            MyObject.O = 0;
            MyObject.U = MyObject .DT .Rows .Count - 1;
            foreach (DataGridViewColumn col in this.dataGridView1 .Columns)
            {
                switch (col.HeaderText)
                {
                    case "水平轴":
                        MyObject.O++;
                        break;
                    default:
                        break;
                }
            }
            MyObject.O = MyObject.Columns - MyObject.O;

            for (int g = 0; g < MyObject.My_Chart1.Series.Count; g++)
            {
                MyObject.My_Chart1.Series[g].Name = "Seris" + g + 1;
            }
            string[] x1 = new string[this.dataGridView1 .RowCount - 2];
            double[] x2 = new double[this.dataGridView1.RowCount - 2];

            if (MyObject.O != 0)
            {
                double[,] y = new double[MyObject.O, this.dataGridView1.RowCount - 2];
                string[] z = new string[MyObject.O + 1];
                readDataService2(ref x1, ref y, ref z);
                for (int i = 0; i < MyObject.O; i++)
                {
                    if (MyObject.My_Chart1.Series.Count > i)
                    {
                        double[] d = new double[this.dataGridView1.RowCount - 2];
                        for (int q = 0; q < this.dataGridView1.RowCount - 2; q++)
                        {
                            d[q] = y[i, q];
                        }
                        MyObject.My_Chart1.Series[i].Points.DataBindXY(x1, d);
                        MyObject.My_Chart1.Series[i].Name = z[i + 1];
                    }
                }
            }
        }
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1 .RowCount -1==MyObject .DT .Rows .Count&&this.tabPage1 .Text ==MyObject .My_Chart1 .Name)
                {
                    Nice();
                    MyObject.My_Chart1.Invalidate();
                    return;
                }
            }
            catch 
            {

            }
        }

        private void LinkageFrm_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
