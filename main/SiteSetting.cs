using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SiteWatcher
{
    public partial class SiteSetting : Form
    {
        public SiteSetting()
        {
            InitializeComponent();

            #region 载入站点配置
            txtSiteName.Text = Core.Configs.ConfigsControl.Instance.SiteName;
            txtDemain.Text = Core.Configs.ConfigsControl.Instance.SiteDomain;
            txtSitePath.Text = Core.Configs.ConfigsControl.Instance.SitePath;
            txtIISPath.Text = Core.Configs.ConfigsControl.Instance.IISLogPath;
            #endregion
        }


        private void btnSaveConfig_Click(object sender, EventArgs e)
        {

            Core.Configs.ConfigsControl.Instance.SiteName = txtSiteName.Text;
            Core.Configs.ConfigsControl.Instance.SiteDomain = txtDemain.Text;
            Core.Configs.ConfigsControl.Instance.SitePath = txtSitePath.Text;
            Core.Configs.ConfigsControl.Instance.IISLogPath = txtIISPath.Text;

            Core.Configs.ConfigsControl.SaveConfig();

            MessageBox.Show("保存成功！");

        }

        private void btnSelSitePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fld = new FolderBrowserDialog();//c#实现的类，代码关键
            fld.ShowDialog();
            txtSitePath.Text = fld.SelectedPath.Trim();

        }

        private void btnSelIISPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fld = new FolderBrowserDialog();//c#实现的类，代码关键
            fld.ShowDialog();
            txtIISPath.Text = fld.SelectedPath.Trim();
        }
    }
}
