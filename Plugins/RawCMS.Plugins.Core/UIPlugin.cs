using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Interfaces;
using RawCMS.Library.Service;
using RawCMS.Library.UI;
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

        public string PluginPath { 
            get 
            {
                return "core";
            } 
        }

        public override void Configure(IApplicationBuilder app)
        {


            //app.UseRewriter(
            //      new RewriteOptions().AddRewrite("^\\/app\\/(.*)\\/$", "app/index.html",true)
            //      );

            Logger.LogInformation("Loading plugin UI");
            foreach (var plugin in this.Engine.Plugins)
            {
                var assembly = plugin.GetType().Assembly.GetName().Name;
                var folder = plugin.GetUIFolder();
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
            var baseFolder = Path.GetDirectoryName(this.Engine.CorePlugin.PluginPath);
            baseFolder = Path.GetDirectoryName(baseFolder);
            baseFolder = Path.GetDirectoryName(baseFolder);
            baseFolder = Path.GetDirectoryName(baseFolder);
            var uiCoreFolder = Path.Combine(baseFolder, "UICore");
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(uiCoreFolder),
                RequestPath = "/app"

            }); ;
        }

        public override void ConfigureMvc(IMvcBuilder builder)
        {
           
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UIService>();
        }

        public override void Setup(IConfigurationRoot configuration)
        {
          
        }


        public override UIMetadata GetUIMetadata()
        {
            var metadata = new UIMetadata();

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type =UIResourceRequirementType.CSS,
                ResourceUrl= "https://fonts.googleapis.com/css?family=Roboto:100,300,400,500,700,900",
                Order=0
            });
            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.CSS,
                ResourceUrl = "https://cdn.jsdelivr.net/npm/@mdi/font@4.x/css/materialdesignicons.min.css",
                Order = 1
            });

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.CSS,
                ResourceUrl = "https://cdn.jsdelivr.net/npm/vuetify@2.x/dist/vuetify.min.css",
                Order = 2
            });
        
            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.CSS,
                ResourceUrl = "https://cdn.jsdelivr.net/npm/epic-spinners@1.1.0/dist/lib/epic-spinners.min.css",
                Order = 3
            });

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.CSS,
                ResourceUrl = "/app/styles.css",
                Order = 4
            });


            //JS

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://cdn.jsdelivr.net/npm/vue@2.x/dist/vue.js",
                Order =1000
            });
            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://unpkg.com/vuex@3.1.1/dist/vuex.js",
                Order = 10001
            });

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://cdn.jsdelivr.net/npm/vuetify@2.x/dist/vuetify.js",
                Order = 1002
            });


            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://unpkg.com/vue-i18n/dist/vue-i18n.js",
                Order = 1003
            });

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://unpkg.com/vue-router/dist/vue-router.js",
                Order = 1004
            });

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://unpkg.com/axios/dist/axios.js",
                Order = 1005
            });

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://cdn.jsdelivr.net/combine/npm/vuelidate@0.7.4/dist/validators.min.js,npm/vuelidate@0.7.4/dist/vuelidate.min.js",
                Order = 1006
            });

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://cdn.jsdelivr.net/npm/epic-spinners@1.1.0/dist/lib/epic-spinners.min.js",
                Order = 1007
            });


            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://cdn.jsdelivr.net/npm/vue-formly@2.5.8/dist/vue-formly.min.js",
                Order = 1008
            });


            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.min.js",
                Order = 1009
            });

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://unpkg.com/vue-chartjs/dist/vue-chartjs.min.js",
                Order = 1010
            });

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://d3js.org/d3-color.v1.min.js",
                Order = 1011
            });
            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://d3js.org/d3-interpolate.v1.min.js",
                Order = 1012
            });

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://unpkg.com/vue-chartjs/dist/vue-chartjs.min.js",
                Order = 1013
            }); 
            
            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://d3js.org/d3-color.v1.min.js",
                Order = 1014
            });

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://d3js.org/d3-interpolate.v1.min.js",
                Order = 1015
            });

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://unpkg.com/vue-monaco@1.1.0/dist/vue-monaco.js",
                Order = 1016
            });

            metadata.Requirements.Add(new Library.UI.UIResourceRequirement()
            {
                Type = UIResourceRequirementType.Javascript,
                ResourceUrl = "https://cdnjs.cloudflare.com/ajax/libs/monaco-editor/0.18.0/min/vs/loader.js",
                Order = 1017
            });

            return metadata;
        }
    }
}
