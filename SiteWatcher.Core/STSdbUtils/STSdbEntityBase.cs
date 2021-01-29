using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteWatcher.Core.STSdbUtils
{
    abstract public class STSdbEntityBase<T>
    {
        public T Id { get; set; }
    }
}
