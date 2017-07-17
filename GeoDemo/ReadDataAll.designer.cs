namespace GeoDemo
{
    partial class ReadDataAll
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl_Data = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_Select = new System.Windows.Forms.Button();
            this.DataPath = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加序列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除此列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除此行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl_Data.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(837, 499);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBox1);
            this.groupBox4.Location = new System.Drawing.Point(353, 436);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(150, 51);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "图形类别";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "普通图",
            "直方图"});
            this.comboBox1.Location = new System.Drawing.Point(30, 21);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 20);
            this.comboBox1.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(747, 455);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(657, 455);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "确定";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(558, 454);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "应用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControl_Data);
            this.groupBox3.Location = new System.Drawing.Point(0, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(825, 404);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "加载数据预览";
            // 
            // tabControl_Data
            // 
            this.tabControl_Data.Controls.Add(this.tabPage1);
            this.tabControl_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Data.Location = new System.Drawing.Point(3, 17);
            this.tabControl_Data.Name = "tabControl_Data";
            this.tabControl_Data.SelectedIndex = 0;
            this.tabControl_Data.Size = new System.Drawing.Size(819, 384);
            this.tabControl_Data.TabIndex = 1;
            this.tabControl_Data.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabControl_Data_MouseUp);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(811, 358);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.button_Select);
            this.groupBox2.Controls.Add(this.DataPath);
            this.groupBox2.Location = new System.Drawing.Point(12, 436);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 51);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据加载选项";
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
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.粘贴ToolStripMenuItem,
            this.添加序列ToolStripMenuItem,
            this.删除此列ToolStripMenuItem,
            this.删除此行ToolStripMenuItem,
            this.插入数据ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 114);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // 添加序列ToolStripMenuItem
            // 
            this.添加序列ToolStripMenuItem.Name = "添加序列ToolStripMenuItem";
            this.添加序列ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.添加序列ToolStripMenuItem.Text = "添加序列";
            this.添加序列ToolStripMenuItem.Click += new System.EventHandler(this.添加序列ToolStripMenuItem_Click);
            // 
            // 删除此列ToolStripMenuItem
            // 
            this.删除此列ToolStripMenuItem.Name = "删除此列ToolStripMenuItem";
            this.删除此列ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.删除此列ToolStripMenuItem.Text = "删除所选列";
            this.删除此列ToolStripMenuItem.Click += new System.EventHandler(this.删除此列ToolStripMenuItem_Click);
            // 
            // 删除此行ToolStripMenuItem
            // 
            this.删除此行ToolStripMenuItem.Name = "删除此行ToolStripMenuItem";
            this.删除此行ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.删除此行ToolStripMenuItem.Text = "删除行";
            this.删除此行ToolStripMenuItem.Click += new System.EventHandler(this.删除此行ToolStripMenuItem_Click);
            // 
            // 插入数据ToolStripMenuItem
            // 
            this.插入数据ToolStripMenuItem.Name = "插入数据ToolStripMenuItem";
            this.插入数据ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.插入数据ToolStripMenuItem.Text = "插入数据";
            this.插入数据ToolStripMenuItem.Click += new System.EventHandler(this.插入数据ToolStripMenuItem_Click);
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabControl_Data.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.Button button_Select;
        private System.Windows.Forms.TextBox DataPath;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ToolStripMenuItem 添加序列ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除此列ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除此行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入数据ToolStripMenuItem;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnCancel;
    }
}