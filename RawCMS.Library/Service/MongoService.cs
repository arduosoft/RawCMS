using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RawCMS.Library.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RawCMS.Library.Service
{
    public class MongoService
    {

        private readonly MongoSettings _settings;
        public MongoService(IOptions<MongoSettings> settings)
        {
            this._settings = settings.Value;
        }
        public MongoClient GetClient()
        {
            string connectionString = "mongodb://"+_settings.Host+":"+_settings.Port;

            var client = new MongoClient(connectionString);

            return client;
        }

        public IMongoDatabase GetDatabase()
        {
            return this.GetClient().GetDatabase(_settings.DBName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return this.GetClient().GetDatabase(_settings.DBName).GetCollection<T>(name);
        }
    }
}
