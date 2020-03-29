using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Core.Attributes
{
    public class PluginInfoAttribute: Attribute
    {
        public int Order { get; set; }
        public PluginInfoAttribute(int order)
        {
            this.Order = order  ;
        }
    }
}
