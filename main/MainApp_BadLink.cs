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
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SiteWatcher.Core;
using SiteWatcher.Ctr;
using SiteWatcher.Entity;
using SiteWatcher.Properties;
using XS.Core;

namespace SiteWatcher
{
    public partial class MainApp
    {
        private List<Entity.BadLinkNoCheck> urlNoChecks = new List<Entity.BadLinkNoCheck>();
        private void tblNoCheckUrl_Click(object sender, EventArgs e)
        {
            BadLinkNoCheck win = new BadLinkNoCheck();
            win.ShowDialog();
        }

        #region 死连接监控

        private delegate void UpdateCheckState(int badnum, int currentnum, int countnum, string key, bool iscomp,int status,string info);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="badnum"></param>
        /// <param name="currentnum">当前任务错误数,</param>
        /// <param name="countnum">当前任务要检查的链接数</param>
        /// <param name="key"></param>
        /// <param name="iscomp">是否完成</param>
        private void MtUpdateCheckState(int badnum, int currentnum, int countnum, string key, bool iscomp, int status, string info)
        {
            if (!iscomp)
            {
                foreach (ListViewItem lvi in lv404links.Items)
                {
                    if (lvi.SubItems[0].Text.Equals(string.Concat("--", key)))
                    {
                        lvi.SubItems[2].Text = badnum.ToString();
                        lvi.SubItems[3].Text = "完成";
                        lvi.SubItems[4].Text = DateTime.Now.ToString();
                        lvi.SubItems[5].Text = string.Format("Http返回状态：{0} {1}",status, info);//返回状态
                        break;
                    }
                }
            }
            else
            {
                List<BadLinkReport> lstBadLinkReport = new List<BadLinkReport>();
                lstBadLinkReport.Clear();//清空之前的报表记录

                string datakey = string.Empty;
                //int reportcountnum = 0;
                string reportinfo = string.Empty;
                int reportbadnum = 0;
                StringBuilder sbBadReportInfo = new StringBuilder();
                foreach (ListViewItem lvi in lv404links.Items)
                {
                    if (lvi.SubItems[0].Text.StartsWith("--"))  //更新报表明细
                    {
                        lvi.Remove();
                        BadLinkReport md = new BadLinkReport();
                        string sHttpStatus = lvi.SubItems[5].Text;
                        md.PUrl = string.Format("{0} {1}", lvi.SubItems[0].Text, sHttpStatus);
                        md.IsOK = (lvi.SubItems[2].Text == "0");
                       
                        md.Info = sHttpStatus;

                        lstBadLinkReport.Add(md);
                        if (!md.IsOK)
                        {
                            sbBadReportInfo.AppendFormat("失败网址:{0},请求{1};", md.PUrl, sHttpStatus);
                        }
                            
                    }
                    else if (lvi.SubItems[0].Text.Equals(key)) //更新任务报表
                    {
                        //reportcountnum = countnum;
                        reportbadnum = badnum;
                        reportinfo = string.Format("完成,总共处理地址{0}条,找到死链接{1}条", countnum, badnum);
                        lvi.SubItems[5].Text = reportinfo;
                        lvi.SubItems[3].Text = "空闲";
                        lvi.BackColor = Color.White;
                        datakey = lvi.SubItems[6].Text;

                    }
                }
                //更新到报告列表
                if (!string.IsNullOrEmpty(datakey) && lstBadLinkReport.Count > 0)
                {
                    Entity.BadLink md = BLL.BadLink.Instance.GetEntity(new Guid(datakey));//BLL.BadLink.Instance.GetOne(datakey);
                    md.Reports = lstBadLinkReport;
                    md.LastTestDate = DateTime.Now;
                    md.BadLinkCount = reportbadnum;
                    BLL.BadLink.Instance.UpdateOne(md);

                }
                //添加到邮件列表
                if (reportbadnum > 0)
                {

                    BLL.EmailQueue.Instance.AddEmailToDB(reportinfo, string.Format("{0},检测时间：{1},检测页面：{2},详细情况：{3}", reportinfo, DateTime.Now, key, sbBadReportInfo));

                }

                if (BadLinkQue.ContainsKey(CurrentBadLink))
                {
                    BadLinkQue.Remove(CurrentBadLink);
                }
                CurrentBadLink = string.Empty;

                if (BadLinkQue.Count > 0)
                {
                    UpdateBadLinkLvItem(null, BadLinkQue.Values.ElementAt(0));
                }
            }

             

        }
        private static object lockobjloadhtml = new object();
        /// <summary>
        /// 从指定的URL下载页面内容(使用WebClient)
        /// </summary>
        /// <param name="url">指定URL</param>
        /// <returns></returns>
        public static string LoadPageContent(string url)
        {
            lock (lockobjloadhtml)
            {
                WebClient wc = new WebClient();
                wc.Headers["User-Agent"] = "blah";
                wc.Credentials = CredentialCache.DefaultCredentials;
                byte[] pageData = wc.DownloadData(url);
                return (Encoding.GetEncoding("gb2312").GetString(pageData));
            }

        }

