using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RawCMS.Library.Schema;
using Jint;

namespace RawCMS.Library.Lambdas
{
    public class JsDispatcher : PreSaveLambda
    {
        public override string Name => "JsDispatcher";

        public override string Description => "JsDispatcher";

        public override void Execute(string collection, ref JObject item, ref Dictionary<string, object> dataContext)
        {
            if (EntityValidation.Entities.TryGetValue(collection, out CollectionSchema settings))
            {
                if (!string.IsNullOrEmpty(settings.PresaveScript))
                {
                    var input = item.ToObject<Dictionary<string, object>>();
                    var add = new Engine()
                      .SetValue("item", input)
                     .Execute(settings.PresaveScript);
                    item = JObject.FromObject(input);
                }
            }
        }
    }
}
