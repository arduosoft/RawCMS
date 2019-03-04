using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using System;
using System.Collections.Generic;

namespace RawCMS.Library.Lambdas
{
    public class AuditLambda : PreSaveLambda
    {
        public override string Name => "AuditLambda";

        public override string Description => "Add audit settings";

        public override void Execute(string collection, ref JObject Item, ref Dictionary<string, object> dataContext)
        {
            if (!Item.ContainsKey("_id") || string.IsNullOrEmpty(Item["_id"].ToString()))
            {
                Item["_createdon"] = DateTime.Now;
            }

            Item["_modifiedon"] = DateTime.Now;
        }
    }
}