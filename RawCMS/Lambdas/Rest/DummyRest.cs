using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using System;

namespace RawCMS.Lambdas.Rest
{
    public class DummyRest : RestLambda
    {
        public override string Name => "DummyRest";

        public override string Description => "I'm a dumb dummy request";

        public override JObject Rest(JObject input)
        {
            JObject result = new JObject()
            {
                { "input",input},
                { "now",DateTime.Now},
            };

            return result;
        }
    }
}