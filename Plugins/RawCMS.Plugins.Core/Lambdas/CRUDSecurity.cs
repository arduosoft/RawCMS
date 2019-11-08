using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using RawCMS.Library.Lambdas;
using RawCMS.Library.Schema;
using RawCMS.Library.Service;
using System;
using System.Collections.Generic;

namespace RawCMS.Plugins.Core.Lambdas
{
    public class CollectionSecurityInfo
    {
        public Dictionary<string, List<string>> AllowedRoleMap { get; set; }
    }

    public abstract class GenericSecurity : DataProcessLambda
    {
        private readonly CRUDService service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public void CheckGeneric(string collection, DataOperation operation)
        {
            if (EntityValidation.Entities.TryGetValue(collection, out CollectionSchema current))
            {
                if (current.PluginConfiguration.TryGetValue("Security", out JObject jObject))
                {
                    CollectionSecurityInfo secinfo = jObject.ToObject<CollectionSecurityInfo>();
                    if (secinfo.AllowedRoleMap != null)
                    {
                        if (secinfo.AllowedRoleMap.TryGetValue(operation.ToString(), out List<string> allowedRoles))
                        {
                            foreach (string role in allowedRoles)
                            {
                                if (_httpContextAccessor.HttpContext.User.IsInRole(role))
                                {
                                    return;
                                }
                            }
                        }
                        throw new Exception("Forbidden");
                    }
                }
            }
        }

        public GenericSecurity(CRUDService service, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            this.service = service;
        }
    }

    public class DeleteSecurity : GenericSecurity
    {
        public override string Name => nameof(DeleteSecurity);

        public override string Description => nameof(DeleteSecurity);

        public override SavePipelineStage Stage => SavePipelineStage.PreSave;

        public override DataOperation Operation => DataOperation.Delete;

        public override void Execute(string collection, ref JObject item, ref Dictionary<string, object> dataContext)
        {
            CheckGeneric(collection, Operation);
        }

        public DeleteSecurity(CRUDService service, IHttpContextAccessor httpContextAccessor) : base(service, httpContextAccessor)
        {
        }
    }

    public class WriteSecurity : GenericSecurity
    {
        public override string Name => nameof(WriteSecurity);

        public override string Description => nameof(WriteSecurity);
        public override SavePipelineStage Stage => SavePipelineStage.PreSave;
        public override DataOperation Operation => DataOperation.Write;

        public override void Execute(string collection, ref JObject item, ref Dictionary<string, object> dataContext)
        {
            CheckGeneric(collection, Operation);
        }

        public WriteSecurity(CRUDService service, IHttpContextAccessor httpContextAccessor) : base(service, httpContextAccessor)
        { }
    }

    public class ReadSecurity : GenericSecurity
    {
        public override string Name => nameof(ReadSecurity);
        public override SavePipelineStage Stage => SavePipelineStage.PreSave;
        public override DataOperation Operation => DataOperation.Read;

        public override string Description => nameof(ReadSecurity);

        public override void Execute(string collection, ref JObject item, ref Dictionary<string, object> dataContext)
        {
            CheckGeneric(collection, Operation);
        }

        public ReadSecurity(CRUDService service, IHttpContextAccessor httpContextAccessor) : base(service, httpContextAccessor)
        { }
    }
}