        //private bool IsHaveBadLinkCheck = false;
        private string CurrentBadLink = string.Empty;
        private Dictionary<string, WatchTimerEventArgs> BadLinkQue = new Dictionary<string, WatchTimerEventArgs>();
        private Thread thThreadCheckLink;
        private void UpdateBadLinkLvItem(object sender, WatchTimerEventArgs arg)
        {
            if (!string.IsNullOrEmpty(CurrentBadLink)) //如果当前有执行任务，请记录到队列
            {
                if (!CurrentBadLink.Equals(arg.Url))
                {
                    if (!BadLinkQue.ContainsKey(arg.Url))
                    {
                        BadLinkQue.Add(arg.Url, arg);
                        foreach (ListViewItem lvi in lv404links.Items)
                        {
                            if (lvi.SubItems[0].Text.Equals(arg.Url))
                            {
                                lvi.SubItems[3].Text = "排队中";
                                lvi.BackColor = Color.Yellow;
                                break;
                            }
                        }
                    }

                }

                return;
            }
            else
            {
                //BadLinkQue.Clear();
                //BadLinkQue.Add(arg.Url, arg);
            }

            foreach (ListViewItem lvi in lv404links.Items)
            {
                if (lvi.SubItems[0].Text.Equals(arg.Url))//&& lvi.SubItems[3].Text.Equals("空闲")
                {
                    lvi.SubItems[4].Text = arg.UpdateTime.ToString();
                    string Content = "";
                    try
                    {
                         Content = LoadPageContent(arg.Url);
                    }
                    catch (Exception e)
                    {
                        BLL.EmailQueue.Instance.AddEmailToDB("发生严重的问题，页面不能访问", string.Format("检查死链模板页面不能访问:{0},检测时间：{1},详细情况：{2}", arg.Url,DateTime.Now, e.Message));
                        continue;
                    }
                    
                    Regex reg = new Regex(@"(?is)<a[^>]*?href=(['""]?)(?<url>[^'""\s>]+)\1[^>]*>(?<text>(?:(?!</?a\b).)*)</a>");
                    MatchCollection mc = reg.Matches(Content);
                    Uri baseUri = new Uri(arg.Url);
                    List<string> urls = new List<string>();

                    foreach (Match m in mc)
                    {

                        Uri uriPage = new Uri(baseUri, m.Groups["url"].Value);
                        
                        string sUrl = uriPage.ToString();
                        //sUrl = XS.Core.WebUtils.UrlEncode(sUrl);
                        urls.Add(sUrl);
                    }

                    List<string> NUrls = (from i in urls select i).Distinct().ToList();

                    if (NUrls.Count > 0)
                    {
                        lvi.SubItems[5].Text = string.Format("找到{0}个网址，正在检查...", NUrls.Count);
                        lvi.SubItems[3].Text = "忙碌中";
                        lvi.BackColor = Color.Red;
                        CurrentBadLink = arg.Url;

                        foreach (string url in NUrls)  //载入所有要检查的地址
                        {
                            ListViewItem lvisub = new ListViewItem();
                            lvisub.BackColor = Color.PowderBlue;
                            lvisub.SubItems[0].Text = string.Concat("--", url);
                            lvisub.SubItems.Add("");
                            lvisub.SubItems.Add("");
                            lvisub.SubItems.Add("等待中");
                            lvisub.SubItems.Add("");
                            lvisub.SubItems.Add("");
                            lv404links.Items.Add(lvisub);
                        }



                        thThreadCheckLink = new Thread(() =>
                        {
                            UpdateCheckState dlgUpdateCheckState = new UpdateCheckState(MtUpdateCheckState);
                            int badnum = 0;
                            int currentnum = 0;
                            int countnum = NUrls.Count;
                            foreach (string surl in NUrls)
                            {
                                //Thread.Sleep(1000);
                                currentnum++;
                                bool IsBadLink = false;
                                string url = surl.Trim();
                                int istate = 0;
                                string errinfo = string.Empty;
                                if (IsToCheck(url))
                                {
                                    istate = WebUtils.ValidateUrl(url,out errinfo);
                                    if (istate != 200)
                                    {
                                        badnum++;
                                        IsBadLink = true;
                                    }
                                }
                               

                                lv404links.Invoke(dlgUpdateCheckState, IsBadLink ? 1 : 0, currentnum, countnum, url, false, istate, errinfo);


                            }

                            lv404links.Invoke(dlgUpdateCheckState, badnum, currentnum, countnum, arg.Url, true,0,"");

                        });
                        thThreadCheckLink.ApartmentState = ApartmentState.STA;
                        thThreadCheckLink.Start();

                        
                    }
                }
            }
        }
       
