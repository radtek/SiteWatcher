using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SiteWatcher
{
    public partial class MobileSetting : Form
    {
        public MobileSetting()
        {
            InitializeComponent();
            texPhoneList.Text = Core.Configs.ConfigsControl.Instance.ReciveMobiles;
        }

        private void btnTestSend_Click(object sender, EventArgs e)
        {
            try
            {
                btnTestSend.Enabled = false;
                btnTestSend.Text = "短信发送中...";

                string[] sPhoneNums = Core.Configs.ConfigsControl.Instance.GetReciveMobiles();

                foreach (string phone in sPhoneNums)
                {
                    Core.Utils.SendMobileMsg("这是一条测试短信！"+DateTime.Now, phone);
                }
                MessageBox.Show("短信已经发送！");
                btnTestSend.Enabled = true;
                btnTestSend.Text = "发送测试短信";
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送失败:" + ex.Message);
            }

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            Core.Configs.ConfigsControl.Instance.ReciveMobiles = texPhoneList.Text;

            Core.Configs.ConfigsControl.SaveConfig();


            MessageBox.Show("设置保存成功！");
        }
    }
}
