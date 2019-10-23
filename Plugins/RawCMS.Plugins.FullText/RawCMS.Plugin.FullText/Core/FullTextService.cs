using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RawCMS.Plugins.FullText.Core
{
    public abstract class FullTextService
    {

        public abstract void CreateIndex(string name);

        public abstract bool IndexExists(string name);

        public virtual void AddDocument<T>(string indexname, T data)
        {
            this.AddDocumentRaw(indexname, data);
        }

        public abstract void AddDocumentRaw(string indexname, object data);

        public virtual T GetDocument<T>(string indexname, string docId)
        {
            var raw = GetDocumentRaw(indexname, docId);
            return raw.ToObject<T>();
        }

        public abstract JObject  GetDocumentRaw(string indexname, string docId);

        public virtual List<T> SearchDocuments<T>(string indexname, string searchQuery, int start, int size)
        {
            List<JObject> data = SearchDocumentsRaw(indexname, searchQuery, start, size);

            var items = data.AsQueryable().Select(x => x.ToObject<T>()).ToList();
            return items;
        }

        public abstract List<JObject> SearchDocumentsRaw(string indexname, string searchQuery, int start, int size);
    }
}
