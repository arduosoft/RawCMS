using System;
using System.Collections.Generic;
using System.Text;
namespace RawCMS.Plugins.LogCollecting.Models
{
    public class LogEntity 
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string Severity { get; set; }
        public string ApplicationId { get; set; }

    }
}
