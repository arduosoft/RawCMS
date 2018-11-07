using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RawCMS.Library.Core
{
    public abstract class SchemaValidationLambda : Lambda
    {
        public abstract List<Error> Validate(JObject input, string collection);
    }
}