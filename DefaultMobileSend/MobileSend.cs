using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using SiteWatcher.Core;
using SiteWatcher.Core.Plugins;

namespace Plugins.MobileSend
{
    [Extension("Winic手机短信发送组件", "1.0", "<a href=\"http://www.ebsite.net\">亿博团队</a>")]
    public class MobileSend : SiteWatcher.Core.Plugins.IMobileSend
    {

        private static string _sUserName = "N4736646";
        private static string _sPass = "62bkcKlCHSc29d";

        /// <summary>
        /// 向某个手机号码发送一条短信
        /// </summary>
        /// <param name="Msg">短信内容</param>
        /// <param name="MobiNumber">手机号码</param>
        /// <param name="UserName">用户帐号</param>
        public void SendMsg(string Msg, string MobiNumber)
        {

            if (!string.IsNullOrEmpty(_sUserName) && !string.IsNullOrEmpty(_sPass))
            {
                string sUser = _sUserName;
                string sPass = _sPass;

                string un = sUser;
                string pw = sPass;
                string phone = MobiNumber;

                string content = string.Format("【{0}】{1}",   "币趣"  , WebUtility.UrlEncode(Msg.Trim()));// string.Concat("【ebsite】", WebUtility.UrlDecode(Msg.Trim()));


                string postJsonTpl = "\"account\":\"{0}\",\"password\":\"{1}\",\"phone\":\"{2}\",\"report\":\"false\",\"msg\":\"{3}\"";
                string jsonBody = string.Format(postJsonTpl, un, pw, phone, content);
                string result = doPostMethodToObj("http://vsms.253.com/msg/send/json", "{" + jsonBody + "}");
                 
            }
            else
            {
                
                throw new Exception("用户名称与密码没有配置正确");
            }

        }
        private string doPostMethodToObj(string url, string jsonBody)
        {
            string result = String.Empty;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            // Create NetworkCredential Object 
            NetworkCredential admin_auth = new NetworkCredential("username", "password");


            // Set your HTTP credentials in your request header
            httpWebRequest.Credentials = admin_auth;

            // callback for handling server certificates
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonBody);
                streamWriter.Flush();
                streamWriter.Close();
                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            return result;
        }
    }
}
