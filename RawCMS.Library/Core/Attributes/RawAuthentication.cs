using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RawCMS.Library.Core.Attributes
{
    public class RawAuthenticationAttribute : AuthorizeAttribute
    {
        public RawAuthenticationAttribute() 
        {
            this.AuthenticationSchemes = "Bearer";
            this.Policy = "ApiKey";
            
        }

        
    }
}