        private bool IsToCheck(string sUrl)
        {
           
            foreach (Entity.BadLinkNoCheck url in urlNoChecks)
            {
                if (XS.Core.WebUtils.UrlEncode(sUrl).Equals(WebUtils.UrlEncode(url.Url)))
                    return false;
            }
            return true;
        }
        private void btnAddBadTime_Click(object sender, EventArgs e)
        {
        
            string txtBadlink = txtBadLink.Text.Trim();
            string sPageName = txtUrlPageName.Text.Trim();

            int timespan = XS.Core.XsUtils.ObjectToInt(drpTimeSpanBadlink.SelectedItem,0); 

            if (timespan <= 0)
            {
                MessageBox.Show("请选择时间间隔！");
                return;
            }
                

            if (!string.IsNullOrEmpty(txtBadlink))
            {
                if (!BLL.BadLink.Instance.IsHaveUrl(txtBadlink))
                {

                    Entity.BadLink md = new BadLink();
                    md.PUrl = txtBadlink;
                    md.LastTestDate = DateTime.Now;
                    md.TimeSpan = timespan;
                    md.PageName = sPageName;
                    BLL.BadLink.Instance.Add(md);
                    BindBadLink();
                }
                else
                {
                    MessageBox.Show("已经存在此地址！");
                }
            }
            else
            {
                MessageBox.Show("请输入正确的网址！");
            }
        }
        private void BindBadLink()
        {
            lv404links.Items.Clear();

            List<Entity.BadLink> lst = BLL.BadLink.Instance.GetList();

            foreach (BadLink link in lst)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.SubItems[0].Text = link.PUrl;
                lvi.SubItems.Add(link.TimeSpan.ToString() + "小时");//更新第一个列的值，如果您使用惯了asp.net,可能觉得这样更新有点不可理解
                lvi.SubItems.Add(link.BadLinkCount.ToString());
                lvi.SubItems.Add("空闲");
                lvi.SubItems.Add(link.LastTestDate.ToString());
                lvi.SubItems.Add("每一次启用");
                lvi.SubItems.Add(link.Id.ToString());
                lvi.SubItems.Add(link.PageName);
                lv404links.Items.Add(lvi);

            }

        }
        private void btnStartBadLinkTimer_Click(object sender, EventArgs e)
        {

            if (btnStartBadLinkTimer.Text.Equals("开始监听"))
            {
                AppStartInit.LoadBadLinkTimer();

                foreach (var linkTimer in AppStartInit.BadLinkTimers)
                {
                    linkTimer.Value.TimerBackEvent += new EventHandler<WatchTimerEventArgs>(UpdateBadLinkLvItem);
                    linkTimer.Value.TimerBack(null, null);//立即执行
                }

                btnStartBadLinkTimer.Image = Resources.stop;
                btnStartBadLinkTimer.Text = "停止监听";
            }
            else
            {
                foreach(WatchLinkTimer timer in AppStartInit.BadLinkTimers.Values)
                {
                    timer.Stop();
                }
                AppStartInit.BadLinkTimers.Clear();

                btnStartBadLinkTimer.Image = Resources.start;
                btnStartBadLinkTimer.Text = "开始监听";
            }

        }



        #endregion

    }
}
