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
    public partial class ReportSettingCPU : Form
    {
        public ReportSettingCPU()
        {
            InitializeComponent();

            txtCpuNum.Text = Core.Configs.ConfigsControl.Instance.CpuMaxNum.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Core.Configs.ConfigsControl.Instance.CpuMaxNum = int.Parse(txtCpuNum.Text);
            Core.Configs.ConfigsControl.SaveConfig();

            MessageBox.Show("保存成功！");

        }
    }
}
