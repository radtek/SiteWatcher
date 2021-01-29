using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteWatcher.Core.STSdbUtils;

namespace SiteWatcher.Entity
{

    public class SiteErr :  STSdbEntityBase<Guid>//Base
    {
        public string SiteName { get; set; }
        public string SitePool{ get; set; } 
        public string TestUrl{ get; set; }
        public int TimeSpan { get; set; }
        public int ErrCount { get; set; }
        public int SendReportCount { get; set; }
        public DateTime LastErrDateTime { get; set; }
        public DateTime AddDateTime { get; set; }
        
         
    }

    
}
