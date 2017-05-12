namespace TOS
{
    partial class Form1
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
            this.readDataBTN = new System.Windows.Forms.Button();
            this.epslonBTN = new System.Windows.Forms.Button();
            this.poleEpslonBTN = new System.Windows.Forms.Button();
            this.epslonCUTBTN = new System.Windows.Forms.Button();
            this.PCECBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.readDataBox = new System.Windows.Forms.TextBox();
            this.epslonBox = new System.Windows.Forms.TextBox();
            this.PCepslonBox = new System.Windows.Forms.TextBox();
            this.epslonCUTBox = new System.Windows.Forms.TextBox();
            this.PCECBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // readDataBTN
            // 
            this.readDataBTN.Location = new System.Drawing.Point(41, 25);
            this.readDataBTN.Name = "readDataBTN";
            this.readDataBTN.Size = new System.Drawing.Size(75, 23);
            this.readDataBTN.TabIndex = 0;
            this.readDataBTN.Text = "读数据";
            this.readDataBTN.UseVisualStyleBackColor = true;
            this.readDataBTN.Click += new System.EventHandler(this.readDataBTN_Click);
            // 
            // epslonBTN
            // 
            this.epslonBTN.Location = new System.Drawing.Point(41, 78);
            this.epslonBTN.Name = "epslonBTN";
            this.epslonBTN.Size = new System.Drawing.Size(75, 23);
            this.epslonBTN.TabIndex = 1;
            this.epslonBTN.Text = "Epslon";
            this.epslonBTN.UseVisualStyleBackColor = true;
            this.epslonBTN.Click += new System.EventHandler(this.epslonBTN_Click);
            // 
            // poleEpslonBTN
            // 
            this.poleEpslonBTN.Location = new System.Drawing.Point(41, 131);
            this.poleEpslonBTN.Name = "poleEpslonBTN";
            this.poleEpslonBTN.Size = new System.Drawing.Size(106, 22);
            this.poleEpslonBTN.TabIndex = 2;
            this.poleEpslonBTN.Text = "极点CUT+Epslon";
            this.poleEpslonBTN.UseVisualStyleBackColor = true;
            this.poleEpslonBTN.Click += new System.EventHandler(this.poleEpslonBTN_Click);
            // 
            // epslonCUTBTN
            // 
            this.epslonCUTBTN.Location = new System.Drawing.Point(41, 183);
            this.epslonCUTBTN.Name = "epslonCUTBTN";
            this.epslonCUTBTN.Size = new System.Drawing.Size(75, 23);
            this.epslonCUTBTN.TabIndex = 11;
            this.epslonCUTBTN.Text = "EpslonCUT";
            this.epslonCUTBTN.Click += new System.EventHandler(this.epslonCUTBTN_Click);
            // 
            // PCECBTN
            // 
            this.PCECBTN.Location = new System.Drawing.Point(41, 236);
            this.PCECBTN.Name = "PCECBTN";
            this.PCECBTN.Size = new System.Drawing.Size(121, 23);
            this.PCECBTN.TabIndex = 4;
            this.PCECBTN.Text = "极点CUT+EpslonCUT";
            this.PCECBTN.UseVisualStyleBackColor = true;
            this.PCECBTN.Click += new System.EventHandler(this.PCECBTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 269);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "时间/s";
            // 
            // readDataBox
            // 
            this.readDataBox.Location = new System.Drawing.Point(195, 27);
            this.readDataBox.Name = "readDataBox";
            this.readDataBox.Size = new System.Drawing.Size(100, 21);
            this.readDataBox.TabIndex = 6;
            // 
            // epslonBox
            // 
            this.epslonBox.Location = new System.Drawing.Point(195, 79);
            this.epslonBox.Name = "epslonBox";
            this.epslonBox.Size = new System.Drawing.Size(100, 21);
            this.epslonBox.TabIndex = 7;
            // 
            // PCepslonBox
            // 
            this.PCepslonBox.Location = new System.Drawing.Point(195, 131);
            this.PCepslonBox.Name = "PCepslonBox";
            this.PCepslonBox.Size = new System.Drawing.Size(100, 21);
            this.PCepslonBox.TabIndex = 8;
            // 
            // epslonCUTBox
            // 
            this.epslonCUTBox.Location = new System.Drawing.Point(195, 183);
            this.epslonCUTBox.Name = "epslonCUTBox";
            this.epslonCUTBox.Size = new System.Drawing.Size(100, 21);
            this.epslonCUTBox.TabIndex = 9;
            // 
            // PCECBox
            // 
            this.PCECBox.Location = new System.Drawing.Point(195, 235);
            this.PCECBox.Name = "PCECBox";
            this.PCECBox.Size = new System.Drawing.Size(100, 21);
            this.PCECBox.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 307);
            this.Controls.Add(this.PCECBox);
            this.Controls.Add(this.epslonCUTBox);
            this.Controls.Add(this.PCepslonBox);
            this.Controls.Add(this.epslonBox);
            this.Controls.Add(this.readDataBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PCECBTN);
            this.Controls.Add(this.epslonCUTBTN);
            this.Controls.Add(this.poleEpslonBTN);
            this.Controls.Add(this.epslonBTN);
            this.Controls.Add(this.readDataBTN);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button readDataBTN;
        private System.Windows.Forms.Button epslonBTN;
        private System.Windows.Forms.Button poleEpslonBTN;
        private System.Windows.Forms.Button epslonCUTBTN;
        private System.Windows.Forms.Button PCECBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox readDataBox;
        private System.Windows.Forms.TextBox epslonBox;
        private System.Windows.Forms.TextBox PCepslonBox;
        private System.Windows.Forms.TextBox epslonCUTBox;
        private System.Windows.Forms.TextBox PCECBox;
    }
}

