using System;
using System.Collections.Generic;
using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
//using Hangfire.Mongo;
using RawCMS.Library.Service;
using RawCMS.Library.DataModel;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core;
using Hangfire.MemoryStorage;
using System.Linq;
using Newtonsoft.Json.Linq;
using Hangfire.Server;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;
using Hangfire.AspNetCore;
using RawCMS.Library.Core.Helpers;
using Hangfire.Mongo;

namespace RawCMS.Library.BackgroundJobs
{   

    public class BackgroundJobService
    {

        protected MongoSettings settings;
        protected ILogger logger;
        protected AppEngine engine;
        protected IServiceCollection services;
        protected IApplicationBuilder app;
        protected ReflectionManager reflectionManager;

        private List<BackgroundJobInstance> jobs= new List<BackgroundJobInstance>();
        public BackgroundJobService(MongoSettings settings, ILogger logger, AppEngine engine)
        {
            this.settings = settings;
            this.logger = logger;
            this.engine = engine;
           this.reflectionManager = this.engine.ReflectionManager;
        }
        public void Configure(IServiceCollection services)
        {


            var migrationOptions = new MongoMigrationOptions
            {
                Strategy = MongoMigrationStrategy.Migrate,
                BackupStrategy = MongoBackupStrategy.Collections,
            };
            var storageOptions = new MongoStorageOptions
            {
                //TODO: read from config                
                MigrationOptions = migrationOptions,
            };

            //TODO: make it parametric
            services.AddHangfire(c => {
                //c.UseMemoryStorage();
                c.UseMongoStorage(settings.ConnectionString, storageOptions);
                c.UseColouredConsoleLogProvider();
                c.UseTypeResolver((typeName) =>
                {
                    var type= jobs.FirstOrDefault(x => typeName.Contains(x.GetType().FullName));
                    if (type != null)
                    {
                        return type.GetType();
                    }
                    return this.reflectionManager.GetTypeByName(new List<Assembly>() { Assembly.GetEntryAssembly(), this.GetType().Assembly},typeName, true,true);                   
                });
            });
          

            this.services = services;
        }


        public void Configure(IApplicationBuilder app)
        {
            this.app = app;


            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                DisplayStorageConnectionString = false,
                Authorization = new[] { new HangfireAuthorizationFilter() { } }
            });

            app.UseHangfireServer();
         
        }


        public void RunOnce(string jobName, JObject data)
        {
             var job=this.engine.Lambdas
                .Where(x => x is BackgroundJobInstance)
                .Cast<BackgroundJobInstance>()
                .Where(x => jobName.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
            //TODO: add a background info class, fill with deatails about job and calling reasong, then convert to json and pass as argument
            BackgroundJob.Enqueue<BackgroundJobInstance>(x => x.Execute(data));
        }


        public void Init()
        {

            jobs = this.engine.Lambdas
           .Where(x => x is BackgroundJobInstance)
           .Cast<BackgroundJobInstance>().ToList();



            var client = new BackgroundJobClient();
            foreach (var job in jobs)
            {
                var info = new JObject();               
                 RecurringJob.AddOrUpdate(job.Name, () => job.Execute(null), job.CronExpression);             
            }
        }
        
    }
}
