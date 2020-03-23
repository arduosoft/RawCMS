using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Interfaces;
using RawCMS.Library.UI;
using RawCMS.Plugins.LogCollecting.Config;

namespace RawCMS.Plugins.LogCollecting
{
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
