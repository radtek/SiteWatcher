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
    public partial class MobileMsgHistory : Form
    {
        public MobileMsgHistory()
        {
            InitializeComponent();

            lvEmails.View = View.Details;//只有设置为这个HeaderStyle才有用
            lvEmails.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvEmails.GridLines = true;//显示网格线
            lvEmails.FullRowSelect = true;//选择时选择是行，而不是某列 
            lvEmails.Columns.Add("短信内容", 400, HorizontalAlignment.Left);//文本左
            lvEmails.Columns.Add("状态", 80, HorizontalAlignment.Center);//文本距中
            lvEmails.Columns.Add("发送时间", 120, HorizontalAlignment.Center);//文本距中
            lvEmails.Columns.Add("接收人", 150, HorizontalAlignment.Center);//文本距中
            lvEmails.Columns.Add("队列时间", 120, HorizontalAlignment.Center);//文本距中

            BindEmails();
        }

        private void BindEmails()
        {
            lvEmails.Items.Clear();
            //List<Entity.Emails> lst2 = BLL.Emails.Instance.GetEmailList();
            List<Entity.EmailQueue> lst = BLL.EmailQueue.Instance.GetList().Where(rd => rd.MsgType == 1).OrderBy(rd => rd.AddDateTime).ToList();

            foreach (var message in lst)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems[0].Text = message.EmailBody;
                string status = "已发送";
                if (message.IsSend == 0)
                {
                    status = "未发送";
                }
                else if (message.IsSend == 2)
                {
                    status = "发送中";
                }
                lvi.SubItems.Add(status);
                lvi.SubItems.Add(message.SendDateTime.ToString());
                lvi.SubItems.Add(message.ToEmail);
                lvi.SubItems.Add(message.AddDateTime.ToString());

                lvEmails.Items.Add(lvi);
            }
        }

        private void btnDelAll_Click(object sender, EventArgs e)
        {
            List<Entity.EmailQueue> lst = BLL.EmailQueue.Instance.GetList().Where(rd => rd.MsgType == 1).OrderBy(rd => rd.AddDateTime).ToList();

            foreach (var message in lst)
            {
                BLL.EmailQueue.Instance.Delete(message.Id);
            }
            BindEmails();
        }

        private void btnDelSend_Click(object sender, EventArgs e)
        {
            List<Entity.EmailQueue> lst = BLL.EmailQueue.Instance.GetList();

            foreach (var message in lst)
            {
                if(message.MsgType==1&& message.IsSend==1)
                    BLL.EmailQueue.Instance.Delete(message.Id);
            }
            BindEmails();
        }
    }
}
