using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using RawCMS.Library.Lambdas;
using RawCMS.Plugins.FullText.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace RawCMS.Plugins.FullText.Lambdas
{
    public class FullTextLambda : PostSaveLambda
    {
        public override string Name => "FullTextMapping";

        public override string Description => this.Name;

        public Dictionary<string, FullTextFilter> CrudFilters { get; set; }

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

                var list = new List<string>()
                {
                    "_id" //id is alway neededs
                };

                //if empty add all
                if (filter.IncludedField == null || filter.IncludedField.Count == 0)
                {
                    list.AddRange(item.Properties().Select(p => p.Name).Distinct().ToList());
                }

                foreach (var field in filter.IncludedField)
                {
                    searchDocument[field] = item[field];
                }

                this.fullTextService.AddDocumentRaw(GetIndexName(collection), searchDocument);
            }
        }

        private static MD5 md5 = MD5.Create();

        private static string GetIndexName(string collection)
        {
            var str = "dix_" + Convert.ToBase64String(md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(collection.ToLower()))).Replace("=", "");
            return str.ToLower().Replace("=", "");
        }

        private void LoadCrudFilters()
        {
            this.CrudFilters = new Dictionary<string, FullTextFilter>();

            foreach (var collection in EntityValidation.Entities.Values)
            {
                if (collection.PluginConfiguration.TryGetValue("FullTextPlugin", out JObject textSettings))
                {
                    this.CrudFilters[collection.CollectionName] = textSettings.ToObject<FullTextFilter>();
                }

                var indexName = GetIndexName(collection.CollectionName);
                if (!this.fullTextService.IndexExists(indexName))
                {
                    this.fullTextService.CreateIndex(indexName);
                }
            }
        }
    }
}