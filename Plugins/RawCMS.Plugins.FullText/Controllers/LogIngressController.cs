using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core.Attributes;
using RawCMS.Plugins.Core.Model;
using RawCMS.Plugins.FullText.Core;
using System;
using System.Collections.Generic;
using RawCMS.Plugins.FullText.Models;
namespace RawCMS.Plugins.FullText.Controllers
{
    [Route("api/[controller]")]
    public class LogIngressController : Controller
    {
        protected LogIngressService service;
        public LogIngressController(LogIngressService service)
        {
            this.service = service;
        }
        [HttpPost()]
        [Route("{applicationId}")]
        public LocalRestMessage<bool> Log([FromRoute]string applicationId, [FromBody] LogEntity item)
        {
            var result = new LocalRestMessage<bool>(true);
            try
            {
                this.service.addLog(applicationId, item);
            }
            catch (Exception err)
            {
                result.Data = false;
                result.Errors.Add(new LocalError()
                {
                    Code = "001",
                    Title = "Unknow issue",
                    Description = err.Message
                });
            }
            return result;
        }
    }
}

