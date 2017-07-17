using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace GeoDemo
{
    class SysData
    {
        //貌似在Common的类中定义了。此处没有什么用
        
        public static DataTable dt;//交会图专用
        public static DataTable ldglljqx_dt;//粒度概率累积专用
        public static DataTable cm1_dt;
        public static DataTable cm2_dt;
        public static string com_tab;
        //public static DataTable sckf_dt;
        //public static string[] column_list;//列名

        //存放绘制交汇图中的深度，X轴曲线值，Y轴曲线值,起始深度，结束深度
        public static double [] Depth = new double[60000];
        public static double [] Xcurve=new double [60000];
        public static double [] Ycurve=new double [60000];
        public static string SdepthValue;
        public static string EdepthValue;

        //交汇图中的
        public static OpenFileDialog abovefile = new OpenFileDialog();//全局变量，存放文件
        public static string textbox1;//存放textbox1中的数据
        public static string textbox2;//存放textbox2中的数据
        public static DataGridView dgv_Data;
        public static bool button_cancel=false;//测井曲线判断是否画红线
        public static bool button_enable_judge = false;//button是否可以用
     

        //存放比例值
        public static float K_Width=1;
        public static float K_Height=1;


        //存放setofline里的设置线性
        public static int line1 = 1;
        public static int line2 = 3;
        public static Color line_color = Color.Gray;
        public static Color line_color2 = Color.Black;
        public static string title = "双击图形在属性中修改图题";
        public static Color title_color = Color.Black;
        public static Font title_font = new Font("宋体", 12, FontStyle.Regular);
        //打印属性
        public static Bitmap PrintBit;
        
        //存放form，用来判断哪个窗口
        //public static Form Save_Form;
        
       ////////0115
        //判断是否双击过label
        public static bool IsDouble = false;
        ////////



        ///////////0120
        //判断选择的是哪个拟合函数
        public static int comboxselected;
        //是否绘制拟合曲线
        public static bool IsDrawLine = false;
        //是否绘制拟合结果
        public static bool IsDrawWords = false;




        //////////0121
        //set中的记忆属性
        public static int comselectitem = 0;//选择种类
        public static Font comboboxFont = title_font;

       

    }
}
