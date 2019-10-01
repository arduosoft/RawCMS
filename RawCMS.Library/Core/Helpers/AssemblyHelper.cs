using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RawCMS.Library.Core.Helpers
{
    public class AssemblyHelper
    {
        public static List<Assembly> GetAllAssembly()
        {
            List<Assembly> allAssembly = new List<Assembly>();
            allAssembly.AddRange(AppDomain.CurrentDomain.GetAssemblies());
            allAssembly.Add(Assembly.GetExecutingAssembly());
            allAssembly.Add(Assembly.GetEntryAssembly());
            return allAssembly;
        }
    }
}