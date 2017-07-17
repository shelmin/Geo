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
using System.Runtime.Serialization;
using System.Drawing.Drawing2D;
using Plytmf.Net.Bottom;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Globalization;


namespace GeoDemo
{
    [Serializable]
    public class FormState:ISerializable
    {
        public static  bool IsData;
        //public static bool IsTitleText;
        //public static bool IsPenColor;
        //public static bool IsSolidBrushColor;
        //public static bool IsFrmName;
        //public static bool IsTitleColor;
        //public static bool IsTitleFont;
        //public static bool IsPenColor1;
        //public static bool IsPencolor2;
        //public static bool IsPenSize1;
        //public static bool IsPenSize2;
        //public static bool IsXBrush;
        //public static bool IsYBrush;
        //public static bool IsXFont;
        //public static bool IsYFont;
        //public static bool IsXnameFont;
        //public static bool IsXnameColor;
        //public static bool IsXnameText;
        //public static bool ISYnameFont;
        //public static bool IsYnameColor;
        //public static bool IsYnameText;
        //public static bool IsXCheck;
        //public static bool IsYCheck;
        public Pen Pen { set; get; }
        public SolidBrush MyBrush { set; get; }
        public Color PenColor { set; get; }
        public Color SolidBrushColor { set; get; }
        public Font LabelFont { set; get; }
         public string FrmName { set; get; }
        public string FrmName0 { set; get; }
        public string TitleText { set; get; }
        public Color TitleColor { set; get; }
        public Font TitleFont { set; get; }
        public Color PenColor1 { set; get; }
        public Color PenColor2 { set; get; }
        public int PenSize1 { set; get; }
        public int PenSize2 { set; get; }
        public DataTable Datatable { set; get; }
        //刻度
        public Color XBrush { set; get; }
        public Color YBrush { set; get; }
        public Font XFont { set; get; }
        public Font YFont { set; get; }


        //轴名
        public Font XnameFont { set; get; }
        public Color XnameColor { set; get; }
        public string XnameText { set; get; }

        public Font YnameFont { set; get; }
        public Color YnameColor { set; get; }
        public string YnameText { set; get; }

        //判断垂直
        public bool XCheck { set; get; }
        public bool YCheck { set; get; }
        public FormState() { }
        public FormState(SerializationInfo info, StreamingContext context) 
        {
            TitleText =(string) info.GetValue("TitleText", typeof(string ));
            PenColor =(Color ) info.GetValue("PenColor", typeof(Color ));
            SolidBrushColor =(Color ) info.GetValue("SolidBrushColor", typeof (Color ));
            FrmName =(string ) info.GetValue("FrmName", typeof (string));
            TitleFont  =(Font)info.GetValue("TitleFont",typeof (Font));
            PenColor1 = (Color )info.GetValue("PenColor1",typeof (Color ));
            TitleColor =(Color ) info.GetValue("TitleColor",typeof (Color ));
            PenColor2 =(Color ) info.GetValue("PenColor2",typeof (Color ));
            PenSize1 =(int) info.GetValue("PenSize1", typeof (int));
            PenSize2 = (int)info.GetValue("PenSize2", typeof(int));
            if (Datatable != null)
            {
               Datatable =(DataTable ) info.GetValue("Datatable", typeof ( DataTable));
            }
            XBrush =(Color ) info.GetValue("XBrush",typeof (Color ));
            YBrush =(Color ) info.GetValue("YBrush",typeof (Color ));
            XFont =(Font ) info.GetValue("XFont",typeof (Font ));
            YFont = (Font)info.GetValue("YFont", typeof(Font));
            XnameFont =(Font ) info.GetValue("XnameFont",typeof (Font ));
            XnameColor =(Color ) info.GetValue("XnameColor",typeof (Color ));
            XnameText =(string ) info.GetValue("XnameText",typeof(string ));
            YnameFont = (Font)info.GetValue("YnameFont", typeof(Font));
            YnameColor = (Color)info.GetValue("YnameColor", typeof(Color));
            YnameText = (string)info.GetValue("YnameText", typeof(string));
            XCheck =(bool ) info.GetValue("XCheck",typeof(bool));
            YCheck = (bool)info.GetValue("YCheck", typeof(bool));
        } 
        public FormState(Color pencolor,Color solidbrushcolor,Font lablfont)
        {
            this.PenColor = pencolor;
            this.SolidBrushColor = solidbrushcolor;
            this.LabelFont = lablfont;
        }
       

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TitleText", TitleText);
            info.AddValue("PenColor", PenColor);
            info.AddValue("SolidBrushColor", SolidBrushColor);
            info.AddValue("FrmName", FrmName);
            info.AddValue("TitleFont", TitleFont);
            info.AddValue("PenColor1", PenColor1);
            info.AddValue("TitleColor", TitleColor);
            info.AddValue("PenColor2", PenColor2);
            info.AddValue("PenSize1", PenSize1);
            info.AddValue("PenSize2", PenSize2);
            if (Datatable != null)
            {
                info.AddValue("Datatable", Datatable);
            }
            info.AddValue("XBrush", XBrush);
            info.AddValue("YBrush", YBrush);
            info.AddValue("XFont", XFont);
            info.AddValue("YFont", YFont);
            info.AddValue("XnameFont", XnameFont);
            info.AddValue("XnameColor", XnameColor);
            info.AddValue("XnameText", XnameText);
            info.AddValue("YnameFont", YnameFont);
            info.AddValue("YnameText", YnameText);
            info.AddValue("YnameColor", YnameColor);
            info.AddValue("XCheck", XCheck);
            info.AddValue("YCheck", YCheck);
        }

