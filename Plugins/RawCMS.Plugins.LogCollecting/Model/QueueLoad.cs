using System;
using System.Collections.Generic;
using System.Text;
namespace RawCMS.Plugins.LogCollecting.Models
{
    public class QueueLoad
    {
        public DateTime Time { get; set; }
        public long QueueSize { get; set; }
        public long MaxSize { get; set; }
    }
}
