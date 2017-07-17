using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GeoDemo
{
    class SaveChart
    {
        private static Color[] SColor;

        public static Color[] SColor1
        {
            get { return SaveChart.SColor; }
            set { SaveChart.SColor = value; }
        }
        private static GradientStyle[] SGradient;

        public static GradientStyle[] SGradient1
        {
            get { return SaveChart.SGradient; }
            set { SaveChart.SGradient = value; }
        }
        private static Color[] SPointColor;

        public static Color[] SPointColor1
        {
            get { return SaveChart.SPointColor; }
            set { SaveChart.SPointColor = value; }
        }

    }
}
