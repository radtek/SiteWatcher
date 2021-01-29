using SiteWatcher.BLL;
using SiteWatcher.Core;
using SiteWatcher.Entity;
using SiteWatcher.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace SiteWatcher
{
    public partial class MainApp
    {
        private void BindErrSites()
        {
            lvErrSiteList.Items.Clear();//先清空，防止重复绑定出错
            List<Entity.SiteErr> mds = BLL.SiteErr.Instance.GetList(); // BLL.SiteErr.Instance.GetAll();

            foreach (var md in mds)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems[0].Text = md.SiteName;
                lvi.SubItems.Add(md.SitePool);
                lvi.SubItems.Add(md.TestUrl);
                lvi.SubItems.Add(md.TimeSpan.ToString());
                lvi.SubItems.Add(md.ErrCount.ToString());
                lvi.SubItems.Add(md.SendReportCount.ToString());
                lvi.SubItems.Add(md.LastErrDateTime.ToString());
                lvi.SubItems.Add(md.Id.ToString());
                lvErrSiteList.Items.Add(lvi);

            }

        }
        private void btnAddSite_Click(object sender, EventArgs e)
        {
            //检测是否重复网址
            List<Entity.SiteErr> siteErrlist = BLL.SiteErr.Instance.GetList();
            if (siteErrlist.Exists(st => st.TestUrl == txtTestUrl.Text.Trim()))
            {
                MessageBox.Show("该网站已经存在！");
                return;
            }
            Entity.SiteErr md = new Entity.SiteErr();
            md.AddDateTime = DateTime.Now;
            md.ErrCount = 0;
            md.LastErrDateTime = DateTime.Now;
            md.SendReportCount = 0;
            md.SiteName = txtSiteName.Text;
            md.SitePool = txtIISPool.Text.Trim();
            md.TestUrl = txtTestUrl.Text.Trim();
            md.TimeSpan = int.Parse(drpTimeSpan.Text);
            BLL.SiteErr.Instance.Add(md);

            BindErrSites();
            //MessageBox.Show("添加成功！");
        }

        private void btnWatchSite_Click(object sender, EventArgs e)
        {
            if (btnWatchSite.Text == "监视")
            {
                AppStartInit.LoadErrorSiteTimer();
                foreach (var errorSiteTimer in AppStartInit.ErrorSiteTimers)
                {
                    errorSiteTimer.Value.TimerBackEvent += new EventHandler<WatchTimerEventArgs>(UpdateErrorSiteLvItem);
                    errorSiteTimer.Value.TimerBack(null, null);//立即执行
                }

                btnWatchSite.Image = Resources.stop;
                btnWatchSite.Text = "停止";
            }
            else
            {
                foreach (WatchLinkTimer errorSiteTimer in AppStartInit.ErrorSiteTimers.Values)
                {
                    errorSiteTimer.Stop();
                }
                AppStartInit.ErrorSiteTimers.Clear();
                btnWatchSite.Image = Resources.start;
                btnWatchSite.Text = "监视";
            }
        }

        private delegate void UpdateSiteState(WatchTimerEventArgs arg);
        private void MtUpdateSiteState(WatchTimerEventArgs arg)
        {
            string url = arg.Url;
            Thread thread = new Thread(()=>
            {
                string reportinfo = "";
                int rel = ValidateUrl(url, out reportinfo);
                if(rel!=1)
                {
                    DateTime dt = DateTime.Now;

                    bool isAddToMobileMsg = (rel != 4 || rel != 5);//4的时候会误报，只email报告
                     

                    foreach (ListViewItem lvi in lvErrSiteList.Items)
                    {
                        if (lvi.SubItems[2].Text.Equals(url))
                        {
                            int errorNum = Convert.ToInt32(lvi.SubItems[4].Text);
                            int reportNum = Convert.ToInt32(lvi.SubItems[4].Text);
                            lvi.SubItems[4].Text = (errorNum + 1).ToString();
                            lvi.SubItems[5].Text = (reportNum + 1).ToString();
                            lvi.SubItems[6].Text = dt.ToString();
                        }
                    }

                    BLL.EmailQueue.Instance.AddEmailToDB(string.Concat(reportinfo, dt), string.Format("{0},检测时间：{1},检测页面：{2}", reportinfo, dt, url), isAddToMobileMsg);
                }
            });
            thread.Start();
        }
        private void UpdateErrorSiteLvItem(object sender, WatchTimerEventArgs arg)
        {
            UpdateSiteState updateSiteState = new UpdateSiteState(MtUpdateSiteState);
            lvErrSiteList.Invoke(updateSiteState, arg);
        }

         /// <summary>
        /// 检测指定的 Uri 是否有效
        /// </summary>
        /// <param name="url">指定的Url地址</param>
        /// <returns>int 0.链接无效 1.表示有效 2.超时</returns>
        public int ValidateUrl(string url,out string err)
        {

            if (Regex.IsMatch(url.Trim(), @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&%\$#\=~])*[^\.\,\)\(\s]$"))
            {
                try
                {
                    //连接到目标网页
                    HttpWebRequest wreq = (HttpWebRequest) WebRequest.Create(url);
                    wreq.Method = "GET";
                    wreq.UserAgent =
                        "Mozilla/5.0 (Windows; U; Windows NT 6.1; zh-CN; rv:1.9.2.8) Gecko/20100722 Firefox/3.6.8";
                    wreq.Timeout = 60*1000; //请求超时设置（1分钟）
                    HttpWebResponse wresp = (HttpWebResponse) wreq.GetResponse();
                    if (wresp.StatusCode == HttpStatusCode.OK)
                    {
                        wresp.Close();
                        err = "";

                        return 1;
                    }
                    else if (wresp.StatusCode == HttpStatusCode.GatewayTimeout)
                    {
                        wresp.Close();
                        err = "连接超时";
                        return 2;
                    }
                    else
                    {
                        err = "未知错误：" + wresp.StatusCode;
                        return 3;
                    }
                }
                catch (WebException ex)
                {
                    if (!Equals(ex.Response, null))
                    {
                       HttpWebResponse hwr =  ex.Response as HttpWebResponse;

                        if (!Equals(hwr, null))
                        {
                            int code = (int)hwr.StatusCode;
                            err = string.Concat("发生异常：", hwr.StatusCode);
                            return code;
                        }
                        else
                        {
                            err = string.Concat("发生异常：", ex.Message);
                            return 4;
                        }
                         
                    }
                }
                catch (Exception e)
                {
                    err = string.Concat("发生异常：", e.Message);
                    return 4;
                }
            }
            err = "不是一个正确的网址，请检查软件中所添加的网址是否正确:" + url;
            return 5;
        }
    }
}
