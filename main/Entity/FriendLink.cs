using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteWatcher.Core.STSdbUtils;

namespace SiteWatcher.Entity
{
    /// <summary>
    /// 友情链接
    /// </summary>
    public class FriendLink :  STSdbEntityBase<Guid>//Base
    {
        
        public string PageName { get; set; } //页面名称
        public string PUrl { get; set; }    //要检测友情链接的页面,如,http://www.baidu.com/abc.html
        public string Domain { get; set; }    //要检测友情链接的页面,如 www.baidu.com/abc.html

        public int TimeSpan { get; set; }//小时

        //public int LinkCount { get; set; }//最后更新友情链接数量
        

        public DateTime LastTestDate { get; set; }

        public bool IsHaveBad { get; set; } //是否有异常

        public List<FriendLinkReport> Reports = new List<FriendLinkReport>();

        public string NoToCheckUrl { get; set; } //忽略检查的网址，一行一个

        public string[] GetNoCheckUrls()
        {
            return XS.Core.Strings.GetString.GetArrByWrap(NoToCheckUrl);
        }

    }

    public class FriendLinkReport :  STSdbEntityBase<Guid>
    {
        
        public string PUrl { get; set; }//友情链接
        public string LinkToText { get; set; }//链出文本
        public bool IsOK { get; set; } //是否能链接
        public bool IsLinkBack { get; set; } //是否链接回来
        public string LinkBackText { get; set; } //反向链接的描文本
        public bool IsNofollow { get; set; } //是否Nofollow
        public int IncdBaidu { get; set; } //百度收录量
        public bool IsNewAdd { get; set; } //是否能链接
        public int CurrentBR { get; set; }
        public int CurrentPR { get; set; }  //暂时改成移动BR
        //public int UpMonthBR { get; set; }  //上月
        //public int UpMonthPR { get; set; } //上月
        public int AddBR { get; set; }  //添加时
        public int AddPR { get; set; } //添加时
        public DateTime LastUpdateDate { get; set; }  //最后一次更新时间
        //public DateTime LastMonthUpdateDate { get; set; } //上月更新时间
        public string Info { get; set; } //汇报情况
        public int OutLinkCount { get; set; } //外链数

        public bool IsNoToCheckUrl { get; set; } //是否忽略检查

    }

}
