using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity.MongoDB;
using MongoDB.Driver;
using RawCMS.Plugins.Auth.Interfaces;
using RawCMS.Plugins.Auth.Repository;
using RawCMS.Plugins.Auth.Configuration;
using RawCMS.Plugins.Auth.Stores;
using Microsoft.AspNetCore.Identity;

namespace RawCMS.Plugins.Auth.Extensions
{
    public static class IdentityServerBuilderExtensions
    {
        /// <summary>
        /// Adds mongo repository (mongodb) for IdentityServer
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddMongoRepository(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IRepository, MongoRepository>();
            return builder;
        }

        /// <summary>
        /// Adds mongodb implementation for the "Asp Net Core Identity" part (saving user and roles)
        /// </summary>
        /// <remarks><![CDATA[
        /// Contains implemenations for
        /// - IUserStore<T>
        /// - IRoleStore<T>
        /// ]]></remarks>
        public static IIdentityServerBuilder AddMongoDbForAspIdentity(this IIdentityServerBuilder builder, IConfigurationRoot configurationRoot)
        {

            //User Mongodb for Asp.net identity in order to get users stored
            var configurationOptions = configurationRoot.Get<ConfigurationOptions>();
            var client = new MongoClient(configurationOptions.MongoConnection);
            var database = client.GetDatabase(configurationOptions.MongoDatabaseName);



            // Configure Asp Net Core Identity / Role to use MongoDB
            builder.Services.AddSingleton<IUserStore<Microsoft.AspNetCore.Identity.MongoDB.IdentityUser>>(x =>
            {
                var usersCollection = database.GetCollection<Microsoft.AspNetCore.Identity.MongoDB.IdentityUser>("Identity_Users");
                IndexChecks.EnsureUniqueIndexOnNormalizedEmail(usersCollection);
                IndexChecks.EnsureUniqueIndexOnNormalizedUserName(usersCollection);
                
                var userStore= new  UserStore<Microsoft.AspNetCore.Identity.MongoDB.IdentityUser>(usersCollection);
                return  (IUserStore<Microsoft.AspNetCore.Identity.IdentityUser>) userStore;
            });

            builder.Services.AddSingleton<Microsoft.AspNetCore.Identity.IRoleStore<TRole>>(x =>
            {
                var rolesCollection = database.GetCollection<TRole>("Identity_Roles");
                IndexChecks.EnsureUniqueIndexOnNormalizedRoleName(rolesCollection);
                return new RoleStore<TRole>(rolesCollection);
            });
            builder.Services.AddIdentity<TIdentity, TRole>();


            return builder;
        }

        /// <summary>
        /// Configure ClientId / Secrets
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configurationOption"></param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddClients(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IClientStore, CustomClientStore>();
            builder.Services.AddTransient<ICorsPolicyService, InMemoryCorsPolicyService>();
            return builder;
        }


        /// <summary>
        /// Configure API  &  Resources
        /// Note: Api's have also to be configured for clients as part of allowed scope for a given clientID 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddIdentityApiResources(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IResourceStore, Store.CustomResourceStore>();
            return builder;
        }

        /// <summary>
        /// Configure Grants
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddPersistedGrants(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton<IPersistedGrantStore, CustomPersistedGrantStore>();
            return builder;
        }

    }
}
