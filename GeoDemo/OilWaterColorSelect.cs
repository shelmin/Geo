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
	public delegate void Refresh(SolidBrush[] b,string[] str);
	public partial class OilWaterColorSelect : Form
	{
		public event Refresh refreshForm;
		private SolidBrush[] brushes;
		private string[] str;
		public OilWaterColorSelect()
		{
			InitializeComponent();
			brushes = new SolidBrush[5];
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ColorDialog d = new ColorDialog();
			if (d.ShowDialog() == DialogResult.OK)
			{
				this.panel_Region1.BackColor = d.Color;
				brushes[0] = new SolidBrush(d.Color);
			}
		}

		private void button_Region2_Click(object sender, EventArgs e)
		{
			ColorDialog d = new ColorDialog();
			if (d.ShowDialog() == DialogResult.OK)
			{
				this.panel_Region2.BackColor = d.Color;
				brushes[1] = new SolidBrush(d.Color);
			}
		}

		private void button_Region3_Click(object sender, EventArgs e)
		{
			ColorDialog d = new ColorDialog();
			if (d.ShowDialog() == DialogResult.OK)
			{
				this.panel_Region3.BackColor = d.Color;
				brushes[2] = new SolidBrush(d.Color);
			}
		}

		private void button_Region4_Click(object sender, EventArgs e)
		{
			ColorDialog d = new ColorDialog();
			if (d.ShowDialog() == DialogResult.OK)
			{
				this.panel_Region4.BackColor = d.Color;
				brushes[3] = new SolidBrush(d.Color);
			}
		}

		private void button_Cancle_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button_Ok_Click(object sender, EventArgs e)
		{
			

			if (refreshForm != null)
			{
				refreshForm(brushes,str);
			}
			this.Close();
		}

        private void button1_Click_1(object sender, EventArgs e)
        {
            ColorDialog d = new ColorDialog();
            if (d.ShowDialog() == DialogResult.OK)
            {
                this.panel1.BackColor = d.Color;
                brushes[4] = new SolidBrush(d.Color);
            }
        }
		
	}
}
