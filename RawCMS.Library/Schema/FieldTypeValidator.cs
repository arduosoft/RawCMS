using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using System.Collections.Generic;

namespace RawCMS.Library.Schema
{
    public abstract class FieldTypeValidator
    {
        public abstract string Type { get; }

        public abstract List<Error> Validate(JObject input, Field field);
    }
}