using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Core.Extension
{
    public class PluginManager
    {
        private static PluginManager pm;
        internal static PluginManager Current
        {
            get { return pm ?? (pm = new PluginManager()); }

        }

        public PluginManager()
        {

        }
    }

}
