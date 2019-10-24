using System.Collections.Generic;

namespace RawCMS.Plugins.FullText.Lambdas
{
    public class FullTextFilter
    {
        public string CollectionName { get; set; }
        public List<string> IncludedField { get; set; }
    }
}