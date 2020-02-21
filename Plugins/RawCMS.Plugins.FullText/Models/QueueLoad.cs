using System;
using System.Collections.Generic;
using System.Text;
namespace RawCMS.Plugins.FullText.Models
{
    public class QueueLoad
    {
        public DateTime Time { get; set; }
        public int QueueSize { get; set; }
        public int MaxSize { get; set; }
    }
}
