using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMSClient.BLL.Core
{
    public class Runner
    {
        private readonly ILogger<Runner> _logger;

        public Runner(ILogger<Runner> logger)
        {
            _logger = logger;
        }

        public void Debug(int eventId,Exception e, string message,object[] args)
        {
            _logger.LogDebug(eventId, e, message, args);
        }

        public void Debug(string message,object[] args)
        {
            _logger.LogDebug(message,args);
        }
        public void Debug(string message)
        {
            _logger.LogDebug(message);
        }
        public void Info(string message,object[] args)
        {
            _logger.LogInformation(message, args);
        }
        public void Info(string message)
        {
            _logger.LogInformation(message);
        }

        public void Warn(string message, object[] args)
        {
            _logger.LogWarning(message, args);
        }
        public void Warn(string message)
        {
            _logger.LogWarning(message);
        }
        public void Error(string message, object[] args)
        {
            _logger.LogError(message, args);
        }
        public void Error(string message)
        {
            _logger.LogError(message);
        }
        public void Error(string message,Exception e)
        {
            _logger.LogError(e, message);
        }

        public void Trace(string message, object[] args)
        {
            _logger.LogTrace(message, args);
        }
        public void Trace(string message)
        {
            _logger.LogTrace(message);
        }
        public void Fatal(string message, object[] args)
        {
            _logger.LogCritical(message, args);
        }
        public void Fatal(string message)
        {
            _logger.LogCritical(message);
        }

    }
}
