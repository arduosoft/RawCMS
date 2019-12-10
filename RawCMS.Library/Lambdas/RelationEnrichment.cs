using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using RawCMS.Library.DataModel;
using RawCMS.Library.Service;
using System.Collections.Generic;
using System.Linq;

namespace RawCMS.Library.Lambdas
{
    public class RelationEnrichment : DataEnrichment
    {
        public override string MetadataName => "rel";

        public override string Name => "RelationEnrichment";

        public override string Description => "";

        protected readonly CRUDService crudService;
        protected readonly EntityService entityService;
        protected readonly RelationInfoService relationInfoService;

        public RelationEnrichment(CRUDService crudService, EntityService entityService, RelationInfoService relationInfoService)
        {
            this.entityService = entityService;
            this.crudService = crudService;
            this.relationInfoService = relationInfoService;
        }

        public override JObject EnrichMetadata(string collection, JObject item, Dictionary<string, object> dataContext)
        {
            JObject result = new JObject();

            Schema.CollectionSchema schema = entityService.GetByName(collection);
            if (dataContext.TryGetValue("expando", out object relationsObj) && relationsObj is List<string> relations)
            {
                foreach (string relName in relations)
                {
                    Schema.Field field = schema.FieldSettings.FirstOrDefault(x => x.Type == "relation" && x.Name == relName);
                    if (field != null)
                    {
                        RelationInfo relationInfo = relationInfoService.GetFromOptions(field, item);

                        DataQuery dq = new DataQuery()
                        {
                            PageNumber = 0,
                            PageSize = 999// make it parametric
                        };

                        if (relationInfo.IsMultiple)
                        {
                            dq.RawQuery = Builders<BsonDocument>.Filter.Eq("_id", relationInfo.Values.FirstOrDefault()).ToJson();
                        }
                        else
                        {
                            dq.RawQuery = Builders<BsonDocument>.Filter.In<BsonObjectId>("_id", relationInfo.Values).ToJson();
                        }

                        ItemList subitems = crudService.Query(relationInfo.LookupCollection, dq);
                        result[field.Name] = subitems.Items;
                    }
                }
            }

            return result;
        }
    }
}