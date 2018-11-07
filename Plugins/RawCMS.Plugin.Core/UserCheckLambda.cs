using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;

namespace RawCMS.Plugins.Core
{
    public class UserCheckLambda : RestLambda
    {
        public override string Description => "";
        public override string Name => "UserCheckLambda";

        public override JObject Rest(JObject input)
        {
            return null;
        }
    }
}