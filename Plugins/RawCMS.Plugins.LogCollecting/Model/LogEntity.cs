using System;

namespace RawCMS.Plugins.LogCollecting.Models
{
    public enum LogEntitySeverity
    {
        ALL = 0,
        TRACE = 1,
        DEBUG = 2,
        INFO = 3,
        WARN = 4,
        ERROR = 5,
        FATAL = 6
    }

    public class LogEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public LogEntitySeverity Severity { get; set; }
        public string ApplicationId { get; set; }
    }
}