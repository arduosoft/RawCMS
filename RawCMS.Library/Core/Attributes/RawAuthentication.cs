using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RawCMS.Library.Core.Attributes
{
    public class RawAuthenticationAttribute : AuthorizeAttribute
    {
        public RawAuthenticationAttribute() 
        {
            this.AuthenticationSchemes = "Bearer";
            
        }

        
    }
}
