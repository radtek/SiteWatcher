using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms; 
using SiteWatcher.Core.Configs.ConfigsBase;

namespace SiteWatcher.Core.Configs
{
    /// <summary>
    /// 尖对客户端的个人习惯设置
    /// </summary>
    public class ConfigsInfo : IConfigInfo
    {

        #region 报警

        public int CpuMaxNum   { get; set; }

        #endregion

        #region 系统异常设置
        //public string ErrUrl1 { get; set; }
        //public string ErrUrl2 { get; set; }
        //public string ErrUrl3 { get; set; }
        //public int ErrUrlTimeSpan { get; set; }
        //public string ErrUrlState { get; set; }
        //public string ErrUrlSolve { get; set; }
        
        #endregion

        public string SelNetCar { get; set; }
        public string SiteName { get; set; }
        public string SitePath { get; set; }
        public string IISLogPath { get; set; }
        public string SiteDomain { get; set; }
        public string WatchType { get; set; }
        public string IISLogGroupType { get; set; }
        public string NoScanFolder { get; set; }
        public string MobileSendPluginName { get; set; }

        private string _ConnectionString = "127.0.0.1";
        
        private string _LastUserName;

        public string GetConnectionString()
        {

            return this._ConnectionString;//CaSa.Core.DES.Decode(this._ConnectionString, "casa369913836");
        }
        public string ConnectionString
        {
            get
            {
                return this._ConnectionString;
            }
            set
            {
                this._ConnectionString = value;
            }
        }
        /// <summary>
        /// 最后登录用户名
        /// </summary>
        public string LastUserName { get; set; }


        #region
        public string SmtpServer { get; set; }
        public string SendEmail { get; set; }
        public string SendEmailPass { get; set; }
        public int SendEmailPort { get; set; }
        public bool SendEmailIsOpenSSL { get; set; }

        public string ReciveEmails { get; set; }



        public string ReciveMobiles { get; set; }

        public string[] GetReciveMobiles()
        {
            if (!string.IsNullOrEmpty(ReciveMobiles))
            {
                return ReciveMobiles.Split('\n');
            }
            return null;
        }

        public string[] GetReciveEmails()
        {
            if (!string.IsNullOrEmpty(ReciveEmails))
            {
                return ReciveEmails.Split('\n');
            }
            return null;
        }

        #endregion



    }
}
