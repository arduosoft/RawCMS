using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RawCMS.Library.Core
{
    public abstract class CollectionValidationLambda : Lambda
    {
        public abstract string[] TargetCollections { get; }

        public abstract List<Error> Validate(JObject item);
    }
}