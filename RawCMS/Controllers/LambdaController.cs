using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawCMS.Library.Core;
using Newtonsoft.Json.Linq;

namespace RawCMS.Controllers
{
    [Route("api/[controller]")]
    public class LambdaController : Controller
    {
        
        [HttpGet()]
        public List<Lambda> Get()
        {
            return LambdaManager.Current.Lambdas;
        }

        
        [HttpPost("{collection}")]
        public JObject Post(string collection)
        {
            var lamba=LambdaManager.Current[collection] as RestLambda;
            if (lamba == null)
            {
                throw new Exception("Lambda not found or not a Rest Lambda");
            }
            return ((HttpLambda) lamba).Execute(HttpContext) as JObject;
        }

       
    }
}
