using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Core.Configuration
{
   public class ConfigurationBase<T>
    {
        public string PluginFullName { get; set; }
        public string Description { get; set; }

        public T Data { get; set; }
    }
}
