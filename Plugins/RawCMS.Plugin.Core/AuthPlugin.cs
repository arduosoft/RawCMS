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
using RawCMS.Library.Core.Interfaces;
using RawCMS.Plugins.Core.MVC;
using Microsoft.AspNetCore.Authorization;

namespace RawCMS.Plugins.Core
{

    public class AuthPlugin : RawCMS.Library.Core.Extension.Plugin, IConfigurablePlugin<AuthConfig>
    {



        public override string Name => "Authorization";

        public override string Description => "Add authorizaton capabilities";

        public override void Init()
        {
            this.Logger.LogInformation("Authorization plugin loaded");
        }

        RawUserStore userStore = new RawUserStore();
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.Configure<ConfigurationOptions>(configuration);




            services.AddSingleton<IUserStore<IdentityUser>>(x => { return userStore; });
            services.AddSingleton<IUserPasswordStore<IdentityUser>>(x => { return userStore; });
            services.AddSingleton<IPasswordValidator<IdentityUser>>(x => { return userStore; });
            services.AddSingleton<IUserClaimStore<IdentityUser>>(x => { return userStore; });
            services.AddSingleton<IPasswordHasher<IdentityUser>>(x => { return userStore; });


            //Add apikey authentication
           

            var roleStore = new RawRoleStore();
            services.AddSingleton<IRoleStore<IdentityRole>>(x => { return roleStore; });



            services.AddIdentity<IdentityUser, IdentityRole>();

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddInMemoryPersistedGrants()
            .AddInMemoryIdentityResources(this.config.GetIdentityResources())
            .AddInMemoryApiResources(this.config.GetApiResources())
            .AddInMemoryClients(this.config.GetClients())
            .AddAspNetIdentity<IdentityUser>();

            

            if (this.config.Mode == OAuthMode.External)
            {
                OAuth2IntrospectionOptions options = new OAuth2IntrospectionOptions();

                //base - address of your identityserver
                options.Authority = this.config.Authority;
                options.ClientSecret = this.config.ClientSecret;
                options.ClientId = this.config.ClientId;
                options.BasicAuthenticationHeaderStyle = IdentityModel.Client.BasicAuthenticationHeaderStyle.Rfc2617;
                if (!string.IsNullOrWhiteSpace(this.config.IntrospectionEndpoint))
                {
                    options.IntrospectionEndpoint = this.config.IntrospectionEndpoint;
                }
                options.TokenTypeHint = "Bearer";
                if (!string.IsNullOrWhiteSpace(this.config.TokenTypeHint))
                {
                    options.TokenTypeHint = this.config.TokenTypeHint;
                }

                options.Validate();

                services.AddAuthentication(OAuth2IntrospectionDefaults.AuthenticationScheme)
                    .AddOAuth2Introspection(x =>
                    {
                        x = options;
                    });


            }
            else
            {
                services.AddAuthentication(OAuth2IntrospectionDefaults.AuthenticationScheme)
                 //.AddOAuth2Introspection( x => {
                 //    x = options;
                 //});
                 .AddIdentityServerAuthentication("Bearer", options =>
                 {
                     options.Authority = this.config.Authority;
                     options.ApiName = this.config.ApiResource;
                     options.ApiSecret = this.config.ClientSecret;
                     options.RequireHttpsMetadata = false;
                 });
            }


            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiKey", policy =>
                    policy.Requirements.Add(new ApiKeyRequirement()
                    {
                        AdminApiKey = this.config.AdminApiKey,
                        ApiKey = this.config.ApiKey
                    }));
            });

            services.AddSingleton<IAuthorizationHandler, RawAuthorizationHandler>();
        }

        IConfigurationRoot configuration;
        public override void Setup(IConfigurationRoot configuration)
        {
            base.Setup(configuration);
            this.configuration = configuration;
        }
        AppEngine appEngine;
        public override void Configure(IApplicationBuilder app, AppEngine appEngine)
        {
            this.appEngine = appEngine;


            userStore.SetCRUDService(this.appEngine.Service);
            userStore.SetLogger(this.appEngine.GetLogger(this));
             userStore.InitData().Wait();

            base.Configure(app, appEngine);

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
          
            app.UseAuthentication();
            app.UseIdentityServer();
          
            

        }

        public AuthConfig GetDefaultConfig()
        {
            return new AuthConfig()
            {
                Mode=OAuthMode.Standalone,
                Authority="http://localhost:50093",
                ClientId="raw.client",
                ClientSecret="raw.secret",
                ApiResource="rawcms"
            };
        }

        AuthConfig config;
        public void SetActualConfig(AuthConfig config)
        {
            this.config = config;
        }
    }
}
