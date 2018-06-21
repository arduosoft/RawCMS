using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Core
{
    public abstract class SchemaValidationLambda: Lambda
    {
        public abstract List<Error> Validate(JObject input, string collection);
    }
}
