/* 
 * 作者;肖宇博
 * 日期：2014/6/18
 * 功能：MyChart类，当鼠标点击或者移动到那幅图，就将那幅图复制给MyChart(相当于图的副本)然后对副本进行操作，这样可以提高代码复用率
 */


using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32;
using System.Globalization;
using Plytmf.Net.Bottom;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms.DataVisualization .Charting;
using System.Linq;

namespace GeoDemo
{
    [Serializable]
    class MyObject
    {
        private static DataTable dt;

        public static DataTable DT 
        {
            get { return MyObject.dt; }
            set { MyObject.dt = value; }
        }

        private static bool addSeries;

        public static bool AddSeries 
        {
            get { return MyObject.addSeries ; }
            set { MyObject.addSeries = value; }
        }

        private static ArrayList list;

        public static ArrayList List 
        {
            get { return MyObject.list; }
            set { MyObject.list = value; }
        }

        private static int u;

        public static int U 
        {
            get { return MyObject.u; }
            set { MyObject.u = value; }
        }

        private static int o;

        public static int O 
        {
            get { return MyObject.o; }
            set { MyObject.o = value; }
        }

        private static int p;

        public static int P 
        {
            get { return MyObject.p; }
            set { MyObject.p = value;}
        }

        private static int columns;

        public static int Columns 
        {
            get { return MyObject.columns; }
            set { MyObject.columns = value; }
        }

        private static string Filepath;

        public static string Filepath1 //本地模板的文件名
        {
            get { return MyObject.Filepath; }
            set { MyObject.Filepath = value; }
        }

        private static byte[] local;//用于存放从本地打开的模板的字节

        public static byte[] local1 
        {
            get { return MyObject.local; }
            set { MyObject.local = value; }
        }

        private static int k;

        public static int k1 
        {
            get { return MyObject.k; }
            set { MyObject.k = value; }
        }

        private static ProjectInfo pfo;//信息

        public static ProjectInfo pfo1 
        {
            get { return MyObject.pfo; }
            set { MyObject.pfo = value; }
        }

        private static ProjectTemplate pro;//模板

        public static ProjectTemplate pro1 
        {
            get { return MyObject.pro; }
            set { MyObject.pro = value; }
        }

        private static byte[] cc;//存放图件信息（字节数，x值，y值）

        public static byte[] cc1 
        {
            get { return MyObject.cc; }
            set { MyObject.cc = value; }
        }

        private static int Chartnum;

        public static int Chartnum1 
        {
            get { return MyObject.Chartnum ; }
            set { MyObject.Chartnum = value; }
        }

        private static byte[] aa;//存入库中的数组
        public static byte[] aa1 
        {
            get { return MyObject.aa; }
            set { MyObject.aa = value; }
        }

        private static byte[] bb;

        public static byte[] bb1 
        {
            get { return MyObject.bb; }
            set { MyObject.bb = value; }
        }

        private static Point []point;

        public static Point[] point1 
        {
            get { return MyObject .point;}
            set { MyObject.point = value;}
        }

        private static Chart My_Chart;

        public static Chart My_Chart1
        {
            get { return MyObject.My_Chart; }
            set { MyObject.My_Chart = value; }
        }

        private static Chart My_Chart0;

        public static Chart My_Chart2
        {
            get { return MyObject.My_Chart0; }
            set { MyObject.My_Chart0 = value; }
        }
        private static int[] A;//为3段元写的用来存储投点数据的数组

        public static int[] A1
        {
            get { return MyObject.A; }
            set { MyObject.A = value; }
        }
        private static int[] B;

        public static int[] B1
        {
            get { return MyObject.B; }
            set { MyObject.B = value; }
        }
        private static int[] C;

        public static int[] C1
        {
            get { return MyObject.C; }
            set { MyObject.C = value; }
        }
        private static int Sum;//存储3段元投点个数


        public static int Sum1
        {
            get { return MyObject.Sum; }
            set { MyObject.Sum = value; }
        }

        private static DataGridView My_Datagridview;//存储3段元投点的数据表

        public static DataGridView My_Datagridview1
        {
            get { return My_Datagridview; }
            set { My_Datagridview = value; }
        }

        private static Annotation MyAnnotation;

        public static Annotation MyAnnotation1
        {
            get { return MyObject.MyAnnotation; }
            set { MyObject.MyAnnotation = value; }
        }


        private static PictureBox mypicturebox;

        public static PictureBox Mypicturebox
        {
            get { return MyObject.mypicturebox; }
            set { MyObject.mypicturebox = value; }
        }

        private static float MyPicBoxMagnification=1;//PictureBox放大倍数

        public static float MyPicBoxMagnification1
        {
            get { return MyObject.MyPicBoxMagnification; }
            set { MyObject.MyPicBoxMagnification = value; }
        }
      





        private static TextBox MyTextBox;

        public static TextBox MyTextBox1
        {
            get { return MyObject.MyTextBox; }
            set { MyObject.MyTextBox = value; }
        }
        private static Title MyTitle;

        public static Title MyTitle1
        {
            get { return MyObject.MyTitle; }
            set { MyObject.MyTitle = value; }
        }
        private static Axis MyAxisX;

        public static Axis MyAxisX1
        {
            get { return MyObject.MyAxisX; }
            set { MyObject.MyAxisX = value; }
        }
        private static Axis MyAxisY;

        public static Axis MyAxisY1
        {
            get { return MyObject.MyAxisY; }
            set { MyObject.MyAxisY = value; }
        }

        private static Pen MyPen=new Pen(Color.Black,2.0f);

        public static Pen MyPen1
        {
            get { return MyObject.MyPen; }
            set { MyObject.MyPen = value; }
        }
        private static SolidBrush MyBrush = new SolidBrush(Color.Yellow);

        public static SolidBrush MyBrush1
        {
            get { return MyObject.MyBrush; }
            set { MyObject.MyBrush = value; }
        }


        private static DataSet dataset;

        public static DataSet Dataset
        {
            get { return MyObject.dataset; }
            set { MyObject.dataset = value; }
        }

        private static string MyCurrentFrmName;

        public static string MyCurrentFrmName1
        {
            get { return MyObject.MyCurrentFrmName; }
            set { MyObject.MyCurrentFrmName = value; }
        }

        private static string MyCurrentFrmName0;

        public static string MyCurrentFrmName2
        {
            get { return MyObject.MyCurrentFrmName0; }
            set { MyObject.MyCurrentFrmName0 = value; }
        }
        private static Form MyCurrentFrm;

        public static Form MyCurrentFrm1
        {
            get { return MyObject.MyCurrentFrm; }
            set { MyObject.MyCurrentFrm = value; }
        }
        private static string FrmName;

        public static string FrmName1
        {
            get { return MyObject.FrmName; }
            set { MyObject.FrmName = value; }
        }

        private static string FrmName0;

        public static string FrmName2
        {
            get { return MyObject.FrmName0; }
            set { MyObject.FrmName0 = value; }
        }
    }
    
   
}
