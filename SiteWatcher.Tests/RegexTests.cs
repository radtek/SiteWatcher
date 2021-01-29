 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SiteWatcher.BLL;

namespace SiteWatcher.Tests
{
    
    public class RegexTests
    {
        [Test]
        public void RegexFinds()
        {
            List<FriendLinkModel> lvLinks = BLL.FriendLink.Instance.GetFriendLinks("http://www.91lai.com/");

            string sDomain = BLL.FriendLink.Instance.GetDomain("http://www.beimai.com/").Trim();
            foreach (var v in lvLinks)
            {

                if (v.LinkUrl.IndexOf(sDomain) > -1)//有没有反向链接 
                {
                    Console.WriteLine("名称:{0};链接:{1},是否IsNofollow:{2}", v.LinkText, v.LinkUrl, v.IsNofollow);
                    break;
                }


            }

             

        }
        [Test]
        public void GetHtml()
        {
            string s = XS.Core.WebUtils.LoadURLString("http://koubei.16888.com/");

            Console.WriteLine(s);

        }

    }
}