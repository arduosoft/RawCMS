using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.FullText.Core
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
