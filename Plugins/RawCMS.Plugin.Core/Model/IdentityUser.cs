using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace RawCMS.Plugins.Core.Model
{
    public class IdentityUser
    {
        public virtual string Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string NormalizedUserName { get; set; }

        
        public virtual string Email { get; set; }

        public virtual string NormalizedEmail { get; set; }
        
        public virtual string PasswordHash { get; set; }
        
       
        public virtual ICollection<string> Roles { get; } = new List<string>();
       
        
        public override string ToString()
        {
            return UserName;
        }

        public virtual JObject Metadata { get; } = new JObject();

        public virtual ICollection<Claim> Claims { get; } = new List<Claim>();
    }
}
