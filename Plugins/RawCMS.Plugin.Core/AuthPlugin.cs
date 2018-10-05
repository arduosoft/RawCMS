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
using RawCMS.Plugins.Core.Configuration;
using RawCMS.Plugins.Core.Stores;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using IdentityModel.AspNetCore.OAuth2Introspection;
using RawCMS.Plugins.Core.Model;

namespace RawCMS.Plugins.Core
{

    public class AuthPlugin : RawCMS.Library.Core.Extension.Plugin
    {


        public class Config
        {
            // scopes define the resources in your system
            public static IEnumerable<IdentityResource> GetIdentityResources()
            {
                return new List<IdentityResource>
            {
                //new IdentityResources.OpenId()
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
                        IdentityServerConstants.StandardScopes.OpenId,
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

            var userStore = new RawUserStore();
            services.AddSingleton<IUserStore<IdentityUser>>(x => {  return userStore;  });
            services.AddSingleton<IUserPasswordStore<IdentityUser>>(x => { return userStore; });
            services.AddSingleton<IPasswordValidator<IdentityUser>>(x => { return userStore; });
            services.AddSingleton<IUserClaimStore<IdentityUser>>(x => { return userStore; });
            services.AddSingleton<IPasswordHasher<IdentityUser>>(x => { return userStore; });

            var roleStore= new RawRoleStore();
            services.AddSingleton<IRoleStore<IdentityRole>>(x => {   return roleStore;  });

           

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

           // OAuth2IntrospectionOptions options = new OAuth2IntrospectionOptions();

            // base-address of your identityserver
           // options.Authority = "http://localhost:28436";
           // options.ClientSecret = "secret";
           // options.ClientId = "api1";
           // options.BasicAuthenticationHeaderStyle = IdentityModel.Client.BasicAuthenticationHeaderStyle.Rfc2617;
           //// options.IntrospectionEndpoint = "http://localhost:28436/connect/introspect";
           // options.TokenTypeHint = "Bearer";
            
           // options.Validate();

             
            services.AddAuthentication(OAuth2IntrospectionDefaults.AuthenticationScheme)
            //.AddOAuth2Introspection( x => {
            //    x = options;
            //});
             .AddIdentityServerAuthentication("Bearer", options =>
             {
                 options.Authority = "http://localhost:28436";
                 options.ApiName = "api1";
                 options.ApiSecret = "secret";
                 options.RequireHttpsMetadata = false;
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

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
          
            app.UseAuthentication();
            app.UseIdentityServer();
          

        }
    }
}
