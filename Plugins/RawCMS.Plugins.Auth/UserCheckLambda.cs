using RawCMS.Library.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace RawCMS.Plugins.Auth
{
    public class UserCheckLambda : RestLambda
    {
        public override string Name => "UserCheckLambda";

        public override string Description => throw new NotImplementedException();

        public override JObject Rest(JObject input)
        {
            
            return null;
            
        }
    }
}
