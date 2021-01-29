using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver.Builders;
using SiteWatcher.Core.STSdbUtils;

namespace SiteWatcher.BLL
{
    /// <summary>
    /// 网站更新页面URL
    /// </summary>
    public class UpdateLink : BllBase<Entity.UpdateLink, Guid>//Base<Entity.UpdateLink>
    {
        static public readonly UpdateLink Instance = new UpdateLink();
        override protected string TableName
        {
            get { return "UpdateLink"; }
        }
        override public Guid Add(Entity.UpdateLink md)
        {
            md.Id = Guid.NewGuid();
            InsertOne(md);
            return md.Id;
        }
       
    }
}
