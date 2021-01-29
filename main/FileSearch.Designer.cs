namespace SiteWatcher
{
    partial class FileSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileSearch));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.txtFilesPath = new System.Windows.Forms.ToolStripTextBox();
            this.btnSelPathForWatch = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.txtFileType = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnToSetting = new System.Windows.Forms.ToolStripButton();
            this.lstFiles = new SiteWatcher.Ctr.DoubleBufferListView();
            this.stTool = new System.Windows.Forms.StatusStrip();
            this.stlbSearchTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbFindingInfo = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.stTool.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel6,
            this.txtFilesPath,
            this.btnSelPathForWatch,
            this.toolStripLabel5,
            this.txtFileType,
            this.toolStripSeparator9,
            this.btnToSetting});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(831, 39);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(32, 36);
            this.toolStripLabel6.Text = "目录";
            // 
            // txtFilesPath
            // 
            this.txtFilesPath.Name = "txtFilesPath";
            this.txtFilesPath.Size = new System.Drawing.Size(200, 39);
            // 
            // btnSelPathForWatch
            // 
            this.btnSelPathForWatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelPathForWatch.Image = ((System.Drawing.Image)(resources.GetObject("btnSelPathForWatch.Image")));
            this.btnSelPathForWatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelPathForWatch.Name = "btnSelPathForWatch";
            this.btnSelPathForWatch.Size = new System.Drawing.Size(36, 36);
            this.btnSelPathForWatch.Text = "toolStripButton8";
            this.btnSelPathForWatch.Click += new System.EventHandler(this.btnSelPathForWatch_Click);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(32, 36);
            this.toolStripLabel5.Text = "类型";
            // 
            // txtFileType
            // 
            this.txtFileType.Name = "txtFileType";
            this.txtFileType.Size = new System.Drawing.Size(200, 39);
            this.txtFileType.Text = ".aspx,.ashx";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 39);
            // 
            // btnToSetting
            // 
            this.btnToSetting.Image = ((System.Drawing.Image)(resources.GetObject("btnToSetting.Image")));
            this.btnToSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnToSetting.Name = "btnToSetting";
            this.btnToSetting.Size = new System.Drawing.Size(92, 36);
            this.btnToSetting.Text = "查找设置";
            this.btnToSetting.Click += new System.EventHandler(this.btnToSetting_Click);
            // 
            // lstFiles
            // 
            this.lstFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFiles.Location = new System.Drawing.Point(0, 39);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(831, 525);
            this.lstFiles.TabIndex = 9;
            this.lstFiles.UseCompatibleStateImageBehavior = false;
            this.lstFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstFiles_MouseDoubleClick);
            // 
            // stTool
            // 
            this.stTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stlbSearchTime});
            this.stTool.Location = new System.Drawing.Point(0, 542);
            this.stTool.Name = "stTool";
            this.stTool.Size = new System.Drawing.Size(831, 22);
            this.stTool.TabIndex = 11;
            this.stTool.Text = "statusStrip1";
            // 
            // stlbSearchTime
            // 
            this.stlbSearchTime.Name = "stlbSearchTime";
            this.stlbSearchTime.Size = new System.Drawing.Size(0, 17);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbFindingInfo);
            this.panel1.Controls.Add(this.btnPause);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 505);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(831, 37);
            this.panel1.TabIndex = 12;
            // 
            // lbFindingInfo
            // 
            this.lbFindingInfo.AutoSize = true;
            this.lbFindingInfo.ForeColor = System.Drawing.Color.Red;
            this.lbFindingInfo.Location = new System.Drawing.Point(4, 11);
            this.lbFindingInfo.Name = "lbFindingInfo";
            this.lbFindingInfo.Size = new System.Drawing.Size(0, 12);
            this.lbFindingInfo.TabIndex = 2;
            // 
            // btnPause
            // 
            this.btnPause.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(535, 0);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(148, 37);
            this.btnPause.TabIndex = 1;
            this.btnPause.Text = "暂停";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(683, 0);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(148, 37);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "结束";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // FileSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 564);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stTool);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FileSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件查找替换";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.stTool.ResumeLayout(false);
            this.stTool.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ctr.DoubleBufferListView lstFiles;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripTextBox txtFilesPath;
        private System.Windows.Forms.ToolStripButton btnSelPathForWatch;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripTextBox txtFileType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton btnToSetting;
        private System.Windows.Forms.StatusStrip stTool;
        private System.Windows.Forms.ToolStripStatusLabel stlbSearchTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbFindingInfo;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
    }
}