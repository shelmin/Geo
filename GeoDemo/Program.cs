using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace GeoDemo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Assembly.Load("NPOI");
            Assembly.Load("NPOI.OOXML");
            Assembly.Load("NPOI.OpenXml4Net");
            Assembly.Load("NPOI.OpenXmlFormats");
            Assembly.Load("DevComponents.DotNetBar2");
            Assembly.Load("ICSharpCode.SharpZipLib");
            //Assembly.Load("Microsoft.CSharp");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFrame());
        }
    }
}
