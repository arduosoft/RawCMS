using Microsoft.AspNetCore.Mvc;
using RawCMS.Plugins.LogCollecting.Model;
using RawCMS.Plugins.LogCollecting.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.LogCollecting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogStaticsController:ControllerBase
    {
        protected LogService service;
        public LogStaticsController(LogService logService)
        {
            service = logService;
        }

        [HttpGet]
        public List<LogStatistic> Get()
        {
            return service.GetStatistic();
        }

        [HttpGet]
        [Route("applicationId")]
        public List<LogStatistic> Get(string applicationId)
        {
            return service.GetStatistic();
        }
    }
}
