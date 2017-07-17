using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeoDemo
{
    public partial class Way_DataLoad : Form
    {
        public Way_DataLoad()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReadDataFromDataBase rdfdb = new ReadDataFromDataBase();
            rdfdb.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data_WellLog cjsj = new Data_WellLog();
            cjsj.ShowDialog();
            this.Close();
         }
    }
}
