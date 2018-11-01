using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RawCMS.Plugins.Core.MVC
{

    public class ApiKeyRequirement : IAuthorizationRequirement
    {
        public string ApiKey { get; set; }
        public string AdminApiKey { get; set; }
    }

    public class RawAuthorizationHandler : AuthorizationHandler<ApiKeyRequirement>
    {

        IHttpContextAccessor _httpContextAccessor = null;

        public RawAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        

        protected override async  Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
        {
            //if user specified, apikey are not used and authentication demandend to roles
            if (context.User != null)
            {
                context.Succeed(requirement);
            }

            bool isAdmin = _httpContextAccessor.HttpContext.Request.Path.Value.StartsWith("system/");

            if (!isAdmin && _httpContextAccessor.HttpContext.Request.Headers["Authorization"] == requirement.ApiKey)
            {
                SetUser("ApiKeyUser", "Authenticated");

                context.Succeed(requirement);
            }

            if (isAdmin && _httpContextAccessor.HttpContext.Request.Headers["Authorization"] == requirement.AdminApiKey)
            {
                SetUser("ApiKeyUser", "Authenticated,Admin");
                context.Succeed(requirement);
            }
        }

        private void SetUser(string username,string roles)
        {
            ClaimsIdentity id = new ClaimsIdentity("ApiKey", ClaimTypes.NameIdentifier, ClaimTypes.Role);
            id.AddClaim(new Claim(ClaimTypes.NameIdentifier, username));
            id.AddClaim(new Claim(ClaimTypes.Role, roles));
            id.AddClaim(new Claim("prova", "prova"));
            _httpContextAccessor.HttpContext.User.AddIdentity(id);
        }
    }
}
