using Microsoft.AspNetCore.Authorization;

namespace RawCMS.Library.Core.Attributes
{
    public class RawAuthenticationAttribute : AuthorizeAttribute
    {
        public RawAuthenticationAttribute()
        {
            AuthenticationSchemes = "Bearer";
        }
    }
}