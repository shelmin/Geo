/*
 * 作者;肖宇博
 * 日期：2014/6/19
 * 功能：这里是保存SetProperty这个窗体的一些设置，当用户再次双击图的时候，弹出的属性设置对话框将会保留用户以前的一些操作习惯（模版功能的雏形）
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GeoDemo
{
    class SaveAttribute
    {
        private static int tag = 0;//代表是否勾选保存设置,0代表没有勾选

        public static int Tag
        {
            get { return SaveAttribute.tag; }
            set { SaveAttribute.tag = value; }
        }
        
        //定义饼图，圆环是否旋转
        private static bool isRotate = false;

        public static bool IsRotate
        {
            get { return SaveAttribute.isRotate; }
            set { SaveAttribute.isRotate = value; }
        }
       

    }
}
