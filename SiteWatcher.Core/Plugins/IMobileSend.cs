using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteWatcher.Core.Plugins
{
    public interface IMobileSend : IProvider
    {
        /// <summary>
        /// 发送一条短信
        /// </summary>
        /// <param name="Msg">短信内容</param>
        /// <param name="MobiNumber">手机号码</param>
        void SendMsg(string Msg, string MobiNumber);
       
    }
}
