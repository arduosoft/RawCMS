using System;
using System.Collections.Generic;
using System.Text;
using RawCMS.Plugins.FullText.Core;

namespace RawCMS.Plugins.LogCollecting.Services
{
   public class LogQueueService
    {
        private readonly FullTextService fullTextService;

        public LogQueueService(FullTextService fullTextService)
        {
            this.fullTextService = fullTextService;
        }
    }
}
