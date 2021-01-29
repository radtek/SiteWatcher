using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver.Builders;
using SiteWatcher.Core.STSdbUtils;

namespace SiteWatcher.BLL
{
    public class ReCreatFile : BllBase<Entity.ReCreatFile, Guid>// Base<Entity.BadLink>
    {
        static public readonly ReCreatFile Instance = new ReCreatFile();
        override protected string TableName
        {
            get { return "ReCreatFile"; }
        }

        override public Guid Add(Entity.ReCreatFile md)
        {
            md.Id = Guid.NewGuid();
            InsertOne(md);
            return md.Id;
        }

        public bool IsHaveUrl(string sFilePath)
        {
            List<Entity.ReCreatFile> lst = base.GetList();
           
           return lst.Exists(d => d.FilePath.Equals(sFilePath));

        }
    }
}
