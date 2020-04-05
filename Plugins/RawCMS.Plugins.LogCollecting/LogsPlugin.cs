using System;
using System.Collections.Generic;
using System.Text;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nest;
using Nest.JsonNetSerializer;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Attributes;
using RawCMS.Library.Core.Interfaces;
using RawCMS.Library.UI;
using RawCMS.Plugins.FullText.Core;
using RawCMS.Plugins.LogCollecting.Config;
using RawCMS.Plugins.LogCollecting.Services;

namespace RawCMS.Plugins.LogCollecting
{
    [PluginInfo(2)]
    public class LogsPlugin : Library.Core.Extension.Plugin, IConfigurablePlugin<LogsPluginConfig>
    {
        public override string Name => "LogsCollecting";

        public override string Description => "";

        public override string Slug => "logs";

        public LogsPlugin(AppEngine appEngine, ILogger<LogsPlugin> logger) : base(appEngine, logger)
        { }

        public override void Configure(IApplicationBuilder app)
        {
            
        }

        public override void ConfigureMvc(IMvcBuilder builder)
        {
            
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var fs = sp.GetService<FullText.Core.ElasticFullTextService>();
            var fs2 = sp.GetService<FullText.Core.FullTextService>();
            services.AddSingleton<LogService, LogService>();


           
               // RegisterElastiServices(services);

           
        }

        private void RegisterElastiServices(IServiceCollection services)
        {
            var pool = new SingleNodeConnectionPool(new Uri(""));
            var connection = new HttpConnection();
            var connectionSettings =
            new ConnectionSettings(pool, connection, (serializer, settings) =>
            {
                return JsonNetSerializer.Default(serializer, settings);
            })
            // new ConnectionSettings(pool, connection)
            .DisableAutomaticProxyDetection()
            .EnableHttpCompression()
            .DisableDirectStreaming()
            .PrettyJson()
            .RequestTimeout(TimeSpan.FromMinutes(2));

            services.AddSingleton<ElasticClient>(new ElasticClient(connectionSettings));
            services.AddSingleton<FullTextService, ElasticFullTextService>();
            services.AddSingleton<FullTextUtilityService, FullTextUtilityService>();
        }


public override void Setup(IConfigurationRoot configuration)
        {
            
        }
        public override UIMetadata GetUIMetadata()
        {
            return new UIMetadata();
        }

    }
}
