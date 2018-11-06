using IdentityServer4.Services;
using Microsoft.Extensions.DependencyInjection;
using RawCMS.Plugins.Core.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.Core.Extensions
{
    public static class IdentityServerExtensions
    {
        public static IIdentityServerBuilder AddProfileServiceCustom(this IIdentityServerBuilder builder, RawUserStore instance)
        {
            builder.Services.AddTransient<IProfileService, RawUserStore>( x=> { return instance; });

            return builder;
        }
    }
}
