namespace SiteWatcher
{
    partial class MumaConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MumaConfig));
            this.label1 = new System.Windows.Forms.Label();
            this.txtNoScanFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveConfigMuma = new System.Windows.Forms.Button();
            this.lstMumaRule = new System.Windows.Forms.ListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "跳过文件夹:";
            // 
            // txtNoScanFolder
            // 
            this.txtNoScanFolder.Location = new System.Drawing.Point(12, 34);
            this.txtNoScanFolder.Multiline = true;
            this.txtNoScanFolder.Name = "txtNoScanFolder";
            this.txtNoScanFolder.Size = new System.Drawing.Size(643, 60);
            this.txtNoScanFolder.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(87, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(287, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "(多个文件夹名称用逗号分开，如test1,test2,test3)";
            // 
            // btnSaveConfigMuma
            // 
            this.btnSaveConfigMuma.Location = new System.Drawing.Point(247, 444);
            this.btnSaveConfigMuma.Name = "btnSaveConfigMuma";
            this.btnSaveConfigMuma.Size = new System.Drawing.Size(136, 35);
            this.btnSaveConfigMuma.TabIndex = 3;
            this.btnSaveConfigMuma.Text = " 保存配置";
            this.btnSaveConfigMuma.UseVisualStyleBackColor = true;
            this.btnSaveConfigMuma.Click += new System.EventHandler(this.btnSaveConfigMuma_Click);
            // 
            // lstMumaRule
            // 
            this.lstMumaRule.Location = new System.Drawing.Point(9, 91);
            this.lstMumaRule.Name = "lstMumaRule";
            this.lstMumaRule.Size = new System.Drawing.Size(628, 204);
            this.lstMumaRule.TabIndex = 4;
            this.lstMumaRule.UseCompatibleStateImageBehavior = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 20);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(497, 40);
            this.textBox1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(515, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 40);
            this.button1.TabIndex = 7;
            this.button1.Text = " 添加规则 ";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.lstMumaRule);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(11, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(644, 301);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "管理木马特征规则";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "规则列表:";
            // 
            // MumaConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 495);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSaveConfigMuma);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNoScanFolder);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MumaConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "木马设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNoScanFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveConfigMuma;
        private System.Windows.Forms.ListView lstMumaRule;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
    }
}