using Hangfire.Dashboard;

namespace RawCMS.Library.BackgroundJobs
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            return true;// httpContext.User.Identity.IsAuthenticated; //TODO: add authentication role
        }
    }
}