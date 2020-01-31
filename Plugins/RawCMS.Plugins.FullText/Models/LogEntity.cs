using System;
using System.Collections.Generic;
using System.Text;
namespace RawCMS.Plugins.FullText.Models
{
    public class LogEntity 
    {
        public DateTime CreateDate { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public Guid Id { get; set; }
        public long ApplicationId { get; set; }

    }
}
