/* 
 * 作者;肖宇博
 * 日期：2014/7/20
 * 功能：在这里实现模版功能，代码很简单。就是将chart图序列化，然后再保存为xml文件，读取模版的时候，可以从xml文件中读取，然后加载到一个
 * 新的chart图上，这样以来就实现了模版供能
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace GeoDemo
{
    class XmlHelper
    {
        #region 保存序列化信息为xml文件
        public static void SaveToXml(Chart chart,string filename)
        {
            chart.Serializer.Content = SerializationContents.Default;
            chart.Serializer.Save(filename);
            
            
        }

        public static void LoadFromXml(Chart chart, string filename)
        {
            chart.Serializer.Load(filename);
        }
        //从xml文件中加载序列化信息
        #endregion
        #region 保存chart到数据库
        public static byte[] SaveChart2DB(Chart chart)//直接保存chart图为字节流存入数据库
        {
            chart.Serializer.Content = SerializationContents.Default;
            MemoryStream stream = new MemoryStream();
            byte[] by = new byte[stream.Length];
            BinaryReader bf=new BinaryReader (stream);
            chart.Serializer.Save(stream);
            StreamWriter sw = new StreamWriter(stream, Encoding.UTF8);
            by = bf.ReadBytes(Convert .ToInt32 (stream .Length ));
            sw.Dispose();
            stream.Close();
            sw.Close();
            return by;
        }

        public static void LoadChartFromDB(Chart chart,byte[]b) 
        {
            UnicodeEncoding unicode = new UnicodeEncoding();
            chart.Serializer.Content = SerializationContents.Default;
            MemoryStream ms = new MemoryStream(b);
            chart.Serializer.Load(ms);
            ms.Close();
        }
        #endregion
        #region 特殊图复制粘贴
        public static string serialpath = Application.StartupPath + @"\serialize.dat";
        
        public static void SaveForm(FormState formclass)//特殊图复制专用
        {
            FileStream fs = new FileStream(serialpath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, formclass);
            fs.Flush();
            fs.Close();
        }
        public static FormState LoadForm()//特殊图粘贴专用
        {
            if (File.Exists(serialpath))  //如序列化文件存在则读取
            {
                try
                {
                    FileStream fs = new FileStream(serialpath, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    FormState p = bf.Deserialize(fs) as FormState;
                    fs.Close();
                    return p;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
        #endregion 
        #region xml版保存/打开特殊图模板
        public static void SaveForm(FormState formclass,String filepath)//特殊图保存模版专用
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            switch (formclass .FrmName )
            {
                case"主图":
                case "油气水":
                case "C_M1":
                case "C_M2":
                    formclass.Serialize(ms, bf);
                    break;
                case "粒度概率累计曲线":
                case "测井曲线":
                    formclass.Serialize2(ms, bf);
                    break;
                case "萨胡成因判别函数":
                    formclass.Serialize1(ms,bf);
                    break;
                default:
                    break;
            }
            FileStream fs = new FileStream(filepath, FileMode.Create);
            bf.Serialize(fs, ms.ToArray());
            fs.Flush();
            fs.Close();
            ms.Close();
        }

        public static FormState LoadForm(string filepath)//特殊图模版从数据库打开专用
        {
            try
            {
                FormState formState = new FormState();
                FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryFormatter bf = new BinaryFormatter();
                byte[] Byte = bf.Deserialize(fs) as byte[];
                MemoryStream ms = new MemoryStream(Byte);
                BinaryFormatter bf1 = new BinaryFormatter();
                FormState p = formState.Deserialize(ms, bf1);
                fs.Close();
                ms.Close();
                return p;
            }
            catch (Exception ee)
            {
                MessageBox.Show("chucoco");
                return null;
            }
        }
        #endregion 

        public static byte[] SaveSpecial2DB(FormState fs) //特殊图保存到数据库专用（直接版），不用xml缓存文件
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            switch (fs.FrmName)
            {
                case "主图":
                case "油气水":
                case "C_M1":
                case "C_M2":
                    fs.Serialize(ms, bf);
                    break;
                case "粒度概率累计曲线":
                case "测井曲线":
                    fs.Serialize2(ms, bf);
                    break;
                case "萨胡成因判别函数":
                    fs.Serialize1(ms, bf);
                    break;
                default:
                    break;
            }
            byte[] Nb = ms.GetBuffer();
            ms.Dispose();
            ms.Close();
            return Nb;
        }

        public static FormState LoadSpecialFromDB(byte[] b) //特殊图从数据库打开专用（直接版），不用xml缓存文件
        {
            FormState formState = new FormState();
            MemoryStream ms = new MemoryStream(b);
            BinaryFormatter bf1 = new BinaryFormatter();
            FormState p = formState.Deserialize(ms, bf1);
            ms.Close();
            return p;
        }

        public static void Save2Local(byte []by,string filepath) 
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(filepath ,FileMode .Create);
            bf.Serialize(fs,by);
            fs.Flush();
            fs.Close();
        }

        public static byte[] LoadFromLocal(string filepath) 
        {
            FileStream fs = new FileStream(filepath ,FileMode.Open );
            BinaryFormatter bf = new BinaryFormatter();
            byte[] by = bf.Deserialize(fs) as byte[];
            return by;
        }
    }      
}
