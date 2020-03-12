using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Interfaces;
using RawCMS.Plugins.Core.Configuration;
using RawCMS.Plugins.Core.Middlewares;

namespace RawCMS.Plugins.Core
{
    public class UIPlugin : RawCMS.Library.Core.Extension.Plugin, IConfigurablePlugin<UIPluginConfig>
    {
        public override string Name => "UIPlugin";

        public override string Description => "The UI CORE";

        protected  UIPluginConfig config;
        public UIPlugin(AppEngine appEngine, UIPluginConfig config, ILogger logger):base(appEngine,logger)
        {
            this.config = config;
        }

        public override void Configure(IApplicationBuilder app)
        {
            Logger.LogInformation("Loading plugin UI");
            foreach (var plugin in this.Engine.Plugins)
            {
                var assembly = plugin.GetType().Assembly.GetName().Name;
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "Plugins", assembly, "UI");
                if (Directory.Exists(folder))
                {
                    Logger.LogInformation($".. UI found for {plugin.Name} with slug {plugin.Slug}");
                    app.UseStaticFiles(new StaticFileOptions()
                    {
                        FileProvider = new PhysicalFileProvider(folder),
                        RequestPath = "/app/modules/" + plugin.Slug,
                        OnPrepareResponse = ctx =>
                        {
                            ctx.Context.Response.Headers.Add("Cache", new Microsoft.Extensions.Primitives.StringValues("no-cache"));
                        }
                    });
                }
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Plugins", "RawCMS.Plugins.Core", "UICore")),
                RequestPath = "/app"

            }); ;
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
    }
}
