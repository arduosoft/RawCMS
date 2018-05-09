using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using RawCMS.Library.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RawCMS.Library.Service
{
    public class CRUDService
    {
        private readonly MongoService _mongoService;
        private readonly MongoSettings _settings;

        JsonWriterSettings js = new JsonWriterSettings()
        {
            OutputMode = JsonOutputMode.Strict,
            GuidRepresentation = GuidRepresentation.CSharpLegacy
        };
        public CRUDService(MongoService mongoService, IOptions<MongoSettings> settings)
        {
            this._mongoService = mongoService;
            this._settings = settings.Value;
        }

        public JObject Insert(string collection, JObject newitem)
        {
            //TODO: create collection if not exists
            try
            {
                _mongoService.GetDatabase().CreateCollection(collection);
            }
            catch 
            {
                //Check for collection exists...
            }

            var json = newitem.ToString();
            _mongoService.GetCollection<BsonDocument>(collection).InsertOne(BsonSerializer.Deserialize<BsonDocument>(json));

            return JObject.Parse(newitem.ToJson(js));


        }

        public JObject Update(string collection, JObject item,bool replace)
        {
            //TODO: create collection if not exists
            try
            {
                _mongoService.GetDatabase().CreateCollection(collection);
            }
            catch
            {
                //Check for collection exists...
            }

            var filter = Builders<BsonDocument>.Filter.Eq("_id", BsonObjectId.Create(item["_id"].Value<string>()));

            

            BsonDocument doc = BsonDocument.Parse(item.ToString());

            doc["_id"] = BsonObjectId.Create(item["_id"].Value<string>());

           

            //_mongoService.GetCollection<BsonDocument>(collection).Up

            UpdateOptions o = new UpdateOptions();
            o.IsUpsert = true;
            o.BypassDocumentValidation = true;


            if (replace)
            {

                _mongoService.GetCollection<BsonDocument>(collection).ReplaceOne(filter, doc, o);

            }
            else
            {
                BsonDocument dbset = new BsonDocument("$set", doc);
                _mongoService.GetCollection<BsonDocument>(collection).UpdateOne(filter, dbset, o);

            }
            return JObject.Parse(item.ToJson(js));


        }


        public bool Delete(string collection, string id)
        {
           

            var filter = Builders<BsonDocument>.Filter.Eq("_id", BsonObjectId.Create(id));   

            UpdateOptions o = new UpdateOptions();
            o.IsUpsert = true;
            o.BypassDocumentValidation = true;




           var result= _mongoService.GetCollection<BsonDocument>(collection).DeleteOne(filter);


            return result.DeletedCount == 1;


        }


        public JObject Get(string collection, string id)
        {

            var filter = Builders<BsonDocument>.Filter.Eq("_id", BsonObjectId.Create(id));


            var results = _mongoService
                .GetCollection<BsonDocument>(collection)
                .Find<BsonDocument>(filter);

            var list = results.ToList();

            var item = list.FirstOrDefault();
            var json = "{}";
            //sanitize id format
            if (item != null)
            {
                 item["_id"] = item["_id"].ToString();
                 json = item.ToJson(js);
            }
            
           

            return JObject.Parse(json);
        }

        public ItemList Query(string collection,DataQuery query)
        {
          

            FilterDefinition<BsonDocument> filter = FilterDefinition<BsonDocument>.Empty;
            if (query.RawQuery != null)
            {
                filter=   new JsonFilterDefinition<BsonDocument>(query.RawQuery);
            }

            var results = _mongoService
                .GetCollection<BsonDocument>(collection).Find<BsonDocument>(filter)
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Limit(query.PageSize);

            var count = _mongoService
                .GetCollection<BsonDocument>(collection).Find<BsonDocument>(filter).Count();

            var list = results.ToList();

            //sanitize id format
            foreach (var item in list)
            {
                item["_id"] = item["_id"].ToString();
            }
           
            
            var json = list.ToJson(js);
            
            return  new ItemList(JArray.Parse(json), (int)count,query.PageNumber, query.PageSize);
        }

    }
}
