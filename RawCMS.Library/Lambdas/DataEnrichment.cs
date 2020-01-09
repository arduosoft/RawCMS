using Newtonsoft.Json.Linq;
using RawCMS.Library.Core.Enum;
using System.Collections.Generic;

namespace RawCMS.Library.Lambdas
{
    public abstract class DataEnrichment : DataProcessLambda
    {
        public override PipelineStage Stage => PipelineStage.PostOperation;

        public override DataOperation Operation => DataOperation.Read;

        public abstract string MetadataName { get; }

        public override void Execute(string collection, ref JObject item, ref Dictionary<string, object> dataContext)
        {
            JObject meta = EnrichMetadata(collection, item, dataContext);
            if (meta != null && meta.HasValues)
            {
                if (item["_metadata"] == null)
                {
                    item["_metadata"] = new JObject();
                }
                item["_metadata"][MetadataName] = meta;
            }
        }

        public abstract JObject EnrichMetadata(string collection, JObject item, Dictionary<string, object> dataContext);
    }
}