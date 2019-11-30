using Jint;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using System.Collections.Generic;

namespace RawCMS.Library.Schema.Validation
{
    public class NullSafeDict<TKey, TValue> : Dictionary<TKey, TValue> where TValue : class
    {
        public new TValue this[TKey key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    return null;
                }
                else
                {
                    return base[key];
                }
            }
            set
            {
                if (!ContainsKey(key))
                {
                    Add(key, value);
                }
                else
                {
                    base[key] = value;
                }
            }
        }
    }

    public abstract class BaseJavascriptValidator : FieldTypeValidator
    {
        public abstract string Javascript { get; }

        public override List<Error> Validate(JObject input, Field field)
        {
            Error[] errors = new Error[] { };
            if (field.Options != null)
            {
                JToken value = input[field.Name];
                if (input[field.Name] == null)
                {
                    return new List<Error>(errors);//null check is done on main validation
                }

                string backendResult = "";
                //This limits to 1level objects only
                NullSafeDict<string, object> jinput = input.ToObject<NullSafeDict<string, object>>();
                NullSafeDict<string, object> options = field.Options.ToObject<NullSafeDict<string, object>>();
                Engine add = new Engine()
                  .SetValue("item", jinput)
                  .SetValue("errors", errors)
                  .SetValue("options", options)
                  .SetValue("name", field.Name)
                  .SetValue("required", field.Required)
                  .SetValue("Type", field.Type)
                  .SetValue("value", value)
                  .SetValue("backendResult", backendResult)
                 .Execute(Javascript);

                string resultEnum = add.GetValue("backendResult").ToString();
                errors = JsonConvert.DeserializeObject<Error[]>(resultEnum);

                return new List<Error>(errors);
            }

            return new List<Error>(errors);
        }
    }
}