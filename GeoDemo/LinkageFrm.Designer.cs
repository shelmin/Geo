namespace GeoDemo
{
    partial class LinkageFrm
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加序列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除所选列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除所选行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl_Data = new System.Windows.Forms.TabControl();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl_Data.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.粘贴ToolStripMenuItem,
            this.添加序列ToolStripMenuItem,
            this.删除所选列ToolStripMenuItem,
            this.删除所选行ToolStripMenuItem,
            this.插入数据ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 114);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            // 
            // 添加序列ToolStripMenuItem
            // 
            this.添加序列ToolStripMenuItem.Name = "添加序列ToolStripMenuItem";
            this.添加序列ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.添加序列ToolStripMenuItem.Text = "添加序列";
            // 
            // 删除所选列ToolStripMenuItem
            // 
            this.删除所选列ToolStripMenuItem.Name = "删除所选列ToolStripMenuItem";
            this.删除所选列ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.删除所选列ToolStripMenuItem.Text = "删除所选列";
            // 
            // 删除所选行ToolStripMenuItem
            // 
            this.删除所选行ToolStripMenuItem.Name = "删除所选行ToolStripMenuItem";
            this.删除所选行ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.删除所选行ToolStripMenuItem.Text = "删除行";
            // 
            // 插入数据ToolStripMenuItem
            // 
            this.插入数据ToolStripMenuItem.Name = "插入数据ToolStripMenuItem";
            this.插入数据ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.插入数据ToolStripMenuItem.Text = "插入数据";
            // 
            // tabPage1
            // 
            this.tabPage1.ContextMenuStrip = this.contextMenuStrip1;
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(395, 292);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(395, 292);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            // 
            // tabControl_Data
            // 
            this.tabControl_Data.Controls.Add(this.tabPage1);
            this.tabControl_Data.Location = new System.Drawing.Point(3, 3);
            this.tabControl_Data.Name = "tabControl_Data";
            this.tabControl_Data.SelectedIndex = 0;
            this.tabControl_Data.Size = new System.Drawing.Size(403, 318);
            this.tabControl_Data.TabIndex = 0;
            // 
            // LinkageFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 324);
            this.Controls.Add(this.tabControl_Data);
            this.Name = "LinkageFrm";
            this.Text = " ";
            this.Load += new System.EventHandler(this.LinkageFrm_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LinkageFrm_MouseClick);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl_Data.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加序列ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除所选列ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除所选行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入数据ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabControl tabControl_Data;
    }
}