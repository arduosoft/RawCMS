using System;
using System.Collections.Generic;
using System.Text;
using RawCMS.Plugins.FullText.Models;
namespace RawCMS.Plugins.FullText.Core
{
    public class LogIngressService
    {
        private readonly FullTextService fullTextService;

        public LogIngressService(FullTextService fullTextService)
        {
            this.fullTextService = fullTextService;
        }

        public void addLog(string applicationId, LogEntity data)
        {
            this.fullTextService.AddDocumentRaw(applicationId, data);
        }
    }
}
