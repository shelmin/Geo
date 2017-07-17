/* 
 * 作者;肖宇博
 * 日期：2014/6/16
 * 功能：这是主界面，所有的图都动态生成在主界面，画图调用的是我写的画图的那个DrawChart类，鼠标的操作调用的是我写的MouseEvent那个类，并且要与右键菜单栏以及开始选项卡上的操作相关联
 * 这里的代码复用率并不高，请后期改动！
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
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Drawing.Imaging;
using System.Collections;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using Plytmf.Net.Bottom;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Globalization;

namespace GeoDemo
{
    public delegate void setclick(object sender, EventArgs e);
    public delegate void paintrefresh();
    public delegate void paintrefresh1();
    public delegate void paintrefresh2();
    public delegate void paintrefresh3();
    public delegate void paintrefresh4();
    public delegate void paintrefresh5();
    
    public partial class MainFrame : DevComponents.DotNetBar.Office2007RibbonForm, IFlow
    {

        public MainFrame()
        {
            InitializeComponent();
            operatingSteps = 0;
            //this.FormBorderStyle = FormBorderStyle.None;

            //改变窗体最大值
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            //绘制panel的位置以及大小
            this.panel1.Location = new System.Drawing.Point(0, ribbonControl1.Height);//在ribboncontroll的下面
            this.panel1.Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height - ribbonControl1.Height);
            //this.MaximizeBox = false;

            //初始化colorpickerbutton的颜色
            colorPickerButton1.SelectedColor = Color.Black;
            #region 设置字体属性
            this.comboBoxFontStyle.SelectedIndex = 0;
            this.comboBoxFontSize.SelectedIndex = 0;
            this.comboBoxFontStyle.SelectedIndexChanged += new EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBoxFontSize.SelectedIndexChanged += new EventHandler(this.comboBox_SelectedIndexChanged);
            this.dictionary = new Dictionary<string, string>();
            this.dictionary.Add("正常", "Regular");
            this.dictionary.Add("加粗", "Bold");
            this.dictionary.Add("斜体", "Italic");
            this.dictionary.Add("下划线", "Underline");
            this.dictionary.Add("删除线", "Strikeout");
            #endregion
        }
        public event setclick buttonsetclick;
        public event paintrefresh pselfrefresh;
        public event paintrefresh1 pselfrefresh1;
        public event paintrefresh2 pselfrefresh2;
        public event paintrefresh3 pselfrefresh3;
        public event paintrefresh4 pselfrefresh4;
        public event paintrefresh5 pselfrefresh5;
        private void MainFrame_Load(object sender, EventArgs e)
        {
            
            foreach (Control c in this.ribbonControl1.Controls)
            {
                c.MouseUp += new MouseEventHandler(c_MouseUp);
            }
            panel1.ContextMenuStrip = contextMenuStrip2;
            this.MouseWheel += panel1_mousewheel;
            #region 初始化Chart数组
            DrawChart dc = new DrawChart();
            dc.InitChart(chart1, NUM);
            dc.InitChart(chart2, NUM);
            dc.InitChart(chart3, NUM);
            dc.InitChart(chart4, NUM);
            dc.InitChart(chart5, NUM);
            dc.InitChart(chart6, NUM);
            dc.InitChart(chart7, NUM);
            dc.InitChart(chart8, NUM);
            dc.InitChart(chart9, NUM);
            dc.InitChart(chart10, NUM);
            dc.InitChart(chart11, NUM);
            dc.InitChart(chart12, NUM);//簇状条形图
            dc.InitChart(chart13, NUM);//堆积条形图
            dc.InitChart(chart14, NUM);//百分比堆积条形图
            dc.InitChart(chart15, NUM);//三维簇状条形图
            dc.InitChart(chart16, NUM);//三维堆积条形图
            dc.InitChart(chart17, NUM);//三维百分比堆积条形图
            dc.InitChart(chart18, NUM);//簇状水平圆柱图
            dc.InitChart(chart19, NUM);//堆积水平圆柱图
            dc.InitChart(chart20, NUM);//百分比堆积水平圆柱图
            dc.InitChart(chart21, NUM);//饼图
            dc.InitChart(chart22, NUM);//分离型饼图
            dc.InitChart(chart23, NUM);//三维饼图
            dc.InitChart(chart24, NUM);//三维分离型饼图
            dc.InitChart(chart25, NUM);//圆环图
            dc.InitChart(chart26, NUM);//分离圆环
            dc.InitChart(chart27, NUM);//面积图
            dc.InitChart(chart28, NUM);//堆积面积图
            dc.InitChart(chart29, NUM);//百分比堆积面积
            dc.InitChart(chart30, NUM);//三维面积
            dc.InitChart(chart31, NUM);//三维堆积面积
            dc.InitChart(chart32, NUM);//三维百分比堆积面积
            dc.InitChart(chart33, NUM);//折线图
            dc.InitChart(chart34, NUM);//带数据标记的折线图
            dc.InitChart(chart35, NUM);//三维折线图
            dc.InitChart(chart36, NUM);//仅带数据标记的散点图
            dc.InitChart(chart37, NUM);//带平滑线和数据标记的散点图
            dc.InitChart(chart38, NUM);//带平滑线散点图
            dc.InitChart(chart39, NUM);//带直线和数据标记的散点图
            dc.InitChart(chart40, NUM);//带直线散点图
            dc.InitChart(chart41, NUM);//递减曲线
            dc.InitChart(chart42, NUM);//相渗曲线
            dc.InitChart(chart43, NUM);//生产开发曲线
            dc.InitChart(chart44, NUM);//水井注水曲线
            dc.InitChart(chart45, NUM);//油井开发曲线
            #endregion

            #region 这一段代码看看可不可以封装
            #region 为每个相通类型的簇状柱形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart1[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseUp);
                chart1[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDown);
                chart1[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
                chart1[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDoubleClick);
                chart1[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseClick);
                chart1[jj].ContextMenuStrip = contextMenuStrip1;
                chart1[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积柱形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart2[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart2_MouseUp);
                chart2[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart2_MouseDown);
                chart2[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart2_MouseMove);
                chart2[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart2_MouseDoubleClick);
                chart2[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart2_MouseClick);
                chart2[jj].ContextMenuStrip = contextMenuStrip1;
                chart2[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);

            }
            #endregion

            #region 为每个相通类型的百分比堆积柱形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart3[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart3_MouseUp);
                chart3[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart3_MouseDown);
                chart3[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart3_MouseMove);
                chart3[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart3_MouseDoubleClick);
                chart3[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart3_MouseClick);
                chart3[jj].ContextMenuStrip = contextMenuStrip1;
                chart3[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);

            }
            #endregion

            #region 为每个相通类型的三维簇状柱形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart4[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart4_MouseUp);
                chart4[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart4_MouseDown);
                chart4[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart4_MouseMove);
                chart4[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart4_MouseDoubleClick);
                chart4[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart4_MouseClick);
                chart4[jj].ContextMenuStrip = contextMenuStrip1;
                chart4[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);

            }
            #endregion

            #region 为每个相通类型的三维堆积柱形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart5[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart5_MouseUp);
                chart5[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart5_MouseDown);
                chart5[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart5_MouseMove);
                chart5[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart5_MouseDoubleClick);
                chart5[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart5_MouseClick);
                chart5[jj].ContextMenuStrip = contextMenuStrip1;
                chart5[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);

            }
            #endregion

            #region 为每个相通类型的三维百分比堆积柱形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart6[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart6_MouseUp);
                chart6[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart6_MouseDown);
                chart6[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart6_MouseMove);
                chart6[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart6_MouseDoubleClick);
                chart6[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart6_MouseClick);
                chart6[jj].ContextMenuStrip = contextMenuStrip1;
                chart6[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);

            }
            #endregion

            #region 为每个相通类型的三维柱形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart7[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart7_MouseUp);
                chart7[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart7_MouseDown);
                chart7[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart7_MouseMove);
                chart7[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart7_MouseDoubleClick);
                chart7[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart7_MouseClick);
                chart7[jj].ContextMenuStrip = contextMenuStrip1;
                chart7[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);

            }
            #endregion

            #region 为每个相通类型的簇状圆柱形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart8[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart8_MouseUp);
                chart8[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart8_MouseDown);
                chart8[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart8_MouseMove);
                chart8[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart8_MouseDoubleClick);
                chart8[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart8_MouseClick);
                chart8[jj].ContextMenuStrip = contextMenuStrip1;
                chart8[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);

            }
            #endregion

            #region 为每个相通类型的堆积圆柱图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart9[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart9_MouseUp);
                chart9[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart9_MouseDown);
                chart9[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart9_MouseMove);
                chart9[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart9_MouseDoubleClick);
                chart9[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart9_MouseClick);
                chart9[jj].ContextMenuStrip = contextMenuStrip1;
                chart9[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的百分比堆积圆柱图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart10[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart10_MouseUp);
                chart10[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart10_MouseDown);
                chart10[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart10_MouseMove);
                chart10[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart10_MouseDoubleClick);
                chart10[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart10_MouseClick);
                chart10[jj].ContextMenuStrip = contextMenuStrip1;
                chart10[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的三维圆柱柱形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart11[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart11_MouseUp);
                chart11[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart11_MouseDown);
                chart11[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart11_MouseMove);
                chart11[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart11_MouseDoubleClick);
                chart11[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart11_MouseClick);
                chart11[jj].ContextMenuStrip = contextMenuStrip1;
                chart11[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的簇状条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart12[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart12_MouseUp);
                chart12[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart12_MouseDown);
                chart12[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart12_MouseMove);
                chart12[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart12_MouseDoubleClick);
                chart12[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart12_MouseClick);
                chart12[jj].ContextMenuStrip = contextMenuStrip1;
                chart12[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);

            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart13[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart13_MouseUp);
                chart13[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart13_MouseDown);
                chart13[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart13_MouseMove);
                chart13[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart13_MouseDoubleClick);
                chart13[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart13_MouseClick);
                chart13[jj].ContextMenuStrip = contextMenuStrip1;
                chart13[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart14[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart14_MouseUp);
                chart14[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart14_MouseDown);
                chart14[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart14_MouseMove);
                chart14[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart14_MouseDoubleClick);
                chart14[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart14_MouseClick);
                chart14[jj].ContextMenuStrip = contextMenuStrip1;
                chart14[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart15[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart15_MouseUp);
                chart15[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart15_MouseDown);
                chart15[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart15_MouseMove);
                chart15[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart15_MouseDoubleClick);
                chart15[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart15_MouseClick);
                chart15[jj].ContextMenuStrip = contextMenuStrip1;
                chart15[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart16[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart16_MouseUp);
                chart16[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart16_MouseDown);
                chart16[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart16_MouseMove);
                chart16[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart16_MouseDoubleClick);
                chart16[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart16_MouseClick);
                chart16[jj].ContextMenuStrip = contextMenuStrip1;
                chart16[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart17[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart17_MouseUp);
                chart17[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart17_MouseDown);
                chart17[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart17_MouseMove);
                chart17[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart17_MouseDoubleClick);
                chart17[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart17_MouseClick);
                chart17[jj].ContextMenuStrip = contextMenuStrip1;
                chart17[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart18[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart18_MouseUp);
                chart18[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart18_MouseDown);
                chart18[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart18_MouseMove);
                chart18[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart18_MouseDoubleClick);
                chart18[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart18_MouseClick);
                chart18[jj].ContextMenuStrip = contextMenuStrip1;
                chart18[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart19[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart19_MouseUp);
                chart19[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart19_MouseDown);
                chart19[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart19_MouseMove);
                chart19[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart19_MouseDoubleClick);
                chart19[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart19_MouseClick);
                chart19[jj].ContextMenuStrip = contextMenuStrip1;
                chart19[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart20[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart20_MouseUp);
                chart20[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart20_MouseDown);
                chart20[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart20_MouseMove);
                chart20[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart20_MouseDoubleClick);
                chart20[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart20_MouseClick);
                chart20[jj].ContextMenuStrip = contextMenuStrip1;
                chart20[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart21[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart21_MouseUp);
                chart21[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart21_MouseDown);
                chart21[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart21_MouseMove);
                chart21[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart21_MouseDoubleClick);
                chart21[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart21_MouseClick);
                chart21[jj].ContextMenuStrip = contextMenuStrip1;
                chart21[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart22[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart22_MouseUp);
                chart22[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart22_MouseDown);
                chart22[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart22_MouseMove);
                chart22[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart22_MouseDoubleClick);
                chart22[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart22_MouseClick);
                chart22[jj].ContextMenuStrip = contextMenuStrip1;
                chart22[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart23[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart23_MouseUp);
                chart23[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart23_MouseDown);
                chart23[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart23_MouseMove);
                chart23[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart23_MouseDoubleClick);
                chart23[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart23_MouseClick);
                chart23[jj].ContextMenuStrip = contextMenuStrip1;
                chart23[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart24[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart24_MouseUp);
                chart24[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart24_MouseDown);
                chart24[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart24_MouseMove);
                chart24[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart24_MouseDoubleClick);
                chart24[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart24_MouseClick);
                chart24[jj].ContextMenuStrip = contextMenuStrip1;
                chart24[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart25[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart25_MouseUp);
                chart25[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart25_MouseDown);
                chart25[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart25_MouseMove);
                chart25[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart25_MouseDoubleClick);
                chart25[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart25_MouseClick);
                chart25[jj].ContextMenuStrip = contextMenuStrip1;
                chart25[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart26[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart26_MouseUp);
                chart26[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart26_MouseDown);
                chart26[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart26_MouseMove);
                chart26[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart26_MouseDoubleClick);
                chart26[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart26_MouseClick);
                chart26[jj].ContextMenuStrip = contextMenuStrip1;
                chart26[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart27[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart27_MouseUp);
                chart27[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart27_MouseDown);
                chart27[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart27_MouseMove);
                chart27[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart27_MouseDoubleClick);
                chart27[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart27_MouseClick);
                chart27[jj].ContextMenuStrip = contextMenuStrip1;
                chart27[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart28[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart28_MouseUp);
                chart28[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart28_MouseDown);
                chart28[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart28_MouseMove);
                chart28[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart28_MouseDoubleClick);
                chart28[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart28_MouseClick);
                chart28[jj].ContextMenuStrip = contextMenuStrip1;
                chart28[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart29[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart29_MouseUp);
                chart29[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart29_MouseDown);
                chart29[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart29_MouseMove);
                chart29[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart29_MouseDoubleClick);
                chart29[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart29_MouseClick);
                chart29[jj].ContextMenuStrip = contextMenuStrip1;
                chart29[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart30[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart30_MouseUp);
                chart30[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart30_MouseDown);
                chart30[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart30_MouseMove);
                chart30[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart30_MouseDoubleClick);
                chart30[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart30_MouseClick);
                chart30[jj].ContextMenuStrip = contextMenuStrip1;
                chart30[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart31[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart31_MouseUp);
                chart31[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart31_MouseDown);
                chart31[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart31_MouseMove);
                chart31[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart31_MouseDoubleClick);
                chart31[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart31_MouseClick);
                chart31[jj].ContextMenuStrip = contextMenuStrip1;
                chart31[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart32[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart32_MouseUp);
                chart32[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart32_MouseDown);
                chart32[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart32_MouseMove);
                chart32[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart32_MouseDoubleClick);
                chart32[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart32_MouseClick);
                chart32[jj].ContextMenuStrip = contextMenuStrip1;
                chart32[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart33[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart33_MouseUp);
                chart33[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart33_MouseDown);
                chart33[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart33_MouseMove);
                chart33[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart33_MouseDoubleClick);
                chart33[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart33_MouseClick);
                chart33[jj].ContextMenuStrip = contextMenuStrip1;
                chart33[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart34[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart34_MouseUp);
                chart34[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart34_MouseDown);
                chart34[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart34_MouseMove);
                chart34[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart34_MouseDoubleClick);
                chart34[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart34_MouseClick);
                chart34[jj].ContextMenuStrip = contextMenuStrip1;
                chart34[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart35[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart35_MouseUp);
                chart35[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart35_MouseDown);
                chart35[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart35_MouseMove);
                chart35[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart35_MouseDoubleClick);
                chart35[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart35_MouseClick);
                chart35[jj].ContextMenuStrip = contextMenuStrip1;
                chart35[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart36[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart36_MouseUp);
                chart36[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart36_MouseDown);
                chart36[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart36_MouseMove);
                chart36[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart36_MouseDoubleClick);
                chart36[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart36_MouseClick);
                chart36[jj].ContextMenuStrip = contextMenuStrip1;
                chart36[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart37[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart37_MouseUp);
                chart37[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart37_MouseDown);
                chart37[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart37_MouseMove);
                chart37[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart37_MouseDoubleClick);
                chart37[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart37_MouseClick);
                chart37[jj].ContextMenuStrip = contextMenuStrip1;
                chart37[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion


            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart38[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart38_MouseUp);
                chart38[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart38_MouseDown);
                chart38[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart38_MouseMove);
                chart38[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart38_MouseDoubleClick);
                chart38[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart38_MouseClick);
                chart38[jj].ContextMenuStrip = contextMenuStrip1;
                chart38[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart39[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart39_MouseUp);
                chart39[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart39_MouseDown);
                chart39[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart39_MouseMove);
                chart39[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart39_MouseDoubleClick);
                chart39[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart39_MouseClick);
                chart39[jj].ContextMenuStrip = contextMenuStrip1;
                chart39[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart40[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart40_MouseUp);
                chart40[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart40_MouseDown);
                chart40[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart40_MouseMove);
                chart40[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart40_MouseDoubleClick);
                chart40[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart40_MouseClick);
                chart40[jj].ContextMenuStrip = contextMenuStrip1;
                chart40[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart42[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart42_MouseUp);
                chart42[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart42_MouseDown);
                chart42[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart42_MouseMove);
                chart42[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart42_MouseDoubleClick);
                chart42[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart42_MouseClick);
                chart42[jj].ContextMenuStrip = contextMenuStrip1;
                chart42[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chartDjAndXsAndSckf_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart41[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart41_MouseUp);
                chart41[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart41_MouseDown);
                chart41[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart41_MouseMove);
                chart41[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart41_MouseDoubleClick);
                chart41[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart41_MouseClick);
                chart41[jj].ContextMenuStrip = contextMenuStrip1;
                chart41[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chartDjAndXsAndSckf_GetToolTipText);
            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart43[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart43_MouseUp);
                chart43[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart43_MouseDown);
                chart43[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart43_MouseMove);
                chart43[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart43_MouseDoubleClick);
                chart43[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart43_MouseClick);
                chart43[jj].ContextMenuStrip = contextMenuStrip1;
                chart43[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chartDjAndXsAndSckf_GetToolTipText);

            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            for (int jj = 0; jj < NUM; jj++)
            {
                chart44[jj].MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart44_MouseUp);
                chart44[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart44_MouseDown);
                chart44[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart44_MouseMove);
                chart44[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart44_MouseDoubleClick);
                chart44[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart44_MouseClick);
                chart44[jj].ContextMenuStrip = contextMenuStrip1;
                chart44[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chartDjAndXsAndSckf_GetToolTipText);

            }
            #endregion

            #region 为每个相通类型的堆积条形图定义Mouse_Event
            //for (int jj = 0; jj < NUM; jj++)
            //{
            //    chart43[jj].MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart45_MouseDown);
            //    chart43[jj].MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart45_MouseMove);
            //    chart43[jj].MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.chart45_MouseDoubleClick);
            //    chart43[jj].MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart45_MouseClick);
            //    chart43[jj].ContextMenuStrip = contextMenuStrip1;
            //    chart43[jj].GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chartDjAndXsAndSckf_GetToolTipText);

            //}
            #endregion

            #endregion



        }

        void c_MouseUp(object sender, MouseEventArgs e)
        {
                this.toolStrip1.Visible = false;
        }

        #region 连接数据库
        private Method method;
        private IStatus iStatus;
        /// <summary>
        /// panel滚轮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///  #region 接口IFlow

        /////实现接口IFlow		
        /// <summary>
        /// 接口类别
        /// </summary>
        public IStatus IStatus
        {
            get
            {
                return this.iStatus;
            }
            set
            {
                iStatus = value;
            }
        }


        /// <summary>
        ///是否破解 
        /// </summary>
        public bool IsDecode
        {
            get
            {
                return true;
            }
        }


        public Method Method
        {
            get
            {
                return method;
            }
            set
            {
                method = value;
            }
        }

        public Plytmf.Net.Bottom.Module Module
        {
            get
            {
                return null;
            }
            set
            {
                ;
            }
        }

        public bool Run(bool showWindow)
        {
            if (showWindow)
            {
                this.Show();
            }
            return true;
        }

        /// <summary>
        /// 当前流程是否用到目标井
        /// </summary>
        /// <param name="well">目标井</param>
        /// <returns></returns>
        public bool ContainWell(Well well)
        {
            return false;
        }

        public bool Quit()
        {
            this.Hide();
            return true;
        }

        #endregion

        private void panel1_mousewheel(object sender, MouseEventArgs e)
        {
            //获取光标位置
            Point mousePoint = new Point(e.X, e.Y);
            //换算成相对本窗体的位置
            mousePoint.Offset(this.Location.X, this.Location.Y);
            //判断是否在panel内
            if (panel1.RectangleToScreen(panel1.DisplayRectangle).Contains(mousePoint))
            { 
                //滚动
                panel1.AutoScrollPosition = new Point(0, panel1.VerticalScroll.Value - e.Delta);
            }
        }

        #region 声明Chart数组

        int[] gap = new int[NUM];//gap[i]的值代表同样的图被点击了几次,gap[0]代表簇状柱形图被点击了几次。gap[1]代表堆积柱形图被点击了几次，以此类推
        //chart创建为数组是因为可能不单单点击一次同样类型的柱形图，比如它可能连续点击多次簇状柱形图，或者多次堆积柱形图
        Chart[] chart1 = new Chart[NUM];//chart1是簇状柱形图
        Chart[] chart2 = new Chart[NUM];//chart2是堆积柱形图
        Chart[] chart3 = new Chart[NUM];//chart3是百分比堆积柱形图
        Chart[] chart4 = new Chart[NUM];//chart4是三维簇状柱形图
        Chart[] chart5 = new Chart[NUM];//chart5是三维堆积柱形图
        Chart[] chart6 = new Chart[NUM];//chart6是三维百分比堆积柱形图
        Chart[] chart7 = new Chart[NUM];//chart7是三维柱形
        Chart[] chart8 = new Chart[NUM];//chart8是簇状圆柱
        Chart[] chart9 = new Chart[NUM];//chart9是堆积圆柱
        Chart[] chart10 = new Chart[NUM];//chart10是百分比堆积柱形图
        Chart[] chart11 = new Chart[NUM];//chart11三维圆柱形图
        Chart[] chart12 = new Chart[NUM];//chart12簇状条形图
        Chart[] chart13 = new Chart[NUM];//chart13堆积条形图
        Chart[] chart14 = new Chart[NUM];//chart14百分比堆积条形图
        Chart[] chart15 = new Chart[NUM];//chart15三维簇状条形图
        Chart[] chart16 = new Chart[NUM];//chart16三维堆积条形图
        Chart[] chart17 = new Chart[NUM];//chart17三维百分比堆积条形图
        Chart[] chart18 = new Chart[NUM];//chart18簇状水平圆柱图
        Chart[] chart19 = new Chart[NUM];//chart19堆积水平圆柱图
        Chart[] chart20 = new Chart[NUM];//chart20百分比堆积水平圆柱图
        Chart[] chart21 = new Chart[NUM];//chart21饼图
        Chart[] chart22 = new Chart[NUM];//chart22分离型饼图
        Chart[] chart23 = new Chart[NUM];//chart23三维饼图
        Chart[] chart24 = new Chart[NUM];//chart24三维分离型饼图
        Chart[] chart25 = new Chart[NUM];//chart25圆环图
        Chart[] chart26 = new Chart[NUM];//chart26三维分离型饼图
        Chart[] chart27 = new Chart[NUM];//chart26三维分离型饼图
        Chart[] chart28 = new Chart[NUM];//chart26三维分离型饼图
        Chart[] chart29 = new Chart[NUM];//chart26三维分离型饼图
        Chart[] chart30 = new Chart[NUM];//chart26三维分离型饼图
        Chart[] chart31 = new Chart[NUM];//chart26三维分离型饼图
        Chart[] chart32 = new Chart[NUM];//chart26三维分离型饼图
        Chart[] chart33 = new Chart[NUM];//chart33折线图
        Chart[] chart34 = new Chart[NUM];//chart34带数据标记的折线图
        Chart[] chart35 = new Chart[NUM];//chart26三维折线图
        Chart[] chart36 = new Chart[NUM];//chart36仅带数据标记的散点图
        Chart[] chart37 = new Chart[NUM];//chart37带平滑线和数据标记的散点图
        Chart[] chart38 = new Chart[NUM];//chart38带平滑线散点图
        Chart[] chart39 = new Chart[NUM];//chart39带直线和数据标记的散点图
        Chart[] chart40 = new Chart[NUM];//chart40带直线散点图

        Chart[] chart41 = new Chart[NUM];//chart41代表递减曲线

        Chart[] chart42 = new Chart[NUM];//chart42代表相渗曲线

        Chart[] chart43 = new Chart[NUM];//chart43代表生产开发曲线
        Chart[] chart44 = new Chart[NUM];//chart44代表注水开发曲线
        Chart[] chart45 = new Chart[NUM];//chart45代表油井开发曲线

        #endregion

        public void MyDelegate(Form frm)
        {
            frm.MouseDown += new MouseEventHandler(this.panel1_MouseDown);
            frm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
        }  //给窗体移动等写的委托（三端元）


        #region 初始化Chart图的一些值，比如初始位置，初始大小
        public const int NUM = 100;//定义每个相同的chart表可以有100个
        public int initPointX = 0;//初始的chart表的横坐标
        public int initPointY = 200;//初始的chart表的纵坐标
        public int initHeight = 400;//初始的chart表的高度
        public int initWidth = 400;//初始的chart表的宽
        public int pianyiliang = 0;//当两个相同的表出现时，后一个偏移这么多免得被挡住
        #endregion

        #region 用于MouseEvent上的一些变量

        int id;//定义相同的chart表的id，用于区分相同的表，和gap[]功能一样，比如说有两个簇状柱形图，用于区分是哪一个
        Point[] startp = new Point[NUM];//起点坐标
        Point[] endp = new Point[NUM];//终点坐标,用于移动图,在Mouse_Move事件中
        int MouseDownID;//这里为何要单独为MouseDown申请一个ID而不继续沿用MouseMove的ID呢？
        int r = 0;//代表鼠标点击的是哪一块，用变量r存储
        /*是这样的，如果沿用同一个ID,当鼠标点击了某一幅图后，准备进行放大缩小的操作时，恰巧鼠标从另一幅图中经过了(此时ID就变为鼠标经过的那个图的ID)，
         * 此时放大就不是点击的那幅图了而是MouseMove过的那幅图！
         */
        #endregion

        public int getPianyiliang()
        {
            Random r = new Random();
            pianyiliang = r.Next(100, 800);
            return pianyiliang;
        }

        //////////////////////////////////////////////////// 这是普通图////////////////////////////////////////////////////////////
        #region 普通图40幅

        //实现鼠标右键chart图 弹出工具栏
        //记录当前界面 为撤销恢复所用
        //为所有chart图使用
        private void chart_MouseUp(object sender, MouseEventArgs e, Chart[] chart, ref int id)
        {
            //MessageBox.Show(MyObject.My_Chart1.Series.Count.ToString());
            //调用设置属性的窗体并且把要设置的对象传过去！！！
            Control ctrl = (Control)sender;
            id = (int)ctrl.Tag;//获得了鼠标点击的那个chart表的id
            MyObject.My_Chart1 = chart[id];//把鼠标点击的那个chart表传过去
            //只让当前的图显示在最上方
            chart[id].BringToFront();
            //只让当前的图边框显示出来
            ToOperateTheChart.ChangeTheChartBoder(MyObject.My_Chart1, Color.WhiteSmoke, BorderSkinStyle.FrameThin1);
            HitTestResult result = MyObject.My_Chart1.HitTest(e.X, e.Y);  //result存储的是每次单击的图标元素（如果有）
            int x = e.X + MyObject.My_Chart1.Location.X;
            int y = e.Y + MyObject.My_Chart1.Location.Y;
            if (e.Button == MouseButtons.Right)
            {
                if (result.ChartElementType == ChartElementType.LegendItem)
                {
                    toolStripComboBox1.Enabled = true;
                    toolStripComboBox1.Text = "图例区";
                    toolStripButton2.Enabled = true;//字体
                    toolStripButton3.Enabled = true;//字体颜色
                    ToOperateTheChart.ChangeLegendItemBoder(MyObject.My_Chart1, Color.Red);
                }
                else if (result.ChartElementType == ChartElementType.PlottingArea)
                {
                    toolStripComboBox1.Enabled = true;
                    toolStripComboBox1.Text = "绘图区";
                    toolStripButton2.Enabled = false;//字体
                    toolStripButton3.Enabled = false;//字体颜色
                    ToOperateTheChart.ChangePlottingAreaBoder(MyObject.My_Chart1, Color.Red);
                }
                else if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    toolStripComboBox1.Enabled = true;
                    for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                    {
                        if (result.Series == MyObject.My_Chart1.Series[i])
                        {
                            toolStripComboBox1.Text = "序列" + (i + 1);
                            toolStripButton2.Enabled = true;//字体
                            toolStripButton3.Enabled = true;//字体颜色
                            MyObject.My_Chart1.Series[i].ShadowColor = Color.Red;
                            MyObject.My_Chart1.Series[i].ShadowOffset = 5;
                        }
                    }

                }
                else if (result.ChartElementType == ChartElementType.AxisLabels)
                {
                    toolStripComboBox1.Enabled = true;
                    for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                    {
                        if (result.Axis == MyObject.My_Chart1.ChartAreas[i].AxisY)
                        {
                            toolStripComboBox1.Text = "垂直轴区";
                            toolStripButton2.Enabled = true;//字体
                            toolStripButton3.Enabled = true;//字体颜色
                        }
                        else if (result.Axis == MyObject.My_Chart1.ChartAreas[0].AxisX)
                        {
                            toolStripComboBox1.Text = "水平轴区";
                            toolStripButton2.Enabled = true;//字体
                            toolStripButton3.Enabled = true;//字体颜色
                        }
                    }
                }
                else if (result.ChartElementType == ChartElementType.Annotation)
                {
                    toolStripComboBox1.Text = "文本区";
                    toolStripComboBox1.Enabled = false;
                    toolStripButton2.Enabled = true;//字体
                    toolStripButton3.Enabled = true;//字体颜色
                }
                else
                {
                    toolStripComboBox1.Enabled = true;
                    toolStripComboBox1.Text = "图表区";
                    toolStripButton2.Enabled = true;//字体
                    toolStripButton3.Enabled = true;//字体颜色
                }

                if (this.toolStrip1.Visible == false)
                {
                    this.toolStrip1.Visible = true;
                }
                if (!this.panel1.Controls.Contains(toolStrip1))
                {
                    this.panel1.Controls.Add(toolStrip1);
                }

                if ((this.panel1.Size.Height - y) < this.contextMenuStrip1.Size.Height)
                {
                    int y1 = this.contextMenuStrip1.Size.Height - (this.panel1.Size.Height - y);
                    this.toolStrip1.Location = new Point(x, y - 25 - y1);
                }
                else
                {
                    this.toolStrip1.Location = new Point(e.X + MyObject.My_Chart1.Location.X, e.Y + MyObject.My_Chart1.Location.Y - 25);
                }
                this.toolStrip1.BringToFront();
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (result.ChartElementType == ChartElementType.Title)//图题区域
                {
                    ToOperateTheChart.ChangeTheChartTitleBoder(MyObject.My_Chart1, Color.Black, ChartDashStyle.Solid);
                    MyObject.MyTitle1 = chart[id].Titles[0];
                }
                else if (result.ChartElementType == ChartElementType.LegendItem)
                {
                    comboBox1.Enabled = true;
                    comboBox1.Text = "图例区";
                    button1.Enabled = true;//字体
                    button2.Enabled = true;//字体颜色
                    ToOperateTheChart.ChangeLegendItemBoder(MyObject.My_Chart1, Color.Red);
                }
                else if (result.ChartElementType == ChartElementType.PlottingArea)
                {
                    comboBox1.Enabled = true;
                    comboBox1.Text = "绘图区";
                    button1.Enabled = false;//字体
                    button2.Enabled = false;//字体颜色
                    ToOperateTheChart.ChangePlottingAreaBoder(MyObject.My_Chart1, Color.Red);
                }
                else if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    comboBox1.Enabled = true;
                    for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                    {
                        if (result.Series == MyObject.My_Chart1.Series[i])
                        {
                            comboBox1.Text = "序列" + (i + 1);
                            button1.Enabled = true;//字体
                            button2.Enabled = true;//字体颜色
                            MyObject.My_Chart1.Series[i].ShadowColor = Color.Red;
                            MyObject.My_Chart1.Series[i].ShadowOffset = 5;
                        }
                    }

                }
                else if (result.ChartElementType == ChartElementType.AxisLabels)
                {
                    comboBox1.Enabled = true;
                    for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                    {
                        if (result.Axis == MyObject.My_Chart1.ChartAreas[i].AxisY)
                        {
                            comboBox1.Text = "垂直轴区";
                            button1.Enabled = true;//字体
                            button2.Enabled = true;//字体颜色
                        }
                        else if (result.Axis == MyObject.My_Chart1.ChartAreas[0].AxisX)
                        {
                            comboBox1.Text = "水平轴区";
                            button1.Enabled = true;//字体
                            button2.Enabled = true;//字体颜色
                        }
                    }
                }
                else if (result.ChartElementType == ChartElementType.Annotation)
                {
                    comboBox1.Text = "文本区";
                    comboBox1.Enabled = false;
                    button1.Enabled = true;//字体
                    button2.Enabled = true;//字体颜色
                }
                else
                {
                    comboBox1.Enabled = true;
                    comboBox1.Text = "图表区";
                    button1.Enabled = true;//字体
                    button2.Enabled = true;//字体颜色
                    //ToOperateTheChart.ChangeTheChartBoder(MyObject.My_Chart1, Color.WhiteSmoke, BorderSkinStyle.FrameThin1);
                }
            }
            getMsg();
        }


        #region 簇状柱形图的相关内容
        //当簇状柱形图菜单栏被点击时调用画簇状柱形图的函数 
        private void cuzhuangzhuxing_Click(object sender, EventArgs e)
        {
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_Cuzhuangzhuxingtu(chart1, gap[0], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中

            this.panel1.Controls.Add(chart1[gap[0]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart1[gap[0]];//将画出来的图直接表示成选中状态 便于直接读取数据

            chart1[gap[0]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[0]++;//当同种类型的图要画第二次时，让Chart1数组的[index]加1
        }


        //当双击某幅图的时候，会弹出属性的对话窗口来修改图的属性
        private void chart1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }

            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }

            MouseEvent.MyMouseDoubleClick(sender, e, chart1, ref id, ref r);
        }

        //当单击某幅图的DataPoint时修改图的颜色，单击图题时修改图题的字体
        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {

                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart1, ref id, ref r);
            this.toolStrip1.Visible = false;
        }

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart1, ref id);
        }

        //记录图形移动前的位置
        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            MouseEvent.MyMouseDown(sender, e, chart1, ref MouseDownID, ref id, ref r, ref startp);
        }

        //记录图形移动后的位置，实现移动图片的效果
        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart1, ref id, ref startp, ref endp);
        }

        #endregion

        #region 堆积柱形图的相关操作


        private void duijizhuxing_Click(object sender, EventArgs e)
        {

            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_Duijizhuxingtu(chart2, gap[1], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart2[gap[1]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart2[gap[1]];//将画出来的图直接表示成选中状态 便于直接读取数据

            chart2[gap[1]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[1]++;//当同种类型的图要画第二次时，让Chart1数组的[index]加1
        }

        private void chart2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart2, ref id, ref r);
        }

        private void chart2_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart2, ref id, ref r);
            this.toolStrip1.Visible = false;
        }

        private void chart2_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart2, ref id);
        }

        private void chart2_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart2, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart2_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart2, ref id, ref startp, ref endp);
        }

        #endregion

        #region 堆积百分比的相关操作

        private void baifenbiduijizhuxing_Click(object sender, EventArgs e)
        {

            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_baifenbiduijizhuxing(chart3, gap[2], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart3[gap[2]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart3[gap[2]];//将画出来的图直接表示成选中状态 便于直接读取数据

            chart3[gap[2]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[2]++;//当同种类型的图要画第二次时，让Chart1数组的[index]加1
        }

        private void chart3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据


                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart3, ref id, ref r);
        }

        private void chart3_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart3, ref id);
        }

        private void chart3_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart3, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart3_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart3, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart3_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart3, ref id, ref startp, ref endp);
        }

        #endregion

        #region 三维簇状柱形图

        private void sanweicuzhuangzhuxing_Click(object sender, EventArgs e)
        {

            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_sanweicuzhuangzhuxing(chart4, gap[3], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart4[gap[3]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart4[gap[3]];//将画出来的图直接表示成选中状态 便于直接读取数据

            chart4[gap[3]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[3]++;//当同种类型的图要画第二次时，让Chart1数组的[index]加1
        }

        private void chart4_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart4, ref id);
        }

        private void chart4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart4, ref id, ref r);
        }

        private void chart4_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart4, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart4_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart4, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart4_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart4, ref id, ref startp, ref endp);
        }

        #endregion

        #region 三维堆积柱形图

        private void sanweiduijizhuxing_Click(object sender, EventArgs e)
        {

            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_sanweiduijizhuxing(chart5, gap[4], initPointX + pianyiliang, initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart5[gap[4]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart5[gap[4]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart5[gap[4]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[4]++;//当同种类型的图要画第二次时，让Chart1数组的[index]加1
        }

        private void chart5_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart5, ref id);
        }

        private void chart5_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart5, ref id, ref r);
        }

        private void chart5_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart5, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }

        }

        private void chart5_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart5, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart5_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart5, ref id, ref startp, ref endp);
        }

        #endregion

        #region 三维百分比堆积柱形图

        private void sanweibaifenbiduijizhuxing_Click(object sender, EventArgs e)
        {

            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_sanweibaifenbiduijizhuxing(chart6, gap[5], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart6[gap[5]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart6[gap[5]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart6[gap[5]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[5]++;//当同种类型的图要画第二次时，让Chart1数组的[index]加1
        }

        private void chart6_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart6, ref id);
        }

        private void chart6_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart6, ref id, ref r);
        }

        private void chart6_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart6, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart6_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart6, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart6_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart6, ref id, ref startp, ref endp);
        }

        #endregion

        #region 三维柱形图

        private void sanweizhuxing_Click(object sender, EventArgs e)
        {

            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_sanweizhuxing(chart7, gap[6], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart7[gap[6]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart7[gap[6]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart7[gap[6]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[6]++;//当同种类型的图要画第二次时，让Chart7数组的[index]加1
        }

        private void chart7_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart7, ref id);
        }

        private void chart7_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart7, ref id, ref r);
        }

        private void chart7_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart7, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart7_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart7, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart7_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart7, ref id, ref startp, ref endp);
        }

        #endregion

        #region 簇状圆柱图

        private void cuzhuangyuanzhu_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_cuzhuangyuanzhu(chart8, gap[7], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart8[gap[7]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart8[gap[7]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart8[gap[7]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[7]++;//当同种类型的图要画第二次时，让Chart7数组的[index]加1
        }

        private void chart8_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart8, ref id);
        }

        private void chart8_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart8, ref id, ref r);
        }

        private void chart8_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart8, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart8_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart8, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart8_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart8, ref id, ref startp, ref endp);
        }

        #endregion

        #region 堆积圆柱图

        private void duijiyuanzhu_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了

            DrawChart dc = new DrawChart();
            dc.Draw_duijiyuanzhu(chart9, gap[8], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart9[gap[8]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart9[gap[8]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart9[gap[8]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[8]++;//当同种类型的图要画第二次时，让Chart7数组的[index]加1
        }

        private void chart9_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart9, ref id);
        }

        private void chart9_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart9, ref id, ref r);
        }

        private void chart9_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart9, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart9_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart9, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart9_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart9, ref id, ref startp, ref endp);
        }


        #endregion

        #region 百分比堆积圆柱图

        private void baifenbiduijiyuanzhu_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_baifenbiduijiyuanzhu(chart10, gap[9], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart10[gap[9]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart10[gap[9]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart10[gap[9]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[9]++;//当同种类型的图要画第二次时，让Chart7数组的[index]加1
        }

        private void chart10_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart10, ref id);
        }

        private void chart10_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart10, ref id, ref r);
        }

        private void chart10_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart10, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart10_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart10, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart10_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart10, ref id, ref startp, ref endp);
        }


        #endregion

        #region 三维圆柱图

        private void sanweiyuanzhu_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_sanweiyuanzhu(chart11, gap[10], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart11[gap[10]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart11[gap[10]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart11[gap[10]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[10]++;//当同种类型的图要画第二次时，让Chart7数组的[index]加1
        }

        private void chart11_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart11, ref id);
        }

        private void chart11_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart11, ref id, ref r);
        }

        private void chart11_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart11, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }
        private void chart11_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            MouseEvent.MyMouseDown(sender, e, chart11, ref MouseDownID, ref id, ref r, ref startp);
        }
        private void chart11_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart11, ref id, ref startp, ref endp);
        }


        #endregion



        #region 簇状条形图

        private void cuzhuangtiaoxing_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_cuzhuangtiaoxing(chart12, gap[11], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart12[gap[11]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart12[gap[11]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart12[gap[11]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[11]++;//当同种类型的图要画第二次时，让Chart7数组的[index]加1
        }

        private void chart12_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart12, ref id);
        }

        private void chart12_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart12, ref id, ref r);
        }

        private void chart12_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart12, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart12_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart12, ref MouseDownID, ref id, ref r, ref startp);
        }
        private void chart12_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart12, ref id, ref startp, ref endp);
        }
        #endregion

        #region 堆积条形图
        private void duijitiaoxing_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_duijitiaoxing(chart13, gap[12], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart13[gap[12]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart13[gap[12]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart13[gap[12]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[12]++;//当同种类型的图要画第二次时，让Chart7数组的[index]加1
        }

        private void chart13_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart13, ref id);
        }

        private void chart13_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart13, ref id, ref r);
        }

        private void chart13_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart13, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart13_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart13, ref MouseDownID, ref id, ref r, ref startp);
        }
        private void chart13_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart13, ref id, ref startp, ref endp);
        }

        #endregion

        #region 百分比堆积条形图

        private void baifenbiduijitiaoxing_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_baifenbiduijitiaoxing(chart14, gap[13], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart14[gap[13]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart14[gap[13]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart14[gap[13]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[13]++;//当同种类型的图要画第二次时，让Chart14数组的[index]加1
        }

        private void chart14_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart14, ref id);
        }

        private void chart14_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart14, ref id, ref r);
        }

        private void chart14_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart14, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart14_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart14, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart14_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart14, ref id, ref startp, ref endp);
        }


        #endregion

        #region 三维簇状条形图

        private void sanweicuzhuangtiaoxing_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_sanweicuzhuangtiaoxing(chart15, gap[14], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart15[gap[14]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart15[gap[14]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart15[gap[14]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[14]++;//当同种类型的图要画第二次时，让Chart14数组的[index]加1
        }

        private void chart15_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart15, ref id);
        }

        private void chart15_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            //调用设置属性的窗体并且把要设置的对象传过去！！！
            MouseEvent.MyMouseDoubleClick(sender, e, chart11, ref id, ref r);
        }

        private void chart15_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart15, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart15_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart15, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart15_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart15, ref id, ref startp, ref endp);
        }


        #endregion

        #region 三维堆积条形图
        private void sanweiduijitiaoxing_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_sanweiduijitiaoxing(chart16, gap[15], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart16[gap[15]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart16[gap[15]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart16[gap[15]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[15]++;//当同种类型的图要画第二次时，让Chart14数组的[index]加1
        }

        private void chart16_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart16, ref id);
        }

        private void chart16_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart16, ref id, ref r);
        }

        private void chart16_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart16, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart16_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart16, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart16_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart16, ref id, ref startp, ref endp);
        }

        #endregion

        #region 三维百分比堆积条形图
        private void sanweibaifenbiduijitiaoxing_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_sanweibaifenbiduijitiaoxing(chart17, gap[16], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart17[gap[16]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart17[gap[16]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart17[gap[16]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[16]++;//当同种类型的图要画第二次时，让Chart14数组的[index]加1
        }

        private void chart17_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart17, ref id);
        }

        private void chart17_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart17, ref id, ref r);
        }

        private void chart17_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart17, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }

        }

        private void chart17_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart17, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart17_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart17, ref id, ref startp, ref endp);
        }
        #endregion

        #region 簇状水平圆柱图

        private void cuzhuangshuipingyuanzhu_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_cuzhuangshuipingyuanzhu(chart18, gap[17], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart18[gap[17]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart18[gap[17]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart18[gap[17]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[17]++;//当同种类型的图要画第二次时，让Chart14数组的[index]加1
        }

        private void chart18_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart18, ref id);
        }

        private void chart18_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart18, ref id, ref r);
        }

        private void chart18_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart18, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }

        }

        private void chart18_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart18, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart18_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart18, ref id, ref startp, ref endp);
        }


        #endregion

        #region 堆积水平圆柱图

        private void duijishuipingyuanzhu_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_duijishuipingyuanzhu(chart19, gap[18], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart19[gap[18]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart19[gap[18]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart19[gap[18]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[18]++;//当同种类型的图要画第二次时，让Chart14数组的[index]加1
        }

        private void chart19_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart19, ref id);
        }

        private void chart19_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart19, ref id, ref r);
        }

        private void chart19_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart19, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart19_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart19, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart19_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart19, ref id, ref startp, ref endp);
        }


        #endregion

        #region 百分比堆积水平圆柱图
        private void baifenbiduijishuipingyuanzhu_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_baifenbiduijishuipingyuanzhu(chart20, gap[19], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart20[gap[19]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart20[gap[19]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart20[gap[19]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[19]++;//当同种类型的图要画第二次时，让Chart14数组的[index]加1
        }

        private void chart20_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart20, ref id);
        }

        private void chart20_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart20, ref id, ref r);
        }

        private void chart20_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart20, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart20_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart20, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart20_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart20, ref id, ref startp, ref endp);
        }
        #endregion



        #region 利用一个Timer控件，每20ms让饼状图显示的角度+1，达到旋转的效果

        public static int angle = 0;//初始化chart表中饼状图显示角的值
        private void timer1_Tick(object sender, EventArgs e)//利用一个Timer控件，每20ms让饼状图显示的角度+1，达到旋转的效果
        {
            if (SaveAttribute.IsRotate == true)
            {

                angle += 1;//每20ms让饼状图显示的角度+1
                if (angle >= 360) //如果角度大于360°之后，复原
                {
                    angle = 0;
                }
                if (MyObject.My_Chart1 != null)
                {
                    MyObject.My_Chart1.Series[0]["PieStartAngle"] = angle.ToString();//chart表中饼状图显示角的值被赋值为angel的值
                }
            }
        }

        #endregion

        #region 饼图

        private void PieChart_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_PieChart(chart21, gap[20], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart21[gap[20]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart21[gap[20]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart21[gap[20]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[20]++;//当同种类型的图要画第二次时，让Chart21数组的[index]加1
        }

        private void chart21_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart21, ref id);
        }

        private void chart21_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart21, ref id, ref r);
        }

        private void chart21_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart21, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart21_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart21, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart21_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart21, ref id, ref startp, ref endp);
        }

        

        #endregion

        #region 分离形饼图

        private void DivPieChart_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_DivPieChart(chart22, gap[21], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart22[gap[21]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart22[gap[21]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart22[gap[21]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[21]++;//当同种类型的图要画第二次时，让Chart21数组的[index]加1
        }

        private void chart22_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart22, ref id);
        }

        private void chart22_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据
                
                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart22, ref id, ref r);
        }

        private void chart22_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart22, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart22_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart22, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart22_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart22, ref id, ref startp, ref endp);
        }


        #endregion

        #region 三维饼图
        private void buttonItem10_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_3DPieChart(chart23, gap[22], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart23[gap[22]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart23[gap[22]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart23[gap[22]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[22]++;//当同种类型的图要画第二次时，让Chart21数组的[index]加1
        }
        private void chart23_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart23, ref id);
        }
        private void chart23_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart23, ref id, ref r);
        }

        private void chart23_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart23, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart23_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart23, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart23_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart23, ref id, ref startp, ref endp);
        }
        #endregion

        #region 三维分离形饼图

        private void buttonItem11_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_3DDivPieChart(chart24, gap[23], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart24[gap[23]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart24[gap[23]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart24[gap[23]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[23]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart24_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart24, ref id);
        }
        private void chart24_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart24, ref id, ref r);
        }

        private void chart24_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart24, ref id, ref r);
        }

        private void chart24_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart24, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart24_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart24, ref id, ref startp, ref endp);
        }


        #endregion

        #region 圆环图

        private void RingChart_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_RingChart(chart25, gap[24], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart25[gap[24]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart25[gap[24]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart25[gap[24]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[24]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart25_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart25, ref id);
        }
        private void chart25_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart25, ref id, ref r);
        }

        private void chart25_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart25, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart25_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart25, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart25_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart25, ref id, ref startp, ref endp);
        }

        #endregion

        #region 分离圆环图

        private void DivRingChart_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_DivRingChart(chart26, gap[25], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart26[gap[25]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart26[gap[25]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart26[gap[25]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[25]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart26_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart26, ref id);
        }
        private void chart26_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart26, ref id, ref r);
        }

        private void chart26_MouseClick(object sender, MouseEventArgs e)
        {
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart26, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart26_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart26, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart26_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart26, ref id, ref startp, ref endp);
        }

        #endregion



        #region 面积图

        private void AreaChart_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_AreaChart(chart27, gap[26], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart27[gap[26]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart27[gap[26]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart27[gap[26]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[26]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart27_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart27, ref id);
        }
        private void chart27_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart27, ref id, ref r);
        }

        private void chart27_MouseClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart27, ref id, ref r);
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
        }

        private void chart27_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart27, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart27_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart27, ref id, ref startp, ref endp);
        }

        #endregion

        #region 堆积面积图

        private void StackAreaChart_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_StackAreaChart(chart28, gap[27], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart28[gap[27]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart28[gap[27]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart28[gap[27]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[27]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart28_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart28, ref id);
        }
        private void chart28_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart28, ref id, ref r);
        }

        private void chart28_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart28, ref id, ref r);
        }

        private void chart28_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart28, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart28_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart28, ref id, ref startp, ref endp);
        }


        #endregion

        #region 百分比堆积面积图

        private void baifenbiStackAreaChart_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_baifenbiStackAreaChart(chart29, gap[28], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart29[gap[28]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart29[gap[28]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart29[gap[28]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[28]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart29_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart29, ref id);
        }
        private void chart29_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart29, ref id, ref r);
        }

        private void chart29_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart29, ref id, ref r);
        }

        private void chart29_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart29, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart29_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart29, ref id, ref startp, ref endp);
        }
        #endregion

        #region 三维面积图


        private void sanweiAreaChart_Click(object sender, EventArgs e)
        {

            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_sanweiAreaChart(chart30, gap[29], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart30[gap[29]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart30[gap[29]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart30[gap[29]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[29]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart30_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart30, ref id);
        }
        private void chart30_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart30, ref id, ref r);
        }

        private void chart30_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart30, ref id, ref r);
        }

        private void chart30_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart30, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart30_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart30, ref id, ref startp, ref endp);
        }
        #endregion

        #region 三维堆积面积图
        private void sanweiStackAreaChart_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_sanweiStackAreaChart(chart31, gap[30], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart31[gap[30]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart31[gap[30]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart31[gap[30]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[30]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart31_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart31, ref id);
        }
        private void chart31_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart31, ref id, ref r);
        }

        private void chart31_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart31, ref id, ref r);
        }

        private void chart31_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart31, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart31_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart31, ref id, ref startp, ref endp);
        }
        #endregion

        #region 三维百分比堆积面积图
        private void sanweibaifenbiStackAreaChart_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_sanweibaifenbiStackAreaChart(chart32, gap[31], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart32[gap[31]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart32[gap[31]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart32[gap[31]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[31]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart32_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart32, ref id);
        }
        private void chart32_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart32, ref id, ref r);
        }

        private void chart32_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart32, ref id, ref r);
        }

        private void chart32_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart32, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart32_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart32, ref id, ref startp, ref endp);
        }
        #endregion



        #region 折线图
        private void zhexiantu_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_zhexiantu(chart33, gap[32], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart33[gap[32]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart33[gap[32]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart33[gap[32]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[32]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart33_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart33, ref id);
        }
        private void chart33_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart

            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart33, ref id, ref r);
        }

        private void chart33_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart33, ref id, ref r);
        }

        private void chart33_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart33, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart33_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart33, ref id, ref startp, ref endp);
        }
        #endregion

        #region 带数据标记的折线图

        private void daishujubiaojidezhexiantu_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_daishujubiaojidezhexiantu(chart34, gap[33], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart34[gap[33]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart34[gap[33]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart34[gap[33]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[33]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart34_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart34, ref id);
        }
        private void chart34_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart34, ref id, ref r);
        }

        private void chart34_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart34, ref id, ref r);
        }

        private void chart34_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart34, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart34_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart34, ref id, ref startp, ref endp);
        }


        #endregion

        #region 三维折线图

        private void sanweizhexiantu_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_sanweizhexiantu(chart35, gap[34], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart35[gap[34]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart35[gap[34]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart35[gap[34]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion

            gap[34]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }

        private void chart35_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart35, ref id);
        }
        private void chart35_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart35, ref id, ref r);
        }

        private void chart35_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart35, ref id, ref r);
        }

        private void chart35_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart35, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart35_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart35, ref id, ref startp, ref endp);
        }

        #endregion



        #region 仅带数据标记的散点图

        private void buttonItem12_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_jindaishujubiaojidesandiantu(chart36, gap[35], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart36[gap[35]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart36[gap[35]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart36[gap[35]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[35]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart36_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart36, ref id);
        }

        private void chart36_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart36, ref id, ref r);
        }

        private void chart36_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart36, ref id, ref r);
        }

        private void chart36_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart36, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart36_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart36, ref id, ref startp, ref endp);
        }

        #endregion

        #region 带平滑线和数据标记的散点图
        private void buttonItem13_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_daipinghuaxianheshujubiaojidesandiantu(chart37, gap[36], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart37[gap[36]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart37[gap[36]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart37[gap[36]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[36]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart37_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart37, ref id);
        }
        private void chart37_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart37, ref id, ref r);
        }

        private void chart37_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart37, ref id, ref r);
        }

        private void chart37_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart37, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart37_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart37, ref id, ref startp, ref endp);
        }

        #endregion

        #region 带平滑线散点图
        private void buttonItem15_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_daipinghuaxiansandiantu(chart38, gap[37], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart38[gap[37]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart38[gap[37]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart38[gap[37]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[37]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart38_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart38, ref id);
        }
        private void chart38_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart38, ref id, ref r);
        }

        private void chart38_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart38, ref id, ref r);
        }

        private void chart38_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart38, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart38_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart38, ref id, ref startp, ref endp);
        }
        #endregion

        #region 带直线和数据标记的散点图


        private void buttonItem16_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_daizhixianheshujubiaojidesandiantu(chart39, gap[38], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart39[gap[38]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart39[gap[38]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart39[gap[38]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[38]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart39_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart39, ref id);
        }
        private void chart39_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart39, ref id, ref r);
        }

        private void chart39_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart39, ref id, ref r);
        }

        private void chart39_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart39, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart39_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart39, ref id, ref startp, ref endp);
        }
        #endregion

        #region 带直线散点图
        private void buttonItem17_Click(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;//图出来之后代表读取数据的按钮可以点击了
            DrawChart dc = new DrawChart();
            dc.Draw_daizhixiansandiantu(chart40, gap[39], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart40[gap[39]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart40[gap[39]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart40[gap[39]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[39]++;//当同种类型的图要画第二次时，让Chart24数组的[index]加1
        }
        private void chart40_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart40, ref id);
        }
        private void chart40_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart40, ref id, ref r);
        }

        private void chart40_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart40, ref id, ref r);
        }

        private void chart40_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart40, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart40_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart40, ref id, ref startp, ref endp);
        }
        #endregion

        #endregion

        //为普通图写的当鼠标移动到普通图的某一块时有提示信息
        private void chart_GetToolTipText(object sender, System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs e)
        {

            // Check selected chart element and set tooltip text
            switch (e.HitTestResult.ChartElementType)
            {
                case ChartElementType.AxisLabels:
                    e.Text = "双击改变坐标轴字体的颜色";
                    break;

                case ChartElementType.DataPoint:
                    e.Text = "双击改变数据点的颜色";
                    break;

                case ChartElementType.Title:
                    e.Text = "双击改变标题的字体,也可在开始中改变标题颜色和字体";
                    break;
            }

        }
        /////////////////////////////////////////////////////普通图部分代码结束////////////////////////////////////////////////////////////



        ////////////////////////////////////////////////////// 这是三端元图/////////////////////////////////////////////////////////////////

        #region  三端元图9幅
        #region 含砾沉积物三角形分类图
        SanHanLiChenJiWu shlcj = new SanHanLiChenJiWu();
        private void buttonItem18_Click(object sender, EventArgs e)
        {
            SanHanLiChenJiWu frm = new SanHanLiChenJiWu();
            frm.TopLevel = false; //设置窗体为非顶级窗体
            frm.FormBorderStyle = FormBorderStyle.None;  //设置窗体没有边框
            frm.Location = new Point(initPointX, initPointY);
            this.panel1.Controls.Add(frm);
            MyDelegate(frm);
            frm.Show();
            ChangeAllBoder();
        }


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEvent.MyMouseDown(sender, e);
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEvent.MySanDuanYuanMouseMove(sender, e);
        }
        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        #endregion



        #region 无砾沉积物三角形分类图
        private void buttonItem19_Click(object sender, EventArgs e)
        {
            SanWuLiChenJiWu frm = new SanWuLiChenJiWu();
            frm.TopLevel = false; //设置窗体为非顶级窗体
            frm.FormBorderStyle = FormBorderStyle.None;  //设置窗体没有边框
            frm.Location = new Point(initPointX, initPointY);
            this.panel1.Controls.Add(frm);
            MyDelegate(frm);
            frm.Show();
            ChangeAllBoder();
        }

        #endregion


        #region 现代沉淀物Shepard分类图
        private void buttonItem20_Click(object sender, EventArgs e)
        {
            SanXianDaiChenDianWuShepard frm = new SanXianDaiChenDianWuShepard();
            frm.TopLevel = false; //设置窗体为非顶级窗体
            frm.FormBorderStyle = FormBorderStyle.None;  //设置窗体没有边框
            frm.Location = new Point(initPointX, initPointY);
            this.panel1.Controls.Add(frm);
            MyDelegate(frm);
            frm.Show();
            ChangeAllBoder();
        }
        #endregion


        #region 图版一图1
        private void buttonItem23_Click(object sender, EventArgs e)
        {
            SanYanShiSanDuanYuan1 frm = new SanYanShiSanDuanYuan1();
            frm.TopLevel = false; //设置窗体为非顶级窗体
            frm.FormBorderStyle = FormBorderStyle.None;  //设置窗体没有边框
            frm.Location = new Point(initPointX, initPointY);
            this.panel1.Controls.Add(frm);
            MyDelegate(frm);
            frm.Show();
            ChangeAllBoder();
        }
        #endregion

        #region 图版一图2
        private void buttonItem24_Click(object sender, EventArgs e)
        {
            SanYanShiSanDuanYuan2 frm = new SanYanShiSanDuanYuan2();
            frm.TopLevel = false; //设置窗体为非顶级窗体
            frm.FormBorderStyle = FormBorderStyle.None;  //设置窗体没有边框
            frm.Location = new Point(initPointX, initPointY);
            this.panel1.Controls.Add(frm);
            MyDelegate(frm);
            frm.Show();
            ChangeAllBoder();
        }
        #endregion


        #region 图版一图3
        private void buttonItem25_Click(object sender, EventArgs e)
        {
            SanYanShiSanDuanYuan3 frm = new SanYanShiSanDuanYuan3();
            frm.TopLevel = false; //设置窗体为非顶级窗体
            frm.FormBorderStyle = FormBorderStyle.None;  //设置窗体没有边框
            frm.Location = new Point(initPointX, initPointY);
            this.panel1.Controls.Add(frm);
            MyDelegate(frm);
            frm.Show();
            ChangeAllBoder();
        }
        #endregion

        #region 图版二图1
        private void buttonItem28_Click(object sender, EventArgs e)
        {
            SanYanShiFenLeiTwo1 frm = new SanYanShiFenLeiTwo1();
            frm.TopLevel = false; //设置窗体为非顶级窗体
            frm.FormBorderStyle = FormBorderStyle.None;  //设置窗体没有边框
            frm.Location = new Point(initPointX, initPointY);
            this.panel1.Controls.Add(frm);
            MyDelegate(frm);
            frm.Show();
            ChangeAllBoder();
        }
        #endregion

        #region 图版二图2
        private void buttonItem29_Click(object sender, EventArgs e)
        {
            SanYanShiFenLeiTwo2 frm = new SanYanShiFenLeiTwo2();
            frm.TopLevel = false; //设置窗体为非顶级窗体
            frm.FormBorderStyle = FormBorderStyle.None;  //设置窗体没有边框
            frm.Location = new Point(initPointX, initPointY);
            this.panel1.Controls.Add(frm);
            MyDelegate(frm);
            frm.Show();
            ChangeAllBoder();
        }
        #endregion


        #region 图版二图3
        private void buttonItem30_Click(object sender, EventArgs e)
        {
            SanYanShiFenLeiTwo3 frm = new SanYanShiFenLeiTwo3();
            frm.TopLevel = false; //设置窗体为非顶级窗体
            frm.FormBorderStyle = FormBorderStyle.None;  //设置窗体没有边框
            frm.Location = new Point(initPointX, initPointY);
            this.panel1.Controls.Add(frm);
            MyDelegate(frm);
            frm.Show();
            ChangeAllBoder();
        }
        #endregion
        #endregion

        //////////////////////////////////////////////////////////三端元图代码结束/////////////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////递减曲线/////////////////////////////////////////////////

        #region 递减曲线

        private void buttonX12_Click_1(object sender, EventArgs e)
        {
            this.buttonX9.Enabled = true;
            DrawChart dc = new DrawChart();
            dc.Draw_dijianquxian(chart41, gap[40], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart41[gap[40]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart41[gap[40]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart41[gap[40]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[40]++;//当同种类型的图要画第二次时，让Chart41数组的[index]加1

            //递减曲线 d = new 递减曲线();
            //d.Show();

        }
        private void chart41_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart41, ref id);
        }
        private void chart41_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart41, ref id, ref r);
        }

        private void chart41_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart41, ref id, ref r);
            this.toolStrip1.Visible = false;
            
        }

        private void chart41_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart41, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart41_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart41, ref id, ref startp, ref endp);
        }

        #endregion

        ///////////////////////////////////////////////////////////递减曲线结束/////////////////////////////////////////////////////////////


        ///////////////////////////////////////////////相渗曲线图//////////////////////////////////////////////////////////////////

        #region 相渗曲线
        private void buttonX13_Click(object sender, EventArgs e)
        {
            //相渗曲线图
            this.buttonX9.Enabled = true;
            DrawChart dc = new DrawChart();
            dc.Draw_xiangshenquxian(chart42, gap[41], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart42[gap[41]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart42[gap[41]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart42[gap[41]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[41]++;//当同种类型的图要画第二次时，让Chart41数组的[index]加1

        }
        private void chart42_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart42, ref id);
        }
        private void chart42_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart42, ref id, ref r);
        }

        private void chart42_MouseClick(object sender, MouseEventArgs e)
        {
            this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart42, ref id, ref r);
        }

        private void chart42_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart42, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart42_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart42, ref id, ref startp, ref endp);
        }

        #endregion


        ///////////////////////////////////////相渗曲线图结束//////////////////////////////////////////////////////////////////



        ////////////////////////////////////////生产开发曲线图//////////////////////////////////////////////////////////////////
        #region 生产开发
        private void buttonX14_Click(object sender, EventArgs e)
        {
            //画出生产开发曲线
            //相渗曲线图
            this.buttonX9.Enabled = true;
            DrawChart dc = new DrawChart();
            dc.Draw_shengchankaifaquxian(chart43, gap[42], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart43[gap[42]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart43[gap[42]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart43[gap[42]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[42]++;//当同种类型的图要画第二次时，让Chart41数组的[index]加1


        }

        //下面这是为递减曲线和相渗曲线和生产开发曲线图写的当鼠标移动到某一个部分，产生的提示信息
        private void chartDjAndXsAndSckf_GetToolTipText(object sender, System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs e)
        {

            // Check selected chart element and set tooltip text
            switch (e.HitTestResult.ChartElementType)
            {
                case ChartElementType.AxisLabels:
                    e.Text = "双击改变坐标轴字体的颜色";
                    break;

                case ChartElementType.Title:
                    e.Text = "双击改变标题的字体,也可在开始中改变标题颜色和字体";
                    break;
            }
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Name == "生产开发曲线")
            {
                for (int i = 0; i < MyObject.My_Chart1.Series[0].Points.Count; i++)
                {
                    MyObject.My_Chart1.Series[0].Points[i].ToolTip = MyObject.My_Chart1.ChartAreas[0].Name + "的第" + (i + 1) + "个数据点";
                }
                for (int i = 0; i < MyObject.My_Chart1.Series[1].Points.Count; i++)
                {
                    MyObject.My_Chart1.Series[1].Points[i].ToolTip = MyObject.My_Chart1.ChartAreas[1].Name + "的第" + (i + 1) + "个数据点";
                }
                for (int i = 0; i < MyObject.My_Chart1.Series[2].Points.Count; i++)
                {
                    MyObject.My_Chart1.Series[2].Points[i].ToolTip = MyObject.My_Chart1.ChartAreas[2].Name + "的第" + (i + 1) + "个数据点";
                }
            }

        }

        private void chart43_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart43, ref id);
        }

        private void chart43_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart43, ref id, ref r);
        }

        private void chart43_MouseClick(object sender, MouseEventArgs e)
        {
            //if (this.panel1.Controls.Contains(toolStrip1))
            //{
            this.toolStrip1.Visible = false;
            //}
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart43, ref id, ref r);
        }

        private void chart43_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart43, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart43_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart43, ref id, ref startp, ref endp);
        }
        #endregion


        ///////////////////////////////////////生产开发曲线图结束//////////////////////////////////////////////////////////////////


        ////////////////////////////////////////水井注水曲线图//////////////////////////////////////////////////////////////////
        #region 水井注水
        private void buttonItem38_Click(object sender, EventArgs e)
        {
            //画出水井注水曲线
            this.buttonX9.Enabled = true;
            DrawChart dc = new DrawChart();
            dc.Draw_shuijinzhushui(chart44, gap[43], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart44[gap[43]]);//将画出来的图添加到主界面中
            MyObject.My_Chart1 = chart44[gap[43]];//将画出来的图直接表示成选中状态 便于直接读取数据
            chart44[gap[43]].BringToFront();
            ChangeAllBoder();
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[43]++;//
        }

        private void chart44_MouseUp(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            chart_MouseUp(sender, e, chart44, ref id);
        }

        private void chart44_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Series[0].Points.Count == 0)
            {
                //如果是刚刚点出来的白色的图还没有数据的话
                buttonX9_Click(sender, e);//双击该图就是要先读取数据

                return;
            }
            MouseEvent.MyMouseDoubleClick(sender, e, chart44, ref id, ref r);
        }

        private void chart44_MouseClick(object sender, MouseEventArgs e)
        {
            //if (this.panel1.Controls.Contains(toolStrip1))
            //{
            this.toolStrip1.Visible = false;
            //}
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            //这里其实想实现一种图形被选中的效果，如果单击了这幅图，那么这幅图边框加粗显示，而其他的图边框置为白色（看不见）相当于没有选择的那种效果
            //ChangeAllBoder();
            MouseEvent.MyMouseClick(sender, e, chart44, ref id, ref r);
        }

        private void chart44_MouseDown(object sender, MouseEventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseDown(sender, e, chart44, ref MouseDownID, ref id, ref r, ref startp);
        }

        private void chart44_MouseMove(object sender, MouseEventArgs e)
        {
            //this.toolStrip1.Visible = false;
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (IsAnnotationSelect[i] == true)
                    {
                        //MessageBox.Show("第"+(i+1)+"个批注被点击");
                        return;
                    }

                }
            }//当批注被点击的时候，只移动和改变批注而不是改变和移动chart
            MouseEvent.MyMouseMove(sender, e, chart44, ref id, ref startp, ref endp);
        }
        #endregion
        ////////////////////////////////////////水井注水曲线图结束//////////////////////////////////////////////////////////////////








        /////////////////////////////////////////主界面上其他代码///////////////////////////////////////////////////////////////

        //先把几个属性改为默认
        public void ChangeToDefaultAttribute()
        {
            MyObject.My_Chart1.ChartAreas[0].Area3DStyle.Enable3D = false;
            for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
            {
                MyObject.My_Chart1.Series[i]["DrawingStyle"] = "Default";//绘图风格为浮雕型
                MyObject.My_Chart1.Series[i].MarkerStyle = MarkerStyle.None;
            }
        }

        #region 实现快速转换的代码
        private void 簇状柱形图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int SeriesNum = MyObject.My_Chart1.Series.Count;

                ChangeToDefaultAttribute();
                for (int i = 0; i < SeriesNum; i++)
                {
                    //迅速改变图形
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Column;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        private void 堆积柱形图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
            try
            {
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                //if (SeriesNum == 1)
                //{
                ChangeToDefaultAttribute();
                for (int i = 0; i < SeriesNum; i++)
                {
                    //迅速改变图形
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedColumn;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 百分比堆积柱形图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                if (SeriesNum > 1)
                {
                    ChangeToDefaultAttribute();
                    //迅速改变图形
                    for (int i = 0; i < SeriesNum; i++)
                    {
                        MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedColumn100;
                    }
                }
                else
                {
                    MessageBox.Show("只有一个序列的图不可转换成两个序列的图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //这里以后加读取数据的代码
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 三维簇状柱形图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                MyObject.My_Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                for (int i = 0; i < SeriesNum; i++)
                {
                    //迅速改变图形
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Column;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void 三维堆积柱形图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                //迅速改变图形
                MyObject.My_Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedColumn;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 三维百分比堆积柱形图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                if (SeriesNum > 1)
                {
                    ChangeToDefaultAttribute();
                    //迅速改变图形
                    MyObject.My_Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                    for (int i = 0; i < SeriesNum; i++)
                    {
                        MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedColumn100;
                    }
                }
                else
                {
                    MessageBox.Show("只有一个序列的图不可转换成两个序列的图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 簇状圆柱图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                for (int i = 0; i < SeriesNum; i++)
                {
                    //迅速改变图形
                    MyObject.My_Chart1.Series[i]["DrawingStyle"] = "Cylinder";//绘图风格为浮雕型
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Column;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 堆积圆柱图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                for (int i = 0; i < SeriesNum; i++)
                {
                    //迅速改变图形
                    MyObject.My_Chart1.Series[i]["DrawingStyle"] = "Cylinder";//绘图风格为浮雕型
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedColumn;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 百分比堆积圆柱图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                if (SeriesNum > 1)
                {
                    ChangeToDefaultAttribute();
                    for (int i = 0; i < SeriesNum; i++)
                    {
                        //迅速改变图形
                        MyObject.My_Chart1.Series[i]["DrawingStyle"] = "Cylinder";//绘图风格为浮雕型
                        MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedColumn100;
                    }
                }
                else
                {
                    MessageBox.Show("只有一个序列的图不可转换成两个序列的图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 三维圆柱图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                //迅速改变图形
                MyObject.My_Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i]["DrawingStyle"] = "Cylinder";//绘图风格为浮雕型
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Column;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void 簇状条形图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                //迅速改变图形
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Bar;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void 堆积条形图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                //迅速改变图形
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedBar;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void 百分比堆积条形图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                if (SeriesNum > 1)
                {
                    ChangeToDefaultAttribute();
                    for (int i = 0; i < SeriesNum; i++)
                    {
                        //迅速改变图形
                        MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedBar100;
                    }
                }
                else
                {
                    MessageBox.Show("只有一个序列的图不可转换成两个序列的图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void 三维簇状条形图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;

                ChangeToDefaultAttribute();
                //迅速改变图形
                MyObject.My_Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Bar;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 三维堆积条形图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                //迅速改变图形
                MyObject.My_Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedBar;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 三维百分比堆积条形图ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                if (SeriesNum > 1)
                {
                    ChangeToDefaultAttribute();
                    //迅速改变图形
                    MyObject.My_Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                    for (int i = 0; i < SeriesNum; i++)
                    {
                        MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedBar100;
                    }
                }
                else
                {
                    MessageBox.Show("只有一个序列的图不可转换成两个序列的图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 簇状水平圆柱图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                //迅速改变图形
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i]["DrawingStyle"] = "Cylinder";//绘图风格为圆柱形
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Bar;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 堆积水平圆柱图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                for (int i = 0; i < SeriesNum; i++)
                {
                    //迅速改变图形
                    MyObject.My_Chart1.Series[i]["DrawingStyle"] = "Cylinder";//绘图风格为圆柱形
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedBar;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 百分比堆积水平圆柱图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                if (SeriesNum > 1)
                {
                    ChangeToDefaultAttribute();
                    for (int i = 0; i < SeriesNum; i++)
                    {
                        //迅速改变图形
                        MyObject.My_Chart1.Series[i]["DrawingStyle"] = "Cylinder";//绘图风格为圆柱形
                        MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedBar100;
                    }
                }
                else
                {
                    MessageBox.Show("只有一个序列的图不可转换成两个序列的图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 普通饼图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                if (SeriesNum == 1)
                {
                    ChangeToDefaultAttribute();

                    MyObject.My_Chart1.Series[0].ChartType = SeriesChartType.Pie;
                    for (int i = 0; i < MyObject .My_Chart1 .Series [0].Points .Count ; i++)
                    {
                        MyObject .My_Chart1.Series[0].Points[i]["Exploded"] = "flase";
                    }
                }
                else
                {
                    MessageBox.Show("多个序列的图不可转换成只含一个序列的饼图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 分离型饼图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                if (SeriesNum == 1)
                {
                    ChangeToDefaultAttribute();

                    MyObject.My_Chart1.Series[0].ChartType = SeriesChartType.Pie;
                    foreach (DataPoint point in MyObject.My_Chart1.Series[0].Points)
                    {
                        point["Exploded"] = "true";  //每次单击的时候都让饼图初始合在一起
                    }
                }
                else
                {
                    MessageBox.Show("多个序列的图不可转换成只含一个序列的饼图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 三维饼图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                if (SeriesNum == 1)
                {
                    ChangeToDefaultAttribute();

                    MyObject.My_Chart1.Series[0].ChartType = SeriesChartType.Pie;
                    MyObject.My_Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                    for (int i = 0; i < MyObject.My_Chart1.Series[0].Points.Count; i++)
                    {
                        MyObject.My_Chart1.Series[0].Points[i]["Exploded"] = "flase";
                    }
                }
                else
                {
                    MessageBox.Show("多个序列的图不可转换成只含一个序列的饼图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 三维分离型饼图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                if (SeriesNum == 1)
                {
                    ChangeToDefaultAttribute();

                    MyObject.My_Chart1.Series[0].ChartType = SeriesChartType.Pie;
                    MyObject.My_Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                    foreach (DataPoint point in MyObject.My_Chart1.Series[0].Points)
                    {
                        point["Exploded"] = "true";  //每次单击的时候都让饼图初始合在一起
                    }
                }
                else
                {
                    MessageBox.Show("多个序列的图不可转换成只含一个序列的饼图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 普通圆环ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                if (SeriesNum == 1)
                {
                    ChangeToDefaultAttribute();
                    MyObject.My_Chart1.Series[0].ChartType = SeriesChartType.Doughnut;
                    for (int i = 0; i < MyObject.My_Chart1.Series[0].Points.Count; i++)
                    {
                        MyObject.My_Chart1.Series[0].Points[i]["Exploded"] = "flase";
                    }
                }
                else
                {
                    MessageBox.Show("多个序列的图不可转换成只含一个序列的圆环图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 分离型圆环ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                if (SeriesNum == 1)
                {
                    ChangeToDefaultAttribute();
                    MyObject.My_Chart1.Series[0].ChartType = SeriesChartType.Doughnut;
                    foreach (DataPoint point in MyObject.My_Chart1.Series[0].Points)
                    {
                        point["Exploded"] = "true";  //每次单击的时候都让饼图初始合在一起
                    }
                }
                else
                {
                    MessageBox.Show("多个序列的图不可转换成只含一个序列的圆环图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 普通面积图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Area;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 堆积面积图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedArea;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 百分比堆积面积ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                if (SeriesNum > 1)
                {
                    ChangeToDefaultAttribute();
                    for (int i = 0; i < SeriesNum; i++)
                    {
                        MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedArea100;
                    }
                }
                else
                {
                    MessageBox.Show("只有一个序列的图不可转换成两个序列的图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 三维面积图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                MyObject.My_Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Area;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 三维堆积面积图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                MyObject.My_Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedArea;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 三维百分比堆积面积图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                if (SeriesNum > 1)
                {
                    ChangeToDefaultAttribute();
                    MyObject.My_Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                    for (int i = 0; i < SeriesNum; i++)
                    {
                        MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.StackedArea100;
                    }
                }
                else
                {
                    MessageBox.Show("只有一个序列的图不可转换成两个序列的图！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 普通折线图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Line;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void 带数据标记的折线图ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Line;
                    MyObject.My_Chart1.Series[i].MarkerStyle = MarkerStyle.Diamond;//设置数据点的形状
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void 三维折线图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                MyObject.My_Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Line;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void 仅带数据标记的散点图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Point;
                    MyObject.My_Chart1.Series[i].MarkerStyle = MarkerStyle.Diamond;
                }

            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 带平滑线和数据标记的散点图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Spline;
                    MyObject.My_Chart1.Series[i].MarkerStyle = MarkerStyle.Diamond;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void 带平滑线散点图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Spline;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 带直线和数据标记的散点图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Line;
                    MyObject.My_Chart1.Series[i].MarkerStyle = MarkerStyle.Diamond;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 带直线散点图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //这里要判断一下，如果是从两个序列转到一个序列就不让它转！一个序列的图可以转到两个序列
                int SeriesNum = MyObject.My_Chart1.Series.Count;
                ChangeToDefaultAttribute();
                for (int i = 0; i < SeriesNum; i++)
                {
                    MyObject.My_Chart1.Series[i].ChartType = SeriesChartType.Line;
                }
            }
            catch
            {
                MessageBox.Show("转换失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion


        #region 实现文本框插入的功能




        private void 插入文本框ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewStarAnnotation(MyObject.My_Chart1);
            MyObject.My_Chart1.AnnotationSelectionChanged += new EventHandler(My_Chart1_AnnotationSelectionChanged);
            toolStripMenuItem2.Enabled = true;
        }

        public void CreateNewStarAnnotation(Chart chart)
        {
            // Create annotation group and add it to the chart annotations collection
            RectangleAnnotation annotation = new RectangleAnnotation();
            annotation.X = 20;
            annotation.Y = 20;
            annotation.Width = 20;
            annotation.Height = 10;
            annotation.IsMultiline = true;
            annotation.Text = "双击此处输入批注内容！";
            annotation.AllowSelecting = true;
            annotation.AllowMoving = true;
            annotation.AllowResizing = true;
            annotation.AllowTextEditing = true;
            annotation.LineDashStyle = ChartDashStyle.NotSet;
            chart.Annotations.Add(annotation);
            //chart.Annotations[0].AxisX = chart.ChartAreas[0].AxisX;
            //chart.Annotations[0].AxisY = chart.ChartAreas[0].AxisY;
            //chart.Annotations[0].AnchorX = 1;
            //chart.Annotations[0].AnchorY = 20;
            //chart.Annotations[0].ResizeToContent();

        }

        public static bool[] IsAnnotationSelect = new bool[NUM];
        public void My_Chart1_AnnotationSelectionChanged(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                for (int i = 0; i < MyObject.My_Chart1.Annotations.Count; i++)
                {
                    if (MyObject.My_Chart1.Annotations[i].IsSelected)
                    {
                        IsAnnotationSelect[i] = true;
                        //把第几个批注被选到的值赋值给一个myannotation
                        MyObject.MyAnnotation1 = MyObject.My_Chart1.Annotations[i];

                        MyObject.My_Chart1.Annotations[i].AllowPathEditing = true;
                    }
                    else
                    {
                        IsAnnotationSelect[i] = false;
                        MyObject.My_Chart1.Annotations[i].AllowPathEditing = false;
                    }
                }
            }
        }
        #endregion

        //这里要为新图加点代码
        #region 实现迅速改变XY轴字体颜色功能
        private void buttonX12_Click(object sender, EventArgs e)
        {
            插入文本框ToolStripMenuItem_Click(sender, e);
        }

        private void x轴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Name != "生产开发曲线")
            {
                FontDialog fd = new FontDialog();
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    MyObject.My_Chart1.ChartAreas[0].AxisX.LabelStyle.Font = fd.Font;
                }
            }
            else if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Name == "生产开发曲线")
            {
                FontDialog fd = new FontDialog();
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    MyObject.My_Chart1.ChartAreas[0].AxisX.LabelStyle.Font = fd.Font;
                    MyObject.My_Chart1.ChartAreas[1].AxisX.LabelStyle.Font = fd.Font;
                    MyObject.My_Chart1.ChartAreas[2].AxisX.LabelStyle.Font = fd.Font;
                    MyObject.My_Chart1.ChartAreas[0].AxisX.TitleFont = fd.Font;
                    MyObject.My_Chart1.ChartAreas[1].AxisX.TitleFont = fd.Font;
                    MyObject.My_Chart1.ChartAreas[2].AxisX.TitleFont = fd.Font;
                }
            }
            else
            {
                MessageBox.Show("请您先选中图表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void y轴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Name != "生产开发曲线")
            {
                FontDialog fd = new FontDialog();
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    MyObject.My_Chart1.ChartAreas[0].AxisY.LabelStyle.Font = fd.Font;
                }
            }

            else if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Name == "生产开发曲线")
            {
                FontDialog fd = new FontDialog();
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    MyObject.My_Chart1.ChartAreas[0].AxisY.LabelStyle.Font = fd.Font;
                    MyObject.My_Chart1.ChartAreas[1].AxisY.LabelStyle.Font = fd.Font;
                    MyObject.My_Chart1.ChartAreas[2].AxisY.LabelStyle.Font = fd.Font;
                    MyObject.My_Chart1.ChartAreas[0].AxisY.TitleFont = fd.Font;
                    MyObject.My_Chart1.ChartAreas[1].AxisY.TitleFont = fd.Font;
                    MyObject.My_Chart1.ChartAreas[2].AxisY.TitleFont = fd.Font;
                }
            }
            else
            {
                MessageBox.Show("请您先选中图表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void x轴ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Name != "生产开发曲线")
            {
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    MyObject.My_Chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = cd.Color;
                }
            }

            else if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Name == "生产开发曲线")
            {
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    MyObject.My_Chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = cd.Color;
                    MyObject.My_Chart1.ChartAreas[1].AxisX.LabelStyle.ForeColor = cd.Color;
                    MyObject.My_Chart1.ChartAreas[2].AxisX.LabelStyle.ForeColor = cd.Color;
                    MyObject.My_Chart1.ChartAreas[0].AxisX.TitleForeColor = cd.Color;
                    MyObject.My_Chart1.ChartAreas[1].AxisX.TitleForeColor = cd.Color;
                    MyObject.My_Chart1.ChartAreas[2].AxisX.TitleForeColor = cd.Color;
                }
            }
            else
            {
                MessageBox.Show("请您先选中图表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void y轴ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Name != "生产开发曲线")
            {
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    MyObject.My_Chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = cd.Color;
                }
            }

            else if (MyObject.My_Chart1 != null && MyObject.My_Chart1.Name == "生产开发曲线")
            {
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    MyObject.My_Chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = cd.Color;
                    MyObject.My_Chart1.ChartAreas[1].AxisY.LabelStyle.ForeColor = cd.Color;
                    MyObject.My_Chart1.ChartAreas[2].AxisY.LabelStyle.ForeColor = cd.Color;
                    MyObject.My_Chart1.ChartAreas[0].AxisY.TitleForeColor = cd.Color;
                    MyObject.My_Chart1.ChartAreas[1].AxisY.TitleForeColor = cd.Color;
                    MyObject.My_Chart1.ChartAreas[2].AxisY.TitleForeColor = cd.Color;
                }
            }
            else
            {
                MessageBox.Show("请您先选中图表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        #endregion

        #region 主界面上的一些联动图的功能

        public void ChangeAllTheChartTitleBoder()
        {
            //将所有图的图题边框全部去掉
            ToOperateTheChart.ChangeTheChartTitleBoder(chart1, gap[0], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart2, gap[1], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart3, gap[2], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart4, gap[3], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart5, gap[4], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart6, gap[5], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart7, gap[6], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart8, gap[7], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart9, gap[8], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart10, gap[9], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart11, gap[10], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart12, gap[11], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart13, gap[12], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart14, gap[13], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart15, gap[14], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart16, gap[15], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart17, gap[16], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart18, gap[17], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart19, gap[18], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart20, gap[19], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart21, gap[20], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart22, gap[21], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart23, gap[22], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart24, gap[23], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart25, gap[24], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart26, gap[25], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart27, gap[26], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart28, gap[27], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart29, gap[28], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart30, gap[29], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart31, gap[30], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart32, gap[31], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart33, gap[32], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart34, gap[33], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart35, gap[34], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart36, gap[35], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart37, gap[36], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart38, gap[37], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart39, gap[38], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart40, gap[39], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart41, gap[40], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart42, gap[41], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart43, gap[42], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart44, gap[43], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeTheChartTitleBoder(chart45, gap[44], Color.Empty, ChartDashStyle.NotSet);
        }

        public void ChangeAllTheChartBoderSkin()
        {
            //将所有图的边框全部去掉
            ToOperateTheChart.ChangeTheChartBoder(chart1, gap[0], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart2, gap[1], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart3, gap[2], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart4, gap[3], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart5, gap[4], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart6, gap[5], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart7, gap[6], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart8, gap[7], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart9, gap[8], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart10, gap[9], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart11, gap[10], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart12, gap[11], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart13, gap[12], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart14, gap[13], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart15, gap[14], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart16, gap[15], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart17, gap[16], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart18, gap[17], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart19, gap[18], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart20, gap[19], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart21, gap[20], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart22, gap[21], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart23, gap[22], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart24, gap[23], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart25, gap[24], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart26, gap[25], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart27, gap[26], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart28, gap[27], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart29, gap[28], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart30, gap[29], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart31, gap[30], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart32, gap[31], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart33, gap[32], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart34, gap[33], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart35, gap[34], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart36, gap[35], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart37, gap[36], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart38, gap[37], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart39, gap[38], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart40, gap[39], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart41, gap[40], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart42, gap[41], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart43, gap[42], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart44, gap[43], BorderSkinStyle.None);
            ToOperateTheChart.ChangeTheChartBoder(chart45, gap[44], BorderSkinStyle.None);
        }

        public void ChangeAllLegendItemBoder()
        {
            //将所有图的图例区边框去掉
            ToOperateTheChart.ChangeLengendItemBoder(chart1, gap[0], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart2, gap[1], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart3, gap[2], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart4, gap[3], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart5, gap[4], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart6, gap[5], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart7, gap[6], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart8, gap[7], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart9, gap[8], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart10, gap[9], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart11, gap[10], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart12, gap[11], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart13, gap[12], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart14, gap[13], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart15, gap[14], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart16, gap[15], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart17, gap[16], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart18, gap[17], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart19, gap[18], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart20, gap[19], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart21, gap[20], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart22, gap[21], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart23, gap[22], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart24, gap[23], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart25, gap[24], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart26, gap[25], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart27, gap[26], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart28, gap[27], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart29, gap[28], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart30, gap[29], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart31, gap[30], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart32, gap[31], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart33, gap[32], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart34, gap[33], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart35, gap[34], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart36, gap[35], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart37, gap[36], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart38, gap[37], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart39, gap[38], Color.Empty, ChartDashStyle.NotSet);
            ToOperateTheChart.ChangeLengendItemBoder(chart40, gap[39], Color.Empty, ChartDashStyle.NotSet);
            //以下5个图没有图例
            //ToOperateTheChart.ChangeLengendItemBoder(chart41, gap[40], Color.Empty, ChartDashStyle.NotSet);
            //ToOperateTheChart.ChangeLengendItemBoder(chart42, gap[41], Color.Empty, ChartDashStyle.NotSet);
            //ToOperateTheChart.ChangeLengendItemBoder(chart43, gap[42], Color.Empty, ChartDashStyle.NotSet);
            //ToOperateTheChart.ChangeLengendItemBoder(chart44, gap[43], Color.Empty, ChartDashStyle.NotSet);
            //ToOperateTheChart.ChangeLengendItemBoder(chart45, gap[44], Color.Empty, ChartDashStyle.NotSet);
        }

        public void ChangeAllPlottingAreaBoder()
        {
            //将所有图的绘图区边框去掉()
            ToOperateTheChart.ChangePlottingAreaBoder(chart1, gap[0], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart2, gap[1], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart3, gap[2], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart4, gap[3], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart5, gap[4], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart6, gap[5], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart7, gap[6], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart8, gap[7], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart9, gap[8], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart10, gap[9], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart11, gap[10], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart12, gap[11], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart13, gap[12], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart14, gap[13], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart15, gap[14], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart16, gap[15], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart17, gap[16], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart18, gap[17], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart19, gap[18], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart20, gap[19], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart21, gap[20], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart22, gap[21], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart23, gap[22], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart24, gap[23], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart25, gap[24], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart26, gap[25], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart27, gap[26], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart28, gap[27], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart29, gap[28], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart30, gap[29], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart31, gap[30], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart32, gap[31], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart33, gap[32], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart34, gap[33], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart35, gap[34], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart36, gap[35], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart37, gap[36], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart38, gap[37], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart39, gap[38], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart40, gap[39], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart41, gap[40], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart42, gap[41], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart43, gap[42], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart44, gap[43], Color.Empty);
            ToOperateTheChart.ChangePlottingAreaBoder(chart45, gap[44], Color.Empty);
        }

        #region 让设置好的边框消失？？疯了吧
        public void ChangeAllTheChartDataPointBoder()
        {
            //把所有的DataPoints的边框也去掉
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart1, gap[0]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart2, gap[1]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart3, gap[2]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart4, gap[3]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart5, gap[4]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart6, gap[5]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart7, gap[6]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart8, gap[7]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart9, gap[8]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart10, gap[9]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart11, gap[10]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart12, gap[11]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart13, gap[12]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart14, gap[13]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart15, gap[14]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart16, gap[15]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart17, gap[16]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart18, gap[17]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart19, gap[18]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart20, gap[19]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart21, gap[20]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart22, gap[21]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart23, gap[22]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart24, gap[23]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart25, gap[24]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart26, gap[25]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart27, gap[26]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart28, gap[27]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart29, gap[28]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart30, gap[29]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart31, gap[30]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart32, gap[31]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart33, gap[32]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart34, gap[33]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart35, gap[34]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart36, gap[35]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart37, gap[36]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart38, gap[37]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart39, gap[38]);
            ToOperateTheChart.ChangeDataPointsBoderWhite(chart40, gap[39]);
            //ToOperateTheChart.ChangeDataPointsBoderWhite(chart41, gap[40]);
            //ToOperateTheChart.ChangeDataPointsBoderWhite(chart42, gap[41]);
        }
        #endregion
        public void ChangeAllBoder()
        {
            //将所有图的图例边框全部去掉
            ChangeAllLegendItemBoder();

            //将所有图的绘图区边框全部去掉
            ChangeAllPlottingAreaBoder();

            //将所有图的图题边框全部去掉
            ChangeAllTheChartTitleBoder();

            //将所有图的边框全部去掉
            ChangeAllTheChartBoderSkin();

            //把所有的DataPoints的边框也去掉
            ChangeAllTheChartDataPointBoder();
        }
        private void MainFrame_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (Control c in this.panel1.Controls)
            {
                if (c is Chart)
                {
                    #region 实现这样一种效果，当鼠标点击主界面的时候，所有的图的边框（图题，边框，DataPoints都消失达到没有被选选中的效果）
                    if (MyObject.My_Chart1 != null)
                    {
                        ChangeAllBoder();

                        foreach (DataPoint point in MyObject.My_Chart1.Series[0].Points)
                        {
                            point["Exploded"] = "false";  //每次单击的时候都让饼图初始合在一起
                        }
                    }
                    #endregion
                }

            }
            //把当前选中状态清空
            MyObject.My_Chart1 = null;
            MyObject.MyTextBox1 = null;
            MyObject.MyTitle1 = null;
            MyObject.MyAxisX1 = null;
            MyObject.MyAxisY1 = null;
        }

        #region 主界面上改变字体的代码

        private Dictionary<string, string> dictionary;//采用字典类型进行区分,key是汉字，value是对应的类型

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontStyle style = (FontStyle)Enum.Parse(typeof(FontStyle), this.dictionary[this.comboBoxFontStyle.SelectedItem.ToString()]);//改变字体样式比如加粗，下划线等
            float size = (float)Convert.ToDouble(this.comboBoxFontSize.SelectedItem.ToString());//改变字体大小
            Font font = new Font(FontFamily.GenericMonospace, size, style);
            this.lblTest.Font = font;

            if (MyObject.MyAnnotation1 != null)
            {
                MyObject.MyAnnotation1.Font = font;
            }

            if (MyObject.MyTitle1 != null)
            {
                this.lblTest.Text = MyObject.My_Chart1.Titles[0].Text;
                MyObject.My_Chart1.Titles[0].Font = font;


            }
            if (MyObject.MyTextBox1 != null)
            {
                this.lblTest.Text = MyObject.MyTextBox1.Text;
                MyObject.MyTextBox1.Font = font;
            }
            if (MyObject.MyAxisX1 != null)
            {
                this.lblTest.Text = "改变X轴字体样式";
                MyObject.My_Chart1.ChartAreas[0].AxisX.LabelStyle.Font = font;
            }
            if (MyObject.MyAxisY1 != null)
            {
                this.lblTest.Text = "改变Y轴字体样式";
                MyObject.My_Chart1.ChartAreas[0].AxisY.LabelStyle.Font = font;
            }
            ///////////0115修改
            if (MyObject.FrmName2 != null)
            {
                SysData.IsDouble = false;
                this.lblTest.Text = MyObject.FrmName2;
                SysData.title_font = font;
                switch (MyObject.FrmName2)
                {
                    case "主图":
                        pselfrefresh();
                        break;
                    case "测井曲线":
                        pselfrefresh();
                        break;
                    case "粒度概率累计曲线":
                        pselfrefresh3();
                        break;
                    case "油气水":
                        pselfrefresh4();
                        break;
                    case "C_M1":
                        pselfrefresh1();
                        break;

                    case "C_M2":
                        pselfrefresh2();
                        break;
                    case "萨胡成因判别函数":
                        pselfrefresh5();
                        break;
                }
                //pselfrefresh1();
            }
            //////////

        }

        #endregion


        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                this.panel1.Controls.Add(chart1[gap[0]]);
                chart1[gap[0]].Location = new Point(initPointX, initPointY);
                // 把第一附图保存到内存流中
                chart1[gap[0]].Serializer.Content = SerializationContents.All;
                MemoryStream ms = new MemoryStream();
                MyObject.My_Chart1.Serializer.Save(ms);

                // 加载内存流中的数据到第二幅图
                ms.Seek(0, SeekOrigin.Begin);
                chart1[gap[0]].Serializer.Load(ms);
                ms.Close();
                gap[0]++;
            }
            #region 三端元的复制
            else if (MyObject.MyCurrentFrmName1 != null)
            {
                FormState fs = XmlHelper.LoadForm();//先从序列话的dat文件中把窗体的信息加载出来
                if (fs != null)
                {
                    switch (MyObject.MyCurrentFrmName1)
                    {
                        case "含砾沉积物三角形分类图":
                            SanHanLiChenJiWu CopyFrom = new SanHanLiChenJiWu();

                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom.cP.Color = fs.PenColor;
                            CopyFrom.MyBrush.Color = fs.SolidBrushColor;
                            CopyFrom.LabelFont = fs.LabelFont;
                            #endregion

                            CopyFrom.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom);
                            CopyFrom.TopLevel = false;
                            CopyFrom.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom);  //然后将该窗体嵌入主界面
                            CopyFrom.Show();
                            break;
                        case "无砾沉积物三角形分类图":

                            SanWuLiChenJiWu CopyFrom2 = new SanWuLiChenJiWu();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom2.cP.Color = fs.PenColor;
                            CopyFrom2.MyBrush.Color = fs.SolidBrushColor;
                            CopyFrom2.LabelFont = fs.LabelFont;
                            #endregion
                            CopyFrom2.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom2);
                            CopyFrom2.TopLevel = false;
                            CopyFrom2.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom2);  //然后将该窗体嵌入主界面
                            CopyFrom2.Show();
                            break;
                        case "现代沉淀物Shepard分类图":

                            SanXianDaiChenDianWuShepard CopyFrom3 = new SanXianDaiChenDianWuShepard();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom3.cP.Color = fs.PenColor;
                            CopyFrom3.MyBrush.Color = fs.SolidBrushColor;
                            CopyFrom3.LabelFont = fs.LabelFont;
                            #endregion
                            CopyFrom3.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom3);
                            CopyFrom3.TopLevel = false;
                            CopyFrom3.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom3);  //然后将该窗体嵌入主界面
                            CopyFrom3.Show();
                            break;

                        case "岩石三端元图图一":

                            SanYanShiSanDuanYuan1 CopyFrom7 = new SanYanShiSanDuanYuan1();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom7.mypen.Color = fs.PenColor;
                            CopyFrom7.mybrush.Color = fs.SolidBrushColor;
                            CopyFrom7.myfont = fs.LabelFont;
                            #endregion
                            CopyFrom7.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom7);
                            CopyFrom7.TopLevel = false;
                            CopyFrom7.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom7);  //然后将该窗体嵌入主界面
                            CopyFrom7.Show();
                            break;

                        case "岩石三端元图图二":

                            SanYanShiSanDuanYuan2 CopyFrom8 = new SanYanShiSanDuanYuan2();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom8.mypen.Color = fs.PenColor;
                            CopyFrom8.mybrush.Color = fs.SolidBrushColor;
                            CopyFrom8.myfont = fs.LabelFont;
                            #endregion
                            CopyFrom8.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom8);
                            CopyFrom8.TopLevel = false;
                            CopyFrom8.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom8);  //然后将该窗体嵌入主界面
                            CopyFrom8.Show();
                            break;

                        case "岩石三端元图图三":

                            SanYanShiSanDuanYuan3 CopyFrom9 = new SanYanShiSanDuanYuan3();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom9.mypen.Color = fs.PenColor;
                            CopyFrom9.mybrush.Color = fs.SolidBrushColor;
                            CopyFrom9.myfont = fs.LabelFont;
                            #endregion
                            CopyFrom9.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom9);
                            CopyFrom9.TopLevel = false;
                            CopyFrom9.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom9);  //然后将该窗体嵌入主界面
                            CopyFrom9.Show();
                            break;
                        case "岩石分类图图版2图一":

                            SanYanShiFenLeiTwo1 CopyFrom4 = new SanYanShiFenLeiTwo1();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom4.mypen.Color = fs.PenColor;
                            CopyFrom4.mybrush.Color = fs.SolidBrushColor;
                            CopyFrom4.myfont = fs.LabelFont;
                            #endregion
                            CopyFrom4.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom4);
                            CopyFrom4.TopLevel = false;
                            CopyFrom4.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom4);  //然后将该窗体嵌入主界面
                            CopyFrom4.Show();
                            break;

                        case "岩石分类图图版2图二":

                            SanYanShiFenLeiTwo2 CopyFrom5 = new SanYanShiFenLeiTwo2();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom5.mypen.Color = fs.PenColor;
                            CopyFrom5.mybrush.Color = fs.SolidBrushColor;
                            CopyFrom5.myfont = fs.LabelFont;
                            #endregion
                            CopyFrom5.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom5);
                            CopyFrom5.TopLevel = false;
                            CopyFrom5.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom5);  //然后将该窗体嵌入主界面
                            CopyFrom5.Show();
                            break;

                        case "岩石分类图图版2图三":

                            SanYanShiFenLeiTwo3 CopyFrom6 = new SanYanShiFenLeiTwo3();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom6.mypen.Color = fs.PenColor;
                            CopyFrom6.mybrush.Color = fs.SolidBrushColor;
                            CopyFrom6.myfont = fs.LabelFont;
                            #endregion
                            CopyFrom6.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom6);
                            CopyFrom6.TopLevel = false;
                            CopyFrom6.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom6);  //然后将该窗体嵌入主界面
                            CopyFrom6.Show();
                            break;

                    }


                }

            }
            #endregion
            else
            {
                MessageBox.Show("您未进行复制或剪切操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                this.panel1.Controls.Remove(MyObject.My_Chart1);
                MyObject.My_Chart1.Visible = false;
            }
            if (MyObject.MyCurrentFrmName1 != null && MyObject.MyCurrentFrm1 != null)//专为三端元
            {

                this.panel1.Controls.Remove(MyObject.MyCurrentFrm1);
                #region
                switch (MyObject.MyCurrentFrmName1)
                {
                    case "含砾沉积物三角形分类图":

                        break;
                    case "无砾沉积物三角形分类图":


                        break;
                    case "现代沉淀物Shepard分类图":



                        break;

                    case "岩石三端元图图一":


                        break;

                    case "岩石三端元图图二":


                        break;

                    case "岩石三端元图图三":


                        break;
                    case "岩石分类图图版2图一":


                        break;

                    case "岩石分类图图版2图二":


                        break;

                    case "岩石分类图图版2图三":


                        break;
                }
                #endregion

            }
            else
            {
                MessageBox.Show("您未选中要剪切的图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem27_Click(object sender, EventArgs e)
        {

        }

        #region 在这里实现菜单栏中的剪切删除复制等相关操作

        //bool ifCutClick = false;
        private void 剪切_Click(object sender, EventArgs e)
        {
            isCopyAndCutClick = true;
            if (MyObject.My_Chart1 != null)
            {
                MyObject.My_Chart2 = MyObject.My_Chart1;
                this.panel1.Controls.Remove(MyObject.My_Chart1);
                //MyObject.My_Chart1.Visible = false;
            }
            else
            {
                MessageBox.Show("您未选中要剪切的图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        public bool isCopyAndCutClick = false;
        private void 复制_Click(object sender, EventArgs e)
        {
            MyObject.My_Chart2 = MyObject.My_Chart1;
            MyObject.FrmName1 = null;
            MyObject.MyCurrentFrmName1 = null;
            isCopyAndCutClick = true;
        }

        private void 删除_Click(object sender, EventArgs e)
        {
            if (this.panel1.Controls.Contains(toolStrip1))
            {
                this.toolStrip1.Visible = false;
            }
            if (MyObject.My_Chart1 != null)
            {
                this.panel1.Controls.Remove(MyObject.My_Chart1);
                MyObject.My_Chart1.Visible = false;
                MyObject.My_Chart1 = null;
                ReadDataAll.dgvData = null;
            }
            else
            {
                MessageBox.Show("您未选中要删除的图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void 粘贴Copy_Click(object sender, EventArgs e)
        {
            if (MyObject.FrmName1 != null)
            {
                #region 特殊图的复制

                FormState fs = XmlHelper.LoadForm();//先从序列话的dat文件中把窗体的信息加载出来


                if (MyObject.FrmName1.Equals("萨胡成因判别函数"))
                {
                    SadhuCauses shcy_form = new SadhuCauses();
                    if (fs != null)
                    {
                        shcy_form.MyText = fs.TitleText;
                        shcy_form.MyColor = fs.TitleColor;
                        shcy_form.MyFont = fs.TitleFont;

                        shcy_form.FormBorderStyle = FormBorderStyle.None;
                        shcy_form.MouseDown += new System.Windows.Forms.MouseEventHandler(this.萨胡成因判别函数_MouseDown);
                        shcy_form.MouseMove += new System.Windows.Forms.MouseEventHandler(this.萨胡成因判别函数_MouseMove);
                        shcy_form.TopLevel = false;
                        shcy_form.Location = new Point(0, 0);//位置排列
                        this.panel1.Controls.Add(shcy_form);  //然后将该窗体嵌入主界面
                        shcy_form.Show();
                    }
                }

                else if (MyObject.FrmName1.Equals("C_M1"))
                {
                    C_M1 cm1_form = new C_M1();
                    if (fs != null)
                    {
                        cm1_form.MyLineColor1 = fs.PenColor1;
                        cm1_form.MyLineColor2 = fs.PenColor2;
                        cm1_form.MyFont = fs.TitleFont;
                        cm1_form.MyLine1 = fs.PenSize1;
                        cm1_form.MyText = fs.TitleText;
                        cm1_form.MyLine2 = fs.PenSize2;
                        cm1_form.FormBorderStyle = FormBorderStyle.None;
                        cm1_form.MouseDown += new MouseEventHandler(this.C_M1_MouseDown);
                        cm1_form.MouseMove += new MouseEventHandler(this.C_M1_MouseMove);
                        cm1_form.TopLevel = false;
                        cm1_form.Location = new Point(0, 0);//位置排列
                        this.panel1.Controls.Add(cm1_form);  //然后将该窗体嵌入主界面
                        cm1_form.Show();
                    }
                }
                else if (MyObject.FrmName1.Equals("主图"))
                {
                    WellLog_Main cjzt_form = new WellLog_Main();
                    if (fs != null)
                    {

                        cjzt_form.FormBorderStyle = FormBorderStyle.None;
                        cjzt_form.TopLevel = false;
                        cjzt_form.Location = new Point(0, 0);//位置排列
                        cjzt_form.MouseDown += new MouseEventHandler(this.cjzt_MouseDown);
                        cjzt_form.MouseMove += new MouseEventHandler(this.cjzt_MouseMove);
                        this.panel1.Controls.Add(cjzt_form);
                        cjzt_form.Show();


                        //////////////////////////////////////////////////////////////////////////
                        jht.Panel_refresh += cjzt_form.Panel_refresh;//添加重绘委托事件
                        cjzt_form.DrawRedline += jht.DrawRedline;
                        cjzt_form.Update_draw += jht.update_draw;
                        cjzt_form.Selfrefresh += jht.selfrefresh;
                        //////////////////////////////////////////////////////////////////////////

                    }
                }
                else if (MyObject.FrmName1.Equals("C_M2"))
                {
                    C_M2 cm2_form = new C_M2();
                    if (fs != null)
                    {
                        cm2_form.MyLineColor1 = fs.PenColor1;
                        cm2_form.MyLineColor2 = fs.PenColor2;
                        cm2_form.MyFont = fs.TitleFont;
                        cm2_form.MyLine1 = fs.PenSize1;
                        cm2_form.MyText = fs.TitleText;
                        cm2_form.MyLine2 = fs.PenSize2;
                        cm2_form.FormBorderStyle = FormBorderStyle.None;
                        cm2_form.MouseDown += new MouseEventHandler(this.C_M2_MouseDown);
                        cm2_form.MouseMove += new MouseEventHandler(this.C_M2_MouseMove);
                        cm2_form.TopLevel = false;
                        cm2_form.Location = new Point(0, 0);//位置排列
                        this.panel1.Controls.Add(cm2_form);  //然后将该窗体嵌入主界面
                        cm2_form.Show();
                    }
                }
                else if (MyObject.FrmName1.Equals("粒度概率累计曲线"))
                {
                    Grabularity ldgl_form = new Grabularity();
                    if (fs != null)
                    {
                        ldgl_form.MyLineColor1 = fs.PenColor1;
                        ldgl_form.MyLineColor2 = fs.PenColor2;
                        ldgl_form.MyFont = fs.TitleFont;
                        ldgl_form.MyLine1 = fs.PenSize1;
                        ldgl_form.MyText = fs.TitleText;
                        ldgl_form.MyLine2 = fs.PenSize2;
                        ldgl_form.FormBorderStyle = FormBorderStyle.None;
                        ldgl_form.MouseDown += new MouseEventHandler(this.ldgl_MouseDown);
                        ldgl_form.MouseMove += new MouseEventHandler(this.ldgl_MouseMove);
                        ldgl_form.TopLevel = false;
                        ldgl_form.Location = new Point(0, 0);//位置排列
                        this.panel1.Controls.Add(ldgl_form);  //然后将该窗体嵌入主界面
                        ldgl_form.Show();
                    }
                }
                else if (MyObject.FrmName1.Equals("测井曲线"))
                {

                    WellLog cjqx_form = new WellLog();


                    if (fs != null)
                    {

                        cjqx_form.FormBorderStyle = FormBorderStyle.None;
                        cjqx_form.TopLevel = false;
                        cjqx_form.Location = new Point(0, 0);//位置排列
                        cjqx_form.MouseDown += new MouseEventHandler(this.cjqx_MouseDown);
                        cjqx_form.MouseMove += new MouseEventHandler(this.cjqx_MouseMove);
                        this.panel1.Controls.Add(cjqx_form);
                        cjqx_form.Show();


                        ////////////////////////////////////////////////////////////////////////////
                        cjqx_form.Panel_refresh += zu.Panel_refresh;//添加重绘委托事件
                        zu.DrawRedline += cjqx_form.DrawRedline;
                        zu.Update_draw += cjqx_form.update_draw;
                        zu.Selfrefresh += cjqx_form.selfrefresh;
                        ////////////////////////////////////////////////////////////////////////////

                    }
                }
                else if (MyObject.FrmName1.Equals("油气水"))
                {
                    OilWater OilWater_form = new OilWater();
                    if (fs != null)
                    {
                        OilWater_form.MyLineColor1 = fs.PenColor1;
                        OilWater_form.MyLineColor2 = fs.PenColor2;
                        OilWater_form.MyFont = fs.TitleFont;
                        OilWater_form.MyLine1 = fs.PenSize1;
                        OilWater_form.MyText = fs.TitleText;
                        OilWater_form.MyLine2 = fs.PenSize2;
                        OilWater_form.FormBorderStyle = FormBorderStyle.None;
                        OilWater_form.MouseDown += new MouseEventHandler(this.ldgl_MouseDown);
                        OilWater_form.MouseMove += new MouseEventHandler(this.ldgl_MouseMove);
                        OilWater_form.TopLevel = false;
                        OilWater_form.Location = new Point(0, 0);//位置排列
                        this.panel1.Controls.Add(OilWater_form);  //然后将该窗体嵌入主界面
                        OilWater_form.Show();
                    }
                }
                #endregion
            }
            else if (MyObject.My_Chart2 != null)
            {
                if (isCopyAndCutClick == false)
                {
                    MessageBox.Show("您未进行复制或剪切操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                this.panel1.Controls.Add(chart1[gap[0]]);
                chart1[gap[0]].Location = new Point(initPointX, initPointY);
                // 把第一附图保存到内存流中
                chart1[gap[0]].Serializer.Content = SerializationContents.All;
                MemoryStream ms = new MemoryStream();
                MyObject.My_Chart2.Serializer.Save(ms);

                // 加载内存流中的数据到第二幅图
                ms.Seek(0, SeekOrigin.Begin);
                chart1[gap[0]].Serializer.Load(ms);
                ms.Close();
                gap[0]++;

            }
            #region 三端元的复制
            else if (MyObject.MyCurrentFrmName1 != null)
            {
                FormState fs = XmlHelper.LoadForm();//先从序列话的dat文件中把窗体的信息加载出来
                if (fs != null)
                {
                    switch (MyObject.MyCurrentFrmName1)
                    {
                        case "含砾沉积物三角形分类图":
                            SanHanLiChenJiWu CopyFrom = new SanHanLiChenJiWu();

                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom.cP.Color = fs.PenColor;
                            CopyFrom.MyBrush.Color = fs.SolidBrushColor;
                            CopyFrom.LabelFont = fs.LabelFont;
                            #endregion

                            CopyFrom.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom);
                            CopyFrom.TopLevel = false;
                            CopyFrom.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom);  //然后将该窗体嵌入主界面
                            CopyFrom.Show();
                            break;
                        case "无砾沉积物三角形分类图":

                            SanWuLiChenJiWu CopyFrom2 = new SanWuLiChenJiWu();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom2.cP.Color = fs.PenColor;
                            CopyFrom2.MyBrush.Color = fs.SolidBrushColor;
                            CopyFrom2.LabelFont = fs.LabelFont;
                            #endregion
                            CopyFrom2.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom2);
                            CopyFrom2.TopLevel = false;
                            CopyFrom2.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom2);  //然后将该窗体嵌入主界面
                            CopyFrom2.Show();
                            break;
                        case "现代沉淀物Shepard分类图":

                            SanXianDaiChenDianWuShepard CopyFrom3 = new SanXianDaiChenDianWuShepard();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom3.cP.Color = fs.PenColor;
                            CopyFrom3.MyBrush.Color = fs.SolidBrushColor;
                            CopyFrom3.LabelFont = fs.LabelFont;
                            #endregion
                            CopyFrom3.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom3);
                            CopyFrom3.TopLevel = false;
                            CopyFrom3.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom3);  //然后将该窗体嵌入主界面
                            CopyFrom3.Show();
                            break;

                        case "岩石三端元图图一":

                            SanYanShiSanDuanYuan1 CopyFrom7 = new SanYanShiSanDuanYuan1();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom7.mypen.Color = fs.PenColor;
                            CopyFrom7.mybrush.Color = fs.SolidBrushColor;
                            CopyFrom7.myfont = fs.LabelFont;
                            #endregion
                            CopyFrom7.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom7);
                            CopyFrom7.TopLevel = false;
                            CopyFrom7.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom7);  //然后将该窗体嵌入主界面
                            CopyFrom7.Show();
                            break;

                        case "岩石三端元图图二":

                            SanYanShiSanDuanYuan2 CopyFrom8 = new SanYanShiSanDuanYuan2();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom8.mypen.Color = fs.PenColor;
                            CopyFrom8.mybrush.Color = fs.SolidBrushColor;
                            CopyFrom8.myfont = fs.LabelFont;
                            #endregion
                            CopyFrom8.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom8);
                            CopyFrom8.TopLevel = false;
                            CopyFrom8.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom8);  //然后将该窗体嵌入主界面
                            CopyFrom8.Show();
                            break;

                        case "岩石三端元图图三":

                            SanYanShiSanDuanYuan3 CopyFrom9 = new SanYanShiSanDuanYuan3();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom9.mypen.Color = fs.PenColor;
                            CopyFrom9.mybrush.Color = fs.SolidBrushColor;
                            CopyFrom9.myfont = fs.LabelFont;
                            #endregion
                            CopyFrom9.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom9);
                            CopyFrom9.TopLevel = false;
                            CopyFrom9.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom9);  //然后将该窗体嵌入主界面
                            CopyFrom9.Show();
                            break;
                        case "岩石分类图图版2图一":

                            SanYanShiFenLeiTwo1 CopyFrom4 = new SanYanShiFenLeiTwo1();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom4.mypen.Color = fs.PenColor;
                            CopyFrom4.mybrush.Color = fs.SolidBrushColor;
                            CopyFrom4.myfont = fs.LabelFont;
                            #endregion
                            CopyFrom4.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom4);
                            CopyFrom4.TopLevel = false;
                            CopyFrom4.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom4);  //然后将该窗体嵌入主界面
                            CopyFrom4.Show();
                            break;

                        case "岩石分类图图版2图二":

                            SanYanShiFenLeiTwo2 CopyFrom5 = new SanYanShiFenLeiTwo2();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom5.mypen.Color = fs.PenColor;
                            CopyFrom5.mybrush.Color = fs.SolidBrushColor;
                            CopyFrom5.myfont = fs.LabelFont;
                            #endregion
                            CopyFrom5.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom5);
                            CopyFrom5.TopLevel = false;
                            CopyFrom5.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom5);  //然后将该窗体嵌入主界面
                            CopyFrom5.Show();
                            break;

                        case "岩石分类图图版2图三":

                            SanYanShiFenLeiTwo3 CopyFrom6 = new SanYanShiFenLeiTwo3();
                            #region 从序列化的文件中读取保存的信息属性给新的窗体

                            CopyFrom6.mypen.Color = fs.PenColor;
                            CopyFrom6.mybrush.Color = fs.SolidBrushColor;
                            CopyFrom6.myfont = fs.LabelFont;
                            #endregion
                            CopyFrom6.FormBorderStyle = FormBorderStyle.None;
                            MyDelegate(CopyFrom6);
                            CopyFrom6.TopLevel = false;
                            CopyFrom6.Location = new Point(0, 0);//位置排列
                            this.panel1.Controls.Add(CopyFrom6);  //然后将该窗体嵌入主界面
                            CopyFrom6.Show();
                            break;

                    }


                }

            }
            #endregion
            else
            {
                MessageBox.Show("您未进行复制或剪切操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        private void 图片另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                SaveFileDialog sa = new SaveFileDialog();
                sa.Filter = "Jpg 图片|*.jpg|Bmp 图片|*.bmp|Gif 图片|*.gif|Png 图片|*.png|Wmf  图片|*.wmf";
                if (sa.ShowDialog() == DialogResult.OK)
                {
                    MyObject.My_Chart1.SaveImage(sa.FileName, ImageFormat.Jpeg);     //保存为jpeg文件 
                }
            }
            else
            {
                MessageBox.Show("您未选中要保存的图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void 属性_Click(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                //如果刚点击出来的时候是白色的没有读取数据
                if (MyObject.My_Chart1.Series[0].Points.Count == 0)
                {
                    MessageBox.Show("请您先读取数据！");
                    return;
                }


                if (MyObject.My_Chart1.Name == "递减曲线")
                {
                    SetDiJianQuXianProperity sd = new SetDiJianQuXianProperity();
                    sd.ShowDialog();
                }
                else if (MyObject.My_Chart1.Name == "相渗曲线")
                {
                    SetXiangshenQuXianProperity sd = new SetXiangshenQuXianProperity();
                    sd.ShowDialog();
                }
                else if (MyObject.My_Chart1.Name == "生产开发曲线")
                {
                    SetShengChanKaiFaQuXianProperity sd = new SetShengChanKaiFaQuXianProperity();
                    sd.ShowDialog();
                }
                else if (MyObject.My_Chart1.Series[0].ChartType == SeriesChartType.Pie || MyObject.My_Chart1.Series[0].ChartType == SeriesChartType.Doughnut)
                {
                    SetProperty2 sp2 = new SetProperty2();
                    sp2.ShowDialog();
                }
                else
                {
                    SetProperty sp = new SetProperty();
                    sp.ShowDialog();
                }
            }
        }
        #endregion

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            //关闭按钮的代码（后期将buttonItem7_Click这么外行的名字改掉！建议改为btnItemClose）
            this.Close();
        }


        private void colorPickerButton1_SelectedColorChanged(object sender, EventArgs e)
        {
            //当点击开始菜单栏中的颜色选择按钮时，改变测试文字的颜色，以及与图发生联动（后期改名字colorPickerButton1！建议改为colpickbtnChange）
            if (MyObject.MyAnnotation1 != null)
            {
                try
                {
                    MyObject.MyAnnotation1.ForeColor = colorPickerButton1.SelectedColor;
                }
                catch
                {
                    return;
                }
            }



            if (MyObject.MyTitle1 != null)
            {
                this.lblTest.Text = MyObject.My_Chart1.Titles[0].Text;

                MyObject.My_Chart1.Titles[0].ForeColor = colorPickerButton1.SelectedColor;
            }
            if (MyObject.MyTextBox1 != null)
            {
                this.lblTest.Text = MyObject.MyTextBox1.Text;
                MyObject.MyTextBox1.ForeColor = colorPickerButton1.SelectedColor;
            }
            if (MyObject.MyAxisX1 != null)
            {
                MyObject.My_Chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = colorPickerButton1.SelectedColor;
            }
            if (MyObject.MyAxisY1 != null)
            {
                MyObject.My_Chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = colorPickerButton1.SelectedColor;
            }
            if (MyObject.MyPen1 != null)
            {
                MyObject.MyPen1.Color = colorPickerButton1.SelectedColor;
            }
            ///////////0115修改
            //if (MyObject.FrmName2 != null)
            //{
            //    SysData.IsDouble = false;
            //    this.lblTest.Text = MyObject.FrmName2;
            //    SysData.title_color = colorPickerButton1.SelectedColor;
            //    switch (MyObject.FrmName2)
            //    {
            //        case "主图":
            //            pselfrefresh();
            //            break;
            //        case "测井曲线":
            //            pselfrefresh();
            //            break;
            //        case "粒度概率累计曲线":
            //            pselfrefresh3();
            //            break;
            //        case "油气水":
            //            pselfrefresh4();
            //            break;
            //        case "C_M1":
            //            pselfrefresh1();
            //            break;
            //        case "C_M2":
            //            pselfrefresh2();
            //            break;
            //        case "萨胡成因判别函数":
            //            pselfrefresh5();
            //            break;
            //    }
            //}
            this.lblTest.ForeColor = colorPickerButton1.SelectedColor;
        }


        //初始化放大缩小的数量
        int size = 20;
        private void btnItemZoomIn_Click(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                //单击开始菜单栏上的放大
                MyObject.My_Chart1.Height = MyObject.My_Chart1.Height + size;
                MyObject.My_Chart1.Width = MyObject.My_Chart1.Width + size;
            }
            if (MyObject.Mypicturebox != null)
            {
                MyObject.Mypicturebox.Height = MyObject.Mypicturebox.Height + size;
                MyObject.Mypicturebox.Width = MyObject.Mypicturebox.Width + size;
                MyObject.MyPicBoxMagnification1 = (float)MyObject.Mypicturebox.Height / initHeight;
            }
        }

        private void btnItemZoomOut_Click(object sender, EventArgs e)
        {
            //单击开始菜单栏上的缩小
            if (MyObject.My_Chart1 != null)
            {
                MyObject.My_Chart1.Height = MyObject.My_Chart1.Height - size;
                MyObject.My_Chart1.Width = MyObject.My_Chart1.Width - size;
            }
            if (MyObject.Mypicturebox != null)
            {
                MyObject.Mypicturebox.Height = MyObject.Mypicturebox.Height - size;
                MyObject.Mypicturebox.Width = MyObject.Mypicturebox.Width - size;
                MyObject.MyPicBoxMagnification1 = (float)MyObject.Mypicturebox.Height / initHeight;
            }
        }


        #region 保存功能的实现
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool BitBlt(//这个只是用来截图的
         IntPtr hdcDest,             //目的DC的句柄 
         int nXDest,             //目的图形的左上角的x坐标 
         int nYDest,                  //目的图形的左上角的y坐标 
         int nWidth,       //目的图形的矩形宽度 
         int nHeight,       //目的图形的矩形高度 
         IntPtr hdcSrc,       //源DC的句柄 
         int nXSrc,        //源图形的左上角的x坐标 
         int nYSrc,          //源图形的左上角的x坐标 
         System.Int32 dwRop          //光栅操作代码 
         );
        Bitmap MyImage;
        int h, w;
        private void buttonItem26_Click(object sender, EventArgs e)
        {
            if (this.panel1.HorizontalScroll.Visible)
                w = this.panel1.HorizontalScroll.Maximum;
            else
                w = this.panel1.Size.Width;
            if (this.panel1.VerticalScroll.Visible)
                h = this.panel1.VerticalScroll.Maximum;
            else
                h = this.panel1.Size.Height;
            Graphics g1 = this.panel1.CreateGraphics();              //获得窗体图形对象 
            MyImage = new Bitmap(w, h, g1);
            Graphics g2 = Graphics.FromImage(MyImage);  //创建位图图形对象 
            g2.Clear(this.panel1.BackColor);
            this.panel1.VerticalScroll.Value = 0;
            this.panel1.HorizontalScroll.Value = 0;
            foreach (Control c in this.panel1.Controls)//把每个控件都绘上去
            {
                Bitmap b = new Bitmap(c.Width, c.Height);
                c.DrawToBitmap(b, new Rectangle(new Point(0, 0), c.Size));
                //c.Location = new Point(c.Location .X,c.Location .Y +this.panel1 .VerticalScroll.
                g2.DrawImage(b, c.Location);
            }
            IntPtr dc1 = g1.GetHdc();     //获得窗体的上下文设备 
            IntPtr dc2 = g2.GetHdc();      //获得位图文件的上下文设备 
            g1.ReleaseHdc(dc1);      //释放窗体的上下文设备 
            g2.ReleaseHdc(dc2);     //释放位图文件的上下文设备 

            SaveFileDialog sa = new SaveFileDialog();
            sa.Filter = "Jpg 图片|*.jpg|Bmp 图片|*.bmp|Gif 图片|*.gif|Png 图片|*.png|Wmf  图片|*.wmf";
            if (sa.ShowDialog() == DialogResult.OK)
            {
                MyImage.Save(sa.FileName, ImageFormat.Jpeg);     //保存为jpeg文件 

            }
        }

        #endregion 保存功能

        #region 实现打印的代码
        private void buttonItem6_Click(object sender, EventArgs e)
        {
            //打印
            #region 先截屏（不是简单的截屏）
            if (this.panel1.HorizontalScroll.Visible)
                w = this.panel1.HorizontalScroll.Maximum;
            else
                w = this.panel1.Size.Width;
            if (this.panel1.VerticalScroll.Visible)
                h = this.panel1.VerticalScroll.Maximum;
            else
                h = this.panel1.Size.Height;
            Graphics g1 = this.panel1.CreateGraphics();              //获得窗体图形对象 
            MyImage = new Bitmap(w, h, g1);
            Graphics g2 = Graphics.FromImage(MyImage);  //创建位图图形对象 
            g2.Clear(this.panel1.BackColor);
            this.panel1.VerticalScroll.Value = 0;
            this.panel1.HorizontalScroll.Value = 0;
            foreach (Control c in this.panel1.Controls)//把每个控件都绘上去
            {
                Bitmap b = new Bitmap(c.Width, c.Height);
                c.DrawToBitmap(b, new Rectangle(new Point(0, 0), c.Size));
                //c.Location = new Point(c.Location .X,c.Location .Y +this.panel1 .VerticalScroll.
                g2.DrawImage(b, c.Location);
            }
            IntPtr dc1 = g1.GetHdc();     //获得窗体的上下文设备 
            IntPtr dc2 = g2.GetHdc();      //获得位图文件的上下文设备 
            g1.ReleaseHdc(dc1);      //释放窗体的上下文设备 
            g2.ReleaseHdc(dc2);     //释放位图文件的上下文设备  

            #endregion
            try
            {
                //打印
                printDialog1.Document = printDocument1;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            catch
            {
                MessageBox.Show("打印意外出错,程序将重启！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Restart();
            }
        }

        #endregion

        #endregion

        #region　实现打印预览的代码
        private void buttonItem5_Click(object sender, EventArgs e)
        {
            //打印预览
            #region 先截屏（不是简单的截屏）
            if (this.panel1.HorizontalScroll.Visible)
                w = this.panel1.HorizontalScroll.Maximum;
            else
                w = this.panel1.Size.Width;
            if (this.panel1.VerticalScroll.Visible)
                h = this.panel1.VerticalScroll.Maximum;
            else
                h = this.panel1.Size.Height;
            Graphics g1 = this.panel1.CreateGraphics();              //获得窗体图形对象 
            MyImage = new Bitmap(w, h, g1);
            Graphics g2 = Graphics.FromImage(MyImage);  //创建位图图形对象 
            g2.Clear(this.panel1.BackColor);
            this.panel1.VerticalScroll.Value = 0;
            this.panel1.HorizontalScroll.Value = 0;
            foreach (Control c in this.panel1.Controls)//把每个控件都绘上去
            {
                Bitmap b = new Bitmap(c.Width, c.Height);
                c.DrawToBitmap(b, new Rectangle(new Point(0, 0), c.Size));
                //c.Location = new Point(c.Location .X,c.Location .Y +this.panel1 .VerticalScroll.
                g2.DrawImage(b, c.Location);
            }
            IntPtr dc1 = g1.GetHdc();     //获得窗体的上下文设备 
            IntPtr dc2 = g2.GetHdc();      //获得位图文件的上下文设备 
            g1.ReleaseHdc(dc1);      //释放窗体的上下文设备 
            g2.ReleaseHdc(dc2);     //释放位图文件的上下文设备  

            #endregion
            PrintPreviewDialog dialog = new PrintPreviewDialog();
            dialog.Document = printDocument1;
            dialog.PrintPreviewControl.AutoZoom = true;
            dialog.ShowDialog();
        }


        private void buttonItem4_Click(object sender, EventArgs e)
        {
            //菜单栏的保存为图片被单击就调用工具栏的函数
            buttonItem26_Click(sender, e);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                float quotient = 1;
                float margin = 20;
                float page_w = e.PageBounds.Width - (2 * margin);
                float page_h = e.PageBounds.Height - (2 * margin);
                if (MyImage.Width >= MyImage.Height)
                {
                    quotient = page_w / MyImage.Width;
                }
                if (MyImage.Width < MyImage.Height)
                {
                    quotient = MyImage.Height / page_h;
                }
                float w = page_w;
                float h = MyImage.Height * quotient;
                e.Graphics.DrawImage(MyImage, margin, margin, w, h);
            }
            catch
            {
                MessageBox.Show("打印意外出错！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        #endregion

        

        #region 调用读取数据的窗体

        private void buttonX9_Click(object sender, EventArgs e)
        {
            //读取数据
            try
            {

                int SeriesCount = MyObject.My_Chart1.Series.Count;
                switch (SeriesCount)
                {
                    case 1:
                        if (MyObject.My_Chart1.Name.Equals("DivPieChart") || MyObject.My_Chart1.Name.Equals("3DDivPieChart") || MyObject.My_Chart1.Name.Equals("DivRingChart"))
                        {
                            //三维分离型饼图,分离型饼图,分离型圆环图
                            ReadData df = new ReadData();
                            df.ShowDialog();
                            foreach (DataPoint point in MyObject.My_Chart1.Series[0].Points)
                            {
                                point["Exploded"] = "true";
                            }
                        }
                        else if (MyObject.My_Chart1.Name.Equals("PieChart") || MyObject.My_Chart1.Name.Equals("3DPieChart") || MyObject.My_Chart1.Name.Equals("RingChart"))
                        {
                            ReadData df = new ReadData();
                            df.ShowDialog();
                        }
                        else
                        {
                            ReadDataAll r = new ReadDataAll();
                            r.ShowDialog();
                        }
                        break;
                    case 2:
                        if (MyObject.My_Chart1.Name.Equals("递减曲线"))
                        {
                            ReadData df = new ReadData();
                            df.ShowDialog();
                        }
                        else
                        {
                            ReadDataAll df = new ReadDataAll();
                            df.ShowDialog();
                        }
                        break;
                    case 3:
                        if (MyObject.My_Chart1.Name.Equals("生产开发曲线"))
                        {
                            ReadData3 f4 = new ReadData3();
                            f4.Show();
                        }
                        else
                        {
                            ReadData2 f3 = new ReadData2();
                            f3.Show();
                        }
                        break;
                    default:
                        ReadData4 f1 = new ReadData4();
                        f1.ShowDialog();
                        break;

                }


            }
            catch
            {
                if (MyObject.FrmName2 != null)
                {
                    string formname = MyObject.FrmName2;
                    switch (formname)
                    {
                        case "主图":
                            Data_WellLog fwell = new Data_WellLog();
                            fwell.ShowDialog();
                            pselfrefresh();
                            break;
                        case "测井曲线":
                            Data_WellLog fwell1 = new Data_WellLog();
                            fwell1.ShowDialog();
                            pselfrefresh();
                            break;
                        case "粒度概率累计曲线":
                            ldglljqx读取数据 ldg = new ldglljqx读取数据();
                            ldg.ShowDialog();
                            pselfrefresh3();
                            break;
                        case "油气水":
                            buttonsetclick(sender, e);
                            break;
                        case "C_M1":
                            Data_Special dsp1 = new Data_Special();
                            dsp1.ShowDialog();
                            pselfrefresh1();
                            break;

                        case "C_M2":
                            Data_Special dsp2 = new Data_Special();
                            dsp2.ShowDialog();
                            pselfrefresh2();
                            break;


                    }
                }
                else
                    MessageBox.Show("读取数据前，请先选中您要读取数据的那张图(单击要选中的图即可)", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        #endregion


        #region 实现模版功能的代码
        private void buttonItem3_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem2_Click(object sender, EventArgs e)////////////、、、、、、、、、、、、、、、、/8.29.修改
        {
            //打开模版
            try
            {
                OpenFileDialog diag = new OpenFileDialog();
                diag.Filter = "All files (*.*)|*.xml";
                if (diag.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XmlHelper.LoadFromXml(chart1[gap[0]], diag.FileName);
                        chart1[gap[0]].Location = new Point(initPointX + getPianyiliang(), initPointY);
                        this.panel1.Controls.Add(chart1[gap[0]]);
                        gap[0]++;
                    }
                    catch { }

                    try
                    {
                        FormState fs = XmlHelper.LoadForm(diag.FileName);
                        switch (fs.FrmName)
                        {
                            case "萨胡成因判别函数":
                                SadhuCauses shcy_form = new SadhuCauses();
                                shcy_form.MyText = fs.TitleText;
                                shcy_form.MyColor = fs.TitleColor;
                                shcy_form.MyFont = fs.TitleFont;

                                shcy_form.FormBorderStyle = FormBorderStyle.None;
                                shcy_form.MouseDown += new System.Windows.Forms.MouseEventHandler(this.萨胡成因判别函数_MouseDown);
                                shcy_form.MouseMove += new System.Windows.Forms.MouseEventHandler(this.萨胡成因判别函数_MouseMove);
                                shcy_form.TopLevel = false;
                                shcy_form.Location = new Point(0, 0);//位置排列
                                this.panel1.Controls.Add(shcy_form);  //然后将该窗体嵌入主界面
                                shcy_form.ShowDialog();
                                break;
                            case "C_M1":
                                C_M1 cm1_form = new C_M1();
                                cm1_form.MyLineColor1 = fs.PenColor1;
                                cm1_form.MyLineColor2 = fs.PenColor2;
                                cm1_form.MyFont = fs.TitleFont;
                                cm1_form.MyLine1 = fs.PenSize1;
                                cm1_form.MyText = fs.TitleText;
                                cm1_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cm1_form.MyXBrush = fs.XBrush;
                                cm1_form.MyYBrush = fs.YBrush;
                                cm1_form.MyXFont = fs.XFont;
                                cm1_form.MyYFont = fs.YFont;
                                cm1_form.MyXnameColor = fs.XnameColor;
                                cm1_form.MyXnameFont = fs.XnameFont;
                                cm1_form.MyXnameText = fs.XnameText;
                                cm1_form.MyYnameColor = fs.YnameColor;
                                cm1_form.MyYnameFont = fs.YnameFont;
                                cm1_form.MyYnameText = fs.YnameText;
                                cm1_form.MyXCheck = fs.XCheck;
                                cm1_form.MyYCheck = fs.YCheck;

                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cm1_form.FormBorderStyle = FormBorderStyle.None;
                                cm1_form.MouseDown += new MouseEventHandler(this.C_M1_MouseDown);
                                cm1_form.MouseMove += new MouseEventHandler(this.C_M1_MouseMove);
                                cm1_form.TopLevel = false;
                                cm1_form.Location = new Point(0, 0);//位置排列
                                this.panel1.Controls.Add(cm1_form);  //然后将该窗体嵌入主界面

                                cm1_form.Show();

                                break;
                            case "C_M2":
                                C_M2 cm2_form = new C_M2();

                                cm2_form.MyLineColor1 = fs.PenColor1;
                                cm2_form.MyLineColor2 = fs.PenColor2;
                                cm2_form.MyFont = fs.TitleFont;
                                cm2_form.MyLine1 = fs.PenSize1;
                                cm2_form.MyText = fs.TitleText;
                                cm2_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cm2_form.MyXBrush = fs.XBrush;
                                cm2_form.MyYBrush = fs.YBrush;
                                cm2_form.MyXFont = fs.XFont;
                                cm2_form.MyYFont = fs.YFont;
                                cm2_form.MyXnameColor = fs.XnameColor;
                                cm2_form.MyXnameFont = fs.XnameFont;
                                cm2_form.MyXnameText = fs.XnameText;
                                cm2_form.MyYnameColor = fs.YnameColor;
                                cm2_form.MyYnameFont = fs.YnameFont;
                                cm2_form.MyYnameText = fs.YnameText;
                                cm2_form.MyXCheck = fs.XCheck;
                                cm2_form.MyYCheck = fs.YCheck;

                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cm2_form.FormBorderStyle = FormBorderStyle.None;
                                cm2_form.MouseDown += new MouseEventHandler(this.C_M2_MouseDown);
                                cm2_form.MouseMove += new MouseEventHandler(this.C_M2_MouseMove);
                                cm2_form.TopLevel = false;
                                cm2_form.Location = new Point(0, 0);//位置排列
                                this.panel1.Controls.Add(cm2_form);  //然后将该窗体嵌入主界面
                                cm2_form.Show();

                                break;
                            case "粒度概率累计曲线":
                                Grabularity ldgl_form = new Grabularity();

                                ldgl_form.MyLineColor1 = fs.PenColor1;
                                ldgl_form.MyLineColor2 = fs.PenColor2;
                                ldgl_form.MyFont = fs.TitleFont;
                                ldgl_form.MyLine1 = fs.PenSize1;
                                ldgl_form.MyText = fs.TitleText;
                                ldgl_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                ldgl_form.MyXBrush = fs.XBrush;
                                ldgl_form.MyYBrush = fs.YBrush;
                                ldgl_form.MyXFont = fs.XFont;
                                ldgl_form.MyYFont = fs.YFont;
                                ldgl_form.MyXnameColor = fs.XnameColor;
                                ldgl_form.MyXnameFont = fs.XnameFont;

                                ldgl_form.MyYnameColor = fs.YnameColor;
                                ldgl_form.MyYnameFont = fs.YnameFont;


                                SysData.ldglljqx_dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                ldgl_form.FormBorderStyle = FormBorderStyle.None;
                                ldgl_form.MouseDown += new MouseEventHandler(this.ldgl_MouseDown);
                                ldgl_form.MouseMove += new MouseEventHandler(this.ldgl_MouseMove);
                                ldgl_form.TopLevel = false;
                                ldgl_form.Location = new Point(0, 0);//位置排列
                                this.panel1.Controls.Add(ldgl_form);  //然后将该窗体嵌入主界面
                                ldgl_form.Show();

                                break;

                            case "测井曲线":
                                WellLog cjqx_form = new WellLog();
                                cjqx_form.MyLineColor1 = fs.PenColor1;
                                cjqx_form.MyLineColor2 = fs.PenColor2;
                                cjqx_form.MyFont = fs.TitleFont;
                                cjqx_form.MyLine1 = fs.PenSize1;
                                cjqx_form.MyText = fs.TitleText;
                                cjqx_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cjqx_form.MyXBrush = fs.XBrush;
                                cjqx_form.MyYBrush = fs.YBrush;
                                cjqx_form.MyXFont = fs.XFont;
                                cjqx_form.MyYFont = fs.YFont;
                                cjqx_form.MyXnameColor = fs.XnameColor;
                                cjqx_form.MyXnameFont = fs.XnameFont;

                                cjqx_form.MyYnameColor = fs.YnameColor;
                                cjqx_form.MyYnameFont = fs.YnameFont;


                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cjqx_form.FormBorderStyle = FormBorderStyle.None;
                                cjqx_form.TopLevel = false;
                                cjqx_form.Location = new Point(0, 0);//位置排列
                                cjqx_form.MouseDown += new MouseEventHandler(this.cjqx_MouseDown);
                                cjqx_form.MouseMove += new MouseEventHandler(this.cjqx_MouseMove);
                                this.panel1.Controls.Add(cjqx_form);
                                cjqx_form.Show();


                                ////////////////////////////////////////////////////////////////////////////
                                cjqx_form.Panel_refresh += zu.Panel_refresh;//添加重绘委托事件
                                zu.DrawRedline += cjqx_form.DrawRedline;
                                zu.Update_draw += cjqx_form.update_draw;
                                zu.Selfrefresh += cjqx_form.selfrefresh;
                                ////////////////////////////////////////////////////////////////////////////
                                break;

                            case "油气水":
                                OilWater OilWater_form = new OilWater();
                                OilWater_form.MyLineColor1 = fs.PenColor1;
                                OilWater_form.MyLineColor2 = fs.PenColor2;
                                OilWater_form.MyFont = fs.TitleFont;
                                OilWater_form.MyLine1 = fs.PenSize1;
                                OilWater_form.MyText = fs.TitleText;
                                OilWater_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                OilWater_form.MyXBrush = fs.XBrush;
                                OilWater_form.MyYBrush = fs.YBrush;
                                OilWater_form.MyXFont = fs.XFont;
                                OilWater_form.MyYFont = fs.YFont;
                                OilWater_form.MyXnameColor = fs.XnameColor;
                                OilWater_form.MyXnameFont = fs.XnameFont;
                                OilWater_form.MyXnameText = fs.XnameText;
                                OilWater_form.MyYnameColor = fs.YnameColor;
                                OilWater_form.MyYnameFont = fs.YnameFont;
                                OilWater_form.MyYnameText = fs.YnameText;
                                OilWater_form.MyXCheck = fs.XCheck;
                                OilWater_form.MyYCheck = fs.YCheck;

                                // SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                OilWater_form.FormBorderStyle = FormBorderStyle.None;
                                OilWater_form.MouseDown += new MouseEventHandler(this.ldgl_MouseDown);
                                OilWater_form.MouseMove += new MouseEventHandler(this.ldgl_MouseMove);
                                OilWater_form.TopLevel = false;
                                OilWater_form.Location = new Point(0, 0);//位置排列
                                this.panel1.Controls.Add(OilWater_form);  //然后将该窗体嵌入主界面
                                OilWater_form.Show();

                                break;

                            case "主图":
                                WellLog_Main cjzt_form = new WellLog_Main();
                                cjzt_form.MyLineColor1 = fs.PenColor1;
                                cjzt_form.MyLineColor2 = fs.PenColor2;
                                cjzt_form.MyFont = fs.TitleFont;
                                cjzt_form.MyLine1 = fs.PenSize1;
                                cjzt_form.MyText = fs.TitleText;
                                cjzt_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cjzt_form.MyXBrush = fs.XBrush;
                                cjzt_form.MyYBrush = fs.YBrush;
                                cjzt_form.MyXFont = fs.XFont;
                                cjzt_form.MyYFont = fs.YFont;
                                cjzt_form.MyXnameColor = fs.XnameColor;
                                cjzt_form.MyXnameFont = fs.XnameFont;
                                cjzt_form.MyXnameText = fs.XnameText;
                                cjzt_form.MyYnameColor = fs.YnameColor;
                                cjzt_form.MyYnameFont = fs.YnameFont;
                                cjzt_form.MyYnameText = fs.YnameText;
                                cjzt_form.MyXCheck = fs.XCheck;
                                cjzt_form.MyYCheck = fs.YCheck;

                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cjzt_form.FormBorderStyle = FormBorderStyle.None;
                                cjzt_form.TopLevel = false;
                                cjzt_form.Location = new Point(0, 0);//位置排列
                                cjzt_form.MouseDown += new MouseEventHandler(this.cjzt_MouseDown);
                                cjzt_form.MouseMove += new MouseEventHandler(this.cjzt_MouseMove);
                                this.panel1.Controls.Add(cjzt_form);
                                cjzt_form.Show();


                                //////////////////////////////////////////////////////////////////////////
                                jht.Panel_refresh += cjzt_form.Panel_refresh;//添加重绘委托事件
                                cjzt_form.DrawRedline += jht.DrawRedline;
                                cjzt_form.Update_draw += jht.update_draw;
                                cjzt_form.Selfrefresh += jht.selfrefresh;
                                //////////////////////////////////////////////////////////////////////////
                                break;

                        }
                    }
                    catch (Exception er) { }
                }
            }
            catch
            {
                MessageBox.Show("模版打开出错！", "出错", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        private void Data_Click(object sender, EventArgs e)
        {

        }

        //private void buttonItem8_Click(object sender, EventArgs e)
        //{
        //    //删除点错的图
        //    删除_Click(sender, e);
        //}

        private void 更改图片类型ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 粘贴_Click(object sender, EventArgs e)
        {

            if (MyObject.My_Chart1 != null)
            {
                this.panel1.Controls.Add(chart1[gap[0]]);
                chart1[gap[0]].Location = new Point(initPointX, initPointY);
                // 把第一附图保存到内存流中
                chart1[gap[0]].Serializer.Content = SerializationContents.All;
                MemoryStream ms = new MemoryStream();
                MyObject.My_Chart1.Serializer.Save(ms);

                // 加载内存流中的数据到第二幅图
                ms.Seek(0, SeekOrigin.Begin); 
                chart1[gap[0]].Serializer.Load(ms);
                ms.Close();
                gap[0]++;

            }

            else
            {
                MessageBox.Show("您未进行复制或剪切操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        //复制粘贴一遍（上边也有部分修改）
        ///////////////////////////////////////特殊图///////////////////////////////////////////////////////////////
        #region 萨胡成因判别函数
        SadhuCauses f = new SadhuCauses();
        private void buttonItem31_Click(object sender, EventArgs e)
        {
            SadhuCauses shcy_form = new SadhuCauses();
            this.pselfrefresh5 += shcy_form.selfrefresh;
            shcy_form.FormBorderStyle = FormBorderStyle.None;
            shcy_form.MouseDown += new System.Windows.Forms.MouseEventHandler(this.萨胡成因判别函数_MouseDown);
            shcy_form.MouseMove += new System.Windows.Forms.MouseEventHandler(this.萨胡成因判别函数_MouseMove);
            shcy_form.TopLevel = false;
            shcy_form.Location = new Point(initPointX + getPianyiliang(), initPointY);//位置排列
            this.panel1.Controls.Add(shcy_form);

            shcy_form.Show();
            ChangeAllBoder();
        }
        private void 萨胡成因判别函数_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEvent.MyMouseDown(sender, e);
        }
        private void 萨胡成因判别函数_MouseMove(object sender, MouseEventArgs e)
        {
            // MouseEvent.MySanDuanYuanMouseMove(sender, e);
            MouseEvent.MyMouseMove(sender, e);
        }

        #endregion

        #region c_m1

        //
        //
        C_M1 xl_cm1 = new C_M1();


        float wide, length;
        Form form_o;
        private void buttonItem35_Click(object sender, EventArgs e)
        {
            //每次点击还原默认值

            SysData.cm1_dt = null;//清空datatable

            SysData.K_Height = 1;
            SysData.K_Width = 1;

            //存放setofline里的设置线性
            SysData.line1 = 1;
            SysData.line2 = 3;
            SysData.line_color = Color.Black;
            SysData.title = "双击图形在属性中修改图题";
            SysData.title_color = Color.Black;
            SysData.title_font = new Font("宋体", 12, FontStyle.Regular);

            C_M1 cm1_form = new C_M1();
            this.pselfrefresh1 += cm1_form.selfrefresh;

            cm1_form.FormBorderStyle = FormBorderStyle.None;
            cm1_form.TopLevel = false;
            cm1_form.Location = new Point(initPointX + getPianyiliang(), initPointY);//位置排列
            cm1_form.MouseDown += new MouseEventHandler(this.C_M1_MouseDown);
            cm1_form.MouseMove += new MouseEventHandler(this.C_M1_MouseMove);
            this.panel1.Controls.Add(cm1_form);
            wide = cm1_form.Width;//记录宽度
            length = cm1_form.Height;//记录长
            form_o = cm1_form;//传参
            xl_cm1 = cm1_form;
            //if (SysData.dt == null)
            //{
            //    特殊图读取数据 dqsj_form = new 特殊图读取数据();
            //    dqsj_form.ShowDialog();
            //}

            cm1_form.Show();

            ChangeAllBoder();
        }
        private void C_M1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEvent.MyMouseDown(sender, e);
        }
        private void C_M1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEvent.MyMouseMove(sender, e);
            if (form_o != null)
            {
                SysData.K_Width = form_o.Width / wide;
                SysData.K_Height = form_o.Height / length;
            }
            //pselfrefresh1();
        }

        #endregion

        #region c_m2
        C_M2 xl_cm2 = new C_M2();
        private void buttonItem36_Click(object sender, EventArgs e)
        {
            //每次点击还原默认值

            SysData.cm2_dt = null;//清空datatable

            SysData.K_Height = 1;
            SysData.K_Width = 1;

            //存放setofline里的设置线性
            SysData.line1 = 1;
            SysData.line2 = 3;
            SysData.line_color = Color.Black;
            SysData.title = "双击图形在属性中修改图题";
            SysData.title_color = Color.Black;
            SysData.title_font = new Font("宋体", 12, FontStyle.Regular);


            C_M2 cm2_form = new C_M2();
            this.pselfrefresh2 += cm2_form.selfrefresh;

            cm2_form.FormBorderStyle = FormBorderStyle.None;
            cm2_form.TopLevel = false;
            cm2_form.Location = new Point(initPointX + getPianyiliang(), initPointY);//位置排列
            cm2_form.MouseDown += new MouseEventHandler(this.C_M2_MouseDown);
            cm2_form.MouseMove += new MouseEventHandler(this.C_M2_MouseMove);
            this.panel1.Controls.Add(cm2_form);
            wide = cm2_form.Width;//记录宽度
            length = cm2_form.Height;//记录长
            form_o = cm2_form;//传参
            xl_cm2 = cm2_form;
            //if (SysData.dt == null)
            //{
            //    特殊图读取数据 dqsj_form = new 特殊图读取数据();
            //    dqsj_form.ShowDialog();
            //}

            cm2_form.Show();
            ChangeAllBoder();
        }

        private void C_M2_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEvent.MyMouseDown(sender, e);
        }
        private void C_M2_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEvent.MyMouseMove(sender, e);
            if (form_o != null)
            {
                SysData.K_Width = form_o.Width / wide;
                SysData.K_Height = form_o.Height / length;
            }

        }

        #endregion

        #region 概率密度
        Grabularity xl_ldglqx = new Grabularity();
        private void buttonItem33_Click(object sender, EventArgs e)
        {
            //每次点击还原默认值
            SysData.ldglljqx_dt = null;//清空datatable


            SysData.K_Height = 1;
            SysData.K_Width = 1;

            //存放setofline里的设置线性
            SysData.line1 = 1;
            SysData.line2 = 3;
            SysData.line_color = Color.Black;
            SysData.title = "双击图形在属性中修改图题";
            SysData.title_color = Color.Black;
            SysData.title_font = new Font("宋体", 12, FontStyle.Regular);


            Grabularity ldgl_form = new Grabularity();
            this.pselfrefresh3 += ldgl_form.selfrefresh;
            ldgl_form.FormBorderStyle = FormBorderStyle.None;
            ldgl_form.TopLevel = false;
            ldgl_form.Location = new Point(initPointX + getPianyiliang(), initPointY);//位置排列
            ldgl_form.MouseDown += new MouseEventHandler(this.ldgl_MouseDown);
            ldgl_form.MouseMove += new MouseEventHandler(this.ldgl_MouseMove);
            this.panel1.Controls.Add(ldgl_form);
            wide = ldgl_form.Width;//记录宽度
            length = ldgl_form.Height;//记录长
            form_o = ldgl_form;//传参
            xl_ldglqx = ldgl_form;
            //if (SysData.dt == null)
            //{
            //    ldglljqx读取数据 ldsj = new ldglljqx读取数据();
            //    ldsj.ShowDialog();
            //}

            ldgl_form.Show();
            ChangeAllBoder();
        }


        private void ldgl_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEvent.MyMouseDown(sender, e);
        }
        private void ldgl_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEvent.MyMouseMove(sender, e);
            SysData.K_Width = form_o.Width / wide;
            SysData.K_Height = form_o.Height / length;

        }

        #endregion

        #region 油气水
        OilWater xl_oilwater = new OilWater();
        private void buttonItem34_Click(object sender, EventArgs e)
        {
            //每次点击还原默认值

            SysData.K_Height = 1;
            SysData.K_Width = 1;

            //存放setofline里的设置线性
            SysData.line1 = 1;
            SysData.line2 = 3;
            SysData.line_color = Color.Black;
            SysData.title = "双击图形在属性中修改图题";
            SysData.title_color = Color.Black;
            SysData.title_font = new Font("宋体", 12, FontStyle.Regular);


            OilWater OilWater_form = new OilWater();
            this.buttonsetclick += OilWater_form.button_Set_Click;
            this.pselfrefresh4 += OilWater_form.selfrefresh;
            OilWater_form.FormBorderStyle = FormBorderStyle.None;
            OilWater_form.TopLevel = false;
            OilWater_form.Location = new Point(initPointX + getPianyiliang(), initPointY);//位置排列
            OilWater_form.MouseDown += new MouseEventHandler(this.OilWater_MouseDown);
            OilWater_form.MouseMove += new MouseEventHandler(this.OilWater_MouseMove);
            this.panel1.Controls.Add(OilWater_form);
            wide = OilWater_form.Width;//记录宽度
            length = OilWater_form.Height;//记录长
            form_o = OilWater_form;//传参
            xl_oilwater = OilWater_form;


            OilWater_form.Show();
            ChangeAllBoder();
        }
        private void OilWater_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEvent.MyMouseDown(sender, e);
        }
        private void OilWater_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEvent.MyMouseMove(sender, e);
            SysData.K_Width = form_o.Width / wide;
            SysData.K_Height = form_o.Height / length;

        }

        #endregion


        #region 交汇图

        WellLog jht;
        WellLog_Main zu;
        private void buttonItem37_Click(object sender, EventArgs e)
        {

            //每次点击还原默认值

            SysData.dt = null;//清空datatable

            SysData.K_Height = 1;
            SysData.K_Width = 1;

            //存放setofline里的设置线性
            SysData.line1 = 1;
            SysData.line2 = 3;
            SysData.line_color = Color.Black;
            SysData.title = "双击图形在属性中修改图题";
            SysData.title_color = Color.Black;
            SysData.title_font = new Font("宋体", 12, FontStyle.Regular);



            WellLog_Main cjzt_form = new WellLog_Main();
            this.pselfrefresh += cjzt_form.Panel_refresh;


            cjzt_form.FormBorderStyle = FormBorderStyle.None;
            cjzt_form.TopLevel = false;
            cjzt_form.Location = new Point(initPointX + getPianyiliang(), initPointY);//位置排列
            cjzt_form.MouseDown += new MouseEventHandler(this.cjzt_MouseDown);
            cjzt_form.MouseMove += new MouseEventHandler(this.cjzt_MouseMove);
            this.panel1.Controls.Add(cjzt_form);

            zu = cjzt_form;

            WellLog cjqx_form = new WellLog();
            this.pselfrefresh += cjqx_form.selfrefresh;

            cjqx_form.FormBorderStyle = FormBorderStyle.None;
            cjqx_form.TopLevel = false;
            cjqx_form.Location = new Point(cjzt_form.Location.X + cjzt_form.Width, cjzt_form.Location.Y);//位置排列
            cjqx_form.MouseDown += new MouseEventHandler(this.cjqx_MouseDown);
            cjqx_form.MouseMove += new MouseEventHandler(this.cjqx_MouseMove);
            this.panel1.Controls.Add(cjqx_form);

            jht = cjqx_form;
            ////////////////////////////////////////////////////////////////////////////
            cjqx_form.Panel_refresh += cjzt_form.Panel_refresh;//添加重绘委托事件
            cjzt_form.DrawRedline += cjqx_form.DrawRedline;
            cjzt_form.Update_draw += cjqx_form.update_draw;
            cjzt_form.Selfrefresh += cjqx_form.selfrefresh;
            ////////////////////////////////////////////////////////////////////////////


            cjqx_form.Show();
            cjzt_form.Show();
            //cjsj.Show();

            ChangeAllBoder();
        }


        private void cjqx_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEvent.MyMouseDown(sender, e);
        }
        private void cjqx_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEvent.MyMouseMove(sender, e);
        }
        private void cjzt_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEvent.MyMouseDown(sender, e);
        }
        private void cjzt_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEvent.MyMouseMove(sender, e);

        }
        #endregion

        private void buttonX10_Click(object sender, EventArgs e)
        {
            //读取数据
            try
            {

                int SeriesCount = MyObject.My_Chart1.Series.Count;
                switch (SeriesCount)
                {

                    case 1:
                        ReadDataAll f = new ReadDataAll();
                        f.ShowDialog();
                        break;
                    case 2:
                        if (MyObject.My_Chart1.Name.Equals("递减曲线"))
                        {
                            ReadDataAll df = new ReadDataAll();
                            df.ShowDialog();
                        }
                        else
                        {
                            ReadData2 f2 = new ReadData2();
                            f2.ShowDialog();
                        }
                        break;
                    case 3:
                        if (MyObject.My_Chart1.Name.Equals("生产开发曲线"))
                        {
                            ReadData3 f4 = new ReadData3();
                            f4.Show();
                        }
                        else
                        {
                            ReadData2 f3 = new ReadData2();
                            f3.Show();
                        }
                        break;
                    default:
                        ReadDataAll f1 = new ReadDataAll();
                        f1.ShowDialog();
                        break;

                }


            }
            catch
            {
                if (MyObject.FrmName2 != null)
                {
                    string formname = MyObject.FrmName2;
                    switch (formname)
                    {
                        case "主图":
                            Data_WellLog fwell = new Data_WellLog();
                            fwell.ShowDialog();
                            pselfrefresh();
                            break;
                        case "测井曲线":
                            ReadDataFromDataBase readwell = new ReadDataFromDataBase();
                            readwell.ShowDialog();
                            pselfrefresh();
                            break;
                        case "粒度概率累计曲线":
                            ldglljqx读取数据 ldg = new ldglljqx读取数据();
                            ldg.ShowDialog();
                            pselfrefresh3();
                            break;
                        case "油气水":
                            buttonsetclick(sender, e);
                            break;
                        case "C_M1":
                            Data_Special dsp1 = new Data_Special();
                            dsp1.ShowDialog();
                            pselfrefresh1();
                            break;

                        case "C_M2":
                            Data_Special dsp2 = new Data_Special();
                            dsp2.ShowDialog();
                            pselfrefresh2();
                            break;


                    }
                }
                else
                    MessageBox.Show("读取数据前，请先选中您要读取数据的那张图(单击要选中的图即可)", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region 模板到数据库
        private void btnanoSavetoDB_Click(object sender, EventArgs e)//另存模板到数据库
        {
            if (this.panel1.Controls.Count < 1)
            {
                MessageBox.Show("你还没绘制图件，不能另存为模板到数据库");
                return;
            }
            Save();
            if (this.panel1.Controls.Count > 0)
            {
                MyObject.aa1 = new byte[MyObject.k1];
                Buffer.BlockCopy(MyObject.bb1, 0, MyObject.aa1, 0, MyObject.k1);
                ProjectTemplate tempTemplate;
                if (Ymhdo.Project.SaveTemplateAs(ShareInfo.GeoAuxMapNew, out tempTemplate))
                {
                    tempTemplate.Data = MyObject.aa1;
                    tempTemplate.Save();
                }
            }
        }

        private void Save()
        {
            MyObject.Chartnum1 = 0;
            MyObject.bb1 = new byte[1000000];
            MyObject.k1 = 0;
            double  w,h;
            if (this.panel1 .HorizontalScroll.Value >0)
            {
                w = this.panel1.HorizontalScroll.Value;
            }
            else
            {
                w = 0;
            }
            if (this.panel1.VerticalScroll.Value >0)
            {
                h = this.panel1.VerticalScroll.Value;
            }
            else
            {
                h = 0;
            }

            
            foreach (Control item in this.panel1.Controls)
            {
                if (item.GetType().Name == "Chart")
                {
                    Chart chart1 = (Chart)item;
                    byte[] by = XmlHelper.SaveChart2DB(chart1);
                    MyObject.cc1 = new byte[6] { (byte)(by.Length / 256), (byte)(by.Length % 256), (byte)((item.Location.X + w) / 256), (byte)((item.Location.X + w) % 256), (byte)((item.Location.Y + h) / 256), (byte)((item.Location.Y + h) % 256)};
                    int p = Convert.ToInt32(MyObject.cc1[0]);
                    int q = Convert.ToInt32(MyObject.cc1[1]);
                    if (MyObject.Chartnum1 == 0)
                    {
                        Array.Copy(MyObject.cc1, 0, MyObject.bb1, 0, MyObject.cc1.Length);
                        Array.Copy(by, 0, MyObject.bb1, 6, by.Length);
                        MyObject.k1 += 6 + by.Length;
                    }
                    else
                    {
                        Array.Copy(MyObject.cc1, 0, MyObject.bb1, MyObject.k1, MyObject.cc1.Length);
                        Array.Copy(by, 0, MyObject.bb1, MyObject.k1 + 6, by.Length);
                        MyObject.k1 += 6 + by.Length;
                    }
                    MyObject.Chartnum1++;
                }
                else
                {
                    switch (item.Text)
                    {
                        case "SanHanLiChenJiWu":
                            FormState fs8 = new FormState(shlcj.cP ,shlcj.MyBrush ,shlcj.LabelFont, "SanHanLiChenJiWu");
                            byte[] by8 = XmlHelper.SaveSpecial2DB(fs8);
                            MyObject.cc1 = new byte[7] { (byte)(255), (byte)(by8.Length / 256), (byte)(by8.Length % 256), (byte)(item.Location.X / 256), (byte)(item.Location.X % 256), (byte)(item.Location.Y / 256), (byte)(item.Location.Y % 256) };
                            if (MyObject.Chartnum1 == 0)
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, 0, MyObject.cc1.Length);
                                Array.Copy(by8, 0, MyObject.bb1, 7, by8.Length);
                                MyObject.k1 += 7 + by8.Length;
                            }
                            else
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, MyObject.k1, MyObject.cc1.Length);
                                Array.Copy(by8, 0, MyObject.bb1, MyObject.k1 + 7, by8.Length);
                                MyObject.k1 += 7 + by8.Length;
                            }

                            MyObject.Chartnum1++;
                            break;
                        case "主图":
                            FormState fs = new FormState(zu.MyText, zu.MyColor, zu.MyFont, zu.MyLineColor1, zu.MyLineColor2, zu.MyLine1, zu.MyLine2, "主图", SysData.dt,
                              zu.MyXBrush, zu.MyYBrush, zu.MyXFont, zu.MyYFont, zu.MyXnameFont, zu.MyXnameColor, zu.MyXnameText, zu.MyYnameFont,
                              zu.MyYnameColor, zu.MyYnameText, zu.MyXCheck, zu.MyYCheck);
                            byte[] by = XmlHelper.SaveSpecial2DB(fs);
                            MyObject.cc1 = new byte[7] { (byte)(255), (byte)(by.Length / 256), (byte)(by.Length % 256), (byte)(item.Location.X / 256), (byte)(item.Location.X % 256), (byte)(item.Location.Y / 256), (byte)(item.Location.Y % 256) };
                            if (MyObject.Chartnum1 == 0)
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, 0, MyObject.cc1.Length);
                                Array.Copy(by, 0, MyObject.bb1, 7, by.Length);
                                MyObject.k1 += 7 + by.Length;
                            }
                            else
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, MyObject.k1, MyObject.cc1.Length);
                                Array.Copy(by, 0, MyObject.bb1, MyObject.k1 + 7, by.Length);
                                MyObject.k1 += 7 + by.Length;
                            }
                            MyObject.Chartnum1++;
                            break;

                        case "萨胡成因判别函数":
                            FormState fs1 = new FormState(f.MyText, f.MyColor, f.MyFont, "萨胡成因判别函数");
                            byte[] by1 = XmlHelper.SaveSpecial2DB(fs1);
                            MyObject.cc1 = new byte[7] { (byte)(255), (byte)(by1.Length / 256), (byte)(by1.Length % 256), (byte)(item.Location.X / 256), (byte)(item.Location.X % 256), (byte)(item.Location.Y / 256), (byte)(item.Location.Y % 256) };
                            if (MyObject.Chartnum1 == 0)
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, 0, MyObject.cc1.Length);
                                Array.Copy(by1, 0, MyObject.bb1, 7, by1.Length);
                                MyObject.k1 += 7 + by1.Length;
                            }
                            else
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, MyObject.k1, MyObject.cc1.Length);
                                Array.Copy(by1, 0, MyObject.bb1, MyObject.k1 + 7, by1.Length);
                                MyObject.k1 += 7 + by1.Length;
                            }
                            MyObject.Chartnum1++;
                            break;

                        case "C_M1":
                            FormState fs2 = new FormState(xl_cm1.MyText, xl_cm1.MyColor, xl_cm1.MyFont, xl_cm1.MyLineColor1, xl_cm1.MyLineColor2, xl_cm1.MyLine1, xl_cm1.MyLine2, "C_M1", SysData.dt,
                                xl_cm1.MyXBrush, xl_cm1.MyYBrush, xl_cm1.MyXFont, xl_cm1.MyYFont, xl_cm1.MyXnameFont, xl_cm1.MyXnameColor, xl_cm1.MyXnameText, xl_cm1.MyYnameFont,
                                xl_cm1.MyYnameColor, xl_cm1.MyYnameText, xl_cm1.MyXCheck, xl_cm1.MyYCheck);
                            byte[] by2 = XmlHelper.SaveSpecial2DB(fs2);
                            MyObject.cc1 = new byte[7] { (byte)(255), (byte)(by2.Length / 256), (byte)(by2.Length % 256), (byte)(item.Location.X / 256), (byte)(item.Location.X % 256), (byte)(item.Location.Y / 256), (byte)(item.Location.Y % 256) };
                            if (MyObject.Chartnum1 == 0)
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, 0, MyObject.cc1.Length);
                                Array.Copy(by2, 0, MyObject.bb1, 7, by2.Length);
                                MyObject.k1 += 7 + by2.Length;
                            }
                            else
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, MyObject.k1, MyObject.cc1.Length);
                                Array.Copy(by2, 0, MyObject.bb1, MyObject.k1 + 7, by2.Length);
                                MyObject.k1 += 7 + by2.Length;
                            }
                            MyObject.Chartnum1++;
                            break;
                        case "C_M2":

                            FormState fs3 = new FormState(xl_cm2.MyText, xl_cm2.MyColor, xl_cm2.MyFont, xl_cm2.MyLineColor1, xl_cm2.MyLineColor2, xl_cm2.MyLine1, xl_cm2.MyLine2, "C_M2", SysData.dt,
                           xl_cm2.MyXBrush, xl_cm2.MyYBrush, xl_cm2.MyXFont, xl_cm2.MyYFont, xl_cm2.MyXnameFont, xl_cm2.MyXnameColor, xl_cm2.MyXnameText, xl_cm2.MyYnameFont,
                           xl_cm2.MyYnameColor, xl_cm2.MyYnameText, xl_cm2.MyXCheck, xl_cm2.MyYCheck);
                            byte[] by3 = XmlHelper.SaveSpecial2DB(fs3);
                            MyObject.cc1 = new byte[7] { (byte)(255), (byte)(by3.Length / 256), (byte)(by3.Length % 256), (byte)(item.Location.X / 256), (byte)(item.Location.X % 256), (byte)(item.Location.Y / 256), (byte)(item.Location.Y % 256) };
                            if (MyObject.Chartnum1 == 0)
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, 0, MyObject.cc1.Length);
                                Array.Copy(by3, 0, MyObject.bb1, 7, by3.Length);
                                MyObject.k1 += 7 + by3.Length;
                            }
                            else
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, MyObject.k1, MyObject.cc1.Length);
                                Array.Copy(by3, 0, MyObject.bb1, MyObject.k1 + 7, by3.Length);
                                MyObject.k1 += 7 + by3.Length;
                            }
                            MyObject.Chartnum1++;
                            break;

                        case "粒度概率累计曲线":
                            FormState fs4 = new FormState(xl_ldglqx.MyText, xl_ldglqx.MyColor, xl_ldglqx.MyFont, xl_ldglqx.MyLineColor1, xl_ldglqx.MyLineColor2, xl_ldglqx.MyLine1, xl_ldglqx.MyLine2, "粒度概率累计曲线", SysData.ldglljqx_dt,
                                    xl_ldglqx.MyXBrush, xl_ldglqx.MyYBrush, xl_ldglqx.MyXFont, xl_ldglqx.MyYFont, xl_ldglqx.MyXnameFont, xl_ldglqx.MyXnameColor, xl_ldglqx.MyYnameFont, xl_ldglqx.MyYnameColor);
                            byte[] by4 = XmlHelper.SaveSpecial2DB(fs4);
                            MyObject.cc1 = new byte[7] { (byte)(255), (byte)(by4.Length / 256), (byte)(by4.Length % 256), (byte)(item.Location.X / 256), (byte)(item.Location.X % 256), (byte)(item.Location.Y / 256), (byte)(item.Location.Y % 256) };
                            if (MyObject.Chartnum1 == 0)
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, 0, MyObject.cc1.Length);
                                Array.Copy(by4, 0, MyObject.bb1, 7, by4.Length);
                                MyObject.k1 += 7 + by4.Length;
                            }
                            else
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, MyObject.k1, MyObject.cc1.Length);
                                Array.Copy(by4, 0, MyObject.bb1, MyObject.k1 + 7, by4.Length);
                                MyObject.k1 += 7 + by4.Length;
                            }
                            MyObject.Chartnum1++;
                            break;
                        case "油气水":
                            FormState fs5 = new FormState(xl_oilwater.MyText, xl_oilwater.MyColor, xl_oilwater.MyFont, xl_oilwater.MyLineColor1, xl_oilwater.MyLineColor2, xl_oilwater.MyLine1, xl_oilwater.MyLine2, "油气水", SysData.dt,
                              xl_oilwater.MyXBrush, xl_oilwater.MyYBrush, xl_oilwater.MyXFont, xl_oilwater.MyYFont, xl_oilwater.MyXnameFont, xl_oilwater.MyXnameColor, xl_oilwater.MyXnameText, xl_oilwater.MyYnameFont,
                              xl_oilwater.MyYnameColor, xl_oilwater.MyYnameText, xl_oilwater.MyXCheck, xl_oilwater.MyYCheck);
                            byte[] by5 = XmlHelper.SaveSpecial2DB(fs5);
                            MyObject.cc1 = new byte[7] { (byte)(255), (byte)(by5.Length / 256), (byte)(by5.Length % 256), (byte)(item.Location.X / 256), (byte)(item.Location.X % 256), (byte)(item.Location.Y / 256), (byte)(item.Location.Y % 256) };
                            if (MyObject.Chartnum1 == 0)
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, 0, MyObject.cc1.Length);
                                Array.Copy(by5, 0, MyObject.bb1, 7, by5.Length);
                                MyObject.k1 += 7 + by5.Length;
                            }
                            else
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, MyObject.k1, MyObject.cc1.Length);
                                Array.Copy(by5, 0, MyObject.bb1, MyObject.k1 + 7, by5.Length);
                                MyObject.k1 += 7 + by5.Length;
                            }
                            MyObject.Chartnum1++;
                            break;
                        case "测井曲线":
                            FormState fs6 = new FormState(jht.MyText, jht.MyColor, jht.MyFont, jht.MyLineColor1, jht.MyLineColor2, jht.MyLine1, jht.MyLine2, "测井曲线", SysData.dt,
                                 jht.MyXBrush, jht.MyYBrush, jht.MyXFont, jht.MyYFont, jht.MyXnameFont, jht.MyXnameColor, jht.MyYnameFont, jht.MyYnameColor);
                            byte[] by6 = XmlHelper.SaveSpecial2DB(fs6);
                            MyObject.cc1 = new byte[7] { (byte)(255), (byte)(by6.Length / 256), (byte)(by6.Length % 256), (byte)(item.Location.X / 256), (byte)(item.Location.X % 256), (byte)(item.Location.Y / 256), (byte)(item.Location.Y % 256) };
                            if (MyObject.Chartnum1 == 0)
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, 0, MyObject.cc1.Length);
                                Array.Copy(by6, 0, MyObject.bb1, 7, by6.Length);
                                MyObject.k1 += 7 + by6.Length;
                            }
                            else
                            {
                                Array.Copy(MyObject.cc1, 0, MyObject.bb1, MyObject.k1, MyObject.cc1.Length);
                                Array.Copy(by6, 0, MyObject.bb1, MyObject.k1 + 7, by6.Length);
                                MyObject.k1 += 7 + by6.Length;
                            }
                            MyObject.Chartnum1++;
                            break;
                        default:
                            break;
                    }
                }

            }
            MyObject.cc1 = new byte[4] { (byte)(-(this.panel1.AutoScrollPosition.X) / 256), (byte)(-(this.panel1.AutoScrollPosition.X) % 256), (byte)(-(this.panel1.AutoScrollPosition.Y) / 256), (byte)(-(this.panel1.AutoScrollPosition.Y) % 256) };
            Array.Copy(MyObject.cc1, 0, MyObject.bb1, MyObject.k1, MyObject.cc1.Length);
            MyObject.k1 += 4;
        }

        private void btnOptpDB_Click(object sender, EventArgs e)//从数据库中打开模板
        {
            AccessModuleSubItem tempItem;
            if (!AccessInfo.CanGetModuleSubItem(ModuleType.CrossPlot, AccessType.OpenTemplate, out tempItem))
            {
                return;
            }
            OpentemplateFromDB otl = new OpentemplateFromDB();
            otl.ShowDialog();
            if (OpentemplateFromDB.Isclose)
            {
                OpenTemp();
            }
            otl.Dispose();
        }

        private void OpenTemp()
        {
            int i = 0, x = 0, y = 0,w,h;
            try
            {
                while (OpentemplateFromDB.bb.Length != 0)
                {
                    if ((OpentemplateFromDB.bb[0]) == (byte)255)
                    {
                        i = Convert.ToInt32((OpentemplateFromDB.bb[1]) * 256 + OpentemplateFromDB.bb[2]);
                        x = Convert.ToInt32((OpentemplateFromDB.bb[3]) * 256 + OpentemplateFromDB.bb[4]);
                        y = Convert.ToInt32((OpentemplateFromDB.bb[5]) * 256 + OpentemplateFromDB.bb[6]);
                        byte[] Byte = new byte[i];
                        Buffer.BlockCopy(OpentemplateFromDB.bb, 7, Byte, 0, i);
                        OpentemplateFromDB.bb = OpentemplateFromDB.bb.Skip(i + 7).Take(OpentemplateFromDB.bb.Length).ToArray();
                        FormState fs = XmlHelper.LoadSpecialFromDB(Byte);
                        switch (fs.FrmName)
                        {
                            case "主图":
                                WellLog_Main cjzt_form = new WellLog_Main();
                                cjzt_form.MyLineColor1 = fs.PenColor1;
                                cjzt_form.MyLineColor2 = fs.PenColor2;
                                cjzt_form.MyFont = fs.TitleFont;
                                cjzt_form.MyLine1 = fs.PenSize1;
                                cjzt_form.MyText = fs.TitleText;
                                cjzt_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cjzt_form.MyXBrush = fs.XBrush;
                                cjzt_form.MyYBrush = fs.YBrush;
                                cjzt_form.MyXFont = fs.XFont;
                                cjzt_form.MyYFont = fs.YFont;
                                cjzt_form.MyXnameColor = fs.XnameColor;
                                cjzt_form.MyXnameFont = fs.XnameFont;
                                cjzt_form.MyXnameText = fs.XnameText;
                                cjzt_form.MyYnameColor = fs.YnameColor;
                                cjzt_form.MyYnameFont = fs.YnameFont;
                                cjzt_form.MyYnameText = fs.YnameText;
                                cjzt_form.MyXCheck = fs.XCheck;
                                cjzt_form.MyYCheck = fs.YCheck;

                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cjzt_form.FormBorderStyle = FormBorderStyle.None;
                                cjzt_form.TopLevel = false;
                                cjzt_form.Location = new Point(x, y);//位置排列
                                cjzt_form.MouseDown += new MouseEventHandler(this.cjzt_MouseDown);
                                cjzt_form.MouseMove += new MouseEventHandler(this.cjzt_MouseMove);
                                this.panel1.Controls.Add(cjzt_form);
                                cjzt_form.Show();


                                //////////////////////////////////////////////////////////////////////////
                                jht.Panel_refresh += cjzt_form.Panel_refresh;//添加重绘委托事件
                                cjzt_form.DrawRedline += jht.DrawRedline;
                                cjzt_form.Update_draw += jht.update_draw;
                                cjzt_form.Selfrefresh += jht.selfrefresh;
                                //////////////////////////////////////////////////////////////////////////
                                break;
                            case "萨胡成因判别函数":
                                SadhuCauses shcy_form = new SadhuCauses();
                                shcy_form.MyText = fs.TitleText;
                                shcy_form.MyColor = fs.TitleColor;
                                shcy_form.MyFont = fs.TitleFont;

                                shcy_form.FormBorderStyle = FormBorderStyle.None;
                                shcy_form.MouseDown += new System.Windows.Forms.MouseEventHandler(this.萨胡成因判别函数_MouseDown);
                                shcy_form.MouseMove += new System.Windows.Forms.MouseEventHandler(this.萨胡成因判别函数_MouseMove);
                                shcy_form.TopLevel = false;
                                shcy_form.Location = new Point(x, y);//位置排列
                                this.panel1.Controls.Add(shcy_form);  //然后将该窗体嵌入主界面
                                shcy_form.Show();
                                break;
                            case "C_M1":
                                C_M1 cm1_form = new C_M1();
                                cm1_form.MyLineColor1 = fs.PenColor1;
                                cm1_form.MyLineColor2 = fs.PenColor2;
                                cm1_form.MyFont = fs.TitleFont;
                                cm1_form.MyLine1 = fs.PenSize1;
                                cm1_form.MyText = fs.TitleText;
                                cm1_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cm1_form.MyXBrush = fs.XBrush;
                                cm1_form.MyYBrush = fs.YBrush;
                                cm1_form.MyXFont = fs.XFont;
                                cm1_form.MyYFont = fs.YFont;
                                cm1_form.MyXnameColor = fs.XnameColor;
                                cm1_form.MyXnameFont = fs.XnameFont;
                                cm1_form.MyXnameText = fs.XnameText;
                                cm1_form.MyYnameColor = fs.YnameColor;
                                cm1_form.MyYnameFont = fs.YnameFont;
                                cm1_form.MyYnameText = fs.YnameText;
                                cm1_form.MyXCheck = fs.XCheck;
                                cm1_form.MyYCheck = fs.YCheck;

                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cm1_form.FormBorderStyle = FormBorderStyle.None;
                                cm1_form.MouseDown += new MouseEventHandler(this.C_M1_MouseDown);
                                cm1_form.MouseMove += new MouseEventHandler(this.C_M1_MouseMove);
                                cm1_form.TopLevel = false;
                                cm1_form.Location = new Point(x, y);//位置排列
                                this.panel1.Controls.Add(cm1_form);  //然后将该窗体嵌入主界面

                                cm1_form.Show();
                                break;
                            case "C_M2":
                                C_M2 cm2_form = new C_M2();

                                cm2_form.MyLineColor1 = fs.PenColor1;
                                cm2_form.MyLineColor2 = fs.PenColor2;
                                cm2_form.MyFont = fs.TitleFont;
                                cm2_form.MyLine1 = fs.PenSize1;
                                cm2_form.MyText = fs.TitleText;
                                cm2_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cm2_form.MyXBrush = fs.XBrush;
                                cm2_form.MyYBrush = fs.YBrush;
                                cm2_form.MyXFont = fs.XFont;
                                cm2_form.MyYFont = fs.YFont;
                                cm2_form.MyXnameColor = fs.XnameColor;
                                cm2_form.MyXnameFont = fs.XnameFont;
                                cm2_form.MyXnameText = fs.XnameText;
                                cm2_form.MyYnameColor = fs.YnameColor;
                                cm2_form.MyYnameFont = fs.YnameFont;
                                cm2_form.MyYnameText = fs.YnameText;
                                cm2_form.MyXCheck = fs.XCheck;
                                cm2_form.MyYCheck = fs.YCheck;

                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cm2_form.FormBorderStyle = FormBorderStyle.None;
                                cm2_form.MouseDown += new MouseEventHandler(this.C_M2_MouseDown);
                                cm2_form.MouseMove += new MouseEventHandler(this.C_M2_MouseMove);
                                cm2_form.TopLevel = false;
                                cm2_form.Location = new Point(x, y);//位置排列
                                this.panel1.Controls.Add(cm2_form);  //然后将该窗体嵌入主界面
                                cm2_form.Show();

                                break;
                            case "粒度概率累计曲线":
                                Grabularity ldgl_form = new Grabularity();

                                ldgl_form.MyLineColor1 = fs.PenColor1;
                                ldgl_form.MyLineColor2 = fs.PenColor2;
                                ldgl_form.MyFont = fs.TitleFont;
                                ldgl_form.MyLine1 = fs.PenSize1;
                                ldgl_form.MyText = fs.TitleText;
                                ldgl_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                ldgl_form.MyXBrush = fs.XBrush;
                                ldgl_form.MyYBrush = fs.YBrush;
                                ldgl_form.MyXFont = fs.XFont;
                                ldgl_form.MyYFont = fs.YFont;
                                ldgl_form.MyXnameColor = fs.XnameColor;
                                ldgl_form.MyXnameFont = fs.XnameFont;

                                ldgl_form.MyYnameColor = fs.YnameColor;
                                ldgl_form.MyYnameFont = fs.YnameFont;


                                SysData.ldglljqx_dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                ldgl_form.FormBorderStyle = FormBorderStyle.None;
                                ldgl_form.MouseDown += new MouseEventHandler(this.ldgl_MouseDown);
                                ldgl_form.MouseMove += new MouseEventHandler(this.ldgl_MouseMove);
                                ldgl_form.TopLevel = false;
                                ldgl_form.Location = new Point(x, y);//位置排列
                                this.panel1.Controls.Add(ldgl_form);  //然后将该窗体嵌入主界面
                                ldgl_form.Show();

                                break;

                            case "测井曲线":
                                WellLog cjqx_form = new WellLog();
                                cjqx_form.MyLineColor1 = fs.PenColor1;
                                cjqx_form.MyLineColor2 = fs.PenColor2;
                                cjqx_form.MyFont = fs.TitleFont;
                                cjqx_form.MyLine1 = fs.PenSize1;
                                cjqx_form.MyText = fs.TitleText;
                                cjqx_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cjqx_form.MyXBrush = fs.XBrush;
                                cjqx_form.MyYBrush = fs.YBrush;
                                cjqx_form.MyXFont = fs.XFont;
                                cjqx_form.MyYFont = fs.YFont;
                                cjqx_form.MyXnameColor = fs.XnameColor;
                                cjqx_form.MyXnameFont = fs.XnameFont;

                                cjqx_form.MyYnameColor = fs.YnameColor;
                                cjqx_form.MyYnameFont = fs.YnameFont;


                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cjqx_form.FormBorderStyle = FormBorderStyle.None;
                                cjqx_form.TopLevel = false;
                                cjqx_form.Location = new Point(x, y);//位置排列
                                cjqx_form.MouseDown += new MouseEventHandler(this.cjqx_MouseDown);
                                cjqx_form.MouseMove += new MouseEventHandler(this.cjqx_MouseMove);
                                this.panel1.Controls.Add(cjqx_form);
                                cjqx_form.Show();


                                ////////////////////////////////////////////////////////////////////////////
                                cjqx_form.Panel_refresh += zu.Panel_refresh;//添加重绘委托事件
                                zu.DrawRedline += cjqx_form.DrawRedline;
                                zu.Update_draw += cjqx_form.update_draw;
                                zu.Selfrefresh += cjqx_form.selfrefresh;
                                ////////////////////////////////////////////////////////////////////////////
                                break;

                            case "油气水":
                                OilWater OilWater_form = new OilWater();
                                OilWater_form.MyLineColor1 = fs.PenColor1;
                                OilWater_form.MyLineColor2 = fs.PenColor2;
                                OilWater_form.MyFont = fs.TitleFont;
                                OilWater_form.MyLine1 = fs.PenSize1;
                                OilWater_form.MyText = fs.TitleText;
                                OilWater_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                OilWater_form.MyXBrush = fs.XBrush;
                                OilWater_form.MyYBrush = fs.YBrush;
                                OilWater_form.MyXFont = fs.XFont;
                                OilWater_form.MyYFont = fs.YFont;
                                OilWater_form.MyXnameColor = fs.XnameColor;
                                OilWater_form.MyXnameFont = fs.XnameFont;
                                OilWater_form.MyXnameText = fs.XnameText;
                                OilWater_form.MyYnameColor = fs.YnameColor;
                                OilWater_form.MyYnameFont = fs.YnameFont;
                                OilWater_form.MyYnameText = fs.YnameText;
                                OilWater_form.MyXCheck = fs.XCheck;
                                OilWater_form.MyYCheck = fs.YCheck;

                                // SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                OilWater_form.FormBorderStyle = FormBorderStyle.None;
                                OilWater_form.MouseDown += new MouseEventHandler(this.ldgl_MouseDown);
                                OilWater_form.MouseMove += new MouseEventHandler(this.ldgl_MouseMove);
                                OilWater_form.TopLevel = false;
                                OilWater_form.Location = new Point(x, y);//位置排列
                                this.panel1.Controls.Add(OilWater_form);  //然后将该窗体嵌入主界面
                                OilWater_form.Show();
                                break;
                        }
                    }
                    else if (OpentemplateFromDB .bb.Length >4)
                    {
                        i = Convert.ToInt32((OpentemplateFromDB.bb[0]) * 256 + OpentemplateFromDB.bb[1]);
                        x = Convert.ToInt32((OpentemplateFromDB.bb[2]) * 256 + OpentemplateFromDB.bb[3]);
                        y = Convert.ToInt32((OpentemplateFromDB.bb[4]) * 256 + OpentemplateFromDB.bb[5]);
                        byte[] Byte = new byte[i];
                        Buffer.BlockCopy(OpentemplateFromDB.bb, 6, Byte, 0, i);
                        OpentemplateFromDB.bb = OpentemplateFromDB.bb.Skip(i + 6).Take(OpentemplateFromDB.bb.Length).ToArray();
                        XmlHelper.LoadChartFromDB(chart1[gap[0]], Byte);
                        chart1[gap[0]].Location = new Point(x, y);
                        this.panel1.Controls.Add(chart1[gap[0]]);
                        gap[0]++;
                       
                    }
                    else
                    {
                        w= Convert.ToInt32((OpentemplateFromDB.bb[0]) * 256 + OpentemplateFromDB.bb[1]);
                        h = Convert.ToInt32((OpentemplateFromDB.bb[2]) * 256 + OpentemplateFromDB.bb[3]);
                        Point p = new Point(w, h);
                        this.panel1.AutoScrollPosition = p;
                        OpentemplateFromDB.bb = OpentemplateFromDB.bb.Skip(4).Take(OpentemplateFromDB.bb.Length).ToArray();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSavetoDB_Click(object sender, EventArgs e)//保存模板到数据库
        {
            if (this.panel1.Controls.Count < 1)
            {
                MessageBox.Show("你还没绘制图件，不能保存模板");
                return;
            }
            if (!Ymhdo.Project.ExistTemplate(MyObject.pro1))
            {
                MessageBox.Show("当前模版是空， 请选择 另存绘图模版到库!");
                return;
            }
            Savetemp2DB();
        }

        private void Savetemp2DB()
        {
            if (MyObject.pro1 == null)
            {
                MessageBox.Show("当前地质插图模板是空，你该选择另存地质插图模板到库中！");
                return;
            }

            if (MyObject.pro1.ReadOnly)
            {
                MessageBox.Show("只有管理员才能保存模板！");
                return;
            }
            if (MyObject.pro1 != null && !(MyObject.pro1.ReadOnly))
            {
                Save();
                MyObject.aa1 = new byte[MyObject.k1];
                Buffer.BlockCopy(MyObject.bb1, 0, MyObject.aa1, 0, MyObject.k1);
                MyObject.pro1.Data = MyObject.aa1;
                MyObject.pro1.Save();
            }
        }
        #endregion
        #region 信息到当前项目
        private void btnSaveAsInfo2DB_Click(object sender, EventArgs e)//另存为信息到当前项目
        {
            if (Ymhdo.Project.ReadOnly)
            {
                MessageBox.Show("本项目不是你创建的，你不能修改本项目地质插图信息!");
                return;
            }
            if (this.panel1.Controls.Count < 1)
            {
                MessageBox.Show("你还没绘制图件，不能另存为信息到当前项目");
                return;
            }
            Save();
            if (this.panel1.Controls.Count > 0)
            {
                MyObject.aa1 = new byte[MyObject.k1];
                Buffer.BlockCopy(MyObject.bb1, 0, MyObject.aa1, 0, MyObject.k1);
                ProjectInfo ProjectInfo;
                if (Ymhdo.Project.SaveInfoAs(ShareInfo.GeoAuxMapNew, out ProjectInfo))
                {
                    ProjectInfo.Info = MyObject.aa1;
                    ProjectInfo.Save();
                }
            }
        }

        private void OpenInfo()
        {
            int i = 0, x = 0, y = 0;
            try
            {
                while (OpenInfoFromDB.dd.Length != 0)
                {
                    if ((OpenInfoFromDB.dd[0]) == (byte)255)
                    {
                        i = Convert.ToInt32((OpenInfoFromDB.dd[1]) * 256 + OpenInfoFromDB.dd[2]);
                        x = Convert.ToInt32((OpenInfoFromDB.dd[3]) * 256 + OpenInfoFromDB.dd[4]);
                        y = Convert.ToInt32((OpenInfoFromDB.dd[5]) * 256 + OpenInfoFromDB.dd[6]);
                        byte[] Byte = new byte[i];
                        Buffer.BlockCopy(OpenInfoFromDB.dd, 7, Byte, 0, i);
                        OpenInfoFromDB.dd = OpenInfoFromDB.dd.Skip(i + 7).Take(OpenInfoFromDB.dd.Length).ToArray();
                        FormState fs = XmlHelper.LoadSpecialFromDB(Byte);
                        switch (fs.FrmName)
                        {
                            case "主图":
                                WellLog_Main cjzt_form = new WellLog_Main();
                                cjzt_form.MyLineColor1 = fs.PenColor1;
                                cjzt_form.MyLineColor2 = fs.PenColor2;
                                cjzt_form.MyFont = fs.TitleFont;
                                cjzt_form.MyLine1 = fs.PenSize1;
                                cjzt_form.MyText = fs.TitleText;
                                cjzt_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cjzt_form.MyXBrush = fs.XBrush;
                                cjzt_form.MyYBrush = fs.YBrush;
                                cjzt_form.MyXFont = fs.XFont;
                                cjzt_form.MyYFont = fs.YFont;
                                cjzt_form.MyXnameColor = fs.XnameColor;
                                cjzt_form.MyXnameFont = fs.XnameFont;
                                cjzt_form.MyXnameText = fs.XnameText;
                                cjzt_form.MyYnameColor = fs.YnameColor;
                                cjzt_form.MyYnameFont = fs.YnameFont;
                                cjzt_form.MyYnameText = fs.YnameText;
                                cjzt_form.MyXCheck = fs.XCheck;
                                cjzt_form.MyYCheck = fs.YCheck;

                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cjzt_form.FormBorderStyle = FormBorderStyle.None;
                                cjzt_form.TopLevel = false;
                                cjzt_form.Location = new Point(x, y);//位置排列
                                cjzt_form.MouseDown += new MouseEventHandler(this.cjzt_MouseDown);
                                cjzt_form.MouseMove += new MouseEventHandler(this.cjzt_MouseMove);
                                this.panel1.Controls.Add(cjzt_form);
                                cjzt_form.Show();


                                //////////////////////////////////////////////////////////////////////////
                                jht.Panel_refresh += cjzt_form.Panel_refresh;//添加重绘委托事件
                                cjzt_form.DrawRedline += jht.DrawRedline;
                                cjzt_form.Update_draw += jht.update_draw;
                                cjzt_form.Selfrefresh += jht.selfrefresh;
                                //////////////////////////////////////////////////////////////////////////
                                break;
                            case "萨胡成因判别函数":
                                SadhuCauses shcy_form = new SadhuCauses();
                                shcy_form.MyText = fs.TitleText;
                                shcy_form.MyColor = fs.TitleColor;
                                shcy_form.MyFont = fs.TitleFont;

                                shcy_form.FormBorderStyle = FormBorderStyle.None;
                                shcy_form.MouseDown += new System.Windows.Forms.MouseEventHandler(this.萨胡成因判别函数_MouseDown);
                                shcy_form.MouseMove += new System.Windows.Forms.MouseEventHandler(this.萨胡成因判别函数_MouseMove);
                                shcy_form.TopLevel = false;
                                shcy_form.Location = new Point(x, y);//位置排列
                                this.panel1.Controls.Add(shcy_form);  //然后将该窗体嵌入主界面
                                shcy_form.Show();
                                break;
                            case "C_M1":
                                C_M1 cm1_form = new C_M1();
                                cm1_form.MyLineColor1 = fs.PenColor1;
                                cm1_form.MyLineColor2 = fs.PenColor2;
                                cm1_form.MyFont = fs.TitleFont;
                                cm1_form.MyLine1 = fs.PenSize1;
                                cm1_form.MyText = fs.TitleText;
                                cm1_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cm1_form.MyXBrush = fs.XBrush;
                                cm1_form.MyYBrush = fs.YBrush;
                                cm1_form.MyXFont = fs.XFont;
                                cm1_form.MyYFont = fs.YFont;
                                cm1_form.MyXnameColor = fs.XnameColor;
                                cm1_form.MyXnameFont = fs.XnameFont;
                                cm1_form.MyXnameText = fs.XnameText;
                                cm1_form.MyYnameColor = fs.YnameColor;
                                cm1_form.MyYnameFont = fs.YnameFont;
                                cm1_form.MyYnameText = fs.YnameText;
                                cm1_form.MyXCheck = fs.XCheck;
                                cm1_form.MyYCheck = fs.YCheck;

                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cm1_form.FormBorderStyle = FormBorderStyle.None;
                                cm1_form.MouseDown += new MouseEventHandler(this.C_M1_MouseDown);
                                cm1_form.MouseMove += new MouseEventHandler(this.C_M1_MouseMove);
                                cm1_form.TopLevel = false;
                                cm1_form.Location = new Point(x, y);//位置排列
                                this.panel1.Controls.Add(cm1_form);  //然后将该窗体嵌入主界面

                                cm1_form.Show();
                                break;
                            case "C_M2":
                                C_M2 cm2_form = new C_M2();

                                cm2_form.MyLineColor1 = fs.PenColor1;
                                cm2_form.MyLineColor2 = fs.PenColor2;
                                cm2_form.MyFont = fs.TitleFont;
                                cm2_form.MyLine1 = fs.PenSize1;
                                cm2_form.MyText = fs.TitleText;
                                cm2_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cm2_form.MyXBrush = fs.XBrush;
                                cm2_form.MyYBrush = fs.YBrush;
                                cm2_form.MyXFont = fs.XFont;
                                cm2_form.MyYFont = fs.YFont;
                                cm2_form.MyXnameColor = fs.XnameColor;
                                cm2_form.MyXnameFont = fs.XnameFont;
                                cm2_form.MyXnameText = fs.XnameText;
                                cm2_form.MyYnameColor = fs.YnameColor;
                                cm2_form.MyYnameFont = fs.YnameFont;
                                cm2_form.MyYnameText = fs.YnameText;
                                cm2_form.MyXCheck = fs.XCheck;
                                cm2_form.MyYCheck = fs.YCheck;

                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cm2_form.FormBorderStyle = FormBorderStyle.None;
                                cm2_form.MouseDown += new MouseEventHandler(this.C_M2_MouseDown);
                                cm2_form.MouseMove += new MouseEventHandler(this.C_M2_MouseMove);
                                cm2_form.TopLevel = false;
                                cm2_form.Location = new Point(x, y);//位置排列
                                this.panel1.Controls.Add(cm2_form);  //然后将该窗体嵌入主界面
                                cm2_form.Show();

                                break;
                            case "粒度概率累计曲线":
                                Grabularity ldgl_form = new Grabularity();

                                ldgl_form.MyLineColor1 = fs.PenColor1;
                                ldgl_form.MyLineColor2 = fs.PenColor2;
                                ldgl_form.MyFont = fs.TitleFont;
                                ldgl_form.MyLine1 = fs.PenSize1;
                                ldgl_form.MyText = fs.TitleText;
                                ldgl_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                ldgl_form.MyXBrush = fs.XBrush;
                                ldgl_form.MyYBrush = fs.YBrush;
                                ldgl_form.MyXFont = fs.XFont;
                                ldgl_form.MyYFont = fs.YFont;
                                ldgl_form.MyXnameColor = fs.XnameColor;
                                ldgl_form.MyXnameFont = fs.XnameFont;

                                ldgl_form.MyYnameColor = fs.YnameColor;
                                ldgl_form.MyYnameFont = fs.YnameFont;


                                SysData.ldglljqx_dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                ldgl_form.FormBorderStyle = FormBorderStyle.None;
                                ldgl_form.MouseDown += new MouseEventHandler(this.ldgl_MouseDown);
                                ldgl_form.MouseMove += new MouseEventHandler(this.ldgl_MouseMove);
                                ldgl_form.TopLevel = false;
                                ldgl_form.Location = new Point(x, y);//位置排列
                                this.panel1.Controls.Add(ldgl_form);  //然后将该窗体嵌入主界面
                                ldgl_form.Show();

                                break;

                            case "测井曲线":
                                WellLog cjqx_form = new WellLog();
                                cjqx_form.MyLineColor1 = fs.PenColor1;
                                cjqx_form.MyLineColor2 = fs.PenColor2;
                                cjqx_form.MyFont = fs.TitleFont;
                                cjqx_form.MyLine1 = fs.PenSize1;
                                cjqx_form.MyText = fs.TitleText;
                                cjqx_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cjqx_form.MyXBrush = fs.XBrush;
                                cjqx_form.MyYBrush = fs.YBrush;
                                cjqx_form.MyXFont = fs.XFont;
                                cjqx_form.MyYFont = fs.YFont;
                                cjqx_form.MyXnameColor = fs.XnameColor;
                                cjqx_form.MyXnameFont = fs.XnameFont;

                                cjqx_form.MyYnameColor = fs.YnameColor;
                                cjqx_form.MyYnameFont = fs.YnameFont;


                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cjqx_form.FormBorderStyle = FormBorderStyle.None;
                                cjqx_form.TopLevel = false;
                                cjqx_form.Location = new Point(x, y);//位置排列
                                cjqx_form.MouseDown += new MouseEventHandler(this.cjqx_MouseDown);
                                cjqx_form.MouseMove += new MouseEventHandler(this.cjqx_MouseMove);
                                this.panel1.Controls.Add(cjqx_form);
                                cjqx_form.Show();


                                ////////////////////////////////////////////////////////////////////////////
                                cjqx_form.Panel_refresh += zu.Panel_refresh;//添加重绘委托事件
                                zu.DrawRedline += cjqx_form.DrawRedline;
                                zu.Update_draw += cjqx_form.update_draw;
                                zu.Selfrefresh += cjqx_form.selfrefresh;
                                ////////////////////////////////////////////////////////////////////////////
                                break;

                            case "油气水":
                                OilWater OilWater_form = new OilWater();
                                OilWater_form.MyLineColor1 = fs.PenColor1;
                                OilWater_form.MyLineColor2 = fs.PenColor2;
                                OilWater_form.MyFont = fs.TitleFont;
                                OilWater_form.MyLine1 = fs.PenSize1;
                                OilWater_form.MyText = fs.TitleText;
                                OilWater_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                OilWater_form.MyXBrush = fs.XBrush;
                                OilWater_form.MyYBrush = fs.YBrush;
                                OilWater_form.MyXFont = fs.XFont;
                                OilWater_form.MyYFont = fs.YFont;
                                OilWater_form.MyXnameColor = fs.XnameColor;
                                OilWater_form.MyXnameFont = fs.XnameFont;
                                OilWater_form.MyXnameText = fs.XnameText;
                                OilWater_form.MyYnameColor = fs.YnameColor;
                                OilWater_form.MyYnameFont = fs.YnameFont;
                                OilWater_form.MyYnameText = fs.YnameText;
                                OilWater_form.MyXCheck = fs.XCheck;
                                OilWater_form.MyYCheck = fs.YCheck;

                                // SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                OilWater_form.FormBorderStyle = FormBorderStyle.None;
                                OilWater_form.MouseDown += new MouseEventHandler(this.ldgl_MouseDown);
                                OilWater_form.MouseMove += new MouseEventHandler(this.ldgl_MouseMove);
                                OilWater_form.TopLevel = false;
                                OilWater_form.Location = new Point(x, y);//位置排列
                                this.panel1.Controls.Add(OilWater_form);  //然后将该窗体嵌入主界面
                                OilWater_form.Show();

                                break;

                        }
                    }
                    else
                    {
                        i = Convert.ToInt32((OpenInfoFromDB.dd[0]) * 256 + OpenInfoFromDB.dd[1]);
                        x = Convert.ToInt32((OpenInfoFromDB.dd[2]) * 256 + OpenInfoFromDB.dd[3]);
                        y = Convert.ToInt32((OpenInfoFromDB.dd[4]) * 256 + OpenInfoFromDB.dd[5]);
                        byte[] Byte = new byte[i];
                        Buffer.BlockCopy(OpenInfoFromDB.dd, 6, Byte, 0, i);
                        OpenInfoFromDB.dd = OpenInfoFromDB.dd.Skip(i + 6).Take(OpenInfoFromDB.dd.Length).ToArray();
                        XmlHelper.LoadChartFromDB(chart1[gap[0]], Byte);
                        chart1[gap[0]].Location = new Point(x, y);
                        this.panel1.Controls.Add(chart1[gap[0]]);
                        gap[0]++;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnOpenInfoFromDB_Click(object sender, EventArgs e)//从当前项目中打开信息
        {
            AccessModuleSubItem tempItem;
            if (!AccessInfo.CanGetModuleSubItem(ModuleType.CrossPlot, AccessType.OpenInfo, out tempItem))
            {
                return;
            }
            OpenInfoFromDB oif = new OpenInfoFromDB();
            oif.ShowDialog();
            if (OpenInfoFromDB.Isclose)
            {
                OpenInfo();
            }
            oif.Dispose();
        }

        private void btnSaveInfo2DB_Click(object sender, EventArgs e)//保存信息到当前项目
        {
            if (this.panel1.Controls.Count < 1)
            {
                MessageBox.Show("你还没绘制图件，不能保存信息");
                return;
            }
            if (!Ymhdo.Project.ExistInfo(MyObject.pfo1))
            {
                MessageBox.Show("当前信息是空， 请选择另存绘图信息到当前项目!");
                return;
            }
            SaveInfo2DB();
        }

        private void SaveInfo2DB()
        {
            if (MyObject.pfo1 == null)
            {
                MessageBox.Show("当前地质插图模板是空，你该选择另存地质插图模板到库中！");
                return;
            }

            if (MyObject.pfo1.ReadOnly)
            {
                MessageBox.Show("只有管理员才能保存模板！");
                return;
            }
            if (MyObject.pfo1 != null && !(MyObject.pfo1.ReadOnly))
            {
                
                Save();
                MyObject.aa1 = new byte[MyObject.k1];
                Buffer.BlockCopy(MyObject.bb1, 0, MyObject.aa1, 0, MyObject.k1);
                MyObject.pfo1.Info = MyObject.aa1;
                MyObject.pfo1.Save();
            }
        }
        #endregion
        #region 模板到本地
        private void btnSaveTemp2Local_Click(object sender, EventArgs e)//保存模板到本地
        {
            if (this.panel1.Controls.Count < 1)
            {
                MessageBox.Show("你还没绘制图件，不能保存模板");
                return;
            }
            if (MyObject.Filepath1 == null)
            {
                MessageBox.Show("当前文件是空， 请选择 另存绘图模版到本地文件!");
                return;
            }
            Savetemp2Local();
        }

        private void Savetemp2Local()
        {
            if (MyObject.Filepath1 != null)
            {
                Save();
                MyObject.aa1 = new byte[MyObject.k1];
                Buffer.BlockCopy(MyObject.bb1, 0, MyObject.aa1, 0, MyObject.k1);
                FileStream fs = new FileStream(MyObject.Filepath1, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, MyObject.aa1);
                fs.Flush();
                fs.Close();
            }
        }

        private void btnOpenTempFromLocal_Click(object sender, EventArgs e)//从本地打开模板
        {
            try
            {
                OpenFileDialog diag = new OpenFileDialog();
                diag.Filter = "All files (*.*)|*.xml";
                if (diag.ShowDialog() == DialogResult.OK)
                {
                    MyObject.Filepath1 = diag.FileName;
                    try
                    {
                        MyObject.local1 = XmlHelper.LoadFromLocal(diag.FileName);
                        OpenLocalTemp();
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {
                MessageBox.Show("模版打开出错！", "出错", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OpenLocalTemp()
        {
            int i = 0, x = 0, y = 0;
            try
            {
                while (MyObject.local1.Length != 0)
                {
                    if ((MyObject.local1[0]) == (byte)255)
                    {
                        i = Convert.ToInt32((MyObject.local1[1]) * 256 + MyObject.local1[2]);
                        x = Convert.ToInt32((MyObject.local1[3]) * 256 + MyObject.local1[4]);
                        y = Convert.ToInt32((MyObject.local1[5]) * 256 + MyObject.local1[6]);
                        byte[] Byte = new byte[i];
                        Buffer.BlockCopy(MyObject.local1, 7, Byte, 0, i);
                        MyObject.local1 = MyObject.local1.Skip(i + 7).Take(MyObject.local1.Length).ToArray();
                        FormState fs = XmlHelper.LoadSpecialFromDB(Byte);
                        switch (fs.FrmName)
                        {
                            case "主图":
                                WellLog_Main cjzt_form = new WellLog_Main();
                                cjzt_form.MyLineColor1 = fs.PenColor1;
                                cjzt_form.MyLineColor2 = fs.PenColor2;
                                cjzt_form.MyFont = fs.TitleFont;
                                cjzt_form.MyLine1 = fs.PenSize1;
                                cjzt_form.MyText = fs.TitleText;
                                cjzt_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cjzt_form.MyXBrush = fs.XBrush;
                                cjzt_form.MyYBrush = fs.YBrush;
                                cjzt_form.MyXFont = fs.XFont;
                                cjzt_form.MyYFont = fs.YFont;
                                cjzt_form.MyXnameColor = fs.XnameColor;
                                cjzt_form.MyXnameFont = fs.XnameFont;
                                cjzt_form.MyXnameText = fs.XnameText;
                                cjzt_form.MyYnameColor = fs.YnameColor;
                                cjzt_form.MyYnameFont = fs.YnameFont;
                                cjzt_form.MyYnameText = fs.YnameText;
                                cjzt_form.MyXCheck = fs.XCheck;
                                cjzt_form.MyYCheck = fs.YCheck;

                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cjzt_form.FormBorderStyle = FormBorderStyle.None;
                                cjzt_form.TopLevel = false;
                                cjzt_form.Location = new Point(x, y);//位置排列
                                cjzt_form.MouseDown += new MouseEventHandler(this.cjzt_MouseDown);
                                cjzt_form.MouseMove += new MouseEventHandler(this.cjzt_MouseMove);
                                this.panel1.Controls.Add(cjzt_form);
                                cjzt_form.Show();


                                //////////////////////////////////////////////////////////////////////////
                                jht.Panel_refresh += cjzt_form.Panel_refresh;//添加重绘委托事件
                                cjzt_form.DrawRedline += jht.DrawRedline;
                                cjzt_form.Update_draw += jht.update_draw;
                                cjzt_form.Selfrefresh += jht.selfrefresh;
                                //////////////////////////////////////////////////////////////////////////
                                break;
                            case "萨胡成因判别函数":
                                SadhuCauses shcy_form = new SadhuCauses();
                                shcy_form.MyText = fs.TitleText;
                                shcy_form.MyColor = fs.TitleColor;
                                shcy_form.MyFont = fs.TitleFont;

                                shcy_form.FormBorderStyle = FormBorderStyle.None;
                                shcy_form.MouseDown += new System.Windows.Forms.MouseEventHandler(this.萨胡成因判别函数_MouseDown);
                                shcy_form.MouseMove += new System.Windows.Forms.MouseEventHandler(this.萨胡成因判别函数_MouseMove);
                                shcy_form.TopLevel = false;
                                shcy_form.Location = new Point(x, y);//位置排列
                                this.panel1.Controls.Add(shcy_form);  //然后将该窗体嵌入主界面
                                shcy_form.Show();
                                break;
                            case "C_M1":
                                C_M1 cm1_form = new C_M1();
                                cm1_form.MyLineColor1 = fs.PenColor1;
                                cm1_form.MyLineColor2 = fs.PenColor2;
                                cm1_form.MyFont = fs.TitleFont;
                                cm1_form.MyLine1 = fs.PenSize1;
                                cm1_form.MyText = fs.TitleText;
                                cm1_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cm1_form.MyXBrush = fs.XBrush;
                                cm1_form.MyYBrush = fs.YBrush;
                                cm1_form.MyXFont = fs.XFont;
                                cm1_form.MyYFont = fs.YFont;
                                cm1_form.MyXnameColor = fs.XnameColor;
                                cm1_form.MyXnameFont = fs.XnameFont;
                                cm1_form.MyXnameText = fs.XnameText;
                                cm1_form.MyYnameColor = fs.YnameColor;
                                cm1_form.MyYnameFont = fs.YnameFont;
                                cm1_form.MyYnameText = fs.YnameText;
                                cm1_form.MyXCheck = fs.XCheck;
                                cm1_form.MyYCheck = fs.YCheck;

                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cm1_form.FormBorderStyle = FormBorderStyle.None;
                                cm1_form.MouseDown += new MouseEventHandler(this.C_M1_MouseDown);
                                cm1_form.MouseMove += new MouseEventHandler(this.C_M1_MouseMove);
                                cm1_form.TopLevel = false;
                                cm1_form.Location = new Point(x, y);//位置排列
                                this.panel1.Controls.Add(cm1_form);  //然后将该窗体嵌入主界面

                                cm1_form.Show();
                                break;
                            case "C_M2":
                                C_M2 cm2_form = new C_M2();

                                cm2_form.MyLineColor1 = fs.PenColor1;
                                cm2_form.MyLineColor2 = fs.PenColor2;
                                cm2_form.MyFont = fs.TitleFont;
                                cm2_form.MyLine1 = fs.PenSize1;
                                cm2_form.MyText = fs.TitleText;
                                cm2_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cm2_form.MyXBrush = fs.XBrush;
                                cm2_form.MyYBrush = fs.YBrush;
                                cm2_form.MyXFont = fs.XFont;
                                cm2_form.MyYFont = fs.YFont;
                                cm2_form.MyXnameColor = fs.XnameColor;
                                cm2_form.MyXnameFont = fs.XnameFont;
                                cm2_form.MyXnameText = fs.XnameText;
                                cm2_form.MyYnameColor = fs.YnameColor;
                                cm2_form.MyYnameFont = fs.YnameFont;
                                cm2_form.MyYnameText = fs.YnameText;
                                cm2_form.MyXCheck = fs.XCheck;
                                cm2_form.MyYCheck = fs.YCheck;

                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cm2_form.FormBorderStyle = FormBorderStyle.None;
                                cm2_form.MouseDown += new MouseEventHandler(this.C_M2_MouseDown);
                                cm2_form.MouseMove += new MouseEventHandler(this.C_M2_MouseMove);
                                cm2_form.TopLevel = false;
                                cm2_form.Location = new Point(x, y);//位置排列
                                this.panel1.Controls.Add(cm2_form);  //然后将该窗体嵌入主界面
                                cm2_form.Show();

                                break;
                            case "粒度概率累计曲线":
                                Grabularity ldgl_form = new Grabularity();

                                ldgl_form.MyLineColor1 = fs.PenColor1;
                                ldgl_form.MyLineColor2 = fs.PenColor2;
                                ldgl_form.MyFont = fs.TitleFont;
                                ldgl_form.MyLine1 = fs.PenSize1;
                                ldgl_form.MyText = fs.TitleText;
                                ldgl_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                ldgl_form.MyXBrush = fs.XBrush;
                                ldgl_form.MyYBrush = fs.YBrush;
                                ldgl_form.MyXFont = fs.XFont;
                                ldgl_form.MyYFont = fs.YFont;
                                ldgl_form.MyXnameColor = fs.XnameColor;
                                ldgl_form.MyXnameFont = fs.XnameFont;

                                ldgl_form.MyYnameColor = fs.YnameColor;
                                ldgl_form.MyYnameFont = fs.YnameFont;


                                SysData.ldglljqx_dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                ldgl_form.FormBorderStyle = FormBorderStyle.None;
                                ldgl_form.MouseDown += new MouseEventHandler(this.ldgl_MouseDown);
                                ldgl_form.MouseMove += new MouseEventHandler(this.ldgl_MouseMove);
                                ldgl_form.TopLevel = false;
                                ldgl_form.Location = new Point(x, y);//位置排列
                                this.panel1.Controls.Add(ldgl_form);  //然后将该窗体嵌入主界面
                                ldgl_form.Show();

                                break;

                            case "测井曲线":
                                WellLog cjqx_form = new WellLog();
                                cjqx_form.MyLineColor1 = fs.PenColor1;
                                cjqx_form.MyLineColor2 = fs.PenColor2;
                                cjqx_form.MyFont = fs.TitleFont;
                                cjqx_form.MyLine1 = fs.PenSize1;
                                cjqx_form.MyText = fs.TitleText;
                                cjqx_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                cjqx_form.MyXBrush = fs.XBrush;
                                cjqx_form.MyYBrush = fs.YBrush;
                                cjqx_form.MyXFont = fs.XFont;
                                cjqx_form.MyYFont = fs.YFont;
                                cjqx_form.MyXnameColor = fs.XnameColor;
                                cjqx_form.MyXnameFont = fs.XnameFont;

                                cjqx_form.MyYnameColor = fs.YnameColor;
                                cjqx_form.MyYnameFont = fs.YnameFont;


                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                cjqx_form.FormBorderStyle = FormBorderStyle.None;
                                cjqx_form.TopLevel = false;
                                cjqx_form.Location = new Point(x, y);//位置排列
                                cjqx_form.MouseDown += new MouseEventHandler(this.cjqx_MouseDown);
                                cjqx_form.MouseMove += new MouseEventHandler(this.cjqx_MouseMove);
                                this.panel1.Controls.Add(cjqx_form);
                                cjqx_form.Show();


                                ////////////////////////////////////////////////////////////////////////////
                                cjqx_form.Panel_refresh += zu.Panel_refresh;//添加重绘委托事件
                                zu.DrawRedline += cjqx_form.DrawRedline;
                                zu.Update_draw += cjqx_form.update_draw;
                                zu.Selfrefresh += cjqx_form.selfrefresh;
                                ////////////////////////////////////////////////////////////////////////////
                                break;

                            case "油气水":
                                OilWater OilWater_form = new OilWater();
                                OilWater_form.MyLineColor1 = fs.PenColor1;
                                OilWater_form.MyLineColor2 = fs.PenColor2;
                                OilWater_form.MyFont = fs.TitleFont;
                                OilWater_form.MyLine1 = fs.PenSize1;
                                OilWater_form.MyText = fs.TitleText;
                                OilWater_form.MyLine2 = fs.PenSize2;
                                //////////////////////////////////////////////////////////////////////////新修改
                                OilWater_form.MyXBrush = fs.XBrush;
                                OilWater_form.MyYBrush = fs.YBrush;
                                OilWater_form.MyXFont = fs.XFont;
                                OilWater_form.MyYFont = fs.YFont;
                                OilWater_form.MyXnameColor = fs.XnameColor;
                                OilWater_form.MyXnameFont = fs.XnameFont;
                                OilWater_form.MyXnameText = fs.XnameText;
                                OilWater_form.MyYnameColor = fs.YnameColor;
                                OilWater_form.MyYnameFont = fs.YnameFont;
                                OilWater_form.MyYnameText = fs.YnameText;
                                OilWater_form.MyXCheck = fs.XCheck;
                                OilWater_form.MyYCheck = fs.YCheck;

                                // SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                OilWater_form.FormBorderStyle = FormBorderStyle.None;
                                OilWater_form.MouseDown += new MouseEventHandler(this.ldgl_MouseDown);
                                OilWater_form.MouseMove += new MouseEventHandler(this.ldgl_MouseMove);
                                OilWater_form.TopLevel = false;
                                OilWater_form.Location = new Point(x, y);//位置排列
                                this.panel1.Controls.Add(OilWater_form);  //然后将该窗体嵌入主界面
                                OilWater_form.Show();

                                break;
                            case "SanHanLiChenJiWu":
                                SanHanLiChenJiWu SanHanLiChenJiWu_form = new SanHanLiChenJiWu();
                                SanHanLiChenJiWu_form.cP = fs.Pen;
                                SanHanLiChenJiWu_form.MyBrush = fs.MyBrush;      //shlcj.LabelFont, "SanHanLiChenJiWu"
                                SanHanLiChenJiWu_form.LabelFont = fs.LabelFont;                          

                                SysData.dt = fs.Datatable;
                                //////////////////////////////////////////////////////////////////////////结束
                                SanHanLiChenJiWu_form.FormBorderStyle = FormBorderStyle.None;                                                           
                               this.panel1.Controls.Add(SanHanLiChenJiWu_form);  //然后将该窗体嵌入主界面
                                SanHanLiChenJiWu_form.Show();

                                break;

                        }
                    }
                    else
                    {
                        i = Convert.ToInt32((MyObject.local1[0]) * 256 + MyObject.local1[1]);
                        x = Convert.ToInt32((MyObject.local1[2]) * 256 + MyObject.local1[3]);
                        y = Convert.ToInt32((MyObject.local1[4]) * 256 + MyObject.local1[5]);
                        byte[] Byte = new byte[i];
                        Buffer.BlockCopy(MyObject.local1, 6, Byte, 0, i);
                        MyObject.local1 = MyObject.local1.Skip(i + 6).Take(MyObject.local1.Length).ToArray();
                        XmlHelper.LoadChartFromDB(chart1[gap[0]], Byte);
                        chart1[gap[0]].Location = new Point(x, y);
                        this.panel1.Controls.Add(chart1[gap[0]]);
                        gap[0]++;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSaveAsTemp2Local_Click(object sender, EventArgs e) //另存为模板到本地
        {
            if (this.panel1.Controls.Count < 1)
            {
                MessageBox.Show("你还没绘制图件，不能另存为模板到数据库");
                return;
            }
            Save();
            if (this.panel1.Controls.Count > 0)
            {
                MyObject.aa1 = new byte[MyObject.k1];
                Buffer.BlockCopy(MyObject.bb1, 0, MyObject.aa1, 0, MyObject.k1);
                try
                {
                    SaveFileDialog diag = new SaveFileDialog();
                    diag.Filter = "All files (*.*)|*.xml";
                    if (diag.ShowDialog() == DialogResult.OK)
                    {
                        XmlHelper.Save2Local(MyObject.aa1, diag.FileName);
                    }
                }
                catch
                {
                }
            }

        }
        #endregion
        private void buttonItem8_Click(object sender, EventArgs e)
        {
            //画出生产开发曲线
            //相渗曲线图
            this.buttonX9.Enabled = true;
            DrawChart dc = new DrawChart();
            dc.Draw_shengchankaifaquxian(chart43, gap[42], initPointX + getPianyiliang(), initPointY, initWidth, initHeight);//调用我写的DrawChart类,我已经将所有画图的方法都封装在此类中
            this.panel1.Controls.Add(chart43[gap[42]]);//将画出来的图添加到主界面中
            #region 晁    修改
            Savelocation.value_x = initPointX + getPianyiliang();
            Savelocation.value_y = initPointY;
            #endregion
            gap[42]++;//当同种类型的图要画第二次时，让Chart41数组的[index]加1
        }



        //public int leap;//重要状态变量
        public ArrayList Mymsg = new ArrayList();//动态数组记录操作步骤      
        public int operatingSteps;//获取当前最新操作步骤
        bool isRecall = false;

        private void btnRecall_Click(object sender, EventArgs e)
        {
            recallTo();
            //Mymsg.Clear();
            //Mymsg.Add(getData());
            //leap = 0;  
        }
        public byte[] getData()
        {
            Save();
            if (this.panel1.Controls.Count > 0)
            {
                MyObject.aa1 = new byte[MyObject.k1];
                Buffer.BlockCopy(MyObject.bb1, 0, MyObject.aa1, 0, MyObject.k1);
            }
            return MyObject.aa1;
        }
        public void getMsg()//1、记录当前步骤
        {
            clearRecord(); //3、清除副本，新一轮开始
            Mymsg.Add(getData());
            operatingSteps = Mymsg.Count;
            if (operatingSteps > 1)
            {
                btnRecall.Enabled = true;
            }
        }
        public void getRecord()//2、获取副本
        {
            if (getRecordCount == 0)
            {
                getRecordCount++;
                foreach (byte[] m in Mymsg)
                {
                    theRecord[theRecordCount++] = m;
                }
                theRecord[theRecordCount] = null;
            }
            else
                return;
        }
        public void recallTo()//实现撤销的方法
        {
            this.panel1.Controls.Clear();
            //leap = 1;
            getRecord();
            isRecall = true;
            btnRepeat.Enabled = true;
            if (theRecordCount - 1 == 0)
            { return; }
            theRecordCount--;
            OpentemplateFromDB.bb = theRecord[theRecordCount - 1];
            OpenTemp();
            if (theRecordCount - 1 == 0)
            {
                btnRecall.Enabled = false;
                //btnRepeat.Enabled = true;
            }
        }
        public void repeatTo()//实现恢复的方法
        {
            this.panel1.Controls.Clear();
            //leap = 2;
            getRecord();
            if (isRecall == false || theRecordCount == operatingSteps)
            { return; }
            theRecordCount++;
            OpentemplateFromDB.bb = theRecord[theRecordCount - 1];
            OpenTemp();
            if (theRecordCount == operatingSteps)
            {
                btnRepeat.Enabled = false;
                btnRecall.Enabled = true;
            }
        }
        public byte[][] theRecord = new byte[10000][];
        public int theRecordCount = 0;//副本数组下标
        public int getRecordCount = 0;//获取副本的次数
        public void clearRecord()//初始化&&清空副本方法
        {
            for (int i = 0; i < theRecord.Length; i++)
            {
                theRecord[i] = null;
            }
            theRecordCount = 0;
            getRecordCount = 0;
            isRecall = false;
        }

        private void panel1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            //switch (leap)
            //{
            //    case 0:

            //        break;
            //    case 1:
            //        leap = 0;
            //        break;
            //    case 2:
            //        leap = 0;
            //        break;
            //    default:
            //        //
            //        break;
            //}
        }

        private void btnRepeat_Click(object sender, EventArgs e)
        {
            if (isRecall == false)
            { return; }
            repeatTo();
            //Mymsg.Clear();
            //Mymsg.Add(getData());
            //leap = 0; 
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //修改字体
            FontDialog diag = new FontDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (toolStripComboBox1.Enabled == true)
                {
                    if (toolStripComboBox1.SelectedIndex == 0)//图表区
                    {
                        //MyObject.MyAnnotation1.Font = diag.Font;
                        MyObject.My_Chart1.Titles[0].Font = diag.Font;
                        if (MyObject.My_Chart1.Legends.Count == 1)
                        {
                            MyObject.My_Chart1.Legends[0].Font = diag.Font;
                        }
                        for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                        {
                            MyObject.My_Chart1.ChartAreas[i].AxisX.TitleFont = diag.Font;
                            MyObject.My_Chart1.ChartAreas[i].AxisX.LabelStyle.Font = diag.Font;
                        }
                        for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                        {
                            MyObject.My_Chart1.ChartAreas[i].AxisY.TitleFont = diag.Font;
                            MyObject.My_Chart1.ChartAreas[i].AxisY.LabelStyle.Font = diag.Font;
                        }
                        for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                        {
                            if (toolStripComboBox1.SelectedItem.ToString() == "序列" + (i + 1))
                            {
                                MyObject.My_Chart1.Series[i].Font = diag.Font;
                            }
                        }
                    }
                    else if (toolStripComboBox1.SelectedIndex == 1)//图例区
                    {
                        if (MyObject.My_Chart1.Legends.Count == 1)
                        {
                            MyObject.My_Chart1.Legends[0].Font = diag.Font;
                        }
                    }
                    else if (toolStripComboBox1.SelectedIndex == 3)//水平轴区
                    {
                        for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                        {
                            MyObject.My_Chart1.ChartAreas[i].AxisX.TitleFont = diag.Font;
                            MyObject.My_Chart1.ChartAreas[i].AxisX.LabelStyle.Font = diag.Font;
                        }
                    }
                    else if (toolStripComboBox1.SelectedIndex == 4)//垂直轴区
                    {
                        for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                        {
                            MyObject.My_Chart1.ChartAreas[i].AxisY.TitleFont = diag.Font;
                            MyObject.My_Chart1.ChartAreas[i].AxisY.LabelStyle.Font = diag.Font;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                        {
                            if (toolStripComboBox1.SelectedItem.ToString() == "序列" + (i + 1))
                            {
                                MyObject.My_Chart1.Series[i].Font = diag.Font;
                            }
                        }
                    }
                }
                else
                {
                    if (MyObject.MyAnnotation1 != null)
                    {
                        MyObject.MyAnnotation1.Font = diag.Font;
                    }
                }
            }
        }

        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            this.toolStrip1.Visible = false;
        }

        private void MainFrame_MouseDown(object sender, MouseEventArgs e)
        {
            this.toolStrip1.Visible = false;
        }

        private void 设置数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MyObject.DT != null)
            {
                MyObject.DT.Columns.Clear();
                MyObject.DT.Rows.Clear();
            }
            DataSet dataSet1 = new DataSet();
            DataTable dataTable1 = dataSet1.Tables.Add();
            if(MyObject .DT !=null )
            {
                for (int k = 0; k < MyObject.My_Chart1.Series[0].Points.Count + 1; k++)
                {
                    DataRow dr = MyObject.DT.NewRow();
                    for (int i = 0; i < MyObject.My_Chart1.Series.Count + 1; i++)
                    {
                        if (k == 0)
                        {
                            if (i == 0)
                            {
                                dataSet1 = MyObject.My_Chart1.DataManipulator.ExportSeriesValues(MyObject.My_Chart1.Series[0].Name);
                                MyObject.DT.Columns.Add("水平轴");
                                dr[i] = MyObject.My_Chart1.ChartAreas[0].AxisX.Title;
                            }
                            else if (i == 1)
                            {
                                dataSet1 = MyObject.My_Chart1.DataManipulator.ExportSeriesValues(MyObject.My_Chart1.Series[0].Name);
                                MyObject.DT.Columns.Add("序列1");
                                dr[i] = MyObject.My_Chart1.Series[0].Name;
                            }
                            else
                            {
                                dataSet1 = MyObject.My_Chart1.DataManipulator.ExportSeriesValues(MyObject.My_Chart1.Series[i - 1].Name);
                                MyObject.DT.Columns.Add("序列" + i);
                                dr[i] = MyObject.My_Chart1.Series[i - 1].Name;
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                dataSet1 = MyObject.My_Chart1.DataManipulator.ExportSeriesValues(MyObject.My_Chart1.Series[0].Name);
                                dr[i] = dataSet1.Tables[0].Rows[k - 1][0].ToString();
                            }
                            else if (i == 1)
                            {
                                dataSet1 = MyObject.My_Chart1.DataManipulator.ExportSeriesValues(MyObject.My_Chart1.Series[0].Name);
                                dr[i] = dataSet1.Tables[0].Rows[k - 1][1].ToString();
                            }
                            else
                            {
                                dataSet1 = MyObject.My_Chart1.DataManipulator.ExportSeriesValues(MyObject.My_Chart1.Series[i - 1].Name);
                                dr[i] = dataSet1.Tables[0].Rows[k - 1][i - 1].ToString();
                            }
                        }
                    }
                    MyObject.DT.Rows.Add(dr);
                }
            }
            ReadDataAll read = new ReadDataAll();
            read.ShowDialog();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeAllBoder();
            foreach (Control c in this.panel1.Controls)
            {
                if (c is Chart)
                {
                    #region 实现这样一种效果，当鼠标点击主界面的时候，所有的图的边框（图题，边框，DataPoints都消失达到没有被选选中的效果）
                    if (MyObject.My_Chart1 != null)
                    {
                        //ChangeAllBoder();

                        foreach (DataPoint point in MyObject.My_Chart1.Series[0].Points)
                        {
                            point["Exploded"] = "false";  //每次单击的时候都让饼图初始合在一起
                        }
                    }
                    #endregion
                }

            }
            //把当前选中状态清空
            MyObject.My_Chart1 = null;
            MyObject.MyAnnotation1 = null;
            MyObject.MyTextBox1 = null;
            MyObject.MyTitle1 = null;
            MyObject.MyAxisX1 = null;
            MyObject.MyAxisY1 = null;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //修改字体颜色
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                if (toolStripComboBox1.Enabled == true)
                {
                    if (toolStripComboBox1.SelectedIndex == 0)//图表区
                    {
                        //MyObject.MyAnnotation1.ForeColor = cd.Color;
                        MyObject.My_Chart1.Titles[0].ForeColor = cd.Color;
                        //MyObject.My_Chart1.BackColor = cd.Color;
                        if (MyObject.My_Chart1.Legends.Count == 1)//图例字体
                        {
                            MyObject.My_Chart1.Legends[0].ForeColor = cd.Color;
                        }
                        for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)//水平轴字体
                        {
                            MyObject.My_Chart1.ChartAreas[i].AxisX.TitleForeColor = cd.Color;
                            MyObject.My_Chart1.ChartAreas[i].AxisX.LabelStyle.ForeColor = cd.Color;
                        }
                        for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)//垂直轴字体
                        {
                            MyObject.My_Chart1.ChartAreas[i].AxisY.TitleForeColor = cd.Color;
                            MyObject.My_Chart1.ChartAreas[i].AxisY.LabelStyle.ForeColor = cd.Color;
                        }
                        for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                        {
                            if (toolStripComboBox1.SelectedItem.ToString() == "序列" + (i + 1))
                            {
                                //for (int j = 0; j < MyObject.My_Chart1.Series[i].Points.Count; j++)
                                //{
                                //    MyObject.My_Chart1.Series[i].Points[j].Color = cd.Color;
                                //}
                                //MyObject.My_Chart1.Series[i].Color = cd.Color;
                                MyObject.My_Chart1.Series[i].LabelForeColor = cd.Color;
                            }
                        }
                    }
                    else if (toolStripComboBox1.SelectedIndex == 1)//图例区
                    {
                        if (MyObject.My_Chart1.Legends.Count == 1)
                        {
                            MyObject.My_Chart1.Legends[0].ForeColor = cd.Color;
                        }
                    }
                    else if (toolStripComboBox1.SelectedIndex == 2)
                    {
                        //for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                        //{
                        //    MyObject.My_Chart1.ChartAreas[i].BackColor = cd.Color;
                        //}
                        toolStripButton3.Enabled = false;
                    }
                    else if (toolStripComboBox1.SelectedIndex == 3)//水平轴区
                    {
                        for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                        {
                            MyObject.My_Chart1.ChartAreas[i].AxisX.TitleForeColor = cd.Color;
                            MyObject.My_Chart1.ChartAreas[i].AxisX.LabelStyle.ForeColor = cd.Color;
                        }
                    }
                    else if (toolStripComboBox1.SelectedIndex == 4)//垂直轴区
                    {
                        for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                        {
                            MyObject.My_Chart1.ChartAreas[i].AxisY.TitleForeColor = cd.Color;
                            MyObject.My_Chart1.ChartAreas[i].AxisY.LabelStyle.ForeColor = cd.Color;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                        {
                            if (toolStripComboBox1.SelectedItem.ToString() == "序列" + (i + 1))
                            {
                                //for (int j = 0; j < MyObject.My_Chart1.Series[i].Points.Count; j++)
                                //{
                                //    MyObject.My_Chart1.Series[i].Points[j].Color = cd.Color;
                                //}
                                //MyObject.My_Chart1.Series[i].Color = cd.Color;
                                MyObject.My_Chart1.Series[i].LabelForeColor = cd.Color;
                            }
                        }
                    }
                }
                else
                {
                    if (MyObject.MyAnnotation1 != null)
                    {
                        MyObject.MyAnnotation1.ForeColor = cd.Color;
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //修改形状填充颜色
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                if (toolStripComboBox1.Enabled == true)
                {
                    if (toolStripComboBox1.SelectedIndex == 0)//图表区
                    {
                        MyObject.My_Chart1.BackColor = cd.Color;
                    }
                    else if (toolStripComboBox1.SelectedIndex == 1)//图例区
                    {
                        if (MyObject.My_Chart1.Legends.Count == 1)
                        {
                            MyObject.My_Chart1.Legends[0].BackColor = cd.Color;
                        }
                    }
                    else if (toolStripComboBox1.SelectedIndex == 2)//绘图区
                    {
                        for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                        {
                            MyObject.My_Chart1.ChartAreas[i].BackColor = cd.Color;
                        }
                    }
                    else if (toolStripComboBox1.SelectedIndex == 3)//水平轴区
                    {
                        //for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                        //{
                        //    MyObject.My_Chart1= cd.Color;
                        //    MyObject.My_Chart1.ChartAreas[i].AxisX.LabelStyle.ForeColor = cd.Color;
                        //}
                    }
                    else if (toolStripComboBox1.SelectedIndex == 4)//垂直轴区
                    {
                        //for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                        //{
                        //    MyObject.My_Chart1.ChartAreas[i].AxisY. = cd.Color;
                        //    MyObject.My_Chart1.ChartAreas[i].AxisY.LabelStyle.ForeColor = cd.Color;
                        //}
                    }
                    else//序列区
                    {
                        for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                        {
                            if (toolStripComboBox1.SelectedItem.ToString() == "序列" + (i + 1))
                            {
                                for (int j = 0; j < MyObject.My_Chart1.Series[i].Points.Count; j++)
                                {
                                    MyObject.My_Chart1.Series[i].Points[j].Color = cd.Color;
                                }
                                MyObject.My_Chart1.Series[i].Color = cd.Color;
                            }
                        }
                    }
                }
                else
                {
                    if (MyObject.MyAnnotation1 != null)
                    {
                        MyObject.MyAnnotation1.BackColor = cd.Color;
                    }
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //修改形状轮廓颜色
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                if (toolStripComboBox1.Enabled == true)
                {
                    if (toolStripComboBox1.SelectedIndex == 0)//图表区
                    {
                        MyObject.My_Chart1.BorderlineColor = cd.Color;
                        //MyObject.My_Chart1.BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
                        //MyObject.My_Chart1.BorderSkin.BackColor = Color.WhiteSmoke;
                        //MyObject.My_Chart1.BorderlineDashStyle = ChartDashStyle.DashDotDot ;
                    }
                    else if (toolStripComboBox1.SelectedIndex == 1)//图例区
                    {
                        if (MyObject.My_Chart1.Legends.Count == 1)
                        {
                            MyObject.My_Chart1.Legends[0].BorderColor = cd.Color;
                        }
                    }
                    else if (toolStripComboBox1.SelectedIndex == 2)//绘图区
                    {
                        for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                        {
                            MyObject.My_Chart1.ChartAreas[i].BorderColor = cd.Color;
                            //MyObject.My_Chart1.ChartAreas[i].ShadowOffset = 10;
                        }
                    }
                    else if (toolStripComboBox1.SelectedIndex == 3)//
                    {
                        //for(int i=0;i<MyObject .My_Chart1 .ChartAreas .Count ;i++)
                        //{
                        //MyObject .My_Chart1 .ChartAreas [i].AxisX .LabelStyle.bord
                        //}
                    }
                    else if (toolStripComboBox1.SelectedIndex == 4)//
                    { }
                    else//数据区
                    {
                        for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                        {
                            if (toolStripComboBox1.SelectedItem.ToString() == "序列" + (i + 1))
                            {
                                for (int j = 0; j < MyObject.My_Chart1.Series[i].Points.Count; j++)
                                {
                                    MyObject.My_Chart1.Series[i].Points[j].BorderColor = cd.Color;
                                }
                                MyObject.My_Chart1.Series[i].BorderColor = cd.Color;
                            }
                        }
                    }
                }
                else
                {
                    if (MyObject.MyAnnotation1 != null)
                    {
                        MyObject.MyAnnotation1.LineColor = cd.Color;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                //修改形状填充颜色
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    if (comboBox1.Enabled == true)
                    {
                        if (comboBox1.SelectedIndex == 0)//图表区
                        {
                            MyObject.My_Chart1.BackColor = cd.Color;
                        }
                        else if (comboBox1.SelectedIndex == 1)//图例区
                        {
                            if (MyObject.My_Chart1.Legends.Count == 1)
                            {
                                MyObject.My_Chart1.Legends[0].BackColor = cd.Color;
                            }
                        }
                        else if (comboBox1.SelectedIndex == 2)//绘图区
                        {
                            for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                            {
                                MyObject.My_Chart1.ChartAreas[i].BackColor = cd.Color;
                            }
                        }
                        else if (comboBox1.SelectedIndex == 3)//水平轴区
                        {
                            //for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                            //{
                            //    MyObject.My_Chart1= cd.Color;
                            //    MyObject.My_Chart1.ChartAreas[i].AxisX.LabelStyle.ForeColor = cd.Color;
                            //}
                        }
                        else if (comboBox1.SelectedIndex == 4)//垂直轴区
                        {
                            //for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                            //{
                            //    MyObject.My_Chart1.ChartAreas[i].AxisY. = cd.Color;
                            //    MyObject.My_Chart1.ChartAreas[i].AxisY.LabelStyle.ForeColor = cd.Color;
                            //}
                        }
                        else//序列区
                        {
                            for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                            {
                                if (comboBox1.SelectedItem.ToString() == "序列" + (i + 1))
                                {
                                    for (int j = 0; j < MyObject.My_Chart1.Series[i].Points.Count; j++)
                                    {
                                        MyObject.My_Chart1.Series[i].Points[j].Color = cd.Color;
                                    }
                                    MyObject.My_Chart1.Series[i].Color = cd.Color;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (MyObject.MyAnnotation1!=null)
                        {
                            MyObject.MyAnnotation1.BackColor = cd.Color;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                //修改字体
                FontDialog diag = new FontDialog();
                if (diag.ShowDialog() == DialogResult.OK)
                {
                    if (comboBox1.Enabled == true)
                    {
                        if (comboBox1.SelectedIndex == 0)//图表区
                        {
                            MyObject.My_Chart1.Titles[0].Font = diag.Font;
                            if (MyObject.My_Chart1.Legends.Count == 1)
                            {
                                MyObject.My_Chart1.Legends[0].Font = diag.Font;
                            }
                            for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                            {
                                MyObject.My_Chart1.ChartAreas[i].AxisX.TitleFont = diag.Font;
                                MyObject.My_Chart1.ChartAreas[i].AxisX.LabelStyle.Font = diag.Font;
                            }
                            for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                            {
                                MyObject.My_Chart1.ChartAreas[i].AxisY.TitleFont = diag.Font;
                                MyObject.My_Chart1.ChartAreas[i].AxisY.LabelStyle.Font = diag.Font;
                            }
                            for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                            {
                                if (comboBox1.SelectedItem.ToString() == "序列" + (i + 1))
                                {
                                    MyObject.My_Chart1.Series[i].Font = diag.Font;
                                }
                            }
                        }
                        else if (comboBox1.SelectedIndex == 1)//图例区
                        {
                            if (MyObject.My_Chart1.Legends.Count == 1)
                            {
                                MyObject.My_Chart1.Legends[0].Font = diag.Font;
                            }
                        }
                        else if (comboBox1.SelectedIndex == 3)//水平轴区
                        {
                            for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                            {
                                MyObject.My_Chart1.ChartAreas[i].AxisX.TitleFont = diag.Font;
                                MyObject.My_Chart1.ChartAreas[i].AxisX.LabelStyle.Font = diag.Font;
                            }
                        }
                        else if (comboBox1.SelectedIndex == 4)//垂直轴区
                        {
                            for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                            {
                                MyObject.My_Chart1.ChartAreas[i].AxisY.TitleFont = diag.Font;
                                MyObject.My_Chart1.ChartAreas[i].AxisY.LabelStyle.Font = diag.Font;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                            {
                                if (comboBox1.SelectedItem.ToString() == "序列" + (i + 1))
                                {
                                    MyObject.My_Chart1.Series[i].Font = diag.Font;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (MyObject.MyAnnotation1 != null)
                        {
                            MyObject.MyAnnotation1.Font = diag.Font;
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                //修改字体颜色
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    if (comboBox1.Enabled == true)
                    {
                        if (comboBox1.SelectedIndex == 0)//图表区
                        {
                            MyObject.My_Chart1.Titles[0].ForeColor = cd.Color;
                            //MyObject.My_Chart1.BackColor = cd.Color;
                            if (MyObject.My_Chart1.Legends.Count == 1)//图例字体
                            {
                                MyObject.My_Chart1.Legends[0].ForeColor = cd.Color;
                            }
                            for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)//水平轴字体
                            {
                                MyObject.My_Chart1.ChartAreas[i].AxisX.TitleForeColor = cd.Color;
                                MyObject.My_Chart1.ChartAreas[i].AxisX.LabelStyle.ForeColor = cd.Color;
                            }
                            for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)//垂直轴字体
                            {
                                MyObject.My_Chart1.ChartAreas[i].AxisY.TitleForeColor = cd.Color;
                                MyObject.My_Chart1.ChartAreas[i].AxisY.LabelStyle.ForeColor = cd.Color;
                            }
                            for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                            {
                                if (comboBox1.SelectedItem.ToString() == "序列" + (i + 1))
                                {
                                    //for (int j = 0; j < MyObject.My_Chart1.Series[i].Points.Count; j++)
                                    //{
                                    //    MyObject.My_Chart1.Series[i].Points[j].Color = cd.Color;
                                    //}
                                    //MyObject.My_Chart1.Series[i].Color = cd.Color;
                                    MyObject.My_Chart1.Series[i].LabelForeColor = cd.Color;
                                }
                            }
                        }
                        else if (comboBox1.SelectedIndex == 1)//图例区
                        {
                            if (MyObject.My_Chart1.Legends.Count == 1)
                            {
                                MyObject.My_Chart1.Legends[0].ForeColor = cd.Color;
                            }
                        }
                        else if (comboBox1.SelectedIndex == 2)
                        {
                            //for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                            //{
                            //    MyObject.My_Chart1.ChartAreas[i].BackColor = cd.Color;
                            //}
                            //toolStripButton3.Enabled = false;
                        }
                        else if (comboBox1.SelectedIndex == 3)//水平轴区
                        {
                            for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                            {
                                MyObject.My_Chart1.ChartAreas[i].AxisX.TitleForeColor = cd.Color;
                                MyObject.My_Chart1.ChartAreas[i].AxisX.LabelStyle.ForeColor = cd.Color;
                            }
                        }
                        else if (comboBox1.SelectedIndex == 4)//垂直轴区
                        {
                            for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                            {
                                MyObject.My_Chart1.ChartAreas[i].AxisY.TitleForeColor = cd.Color;
                                MyObject.My_Chart1.ChartAreas[i].AxisY.LabelStyle.ForeColor = cd.Color;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                            {
                                if (comboBox1.SelectedItem.ToString() == "序列" + (i + 1))
                                {
                                    //for (int j = 0; j < MyObject.My_Chart1.Series[i].Points.Count; j++)
                                    //{
                                    //    MyObject.My_Chart1.Series[i].Points[j].Color = cd.Color;
                                    //}
                                    //MyObject.My_Chart1.Series[i].Color = cd.Color;
                                    MyObject.My_Chart1.Series[i].LabelForeColor = cd.Color;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (MyObject.MyAnnotation1 != null)
                        {
                            MyObject.MyAnnotation1.ForeColor = cd.Color;
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                //修改形状轮廓颜色
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    if (comboBox1.Enabled == true)
                    {
                        if (comboBox1.SelectedIndex == 0)//图表区
                        {
                            MyObject.My_Chart1.BorderlineColor = cd.Color;
                            //MyObject.My_Chart1.BorderSkin.SkinStyle = BorderSkinStyle.FrameThin1;
                            //MyObject.My_Chart1.BorderSkin.BackColor = Color.WhiteSmoke;
                            //MyObject.My_Chart1.BorderlineDashStyle = ChartDashStyle.DashDotDot ;
                        }
                        else if (comboBox1.SelectedIndex == 1)//图例区
                        {
                            if (MyObject.My_Chart1.Legends.Count == 1)
                            {
                                MyObject.My_Chart1.Legends[0].BorderColor = cd.Color;
                            }
                        }
                        else if (comboBox1.SelectedIndex == 2)//绘图区
                        {
                            for (int i = 0; i < MyObject.My_Chart1.ChartAreas.Count; i++)
                            {
                                MyObject.My_Chart1.ChartAreas[i].BorderColor = cd.Color;
                                //MyObject.My_Chart1.ChartAreas[i].ShadowOffset = 10;
                            }
                        }
                        else if (comboBox1.SelectedIndex == 3)//
                        {
                            //for(int i=0;i<MyObject .My_Chart1 .ChartAreas .Count ;i++)
                            //{
                            //MyObject .My_Chart1 .ChartAreas [i].AxisX .LabelStyle.bord
                            //}
                        }
                        else if (comboBox1.SelectedIndex == 4)//
                        { }
                        else//数据区
                        {
                            for (int i = 0; i < MyObject.My_Chart1.Series.Count; i++)
                            {
                                if (comboBox1.SelectedItem.ToString() == "序列" + (i + 1))
                                {
                                    for (int j = 0; j < MyObject.My_Chart1.Series[i].Points.Count; j++)
                                    {
                                        MyObject.My_Chart1.Series[i].Points[j].BorderColor = cd.Color;
                                    }
                                    MyObject.My_Chart1.Series[i].BorderColor = cd.Color;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (MyObject.MyAnnotation1 != null)
                        {
                            MyObject.MyAnnotation1.LineColor = cd.Color;
                        }
                    }
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MyObject.My_Chart1.Annotations.Remove(MyObject.MyAnnotation1);
            if (MyObject.My_Chart1.Annotations.Count == 0)
            {
                toolStripMenuItem2.Enabled = false;
            }
        }

        private void buttonX14_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox14_Enter(object sender, EventArgs e)
        {

        }

        private void office2007StartButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            btnSaveAsTemp2Local_Click(sender,e);
            foreach (Control ctr in this.panel1.Controls)
            {
                this.panel1.Controls.Remove(ctr);
            }
        }

        private void 数据联动窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MyObject.My_Chart1 != null)
            {
                if (MyObject.My_Chart1.Series[0].Points.Count == 0)
                {
                    MessageBox.Show("请您先读取数据");
                    return;
                }
                if (MyObject.DT != null)
                {
                    MyObject.DT.Columns.Clear();
                    MyObject.DT.Rows.Clear();
                }
                DataSet dataSet1 = new DataSet();
                DataTable dataTable1 = dataSet1.Tables.Add();
                for (int k = 0; k < MyObject.My_Chart1.Series[0].Points.Count + 1; k++)
                {
                    DataRow dr = MyObject.DT.NewRow();
                    for (int i = 0; i < MyObject.My_Chart1.Series.Count + 1; i++)
                    {
                        if (k == 0)
                        {
                            if (i == 0)
                            {
                                dataSet1 = MyObject.My_Chart1.DataManipulator.ExportSeriesValues(MyObject.My_Chart1.Series[0].Name);
                                MyObject.DT.Columns.Add("水平轴");
                                dr[i] = MyObject.My_Chart1.ChartAreas[0].AxisX.Title;
                            }
                            else if (i == 1)
                            {
                                dataSet1 = MyObject.My_Chart1.DataManipulator.ExportSeriesValues(MyObject.My_Chart1.Series[0].Name);
                                MyObject.DT.Columns.Add("序列1");
                                dr[i] = MyObject.My_Chart1.Series[0].Name;
                            }
                            else
                            {
                                dataSet1 = MyObject.My_Chart1.DataManipulator.ExportSeriesValues(MyObject.My_Chart1.Series[i - 1].Name);
                                MyObject.DT.Columns.Add("序列" + i);
                                dr[i] = MyObject.My_Chart1.Series[i - 1].Name;
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                dataSet1 = MyObject.My_Chart1.DataManipulator.ExportSeriesValues(MyObject.My_Chart1.Series[0].Name);
                                dr[i] = dataSet1.Tables[0].Rows[k - 1][0].ToString();
                            }
                            else if (i == 1)
                            {
                                dataSet1 = MyObject.My_Chart1.DataManipulator.ExportSeriesValues(MyObject.My_Chart1.Series[0].Name);
                                dr[i] = dataSet1.Tables[0].Rows[k - 1][1].ToString();
                            }
                            else
                            {
                                dataSet1 = MyObject.My_Chart1.DataManipulator.ExportSeriesValues(MyObject.My_Chart1.Series[i - 1].Name);
                                dr[i] = dataSet1.Tables[0].Rows[k - 1][i - 1].ToString();
                            }
                        }
                    }
                    MyObject.DT.Rows.Add(dr);
                }
                LinkageFrm lf = new LinkageFrm();
                lf.TopLevel = false;
                this.panel1.Controls.Add(lf);
                lf.Show();
            }

        }


        /////////////////////////////////////////主界面上其他代码结束///////////////////////////////////////////////////////////////
        //}

    }
}