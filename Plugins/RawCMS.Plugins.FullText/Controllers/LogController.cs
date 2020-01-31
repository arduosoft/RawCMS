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
    [Route("api")]
    class LogController : Controller
    {
       
        [HttpGet()]
        [Route("write")]
        public void write()
        {
            Console.WriteLine("ciao");
            }
    }
        
}

