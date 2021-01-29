using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteWatcher.Core.STSdbUtils;

namespace SiteWatcher.Entity
{ 

    public class ReCreatFile :  STSdbEntityBase<Guid>
    {
        /// <summary>
        /// 0代表，utfg-8,1代表gb2312
        /// </summary>
        public int FileType { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsStart { get; set; }
        public string FilePath { get; set; } 
        public string ContentTem { get; set; }
        public DateTime AdDateTime { get; set; }
    }

}
