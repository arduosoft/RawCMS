
using Elasticsearch.Net;
using Nest;
using RawCMS.Plugins.FullText.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Newtonsoft.Json;
using Nest.JsonNetSerializer;

namespace RawCMS.Test
{
    public class ElasticFullText
    {
        public class LogDocument
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string Body { get; set; }
        }

        //public class MyFirstCustomJsonNetSerializer : ConnectionSettingsAwareSerializerBase
        //{
        //    public MyFirstCustomJsonNetSerializer(IElasticsearchSerializer builtinSerializer, IConnectionSettingsValues connectionSettings)
        //        : base(builtinSerializer, connectionSettings) { }

        //    protected override JsonSerializerSettings CreateJsonSerializerSettings() =>
        //        new JsonSerializerSettings
        //        {
        //            NullValueHandling = NullValueHandling.Include
        //        };

        //    protected override void ModifyContractResolver(ConnectionSettingsAwareContractResolver resolver) =>
        //        resolver.NamingStrategy = new SnakeCaseNamingStrategy();
        //}

        [Fact]
        public void CRUD()
        {

            var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9300"));
            var connection = new HttpConnection();
            var connectionSettings =
            new ConnectionSettings(pool, connection, (serializer, settings) =>
            {


                //return new MyFirstCustomJsonNetSerializer(serializer, settings);
                return JsonNetSerializer.Default(serializer, settings);
            })
           // new ConnectionSettings(pool, connection)
            .DisableAutomaticProxyDetection()
            
            .EnableHttpCompression()
            .DisableDirectStreaming()
            .PrettyJson()
            .RequestTimeout(TimeSpan.FromMinutes(2));
            
            

            var client = new ElasticClient(connectionSettings);
            
            var service = new ElasticFullTextService(client);

            var indexName = Guid.NewGuid().ToString();

            service.CreateIndex(indexName);

            LogDocument doc=null;
            for (int i = 0; i < 500; i++)

            {

                doc = new LogDocument()
                {
                    Body = $"My first document into index, position is number{i}"
                };
                service.AddDocument(indexName, doc);
            }

            

            var item = service.GetDocumentRaw(indexName, doc.Id.ToString());
            Assert.Equal(item["Id"],doc.Id.ToString());

            //search



            var items = service.SearchDocumentsRaw(indexName, "number1*", 0,140);
            Assert.Equal(items.Count, 111);

        }
    }
}
