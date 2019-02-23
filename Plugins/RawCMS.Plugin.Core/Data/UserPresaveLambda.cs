using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Interfaces;
using RawCMS.Library.Service;
using RawCMS.Plugins.Core.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.Core.Data
{
    public class UserPresaveLambda : PreSaveLambda, IRequireCrudService
    {
        public override string Name => "User Presave lambda";

        public override string Description => "provide normalized name and prevent password change";

        private CRUDService service;

        public override void Execute(string collection, ref JObject item)
        {
            item["NormalizedUserName"] = RawUserStore.NormalizeString(item["UserName"].Value<string>());
            if (item.ContainsKey("NormalizedEmail"))
            {
                item["NormalizedEmail"] = RawUserStore.NormalizeString(item["NormalizedEmail"].Value<string>());
            }

            //Password cant' be changed during update 
            if (item.ContainsKey("_id") && !string.IsNullOrWhiteSpace(item["_id"].Value<string>()) && !item.ContainsKey("NewPassword"))
            {
                var id = item["_id"].Value<string>();
                
                var old = service.Get(collection, id);
                item["PasswordHash"] = old["PasswordHash"];
            }
            else
            {
                item["PasswordHash"] = RawUserStore.ComputePasswordHash(item["NewPassword"].Value<string>());
            }
        }

        public void SetCRUDService(CRUDService service)
        {
            this.service = service;
        }

    }
}

