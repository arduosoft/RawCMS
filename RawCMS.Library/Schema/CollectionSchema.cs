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

        public JObject Options { get; set; }
    }

    public class CollectionSchema
    {
        public string CollectionName { get; set; }
        public bool AllowNonMappedFields { get; set; }


        public List<Field> FieldSettings { get; set; } = new List<Field>();
    }
}
