using System;
using System.Collections.Generic;
using System.Text;
using Hangfire.Dashboard;


namespace RawCMS.Plugins.FullText.Models
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
            public bool Authorize(DashboardContext context)
            {
                var httpContext = context.GetHttpContext();
                var filter = new BasicAuthAuthorizationFilter(
                new BasicAuthAuthorizationFilterOptions
                    {
                        RequireSsl = true,
                        LoginCaseSensitive = true,
                        Users = new[]
                    {
                    new BasicAuthAuthorizationUser
                    {
                        Login = "Admin",
                        PasswordClear = "Admin"
                    } 
                }
            });
            return httpContext.User.Identity.IsAuthenticated;
            
            }


    }
}

