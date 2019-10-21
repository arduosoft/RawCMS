using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core.Extension;
using RawCMS.Plugins.ApiGateway.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RawCMS.Plugins.ApiGateway.Interfaces
{
    public abstract class GatewayMiddleware : Middleware<ApiGatewayConfig>
    {
        internal IEnumerable<RawHandler> handlers { get; set; }

        public GatewayMiddleware(RequestDelegate requestDelegate, ILogger logger, ApiGatewayConfig config, IEnumerable<RawHandler> handlers) :
            base(requestDelegate, logger, config)
        {
            this.handlers = handlers;
        }
    }
}
