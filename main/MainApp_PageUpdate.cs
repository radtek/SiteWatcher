using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SiteWatcher.Core;
using SiteWatcher.Ctr;
using SiteWatcher.Entity;
using SiteWatcher.Properties;

namespace SiteWatcher
{
    public partial class MainApp
    {

        #region 页面更新监控

        private void BindUpdateLink()
        {

            lvPageUpdate.Items.Clear();
            List<Entity.UpdateLink> lst = BLL.UpdateLink.Instance.GetList();

            foreach (UpdateLink link in lst)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.SubItems[0].Text = link.PUrl;
                lvi.SubItems.Add(link.TimeSpan.ToString() + "小时");//更新第一个列的值，如果您使用惯了asp.net,可能觉得这样更新有点不可理解
                lvi.SubItems.Add(link.LastTestDate.ToString());
                lvi.SubItems.Add(DateTime.Now.ToString());
                lvi.SubItems.Add("第一次启用");
                lvi.SubItems.Add("");
                lvi.SubItems.Add(link.Id.ToString());
                lvPageUpdate.Items.Add(lvi);

            }

        }

        private void btnAddUpdateLink_Click(object sender, EventArgs e)
        {
            string txtBadlink = txtUpdateLink.Text;
            List<UpdateLink> linkList = BLL.UpdateLink.Instance.GetList(); //BLL.UpdateLink.Instance.GetAll();
            if (linkList.Exists(lk => lk.PUrl == txtBadlink))
            {
                MessageBox.Show("该网址已经存在！");
                return;
            }
            int timespan = int.Parse(drpTimeSpanUpdate.SelectedItem.ToString());

            if (!string.IsNullOrEmpty(txtBadlink))
            {
                Entity.UpdateLink md = new UpdateLink();
                md.PUrl = txtBadlink;
                md.LastTestDate = DateTime.Now;
                md.TimeSpan = timespan;
                BLL.UpdateLink.Instance.Add(md);
                BindUpdateLink();
            }


        }

        private void btnStartWachUpdate_Click(object sender, EventArgs e)
        {
            if (btnStartWachUpdate.Text.Equals("开始监听"))
            {
                btnStartWachUpdate.Image = Resources.stop;
                btnStartWachUpdate.Text = "停止监听";

                //开始监听Timer，将Timer添加到资源字典
                AppStartInit.LoadUpdateLinkTimer();

                foreach (var linkTimer in AppStartInit.UpdateLinkTimers)
                {
                    linkTimer.Value.TimerBackEvent += new EventHandler<WatchTimerEventArgs>(UpdateLinkLvItem);
                    linkTimer.Value.TimerBack(null, null);
                }
            }
            else
            {
                btnStartWachUpdate.Image = Resources.start;
                btnStartWachUpdate.Text = "开始监听";
                //结束监听Timer
                foreach (WatchLinkTimer timer in AppStartInit.UpdateLinkTimers.Values)
                {
                    timer.Stop();
                }
                AppStartInit.UpdateLinkTimers.Clear();
            }
        }
        private void UpdateLinkLvItem(object sender, WatchTimerEventArgs arg)
        {
            foreach (ListViewItem lvi in lvPageUpdate.Items)
            {
                if (lvi.SubItems[0].Text.Equals(arg.Url))
                {
                    string lastmd5 = lvi.SubItems[5].Text;
                    string currentmd5 = "";
                    string content = "";
                    try
                    {
                        content = XS.Core.WebUtils.GetHtml(arg.Url);
                    }
                    catch (Exception e)
                    {
                        XS.Core.Log.ErrorLog.ErrorFormat("检查页面是否更新时，获取页面内容发生错误,页面:{0},错误：{1}", arg.Url,e.Message);

                        BLL.EmailQueue.Instance.AddEmailToDB("检查页面是否更新时，获取页面内容发生错误", string.Format("检查页面是否更新时，获取页面内容发生错误,页面:{0},错误：{1}", arg.Url, e.Message));

                    }
                    

                    if (!string.IsNullOrEmpty(content))
                    {
                        currentmd5 = Utils.MD5(content);
                    }
                    else
                    {
                        lvi.SubItems[4].Text = "获取页面发生异常，请查看日志";
                        return;
                    }
                    lvi.SubItems[3].Text = DateTime.Now.ToString();
                    if (!string.IsNullOrEmpty(lastmd5))
                    {
                        if (lastmd5.Equals(currentmd5))
                        {
                            lvi.SubItems[4].Text = "未更新";
                            BLL.EmailQueue.Instance.AddEmailToDB(string.Concat("发现有一个页面未能按时更新", DateTime.Now), string.Format("发现有一个页面未能按时更新,时间:{0},发现页面:{1}", DateTime.Now, arg.Url));
                        }
                        else
                        {
                            lvi.SubItems[4].Text = "正常更新";
                        }
                    }
                    else
                    {
                        lvi.SubItems[4].Text = "第一次检查";
                    }

                    lvi.SubItems[5].Text = currentmd5;
                }
            }
        }

        #endregion

    }
}
