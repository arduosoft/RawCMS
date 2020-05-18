using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RawCMS.Library.BackgroundJobs;
using RawCMS.Plugins.Core.Model;
using RawCMS.Plugins.LogCollecting.Models;
using RawCMS.Plugins.LogCollecting.Services;

namespace RawCMS.Plugins.LogCollecting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogIngressController : ControllerBase
    {
        protected LogService service;
        protected BackgroundJobService jobs;

        public LogIngressController(LogService service, BackgroundJobService jobs)
        {
            this.service = service;
            this.jobs = jobs;
        }

        [HttpPost()]
        [Route("{applicationId}")]
        public RestMessage<bool> Log([FromRoute]string applicationId, [FromBody] List<LogEntity> items)
        {
            var result = new RestMessage<bool>(true);
            try
            {
                this.service.EnqueueLog(applicationId, items);
                this.jobs.RunOnce("LogCollectingIngestor", new JObject());
            }
            catch (Exception err)
            {
                result.Data = false;
                result.Errors.Add(new Library.Core.Error()
                {
                    Code = "001",
                    Title = "untrapped exception"
                });
            }
            return result;
        }
    }
}