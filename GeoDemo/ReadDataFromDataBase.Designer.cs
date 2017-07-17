namespace GeoDemo
{
    partial class ReadDataFromDataBase
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
            this.btnWell = new System.Windows.Forms.Button();
            this.btnXcurve = new System.Windows.Forms.Button();
            this.btnYcurve = new System.Windows.Forms.Button();
            WellPath = new System.Windows.Forms.TextBox();
            XcurveID = new System.Windows.Forms.TextBox();
            YcurveID = new System.Windows.Forms.TextBox();
            this.Sdepth = new System.Windows.Forms.TextBox();
            this.Edepth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnBind = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWell
            // 
            this.btnWell.Location = new System.Drawing.Point(30, 12);
            this.btnWell.Name = "btnWell";
            this.btnWell.Size = new System.Drawing.Size(75, 23);
            this.btnWell.TabIndex = 0;
            this.btnWell.Text = "参考井";
            this.btnWell.UseVisualStyleBackColor = true;
            this.btnWell.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnXcurve
            // 
            this.btnXcurve.Location = new System.Drawing.Point(30, 54);
            this.btnXcurve.Name = "btnXcurve";
            this.btnXcurve.Size = new System.Drawing.Size(75, 23);
            this.btnXcurve.TabIndex = 1;
            this.btnXcurve.Text = "X轴曲线";
            this.btnXcurve.UseVisualStyleBackColor = true;
            this.btnXcurve.Click += new System.EventHandler(this.button2_Click);
            this.btnXcurve.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnXcurve_MouseClick);
            // 
            // btnYcurve
            // 
            this.btnYcurve.Location = new System.Drawing.Point(30, 95);
            this.btnYcurve.Name = "btnYcurve";
            this.btnYcurve.Size = new System.Drawing.Size(75, 23);
            this.btnYcurve.TabIndex = 2;
            this.btnYcurve.Text = "Y轴曲线";
            this.btnYcurve.UseVisualStyleBackColor = true;
            this.btnYcurve.Click += new System.EventHandler(this.button3_Click);
            this.btnYcurve.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnYcurve_MouseClick);
            // 
            // WellPath
            // 
            WellPath.Location = new System.Drawing.Point(127, 14);
            WellPath.Name = "WellPath";
            WellPath.ReadOnly = true;
            WellPath.Size = new System.Drawing.Size(148, 21);
            WellPath.TabIndex = 5;
            WellPath.TextChanged += new System.EventHandler(this.WellPath_TextChanged);
            // 
            // XcurveID
            // 
            XcurveID.Location = new System.Drawing.Point(127, 56);
            XcurveID.Name = "XcurveID";
            XcurveID.ReadOnly = true;
            XcurveID.Size = new System.Drawing.Size(148, 21);
            XcurveID.TabIndex = 6;
            XcurveID.TextChanged += new System.EventHandler(this.XcurveID_TextChanged);
            // 
            // YcurveID
            // 
            YcurveID.Location = new System.Drawing.Point(127, 97);
            YcurveID.Name = "YcurveID";
            YcurveID.ReadOnly = true;
            YcurveID.Size = new System.Drawing.Size(148, 21);
            YcurveID.TabIndex = 7;
            YcurveID.TextChanged += new System.EventHandler(this.YcurveID_TextChanged);
            // 
            // Sdepth
            // 
            this.Sdepth.Location = new System.Drawing.Point(127, 140);
            this.Sdepth.Name = "Sdepth";
            this.Sdepth.Size = new System.Drawing.Size(76, 21);
            this.Sdepth.TabIndex = 8;
            this.Sdepth.TextChanged += new System.EventHandler(this.Sdepth_TextChanged);
            // 
            // Edepth
            // 
            this.Edepth.Location = new System.Drawing.Point(127, 174);
            this.Edepth.Name = "Edepth";
            this.Edepth.Size = new System.Drawing.Size(76, 21);
            this.Edepth.TabIndex = 9;
            this.Edepth.TextChanged += new System.EventHandler(this.Edepth_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "起始深度";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "结束深度";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 218);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(366, 218);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnBind
            // 
            this.btnBind.Enabled = false;
            this.btnBind.Location = new System.Drawing.Point(285, 218);
            this.btnBind.Name = "btnBind";
            this.btnBind.Size = new System.Drawing.Size(75, 23);
            this.btnBind.TabIndex = 14;
            this.btnBind.Text = "应用";
            this.btnBind.UseVisualStyleBackColor = true;
            this.btnBind.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // ReadDataFromDataBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 262);
            this.Controls.Add(this.btnBind);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Edepth);
            this.Controls.Add(this.Sdepth);
            this.Controls.Add(YcurveID);
            this.Controls.Add(XcurveID);
            this.Controls.Add(WellPath);
            this.Controls.Add(this.btnYcurve);
            this.Controls.Add(this.btnXcurve);
            this.Controls.Add(this.btnWell);
            this.Name = "ReadDataFromDataBase";
            this.Text = "从数据库中读取数据";
            this.Load += new System.EventHandler(this.ReadDataFromDataBase_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWell;
        private System.Windows.Forms.Button btnXcurve;
        private System.Windows.Forms.Button btnYcurve;
        private System.Windows.Forms.TextBox Sdepth;
        private System.Windows.Forms.TextBox Edepth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnBind;
        public static System.Windows.Forms.TextBox WellPath;
        public static System.Windows.Forms.TextBox XcurveID;
        public static System.Windows.Forms.TextBox YcurveID;

    }
}