        public FormState(Pen Cp, SolidBrush Mybrush,Font LabelFont,string frmname)
        {
            this.Pen=Cp;
            this.MyBrush = Mybrush;
            this.LabelFont = LabelFont;
            this.FrmName = frmname;

        }
        public FormState(string titletext, Color titlecolor, Font titlefont)//专为萨呼成因
        {
            this.TitleText = titletext;
            this.TitleColor = titlecolor;
            this.TitleFont = titlefont;
        }
        public FormState(string titletext, Color titlecolor, Font titlefont,string frmname)//专为萨呼成因
        {
            this.TitleText = titletext;
            this.TitleColor = titlecolor;
            this.TitleFont = titlefont;
            //this.FrmName = frmname;
            this.FrmName = frmname;
        }
        public FormState(string titletext, Color titlecolor, Font titlefont, Color pencolor1, Color pencolor2, int pensize1, int pensize2,DataTable datatable,
            Color xbrush, Color ybrush, Font xfont, Font yfont,Font xnamefont,Color xnamecolor,Font ynamefont,Color ynamecolor)
        {
            this.TitleText = titletext;
            this.TitleColor = titlecolor;
            this.TitleFont = titlefont;
            this.PenColor1 = pencolor1;
            this.PenColor2 = pencolor2;
            this.PenSize1 = pensize1;
            this.PenSize2 = pensize2;
            this.Datatable = datatable;
            this.XBrush = xbrush;
            this.YBrush = ybrush;
            this.XFont = xfont;
            this.YFont = yfont;
            this.XnameFont = xnamefont;
            this.XnameColor = xnamecolor;
            this.YnameFont = ynamefont;
            this.YnameColor = ynamecolor;

        }
        public FormState(string titletext, Color titlecolor, Font titlefont, Color pencolor1, Color pencolor2, int pensize1, int pensize2, string frmname,DataTable datatable,
            Color xbrush, Color ybrush, Font xfont, Font yfont, Font xnamefont, Color xnamecolor, Font ynamefont, Color ynamecolor)//专为粒度概率和测井曲线
        {
            this.TitleText = titletext;
            this.TitleColor = titlecolor;
            this.TitleFont = titlefont;
            this.PenColor1 = pencolor1;
            this.PenColor2 = pencolor2;
            this.PenSize1 = pensize1;
            this.PenSize2 = pensize2;
            this.FrmName = frmname;
            this.Datatable = datatable;
            this.XBrush = xbrush;
            this.YBrush = ybrush;
            this.XFont = xfont;
            this.YFont = yfont;
            this.XnameFont = xnamefont;
            this.XnameColor = xnamecolor;
            this.YnameFont = ynamefont;
            this.YnameColor = ynamecolor;
        }
        public FormState(string titletext, Color titlecolor, Font titlefont, Color pencolor1, Color pencolor2, int pensize1, int pensize2, DataTable datatable,
            Color xbrush,Color ybrush,Font xfont,Font yfont,Font xnamefont,Color xnamecolor,string xnametext,Font ynamefont,Color ynamecolor,
            string ynametext,bool xcheck,bool ycheck)
        {
            this.TitleText = titletext;
            this.TitleColor = titlecolor;
            this.TitleFont = titlefont;
            this.PenColor1 = pencolor1;
            this.PenColor2 = pencolor2;
            this.PenSize1 = pensize1;
            this.PenSize2 = pensize2;
            this.Datatable = datatable;
            this.XBrush = xbrush;
            this.YBrush = ybrush;
            this.XFont = xfont;
            this.YFont = yfont;
            this.XnameFont = xnamefont;
            this.XnameColor = xnamecolor;
            this.XnameText = xnametext;
            this.YnameFont = ynamefont;
            this.YnameColor = ynamecolor;
            this.YnameText = ynametext;
            this.XCheck = xcheck;
            this.YCheck = ycheck;

        }
        public FormState(string titletext, Color titlecolor, Font titlefont, Color pencolor1, Color pencolor2, int pensize1, int pensize2, string frmname, DataTable datatable,
             Color xbrush,Color ybrush,Font xfont,Font yfont,Font xnamefont,Color xnamecolor,string xnametext,Font ynamefont,Color ynamecolor,
            string ynametext,bool xcheck,bool ycheck)//全属性
        {
            this.TitleText = titletext;
            this.TitleColor = titlecolor;
            this.TitleFont = titlefont;
            this.PenColor1 = pencolor1;
            this.PenColor2 = pencolor2;
            this.PenSize1 = pensize1;
            this.PenSize2 = pensize2;
            this.FrmName = frmname;
            this.Datatable = datatable;
            this.XBrush = xbrush;
            this.YBrush = ybrush;
            this.XFont = xfont;
            this.YFont = yfont;
            this.XnameFont = xnamefont;
            this.XnameColor = xnamecolor;
            this.XnameText = xnametext;
            this.YnameFont = ynamefont;
            this.YnameColor = ynamecolor;
            this.YnameText = ynametext;
            this.XCheck = xcheck;
            this.YCheck = ycheck;
        }


