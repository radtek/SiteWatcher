using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SiteWatcher.Core;
using SiteWatcher.Entity;

namespace SiteWatcher
{
    public class AppStartInit
    {
        public static Dictionary<string, WatchLinkTimer> BadLinkTimers = new Dictionary<string, WatchLinkTimer>();

       static  public void LoadBadLinkTimer()
        {
           if (BadLinkTimers.Count == 0)
           {
               BadLinkTimers.Clear();
               List<Entity.BadLink> lst = BLL.BadLink.Instance.GetList();

               foreach (BadLink link in lst)
               {
                   WatchLinkTimer md = new WatchLinkTimer(link.PUrl, link.TimeSpan,TimeSpanUnit.Hour, link.Id.ToString());
                   md.Start();
                   BadLinkTimers.Add(link.PUrl, md);
               }
               
           }
          
 
        }


       public static Dictionary<string, WatchLinkTimer> UpdateLinkTimers = new Dictionary<string, WatchLinkTimer>();

       static public void LoadUpdateLinkTimer()
       {
           if (UpdateLinkTimers.Count == 0)
           {
               UpdateLinkTimers.Clear();
               List<Entity.UpdateLink> lst = BLL.UpdateLink.Instance.GetList();

               foreach (UpdateLink link in lst)
               {
                   WatchLinkTimer md = new WatchLinkTimer(link.PUrl, link.TimeSpan, TimeSpanUnit.Hour, link.Id.ToString());
                   md.Start();
                   UpdateLinkTimers.Add(link.PUrl, md);
               }
               
           }
       }




       public static Dictionary<string, WatchLinkTimer> ErrorSiteTimers = new Dictionary<string, WatchLinkTimer>();

       static public void LoadErrorSiteTimer()
       {
           if (ErrorSiteTimers.Count == 0)
           {
               ErrorSiteTimers.Clear();
               List<SiteErr> siteErrlist = BLL.SiteErr.Instance.GetList();

               foreach (SiteErr site in siteErrlist)
               {
                   WatchLinkTimer st = new WatchLinkTimer(site.TestUrl, site.TimeSpan, TimeSpanUnit.Minute, site.Id.ToString());
                   st.Start();
                   ErrorSiteTimers.Add(site.TestUrl, st);
               }

           }
       }

        public static Dictionary<string, WatchLinkTimer> FriendTimers = new Dictionary<string, WatchLinkTimer>();

        static public void LoadFriendTimer()
        {
            if (FriendTimers.Count == 0)
            {
                FriendTimers.Clear();
                List<FriendLink> siteErrlist = BLL.FriendLink.Instance.GetList();

                foreach (FriendLink site in siteErrlist)
                {
                    WatchLinkTimer st = new WatchLinkTimer(site.PUrl, site.TimeSpan, TimeSpanUnit.Hour, site.Id.ToString());
                    st.Start();
                    FriendTimers.Add(site.PUrl, st);
                }

            }
        }

    }
}
