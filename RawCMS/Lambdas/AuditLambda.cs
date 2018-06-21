using RawCMS.Library.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace RawCMS.Library.Lambdas
{
    public class AuditLambda2 : PreSaveLambda
    {
        public override string Name => "AuditLambda";

        public override string Description => "Add audit settings";

        public override void Execute(string collection, JObject Item)
        {
            if (!Item.ContainsKey("_id") || string.IsNullOrEmpty(Item["_id"].ToString()))
            {
                Item["_createdon"] = DateTime.Now;
            }

            Item["_modifiedon"] = DateTime.Now;

        }
    }
}
