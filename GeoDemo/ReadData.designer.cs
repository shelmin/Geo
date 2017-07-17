namespace GeoDemo
{
    partial class ReadData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl_Data = new System.Windows.Forms.TabControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox_Bind = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBox_Value1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_Value2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Bind = new System.Windows.Forms.Button();
            this.button_Select = new System.Windows.Forms.Button();
            this.DataPath = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox_Bind.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(837, 499);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControl_Data);
            this.groupBox3.Location = new System.Drawing.Point(0, 17);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(505, 482);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "加载数据预览";
            // 
            // tabControl_Data
            // 
            this.tabControl_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Data.Location = new System.Drawing.Point(3, 17);
            this.tabControl_Data.Name = "tabControl_Data";
            this.tabControl_Data.SelectedIndex = 0;
            this.tabControl_Data.Size = new System.Drawing.Size(499, 462);
            this.tabControl_Data.TabIndex = 1;
            this.tabControl_Data.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabControl_Data_MouseUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.groupBox_Bind);
            this.groupBox2.Controls.Add(this.button_Select);
            this.groupBox2.Controls.Add(this.DataPath);
            this.groupBox2.Location = new System.Drawing.Point(504, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(327, 479);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据加载选项";
            // 
            // groupBox_Bind
            // 
            this.groupBox_Bind.Controls.Add(this.comboBox1);
            this.groupBox_Bind.Controls.Add(this.groupBox4);
            this.groupBox_Bind.Controls.Add(this.button_Bind);
            this.groupBox_Bind.Location = new System.Drawing.Point(8, 49);
            this.groupBox_Bind.Name = "groupBox_Bind";
            this.groupBox_Bind.Size = new System.Drawing.Size(313, 175);
            this.groupBox_Bind.TabIndex = 4;
            this.groupBox_Bind.TabStop = false;
            this.groupBox_Bind.Text = "数据绑定";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "普通图",
            "直方图"});
            this.comboBox1.Location = new System.Drawing.Point(58, 139);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 20);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBox_Value1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.comboBox_Value2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(58, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(207, 100);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "图形1";
            // 
            // comboBox_Value1
            // 
            this.comboBox_Value1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Value1.FormattingEnabled = true;
            this.comboBox_Value1.Location = new System.Drawing.Point(63, 20);
            this.comboBox_Value1.Name = "comboBox_Value1";
            this.comboBox_Value1.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Value1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y轴：";
            // 
            // comboBox_Value2
            // 
            this.comboBox_Value2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Value2.FormattingEnabled = true;
            this.comboBox_Value2.Location = new System.Drawing.Point(63, 61);
            this.comboBox_Value2.Name = "comboBox_Value2";
            this.comboBox_Value2.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Value2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "X轴：";
            // 
            // button_Bind
            // 
            this.button_Bind.Enabled = false;
            this.button_Bind.Location = new System.Drawing.Point(190, 136);
            this.button_Bind.Name = "button_Bind";
            this.button_Bind.Size = new System.Drawing.Size(75, 23);
            this.button_Bind.TabIndex = 3;
            this.button_Bind.Text = "数据绑定";
            this.button_Bind.UseVisualStyleBackColor = true;
            this.button_Bind.Click += new System.EventHandler(this.button_Bind_Click);
            // 
            // button_Select
            // 
            this.button_Select.Location = new System.Drawing.Point(219, 18);
            this.button_Select.Name = "button_Select";
            this.button_Select.Size = new System.Drawing.Size(75, 23);
            this.button_Select.TabIndex = 1;
            this.button_Select.Text = "选择文件";
            this.button_Select.UseVisualStyleBackColor = true;
            this.button_Select.Click += new System.EventHandler(this.button_Select_Click);
            // 
            // DataPath
            // 
            this.DataPath.Enabled = false;
            this.DataPath.Location = new System.Drawing.Point(7, 21);
            this.DataPath.Name = "DataPath";
            this.DataPath.Size = new System.Drawing.Size(205, 21);
            this.DataPath.TabIndex = 0;
            this.DataPath.TextChanged += new System.EventHandler(this.DataPath_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.粘贴ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // ReadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(837, 499);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ReadData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据读取";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.读取数据_FormClosed);
            this.Load += new System.EventHandler(this.读取数据_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox_Bind.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabControl tabControl_Data;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox_Bind;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.ComboBox comboBox_Value1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox comboBox_Value2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Bind;
        private System.Windows.Forms.Button button_Select;
        private System.Windows.Forms.TextBox DataPath;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}