using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Core.Extension
{
    public class MiddlewarePriorityAttribute :Attribute
    {
        public int Order { get; set; }
    }
}
