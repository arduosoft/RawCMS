using System.Collections.Generic;
using MongoDB.Bson;

namespace RawCMS.Library.Service
{
    public class RelationInfo
    {
        public bool IsMultiple { get; set; }
        public string LookupCollection { get; set; }
        public List<BsonObjectId> Values { get; set; }
    }
}