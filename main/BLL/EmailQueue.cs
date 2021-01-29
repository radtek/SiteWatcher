using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SiteWatcher.Core.STSdbUtils;
using XS.Core;

namespace SiteWatcher.BLL
{
    public class EmailQueue : BllBase<Entity.EmailQueue, Guid>//Base<Entity.EmailQueue>
    {
        static public readonly EmailQueue Instance = new EmailQueue();
        //override public string TableName
        //{
        //    get { return "EmailQueue"; }
        //}

        override protected string TableName
        {
            get { return "EmailQueue"; }
        }

        override public Guid Add(Entity.EmailQueue md )
        {
            md.Id = Guid.NewGuid();
            InsertOne(md);
            return md.Id;
        }

        public void AddEmailToDB(string Title, string Body)
        {
            AddEmailToDB(Title, Body, true);
        }

        public void AddEmailToDB(string Title, string Body,bool IsAddToMobile)
        {
            string[] ReciveEmails = Core.Configs.ConfigsControl.Instance.GetReciveEmails();
            if (!Equals(ReciveEmails, null))
            {
                foreach (string email in ReciveEmails)
                {
                    Entity.EmailQueue md = new Entity.EmailQueue();
                    md.ToEmail = email;
                    md.AddDateTime = DateTime.Now;
                    md.EmailTilte = Title;
                    md.EmailBody = Body;
                    md.MsgType = 0;//email
                    Add(md);
                }
            }
           

            if (IsAddToMobile)
            {
                string[] ReciveMobiles = Core.Configs.ConfigsControl.Instance.GetReciveMobiles();
                if (!Equals(ReciveMobiles, null))
                {
                    foreach (string mobile in ReciveMobiles)
                    {
                        Entity.EmailQueue md = new Entity.EmailQueue();
                        md.ToEmail = mobile;
                        md.AddDateTime = DateTime.Now;
                        md.EmailTilte = Title;
                        md.EmailBody = Body;
                        md.MsgType = 1;//手机
                        Add(md);
                    }
                }
                
            }
            
        }

        public IEnumerable<Entity.EmailQueue> GetNoList()
        {
            //SortByDocument sort = new SortByDocument { { "AddDateTime", -1} };
            //return base.GetAll(Query.EQ("IsSend", 0), sort, 10, new[] { "ToEmail", "EmailTilte", "EmailBody", "AddDateTime", "MsgType" });

            List< Entity.EmailQueue > lst =  base.GetList();
             
            return lst.Where(d => d.IsSend == 0);

        }
        public void SendEmail()
        {
            IEnumerable<Entity.EmailQueue> lst = GetNoList();
            foreach (Entity.EmailQueue queue in lst)
            {
                queue.IsSend = 2;//设置为发送中
                base.UpdateOne(queue);//更新数据库，防止下次再查询出来
                Thread.Sleep(1000);
                if (!string.IsNullOrEmpty(queue.ToEmail))
                {
                    Amib.Threading.IWorkItemResult wir = ThreadPoolManager.Instance.QueueWorkItem(new Amib.Threading.WorkItemCallback(SendOneEmail), queue);
                     
                }
            }

        }

        private object SendOneEmail(object sender)
        {
            Entity.EmailQueue md = sender as Entity.EmailQueue;

            if (!Equals(md, null))
            {
                if (md.MsgType==0)
                {
                    BLL.SMTPSend.SendEmails(md.EmailTilte, md.EmailBody, md.ToEmail);
                }
                else
                {
                    //BLL.SMSSend.SendMessage(md.EmailBody, md.ToEmail);
                    Core.Utils.SendMobileMsg(md.EmailBody, md.ToEmail);
                }
                md.IsSend = 1;
                base.UpdateOne(md);
            }
            return 1;
        }

    }
}
