using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteWatcher.Core.STSdbUtils;

namespace SiteWatcher.Entity
{
    public class BadLink :  STSdbEntityBase<Guid>//Base
    {
        public string PageName { get; set; }
        public string PUrl { get; set; }

        public int TimeSpan { get; set; }//小时

        public DateTime LastTestDate { get; set; }

        public int BadLinkCount { get; set; }

        public List<BadLinkReport> Reports = new List<BadLinkReport>(); 
         
    }

    public class BadLinkReport  :  STSdbEntityBase<Guid>
    {
        public string PUrl { get; set; }
        public bool IsOK { get; set; }
        public string Info { get; set; }
    }

}
