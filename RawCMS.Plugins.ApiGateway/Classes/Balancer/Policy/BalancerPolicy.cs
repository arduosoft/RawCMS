using Microsoft.AspNetCore.Http;
using RawCMS.Plugins.ApiGateway.Classes;
using RawCMS.Plugins.ApiGateway.Classes.Settings;
using RawCMS.Plugins.ApiGateway.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RawCMS.Plugins.ApiGateway.Classes.Balancer.Policy
{
    public abstract class BalancerPolicy
    {
        public ApiGatewayConfig Config { get; private set; }

        public BalancerPolicy(ApiGatewayConfig config)
        {
            Config = config;
        }

        public abstract string Name { get; }

        public bool IsEnable(HttpContext context)
        {
            var vhost = context.Items["bal-vhost"] as BalancerOption;
            return vhost!= null && string.Compare(vhost.Policy, Name, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public abstract Task<object> Execute(BalancerData balancerData, HttpContext context, RawHandler handler);
    }
}
