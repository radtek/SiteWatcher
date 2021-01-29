namespace SiteWatcher
{
    partial class BadLinkNoCheck
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
            this.lbList = new System.Windows.Forms.ListBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelSels = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbList
            // 
            this.lbList.FormattingEnabled = true;
            this.lbList.ItemHeight = 12;
            this.lbList.Location = new System.Drawing.Point(12, 75);
            this.lbList.Name = "lbList";
            this.lbList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbList.Size = new System.Drawing.Size(420, 196);
            this.lbList.TabIndex = 0;
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(13, 36);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(343, 21);
            this.txtUrl.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(363, 36);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(69, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelSels
            // 
            this.btnDelSels.Location = new System.Drawing.Point(326, 277);
            this.btnDelSels.Name = "btnDelSels";
            this.btnDelSels.Size = new System.Drawing.Size(106, 23);
            this.btnDelSels.TabIndex = 3;
            this.btnDelSels.Text = "删除所选";
            this.btnDelSels.UseVisualStyleBackColor = true;
            this.btnDelSels.Click += new System.EventHandler(this.btnDelSels_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "修改后需要重启软件";
            // 
            // BadLinkNoCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 320);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDelSels);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.lbList);
            this.Name = "BadLinkNoCheck";
            this.Text = "忽略地址";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbList;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelSels;
        private System.Windows.Forms.Label label1;
    }
}