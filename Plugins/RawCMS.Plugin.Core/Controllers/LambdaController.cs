using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RawCMS.Plugins.Core.Controllers
{
    [AllowAnonymous]
    [RawAuthentication]
    [Route("api/[controller]")]
    public class LambdaController : Controller
    {
        private readonly AppEngine lambdaManager;

        public LambdaController(AppEngine lambdaManager)
        {
            this.lambdaManager = lambdaManager;
        }

        [HttpGet()]
        public List<Lambda> Get()
        {
            return lambdaManager.Lambdas.Where(x => typeof(RestLambda).IsAssignableFrom(x.GetType())).ToList();
        }

        [AllowAnonymous]
        [RawAuthentication]
        [HttpPost("{lambda}")]
        public JObject Post(string lambda)
        {
            RestLambda lamba = lambdaManager.Lambdas.SingleOrDefault(x => typeof(RestLambda).IsAssignableFrom(x.GetType()) && x.Name == lambda) as RestLambda;
            if (lamba == null)
            {
                throw new Exception("Lambda not found or not a Rest Lambda");
            }
            return lamba.Execute(HttpContext) as JObject;
        }
    }
}