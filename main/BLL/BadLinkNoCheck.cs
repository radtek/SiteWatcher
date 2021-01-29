using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver.Builders;
using SiteWatcher.Core.STSdbUtils;

namespace SiteWatcher.BLL
{
    public class BadLinkNoCheck : BllBase<Entity.BadLinkNoCheck, Guid>// Base<Entity.BadLink>
    {
        static public readonly BadLinkNoCheck Instance = new BadLinkNoCheck();
        override protected string TableName
        {
            get { return "BadLinkNoCheck"; }
        }

        public void Del(string sUrl)
        {
           
        }

        override public Guid Add(Entity.BadLinkNoCheck md)
        {
            md.Id = Guid.NewGuid();
            InsertOne(md);
            return md.Id;
        }
    }
}
