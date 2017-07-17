/*
 *作者：肖宇博
 *时间：2014/6/19
 * 功能:设置饼图窗体的属性
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GeoDemo
{
    public partial class SetProperty2 : Form
    {
        public SetProperty2()
        {
            InitializeComponent();

            #region 绘制属性

            colorPickerButton2.SelectedColor = MyObject.My_Chart1.Series[0].BorderColor;//边框颜色
            BorderSizeCom.SelectedIndex = MyObject.My_Chart1.Series[0].BorderWidth;//边框大小
            BorderDashStyleCom.SelectedIndex = Convert.ToInt16(MyObject.My_Chart1.Series[0].BorderDashStyle);//边框样式
            ShadowOffset.SelectedIndex = MyObject.My_Chart1.Series[0].ShadowOffset;//阴影偏移量                                                            

            #region    是否显示数值
            if (MyObject.My_Chart1.Series[0].IsValueShownAsLabel)
            {
                this.comboBox1.SelectedIndex = 0;
            }
            else
            {
                this.comboBox1.SelectedIndex = 1;
            }
            #endregion


            #region    是否显示图例项
            if (MyObject.My_Chart1.Legends[0].Enabled)//
            {
                this.comboBox3.SelectedIndex = 0;
            }
            else
            {
                this.comboBox3.SelectedIndex = 1;
            }
            #endregion


            #region    图例项显示方式
            if (MyObject.My_Chart1.Legends[0].Docking == Docking.Top)
            {
                this.comboBox4.SelectedIndex = 0;
            }
            else if (MyObject.My_Chart1.Legends[0].Docking == Docking.Bottom)
            {
                this.comboBox4.SelectedIndex = 1;
            }
            else if (MyObject.My_Chart1.Legends[0].Docking == Docking.Left)
            {
                this.comboBox4.SelectedIndex = 2;
            }
            else
            {
                this.comboBox4.SelectedIndex = 3;
            }
            #endregion

            #region 旋转状态
            if (SaveAttribute.IsRotate == true)
            {
                checkBox1.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox1.CheckState = CheckState.Unchecked;
            }
            #endregion   

            #endregion


            #region 文本特色

            this.comboBox2.SelectedIndex = -1;//选择文本
            title.Text = "双击图形在属性中修改主标题";

            #endregion


            #region 笔刷风格

            colorPickerButton1.SelectedColor = MyObject.My_Chart1.Series[0].BackSecondaryColor;
            GradientCom.SelectedIndex = Convert.ToInt16(MyObject.My_Chart1.Series[0].BackGradientStyle);//颜色分布

            #region 填充方式
            switch (Convert.ToString(MyObject.My_Chart1.Series[0].BackHatchStyle))
            {
                case "None": HatchingCom.SelectedIndex = 0; break;
                case "Cross": HatchingCom.SelectedIndex = 1; break;
                case "Vertical": HatchingCom.SelectedIndex = 2; break;
                case "Horizontal": HatchingCom.SelectedIndex = 3; break;
                case "BackwardDiagonal": HatchingCom.SelectedIndex = 4; break;
                case "ForwardDiagonal": HatchingCom.SelectedIndex = 5; break;
                case "OutlinedDiamond": HatchingCom.SelectedIndex = 6; break;
                case "Weave": HatchingCom.SelectedIndex = 7; break;
            }
            #endregion

            #endregion

        }

        Dictionary<string, string> LineStyleDictionary = new Dictionary<string, string>();
        Dictionary<string, string> ColorDistributeDictionary = new Dictionary<string, string>();
        Dictionary<string, string> HatchDictionary = new Dictionary<string, string>();

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK2_Click(object sender, EventArgs e)
        {
            OK();//无论是图形属性窗口里面的确定按钮还是格线和文本属性里的确定按钮被点击
            this.Close();//全部设计了之后点击确定按钮并且退出设计窗体
        }
        
        private void buttonX4_Click(object sender, EventArgs e)
        {
            OK();//无论是图形属性窗口里面的确定按钮还是格线和文本属性里的确定按钮被点击
            this.Close();//全部设计了之后点击确定按钮并且退出设计窗体
        }

        public void OK()//无论是哪个确定按钮被点击了都调用同一个方法
        {

            #region 当图形风格确定按钮被点击的时候

            #region 设置底色
            MyObject.My_Chart1.Series[0].BackSecondaryColor = colorPickerButton1.SelectedColor;
            #endregion

            #region 设置BackGradientStyle
            if (GradientCom.SelectedItem != null)
            {
                MyObject.My_Chart1.Series[0].BackGradientStyle = (GradientStyle)GradientStyle.Parse(typeof(GradientStyle), this.ColorDistributeDictionary[GradientCom.SelectedItem.ToString()]);

            }
            #endregion

            #region 设置BackHatchStyle背景阴影样式
            if (HatchingCom.SelectedItem != null)
            {
                MyObject.My_Chart1.Series[0].BackHatchStyle = (ChartHatchStyle)ChartHatchStyle.Parse(typeof(ChartHatchStyle), this.HatchDictionary[HatchingCom.SelectedItem.ToString()]);
            }
            #endregion

            #endregion

            #region 当边框风格和文本风格确定按钮被点击的时候



            #region 选取边框颜色
            //边框颜色
            if (colorPickerButton2.SelectedColor != Color.Empty)
            {
                //MyObject.My_Chart1.BorderlineColor = colorPickerButton2.SelectedColor;
                MyObject.My_Chart1.Series[0].BorderColor = colorPickerButton2.SelectedColor;
            }
            #endregion

            #region 设置数据点边框宽度
            if (BorderSizeCom.SelectedItem != null)
            {
                MyObject.My_Chart1.Series[0].BorderWidth = int.Parse(BorderSizeCom.GetItemText(BorderSizeCom.SelectedItem));
            }
            #endregion

            #region 数据点边框样式
            if (BorderDashStyleCom.SelectedItem != null)
            {
                MyObject.My_Chart1.Series[0].BorderDashStyle = (ChartDashStyle)ChartDashStyle.Parse(typeof(ChartDashStyle), this.LineStyleDictionary[BorderDashStyleCom.SelectedItem.ToString()]);

            }
            #endregion

            #region 阴影偏移量
            if (ShadowOffset.SelectedItem != null)
            {
                MyObject.My_Chart1.Series[0].ShadowOffset = int.Parse(ShadowOffset.GetItemText(ShadowOffset.SelectedItem));
            }
            #endregion


            //设置文本
            switch (this.comboBox2.Text)
            {
                case "主标题":
                    MyObject.My_Chart1.Titles[0].ForeColor = title.ForeColor;
                    MyObject.My_Chart1.Titles[0].Font = title.Font;
                    MyObject.My_Chart1.Titles[0].Text = title.Text;
                    break;
                //case "X轴标题":
                //    MyObject.My_Chart1.ChartAreas[0].AxisX.TitleForeColor = title.ForeColor;
                //    MyObject.My_Chart1.ChartAreas[0].AxisX.TitleFont = title.Font;
                //    MyObject.My_Chart1.ChartAreas[0].AxisX.Title = title.Text;
                //    break;
                //case "Y轴标题":
                //    MyObject.My_Chart1.ChartAreas[0].AxisY.TitleForeColor = title.ForeColor;
                //    MyObject.My_Chart1.ChartAreas[0].AxisY.TitleFont = title.Font;
                //    MyObject.My_Chart1.ChartAreas[0].AxisY.Title = title.Text;
                //    break;
            }


            #endregion

            if (this.comboBox1.SelectedIndex >= 0)
            {//显示数值
                if (this.comboBox1.SelectedIndex == 1)
                {
                    MyObject.My_Chart1.Series[0]["PieLabelStyle"] = "Disabled";
                }
                else
                {
                    MyObject.My_Chart1.Series[0]["PieLabelStyle"] = "Inside";
                    //MyObject.My_Chart1.Series[0].LabelForeColor = label5.ForeColor;
                    //MyObject.My_Chart1.Series[0].Font = label5.Font;
                }

            }
            if (this.comboBox3.SelectedIndex >= 0)
            {//显示图例项
                if (this.comboBox3.SelectedIndex == 1)
                {
                    MyObject.My_Chart1.Legends[0].Enabled = false;
                }
                else
                {
                    MyObject.My_Chart1.Legends[0].Enabled = true;
                    //MyObject.My_Chart1.Legends[0].ForeColor = label5.ForeColor;
                    //MyObject.My_Chart1.Legends[0].Font = label5.Font;
                }

            }
            if (this.comboBox4.SelectedIndex >= 0)
            {//图例项显示方式
                if (this.comboBox4.SelectedIndex == 0)
                {
                    MyObject.My_Chart1.Legends[0].LegendStyle = LegendStyle.Table;
                    MyObject.My_Chart1.Legends[0].Docking = Docking.Top;
                }
                else if (this.comboBox4.SelectedIndex == 1)
                {
                    MyObject.My_Chart1.Legends[0].LegendStyle = LegendStyle.Table;
                    MyObject.My_Chart1.Legends[0].Docking = Docking.Bottom;
                }
                else if (this.comboBox4.SelectedIndex == 2)
                {
                    MyObject.My_Chart1.Legends[0].LegendStyle = LegendStyle.Table;
                    MyObject.My_Chart1.Legends[0].Docking = Docking.Left;
                }
                else
                {
                    MyObject.My_Chart1.Legends[0].LegendStyle = LegendStyle.Table;
                    MyObject.My_Chart1.Legends[0].Docking = Docking.Right;
                }
            }
            switch (this.comboBox2.Text)
            {
                case "数值":
                    MyObject.My_Chart1.Series[0].LabelForeColor = label5.ForeColor;
                    MyObject.My_Chart1.Series[0].Font = label5.Font;
                    break;
                case "图例项":
                    MyObject.My_Chart1.Legends[0].ForeColor = label5.ForeColor;
                    MyObject.My_Chart1.Legends[0].Font = label5.Font;
                    break;
            }


            MainFrame mf = new MainFrame();

            if (checkBox1.Checked)
            {
                SaveAttribute.IsRotate = true;
                mf.timer1.Start();
            }
            else
            {
                SaveAttribute.IsRotate = false;
                mf.timer1.Stop();
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //弹出修改颜色对话框
            ColorDialog diag = new ColorDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                this.title.ForeColor = diag.Color;
            }
        }

        private void btnChangeFont_Click(object sender, EventArgs e)
        {
            //弹出修改字体对话框

            FontDialog diag = new FontDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                this.title.Font = diag.Font;
            }
        }

        private void btnPageSet_Click(object sender, EventArgs e)
        {
            try
            {
                MyObject.My_Chart1.Printing.PageSetup();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Chart Control for .NET Framework", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnPreView_Click(object sender, EventArgs e)
        {
            try
            {
                MyObject.My_Chart1.Printing.PrintPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Chart Control for .NET Framework", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                MyObject.My_Chart1.Printing.Print(ShowPrintDiag.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Chart Control for .NET Framework", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click_1(object sender, EventArgs e)
        {

            //修改颜色 
            ColorDialog diag = new ColorDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (this.comboBox2.SelectedIndex == 0)
                {
                    this.title.ForeColor = diag.Color;
                }
                else if (this.comboBox2.SelectedIndex == 1||this .comboBox2 .SelectedIndex ==2)
                {
                    this.label5.ForeColor = diag.Color;
                }

            }
        }

        private void btnChangeFont_Click_1(object sender, EventArgs e)
        {
            FontDialog diag = new FontDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (this.comboBox2.SelectedIndex == 0 )
                {
                    this.title.Font = diag.Font;
                }
                else if (this.comboBox2.SelectedIndex == 1||this.comboBox2 .SelectedIndex ==2)
                {
                    this.label5.Font = diag.Font;
                }
            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            OK();
            this.Close();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetProperty2_Load(object sender, EventArgs e)
        {
          
            //线性
            this.LineStyleDictionary.Add("不设置", "NotSet");
            this.LineStyleDictionary.Add("虚线", "Dash");
            this.LineStyleDictionary.Add("虚线点", "DashDot");
            this.LineStyleDictionary.Add("虚线点点", "DashDotDot");
            this.LineStyleDictionary.Add("点", "Dot");
            this.LineStyleDictionary.Add("实线", "Solid");

            //颜色分布
            this.ColorDistributeDictionary.Add("无", "None");
            this.ColorDistributeDictionary.Add("左右", "LeftRight");
            this.ColorDistributeDictionary.Add("顶底", "TopBottom");
            this.ColorDistributeDictionary.Add("居中", "Center");
            this.ColorDistributeDictionary.Add("水平居中", "HorizontalCenter");
            this.ColorDistributeDictionary.Add("垂直居中", "VerticalCenter");

            //阴影
            this.HatchDictionary.Add("不填充", "None");
            this.HatchDictionary.Add("均匀填充", "Cross");
            this.HatchDictionary.Add("水平填充", "Horizontal");
            this.HatchDictionary.Add("垂直填充", "Vertical");
            this.HatchDictionary.Add("从左上到右下", "ForwardDiagonal");
            this.HatchDictionary.Add("从左下到右上", "BackwardDiagonal");
            this.HatchDictionary.Add("渐变填充", "OutlinedDiamond");
            this.HatchDictionary.Add("岩性填充", "Weave");
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.title.Text = "";
            if (this.comboBox2.Text.Equals("数值") )
            {
                this.ChangeTitle.Enabled = false;
                this.label5.Font = MyObject.My_Chart1.Series[0].Font;
                this.label5.ForeColor = MyObject.My_Chart1.Series[0].LabelForeColor;
                this.colorPickerButton3.SelectedColor = MyObject.My_Chart1.Series[0].LabelForeColor;
            }
            else if (this.comboBox2.Text.Equals("图例项"))
            {
                this.ChangeTitle.Enabled = false;
                this.label5.Font = MyObject.My_Chart1.Legends[0].Font;
                this.label5.ForeColor = MyObject.My_Chart1.Legends[0].ForeColor;
                this.colorPickerButton3.SelectedColor = MyObject.My_Chart1.Legends[0].ForeColor;
            }
            else
            {
                this.ChangeTitle.Enabled = true;
                this.ChangeTitle.TitleText = "修改" + this.comboBox2.Text;
                this.title.Text = MyObject.My_Chart1.Titles[0].Text;
                this.title.Font = MyObject.My_Chart1.Titles[0].Font;
                this.colorPickerButton3.SelectedColor = MyObject.My_Chart1.Titles[0].ForeColor;
            }







        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void colorPickerButton3_SelectedColorChanged(object sender, EventArgs e)
        {
            if (this.comboBox2.SelectedIndex == 0)
            {
                this.title.ForeColor = colorPickerButton3.SelectedColor;
            }
            else if (this.comboBox2.SelectedIndex == 1 || this.comboBox2.SelectedIndex == 2)
            {
                this.label5.ForeColor = colorPickerButton3.SelectedColor;
            }
        }

        private void comboBox5_MouseDown(object sender, MouseEventArgs e)
        {
            FontDialog diag = new FontDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (this.comboBox2.SelectedIndex == 0)
                {
                    this.title.Font = diag.Font;
                }
                else if (this.comboBox2.SelectedIndex == 1 || this.comboBox2.SelectedIndex == 2)
                {
                    this.label5.Font = diag.Font;
                }
            }
        }

      

      
      
    }
}