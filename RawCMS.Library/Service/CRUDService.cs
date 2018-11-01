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
using RawCMS.Library.Core;
using RawCMS.Library.Core.Exceptions;
using RawCMS.Library.Core.Interfaces;

namespace RawCMS.Library.Service
{
    public class CRUDService:IRequireApp
    {
        private readonly MongoService _mongoService;
        private readonly MongoSettings _settings;
        private  AppEngine lambdaManager;
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

            InvokeValidation(newitem, collection);


            EnsureCollection(collection);



            InvokeProcess(collection, ref newitem, SavePipelineStage.PreSave);

            var json = newitem.ToString();
            _mongoService.GetCollection<BsonDocument>(collection).InsertOne(BsonSerializer.Deserialize<BsonDocument>(json));
            InvokeProcess(collection, ref newitem, SavePipelineStage.PostSave);

            return JObject.Parse(newitem.ToJson(js));


        }


        private void InvokeValidation(JObject newitem, string collection)
        {
            var errors = this.Validate(newitem, collection);
            if (errors.Count > 0)
            {
                throw new ValidationException(errors, null);

            }
        }

        private void InvokeProcess(string collection, ref JObject item, SavePipelineStage save)
        {
            var processhandlers = lambdaManager.Lambdas
                .Where(x => x is DataProcessLambda)
                .Where(x => ((DataProcessLambda)x).Stage == save)
                .ToList();

            foreach (DataProcessLambda h in processhandlers)
            {
                h.Execute(collection, ref item);
            }

        }


        public JObject Update(string collection, JObject item, bool replace)
        {

            //TODO: why do not manage validation as a simple presave process?
            InvokeValidation(item, collection);

            //TODO: create collection if not exists
            EnsureCollection(collection);

            var filter = Builders<BsonDocument>.Filter.Eq("_id", BsonObjectId.Create(item["_id"].Value<string>()));



            InvokeProcess(collection, ref item, SavePipelineStage.PreSave);

            //insert id (mandatory)
            BsonDocument doc = BsonDocument.Parse(item.ToString());
            doc["_id"] = BsonObjectId.Create(item["_id"].Value<string>());

            //set into "incremental" update mode
            doc = new BsonDocument("$set", doc);

            //_mongoService.GetCollection<BsonDocument>(collection).Up

            UpdateOptions o = new UpdateOptions()
            {
                IsUpsert = true,
                BypassDocumentValidation = true
            };







            if (replace)
            {

                _mongoService.GetCollection<BsonDocument>(collection).ReplaceOne(filter, doc, o);
                InvokeProcess(collection, ref item, SavePipelineStage.PostSave);

            }
            else
            {
                BsonDocument dbset = new BsonDocument("$set", doc);
                _mongoService.GetCollection<BsonDocument>(collection).UpdateOne(filter, dbset, o);
                InvokeProcess(collection, ref item, SavePipelineStage.PostSave);

            }
            return JObject.Parse(item.ToJson(js));


        }

        public void EnsureCollection(string collection)
        {
            try
            {

                _mongoService.GetDatabase().CreateCollection(collection);

            }
            catch
            {
                //Check for collection exists...
            }
        }

        public bool Delete(string collection, string id)
        {

            EnsureCollection(collection);

            var filter = Builders<BsonDocument>.Filter.Eq("_id", BsonObjectId.Create(id));


            var result = _mongoService.GetCollection<BsonDocument>(collection).DeleteOne(filter);


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

        public ItemList Query(string collection, DataQuery query)
        {


            FilterDefinition<BsonDocument> filter = FilterDefinition<BsonDocument>.Empty;
            if (query.RawQuery != null)
            {
                filter = new JsonFilterDefinition<BsonDocument>(query.RawQuery);
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

            return new ItemList(JArray.Parse(json), (int)count, query.PageNumber, query.PageSize);
        }







        public List<Error> Validate(JObject item, string collection)
        {
            List<Error> result = new List<Error>();
            result.AddRange(ValidateGeneric(item, collection));
            result.AddRange(ValidateSpecific(item, collection));


            return result;

        }


        private  List<Error> ValidateSpecific(JObject item, string collection)
        {
            List<Error> result = new List<Error>();

            var labdas = this.lambdaManager.Lambdas
                .Where(x => x is CollectionValidationLambda)
                .Where(x => ((CollectionValidationLambda)x).TargetCollections.Contains(collection))
                .ToList();

            foreach (CollectionValidationLambda lambda in labdas)
            {
                var errors = lambda.Validate(item);
                result.AddRange(errors);
            }

            return result;


        }
        private  List<Error> ValidateGeneric(JObject item, string collection)
        {
            List<Error> result = new List<Error>();

            var labdas = lambdaManager.Lambdas
                .Where(x => x is SchemaValidationLambda).ToList();

            foreach (SchemaValidationLambda lambda in labdas)
            {
                var errors = lambda.Validate(item, collection);
                result.AddRange(errors);
            }

            return result;
        }

        public void SetAppEngine(AppEngine manager)
        {
            this.lambdaManager = manager;
        }
    }
}
