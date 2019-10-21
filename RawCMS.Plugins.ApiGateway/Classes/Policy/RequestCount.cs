using Microsoft.AspNetCore.Http;
using RawCMS.Plugins.ApiGateway.Classes.Settings;
using RawCMS.Plugins.ApiGateway.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawCMS.Plugins.ApiGateway.Classes.Policy
{
    public class RequestCount : BalancerPolicy
    {
        public RequestCount(ApiGatewayConfig config) : base(config)
        {
        }

        public override string Name => "RequestCount";

        public async override Task<object> Execute(BalancerData balancerData, HttpContext context, RawHandler handler)
        {
            var vhost = context.Items["bal-vhost"] as BalancerOptions;
            var key = balancerData.Scores.OrderByDescending(x => x.Value).FirstOrDefault().Key;
            var node = vhost.Nodes[key];
            await handler.HandleRequest(context, node);
            return node;
        }
    }
}
