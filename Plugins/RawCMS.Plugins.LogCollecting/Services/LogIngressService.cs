using System;
using System.Collections.Generic;
using System.Text;
using RawCMS.Plugins.FullText.Core;
using RawCMS.Plugins.LogCollecting.Controllers;
using RawCMS.Plugins.LogCollecting.Models;

namespace RawCMS.Plugins.LogCollecting.Services
{
    public class LogService
    {
        private readonly FullTextService fullTextService;
        private readonly LogQueue logQueue;

        public LogService(FullTextService fullTextService)
        {
            this.fullTextService = fullTextService;
            this.logQueue = new LogQueue();
        }

        public void PersistLog(string applicationId, LogEntity data)
        {
            this.fullTextService.AddDocumentRaw(applicationId, data);

        }

        public void EnqueueLog(string applicationId, List<LogEntity> data)
        {
            data.ForEach(x =>
                logQueue.Enqueue(x)
                );

            logQueue.AppendLoadValue();

        }


        public void PersistLog(string applicationId, List<LogEntity> data)
        {

        }

        public void PersistLog()
        {
            //For each application
            //PersistLog(
        }
    }
}
