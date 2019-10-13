using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RawCMS.Plugins.ApiGateway.Classes;
using RawCMS.Plugins.ApiGateway.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RawCMS.Plugins.ApiGateway.Middleware
{
    public class BalancerMiddleware : GatewayMiddleware
    {
        public BalancerMiddleware(RequestDelegate requestDelegate, ILogger logger, ApiGatewayConfig config) 
            : base(requestDelegate, logger, config)
        {
        }

        public override string Name => "BalancerMiddleware";

        public override string Description => "Enable Balancer capability";

        public override async Task InvokeAsync(HttpContext context)
        {

            logger.LogDebug("Invoke BalancerMiddleware start");
            if(!pluginConfig.EnableApiGateway)
            {

            }else
            {

            }
            await next(context);
        }
    }
}
