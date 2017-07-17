/* 
 * 作者;肖宇博
 * 日期：2014/7/9
 * 功能：这是设置递减曲线属性的窗体
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
using System.Collections;

namespace GeoDemo
{
    public partial class SetDiJianQuXianProperity : Form
    {
        public SetDiJianQuXianProperity()
        {
            InitializeComponent();
            this.comboBox5.SelectedIndex = 0;
            this.comboBox4.SelectedIndex = 0;
            #region 晁 修改
            #region  笔刷风格
            if (MyObject.My_Chart1.Series[1].Color == Color.Empty)
            {
                this.colorPickerButton1.SelectedColor = Color.Red;
            }
            else
            {
                this.colorPickerButton1.SelectedColor = MyObject.My_Chart1.Series[1].Color;
            }
            if (Convert.ToInt32(MyObject.My_Chart1.Series[1].BorderDashStyle) == 5)
            {
                this.comboBox1.SelectedIndex = 0;
            }
            else
            {
                this.comboBox1.SelectedIndex = 1;
            }
            switch (Convert.ToString(MyObject.My_Chart1.ChartAreas[0].AxisX.LabelStyle.Format))
            {
                case "d   ": this.comboBox3.SelectedIndex = 0; break;
                case "M   ": this.comboBox3.SelectedIndex = 1; break;
                case "Y": this.comboBox3.SelectedIndex = 2; break;
                case "ddd,d": this.comboBox3.SelectedIndex = 3; break;
                case "D": this.comboBox3.SelectedIndex = 4; break;
                case "G": this.comboBox3.SelectedIndex = 5; break;
            }
            //    MessageBox.Show(Convert.ToString(MyObject.My_Chart1.ChartAreas[0].AxisX.LabelStyle.Format));          
            #endregion
            #endregion  晁  修改

            Yinterval.Text = "20"; //Y轴精度
            Ymaximum.Text = MyObject.My_Chart1.ChartAreas[0].AxisY.Maximum.ToString();//Y轴上刻度
            Yminimum.Text = MyObject.My_Chart1.ChartAreas[0].AxisY.Minimum.ToString();//Y轴下刻度
        }


        // 这里需要说明一下，定义如下变量，后来数据接口做出来后这些变量都是要直接从TXT读取的。
        //日期变量读取
        DateTime[] dt2;
        ArrayList dtal = new ArrayList();
        //产量数据读取
        double[] p2;


        //int InitForeCastStartTime = 7; //初始的预测开始的时间的下标就是p2的114所在的index
        //int Qi = 119;
        //int EndTime = 11;



        double b = 0;
        int Maxp2Index = 0; //p2最大值的下标
        static int DiJianStartIndex = 0;//递减开始的那个p2[i]值的下表就是i的值,就是Qi
        static int DiJianEndIndex = 0;//递减开始的那个p2[i]值的下表就是i的值,就是Qi
        private void SetDianJianQuXianProperity_Load(object sender, EventArgs e)
        {


            //先让从txt中读到的值复制给dt2和p2数组
            ReadDataFromDatatable readdatafromdatatable = new ReadDataFromDatatable();
            int countx = readdatafromdatatable.getCounx();//从datatable中读取有多少行数据
            dt2 = new DateTime[countx];

            p2 = new double[countx];
            readdatafromdatatable.readDataService(ref dt2, ref p2);//将datatable中的数据读到两个数组中去


            for (int a = 0; a < dt2.Length; a++)
            {
                dtal.Add(dt2[a]);//把dt2的值放入arraylist
            }

            double maxp2 = p2.ToArray().Max();
            Maxp2Index = 0;
            //得到最大值的下标
            for (int i = 0; i < p2.Length; i++)
            {
                if (p2[i] == maxp2)
                {
                    Maxp2Index = i;
                    break;
                }
            }
            //为用户选择第一次递减的值的那个combox加载值
            for (int j = Maxp2Index; j < p2.Length; j++)
            {
                this.comboBox2.Items.Add(p2[j]);
            }
            this.comboBox2.SelectedIndex = 0;//默认选择从最大值119开始递减



            for (int j = 1; j < countx; j++)
            {
                endTime.Items.Add(dt2[countx - 1].AddMonths(j));
                dtal.Add(dt2[countx - 1].AddMonths(j));//把后来加入的值也放入arraylist

            }

            this.dictionary.Add("实线", "Solid");
            this.dictionary.Add("虚线", "Dash");

        }


        //b=0时递减率计算 ，qt为产量，t为递减开始时间
        public double Arps1(double qt, int t, int Qi)
        {
            double Di;
            Di = Math.Log(Convert.ToDouble(Qi) / Convert.ToDouble(qt)) / t;

            return Di;
        }
        //0<b<1时递减率计算
        public double Arps2(double qt, int t, double b, int Qi)
        {
            double Di;
            Di = (Math.Pow((Convert.ToDouble(Qi) / Convert.ToDouble(qt)), b) - 1) / (b * t);
            return Di;
        }
        //b=1时递减率
        public double Arps3(double qt, int t, int Qi)
        {
            double Di;
            Di = (Convert.ToDouble(Qi) / Convert.ToDouble(qt) - 1) / Convert.ToDouble(t);
            return Di;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                this.textBox1.Enabled = true;
            }
            else
            {
                this.textBox1.Enabled = false;
            }
        }

        //private void radioButton2_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (radioButton2.Checked)
        //    {
        //        this.textBox1.Enabled = true;
        //    }
        //    else
        //    {
        //        this.textBox1.Enabled = false;
        //    }
        //}

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                this.textBox1.Enabled = true;
            }
            else
            {
                this.textBox1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OK();
            this.Close();
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim().Equals(string.Empty) || (Convert.ToDouble(textBox1.Text)) < 0.0 || (Convert.ToDouble(textBox1.Text) > 1))
                {
                    errorProvider1.SetError(textBox1, "输入参数不合法，请重新输入!");
                    this.button1.Enabled = false;
                }
                else
                {
                    this.button1.Enabled = true;
                    errorProvider1.Clear();
                }
            }
            catch
            {
                errorProvider1.SetError(textBox1, "输入参数不合法，请重新输入!");
            }
        }




        private void endTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            DiJianEndIndex = this.endTime.SelectedIndex + dt2.Length;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //这里用一下字典的知识

        private Dictionary<string, string> dictionary = new Dictionary<string, string>();//申明一个字典类型

        private void button3_Click(object sender, EventArgs e)
        {

            OK();
            this.Close();
        }

        public void OK()
        {

            #region 功能
            if (MyObject.My_Chart1 != null)
            {
                //当b=0;
                if (radioButton1.Checked)
                {
                    double t = 0;
                    MyObject.My_Chart1.Series[1].Points.Clear();//每当改变起始时间或者结束时间的时候，都先清除上个序列的值
                    MyObject.My_Chart1.Series[0].Points.Clear();//每当改变起始时间或者结束时间的时候，都先清除上个序列的值
                    //MyObject.My_Chart1.Series[1].XValueType = ChartValueType.DateTime;
                    //MyObject.My_Chart1.Series[0].XValueType = ChartValueType.DateTime;
                    //然后再绑定基础的那个部分
                    for (int i = 0; i < p2.Length; i++)
                    {
                        MyObject.My_Chart1.Series[0].Points.AddXY(dt2[i], p2[i]);

                    }
                    for (int i = 0; i <= DiJianStartIndex; i++)
                    {
                        MyObject.My_Chart1.Series[1].Points.AddXY(dt2[i], p2[i]);
                        MyObject.My_Chart1.Series[1].Points[i].Color = Color.Transparent;//前面的线让他看不到
                    }
                    for (int i = DiJianStartIndex + 1; i < p2.Length; i++)
                    {
                        double temp = 0;
                        double temp1 = Convert.ToDouble(((1 - temp) * p2[i - 1]).ToString("0.00"));
                        temp = Arps1(p2[i], (i - DiJianStartIndex), (int)p2[DiJianStartIndex]);
                        MyObject.My_Chart1.Series[1].Points.AddXY(dt2[i], temp1);
                        MyObject.My_Chart1.Series[1].Points[i].ToolTip = "预测的产值为：" + temp1;
                        MyObject.My_Chart1.Series[1].Points[i].Color = colorPickerButton1.SelectedColor;//后面的线显示出来
                        t = temp;
                    }
                    //再利用最后一个值让曲线延伸一段
                    double temp2 = p2[p2.Length - 1];
                    for (int j = p2.Length; j <= DiJianEndIndex; j++)
                    {
                        double temp3 = Convert.ToDouble(((1 - t) * temp2).ToString("0.00"));
                        MyObject.My_Chart1.Series[0].Points.AddXY(dtal[j], temp3);
                        MyObject.My_Chart1.Series[0].IsValueShownAsLabel = false;
                        MyObject.My_Chart1.Series[0].Points[j].Color = Color.Red;
                        MyObject.My_Chart1.Series[1].Points.AddXY(dtal[j], temp3);
                        MyObject.My_Chart1.Series[1].Points[j].ToolTip = "预测的产值为：" + temp3;
                        MyObject.My_Chart1.Series[1].Points[j].Color = colorPickerButton1.SelectedColor;//后面的线显示出来
                        temp2 = temp2 * (1 - t);
                    }

                }


                //当b=1的时候
                else if (radioButton3.Checked)
                {
                    double t = 0;
                    MyObject.My_Chart1.Series[1].Points.Clear();//每当改变起始时间或者结束时间的时候，都先清除上个序列的值
                    MyObject.My_Chart1.Series[0].Points.Clear();//每当改变起始时间或者结束时间的时候，都先清除上个序列的值
                    //MyObject.My_Chart1.Series[1].XValueType = ChartValueType.DateTime;
                    //MyObject.My_Chart1.Series[0].XValueType = ChartValueType.DateTime;
                    //然后再绑定基础的那个部分
                    for (int i = 0; i < p2.Length; i++)
                    {
                        MyObject.My_Chart1.Series[0].Points.AddXY(dt2[i], p2[i]);

                    }
                    for (int i = 0; i <= DiJianStartIndex; i++)
                    {
                        MyObject.My_Chart1.Series[1].Points.AddXY(dt2[i], p2[i]);
                        MyObject.My_Chart1.Series[1].Points[i].Color = Color.Transparent;//前面的线让他看不到
                    }
                    for (int i = DiJianStartIndex + 1; i < p2.Length; i++)
                    {
                        double temp = 0;

                        temp = Arps3(p2[i], (i - DiJianStartIndex), (int)p2[DiJianStartIndex]);
                        double temp1 = Convert.ToDouble(((1 - temp) * p2[i - 1]).ToString("0.00"));
                        MyObject.My_Chart1.Series[1].Points.AddXY(dt2[i], temp1);
                        MyObject.My_Chart1.Series[1].Points[i].ToolTip = "预测的产值为：" + temp1;
                        MyObject.My_Chart1.Series[1].Points[i].Color = colorPickerButton1.SelectedColor;//后面的线显示出来
                        t = temp;
                    }
                    //再利用最后一个值让曲线延伸一段
                    double temp2 = p2[p2.Length - 1];
                    for (int j = p2.Length; j <= DiJianEndIndex; j++)
                    {
                        double temp3 = Convert.ToDouble(((1 - t) * temp2).ToString("0.00"));
                        MyObject.My_Chart1.Series[0].Points.AddXY(dtal[j], temp3);
                        MyObject.My_Chart1.Series[0].IsValueShownAsLabel = false;
                        MyObject.My_Chart1.Series[0].Points[j].Color = Color.Red;
                        MyObject.My_Chart1.Series[1].Points.AddXY(dtal[j], temp3);
                        MyObject.My_Chart1.Series[1].Points[j].ToolTip = "预测的产值为：" + temp3;
                        MyObject.My_Chart1.Series[1].Points[j].Color = colorPickerButton1.SelectedColor;//后面的线显示出来
                        temp2 = temp2 * (1 - t);
                    }
                }
                //当0<b<1的时候
                else if (radioButton2.Checked)
                {
                    double t = 0;
                    MyObject.My_Chart1.Series[1].Points.Clear();//每当改变起始时间或者结束时间的时候，都先清除上个序列的值
                    MyObject.My_Chart1.Series[0].Points.Clear();//每当改变起始时间或者结束时间的时候，都先清除上个序列的值
                    //MyObject.My_Chart1.Series[1].XValueType = ChartValueType.DateTime;
                    //MyObject.My_Chart1.Series[0].XValueType = ChartValueType.DateTime;
                    //然后再绑定基础的那个部分
                    for (int i = 0; i < p2.Length; i++)
                    {
                        MyObject.My_Chart1.Series[0].Points.AddXY(dt2[i], p2[i]);

                    }
                    for (int i = 0; i <= DiJianStartIndex; i++)
                    {
                        MyObject.My_Chart1.Series[1].Points.AddXY(dt2[i], p2[i]);
                        MyObject.My_Chart1.Series[1].Points[i].Color = Color.Transparent;//前面的线让他看不到
                    }
                    for (int i = DiJianStartIndex + 1; i < p2.Length; i++)
                    {
                        double temp = 0;
                        b = Convert.ToDouble(textBox1.Text.ToString());
                        temp = Arps2(p2[i], (i - DiJianStartIndex), b, (int)p2[DiJianStartIndex]);
                        double temp1 = Convert.ToDouble(((1 - temp) * p2[i - 1]).ToString("0.00"));
                        MyObject.My_Chart1.Series[1].Points.AddXY(dt2[i], temp1);
                        MyObject.My_Chart1.Series[1].Points[i].ToolTip = "预测的产值为：" + temp1;
                        MyObject.My_Chart1.Series[1].Points[i].Color = colorPickerButton1.SelectedColor;//后面的线显示出来
                        t = temp;
                    }
                    //再利用最后一个值让曲线延伸一段
                    double temp2 = p2[p2.Length - 1];
                    for (int j = p2.Length; j <= DiJianEndIndex; j++)
                    {
                        double temp3 = Convert.ToDouble(((1 - t) * temp2).ToString("0.00"));
                        MyObject.My_Chart1.Series[0].Points.AddXY(dtal[j], temp3);
                        MyObject.My_Chart1.Series[0].IsValueShownAsLabel = false;
                        MyObject.My_Chart1.Series[0].Points[j].Color = Color.Red;
                        MyObject.My_Chart1.Series[1].Points.AddXY(dtal[j], temp3);
                        MyObject.My_Chart1.Series[1].Points[j].ToolTip = "预测的产值为：" + temp3;
                        MyObject.My_Chart1.Series[1].Points[j].Color = colorPickerButton1.SelectedColor;//后面的线显示出来
                        temp2 = temp2 * (1 - t);
                    }


                }
            }
            #endregion

            #region 图形风格
            //确定按钮被按下
            ChartDashStyle style = (ChartDashStyle)Enum.Parse(typeof(ChartDashStyle), this.dictionary[this.comboBox1.SelectedItem.ToString()]);
            if (MyObject.My_Chart1 != null)
            {
                MyObject.My_Chart1.Series[1].Color = colorPickerButton1.SelectedColor;
                MyObject.My_Chart1.Series[1].BorderDashStyle = style;

            }
            #endregion

            #region 选取Y轴精度
            if (Yinterval.Text != string.Empty && Yinterval.Text != Convert.ToString(0))
            {
                //判断如果输入的不是数字 也不是0
                MyObject.My_Chart1.ChartAreas[0].AxisY.Interval = double.Parse(Yinterval.Text.ToString());
            }
            else
            {
                MessageBox.Show("Y轴精度输入值有误！");
            }
            #endregion

            #region 选取Y轴最大值
            if (Ymaximum.Text != string.Empty)
            {
                //判断如果输入的不是数字
                MyObject.My_Chart1.ChartAreas[0].AxisY.Maximum = double.Parse(Ymaximum.Text.ToString());
            }
            else
            {
                MessageBox.Show("Y轴最大值输入有误！");
            }
            #endregion

            #region 选取Y轴最小值
            if (Yminimum.Text != string.Empty)
            {
                //判断如果输入的不是数字
                MyObject.My_Chart1.ChartAreas[0].AxisY.Minimum = double.Parse(Yminimum.Text.ToString());
            }
            else
            {
                MessageBox.Show("Y轴最小值输入有误！");
            }
            #endregion

            #region 设置x轴样式
            MyObject.My_Chart1.ChartAreas[0].AxisX.LabelStyle.Format = GetDateItem(this.comboBox3.SelectedIndex);

            #endregion

            //设置图题

            //设置文本
            switch (this.comboBox5.Text)
            {
                case "图题":
                    MyObject.My_Chart1.Titles[0].ForeColor = title.ForeColor;
                    MyObject.My_Chart1.Titles[0].Font = title.Font;
                    MyObject.My_Chart1.Titles[0].Text = title.Text;
                    break;
                case "X轴标题":
                    MyObject.My_Chart1.ChartAreas[0].AxisX.TitleForeColor = title.ForeColor;
                    MyObject.My_Chart1.ChartAreas[0].AxisX.TitleFont = title.Font;
                    MyObject.My_Chart1.ChartAreas[0].AxisX.Title = title.Text;
                    break;
                case "Y轴标题":
                    MyObject.My_Chart1.ChartAreas[0].AxisY.TitleForeColor = title.ForeColor;
                    MyObject.My_Chart1.ChartAreas[0].AxisY.TitleFont = title.Font;
                    MyObject.My_Chart1.ChartAreas[0].AxisY.Title = title.Text;
                    break;
            }


            if (this.comboBox4.SelectedIndex >= 0)
            {
                if (this.comboBox4.SelectedIndex == 1)
                {
                    MyObject.My_Chart1.Series[0].IsValueShownAsLabel = false;

                }
                else
                {
                    MyObject.My_Chart1.Series[0].IsValueShownAsLabel = true;
                    MyObject.My_Chart1.Series[0].LabelForeColor = label9.ForeColor;
                    MyObject.My_Chart1.Series[0].Font = label9.Font;
                }

            }



        }

        private void tabControlPanel2_Click(object sender, EventArgs e)
        {

        }
        private string GetDateItem(int item)
        {
            string format;
            switch (item)
            {
                case 0:
                    format = "d";
                    break;
                case 1:
                    format = "M";
                    break;
                case 2:
                    format = "Y";
                    break;
                case 3:
                    format = "ddd,d";
                    break;
                case 4:
                    format = "D";
                    break;
                case 5:
                    format = "G";
                    break;
                default:
                    format = "";
                    break;
            }

            return format;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            FontDialog diag = new FontDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                this.title.Font = diag.Font;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ColorDialog diag = new ColorDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                this.title.ForeColor = diag.Color;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //页面设置
            try
            {
                MyObject.My_Chart1.Printing.PageSetup();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Chart Control for .NET Framework", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //预览
            try
            {
                MyObject.My_Chart1.Printing.PrintPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Chart Control for .NET Framework", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //打印
            try
            {
                MyObject.My_Chart1.Printing.Print(ShowPrintDiag.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Chart Control for .NET Framework", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DiJianStartIndex = Maxp2Index + this.comboBox2.SelectedIndex;//这就是用户选择的第一次递减的值在p2数组的下标，如从119递减
            //该值就是6，如果从114递减该值就为7
            //MessageBox.Show(dt2[DiJianStartIndex].ToString());
            this.startTime.Text = dt2[DiJianStartIndex].ToShortDateString();


        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            OK();
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            //数值颜色
            //修改颜色 
            ColorDialog diag = new ColorDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (comboBox5.SelectedIndex == 0 || comboBox5.SelectedIndex == 2 || comboBox5.SelectedIndex == 3)
                {
                    this.title.ForeColor = diag.Color;
                }
                else if (this.comboBox5.SelectedIndex == 1)
                {
                    this.label9.ForeColor = diag.Color;
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            FontDialog diag = new FontDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (this.comboBox5.SelectedIndex == 0 || this.comboBox5.SelectedIndex == 2 || this.comboBox5.SelectedIndex == 3)
                {
                    this.title.Font = diag.Font;
                }
                else if (this.comboBox5.SelectedIndex == 1)
                {
                    this.label9.Font = diag.Font;
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            OK();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.title.Text = "";
            if (this.comboBox5.Text.Equals("数值"))
            {
                this.ChangeTitle.Enabled = false;
            }
            else
            {
                this.ChangeTitle.Enabled = true;
                this.ChangeTitle.TitleText = "修改" + this.comboBox5.Text;
                if (this.comboBox5.Text.Equals("图题"))
                {
                    this.title.Text = "图题";

                }
                else if (this.comboBox5.Text.Equals("X轴标题"))
                {
                    this.title.Text = "X轴标题";
                }
                else if (this.comboBox5.Text.Equals("Y轴标题"))
                {
                    this.title.Text = "Y轴标题";
                }

            }
        }
    }
}
