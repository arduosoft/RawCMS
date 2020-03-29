using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawCMS.Library.BackgroundJobs;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Extension;
using RawCMS.Library.Core.Interfaces;
using RawCMS.Library.DataModel;
using RawCMS.Library.UI;
using RawCMS.Plugins.Core.Configuration;

namespace RawCMS.Plugins.Core
{
    public class BackgoundJobPlugin : Plugin, IConfigurablePlugin<BackgroundJobSettings>
    {
        public override string Name => "BackgoundJobPlugin";

        public override string Description => "BackgoundJobPlugin";
        public IServiceCollection services;

        public override string Slug => "backgroundjobs";

        protected BackgroundJobService jobServices;
        public BackgoundJobPlugin(AppEngine appEngine, ILogger logger) : base(appEngine, logger)
        {
            Logger.LogInformation("BackgoundJobPlugin plugin loaded");
        }

        public override void Configure(IApplicationBuilder app)
        {
            this.jobServices.Configure(app);
            this.jobServices.Init();
        }

        public override void ConfigureMvc(IMvcBuilder builder)
        {
            
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            
            this.services = services;
            var sp=services.BuildServiceProvider();
            var setting = sp.GetService<MongoSettings>();
            var loggerFactory = sp.GetService<ILoggerFactory>();
            jobServices = new BackgroundJobService(setting, loggerFactory.CreateLogger<BackgroundJobService>(), this.Engine);
            this.services.AddSingleton<BackgroundJobService>(jobServices);
            this.jobServices.Configure(this.services);
        }

        public override void Setup(IConfigurationRoot configuration)
        {
            
        }

        public override UIMetadata GetUIMetadata()
        {
            return base.GetUIMetadata();
        }
    }
}
