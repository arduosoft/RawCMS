using Newtonsoft.Json.Linq;
using RawCMS.Library.Lambdas;
using RawCMS.Plugins.FullText.Lambdas;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace RawCMS.Plugins.FullText.Core
{
    public class FullTextUtilityService
    {
        private static Dictionary<string, FullTextFilter> CrudFilters { get; set; } = new Dictionary<string, FullTextFilter>();
        private static MD5 md5 = MD5.Create();
        protected readonly FullTextService fullTextService;

        public FullTextUtilityService(FullTextService fullTextService)
        {
            this.fullTextService = fullTextService;
            LoadCrudFilters();
        }

        public FullTextFilter GetFilter(string collection)
        {
            if (CrudFilters.TryGetValue(collection, out FullTextFilter filter))
            {
                return filter;
            }
            return null;
        }

        public string GetIndexName(string collection)
        {
            string str = "dix_" + Convert.ToBase64String(md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(collection.ToLower()))).Replace("=", "");
            return str.ToLower().Replace("=", "");
        }

        private void LoadCrudFilters()
        {
            lock (CrudFilters)
            {
                CrudFilters = new Dictionary<string, FullTextFilter>();

                foreach (Library.Schema.CollectionSchema collection in EntityValidation.Entities.Values)
                {
                    if (collection.PluginConfiguration.TryGetValue("FullTextPlugin", out JObject textSettings))
                    {
                        CrudFilters[collection.CollectionName] = textSettings.ToObject<FullTextFilter>();
                    }

                    EnsureIndex(collection);
                }
            }
        }

        public void EnsureIndex(Library.Schema.CollectionSchema collection)
        {
            string indexName = GetIndexName(collection.CollectionName);
            if (!fullTextService.IndexExists(indexName))
            {
                fullTextService.CreateIndex(indexName);
            }
        }
    }
}