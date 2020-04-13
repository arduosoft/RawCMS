using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RawCMS.Library.BackgroundJobs;

namespace RawCMS.Lambdas.Background
{
    public class PingJob : BackgroundJobInstance
    {
        public override string CronExpression => Hangfire.Cron.Minutely();

        public override string Name => "Ping Every minute";

        public override string Description => "Ping Every minute";

        protected ILogger logger;

        public PingJob(ILogger logger)
        {
            this.logger = logger;
        }

        public override void Execute(JObject data)
        {
            this.logger.LogInformation($"Job triggered, with data {data}");
        }
    }
}