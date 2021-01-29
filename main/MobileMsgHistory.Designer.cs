namespace SiteWatcher
{
    partial class MobileMsgHistory
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
            this.lvEmails = new System.Windows.Forms.ListView();
            this.btnDelSend = new System.Windows.Forms.Button();
            this.btnDelAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvEmails
            // 
            this.lvEmails.Location = new System.Drawing.Point(0, 0);
            this.lvEmails.Name = "lvEmails";
            this.lvEmails.Size = new System.Drawing.Size(856, 530);
            this.lvEmails.TabIndex = 6;
            this.lvEmails.UseCompatibleStateImageBehavior = false;
            // 
            // btnDelSend
            // 
            this.btnDelSend.Location = new System.Drawing.Point(732, 539);
            this.btnDelSend.Name = "btnDelSend";
            this.btnDelSend.Size = new System.Drawing.Size(112, 31);
            this.btnDelSend.TabIndex = 8;
            this.btnDelSend.Text = "清空已发送记录";
            this.btnDelSend.UseVisualStyleBackColor = true;
            this.btnDelSend.Click += new System.EventHandler(this.btnDelSend_Click);
            // 
            // btnDelAll
            // 
            this.btnDelAll.Location = new System.Drawing.Point(582, 539);
            this.btnDelAll.Name = "btnDelAll";
            this.btnDelAll.Size = new System.Drawing.Size(112, 31);
            this.btnDelAll.TabIndex = 7;
            this.btnDelAll.Text = "清空所有记录";
            this.btnDelAll.UseVisualStyleBackColor = true;
            this.btnDelAll.Click += new System.EventHandler(this.btnDelAll_Click);
            // 
            // MobileMsgHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(856, 582);
            this.Controls.Add(this.btnDelSend);
            this.Controls.Add(this.btnDelAll);
            this.Controls.Add(this.lvEmails);
            this.Name = "MobileMsgHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "短信记录列表";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvEmails;
        private System.Windows.Forms.Button btnDelSend;
        private System.Windows.Forms.Button btnDelAll;
    }
}