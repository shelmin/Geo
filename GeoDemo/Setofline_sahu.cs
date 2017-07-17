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
    public delegate void paint5();
    public partial class Setofline_sahu : Form
    {
        SadhuCauses form1 = new SadhuCauses();
        public event paint5 paint_refresh;
        public Setofline_sahu(SadhuCauses shform)
        {
            form1 = shform;
            InitializeComponent();
        }
        //边框
        private void grid_SelectedIndexChanged(object sender, EventArgs e)
        {
           // SysData.line2 = this.grid.SelectedIndex;
        }
        //网格
        private void tick_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SysData.line1 = this.tick.SelectedIndex;
        }
        //颜色
        private void colorPickerButton3_SelectedColorChanged(object sender, EventArgs e)
        {
           // SysData.line_color = this.colorPickerButton3.SelectedColor;
        }
        //默认值
        private void btnDefaultSet2_Click(object sender, EventArgs e)
        {
            //存放setofline里的设置线性
            SysData.line1 = 1;
            SysData.line2 = 3;
            SysData.line_color = Color.Black;
            SysData.title = "双击图形在属性中修改图题";
            SysData.title_color = Color.Black;
            SysData.title_font = new Font("宋体", 12, FontStyle.Regular);
            paint_refresh();

        }

        private void btnOK2_Click(object sender, EventArgs e)
        {
            form1.mytext = this.title.Text;
            paint_refresh();
            this.Close();
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            ColorDialog diag = new ColorDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                form1.mycolor = diag.Color;
            }
        }

        private void btnChangeFont_Click(object sender, EventArgs e)
        {
            FontDialog diag = new FontDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                form1.myfont = diag.Font;
            }
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
     
    }
}
