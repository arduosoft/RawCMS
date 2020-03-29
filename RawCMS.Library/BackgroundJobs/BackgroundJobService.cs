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

namespace RawCMS.Library.BackgroundJobs
{
    public class BackgroundJobService
    {

        protected MongoSettings settings;
        protected ILogger logger;
        protected AppEngine engine;

        public BackgroundJobService(MongoSettings settings, ILogger logger, AppEngine engine)
        {
            this.settings = settings;
            this.logger = logger;
            this.engine = engine;
        }
        public void Configure(IServiceCollection services)
        {


            //var migrationOptions = new MongoMigrationOptions
            //{
            //    Strategy = MongoMigrationStrategy.Migrate,
            //    BackupStrategy = MongoBackupStrategy.Collections,                
            //};
            //var storageOptions = new MongoStorageOptions
            //{
            //    //TODO: read from config                
            //    MigrationOptions = migrationOptions,
            //};


            //// Add Hangfire services.
            //services.AddHangfire(configuration => configuration.UseMongoStorage(settings.ConnectionString, storageOptions));

            services.AddHangfire(c => c.UseMemoryStorage());

            // Add the processing server as IHostedService
            services.AddHangfireServer();

        
        }


        public void Configure(IApplicationBuilder app)
        {
         

            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                DisplayStorageConnectionString = false,
                Authorization = new[] { new HangfireAuthorizationFilter() { } }
            });

          
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
                var jobs = this.engine.Lambdas
         .Where(x => x is BackgroundJobInstance)
         .Cast<BackgroundJobInstance>().ToList();

            foreach (var job in jobs)
            {
                var info = new JObject();
                //TODO: add a background info class, fill with deatails about job and calling reasong, then convert to json and pass as argument
                RecurringJob.AddOrUpdate(job.Name, () => job.Execute(info), job.CronExpression);
            }
        }

    }
}
