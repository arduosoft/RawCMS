using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.FullText.Lambdas
{
    public class FullTextFilter
    {
        public string CollectionName { get; set; }
        public List<string> IncludedField { get; set; }
    }
}
