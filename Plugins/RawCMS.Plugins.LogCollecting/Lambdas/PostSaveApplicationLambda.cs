using Newtonsoft.Json.Linq;
using RawCMS.Library.Lambdas;
using RawCMS.Plugins.FullText.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Plugins.LogCollecting.Lambdas
{
    public class PostSaveApplicationLambda : PostSaveLambda
    {
        private readonly FullTextService fullText;
        public PostSaveApplicationLambda(FullTextService fullTextService)
        {
            fullText = fullTextService;
        }

        public override string Name => "PostSaveApplicationLambda";

        public override string Description => "Create full text index post save application";

        public override void Execute(string collection, ref JObject item, ref Dictionary<string, object> dataContext)
        {
            if (collection == "application")
            {
                var id = item["_id"].Value<string>();
                if (!string.IsNullOrEmpty(id))
                {
                    var indexName = "log_" + id;
                    fullText.CreateIndex(indexName);
                }
            }
        }
    }
}
