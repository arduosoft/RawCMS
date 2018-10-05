using RawCMS.Library.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace RawCMS.Plugins.Core
{
    public class UserCheckLambda : RestLambda
    {
        public override string Name => "UserCheckLambda";

        public override string Description => "";

        public override JObject Rest(JObject input)
        {
            
            return null;
            
        }
    }
}
