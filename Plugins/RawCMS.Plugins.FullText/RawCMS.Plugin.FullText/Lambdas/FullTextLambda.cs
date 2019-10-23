using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using RawCMS.Library.Lambdas;
using RawCMS.Library.Schema;
using RawCMS.Plugins.FullText.Core;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RawCMS.Plugins.FullText.Lambdas
{
    public class FullTextLambda : PostSaveLambda
    {
        public override string Name => "FullTextMapping";

        public override string Description => this.Name;

        public  Dictionary<string,FullTextFilter> CrudFilters { get; set; }

        protected readonly FullTextService fullTextService;

        public FullTextLambda(FullTextService fullTextService)
        {
            this.fullTextService = fullTextService;
        }

        public override void Execute(string collection, ref JObject item, ref Dictionary<string, object> dataContext)
        {
            if (this.CrudFilters == null)
            {
                LoadCrudFilters();
            }


            if (CrudFilters.TryGetValue(collection, out FullTextFilter filter))
            {
                JObject searchDocument = new JObject();
                foreach (var field in filter.IncludedField)
                {
                    searchDocument[field] = item[field];
                }
                this.fullTextService.AddDocumentRaw(GetIndexName(collection), searchDocument);
            }
        }

        static MD5 md5 = MD5.Create();
        private static string GetIndexName(string collection)
        {            
            
            return "dix_" + System.Text.Encoding.UTF8.GetString(md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes( collection.ToLower())));
        }

        private void LoadCrudFilters()
        {

            foreach(var collection  in EntityValidation.Entities.Values)
            { 
                if (collection.PluginConfiguration.TryGetValue("FullTextPlugin", out JObject textSettings))
                {
                    this.CrudFilters[collection.CollectionName] = textSettings.ToObject<FullTextFilter>();
                }
            }
        }
    }
}
