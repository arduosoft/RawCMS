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

namespace RawCMS.Library.BackgroundJobs
{

    

    public class DependencyJobActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public DependencyJobActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type jobType)
        {
            //return _serviceProvider.BuildServiceProvider().GetService(jobType);
            if (jobType.FullName.Contains("PingJob2"))
            {
                Console.WriteLine("LOG2");
            }
            
            var implementation = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService(jobType);
            return implementation;
        }

        public override JobActivatorScope BeginScope(PerformContext context)
        {
            if (context.BackgroundJob.Job.Type.FullName.Contains("PingJob2"))
            {
                Console.WriteLine("LOG2");
            }

            if (context.Job.Type.FullName.Contains("PingJob2"))
            {
                Console.WriteLine("LOG2");
            }
            var job = context.BackgroundJob.Job;
            
            var x= base.BeginScope(context);
            return x;
        }

        public override JobActivatorScope BeginScope(JobActivatorContext context)
        {
            if (context.BackgroundJob.Job.Type.FullName.Contains("PingJob2"))
            {
                Console.WriteLine("LOG2");
            }           

            var x=base.BeginScope(context);
            return x;
        }

        public override JobActivatorScope BeginScope()
        {
            return base.BeginScope();
        }

    }

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
           // this.reflectionManager = reflectionManager;
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

            services.AddSingleton<DependencyJobActivator>();
            var dep = services.BuildServiceProvider().GetService<DependencyJobActivator>();

            services.AddHangfire(c => {
                c.UseMemoryStorage();
                c.UseColouredConsoleLogProvider();
                c.UseTypeResolver((typeName) =>
                {
                    var type= jobs.FirstOrDefault(x => typeName.Contains(x.GetType().FullName));
                    if (type != null)
                    {
                        return type.GetType();
                    }
                    //return this.reflectionManager.GetTypeByName(typeName, true);
                    var t= Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => typeName.Contains(x.GetType().FullName));
                    return typeof(JObject);
                });
            });

            //// Add the processing server as IHostedService
            //services.AddHangfireServer(x =>
            //{
            //    x.Activator = dep;


            //});

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

            app.UseHangfireServer(new BackgroundJobServerOptions()
            {
                Activator = new DependencyJobActivator(app.ApplicationServices)
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

            jobs = this.engine.Lambdas
           .Where(x => x is BackgroundJobInstance)
           .Cast<BackgroundJobInstance>().ToList();



            var client = new BackgroundJobClient();
            foreach (var job in jobs)
            {
                job.Execute(null);
                var info = new JObject();
               
                 RecurringJob.AddOrUpdate(job.Name, () => job.Execute(null), job.CronExpression);
             
            }
        }

        public async Task StartBouncePolling(BackgroundJobInstance job)
        {
        }
        public void InvokeGeneric(Type scenarioType,string jobname,string cron)
        {
            MethodInfo addOrUpdate = typeof(RecurringJob).GetMethods().Where(x => x.Name == "AddOrUpdate" && x.IsGenericMethod && x.IsGenericMethodDefinition).Select(m => new
            {
                Method = m,
                Params = m.GetParameters(),
                Args = m.GetGenericArguments()
            })
                 .Where(x => x.Params.Length == 5
                             && x.Params[0].ParameterType == typeof(string)
                             && x.Params[2].ParameterType == typeof(string)
                             && x.Params[3].ParameterType == typeof(TimeZoneInfo)
                             && x.Params[4].ParameterType == typeof(string)
                             )
                 .Select(x => x.Method).FirstOrDefault();

            MethodInfo generic = addOrUpdate.MakeGenericMethod(scenarioType);
            ParameterExpression param = Expression.Parameter(scenarioType, "x");
            ConstantExpression someValue = Expression.Constant(jobname, typeof(string));
            MethodCallExpression methodCall = Expression.Call(param, scenarioType.GetMethod("Execute", new Type[] { typeof(string) }), someValue);
            LambdaExpression expre = Expression.Lambda(methodCall, new ParameterExpression[] { param });
            generic.Invoke(null, new object[] { jobname, expre, cron, TimeZoneInfo.Utc, null });
        }

        public void GenericExecute(BackgroundJobInstance job)
        {
            job.Execute(null);
            
        }

    }
}
