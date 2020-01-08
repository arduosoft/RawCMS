using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Schema;

namespace RawCMS.Library.Service
{
    public class RelationInfoService
    {
        public RelationInfo GetFromOptions(Field field, JObject input)
        {
            RelationInfo relation = new RelationInfo()
            {
                IsMultiple = IsMultiple(field),
                LookupCollection = GetLookupCollectionName(field),
                Values = GetLookupValue(input[field.Name] as JToken)
            };
            return relation;
        }

        private List<BsonObjectId> GetLookupValue(JToken input)
        {
            if (input == null)
            {
                return new List<BsonObjectId>();
            }

            // Array
            if (input.Type == JTokenType.Array)
            {
                return input.Values<string>().Select(x => BsonObjectId.Create(x)).ToList();
            }

            // Single value
            return new List<BsonObjectId>() { BsonObjectId.Create(input.Value<string>()) };
        }

        private bool IsMultiple(Field field)
        {
            return ((field.Options["Multiple"] as JValue)?.Value as bool?).GetValueOrDefault(false);
        }

        private string GetLookupCollectionName(Field field)
        {
            if (field.Options["Collection"] != null)
            {
                return field.Options["Collection"].ToString();
            }
            return null;
        }
    }
}