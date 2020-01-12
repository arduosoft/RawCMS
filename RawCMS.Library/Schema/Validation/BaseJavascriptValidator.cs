﻿using Jint;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using System;
using System.Collections.Generic;

namespace RawCMS.Library.Schema.Validation
{
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

                object convValue = null;
                convValue = GetObjectValue(value, convValue);
                string debug = "";
                Engine add = new Engine((X) =>
                {
                    X.AllowClr();
                    X.Culture(new System.Globalization.CultureInfo("en-US"));
                })
                   .SetValue("log", new Action<object>((x) =>
                   {
                       Console.WriteLine(x);
                   }))
                  .SetValue("item", jinput)
                  .SetValue("errors", errors)
                  .SetValue("options", options)
                  .SetValue("name", field.Name)
                  .SetValue("required", field.Required)
                  .SetValue("Type", field.Type)
                  .SetValue("value", convValue)
                  .SetValue("backendResult", backendResult)
                  .SetValue("debug", debug)
                 .Execute(Javascript);

                string resultEnum = add.GetValue("backendResult").ToString();
                errors = JsonConvert.DeserializeObject<Error[]>(resultEnum);

                return new List<Error>(errors);
            }

            return new List<Error>(errors);
        }

        private static object GetObjectValue(JToken value, object convValue)
        {
            if (value == null)
            {
                return null;
            }

            switch ((value as JValue).Type)
            {
                //case JTokenType.Object:
                //    break;
                //case JTokenType.Array:
                //    break;
                //case JTokenType.Constructor:
                //    break;
                //case JTokenType.Property:
                //    break;
                //case JTokenType.Comment:
                //    break;
                case JTokenType.Integer:
                    return value.ToObject<int>();

                case JTokenType.Float:
                    return value.ToObject<double>();

                case JTokenType.String:
                    return value.ToObject<string>();

                case JTokenType.Boolean:
                    return value.ToObject<bool>();

                case JTokenType.Null:
                    return null;

                case JTokenType.Undefined:
                    return null;

                case JTokenType.Date:
                    return value.ToObject<DateTime>();
                //case JTokenType.Raw:
                //    break;
                //case JTokenType.Bytes:
                //    break;
                case JTokenType.Guid:
                    return value.ToObject<Guid>().ToString();

                case JTokenType.Uri:
                    break;
                //case JTokenType.TimeSpan:
                //    break;
                case JTokenType.None:
                    return null;
            }
            return convValue;
        }
    }
}