        public void Serialize1(MemoryStream stream, BinaryFormatter bformatter)
        {
            bformatter.Serialize(stream, this.FrmName);
            bformatter.Serialize(stream, this.TitleText);
            bformatter.Serialize(stream, TitleColor);
            bformatter.Serialize(stream, TitleFont);
            
        }

        public void Serialize2(MemoryStream stream,BinaryFormatter bformatter) 
        {
            bformatter.Serialize(stream, this.FrmName);
            bformatter.Serialize(stream, this.TitleText);
            bformatter.Serialize(stream, TitleColor);
            bformatter.Serialize(stream, TitleFont);
            bformatter.Serialize(stream, PenColor1);
            bformatter.Serialize(stream, PenColor2);
            bformatter.Serialize(stream, PenSize1);
            bformatter.Serialize(stream, PenSize2);
            if (Datatable !=null )
            {
                IsData = true;
                bformatter.Serialize(stream, Datatable);
            }
            bformatter.Serialize(stream, XBrush);
            bformatter.Serialize(stream, YBrush);
            bformatter.Serialize(stream, XFont);
            bformatter.Serialize(stream, YFont);
            bformatter.Serialize(stream, XnameFont);
            bformatter.Serialize(stream, XnameColor);
            bformatter.Serialize(stream, YnameFont);
            bformatter.Serialize(stream, YnameColor);
        }


