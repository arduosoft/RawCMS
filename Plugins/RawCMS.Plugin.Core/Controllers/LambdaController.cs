using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawCMS.Library.Core;
using Newtonsoft.Json.Linq;

namespace RawCMS.Plugin.Core.Controllers
{
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
            return lambdaManager.Lambdas.Where(x=>typeof(RestLambda).IsAssignableFrom(x.GetType())).ToList();
        }

        
        [HttpPost("{lambda}")]
        public JObject Post(string lambda)
        {
            var lamba= lambdaManager.Lambdas.SingleOrDefault(x=> typeof(RestLambda).IsAssignableFrom(x.GetType()) && x.Name==lambda) as RestLambda;
            if (lamba == null)
            {
                throw new Exception("Lambda not found or not a Rest Lambda");
            }
            return ((HttpLambda) lamba).Execute(HttpContext) as JObject;
        }

       
    }
}
