namespace GeoDemo
{
    partial class ReadData2
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
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox_Bind = new System.Windows.Forms.GroupBox();
            this.图形2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_Value3 = new System.Windows.Forms.ComboBox();
            this.button_Bind = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Value2 = new System.Windows.Forms.ComboBox();
            this.comboBox_Value1 = new System.Windows.Forms.ComboBox();
            this.button_Select = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl_Data = new System.Windows.Forms.TabControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DataPath = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox_Bind.SuspendLayout();
            this.图形2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y轴：";
            // 
            // groupBox_Bind
            // 
            this.groupBox_Bind.Controls.Add(this.图形2);
            this.groupBox_Bind.Controls.Add(this.button_Bind);
            this.groupBox_Bind.Controls.Add(this.groupBox4);
            this.groupBox_Bind.Location = new System.Drawing.Point(8, 49);
            this.groupBox_Bind.Name = "groupBox_Bind";
            this.groupBox_Bind.Size = new System.Drawing.Size(313, 254);
            this.groupBox_Bind.TabIndex = 4;
            this.groupBox_Bind.TabStop = false;
            this.groupBox_Bind.Text = "数据绑定";
            // 
            // 图形2
            // 
            this.图形2.Controls.Add(this.textBox1);
            this.图形2.Controls.Add(this.label4);
            this.图形2.Controls.Add(this.label3);
            this.图形2.Controls.Add(this.comboBox_Value3);
            this.图形2.Location = new System.Drawing.Point(23, 148);
            this.图形2.Name = "图形2";
            this.图形2.Size = new System.Drawing.Size(200, 100);
            this.图形2.TabIndex = 8;
            this.图形2.TabStop = false;
            this.图形2.Text = "图形2";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(60, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 21);
            this.textBox1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "X轴：";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Y轴：";
            // 
            // comboBox_Value3
            // 
            this.comboBox_Value3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Value3.FormattingEnabled = true;
            this.comboBox_Value3.Location = new System.Drawing.Point(60, 66);
            this.comboBox_Value3.Name = "comboBox_Value3";
            this.comboBox_Value3.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Value3.TabIndex = 5;
            // 
            // button_Bind
            // 
            this.button_Bind.Enabled = false;
            this.button_Bind.Location = new System.Drawing.Point(234, 135);
            this.button_Bind.Name = "button_Bind";
            this.button_Bind.Size = new System.Drawing.Size(75, 23);
            this.button_Bind.TabIndex = 3;
            this.button_Bind.Text = "数据绑定";
            this.button_Bind.UseVisualStyleBackColor = true;
            this.button_Bind.Click += new System.EventHandler(this.button_Bind_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.comboBox_Value2);
            this.groupBox4.Controls.Add(this.comboBox_Value1);
            this.groupBox4.Location = new System.Drawing.Point(23, 31);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 100);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "图形1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "X轴：";
            // 
            // comboBox_Value2
            // 
            this.comboBox_Value2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Value2.FormattingEnabled = true;
            this.comboBox_Value2.Location = new System.Drawing.Point(62, 67);
            this.comboBox_Value2.Name = "comboBox_Value2";
            this.comboBox_Value2.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Value2.TabIndex = 2;
            // 
            // comboBox_Value1
            // 
            this.comboBox_Value1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Value1.FormattingEnabled = true;
            this.comboBox_Value1.Location = new System.Drawing.Point(62, 30);
            this.comboBox_Value1.Name = "comboBox_Value1";
            this.comboBox_Value1.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Value1.TabIndex = 1;
            this.comboBox_Value1.SelectedIndexChanged += new System.EventHandler(this.comboBox_Value1_SelectedIndexChanged);
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
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(827, 489);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
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
            this.groupBox2.Size = new System.Drawing.Size(317, 469);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据加载选项";
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
            // ReadData2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 489);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ReadData2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "读取数据";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.读取数据2_FormClosed);
            this.Load += new System.EventHandler(this.读取数据2_Load);
            this.groupBox_Bind.ResumeLayout(false);
            this.图形2.ResumeLayout(false);
            this.图形2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox_Bind;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox comboBox_Value3;
        private System.Windows.Forms.Button button_Bind;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox comboBox_Value2;
        public System.Windows.Forms.ComboBox comboBox_Value1;
        private System.Windows.Forms.Button button_Select;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabControl tabControl_Data;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox DataPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox 图形2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
    }
}