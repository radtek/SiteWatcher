using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteWatcher.Core.STSdbUtils;

namespace SiteWatcher.Entity
{
    public class BadLinkNoCheck :  STSdbEntityBase<Guid>//Base
    {
        public string Url { get; set; }
         
    }
     
}
