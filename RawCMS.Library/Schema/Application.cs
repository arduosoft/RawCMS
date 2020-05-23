using Newtonsoft.Json;
using System;

namespace RawCMS.Library.Schema
{
    public abstract class EntityBase
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
    }

    public class Application : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] AllowedRoles { get; set; }

        public Guid PublicId { get; set; }
    }
}