using RawCMS.Library.Core.Extension;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using RawCMS.Library.Core;
using RawCMS.Plugins.Auth.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Models;
using IdentityServer4.AccessTokenValidation;
using AspNetCore.Identity.MongoDbCore;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
                //NOT IMPLEMENTED YET   
            }

            public override void Configure(IApplicationBuilder app, AppEngine appEngine)
        {
            base.Configure(app, appEngine);
            
                //NOT IMPLEMENTED YET
        }
    }
}
