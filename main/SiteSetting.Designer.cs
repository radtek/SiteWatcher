namespace SiteWatcher
{
    partial class SiteSetting
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
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.btnSelIISPath = new System.Windows.Forms.Button();
            this.txtIISPath = new System.Windows.Forms.TextBox();
            this.txtSitePath = new System.Windows.Forms.TextBox();
            this.txtDemain = new System.Windows.Forms.TextBox();
            this.txtSiteName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSelSitePath = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Location = new System.Drawing.Point(258, 199);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(153, 37);
            this.btnSaveConfig.TabIndex = 21;
            this.btnSaveConfig.Text = "保存设置";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // btnSelIISPath
            // 
            this.btnSelIISPath.Location = new System.Drawing.Point(591, 137);
            this.btnSelIISPath.Name = "btnSelIISPath";
            this.btnSelIISPath.Size = new System.Drawing.Size(75, 23);
            this.btnSelIISPath.TabIndex = 20;
            this.btnSelIISPath.Text = "选择目录";
            this.btnSelIISPath.UseVisualStyleBackColor = true;
            this.btnSelIISPath.Click += new System.EventHandler(this.btnSelIISPath_Click);
            // 
            // txtIISPath
            // 
            this.txtIISPath.Location = new System.Drawing.Point(122, 139);
            this.txtIISPath.Name = "txtIISPath";
            this.txtIISPath.Size = new System.Drawing.Size(463, 21);
            this.txtIISPath.TabIndex = 19;
            // 
            // txtSitePath
            // 
            this.txtSitePath.Location = new System.Drawing.Point(122, 103);
            this.txtSitePath.Name = "txtSitePath";
            this.txtSitePath.Size = new System.Drawing.Size(463, 21);
            this.txtSitePath.TabIndex = 16;
            // 
            // txtDemain
            // 
            this.txtDemain.Location = new System.Drawing.Point(122, 66);
            this.txtDemain.Name = "txtDemain";
            this.txtDemain.Size = new System.Drawing.Size(277, 21);
            this.txtDemain.TabIndex = 14;
            // 
            // txtSiteName
            // 
            this.txtSiteName.Location = new System.Drawing.Point(122, 26);
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.Size = new System.Drawing.Size(277, 21);
            this.txtSiteName.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "IIS日志存放目录：";
            // 
            // btnSelSitePath
            // 
            this.btnSelSitePath.Location = new System.Drawing.Point(591, 101);
            this.btnSelSitePath.Name = "btnSelSitePath";
            this.btnSelSitePath.Size = new System.Drawing.Size(75, 23);
            this.btnSelSitePath.TabIndex = 17;
            this.btnSelSitePath.Text = "选择目录";
            this.btnSelSitePath.UseVisualStyleBackColor = true;
            this.btnSelSitePath.Click += new System.EventHandler(this.btnSelSitePath_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "网站存放目录：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "网站域名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "网站名称：";
            // 
            // SiteSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 309);
            this.Controls.Add(this.btnSaveConfig);
            this.Controls.Add(this.btnSelIISPath);
            this.Controls.Add(this.txtIISPath);
            this.Controls.Add(this.txtSitePath);
            this.Controls.Add(this.txtDemain);
            this.Controls.Add(this.txtSiteName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSelSitePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SiteSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "网站设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button btnSelIISPath;
        private System.Windows.Forms.TextBox txtIISPath;
        private System.Windows.Forms.TextBox txtSitePath;
        private System.Windows.Forms.TextBox txtDemain;
        private System.Windows.Forms.TextBox txtSiteName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSelSitePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}