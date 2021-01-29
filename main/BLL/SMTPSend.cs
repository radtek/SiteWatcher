using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Windows.Forms;
using SiteWatcher.Entity;
using System.Net;

namespace SiteWatcher.BLL
{
    public class SMTPSend
    {


        public static void SendEmails(string sTitle,string Body,string ToEmail)
        {
            EmailModel em = new EmailModel();
            em.Title = sTitle;
            em.Body = Body;
            em.EnableSsl = Core.Configs.ConfigsControl.Instance.SendEmailIsOpenSSL;
            em.From = Core.Configs.ConfigsControl.Instance.SendEmail;
            em.Port = Core.Configs.ConfigsControl.Instance.SendEmailPort;
            em.FromPass = Core.Configs.ConfigsControl.Instance.SendEmailPass;
            em.Smtp = Core.Configs.ConfigsControl.Instance.SmtpServer;
            em.To = ToEmail;

            SMTPSend emailSend = new SMTPSend();
            emailSend.SMTP_Send(em);

            //string[] sEmails = Configs.ConfigsControl.Instance.GetReciveEmails();
            //SMTPSend emailSend = new SMTPSend();
            //foreach (string email in sEmails)
            //{
            //    EmailModel em = new EmailModel();
            //    em.Title = sTitle;
            //    em.Body = Body;
            //    em.EnableSsl = Configs.ConfigsControl.Instance.SendEmailIsOpenSSL;
            //    em.From = Configs.ConfigsControl.Instance.SendEmail;
            //    em.Port = Configs.ConfigsControl.Instance.SendEmailPort;
            //    em.FromPass = Configs.ConfigsControl.Instance.SendEmailPass;
            //    em.Smtp = Configs.ConfigsControl.Instance.SmtpServer;
            //    em.To = email;

            //    emailSend.SMTP_Send(em);
            //}
        }

        private MailMessage mailMessage = new MailMessage();
        private SmtpClient smtpClient = new SmtpClient();

        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        public bool SMTP_Send(EmailModel model)
        {
            mailMessage.To.Add(new System.Net.Mail.MailAddress(model.To)); //收件人地址
            mailMessage.From = new System.Net.Mail.MailAddress(model.From); //发件人地址
            mailMessage.Subject = model.Title;
            mailMessage.Body = model.Body;
            mailMessage.IsBodyHtml = model.IsBodyHtml;
            mailMessage.BodyEncoding = model.MailEncoding;//System.Text.Encoding.UTF8;
            mailMessage.Priority = model.Priority;//System.Net.Mail.MailPriority.Normal;          
            //smtpClient.EnableSsl = true;
            if (model.Port > 0)
                smtpClient.Port = model.Port;
            //smtpClient = new SmtpClient();
            smtpClient.Credentials = new System.Net.NetworkCredential
            (mailMessage.From.Address, model.FromPass);//设置发件人身份的票据 
            smtpClient.EnableSsl = model.EnableSsl;
            smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtpClient.Host = model.Smtp;//"smtp." + mailMessage.From.Host;
            smtpClient.Send(mailMessage);
            return true;
        }

        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="Path"></param>
        public void SMTP_Attachments(string Path)
        {
            string[] path = Path.Split(',');
            Attachment data;
            ContentDisposition disposition;
            for (int i = 0; i < path.Length; i++)
            {
                data = new Attachment(path[i], System.Net.Mime.MediaTypeNames.Application.Octet);//实例化附件 
                disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(path[i]);//获取附件的创建日期 
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(path[i]);// 获取附件的修改日期 
                disposition.ReadDate = System.IO.File.GetLastAccessTime(path[i]);//获取附件的读取日期 
                mailMessage.Attachments.Add(data);//添加到附件中 
            }
        }

    }
}
