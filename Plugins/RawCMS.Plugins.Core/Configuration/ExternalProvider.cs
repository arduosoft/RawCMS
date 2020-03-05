using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.Core.Configuration
{
    public enum OAuthMode
    {
        JWT,
        Introspection
    }

    public class ExternalProvider
    {
        public OAuthMode Mode { get; set; }
        public string SchemaName { get; set; }
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string UserInfoEndpoint { get; set; }
        public string RoleClaimType { get; set; }
    }
}
