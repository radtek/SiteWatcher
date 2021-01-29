using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver.Builders;

namespace SiteWatcher.BLL
{
    public class IISLog : MongDbBase<Entity.IISLog>
    {
        static public readonly IISLog Instance = new IISLog();
        override public string TableName
        {
            get { return "IISLog"; }
        }

       
    }
}
