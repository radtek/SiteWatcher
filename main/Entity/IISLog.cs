using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XS.DataProfile;

namespace SiteWatcher.Entity
{
    public class IISLog : MongodbEntityBase
    {
        public string VUrl { get; set; }//用户在当前时间访问哪一个文件或具体页面


        public int VDateInc { get; set; }//日期
        public string VDateShow { get; set; }

        public int VTime { get; set; }//时间  秒


        public string VPostGet { get; set; }//Post还是Get


        public string VQuery { get; set; }//查询的字符串


        public int VPort { get; set; }//访问的端口


        public string VIP { get; set; }//来访IP
        public long VIPInc { get; set; }//来访IP


        public string VAgent { get; set; }//访问的搜索引擎和蜘蛛名称


        public int VStatus { get; set; }//服务器返回的状态


        public int VTimeTaken { get; set; }//处理时间


        public int VSCbyte { get; set; }//服务端传送到客户端的字节大小


        public int VCSbyte { get; set; }//客户端传送到服务端的字节大小
        /// <summary>
        /// 蜘蛛标记
        /// </summary>
        public int SpiderTag { get; set; }
         
    }
}
