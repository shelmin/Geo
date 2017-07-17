namespace GeoDemo
{
	partial class Form_DataSet
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
            this.DataLoad = new System.Windows.Forms.GroupBox();
            this.button_DataLoad = new System.Windows.Forms.Button();
            this.textBox_DataLoad = new System.Windows.Forms.TextBox();
            this.groupBox_AxisSet = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox_Formula = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_K = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancle = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.DataLoad.SuspendLayout();
            this.groupBox_AxisSet.SuspendLayout();
            this.groupBox_Formula.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataLoad
            // 
            this.DataLoad.Controls.Add(this.button_DataLoad);
            this.DataLoad.Controls.Add(this.textBox_DataLoad);
            this.DataLoad.Location = new System.Drawing.Point(39, 28);
            this.DataLoad.Name = "DataLoad";
            this.DataLoad.Size = new System.Drawing.Size(383, 55);
            this.DataLoad.TabIndex = 0;
            this.DataLoad.TabStop = false;
            this.DataLoad.Text = "数据加载";
            // 
            // button_DataLoad
            // 
            this.button_DataLoad.Location = new System.Drawing.Point(290, 19);
            this.button_DataLoad.Name = "button_DataLoad";
            this.button_DataLoad.Size = new System.Drawing.Size(75, 23);
            this.button_DataLoad.TabIndex = 1;
            this.button_DataLoad.Text = "选择文件";
            this.button_DataLoad.UseVisualStyleBackColor = true;
            this.button_DataLoad.Click += new System.EventHandler(this.button_DataLoad_Click);
            // 
            // textBox_DataLoad
            // 
            this.textBox_DataLoad.Location = new System.Drawing.Point(7, 21);
            this.textBox_DataLoad.Name = "textBox_DataLoad";
            this.textBox_DataLoad.Size = new System.Drawing.Size(268, 21);
            this.textBox_DataLoad.TabIndex = 0;
            // 
            // groupBox_AxisSet
            // 
            this.groupBox_AxisSet.Controls.Add(this.label2);
            this.groupBox_AxisSet.Controls.Add(this.label1);
            this.groupBox_AxisSet.Controls.Add(this.comboBox2);
            this.groupBox_AxisSet.Controls.Add(this.comboBox1);
            this.groupBox_AxisSet.Location = new System.Drawing.Point(39, 102);
            this.groupBox_AxisSet.Name = "groupBox_AxisSet";
            this.groupBox_AxisSet.Size = new System.Drawing.Size(383, 49);
            this.groupBox_AxisSet.TabIndex = 1;
            this.groupBox_AxisSet.TabStop = false;
            this.groupBox_AxisSet.Text = "坐标轴数据";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "纵坐标:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "横坐标:";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "对数",
            "Rt^(-1/m)"});
            this.comboBox2.Location = new System.Drawing.Point(244, 20);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "孔隙度",
            "密度"});
            this.comboBox1.Location = new System.Drawing.Point(50, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox_Formula
            // 
            this.groupBox_Formula.Controls.Add(this.textBox1);
            this.groupBox_Formula.Controls.Add(this.label5);
            this.groupBox_Formula.Controls.Add(this.textBox_K);
            this.groupBox_Formula.Controls.Add(this.label4);
            this.groupBox_Formula.Controls.Add(this.label3);
            this.groupBox_Formula.Location = new System.Drawing.Point(39, 194);
            this.groupBox_Formula.Name = "groupBox_Formula";
            this.groupBox_Formula.Size = new System.Drawing.Size(383, 48);
            this.groupBox_Formula.TabIndex = 2;
            this.groupBox_Formula.TabStop = false;
            this.groupBox_Formula.Text = "计算公式";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(298, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(67, 21);
            this.textBox1.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(275, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "C=";
            // 
            // textBox_K
            // 
            this.textBox_K.Location = new System.Drawing.Point(193, 18);
            this.textBox_K.Name = "textBox_K";
            this.textBox_K.Size = new System.Drawing.Size(73, 21);
            this.textBox_K.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(170, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "K=";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "K * den + C:";
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(258, 276);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(73, 23);
            this.button_OK.TabIndex = 3;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancle
            // 
            this.button_Cancle.Location = new System.Drawing.Point(347, 276);
            this.button_Cancle.Name = "button_Cancle";
            this.button_Cancle.Size = new System.Drawing.Size(75, 23);
            this.button_Cancle.TabIndex = 4;
            this.button_Cancle.Text = "取消";
            this.button_Cancle.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(457, 351);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.DataLoad);
            this.tabPage1.Controls.Add(this.button_Cancle);
            this.tabPage1.Controls.Add(this.groupBox_AxisSet);
            this.tabPage1.Controls.Add(this.button_OK);
            this.tabPage1.Controls.Add(this.groupBox_Formula);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(449, 325);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(449, 325);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "参数";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(353, 287);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 29);
            this.button2.TabIndex = 2;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(250, 287);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "选定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Location = new System.Drawing.Point(36, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 232);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "岩石特性";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(76, 199);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(91, 21);
            this.textBox6.TabIndex = 9;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(235, 121);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(91, 21);
            this.textBox5.TabIndex = 8;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(76, 121);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(91, 21);
            this.textBox4.TabIndex = 7;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(235, 48);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(91, 21);
            this.textBox3.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(29, 208);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "n=";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(200, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "m=";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "Rw=";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(200, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "b=";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "a=";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(76, 48);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(91, 21);
            this.textBox2.TabIndex = 0;
            // 
            // Form_DataSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 351);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form_DataSet";
            this.Text = "数据设置";
            this.Load += new System.EventHandler(this.DataSet_Load);
            this.DataLoad.ResumeLayout(false);
            this.DataLoad.PerformLayout();
            this.groupBox_AxisSet.ResumeLayout(false);
            this.groupBox_AxisSet.PerformLayout();
            this.groupBox_Formula.ResumeLayout(false);
            this.groupBox_Formula.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox DataLoad;
		private System.Windows.Forms.Button button_DataLoad;
		private System.Windows.Forms.TextBox textBox_DataLoad;
		private System.Windows.Forms.GroupBox groupBox_AxisSet;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.GroupBox groupBox_Formula;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox_K;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button_OK;
		private System.Windows.Forms.Button button_Cancle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
	}
}