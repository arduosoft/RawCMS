using RawCMS.Library.Core.Extension;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using RawCMS.Library.Core;
using RawCMS.Plugins.Auth.Model;
using Microsoft.AspNetCore.Identity;

namespace RawCMS.Plugins.Auth
{
    public class AuthPlugin : Plugin
    {
        public override string Name => "Authorization";

        public override string Description => "Add authorizaton capabilities";

        public override void Init()
        {
            this.Logger.LogInformation("Authorization plugin loaded");
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
            (
                "mongodb://localhost:27017",
                "MongoDbTests"
            )
            .AddDefaultTokenProviders();
        }

        public override void Configure(IApplicationBuilder app, AppEngine appEngine)
        {
            base.Configure(app, appEngine);

            app.UseIdentityServer();
        }
    }
}