        public void Serialize(MemoryStream stream, BinaryFormatter bformatter) 
        {
                bformatter.Serialize(stream, this.FrmName);
                    bformatter.Serialize(stream, this.TitleText);
                    bformatter.Serialize(stream, this.PenColor);
                    bformatter.Serialize(stream, this.SolidBrushColor);
                    bformatter.Serialize(stream, TitleFont);
                    bformatter.Serialize(stream, PenColor1);
                    bformatter.Serialize(stream, TitleColor);
                    bformatter.Serialize(stream, PenColor2);
                    bformatter.Serialize(stream, PenSize1);
                    bformatter.Serialize(stream, PenSize2);
                    if (Datatable !=null )
                    {
                        IsData = true;
                        bformatter.Serialize(stream, Datatable);
                    }
                    bformatter.Serialize(stream, XBrush);
                    bformatter.Serialize(stream, YBrush);
                    bformatter.Serialize(stream, XFont);
                    bformatter.Serialize(stream, YFont);
                bformatter.Serialize(stream, XnameFont);
                bformatter.Serialize(stream, XnameColor);
                bformatter.Serialize(stream, XnameText);
                bformatter.Serialize(stream, YnameFont);
                bformatter.Serialize(stream, YnameText);
                bformatter.Serialize(stream, YnameColor);
                bformatter.Serialize(stream, XCheck);
                bformatter.Serialize(stream, YCheck);
        }
        public FormState  Deserialize(MemoryStream stream, BinaryFormatter bformatter) 
        {
            FormState fs = new FormState();
            fs.FrmName = (string)bformatter.Deserialize(stream);
            switch (fs.FrmName )
            {
                case "主图":
                case "油气水":
                case "C_M1":
                case "C_M2":
                    fs.TitleText = (string)bformatter.Deserialize(stream);
                    fs.PenColor = (Color)bformatter.Deserialize(stream);
                    fs.SolidBrushColor = (Color)bformatter.Deserialize(stream);

                    fs.TitleFont = (Font)bformatter.Deserialize(stream);
                    fs.PenColor1 = (Color)bformatter.Deserialize(stream);
                    fs.TitleColor = (Color)bformatter.Deserialize(stream);
                    fs.PenColor2 = (Color)bformatter.Deserialize(stream);
                    fs.PenSize1 = (int)bformatter.Deserialize(stream);
                    fs.PenSize2 = (int)bformatter.Deserialize(stream);
                    if (IsData )
                    {
                        fs.Datatable = (DataTable)bformatter.Deserialize(stream);
                    }
                    fs.XBrush = (Color)bformatter.Deserialize(stream);
                    fs.YBrush = (Color)bformatter.Deserialize(stream);
                    fs.XFont = (Font)bformatter.Deserialize(stream);
                    fs.YFont = (Font)bformatter.Deserialize(stream);
                    fs.XnameFont = (Font)bformatter.Deserialize(stream);
                    fs.XnameColor = (Color)bformatter.Deserialize(stream);
                    fs.XnameText = (string)bformatter.Deserialize(stream);
                    fs.YnameFont = (Font)bformatter.Deserialize(stream);
                    fs.YnameText = (string)bformatter.Deserialize(stream);
                    fs.YnameColor = (Color)bformatter.Deserialize(stream);
                    fs.XCheck = (bool)bformatter.Deserialize(stream);
                    fs.YCheck = (bool)bformatter.Deserialize(stream);
                    break;
                case "萨胡成因判别函数":
                    fs.TitleText = (string)bformatter.Deserialize(stream);
                    fs.TitleColor = (Color)bformatter.Deserialize(stream);
                    fs.TitleFont = (Font)bformatter.Deserialize(stream);
                    break;
                case "粒度概率累计曲线":
                case "测井曲线":
                    fs.TitleText = (string)bformatter.Deserialize(stream);
                    fs.TitleColor = (Color)bformatter.Deserialize(stream);
                    fs.TitleFont = (Font)bformatter.Deserialize(stream);
                    fs.PenColor1 = (Color)bformatter.Deserialize(stream);
                    fs.PenColor2 = (Color)bformatter.Deserialize(stream);
                    fs.PenSize1 = (int)bformatter.Deserialize(stream);
                    fs.PenSize2 = (int)bformatter.Deserialize(stream);
                    if (IsData )
                    {
                        fs.Datatable = (DataTable)bformatter.Deserialize(stream);
                    }
                    fs.XBrush = (Color)bformatter.Deserialize(stream);
                    fs.YBrush = (Color)bformatter.Deserialize(stream);
                    fs.XFont = (Font)bformatter.Deserialize(stream);
                    fs.YFont = (Font)bformatter.Deserialize(stream);
                    fs.XnameFont = (Font)bformatter.Deserialize(stream);
                    fs.XnameColor = (Color)bformatter.Deserialize(stream);
                    fs.YnameFont = (Font)bformatter.Deserialize(stream);
                    fs.YnameColor = (Color)bformatter.Deserialize(stream);
                    break;
                default:
                    break;
            }
           
            return fs;
        }
    }
}
