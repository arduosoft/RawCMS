using Newtonsoft.Json.Linq;
using RawCMS.Library.Schema;
using RawCMS.Library.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Service
{
    public class CollectionMetadataService : ICollectionMetadata
    {
        private readonly CRUDService _service;

        private ICollection<CollectionSchema> collectionSchemas { get; set; }

       public CollectionMetadataService(CRUDService Service)
        {
            this._service = Service;
            LoadCollections();
        }


        public ICollection<CollectionSchema> GetCollectionSchemas()
        {
            return this.collectionSchemas;
        }

        public void LoadCollections()
        {
            this.collectionSchemas = new List<CollectionSchema>();
            JArray dbEntities = this._service.Query("_schema", new DataModel.DataQuery()
            {
                PageNumber = 1,
                PageSize = int.MaxValue,
                RawQuery = null
            }).Items;

            foreach (JToken item in dbEntities)
            {
                CollectionSchema schema = item.ToObject<CollectionSchema>();
                this.collectionSchemas.Add(schema);   
            }
        }
    }
}
