using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Schema;

namespace RawCMS.Library.Service
{
    public class RelationInfo
    {
        public bool IsMultiple { get; set; }
        public string LookupCollection { get; set; }
        public List<BsonObjectId> Values { get; set; }
    }

    public class RelationInfoService
    {
        public RelationInfo GetFromOptions(Field field, JObject input)
        {
            RelationInfo relation = new RelationInfo()
            {
                IsMultiple = IsMultiple(field),
                LookupCollection = GetLookupCollectionName(field),
                Values = GetLookupValue((JValue)input[field.Name])
            };
            return relation;
        }

        private List<BsonObjectId> GetLookupValue(JValue input)
        {
            if (input == null)
            {
                return new List<BsonObjectId>();
            }

            if (input.Type == JTokenType.Array)
            {
                return input.Value<List<string>>().Select(x => BsonObjectId.Create(x)).ToList();
            }
            return new List<BsonObjectId>() { BsonObjectId.Create(input.Value<string>()) };
        }

        private bool IsMultiple(Field field)
        {
            string[] positiveMatch = new string[] { "1", "true" };
            var optionVal = field.Options["Multiple"];
            if (optionVal != null && optionVal.HasValues)
            {
                return positiveMatch.Any(x => x.Equals(field.Options["Multiple"].ToString(), StringComparison.InvariantCultureIgnoreCase));
            }
            return false;
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