using System;
using System.Collections.Generic;
using System.Text;
using Hangfire;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RawCMS.Library.BackgroundJobs;
using RawCMS.Library.Schema;
using RawCMS.Library.Service;
using RawCMS.Plugins.LogCollecting.Services;

namespace RawCMS.Plugins.LogCollecting.Jobs

{ 
     public class PingJob2 : BackgroundJobInstance
    {
        public override string CronExpression => Hangfire.Cron.Minutely();

        public override string Name => "Ping Every3 minute";

        public override string Description => "Ping Every minute";

        protected ILogger logger;
        public PingJob2(ILogger logger)
        {
            this.logger = logger;
        }

        public override void Execute(JObject data)
        {
            this.logger.LogInformation($"Job triggered,2 with data {data}");
        }
    }

public class LogCollectingIngestor : BackgroundJobInstance
    {
        public override string CronExpression => Cron.Minutely();

        public override string Name => "LogCollectingIngestor";

        public override string Description => "LogCollectingIngestor";

        protected LogService logService;

        protected CRUDService cRUDService;
        public LogCollectingIngestor(LogService logService, CRUDService cRUDService)
        {
            this.logService = logService;
            this.cRUDService = cRUDService;
        }
        public override void Execute(JObject data)
        {
            this.logService.PersistLog();           
        }
    }
}
