using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Schema;
using RawCMS.Library.Service;

namespace RawCMS.Library.Lambdas
{
    public class SchemaDeleteLambda : PostDeleteLambda
    {
        private readonly EntityService _entityService;
        public SchemaDeleteLambda(EntityService entityService)
        {
            _entityService = entityService;
        }

        public override string Name => "SchemaDeleteLambda";

        public override string Description => "Update schema in entity service";

        public override void Execute(string collection, ref JObject item, ref Dictionary<string, object> dataContext)
        {
            if (collection.Equals("_schema"))
            {
                var schema = item.ToObject<CollectionSchema>();
                _entityService.AddOrReplaceEntity(schema.CollectionName, schema, this.Operation);
            }
        }
    }
}
