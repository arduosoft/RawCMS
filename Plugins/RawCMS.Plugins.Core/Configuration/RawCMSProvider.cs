using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.Core.Configuration
{
    public class RawCMSProvider
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ApiResource { get; set; }

        public string AdminApiKey { get; set; }
        public string ApiKey { get; set; }
    }
}
