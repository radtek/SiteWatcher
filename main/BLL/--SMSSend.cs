//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace SiteWatcher.BLL
//{
//    public class SMSSend
//    {

//        private static string _sUserName="SDK-BBX-010-10167";
//        private static string _sPass = "959865";

//        /// <summary>
//        /// 向某个手机号码发送一条短信
//        /// </summary>
//        /// <param name="Msg">短信内容</param>
//        /// <param name="MobiNumber">手机号码</param>
//        /// <param name="UserName">用户帐号</param>
//        public static void SendMessage(string Msg, string MobiNumber)
//        {
//            //throw new Exception("手机号:" + MobiNumber + " 短信:" + Msg + " 用户名:" + UserName);
//            cn.entinfo.sdk2.WebService msm = new cn.entinfo.sdk2.WebService();
//            if (!string.IsNullOrEmpty(_sUserName) && !string.IsNullOrEmpty(_sPass))
//            {
//                string sUser = _sUserName;
//                string sPass = _sPass;
//                Msg += "【北迈汽配网】";
//                string state = msm.SendSMSEx(sUser, sPass, MobiNumber, Msg, "");
//            }
//            else
//            {

//            }

//        }
//        public static bool ValidateAccount(string username,string password)
//        {
//            cn.entinfo.sdk2.WebService msm = new cn.entinfo.sdk2.WebService();
//            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
//            {
//                string state = msm.GetStatus(username, password);
//                if(state=="2 已用")
//                {
//                    return true;
//                }
//                return false;
//            }
//            return false;
//        }
//    }
//}
