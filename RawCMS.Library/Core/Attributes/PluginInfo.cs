using System;

namespace RawCMS.Library.Core.Attributes
{
    public class PluginInfoAttribute : Attribute
    {
        public int Order { get; set; }

        public PluginInfoAttribute(int order)
        {
            this.Order = order;
        }
    }
}