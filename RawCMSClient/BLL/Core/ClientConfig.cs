

using Microsoft.Extensions.Configuration;
using System.IO;

namespace RawCMSClient.BLL.Core
{
    public class ClientConfig
    {
        private static IConfigurationBuilder _builder = null;
        private static IConfigurationRoot _configuration = null;

        public static IConfigurationRoot Config { get
            {
             
                if (_configuration == null)
                {
                    _builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                    _configuration = _builder.Build();
                }
                return _configuration;

             
            }
            set { }
            }
        
     
        public static T GetValue<T>(string key)
        {
            return Config.GetValue<T>(key);

        }

    }
}
