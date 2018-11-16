using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

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
        public string Authority { get; set; }
        public string IntrospectionEndpoint { get; internal set; }
        public string TokenTypeHint { get; internal set; }

        public bool OauthEnabled { get; set; }

        public string AdminApiKey { get; set; }
        public string ApiKey { get; set; }

        // scopes define the resources in your system
        public IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResource("custom",new string[]{ ClaimTypes.Email,ClaimTypes.NameIdentifier, ClaimTypes.Name})
            };
        }

        public IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(ApiResource, ApiResource)
                {
                    ApiSecrets = new List<Secret>
                {
                    new Secret(ClientSecret.Sha256())
                },
                Scopes=
                {
                    new Scope("openid"),
                },
                UserClaims= new string[]{ ClaimTypes.NameIdentifier, ClaimTypes.Email}
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
                    ClientId = ClientId,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,

                    ClientSecrets =
                    {
                        new Secret(ClientSecret.Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
            };
        }
    }
}