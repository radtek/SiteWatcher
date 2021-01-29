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
    public partial class MumaConfig : Form
    {
        public MumaConfig()
        {
            InitializeComponent();
            txtNoScanFolder.Text = Core.Configs.ConfigsControl.Instance.NoScanFolder;
        }

        private void btnSaveConfigMuma_Click(object sender, EventArgs e)
        {
            Core.Configs.ConfigsControl.Instance.NoScanFolder = txtNoScanFolder.Text;
            Core.Configs.ConfigsControl.SaveConfig();

            MessageBox.Show("配置保存成功！");
        }
    }
}
