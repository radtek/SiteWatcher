using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver.Builders;
using SiteWatcher.Core.STSdbUtils;

namespace SiteWatcher.BLL
{
    public class BadLink : BllBase<Entity.BadLink, Guid>// Base<Entity.BadLink>
    {
        static public readonly BadLink Instance = new BadLink();
        override protected string TableName
        {
            get { return "BadLink"; }
        }

        override public Guid Add(Entity.BadLink md)
        {
            md.Id = Guid.NewGuid();
            InsertOne(md);
            return md.Id;
        }

        public bool IsHaveUrl(string sUrl)
        {
            List<Entity.BadLink> lst = base.GetList();
           
           return lst.Exists(d => d.PUrl.Equals(sUrl));

        }
    }
}
