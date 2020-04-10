using Hangfire;
using Newtonsoft.Json.Linq;
using RawCMS.Library.BackgroundJobs;
using RawCMS.Library.Service;
using RawCMS.Plugins.LogCollecting.Services;

namespace RawCMS.Plugins.LogCollecting.Jobs

{
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