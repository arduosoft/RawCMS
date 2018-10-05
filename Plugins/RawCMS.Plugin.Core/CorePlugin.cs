using RawCMS.Library.Core.Extension;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RawCMS.Library.Core;
using RawCMS.Library.DataModel;
using Microsoft.Extensions.Options;
using RawCMS.Library.Service;

namespace RawCMS.Plugins.Core
{



    public class CorePlugin : RawCMS.Library.Core.Extension.Plugin
    {
        public override string Name => "Core";

        public override string Description => "Add core CMS capabilities";

        public override void Init()
        {
            this.Logger.LogInformation("Core plugin loaded");
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.Configure<MongoSettings>(x =>
            {
                x = mongoSettings;
            });

            IOptions<MongoSettings> settingsOptions = Options.Create<MongoSettings>(mongoSettings);
            MongoService mongoService = new MongoService(settingsOptions);
            CRUDService crudService = new CRUDService(mongoService, settingsOptions);

            this.Engine.Service = crudService;

            services.AddSingleton<MongoService>(mongoService);
            services.AddSingleton<CRUDService>(crudService);
            services.AddSingleton<AppEngine>(this.Engine);


        }

        public override void Configure(IApplicationBuilder app, AppEngine appEngine)
        {
            base.Configure(app, appEngine);


        }

        MongoSettings mongoSettings = new MongoSettings();
        public override void Setup(IConfigurationRoot configuration)
        {

            configuration.GetSection("MongoSettings").Bind(mongoSettings);
        }
    }
}