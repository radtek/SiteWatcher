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
    public partial class EmailSetting : Form
    {
        public EmailSetting()
        {
            InitializeComponent();


            txtSendEmail.Text = Core.Configs.ConfigsControl.Instance.SendEmail;
            txtSmtpServer.Text = Core.Configs.ConfigsControl.Instance.SmtpServer;
            txtEmailPass.Text = Core.Configs.ConfigsControl.Instance.SendEmailPass;
            txtPort.Text = Core.Configs.ConfigsControl.Instance.SendEmailPort.ToString();
            cbIsSelSSL.Checked = Core.Configs.ConfigsControl.Instance.SendEmailIsOpenSSL;
            txtReciveEmails.Text = Core.Configs.ConfigsControl.Instance.ReciveEmails;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Core.Configs.ConfigsControl.Instance.SendEmail = txtSendEmail.Text;
            Core.Configs.ConfigsControl.Instance.SmtpServer = txtSmtpServer.Text;
            Core.Configs.ConfigsControl.Instance.SendEmailPass = txtEmailPass.Text;
            Core.Configs.ConfigsControl.Instance.SendEmailPort = int.Parse(txtPort.Text);
            Core.Configs.ConfigsControl.Instance.SendEmailIsOpenSSL = cbIsSelSSL.Checked;

            Core.Configs.ConfigsControl.Instance.ReciveEmails = txtReciveEmails.Text;

            Core.Configs.ConfigsControl.SaveConfig();
             

            MessageBox.Show("设置保存成功！");

        }

        private void btnTestSend_Click(object sender, EventArgs e)
        {

            try
            {
                btnTestSend.Enabled = false;
                btnTestSend.Text = "邮箱发送中...";

                string[] sEmails = Core.Configs.ConfigsControl.Instance.GetReciveEmails();

                foreach (string sEmail in sEmails)
                {
                    BLL.SMTPSend.SendEmails("这是一份测试邮件", "这是一份测试邮件SendEmails" + DateTime.Now, sEmail);
                }               

                MessageBox.Show("邮件已经发送，请到指定邮箱查收！");

                btnTestSend.Enabled = true;
                btnTestSend.Text = "发送测试邮件";
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送失败:"+ex.Message);
            }
            

        }
    }
}
