using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SiteWatcher.Entity;
using MongoDB.Driver.Builders;
using MongoDB.Driver;

namespace SiteWatcher
{
    public partial class EmailHistory : Form
    {
        public EmailHistory()
        {
            InitializeComponent();


            lvEmailList.View = View.Details;//只有设置为这个HeaderStyle才有用
            lvEmailList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvEmailList.GridLines = true;//显示网格线
            lvEmailList.FullRowSelect = true;//选择时选择是行，而不是某列
            lvEmailList.Columns.Add("发送目标", 100, HorizontalAlignment.Center);
            lvEmailList.Columns.Add("状态", 100, HorizontalAlignment.Center);
            lvEmailList.Columns.Add("添加时间", 100, HorizontalAlignment.Center);
            lvEmailList.Columns.Add("标题", 200, HorizontalAlignment.Center);
            lvEmailList.Columns.Add("邮件内容", 500, HorizontalAlignment.Center);
            BindData();
        }

        private void BindData()
        {
            lvEmailList.Items.Clear();
            SortByDocument sort = new SortByDocument { { "AddDateTime", -1 } };
             List<Entity.EmailQueue> lst =  BLL.EmailQueue.Instance.GetList().Where(rd=>rd.MsgType==0).OrderBy(rd=>rd.AddDateTime).ToList();
             foreach (EmailQueue queue in lst)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems[0].Text = queue.ToEmail;
                string status = "已发送";
                if(queue.IsSend==0)
                {
                    status = "未发送";
                }
                else if(queue.IsSend==2)
                {
                    status = "发送中";
                }
                lvi.SubItems.Add(status);
                lvi.SubItems.Add(queue.AddDateTime.ToLocalTime().ToString());
                lvi.SubItems.Add(queue.EmailTilte);
                lvi.SubItems.Add(queue.EmailBody);
                lvEmailList.Items.Add(lvi);
            }
        }

        private void btnDelAll_Click(object sender, EventArgs e)
        {
            List<Entity.EmailQueue> lst = BLL.EmailQueue.Instance.GetList().Where(rd => rd.MsgType == 0).OrderBy(rd => rd.AddDateTime).ToList();

            foreach (var message in lst)
            {
                BLL.EmailQueue.Instance.Delete(message.Id);
            }
            BindData();

        }

        private void btnDelSend_Click(object sender, EventArgs e)
        {
            List<Entity.EmailQueue> lst = BLL.EmailQueue.Instance.GetList();
            foreach (EmailQueue queue in lst)
            {
                if (queue.IsSend==1&& queue.MsgType==0)
                    BLL.EmailQueue.Instance.Delete(queue.Id);
            }
            BindData();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
           BLL.EmailQueue.Instance.SendEmail();
            MessageBox.Show("成功发送数据!");
        }
    }
}
