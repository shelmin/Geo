namespace GeoDemo
{
    partial class Way_DataLoad
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
            this.loadfromFile = new System.Windows.Forms.Button();
            this.loadfromDB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loadfromFile
            // 
            this.loadfromFile.Location = new System.Drawing.Point(85, 89);
            this.loadfromFile.Name = "loadfromFile";
            this.loadfromFile.Size = new System.Drawing.Size(115, 23);
            this.loadfromFile.TabIndex = 0;
            this.loadfromFile.Text = "从文件中加载数据";
            this.loadfromFile.UseVisualStyleBackColor = true;
            this.loadfromFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // loadfromDB
            // 
            this.loadfromDB.Location = new System.Drawing.Point(85, 154);
            this.loadfromDB.Name = "loadfromDB";
            this.loadfromDB.Size = new System.Drawing.Size(115, 23);
            this.loadfromDB.TabIndex = 1;
            this.loadfromDB.Text = "从数据库加载数据";
            this.loadfromDB.UseVisualStyleBackColor = true;
            this.loadfromDB.Click += new System.EventHandler(this.button2_Click);
            // 
            // Way_DataLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.loadfromDB);
            this.Controls.Add(this.loadfromFile);
            this.Name = "Way_DataLoad";
            this.Text = "数据加载方式";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button loadfromFile;
        private System.Windows.Forms.Button loadfromDB;
    }
}