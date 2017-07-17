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
using System.Threading;
using System.Xml;

namespace GeoDemo
{
    class Xml2Byte
    {
        public  static void CreateNewXml() 
        {
            //首先创建 XmlDocument xml文档
            XmlDocument xml = new XmlDocument();

            //创建根节点 config
            XmlElement config = xml.CreateElement("Config");
            //把根节点加到xml文档中
            xml.AppendChild(config);

            //创建一个节点 path(用于做子节点)
            XmlElement path = xml.CreateElement("Path");
            //path节点中的文本内容为 E:\Test\ @用于转义后面的'\'
            path.InnerText = @"C:\Users\zhangzh\Desktop\地质\GeoDemoBeta1.0版(0121)\GeoDemoBeta1.0版\GeoDemo\GeoDemo\bin\Debug\";
            //将path添加为config的子节点
            config.AppendChild(path);

            //以下Regex同理
            XmlElement regex = xml.CreateElement("Regex");
            regex.InnerText = "<![CDDATA[@^abc$]]>";
            config.AppendChild(regex);


            XmlElement ini = xml.CreateElement("ini");
            //所以我们需要创建 ini标签里的xml属性 属性名为timeout
            XmlAttribute timeout = xml.CreateAttribute("timeout");
            //timeout属性的内容为200
            timeout.InnerText = "200";
            //标签ini里的文档内容为 time
            ini.InnerText = "time";
            //创建完标签的属性timeout 后需要将其添加到ini标签的属性里
            ini.Attributes.Append(timeout);
            //最后将ini标签添加到config 父节点里
            config.AppendChild(ini);

            //最后将整个xml文件保存在D盘 
            xml.Save(Application.StartupPath + @"\abc.xml");
            //xml.RemoveAll();
        }

        public static void OCreateNewXml() 
        {
            //首先创建 XmlDocument xml文档
            XmlDocument xml = new XmlDocument();

            //创建根节点 config
            XmlElement config = xml.CreateElement("Config");
            //把根节点加到xml文档中
            xml.AppendChild(config);

            //创建一个节点 path(用于做子节点)
            XmlElement path = xml.CreateElement("Path");
            //path节点中的文本内容为 E:\Test\ @用于转义后面的'\'
            path.InnerText = @"C:\Users\zhangzh\Desktop\地质\GeoDemoBeta1.0版(0121)\GeoDemoBeta1.0版\GeoDemo\GeoDemo\bin\Debug\";
            //将path添加为config的子节点
            config.AppendChild(path);

            //以下Regex同理
            XmlElement regex = xml.CreateElement("Regex");
            regex.InnerText = "<![CDDATA[@^abc$]]>";
            config.AppendChild(regex);


            XmlElement ini = xml.CreateElement("ini");
            //所以我们需要创建 ini标签里的xml属性 属性名为timeout
            XmlAttribute timeout = xml.CreateAttribute("timeout");
            //timeout属性的内容为200
            timeout.InnerText = "200";
            //标签ini里的文档内容为 time
            ini.InnerText = "time";
            //创建完标签的属性timeout 后需要将其添加到ini标签的属性里
            ini.Attributes.Append(timeout);
            //最后将ini标签添加到config 父节点里
            config.AppendChild(ini);

            //最后将整个xml文件保存在D盘 
            xml.Save(Application.StartupPath + @"\abcd.xml");
            //xml.RemoveAll();
        }
        
    }
}
