using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace SiteWatcher.Entity
{
    abstract public class Base
    {
        public ObjectId _id { get; set; }
    }
}
