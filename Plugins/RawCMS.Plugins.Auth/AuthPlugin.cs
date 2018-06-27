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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Models;

namespace RawCMS.Plugins.Auth
{

    public class Config
    {
        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                   

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" },
                    Enabled=true,
                   
                }
            };
        }
    }

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

            //services.AddIdentity<ApplicationUser, ApplicationRole>()
            //.AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
            //(
            //    "mongodb://localhost:27017",
            //    "MongoDbTests"
            //)
            //.AddDefaultTokenProviders();

            services.AddIdentityServer(x =>
            {
                x.Endpoints.EnableAuthorizeEndpoint = true;
            })
              .AddDeveloperSigningCredential()
              .AddInMemoryApiResources(Config.GetApiResources())
              .AddInMemoryClients(Config.GetClients()).
              AddTestUsers(new List<IdentityServer4.Test.TestUser>() {
                  new IdentityServer4.Test.TestUser()
                  {
                      Username="prova",
                      Password="prova",
                      IsActive=true,
                      SubjectId=Guid.NewGuid().ToString(),
                       ProviderName="HardCoded"
                  }
              });
        }

        public override void Configure(IApplicationBuilder app, AppEngine appEngine)
        {
            base.Configure(app, appEngine);

            app.UseIdentityServer();
        }
    }
}
