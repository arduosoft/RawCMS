using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;

namespace RawCMS.Plugins.Core.MVC
{
    public class RawAuthorizationAttribute : ActionFilterAttribute
    {
        private readonly string apikey;
        private readonly string adminapikey;

        public RawAuthorizationAttribute(string apikey, string adminapikey) : base()
        {
            this.adminapikey = adminapikey;
            this.apikey = apikey;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            //if user specified, apikey are not used and authentication demandend to roles
            if (context.HttpContext.User != null && context.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            bool isAdmin = context.HttpContext.Request.Path.Value.StartsWith("system/");

            if (!isAdmin && !string.IsNullOrWhiteSpace(apikey) && context.HttpContext.Request.Headers["Authorization"] == apikey)
            {
                SetUser("ApiKeyUser", "Authenticated", context.HttpContext);
            }

            if (isAdmin && !string.IsNullOrWhiteSpace(adminapikey) && context.HttpContext.Request.Headers["Authorization"] == adminapikey)
            {
                SetUser("ApiKeyUser", "Authenticated,Admin", context.HttpContext);
            }
            if (context.HttpContext.User == null || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                Send401(context.HttpContext);
            }
        }

        private void Send401(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 401;
            httpContext.Response.Clear();
            httpContext.Response.WriteAsync("user not athenticated and api missing.");
            throw new Exception("user not athenticated and api missing.");
        }

        private void SetUser(string username, string roles, HttpContext httpContext)
        {
            ClaimsIdentity id = new ClaimsIdentity("ApiKey", ClaimTypes.NameIdentifier, ClaimTypes.Role);
            id.AddClaim(new Claim(ClaimTypes.NameIdentifier, username));
            id.AddClaim(new Claim(ClaimTypes.Role, roles));
            id.AddClaim(new Claim("prova", "prova"));
            httpContext.User = new ClaimsPrincipal();
            httpContext.User.AddIdentity(id);
        }
    }
}