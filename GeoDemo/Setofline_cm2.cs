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
    public delegate void paint2();
    public partial class Setofline_cm2 : Form
    {
        C_M2 form1 = new C_M2();
        public event paint2 paint_refresh;
        public Setofline_cm2(C_M2 cm1form)
        {
            form1 = cm1form;
            InitializeComponent();
            #region 记忆功能

            comboBox1.SelectedIndex = SysData.comselectitem;
            switch (SysData.comselectitem)
            {
                case 0:
                    {
                        this.comboBox2.Text = form1.myfont.FontFamily.Name;
                        this.colorPickerButton2.SelectedColor = form1.mycolor;
                        break;

                    }
                case 1:
                    {
                        this.comboBox2.Text = form1.myxfont.FontFamily.Name;
                        this.colorPickerButton2.SelectedColor = form1.myxbrush;
                        break;
                    }
                case 2:
                    {
                        this.comboBox2.Text = form1.myxnamefont.FontFamily.Name;
                        this.colorPickerButton2.SelectedColor = form1.myxnamecolor;
                        break;
                    }
                case 3:
                    {
                        this.comboBox2.Text = form1.myyfont.FontFamily.Name;
                        this.colorPickerButton2.SelectedColor = form1.myybrush;
                        break;
                    }
                case 4:
                    {
                        this.comboBox2.SelectedText = form1.myynamefont.FontFamily.Name;
                        this.colorPickerButton2.SelectedColor = form1.myynamecolor;
                        break;
                    }
            }


            #region   晁 修改
            #region  笔刷风格
            this.grid.SelectedIndex = form1.myline2;//边框粗细
            this.colorPickerButton1.SelectedColor = form1.mylinecolor2;//边框颜色
            this.colorPickerButton3.SelectedColor = form1.mylinecolor1;//网格颜色
            this.tick.SelectedIndex = form1.myline1;//网格粗细
            #endregion  笔刷风格
            #endregion  晁  修改


            #endregion
        }
        
       
        
       
        //网格颜色
       
        //默认值
        private void btnDefaultSet2_Click(object sender, EventArgs e)
        {
            form1.myxfont = new Font("宋体", 12, FontStyle.Regular);
            form1.myyfont = new Font("宋体", 12, FontStyle.Regular);
            form1.myxbrush = Color.Black;
            form1.myybrush = Color.Black;
            form1.myxcheck = false;
            form1.myycheck = false;
            form1.myxnametext = "X轴";
            form1.myxnamefont = new Font("宋体", 12, FontStyle.Regular);
            form1.myxnamecolor = Color.Black;
            form1.myynametext = "Y轴";
            form1.myynamefont = new Font("宋体", 12, FontStyle.Regular);
            form1.myynamecolor = Color.Black;
            SysData.title = "双击图形在属性中修改图题";
            SysData.title_color = Color.Black;
            SysData.title_font = new Font("宋体", 12, FontStyle.Regular);
            paint_refresh();

        }

        private void btnOK2_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "标题":
                    form1.mytext = title.Text;
                    break;
                case "X轴名":
                    form1.myxnametext = title.Text;
                    form1.myxcheck = checkBox1.Checked;
                    break;
                case "Y轴名":
                    form1.myynametext = title.Text;
                    form1.myycheck = checkBox1.Checked;
                    break;
            }
            //0121 应用时根据记忆的属性修改
            if (comboBox1.SelectedItem.ToString() == "X轴刻度值")
            {
                form1.myxbrush = this.colorPickerButton2.SelectedColor;
                form1.myxfont = SysData.comboboxFont;
            }
            else if (comboBox1.SelectedItem.ToString() == "Y轴刻度值")
            {
                form1.myybrush = this.colorPickerButton2.SelectedColor;
                form1.myyfont = SysData.comboboxFont;
            }
            else if (comboBox1.SelectedItem.ToString() == "标题")
            {
                form1.mycolor = SysData.title_color = this.colorPickerButton2.SelectedColor;
                form1.myfont = SysData.title_font = SysData.comboboxFont;
            }
            else if (comboBox1.SelectedItem.ToString() == "X轴名")
            {
                form1.MyXnameColor = this.colorPickerButton2.SelectedColor;
                form1.myxnamefont = SysData.comboboxFont;
            }
            else if (comboBox1.SelectedItem.ToString() == "Y轴名")
            {
                form1.MyYnameColor = this.colorPickerButton2.SelectedColor;
                form1.myynamefont = SysData.comboboxFont;
            }
            paint_refresh();
            this.Close();
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

       
        #region 打印

        //打印设置
        private void btnPageSet_Click(object sender, EventArgs e)
        {
            this.pageSetupDialog1.ShowDialog();
        }
        //打印预览
        private void btnPreView_Click(object sender, EventArgs e)
        {

            this.printPreviewDialog1.ShowDialog();
        }
        //打印内容
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(SysData.PrintBit, 0, 0, SysData.PrintBit.Width, SysData.PrintBit.Height);
        }

        //打印
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (ShowPrintDiag.Checked)
            {
                if (this.printDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.printDocument1.Print();
                }
            }
            else
                this.printDocument1.Print();

        }
        #endregion

        

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ///////////文本风格
        private void comboBox2_Click(object sender, EventArgs e)
        {
            FontDialog diag = new FontDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (comboBox1.SelectedItem.ToString() == "X轴刻度值")
                {
                    form1.myxfont = diag.Font;
                }
                else if (comboBox1.SelectedItem.ToString() == "Y轴刻度值")
                {
                    form1.myyfont = diag.Font;
                }
                else if (comboBox1.SelectedItem.ToString() == "标题")
                {
                    form1.myfont =SysData.title_font= diag.Font;
                }
                else if (comboBox1.SelectedItem.ToString() == "X轴名")
                {
                    form1.myxnamefont = diag.Font;
                }
                else if (comboBox1.SelectedItem.ToString() == "Y轴名")
                {
                    form1.myynamefont = diag.Font;
                }
                else
                    MessageBox.Show("请选择种类", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            SysData.comboboxFont = diag.Font;
            this.comboBox2.Text = diag.Font.Name;//显示字体到combobox2
        }
       
        //颜色
        private void colorPickerButton2_SelectedColorChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "X轴刻度值")
            {
                form1.myxbrush = this.colorPickerButton2.SelectedColor;
            }
            else if (comboBox1.SelectedItem.ToString() == "Y轴刻度值")
            {
                form1.myybrush = this.colorPickerButton2.SelectedColor;
            }
            else if (comboBox1.SelectedItem.ToString() == "标题")
            {
                form1.mycolor = SysData.title_color = this.colorPickerButton2.SelectedColor;
                
            }
            else if (comboBox1.SelectedItem.ToString() == "X轴名")
            {
                form1.MyXnameColor = this.colorPickerButton2.SelectedColor;
            }
            else if (comboBox1.SelectedItem.ToString() == "Y轴名")
            {
                form1.MyYnameColor = this.colorPickerButton2.SelectedColor;
            }
            else
                MessageBox.Show("请选择种类", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        //应用
        private void buttonX3_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "标题":
                    form1.mytext = title.Text;
                    break;
                case "X轴名":
                    form1.myxnametext = title.Text;
                    form1.myxcheck = checkBox1.Checked;
                    break;
                case "Y轴名":
                    form1.myynametext = title.Text;
                    form1.myycheck = checkBox1.Checked;
                    break;
            }
            //0121 应用时根据记忆的属性修改
            if (comboBox1.SelectedItem.ToString() == "X轴刻度值")
            {
                form1.myxbrush = this.colorPickerButton2.SelectedColor;
                form1.myxfont = SysData.comboboxFont;
            }
            else if (comboBox1.SelectedItem.ToString() == "Y轴刻度值")
            {
                form1.myybrush = this.colorPickerButton2.SelectedColor;
                form1.myyfont = SysData.comboboxFont;
            }
            else if (comboBox1.SelectedItem.ToString() == "标题")
            {
                form1.mycolor = SysData.title_color = this.colorPickerButton2.SelectedColor;
                form1.myfont = SysData.title_font = SysData.comboboxFont;
            }
            else if (comboBox1.SelectedItem.ToString() == "X轴名")
            {
                form1.MyXnameColor = this.colorPickerButton2.SelectedColor;
                form1.myxnamefont = SysData.comboboxFont;
            }
            else if (comboBox1.SelectedItem.ToString() == "Y轴名")
            {
                form1.MyYnameColor = this.colorPickerButton2.SelectedColor;
                form1.myynamefont = SysData.comboboxFont;
            }
            //       
            paint_refresh();
        }
        /////////////笔刷风格
        //边框
        private void grid_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //SysData.line2 = this.grid.SelectedIndex;
            form1.myline2 = this.grid.SelectedIndex;
        }

        private void colorPickerButton1_SelectedColorChanged_1(object sender, EventArgs e)
        {
            form1.mylinecolor2 = this.colorPickerButton1.SelectedColor;
        }
        //网格
        private void tick_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            form1.myline1 = this.tick.SelectedIndex;
        }
        //网格颜色
        private void colorPickerButton3_SelectedColorChanged_1(object sender, EventArgs e)
        {
            form1.mylinecolor1 = this.colorPickerButton3.SelectedColor;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            paint_refresh();
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            paint_refresh();
            this.Close();
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            this.grid.SelectedIndex = form1.myline2 = 3;
            this.tick.SelectedIndex = form1.myline1 = 0;
            this.colorPickerButton3.SelectedColor = form1.mylinecolor1 = Color.Gray;
            this.colorPickerButton1.SelectedColor = form1.mylinecolor2 = Color.Black;
            paint_refresh();
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SysData.comselectitem = comboBox1.SelectedIndex;//记忆选择的内容
            if (comboBox1.SelectedItem.ToString() == "标题" || comboBox1.SelectedItem.ToString() == "X轴刻度值" || comboBox1.SelectedItem.ToString() == "Y轴刻度值")
            {
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
            }
            else
            {
                checkBox1.Enabled = true;
            }
            title.Text = comboBox1.SelectedItem.ToString();
        }

       
    }
}
