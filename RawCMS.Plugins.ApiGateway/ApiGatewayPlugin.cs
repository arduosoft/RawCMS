using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Extension;
using RawCMS.Library.Core.Helpers;
using RawCMS.Library.Core.Interfaces;
using RawCMS.Plugins.ApiGateway.Classes;
using RawCMS.Plugins.ApiGateway.Classes.Policy;
using RawCMS.Plugins.ApiGateway.Interfaces;

namespace RawCMS.Plugins.ApiGateway
{
    public class ApiGatewayPlugin : Plugin, IConfigurablePlugin<ApiGatewayConfig>
    {
        private AppEngine appEngine { get; set; }
        private ApiGatewayConfig config { get; set; }
        public ApiGatewayPlugin(AppEngine engine, ILogger logger, ApiGatewayConfig gatewayConfig) : base(engine, logger)
        {
            appEngine = engine;
            config = gatewayConfig;
        }

        public override string Name => "ApiGatewayPlugin";

        public override string Description => "Add Api Gateway capability";

        public override void Configure(IApplicationBuilder app)
        {
        }

        public override void ConfigureMvc(IMvcBuilder builder)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.RegisterAllTypes<BalancerPolicy>(Assembly.GetExecutingAssembly(), ServiceLifetime.Singleton);
            services.RegisterAllTypes<RawHandler>(Assembly.GetExecutingAssembly(), ServiceLifetime.Singleton);
            services.AddSingleton<BalancerDispatcher>();
        }

        public override void Setup(IConfigurationRoot configuration)
        {
        }
    }
}
