namespace GeoDemo
{
    partial class Data_WellLog
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
            this.button_reset = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox_Bind = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Value3 = new System.Windows.Forms.ComboBox();
            this.comboBox_Value2 = new System.Windows.Forms.ComboBox();
            this.comboBox_Value1 = new System.Windows.Forms.ComboBox();
            this.button_Bind = new System.Windows.Forms.Button();
            this.button_Draw = new System.Windows.Forms.Button();
            this.button_Select = new System.Windows.Forms.Button();
            this.DataPath = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox_Bind.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(837, 502);
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
            this.tabControl_Data.Location = new System.Drawing.Point(0, 18);
            this.tabControl_Data.Name = "tabControl_Data";
            this.tabControl_Data.SelectedIndex = 0;
            this.tabControl_Data.Size = new System.Drawing.Size(505, 467);
            this.tabControl_Data.TabIndex = 0;
            this.tabControl_Data.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabControl_Data_MouseUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.button_reset);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox_Bind);
            this.groupBox2.Controls.Add(this.button_Bind);
            this.groupBox2.Controls.Add(this.button_Draw);
            this.groupBox2.Controls.Add(this.button_Select);
            this.groupBox2.Controls.Add(this.DataPath);
            this.groupBox2.Location = new System.Drawing.Point(504, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(327, 482);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据加载选项";
            // 
            // button_reset
            // 
            this.button_reset.Location = new System.Drawing.Point(31, 230);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(75, 23);
            this.button_reset.TabIndex = 6;
            this.button_reset.Text = "数据重置";
            this.button_reset.UseVisualStyleBackColor = true;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(8, 259);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(319, 191);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "图形属性控制";
            // 
            // groupBox_Bind
            // 
            this.groupBox_Bind.Controls.Add(this.label3);
            this.groupBox_Bind.Controls.Add(this.label2);
            this.groupBox_Bind.Controls.Add(this.label1);
            this.groupBox_Bind.Controls.Add(this.comboBox_Value3);
            this.groupBox_Bind.Controls.Add(this.comboBox_Value2);
            this.groupBox_Bind.Controls.Add(this.comboBox_Value1);
            this.groupBox_Bind.Location = new System.Drawing.Point(8, 49);
            this.groupBox_Bind.Name = "groupBox_Bind";
            this.groupBox_Bind.Size = new System.Drawing.Size(313, 175);
            this.groupBox_Bind.TabIndex = 4;
            this.groupBox_Bind.TabStop = false;
            this.groupBox_Bind.Text = "数据绑定";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Y轴";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "X轴";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "深度";
            // 
            // comboBox_Value3
            // 
            this.comboBox_Value3.FormattingEnabled = true;
            this.comboBox_Value3.Location = new System.Drawing.Point(165, 104);
            this.comboBox_Value3.Name = "comboBox_Value3";
            this.comboBox_Value3.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Value3.TabIndex = 3;
            // 
            // comboBox_Value2
            // 
            this.comboBox_Value2.FormattingEnabled = true;
            this.comboBox_Value2.Location = new System.Drawing.Point(165, 61);
            this.comboBox_Value2.Name = "comboBox_Value2";
            this.comboBox_Value2.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Value2.TabIndex = 2;
            // 
            // comboBox_Value1
            // 
            this.comboBox_Value1.FormattingEnabled = true;
            this.comboBox_Value1.Location = new System.Drawing.Point(165, 20);
            this.comboBox_Value1.Name = "comboBox_Value1";
            this.comboBox_Value1.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Value1.TabIndex = 1;
            this.comboBox_Value1.SelectedIndexChanged += new System.EventHandler(this.comboBox_Value1_SelectedIndexChanged);
            // 
            // button_Bind
            // 
            this.button_Bind.Location = new System.Drawing.Point(219, 230);
            this.button_Bind.Name = "button_Bind";
            this.button_Bind.Size = new System.Drawing.Size(75, 23);
            this.button_Bind.TabIndex = 3;
            this.button_Bind.Text = "数据绑定";
            this.button_Bind.UseVisualStyleBackColor = true;
            this.button_Bind.Click += new System.EventHandler(this.button_Bind_Click);
            // 
            // button_Draw
            // 
            this.button_Draw.Location = new System.Drawing.Point(219, 453);
            this.button_Draw.Name = "button_Draw";
            this.button_Draw.Size = new System.Drawing.Size(75, 23);
            this.button_Draw.TabIndex = 2;
            this.button_Draw.Text = "绘制图形";
            this.button_Draw.UseVisualStyleBackColor = true;
            this.button_Draw.Click += new System.EventHandler(this.button_Draw_Click);
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
            this.DataPath.Location = new System.Drawing.Point(7, 21);
            this.DataPath.Name = "DataPath";
            this.DataPath.Size = new System.Drawing.Size(205, 21);
            this.DataPath.TabIndex = 0;
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
            // Data_WellLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(837, 502);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Data_WellLog";
            this.Text = "测井数据读取";
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox_Bind.ResumeLayout(false);
            this.groupBox_Bind.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_Select;
        private System.Windows.Forms.TextBox DataPath;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_Bind;
        private System.Windows.Forms.Button button_Draw;
        private System.Windows.Forms.TabControl tabControl_Data;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox_Bind;
        private System.Windows.Forms.ComboBox comboBox_Value1;
        private System.Windows.Forms.ComboBox comboBox_Value2;
        private System.Windows.Forms.ComboBox comboBox_Value3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_reset;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
    }
}