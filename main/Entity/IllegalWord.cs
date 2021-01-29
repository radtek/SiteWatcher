using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteWatcher.Core.STSdbUtils;

namespace SiteWatcher.Entity
{
    public class IllegalWord : STSdbEntityBase<Guid>//Base
    {
        public string WordName { get; set; }

        public int WordNum { get; set; }//小时

        public DateTime AddDateTime { get; set; }

         
    }
}
