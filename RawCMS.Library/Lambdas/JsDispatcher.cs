using Jint;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using RawCMS.Library.Schema;
using System.Collections.Generic;

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
                    Dictionary<string, object> input = item.ToObject<Dictionary<string, object>>();
                    Engine add = new Engine()
                      .SetValue("item", input)
                     .Execute(settings.PresaveScript);
                    item = JObject.FromObject(input);
                }
            }
        }
    }
}