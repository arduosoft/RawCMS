using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core.Extension;
using RawCMS.Plugins.ApiGateway.Classes;
using RawCMS.Plugins.ApiGateway.Classes.Handles;
using RawCMS.Plugins.ApiGateway.Classes.Settings;
using RawCMS.Plugins.ApiGateway.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RawCMS.Plugins.ApiGateway.Middleware
{
    [MiddlewarePriority(Order = 1)]
    public class BalancerMiddleware : GatewayMiddleware
    {
        private BalancerDispatcher dispatcher { get; set; }
        private Dictionary<string, BalancerData> data { get; set; }

        public BalancerMiddleware(RequestDelegate requestDelegate, ILogger logger, ApiGatewayConfig config, IEnumerable<RawHandler> handlers,  BalancerDispatcher balancerDispatcher) 
            : base(requestDelegate, logger, config, handlers)
        {
            dispatcher = balancerDispatcher;
            data = new Dictionary<string, BalancerData>();
        }

        public override string Name => "BalancerMiddleware";

        public override string Description => "Enable Balancer capability";

        public override async Task InvokeAsync(HttpContext context)
        {

            logger.LogDebug("Invoke BalancerMiddleware start");
            var host = context.Request.Host.Value;
            var scheme = context.Request.Scheme;
            var port = context.Request.Host.Port;
            var path = context.Request.Path;

            var vhosts = pluginConfig?.Balancer?.Where(x => x.Host.Equals(host, StringComparison.InvariantCultureIgnoreCase) &&
                                         x.Scheme.Equals(scheme, StringComparison.InvariantCultureIgnoreCase) &&
                                         new Regex(x.Path).Match(path).Success &&
                                         x.Port == port && x.Enable).ToList();
            // TODO: get regex that not contains other regex
            var vhost = vhosts?.OrderByDescending(x => x.Path?.Length).FirstOrDefault();
            context.Items["bal-host"] = host;
            context.Items["bal-vhost"] = vhost;
            var policy = dispatcher.GetActiveBalancerPolicy(context);
            if (policy != null)
            {
                BalancerData balancerData = null;
                if (!data.ContainsKey(host))
                {
                    balancerData = data[host] = new BalancerData();
                    for (int i = 0; i < vhost.Nodes.Length; i++)
                    {
                        balancerData.Scores[i] = 0;
                    }
                }
                else
                {
                    balancerData = data[host];
                }

                context.Request.Headers["X-Forwarded-For"] = context.Connection.RemoteIpAddress.ToString();
                context.Request.Headers["X-Forwarded-Proto"] = context.Request.Protocol.ToString();
                int portDest = context.Request.Host.Port ?? (context.Request.IsHttps ? 443 : 80);
                context.Request.Headers["X-Forwarded-Port"] = portDest.ToString();
                var handlerType = context.WebSockets.IsWebSocketRequest ? HandlerProtocolType.Socket : HandlerProtocolType.Http;
                context.Items["bal-destination"] = await policy.Execute(balancerData, context, handlers.First(x => x.HandlerRequestType == handlerType));
            }
            else
            {
                await next(context);
            }
        }
    }
}
