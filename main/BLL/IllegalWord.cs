using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver.Builders;
using SiteWatcher.Core.STSdbUtils;

namespace SiteWatcher.BLL
{
    public class IllegalWord : BllBase<Entity.IllegalWord, Guid>// Base<Entity.IllegalWord>
    {
        static public readonly IllegalWord Instance = new IllegalWord();
        override protected string TableName
        {
            get { return "IllegalWord"; }
        }
        override public Guid Add(Entity.IllegalWord md)
        {
            md.Id = Guid.NewGuid();
            InsertOne(md);
            return md.Id;
        }
       
    }
}
