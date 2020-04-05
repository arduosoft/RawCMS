using System;
using System.Collections.Generic;
using System.Text;
using RawCMS.Library.Schema;
using RawCMS.Library.Service;
using RawCMS.Plugins.FullText.Core;
using RawCMS.Plugins.LogCollecting.Controllers;
using RawCMS.Plugins.LogCollecting.Models;
using System.Linq;

namespace RawCMS.Plugins.LogCollecting.Services
{
    public class LogService
    {
        private readonly FullTextService fullTextService;
        private readonly LogQueue logQueue;
        private readonly CRUDService crudService;

        private const int LOG_PROCESSING_SIZE= 100000;
        private const int PROCESSING_ENTRY_COUNT = 1000;
        public LogService(FullTextService fullTextService, CRUDService crudService)
        {
            this.fullTextService = fullTextService;
            this.crudService = crudService;
            this.logQueue = new LogQueue();
        }

        public void PersistLog(string applicationId, LogEntity data)
        {
            this.fullTextService.AddDocumentRaw(applicationId, data);

        }

        public void EnqueueLog(string applicationId, List<LogEntity> data)
        {
            data.ForEach(x => {
                x.ApplicationId = applicationId;
                logQueue.Enqueue(x);
                }
                );

            logQueue.AppendLoadValue();

        }


        public void PersistLog()
        {
            var applications = this.crudService.Query<Application>("applications", new Library.DataModel.DataQuery())
                .Items
                .Where(x=>x.PublicId != null).ToList();

            int processedLog = 0;

            List<LogEntity> batch;
            string indexname = "";
            while (processedLog < LOG_PROCESSING_SIZE && (batch = this.logQueue.Dequeue(PROCESSING_ENTRY_COUNT)) != null)
            {
                foreach (var log in batch)
                {
                    indexname = "log_" + applications.FirstOrDefault(x => x.PublicId.Equals(log.ApplicationId)).Id;

                    //TODO: implement batch insert for performance
                    this.fullTextService.AddDocument<LogEntity>(indexname, log);
                }
            }


        }
    }
}
