using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteWatcher.Core.STSdbUtils;

namespace SiteWatcher.Entity
{
    public class EmailQueue : STSdbEntityBase<Guid>//Base
    {
        public string ToEmail { get; set; }

        public string EmailTilte { get; set; }

        public string EmailBody { get; set; }

        public DateTime AddDateTime { get; set; }

        public DateTime SendDateTime { get; set; }
        /// <summary>
        /// 如果是0表示未发送，如果是1表示发送成功，如果是2发送中
        /// </summary>
        public int IsSend { get; set; }
        /// <summary>
        /// 0表示Email,1表示手机短信
        /// </summary>
        public int MsgType { get; set; }
         
    }
 
}
