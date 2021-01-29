using System;
using System.Collections.Generic;
using System.Text;

namespace SiteWatcher.Entity
{
    public class SpiderBLL
    {
        static public string GetNameByTag(string tag,List<SpiderEntity>  Spiders)
        {
            foreach (SpiderEntity spider in Spiders)
            {
                if (spider.SpiderTag.ToString() == tag)
                {
                    return spider.SpiderCnName;

                }
            }
            return "未知";
            
        }
       static public List<SpiderEntity> GetSpiders()
        {
            List<SpiderEntity> SpiderList = new List<SpiderEntity>();


            SpiderList.Add(new SpiderEntity() { SpiderCnName = "百度", SpiderEnName = "Baiduspider", SpiderTag = 1 });

            SpiderList.Add(new SpiderEntity() { SpiderCnName = "谷歌", SpiderEnName = "Googlebot", SpiderTag = 2 });
            SpiderList.Add(new SpiderEntity() { SpiderCnName = "360搜索", SpiderEnName = "360Spider", SpiderTag = 3 });
            SpiderList.Add(new SpiderEntity() { SpiderCnName = "SOSO", SpiderEnName = "Sosospider", SpiderTag = 4 });
            SpiderList.Add(new SpiderEntity() { SpiderCnName = "搜狗", SpiderEnName = "sogou", SpiderTag = 5 });
            SpiderList.Add(new SpiderEntity() { SpiderCnName = "MSN", SpiderEnName = "msnbot", SpiderTag = 6 });
            SpiderList.Add(new SpiderEntity() { SpiderCnName = "雅虎", SpiderEnName = "Yahoo", SpiderTag = 7 });
            SpiderList.Add(new SpiderEntity() { SpiderCnName = "必应", SpiderEnName = "bingbot", SpiderTag = 8 });
            SpiderList.Add(new SpiderEntity() { SpiderCnName = "一搜", SpiderEnName = "YisouSpider", SpiderTag = 9 });
            SpiderList.Add(new SpiderEntity() { SpiderCnName = "有道", SpiderEnName = "YoudaoBot", SpiderTag = 10 });

            //SpiderList.Add(new SpiderEntity() { SpiderCnName = "IE", SpiderEnName = "MSIE", SpiderTag = 11 });
            //SpiderList.Add(new SpiderEntity() { SpiderCnName = "IE8", SpiderEnName = "MSIE 8.0", SpiderTag = 12 });
            //SpiderList.Add(new SpiderEntity() { SpiderCnName = "IE6", SpiderEnName = "MSIE 6.0", SpiderTag = 13 });
            //SpiderList.Add(new SpiderEntity() { SpiderCnName = "IE7", SpiderEnName = "MSIE 7.0", SpiderTag = 14 });
            //SpiderList.Add(new SpiderEntity() { SpiderCnName = "Firefox", SpiderEnName = "Firefox", SpiderTag = 15 });
            //SpiderList.Add(new SpiderEntity() { SpiderCnName = "Chrome", SpiderEnName = "Chrome", SpiderTag = 16 });
            //SpiderList.Add(new SpiderEntity() { SpiderCnName = "360浏览器", SpiderEnName = "360browser", SpiderTag = 17 });
            //SpiderList.Add(new SpiderEntity() { SpiderCnName = "手机", SpiderEnName = "Mobile", SpiderTag = 18 });

            return SpiderList;
        }

    }
    public class SpiderEntity
    {
        public string SpiderCnName { get; set; }
        public string SpiderEnName { get; set; }
        public int SpiderTag { get; set; }//0为其他
    }
}
