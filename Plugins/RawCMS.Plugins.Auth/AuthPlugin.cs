using System;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core.Extension;
using RawCMS.Plugins.Auth.Configuration;
using RawCMS.Plugins.Auth.Extensions;

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

            services.Configure<ConfigurationOptions>(configuration);



            services.AddIdentityServer(
                   // Enable IdentityServer events for logging capture - Events are not turned on by default
                   options =>
                   {
                       options.Events.RaiseSuccessEvents = true;
                       options.Events.RaiseFailureEvents = true;
                       options.Events.RaiseErrorEvents = true;
                   }
               )
               .AddTemporarySigningCredential()
               .AddMongoRepository()
               .AddMongoDbForAspIdentity<IdentityUser, IdentityRole>(configuration)
               .AddClients()
               .AddIdentityApiResources()
               .AddPersistedGrants()
               .AddTestUsers(Config.GetUsers())
               .AddProfileService<ProfileService>();

        }

        IConfigurationRoot configuration;
        public override void Setup(IConfigurationRoot configuration)
        {
            base.Setup(configuration);
            this.configuration = configuration;
        }
        public override void Configure(IApplicationBuilder app, AppEngine appEngine)
        {
            base.Configure(app, appEngine);
            
                //NOT IMPLEMENTED YET
        }
    }
}
