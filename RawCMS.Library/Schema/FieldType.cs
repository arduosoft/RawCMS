using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace RawCMS.Library.Schema
{
    public class OptionParameter
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public abstract class FieldType
    {
        public abstract string TypeName { get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public abstract FieldGraphType GraphType { get; }

        public virtual List<OptionParameter> OptionParameter { get; set; } = new List<OptionParameter>();
    }
}