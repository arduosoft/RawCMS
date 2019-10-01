using Microsoft.Extensions.Logging;

namespace RawCMS.Library.Core.Helpers
{
    public class ApplicationLogger
    {
        private static ILoggerFactory _loggerFactory;

        public static ILoggerFactory LoggerFactory { get => _loggerFactory; set => _loggerFactory = value; }

        public static void SetLogFactory(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
        }

        public static ILogger CreateLogger<T>()
        {
            return LoggerFactory.CreateLogger<T>();
        }

        public static ILogger CreateLogger(string name)
        {
            return LoggerFactory.CreateLogger(name);
        }

        public static NLog.Logger CreateRawLogger(string env)
        {
            string path = GetConfigPath(env);
            return NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();
        }

        public static string GetConfigPath(string env)
        {
            if (env != null)
            {
                env = "." + env;
            }
            string path = $"./conf/NLog{env}.config";
            return path;
        }
    }
}