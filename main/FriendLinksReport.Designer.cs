namespace SiteWatcher
{
    partial class FriendLinksReport
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
            this.components = new System.ComponentModel.Container();
            this.lvReport = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.cmnMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制网址ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开网址ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFriendLinkNoCheck = new System.Windows.Forms.TextBox();
            this.btnFriendLinkNoCheck = new System.Windows.Forms.Button();
            this.cmnMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvReport
            // 
            this.lvReport.Location = new System.Drawing.Point(12, 58);
            this.lvReport.Name = "lvReport";
            this.lvReport.Size = new System.Drawing.Size(833, 523);
            this.lvReport.TabIndex = 0;
            this.lvReport.UseCompatibleStateImageBehavior = false;
            this.lvReport.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvReport_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "检测情况：";
            // 
            // txtInfo
            // 
            this.txtInfo.BackColor = System.Drawing.SystemColors.Control;
            this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInfo.Location = new System.Drawing.Point(97, 21);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(732, 14);
            this.txtInfo.TabIndex = 2;
            // 
            // cmnMenu
            // 
            this.cmnMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制网址ToolStripMenuItem,
            this.打开网址ToolStripMenuItem});
            this.cmnMenu.Name = "cmnMenu";
            this.cmnMenu.Size = new System.Drawing.Size(125, 48);
            // 
            // 复制网址ToolStripMenuItem
            // 
            this.复制网址ToolStripMenuItem.Name = "复制网址ToolStripMenuItem";
            this.复制网址ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.复制网址ToolStripMenuItem.Text = "复制网址";
            this.复制网址ToolStripMenuItem.Click += new System.EventHandler(this.复制网址ToolStripMenuItem_Click);
            // 
            // 打开网址ToolStripMenuItem
            // 
            this.打开网址ToolStripMenuItem.Name = "打开网址ToolStripMenuItem";
            this.打开网址ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.打开网址ToolStripMenuItem.Text = "打开网址";
            this.打开网址ToolStripMenuItem.Click += new System.EventHandler(this.打开网址ToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(873, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "忽略网址(一行一个):";
            // 
            // txtFriendLinkNoCheck
            // 
            this.txtFriendLinkNoCheck.Location = new System.Drawing.Point(863, 58);
            this.txtFriendLinkNoCheck.Multiline = true;
            this.txtFriendLinkNoCheck.Name = "txtFriendLinkNoCheck";
            this.txtFriendLinkNoCheck.Size = new System.Drawing.Size(209, 476);
            this.txtFriendLinkNoCheck.TabIndex = 4;
            // 
            // btnFriendLinkNoCheck
            // 
            this.btnFriendLinkNoCheck.Location = new System.Drawing.Point(919, 553);
            this.btnFriendLinkNoCheck.Name = "btnFriendLinkNoCheck";
            this.btnFriendLinkNoCheck.Size = new System.Drawing.Size(118, 23);
            this.btnFriendLinkNoCheck.TabIndex = 5;
            this.btnFriendLinkNoCheck.Text = "保存";
            this.btnFriendLinkNoCheck.UseVisualStyleBackColor = true;
            this.btnFriendLinkNoCheck.Click += new System.EventHandler(this.btnFriendLinkNoCheck_Click);
            // 
            // FriendLinksReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 593);
            this.Controls.Add(this.btnFriendLinkNoCheck);
            this.Controls.Add(this.txtFriendLinkNoCheck);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvReport);
            this.Name = "FriendLinksReport";
            this.Text = "死链报告";
            this.cmnMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvReport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.ContextMenuStrip cmnMenu;
        private System.Windows.Forms.ToolStripMenuItem 复制网址ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开网址ToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFriendLinkNoCheck;
        private System.Windows.Forms.Button btnFriendLinkNoCheck;
    }
}