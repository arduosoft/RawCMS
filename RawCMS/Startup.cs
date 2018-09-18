using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Service;
using RawCMS.Library.DataModel;
using NLog.Web;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using RawCMS.Library.Core;
using RawCMS.Plugins.Auth;

namespace RawCMS
{
    public class Startup
    {
        AuthPlugin dd = new AuthPlugin();
        private ILoggerFactory loggerFactory;
        private ILogger logger;
        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
            this.logger=loggerFactory.CreateLogger(typeof(Startup));


            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        AppEngine appEngine;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           // AppEngine appEngine = new AppEngine();

            services.Configure<MongoSettings>(x =>
            {
                Configuration.GetSection("MongoSettings").Bind(x);
            });

            

            //Move to base plugin!
            MongoSettings settings=new MongoSettings();
            Configuration.GetSection("MongoSettings").Bind(settings);
            IOptions<MongoSettings> settingsOptions = Options.Create<MongoSettings>(settings);
            MongoService mongoService = new MongoService(settingsOptions);
            CRUDService crudService = new CRUDService(mongoService, settingsOptions);
             appEngine = new AppEngine(loggerFactory, crudService);

          

            services.AddSingleton<MongoService>(mongoService);
            services.AddSingleton<CRUDService>(crudService);
            services.AddSingleton<AppEngine>(appEngine);

            


            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Web API", Version = "v1" });
                //x.IncludeXmlComments(AppContext.BaseDirectory + "YourProject.Api.xml");
                c.IgnoreObsoleteProperties();
                c.IgnoreObsoleteActions();
                c.DescribeAllEnumsAsStrings();

            });

            //Invoke appEngine

            appEngine.Plugins.OrderBy(x => x.Priority).ToList().ForEach(x =>
            {
                x.ConfigureServices(services);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
           // loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();
            env.ConfigureNLog(".\\conf\\nlog.config");
           
           

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }




            appEngine.Plugins.OrderBy(x => x.Priority).ToList().ForEach(x =>
            {
                x.Configure(app, appEngine);
            });

            app.UseMvc();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{collection?}/{id?}");
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseStaticFiles();


            app.UseWelcomePage();


        }
    }
}
