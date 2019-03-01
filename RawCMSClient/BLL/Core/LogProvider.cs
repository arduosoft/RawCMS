using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace RawCMSClient.BLL.Core
{
    public class LogProvider
    {
        private static ServiceProvider _provider = null;
        
        public static Runner Runner { get
            {
                if (_provider == null)
                {
                    _provider = new ServiceCollection()
                         .AddLogging(builder =>
                         {
                             builder.SetMinimumLevel(LogLevel.Trace);
                             builder.AddNLog(new NLogProviderOptions
                             {
                                 CaptureMessageTemplates = true,
                                 CaptureMessageProperties = true
                             });
                         })
                         .AddTransient<Runner>()
                         .BuildServiceProvider();
                }

                return _provider.GetRequiredService<Runner>();
            }
        }

    }

}
