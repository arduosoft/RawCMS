using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;

using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Extension;
using RawCMS.Plugins.Auth.Configuration;
using RawCMS.Plugins.Auth.Extensions;
using RawCMS.Plugins.Auth.Stores;

namespace RawCMS.Plugins.Auth
{

    public class AuthPlugin : Plugin
    {


        public class Config
        {
            // scopes define the resources in your system
            public static IEnumerable<IdentityResource> GetIdentityResources()
            {
                return new List<IdentityResource>
            {
                new IdentityResources.OpenId()
            };
            }

            public static IEnumerable<ApiResource> GetApiResources()
            {
                return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
                {
                    ApiSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                Scopes=
                {
                    new Scope("openid"), 
                }
                }
            };
            }

            // clients want to access resources (aka scopes)
            public static IEnumerable<Client> GetClients()
            {
                // client credentials client
               return new List<Client>
            {
                

                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    
                    
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =
                    { 
                        IdentityServerConstants.StandardScopes.OpenId
                    }
                },

               
            };
            }
        }

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

            services.AddSingleton<IUserStore<IdentityUser>>(x =>
            {

                var userStore = new RawUserStore();
                return userStore;
            });


            services.AddSingleton<IUserPasswordStore<IdentityUser>>(x =>
            {

                var userStore = new RawUserStore();
                return userStore;
            });

            services.AddSingleton<IPasswordValidator<IdentityUser>>(x =>
            {

                var userStore = new RawUserStore();
                return userStore;
            });

            services.AddSingleton<IPasswordHasher<IdentityUser>>(x =>
            {

                var userStore = new RawUserStore();
                return userStore;

            });
            services.AddSingleton<IRoleStore<IdentityRole>>(x =>
            {

                return new RawRoleStore();
            });

            services.AddIdentity<IdentityUser, IdentityRole>();

            //services.AddIdentityServer(
            //       // Enable IdentityServer events for logging capture - Events are not turned on by default
            //       options =>
            //       {
            //           options.Events.RaiseSuccessEvents = true;
            //           options.Events.RaiseFailureEvents = true;
            //           options.Events.RaiseErrorEvents = true;
            //       }
            //   ).AddDeveloperSigningCredential()
            //   .AddMongoRepository()
            //   .AddMongoDbForAspIdentity(configuration);


            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddInMemoryPersistedGrants()
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryClients(Config.GetClients())
            .AddAspNetIdentity<IdentityUser>();
            //.AddTestUsers(new List<IdentityServer4.Test.TestUser>()
            //{
            //    new IdentityServer4.Test.TestUser()
            //    {
            //        SubjectId="SSS",
            //        IsActive=true,

            //        Username="bob",
            //        Password="Password.1"
            //    }
            //});

          //  JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                // base-address of your identityserver
                options.Authority = "http://localhost:28436";
                options.ApiSecret = "secret";
                // name of the API resource
                options.ApiName = "api1";
                options.RequireHttpsMetadata = false;
                options.SupportedTokens = SupportedTokens.Both;
            });


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

           // JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            app.UseIdentityServer();
            app.UseAuthentication();

        }
    }
}
