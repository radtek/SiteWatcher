using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteWatcher.Entity
{
    public class ListItem
    {
        public ListItem()
        {
            
        }
        public ListItem(string _Text,string _Value)
        {
            Text = _Text;
            Value = _Value;
        }
        public string Value { get; set; }
        public string Text { get; set; }
    }
}
