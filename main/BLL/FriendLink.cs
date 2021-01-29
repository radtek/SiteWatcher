using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MongoDB.Driver.Builders;
using SiteWatcher.Core.STSdbUtils;

namespace SiteWatcher.BLL
{
    /// <summary>
    /// yaoqing链接
    /// </summary>
    public class FriendLink : BllBase<Entity.FriendLink, Guid>//Base<Entity.UpdateLink>
    {
        static public readonly FriendLink Instance = new FriendLink();
        override protected string TableName
        {
            get { return "FriendLinks"; }
        }
        override public Guid Add(Entity.FriendLink md)
        {
            md.Id = Guid.NewGuid();
            InsertOne(md);
            return md.Id;
        }
         public bool IsBadLink(Entity.FriendLinkReport report)
         {
             if (!report.IsOK)
                 return true;
             if(!report.IsLinkBack)
                return true;
            if (report.IsNofollow)
                return true;
            return false;
        }

        public List<FriendLinkModel> GetFriendLinks(string url)
        {
            string s = XS.Core.WebUtils.GetHtml(url);
            return GetFriendLinks(url, s);
        }

        public string GetDomain(string sUrl)
        {
            return XS.Core.RegexBll.RegexFind(@"(http|ftp|https://)[^\.]*\.(?<domain>[^/|?]*)", sUrl, "domain"); 
        }
        public List<FriendLinkModel> GetFriendLinks(string url, string sHtml)
        {
            string sSiteUrl = GetDomain(url);//需要过滤掉自己及相关自己的子域名


            List<FriendLinkModel> rz = new List<FriendLinkModel>();


            Regex r = new Regex(@"(?is)<a[^>]*?href=[^>]*?(['""]?)(?<url>[^'""\s>]+)\1[^>]*>(?<text>(?:(?!</?a\b).)*)</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            MatchCollection mc = r.Matches(sHtml);
            List<string> sUrlTem = new List<string>();
            for (int i = 0; i < mc.Count; i++)
            {
                string sUrl = mc[i].Groups["url"].Value;
                if ((sUrl.StartsWith("http") || sUrl.StartsWith("https"))&&!sUrlTem.Contains(sUrl)&& sUrl.IndexOf(sSiteUrl) == -1)
                {
                    sUrlTem.Add(sUrl);

                    FriendLinkModel mo = new FriendLinkModel();
                    string sAHtml = mc[i].Groups[0].Value;//整个a标签的html,里包含nofollow
                    bool isNofollow = false;
                    if (!string.IsNullOrEmpty(sAHtml))
                    {
                        sAHtml = sAHtml.ToLower();
                        if (sAHtml.IndexOf("nofollow") > -1)
                            isNofollow = true;
                    }

                    string sText = mc[i].Groups["text"].Value;

                    mo.LinkUrl = sUrl.ToLower().Trim();
                    mo.IsNofollow = isNofollow;
                    mo.LinkText = sText;
                    rz.Add(mo);
                    
                }
                

            }
             

            return rz;
        }
    }

    public class FriendLinkModel
    {
        public string LinkUrl { get; set; }
        public string LinkText { get; set; }
        public bool IsNofollow { get; set; }
    }


}
