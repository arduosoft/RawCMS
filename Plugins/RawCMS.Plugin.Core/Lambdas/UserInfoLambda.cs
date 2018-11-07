using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using System;

namespace RawCMS.Plugins.Core.Lambdas
{
    public class UserInfoLambda : RestLambda
    {
        public override string Name => "UserInfo";

        public override string Description => throw new NotImplementedException();

        public override JObject Rest(JObject input)
        {
            JObject jj = new JObject
            {
                ["IsAuthenticated"] = Request.User.Identity.IsAuthenticated
            };
            foreach (System.Security.Claims.Claim claim in Request.User.Claims)
            {
                jj[claim.Type] = claim.Value;
            }
            return jj;
        }
    }
}