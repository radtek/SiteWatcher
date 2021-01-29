namespace SiteWatcher
{
    partial class EmailHistory
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
            this.lvEmailList = new System.Windows.Forms.ListView();
            this.btnDelAll = new System.Windows.Forms.Button();
            this.btnDelSend = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvEmailList
            // 
            this.lvEmailList.Location = new System.Drawing.Point(12, 29);
            this.lvEmailList.Name = "lvEmailList";
            this.lvEmailList.Size = new System.Drawing.Size(952, 487);
            this.lvEmailList.TabIndex = 0;
            this.lvEmailList.UseCompatibleStateImageBehavior = false;
            // 
            // btnDelAll
            // 
            this.btnDelAll.Location = new System.Drawing.Point(466, 542);
            this.btnDelAll.Name = "btnDelAll";
            this.btnDelAll.Size = new System.Drawing.Size(112, 31);
            this.btnDelAll.TabIndex = 1;
            this.btnDelAll.Text = "清空所有记录";
            this.btnDelAll.UseVisualStyleBackColor = true;
            this.btnDelAll.Click += new System.EventHandler(this.btnDelAll_Click);
            // 
            // btnDelSend
            // 
            this.btnDelSend.Location = new System.Drawing.Point(616, 542);
            this.btnDelSend.Name = "btnDelSend";
            this.btnDelSend.Size = new System.Drawing.Size(112, 31);
            this.btnDelSend.TabIndex = 2;
            this.btnDelSend.Text = "清空已发送记录";
            this.btnDelSend.UseVisualStyleBackColor = true;
            this.btnDelSend.Click += new System.EventHandler(this.btnDelSend_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(773, 542);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(156, 31);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "发送所有未成功邮件";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // EmailHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 599);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnDelSend);
            this.Controls.Add(this.btnDelAll);
            this.Controls.Add(this.lvEmailList);
            this.Name = "EmailHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "提醒邮件发送记录";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvEmailList;
        private System.Windows.Forms.Button btnDelAll;
        private System.Windows.Forms.Button btnDelSend;
        private System.Windows.Forms.Button btnSend;
    }
}