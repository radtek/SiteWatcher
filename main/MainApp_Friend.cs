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
using SiteWatcher.BLL;
using SiteWatcher.Core;
using SiteWatcher.Ctr;
using SiteWatcher.Entity;
using SiteWatcher.Properties;
using FriendLink = SiteWatcher.Entity.FriendLink;

namespace SiteWatcher
{
    public partial class MainApp
    {

        private void btnWatchWebLinkUrl_Click(object sender, EventArgs e)
        {

            if (btnWatchWebLinkUrl.Text.Equals("开始检控"))
            {
                AppStartInit.LoadFriendTimer();
                ;

                foreach (var linkTimer in AppStartInit.FriendTimers)
                {
                    linkTimer.Value.TimerBackEvent += new EventHandler<WatchTimerEventArgs>(UpdateFriendLinkLvItem);
                    linkTimer.Value.TimerBack(null, null); //点击后,立即执行
                }

                btnWatchWebLinkUrl.Image = Resources.stop;
                btnWatchWebLinkUrl.Text = "停止检控";
            }
            else
            {
                foreach (WatchLinkTimer timer in AppStartInit.FriendTimers.Values)
                {
                    timer.Stop();
                }
                AppStartInit.FriendTimers.Clear();

                btnWatchWebLinkUrl.Image = Resources.start;
                btnWatchWebLinkUrl.Text = "开始检控";
            }

            //MessageBox.Show(timespan.ToString());

        }

        private void UpdateFriendLinkLvItem(object sender, WatchTimerEventArgs arg)
        {
            

            string sUrl = arg.Url;
            Entity.FriendLink model = BLL.FriendLink.Instance.GetEntity(new Guid(arg.Id));

            try
            {
                string[] sNoCheckUrls = model.GetNoCheckUrls();
                List<FriendLinkModel> urls = BLL.FriendLink.Instance.GetFriendLinks(sUrl);

                List<string> urltemp = new List<string>();

                foreach (var url2 in model.Reports)
                {
                    urltemp.Add(url2.PUrl);
                }

                foreach (var s in urls)
                {
                    if (!urltemp.Contains(s.LinkUrl))
                    {
                        FriendLinkReport rp = new FriendLinkReport();
                        rp.PUrl = s.LinkUrl;
                        rp.IsOK = true;
                        rp.IsNewAdd = true;
                        rp.Info = "等待检测";
                        rp.LinkToText = s.LinkText;

                        if (!Equals(sNoCheckUrls, null) && sNoCheckUrls.Contains(rp.PUrl))
                        {
                            rp.Info = "忽略站点不做检测";
                            rp.IsNoToCheckUrl = true;
                        }

                        model.Reports.Add(rp);
                    }
                    
                    

                }
                BLL.FriendLink.Instance.UpdateOne(model);
                CheckFriedLink(model);
            }
            catch (Exception e)
            {
                if (!Equals(model, null))
                {
                    model.IsHaveBad = true;
                    BLL.FriendLink.Instance.UpdateOne(model);

                }
                else
                {
                    XS.Core.Log.ErrorLog.InfoFormat("更新{0}出错,model为null,具体原因:{1}", sUrl, e.Message);
                }
            }
            //重新绑定数据
            BindWebLink();

        }

