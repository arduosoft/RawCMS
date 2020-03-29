using System;
using System.Collections.Generic;
using System.Text;
using Hangfire;
using Newtonsoft.Json.Linq;
using RawCMS.Library.BackgroundJobs;

namespace RawCMS.Plugins.LogCollecting.Jobs
{
    public class LogCollectingIngestor : BackgroundJobInstance
    {
        public override string CronExpression => Cron.Minutely;

        public override string Name => "LogCollectingIngestor";

        public override string Description => "LogCollectingIngestor";

        public override void Execute(JObject data)
        {
            
        }
    }
}
