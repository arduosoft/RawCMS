using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Core
{
    public abstract class CollectionValidationLambda:Lambda
    {
        public abstract string[] TargetCollections { get; }
        public abstract List<Error> Validate(JObject item);

    }
}
