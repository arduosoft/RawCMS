using RawCMS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace RawCMS.Lambdas.Rest
{
    public class DummyRest : RestLambda
    {
        public override string Name => "DummyRest";

        public override string Description => "I'm a dumb dummy request";

        public override JObject Rest(JObject input)
        {
            var result = new JObject()
            {
                { "input",input},
                { "now",DateTime.Now},
               
            };

            
            return result;
        }
    }
}
