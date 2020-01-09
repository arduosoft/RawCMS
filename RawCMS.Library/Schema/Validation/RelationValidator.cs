using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using RawCMS.Library.Service;

namespace RawCMS.Library.Schema.Validation
{
    public abstract class RelationValidator : FieldTypeValidator
    {
        protected readonly CRUDService cRUDService;
        protected readonly RelationInfoService relationInfoService;

        public RelationValidator(CRUDService cRUDService, RelationInfoService relationInfoService)
        {
            this.cRUDService = cRUDService;
            this.relationInfoService = relationInfoService;
        }

        public override List<Error> Validate(JObject input, Field field)
        {
            List<Error> errors = new List<Error>();
            if (field.Options != null)
            {
                JToken value = input[field.Name];
                if (input[field.Name] == null)
                {
                    return errors;
                }

                RelationInfo relationInfo = relationInfoService.GetFromOptions(field, input);
                if (!string.IsNullOrEmpty(relationInfo.LookupCollection))
                {
                    FilterDefinition<BsonDocument> filter = FilterDefinition<BsonDocument>.Empty;

                    if (relationInfo.IsMultiple)
                    {
                        Builders<BsonDocument>.Filter.In<BsonObjectId>("_id", relationInfo.Values);
                    }
                    else
                    {
                        filter = Builders<BsonDocument>.Filter.Eq("_id", relationInfo.Values.FirstOrDefault());
                    }
                    long match = cRUDService.Count(relationInfo.LookupCollection, filter);

                    if (match < 1)
                    {
                        errors.Add(new Error()
                        {
                            Code = "REL-01",
                            Title = "Item not found on lookup collection",
                            Description = ""
                        });
                    }
                }

                return errors;
            }

            return new List<Error>(errors);
        }
    }
}