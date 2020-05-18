using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.LogCollecting.Model
{
    public class LogStatistic
    {
        public string ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public int Count { get; set; }
        public DateTime Time { get; set; }
    }
}
