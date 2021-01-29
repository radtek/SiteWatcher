using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver.Builders;
using SiteWatcher.Core.STSdbUtils;

namespace SiteWatcher.BLL
{
    /// <summary>
    /// 网站异常
    /// </summary>
    public class SiteErr : BllBase<Entity.SiteErr, Guid>//Base<Entity.SiteErr>
    {
        static public readonly SiteErr Instance = new SiteErr();
        override protected string TableName
        {
            get { return "SiteErr"; }
        }

        override public Guid Add(Entity.SiteErr md)
        {
            md.Id = Guid.NewGuid();
            InsertOne(md);
            return md.Id;
        }
    }
}
