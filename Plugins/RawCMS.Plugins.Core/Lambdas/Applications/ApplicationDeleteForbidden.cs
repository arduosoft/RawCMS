using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Lambdas;
using RawCMS.Library.Schema;

namespace RawCMS.Plugins.Core.Lambdas.Applications
{
    public class ApplicationDeleteForbidden : PreDeleteLambda
    {
        public override string Name => "ApplicationDeleteForbidden";

        public override string Description => "application collection cannot be deleted";

        public override void Execute(string collection, ref JObject item, ref Dictionary<string, object> dataContext)
        {
            if (collection == "_schema")
            {
                var schema = item.ToObject<CollectionSchema>();
                if (schema.CollectionName == "application")
                {
                    throw new Exception("Unable to delete application ");
                }
            }
        }
    }
}
