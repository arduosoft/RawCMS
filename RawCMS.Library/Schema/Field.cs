using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Schema
{
    public class Field
    {
        public string Name { get; set; }

        public bool Required { get; set; }

        public string Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FieldBaseType BaseType { get; set; }

        public JObject Options { get; set; }
    }
}
