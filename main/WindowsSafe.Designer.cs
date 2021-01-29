namespace SiteWatcher
{
    partial class WindowsSafe
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbIsAddPortToFire = new System.Windows.Forms.CheckBox();
            this.btnSavePort = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lbTab1Info = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtNetstatInfo = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(480, 262);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbIsAddPortToFire);
            this.tabPage1.Controls.Add(this.btnSavePort);
            this.tabPage1.Controls.Add(this.txtPort);
            this.tabPage1.Controls.Add(this.lbTab1Info);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(472, 236);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "远程桌面";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cbIsAddPortToFire
            // 
            this.cbIsAddPortToFire.AutoSize = true;
            this.cbIsAddPortToFire.Checked = true;
            this.cbIsAddPortToFire.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsAddPortToFire.Location = new System.Drawing.Point(23, 122);
            this.cbIsAddPortToFire.Name = "cbIsAddPortToFire";
            this.cbIsAddPortToFire.Size = new System.Drawing.Size(216, 16);
            this.cbIsAddPortToFire.TabIndex = 3;
            this.cbIsAddPortToFire.Text = "是否同时向防火墙添加端口入站规则";
            this.cbIsAddPortToFire.UseVisualStyleBackColor = true;
            // 
            // btnSavePort
            // 
            this.btnSavePort.Location = new System.Drawing.Point(94, 95);
            this.btnSavePort.Name = "btnSavePort";
            this.btnSavePort.Size = new System.Drawing.Size(75, 23);
            this.btnSavePort.TabIndex = 2;
            this.btnSavePort.Text = "修改端口";
            this.btnSavePort.UseVisualStyleBackColor = true;
            this.btnSavePort.Click += new System.EventHandler(this.btnSavePort_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(23, 95);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(65, 21);
            this.txtPort.TabIndex = 1;
            // 
            // lbTab1Info
            // 
            this.lbTab1Info.AutoSize = true;
            this.lbTab1Info.ForeColor = System.Drawing.Color.Red;
            this.lbTab1Info.Location = new System.Drawing.Point(21, 17);
            this.lbTab1Info.Name = "lbTab1Info";
            this.lbTab1Info.Size = new System.Drawing.Size(0, 12);
            this.lbTab1Info.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtNetstatInfo);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(472, 236);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "端口情况";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtNetstatInfo
            // 
            this.txtNetstatInfo.Location = new System.Drawing.Point(6, 6);
            this.txtNetstatInfo.Name = "txtNetstatInfo";
            this.txtNetstatInfo.Size = new System.Drawing.Size(460, 224);
            this.txtNetstatInfo.TabIndex = 3;
            this.txtNetstatInfo.Text = "";
            // 
            // WindowsSafe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 280);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WindowsSafe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows安全设置";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lbTab1Info;
        private System.Windows.Forms.Button btnSavePort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.CheckBox cbIsAddPortToFire;
        private System.Windows.Forms.RichTextBox txtNetstatInfo;
    }
}