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
    public partial class AddorDelSerise : Form
    {
        public int pos;
        public string r;
        public AddorDelSerise()
        {
            InitializeComponent();
        }
        public int RowIndex = 0;
      //  public string InitSeriesCount = (string)MyObject.My_Chart1.Name;
        private void AddorDelSerise_Load(object sender, EventArgs e)
        {
            //窗体加载出来有现在该图有几个序列
            RowIndex = MyObject.My_Chart1.Series.Count;
            listView1.Alignment = ListViewAlignment.Left;//左对齐
            listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度  
            for (int i = 0; i < RowIndex; i++)   //添加10行数据  
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = MyObject .My_Chart1 .Series [i].Name ;
                listView1.Items.Add(lvi);
            }

            listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。  
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)//将listview的内容传到TextBox里去
        {
            if (listView1 .SelectedItems .Count>0)
            {
            foreach (ListViewItem  lvi in listView1.SelectedItems)
            {
                pos = lvi.Index;
                    textBox1.Text = lvi.Text; 
                    r = lvi.Text; 
            }
            
            }
        }
        public static bool isBtnAddSeriseClick = false;
      
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //DialogResult dr = MessageBox.Show("您点击了添加一条线，那么请你请先读入一组数据", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.None);
            //if (dr == DialogResult.Yes)
            //{
                isBtnAddSeriseClick = true;
                ReadDataAll f = new ReadDataAll();
                f.ShowDialog();
            //}
            //this.Close();
        }


        public int getInitSeriesCount(string name)
        {
            if (name.Equals("chart3") || name.Equals("chart6") || name.Equals("chart10") || name.Equals("chart14") || name.Equals("chart17") || name.Equals("chart20") || name.Equals("chart29") || name.Equals("chart32"))
                return 2;
            else
                return 1;
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
          
           //点击完了删除listview要发生变化的！！

            if (getInitSeriesCount(MyObject.My_Chart1.Name) < MyObject.My_Chart1.Series.Count)
            {
                int length = listView1.SelectedItems.Count;
                for (int i = 0; i < length; i++)
                {
                    int j = (listView1.SelectedItems[i].Index + 1);
                  //  如果该图本来有一个序列，你把他删掉就不可以。。。如果原来有两个，最低只能删到两个序列不可以删成为1个
                    listView1.Items[j-1].Remove();
                    MyObject.My_Chart1.Series.RemoveAt(j - 1);
                }
                //this.Close();
            }
            else
             {
                 MessageBox.Show("该图不可再删除序列了！");
                 //this.btnDel.Enabled = false;
             }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
        public void check() //检测是否有重复命名的序列
        {
            try
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    MyObject.My_Chart1.Series[i].Name = listView1.Items[i].Text;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("不能输入与其它数据列一样的名字！");
                listView1.Items[pos].Text = r;
                listView1.Items[pos].Selected = true;
                textBox1.Text = r;
                return;
            }
        }

        private void AddorDelSerise_FormClosed(object sender, FormClosedEventArgs e)
        {
            check();
            
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)//修改序列名字，7月15
        {
            //if (e.KeyChar == 13 && textBox1.Text.Length > 0) 
            //{
            //    if ((pos > -1) && (listView1.SelectedItems.Count > 0))
            //    {
            //        listView1.Items[pos].Text = textBox1.Text;
            //    }
            //}
            //check();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter && textBox1.Text.Length > 0) 
            //{
            //    if ((pos > -1) && (listView1.SelectedItems.Count > 0))
            //    {
            //        listView1.Items[pos].Text = textBox1.Text;
            //    }
            //}
            //check();
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if ((pos > -1) && (listView1.SelectedItems.Count > 0))
            {
                listView1.Items[pos].Text = textBox1.Text;
            }
            check();
        }
    }
}
