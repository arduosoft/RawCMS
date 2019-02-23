using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.GraphQL.Classes
{
    public class GraphQLSettings
    {
        public string Path { get; set; }

        public string GraphiQLPath { get; set; }

        public Func<HttpContext, object> BuildUserContext { get; set; }
        public bool EnableMetrics { get; set; }
    }
}
