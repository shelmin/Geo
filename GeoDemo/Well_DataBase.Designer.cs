namespace GeoDemo
{
    partial class Well_DataBase
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
            this.Wells = new System.Windows.Forms.GroupBox();
            this.treeWell = new System.Windows.Forms.TreeView();
            this.Curves = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Wells.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Wells
            // 
            this.Wells.Controls.Add(this.treeWell);
            this.Wells.Location = new System.Drawing.Point(3, 5);
            this.Wells.Name = "Wells";
            this.Wells.Size = new System.Drawing.Size(224, 370);
            this.Wells.TabIndex = 0;
            this.Wells.TabStop = false;
            this.Wells.Text = "数据库";
            this.Wells.Enter += new System.EventHandler(this.Wells_Enter);
            // 
            // treeWell
            // 
            this.treeWell.Location = new System.Drawing.Point(6, 20);
            this.treeWell.Name = "treeWell";
            this.treeWell.Size = new System.Drawing.Size(212, 344);
            this.treeWell.TabIndex = 0;
            this.treeWell.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeWell_AfterSelect);
            // 
            // Curves
            // 
            this.Curves.Location = new System.Drawing.Point(233, 5);
            this.Curves.Name = "Curves";
            this.Curves.Size = new System.Drawing.Size(283, 215);
            this.Curves.TabIndex = 1;
            this.Curves.TabStop = false;
            this.Curves.Text = "groupBox2";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtInfo);
            this.groupBox3.Location = new System.Drawing.Point(233, 226);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(439, 149);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // txtInfo
            // 
            this.txtInfo.Location = new System.Drawing.Point(6, 11);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(277, 132);
            this.txtInfo.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(342, 381);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(441, 381);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Well_DataBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 415);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.Curves);
            this.Controls.Add(this.Wells);
            this.Name = "Well_DataBase";
            this.Text = "从项目中读取井数据";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Well_DataBase_FormClosed);
            this.Load += new System.EventHandler(this.Well_DataBase_Load);
            this.Wells.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Wells;
        private System.Windows.Forms.GroupBox Curves;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TreeView treeWell;
        private System.Windows.Forms.TextBox txtInfo;
    }
}