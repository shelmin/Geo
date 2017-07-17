using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plytmf.Net.Bottom;

namespace GeoDemo
{
    public partial class Well_DataBase : Form
    {
        public static string WellName;
        public Well_DataBase()
        {
            InitializeComponent();
        }
        public static Well well;
        private void Well_DataBase_Load(object sender, EventArgs e)
        {
            InitTree();
        }
        private void InitTree()
        {
            TreeNode oilFieldNode = new TreeNode(Ymhdo.Project.OilField.Name);
            foreach (Well well in Ymhdo.Project.Wells)
            {
                TreeNode wellNode = new TreeNode(well.Name);
                wellNode.Tag = well;
                //foreach (Curve curve in well.Curves)
                //{
                //    TreeNode curveNode = new TreeNode(curve.UniqueName);
                //    curveNode.Tag = curve;
                //    wellNode.Nodes.Add(curveNode);
                //}
                oilFieldNode.Nodes.Add(wellNode);
            }
            treeWell.Nodes.Add(oilFieldNode);
            treeWell.ExpandAll();
        }
        private void Wells_Enter(object sender, EventArgs e)
        {

        }

        private void GetAttribute() 
        {
            WellName = this.treeWell.SelectedNode.Text; 
            
        }
        private void treeWell_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.treeWell.SelectedNode == null)
            {
                return;
            }
            if (treeWell.SelectedNode.Tag is Well)
            {
                Well well = treeWell.SelectedNode.Tag as Well;
                string str = "X坐标：" + well.StaticInfo.XCooddinate.ToString() + "  Y坐标：" + well.StaticInfo.YCooddinate.ToString();
                double dep = (well.Sdep + well.Edep) * 0.8;
                double x, y, verDep;
                well.GetXYZ(dep, out x, out y, out verDep);
                string str1 = "Dep：" + "  X：" + x.ToString() + "  Y：" + y.ToString() + "  verDep：" + verDep.ToString();
                this.txtInfo.Text = str + "\r\n" + str1;
                //this.comboStratumData.SelectedIndex = 1;
                //RefreshStratumData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            well = treeWell.SelectedNode.Tag as Well;
            GetAttribute();
            ReadDataFromDataBase.WellPath.Text = WellName;
            this.Close(); 
        }

        private void Well_DataBase_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }
    }
}
