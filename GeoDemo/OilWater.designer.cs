namespace GeoDemo
{
	partial class OilWater
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.panelMark = new System.Windows.Forms.Panel();
            this.button_paint = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Fill = new System.Windows.Forms.Button();
            this.groupBox_Paint = new System.Windows.Forms.GroupBox();
            this.panel_XY = new System.Windows.Forms.Panel();
            this.button_Set = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox_Paint.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMark
            // 
            this.panelMark.Location = new System.Drawing.Point(12, 368);
            this.panelMark.Name = "panelMark";
            this.panelMark.Size = new System.Drawing.Size(110, 112);
            this.panelMark.TabIndex = 0;
            this.panelMark.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMark_Paint);
            // 
            // button_paint
            // 
            this.button_paint.Enabled = false;
            this.button_paint.Location = new System.Drawing.Point(12, 266);
            this.button_paint.Name = "button_paint";
            this.button_paint.Size = new System.Drawing.Size(75, 23);
            this.button_paint.TabIndex = 1;
            this.button_paint.Text = "绘制";
            this.button_paint.UseVisualStyleBackColor = true;
            this.button_paint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_paint_MouseClick);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Enabled = false;
            this.button_Cancel.Location = new System.Drawing.Point(12, 223);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 2;
            this.button_Cancel.Text = "重绘";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Fill
            // 
            this.button_Fill.Enabled = false;
            this.button_Fill.Location = new System.Drawing.Point(12, 185);
            this.button_Fill.Name = "button_Fill";
            this.button_Fill.Size = new System.Drawing.Size(75, 23);
            this.button_Fill.TabIndex = 3;
            this.button_Fill.Text = "填充";
            this.button_Fill.UseVisualStyleBackColor = true;
            this.button_Fill.Click += new System.EventHandler(this.button_Fill_Click);
            // 
            // groupBox_Paint
            // 
            this.groupBox_Paint.BackColor = System.Drawing.Color.White;
            this.groupBox_Paint.Controls.Add(this.panel_XY);
            this.groupBox_Paint.Location = new System.Drawing.Point(128, 39);
            this.groupBox_Paint.Name = "groupBox_Paint";
            this.groupBox_Paint.Size = new System.Drawing.Size(582, 447);
            this.groupBox_Paint.TabIndex = 4;
            this.groupBox_Paint.TabStop = false;
            this.groupBox_Paint.Text = "绘图区";
            // 
            // panel_XY
            // 
            this.panel_XY.Location = new System.Drawing.Point(6, 13);
            this.panel_XY.Name = "panel_XY";
            this.panel_XY.Size = new System.Drawing.Size(570, 428);
            this.panel_XY.TabIndex = 0;
            this.panel_XY.Click += new System.EventHandler(this.panel_XY_Click);
            this.panel_XY.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_XY_Paint);
            this.panel_XY.DoubleClick += new System.EventHandler(this.panel_XY_DoubleClick);
            // 
            // button_Set
            // 
            this.button_Set.Location = new System.Drawing.Point(12, 147);
            this.button_Set.Name = "button_Set";
            this.button_Set.Size = new System.Drawing.Size(75, 23);
            this.button_Set.TabIndex = 5;
            this.button_Set.Text = "设置";
            this.button_Set.UseVisualStyleBackColor = true;
            this.button_Set.Click += new System.EventHandler(this.button_Set_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(12, 308);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "投点";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(346, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // OilWater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 498);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_Set);
            this.Controls.Add(this.groupBox_Paint);
            this.Controls.Add(this.button_Fill);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_paint);
            this.Controls.Add(this.panelMark);
            this.Name = "OilWater";
            this.Text = "油气水";
            this.Load += new System.EventHandler(this.油气水_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OilWater_MouseClick);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OilWater_MouseUp);
            this.groupBox_Paint.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panelMark;
		private System.Windows.Forms.Button button_paint;
		private System.Windows.Forms.Button button_Cancel;
		private System.Windows.Forms.Button button_Fill;
		private System.Windows.Forms.GroupBox groupBox_Paint;
		private System.Windows.Forms.Panel panel_XY;
		private System.Windows.Forms.Button button_Set;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
	}
}

