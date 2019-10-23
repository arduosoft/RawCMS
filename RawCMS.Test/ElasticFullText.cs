
using Elasticsearch.Net;
using Nest;
using RawCMS.Plugins.FullText.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RawCMS.Test
{
    public class ElasticFullText
    {
        public class LogDocument
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string Body { get; set; }
        }

        [Fact]
        public void CRUD()
        {

            var uri = new Uri("http://localhost:9300");
            var connectionConfiguration = new ConnectionSettings(uri)
            .DisableAutomaticProxyDetection()
            .EnableHttpCompression()
            .DisableDirectStreaming()
            .PrettyJson()
            .RequestTimeout(TimeSpan.FromMinutes(2));
            
            

            var client = new ElasticClient(connectionConfiguration);
            
            var service = new ElasticFullTextService(client);

            var indexName = Guid.NewGuid().ToString();

            service.CreateIndex(indexName);

            var doc = new LogDocument()
            {
                Body = "My first document into index"
            };

            service.AddDocument(indexName, doc);

            var item = service.GetDocumentRaw(indexName, doc.Id.ToString());
            Assert.Equal(item["Id"],doc.Id.ToString());

        }
    }
}