        private void CheckFriedLink(Entity.FriendLink model)
        {
            model.IsHaveBad = false;
            List<FriendLinkReport> urls = model.Reports;

            foreach (var url in urls)
            {
                if (!url.IsNoToCheckUrl)
                {
                    try
                    {
                        string sSEO = XS.Core.WebUtils.GetHtml(string.Format("http://www.aizhan.com/cha/{0}", url.PUrl.Replace("http://", "").Replace("https://", "")));

                        int iBr = XS.Core.XsUtils.StrToInt(XS.Core.RegexBll.RegexFind("images/br/([0-9]*).gif", sSEO, 1));
                        int iPr = XS.Core.XsUtils.StrToInt(XS.Core.RegexBll.RegexFind("/br/br([0-9]*).gif", sSEO, 1));

                        if (url.IsNewAdd)
                        {
                            url.AddBR = iBr;
                            url.AddPR = iPr;//移动 BR
                        }
                        url.CurrentBR = iBr;
                        url.CurrentPR = iPr;
                        url.LastUpdateDate = DateTime.Now;
                        url.IsNewAdd = false;
                        url.IsOK = true;
                        url.Info = "访问正常";

                        //string sHtml = XS.Core.WebUtils.LoadURLString(url.PUrl);
                        List<FriendLinkModel> lvLinks = BLL.FriendLink.Instance.GetFriendLinks(url.PUrl);
                        url.OutLinkCount = lvLinks.Count;

                        if (lvLinks.Count > 0)
                        {
                            url.IsLinkBack = false;
                            string sDomain = BLL.FriendLink.Instance.GetDomain(model.PUrl).Trim();
                            foreach (var outlink in lvLinks)
                            {
                                if (outlink.LinkUrl.IndexOf(sDomain) > -1)//有没有反向链接 
                                {
                                    url.IsLinkBack = true;
                                    url.LinkBackText = outlink.LinkText;
                                    url.IsNofollow = outlink.IsNofollow;
                                    break;
                                }

                            }

                        }
                        else
                        {
                            url.IsLinkBack = true;
                            url.IsOK = false;
                            url.Info = "访问页面失败！";
                        }



                    }
                    catch (Exception e)
                    {
                        url.IsOK = false;
                        url.Info = "无法访问";
                    }

                    if (BLL.FriendLink.Instance.IsBadLink(url)) //发现异常，发送报告
                    {
                        model.IsHaveBad = true;
                        BLL.EmailQueue.Instance.AddEmailToDB("友情链接有异常", string.Format("来自{0}友情链接中的{1}发生异常，原因：{2}",
                            model.PUrl,
                            url.PUrl,
                            url.IsOK ? "百度权重或GooglePr下降" : "无法访问"
                            ));
                    }
                }
                
            }

            model.LastTestDate = DateTime.Now;
            BLL.FriendLink.Instance.UpdateOne(model);

            if (model.IsHaveBad)
            {
                foreach (ListViewItem lvi in lvWebLinkUrl.Items)
                {
                    if (lvi.SubItems[4].Text.Equals(model.Id.ToString()))
                    {
                        lvi.SubItems[2].Text = "有异常";
                        lvi.BackColor = Color.Red;
                        break;
                    }
                }
            }

           

        }
        private void lvWebLinkUrl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = lvWebLinkUrl.HitTest(e.X, e.Y);
            if (info.Item != null)
            {
                var videoitem = info.Item as ListViewItem;
                string dataid = videoitem.SubItems[5].Text;
                FriendLinksReport dlg = new FriendLinksReport(dataid);
                dlg.Show();

            }
        }

        private void btnAddWebLinkUrl_Click(object sender, EventArgs e)
        {
            string txtWeblink = txtWebLink.Text.Trim();
            List<FriendLink> linkList = BLL.FriendLink.Instance.GetList(); //BLL.UpdateLink.Instance.GetAll();
            if (linkList.Exists(lk => lk.PUrl == txtWeblink))
            {
                MessageBox.Show("该网址已经存在！");
                return;
            }
            int timespan = XS.Core.XsUtils.ObjectToInt(drpTimeSpanWebLink.SelectedItem,24);

            if (!string.IsNullOrEmpty(txtWeblink))
            {

                //XS.Core.WebUtils.LoadURLString(txtWeblink);

                Entity.FriendLink md = new FriendLink();
                md.PUrl = txtWeblink;
                md.LastTestDate = DateTime.Now;
                md.TimeSpan = timespan;
                md.IsHaveBad = false;
                BLL.FriendLink.Instance.Add(md);
                BindWebLink();
            }
        }

        private void BindWebLink()
        {

            lvWebLinkUrl.Items.Clear();
            List<Entity.FriendLink> lst = BLL.FriendLink.Instance.GetList();

            foreach (FriendLink link in lst)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.SubItems[0].Text = link.PUrl;
                lvi.SubItems.Add(link.TimeSpan + "小时");//更新第一个列的值，如果您使用惯了asp.net,可能觉得这样更新有点不可理解
                 
                lvi.SubItems.Add(link.IsHaveBad?"有异常":"正常");

                lvi.SubItems.Add(link.Reports.Count.ToString());

                lvi.SubItems.Add(link.LastTestDate.ToString());
                lvi.SubItems.Add(link.Id.ToString());
                lvWebLinkUrl.Items.Add(lvi);

            }

        }
        private void btnDelFriendLink_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvWebLinkUrl.SelectedItems)
            {
                string sid = lvi.SubItems[5].Text;

                BLL.FriendLink.Instance.Delete(new Guid(sid));

                BindWebLink();

            }
             
        }


    }
}
