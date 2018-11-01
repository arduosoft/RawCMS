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
using RawCMS.Library.Core.Interfaces;
using RawCMS.Library.Core.Configuration;
using Newtonsoft.Json.Linq;

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
            services.AddHttpContextAccessor();

            crudService.EnsureCollection("_configuration");


            this.Engine.Plugins.ForEach(x => SetConfiguration(x,crudService));          


        }

        private void SetConfiguration(Plugin plugin, CRUDService crudService)
        {
            var confitf = plugin.GetType().GetInterface("IConfigurablePlugin`1");
            if (confitf != null)
            {
                var confType=confitf.GetGenericArguments()[0];
                var pluginType = plugin.GetType();

                var confItem=crudService.Query("_configuration", new DataQuery()
                {
                    PageNumber = 1,
                    PageSize = 1,
                    RawQuery = @"{""plugin_name"":""" + pluginType.FullName + @"""}"
                });

                JObject confToSave = null;

                if (confItem.TotalCount == 0)
                {
                    confToSave = new JObject();

                    confToSave["plugin_name"] = plugin.GetType().FullName;
                    confToSave["data"] = JToken.FromObject(pluginType.GetMethod("GetDefaultConfig").Invoke(plugin, new object[] { }));
                    crudService.Insert("_configuration", confToSave);
                }
                else
                {
                    confToSave = confItem.Items.First as JObject;
                }

                var objData= confToSave["data"].ToObject(confType);

                pluginType.GetMethod("SetActualConfig").Invoke(plugin, new object[] { objData });

            }
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