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
    public partial class SadhuCauses : Form
    {
        public Font MyFont = SysData.title_font;
        public Color MyColor = SysData.title_color;
        public string MyText = "萨胡成因判别函数";
        public SadhuCauses()
        {
            InitializeComponent();
            
        }
        
        public void selfrefresh()
        {
            this.Refresh();
            label1.Text = MyText;
            if (SysData.IsDouble)
            {
                label1.Font = MyFont;
                label1.ForeColor = MyColor;

            }
            else
            {
                label1.Font = SysData.title_font;
                label1.ForeColor = SysData.title_color;
            }

        }
        private void 萨胡成因判别函数_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            MyObject.FrmName2 = "萨胡成因判别函数";
            label1.Text = MyText;
            if (SysData.IsDouble)
            {
                label1.Font = MyFont;
                label1.ForeColor = MyColor;

            }
            else
            {
                label1.Font = SysData.title_font;
                label1.ForeColor = SysData.title_color;
            }
            Bitmap bit = new Bitmap(".\\萨胡成因函数.png");
            Graphics g = Graphics.FromImage(bit);
            //在图片上写字（判别那一列）
            g.DrawString("风沙和海滩", new Font("宋体", 8, FontStyle.Regular), new SolidBrush(Color.Black), 720, 200);
            //this.pictureBox1.Image = bit;
            this.pictureBox1.BackgroundImage = bit;
        }
       

        #region 窗体传值


        //label文字
        public string mytext
        {
            get
            {
                return MyText;
            }
            set
            {
                MyText = value;
                //this.label1.Text = MyText;
            }
        }
        //label字样
        public Font myfont
        {
            get
            {
                return MyFont;
            }
            set
            {
                MyFont = value;
                //this.label1.Font = MyFont;
            }
        }
        //label颜色
        public Color mycolor
        {
            get
            {
                return MyColor;
            }
            set
            {
                MyColor = value;
                //this.label1.ForeColor = MyColor;
            }
        }
      

        #endregion

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Setofline_sahu setline = new Setofline_sahu(this);
            setline.paint_refresh += selfrefresh;
            setline.ShowDialog();
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            SysData.IsDouble = true;
            FontDialog diag = new FontDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                this.label1.Font = diag.Font;
                MyFont = diag.Font;
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //复制窗体,把窗体内容序列化
            FormState fs = new FormState(MyText, MyColor, MyFont);
            XmlHelper.SaveForm(fs);
            MyObject.FrmName1 = "萨胡成因判别函数";//如果萨胡成因的窗体被点击了，那么就把名字给当前操作窗体的名字
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MyObject.FrmName1 = null;
            this.Close();
        }

        private void SadhuCauses_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
            MyObject.FrmName2 = "萨胡成因判别函数";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.BringToFront();
            MyObject.FrmName2 = "萨胡成因判别函数";
        }

        private void SadhuCauses_MouseUp(object sender, MouseEventArgs e)
        {
            MainFrame mf = new MainFrame();
            mf.getMsg();
        }
    }
}
