using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GeoDemo
{
    class Oilwater_Data
    {
        public static float a, b, Rw, m, n, Top_Y,K,C;//参数
        public static PointF[] Draw_point;//保存转化后的点
        public static string[] Draw_str;//保存油层地质
        //保存横坐标形式
        public static string str_X;
        //保存纵坐标形式
        public static string str_Y;

        //全局结构体
        public struct region_Area
        {
            public Region Area;
            public string AreaName;
            public SolidBrush AreaBrush;
        }
        public static region_Area[] OW_Area=new region_Area[5];//存放每个区域
       
        
    }
}
