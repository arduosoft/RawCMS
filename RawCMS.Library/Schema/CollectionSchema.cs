using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Schema
{
   

    public class CollectionSchema
    {
        public string CollectionName { get; set; }

        public JArray FieldSettings { get; set; }
    }
}
