namespace SiteWatcher
{
    partial class ReCreactFileList
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btcSave = new System.Windows.Forms.Button();
            this.lvReCreactFile = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.cbIsStart = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件路径";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(85, 24);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(578, 21);
            this.txtPath.TabIndex = 1;
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(85, 62);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.Size = new System.Drawing.Size(578, 230);
            this.txtContent.TabIndex = 3;
            this.txtContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContent_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "恢复内容";
            // 
            // btcSave
            // 
            this.btcSave.Location = new System.Drawing.Point(402, 298);
            this.btcSave.Name = "btcSave";
            this.btcSave.Size = new System.Drawing.Size(75, 29);
            this.btcSave.TabIndex = 4;
            this.btcSave.Text = "添加";
            this.btcSave.UseVisualStyleBackColor = true;
            this.btcSave.Click += new System.EventHandler(this.btcSave_Click);
            // 
            // lvReCreactFile
            // 
            this.lvReCreactFile.Location = new System.Drawing.Point(12, 379);
            this.lvReCreactFile.Name = "lvReCreactFile";
            this.lvReCreactFile.Size = new System.Drawing.Size(663, 179);
            this.lvReCreactFile.TabIndex = 5;
            this.lvReCreactFile.UseCompatibleStateImageBehavior = false;
            this.lvReCreactFile.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvReCreactFile_MouseDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 357);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "已保存列表";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(588, 298);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 29);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(496, 298);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 29);
            this.btnDel.TabIndex = 8;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // cbIsStart
            // 
            this.cbIsStart.AutoSize = true;
            this.cbIsStart.Checked = true;
            this.cbIsStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsStart.Location = new System.Drawing.Point(318, 305);
            this.cbIsStart.Name = "cbIsStart";
            this.cbIsStart.Size = new System.Drawing.Size(72, 16);
            this.cbIsStart.TabIndex = 9;
            this.cbIsStart.Text = "是否启用";
            this.cbIsStart.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "文件路径";
            // 
            // cbBox
            // 
            this.cbBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBox.FormattingEnabled = true;
            this.cbBox.Items.AddRange(new object[] {
            "UTF-8",
            "gb2312"});
            this.cbBox.Location = new System.Drawing.Point(86, 303);
            this.cbBox.Name = "cbBox";
            this.cbBox.Size = new System.Drawing.Size(121, 20);
            this.cbBox.TabIndex = 11;
            // 
            // ReCreactFileList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 572);
            this.Controls.Add(this.cbBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbIsStart);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lvReCreactFile);
            this.Controls.Add(this.btcSave);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label1);
            this.Name = "ReCreactFileList";
            this.Text = "快速恢复文件管理";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btcSave;
        private System.Windows.Forms.ListView lvReCreactFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.CheckBox cbIsStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbBox;
    }
}