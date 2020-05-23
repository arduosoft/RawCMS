using Newtonsoft.Json.Linq;
using RawCMS.Library.Lambdas;
using System;
using System.Collections.Generic;

namespace RawCMS.Plugins.Core.Lambdas.Applications
{
    public class PublicIDLambda : PreSaveLambda
    {
        public override string Name => "Generate public ID";

        public override string Description => "Generate public id for application if not filled";

        public override void Execute(string collection, ref JObject item, ref Dictionary<string, object> dataContext)
        {
            if (collection == "application")
            {
                if (!item.ContainsKey("PublicId") || item["PublicId"] == null)
                {
                    item["PublicId"] = Guid.NewGuid().ToString();
                }
            }
        }
    }
}