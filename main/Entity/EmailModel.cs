using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace SiteWatcher.Entity
{
    public class EmailModel
    {
        private string _To;
        /// <summary>
        /// 发件人姓名
        /// </summary>
        public string To
        {
            get
            {
                return _To;
            }
            set
            {
                _To = value;
            }
        }

        private int _Port;
        /// <summary>
        /// 端口
        /// </summary>
        public int Port
        {
            set
            {
                _Port = value;
            }
            get
            {
                return _Port;
            }
        }
        private string _From;
        /// <summary>
        /// 发件人地址
        /// </summary>
        public string From
        {
            set
            {
                _From = value;
            }
            get
            {
                return _From;
            }
        }
        private string _FromPass;
        /// <summary>
        /// 发件人邮件地址对应的密码
        /// </summary>
        public string FromPass
        {
            get
            {
                return _FromPass;
            }
            set
            {
                _FromPass = value;
            }
        }


        private string _Title;
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }

        private string _Body;
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body
        {
            get
            {
                return _Body;
            }
            set
            {
                _Body = value;
            }
        }

        private MailPriority _Priority = System.Net.Mail.MailPriority.Normal;
        /// <summary>
        /// 邮件级别
        /// </summary>
        public MailPriority Priority
        {
            get
            {
                return _Priority;
            }
            set
            {
                _Priority = value;
            }
        }

        private Encoding _MailEncoding = System.Text.Encoding.UTF8;
        /// <summary>
        /// 邮件编码
        /// </summary>
        public Encoding MailEncoding
        {
            get
            {
                return _MailEncoding;
            }
            set
            {
                _MailEncoding = value;
            }
        }

        private bool _IsBodyHtml = true;
        /// <summary>
        /// 邮件内容是否支持html
        /// </summary>
        public bool IsBodyHtml
        {
            get
            {
                return _IsBodyHtml;
            }
            set
            {
                _IsBodyHtml = value;
            }
        }

        private string smtp;
        public string Smtp
        {
            get
            {
                return smtp;
            }
            set
            {
                smtp = value;
            }
        }
        private bool enableSsl;
        public bool EnableSsl
        {
            get
            {
                return enableSsl;
            }
            set
            {
                enableSsl = value;
            }
        }
    }
}
