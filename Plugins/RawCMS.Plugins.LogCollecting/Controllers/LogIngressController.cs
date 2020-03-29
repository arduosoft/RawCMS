using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core.Attributes;
using RawCMS.Plugins.Core.Model;
using RawCMS.Plugins.FullText.Core;
using RawCMS.Plugins.LogCollecting.Models;
using RawCMS.Plugins.LogCollecting.Services;
using System;
using System.Collections.Generic;
namespace RawCMS.Plugins.LogCollecting.Controllers
{
    [Route("api/[controller]")]
    public class LogIngressController : Controller
    {
        protected LogService service;
        public LogIngressController(LogService service)
        {
            this.service = service;
        }
        [HttpPost()]
        [Route("{applicationId}")]
        public RestMessage<bool> Log([FromRoute]string applicationId, [FromBody] List<LogEntity> items)
        {
            var result = new RestMessage<bool>(true);
            try
            {
                this.service.EnqueueLog(applicationId, items);
            }
            catch (Exception err)
            {

                result.Data = false;
                result.Errors.Add(new Library.Core.Error()
                {
                    Code="001",
                    Title="untrapped exception"
                });
            }
            return result;
        }
    }
}

