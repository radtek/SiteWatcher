using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteWatcher.Core.STSdbUtils;

namespace SiteWatcher.Entity
{
    public class UpdateLink : STSdbEntityBase<Guid>// Base
    {
        public string PUrl { get; set; }

        public int TimeSpan { get; set; }//小时

        public DateTime LastTestDate { get; set; }

        public string LastContent { get; set; }

         
    }
}
