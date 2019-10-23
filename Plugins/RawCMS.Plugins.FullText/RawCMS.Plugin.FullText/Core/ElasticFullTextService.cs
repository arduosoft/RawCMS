using System;
using System.Collections.Generic;
using System.Text;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json.Linq;

namespace RawCMS.Plugins.FullText.Core
{

    public class JObjectResponse : ElasticsearchResponse<JObject>
    {

    }


    public class StringResponse : ElasticsearchResponse<string>
    {        
    }



    public class ElasticFullTextService : FullTextService
    {
        protected ElasticClient client;

        public ElasticFullTextService(ElasticClient client)
        {
            this.client = client;    
        }

       
        public override void AddDocumentRaw(string indexname, object data)
        {
            var jobj = JObject.FromObject(data);

            //Resolve object id. elastic whant an "id" field, mongo uses _id... 
            if (!jobj.ContainsKey("Id"))
            {
                if (jobj.ContainsKey("_id"))
                {
                    jobj["Id"] = jobj["_id"];
                }
                else if (jobj.ContainsKey("id"))
                {
                    jobj["Id"] = jobj["id"];
                }
                else 
                {
                    jobj["Id"] = Guid.NewGuid();
                }
            }
            client.LowLevel.Index<JObjectResponse>(indexname,  jobj["Id"].ToString(), PostData.String(jobj.ToString()));
        }

        public override void CreateIndex(string name)
        {
            var resp=client.Indices.Create(name);
            if (!resp.IsValid && resp.ServerError != null && resp.ServerError.Status != 200)
            {
                throw new Exception($"Error during init {resp.ServerError.Error.ToString()}");//TODO: check if the string commutation of error prduces a nice message
            }
        }

        public override JObject GetDocumentRaw(string indexname, string docId)
        {
            var response = client.LowLevel.Get< JObjectResponse > (indexname, docId);
            var json=Encoding.UTF8.GetString(response.ResponseBodyInBytes);
            return JObject.Parse(json);
        }

        public override bool IndexExists(string name)
        {
            var ex= this.client.Indices.Exists(name);
            return ex.Exists;
        }

        public override List<JObject> SearchDocumentsRaw(string indexname, string searchQuery, int start, int size)
        {
            var searchResponse = client.Search<JObject>(s => s
                         .Size(size)
                         .Skip(start)
                         .Query(q => q.QueryString(

                             qs => qs.Query(searchQuery)
                             .AllowLeadingWildcard(true)
                             )

                         )
                     );

            return new List<JObject>(searchResponse.Documents);
        }
    }
}
