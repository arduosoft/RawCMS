using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.ApiGateway.Classes
{
    public class ApiGatewayConfig
    {
        public ApiGatewayConfig()
        {
            EnableApiGateway = false;
        }

        public bool EnableApiGateway { get; set; }
    }
}
