using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core.Helpers;
using RawCMS.Plugins.ApiGateway.Classes.Policy;
using RawCMS.Plugins.ApiGateway.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RawCMS.Plugins.ApiGateway.Classes
{
    public class BalancerDispatcher
    {
        private List<BalancerPolicy> policies { get; set; }
        public BalancerDispatcher(IEnumerable<BalancerPolicy> policies)
        {
            this.policies = policies.ToList();
        }

        public BalancerPolicy GetActiveBalancerPolicy(HttpContext context)
        {
            return policies.Where(x => x.IsEnable(context)).FirstOrDefault();
        }
    }
}
