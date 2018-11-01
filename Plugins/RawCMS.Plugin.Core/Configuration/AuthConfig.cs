using IdentityServer4;
using IdentityServer4.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.Core.Configuration
{
    public enum OAuthMode
    {
        Standalone,
        External
    }

    public class AuthConfig
    {
        public OAuthMode Mode { get; set; }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ApiResource { get; set; }
        public string Authority { get;  set; }
        public string IntrospectionEndpoint { get; internal set; }
        public string TokenTypeHint { get; internal set; }

        public bool OauthEnabled { get; set; }

        public string AdminApiKey { get; set; }
        public string ApiKey { get; set; }



        // scopes define the resources in your system
        public  IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                //new IdentityResources.OpenId()
            };
        }

        public IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(this.ApiResource, this.ApiResource)
                {
                    ApiSecrets = new List<Secret>
                {
                    new Secret(this.ClientSecret.Sha256())
                },
                Scopes=
                {
                    new Scope("openid"),
                }
                }
            };
        }

      
        // clients want to access resources (aka scopes)
        public IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {

                new Client
                {
                    ClientId = this.ClientId,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,


                    ClientSecrets =
                    {
                        new Secret(this.ClientSecret.Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                    }
                },


            };
        }

    }
}
