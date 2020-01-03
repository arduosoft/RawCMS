using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using RawCMS.Library.DataModel;
using RawCMS.Library.Service;

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
            if (schema != null && dataContext.TryGetValue("expando", out object relationsObj) && relationsObj is List<string> relations)
            {
                foreach (string relName in relations)
                {
                    Schema.Field field = schema.FieldSettings.FirstOrDefault(x => x.Type == "relation" && x.Name == relName);
                    if (field != null)
                    {
                        RelationInfo relationInfo = relationInfoService.GetFromOptions(field, item);

                        var hasRefAttached = relationInfo.Values.Count >= 1;

                        if (!hasRefAttached)
                        {
                            result[field.Name] = relationInfo.IsMultiple ? new JArray() : null;
                            continue;
                        }

                        DataQuery dq = new DataQuery()
                        {
                            PageNumber = 1,
                            PageSize = 999// make it parametric
                        };

                        BsonDocument b = new BsonDocument();

                        if (relationInfo.IsMultiple)
                        {
                            BsonDocument inc = new BsonDocument
                            {
                                ["$in"] = new BsonArray(relationInfo.Values)
                            };
                            b["_id"] = inc;
                        }
                        else
                        {
                            b["_id"] = relationInfo.Values.FirstOrDefault();
                        }

                        dq.RawQuery = b.ToJson();

                        ItemList subitems = crudService.Query(relationInfo.LookupCollection, dq);

                        if (relationInfo.IsMultiple)
                        {
                            result[field.Name] = subitems.Items;
                        }
                        else
                        {
                            result[field.Name] = subitems.Items.FirstOrDefault();
                        }
                    }
                }
            }

            return result;
        }

        //    JObject queryJSON = new JObject();
        //                    if (relationInfo.IsMultiple)
        //                    {
        //                        queryJSON["_id"] = relationInfo.Values.FirstOrDefault().ToString();
        //}
        //                    else
        //                    {
        //                        queryJSON["_id"] = new JArray(relationInfo.Values.Select(x => x.ToString()).ToList());
        //                    }
    }
}