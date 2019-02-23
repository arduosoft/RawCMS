using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RawCMS.Library.DataModel;
using System;

namespace RawCMS.Library.Service
{
    public class MongoService
    {
        private readonly MongoSettings _settings;
        private readonly ILogger logger;
        private readonly MongoClient client;

        public MongoService(IOptions<MongoSettings> settings, ILogger logger)
        {
            this.logger = logger;
            _settings = settings.Value;
            client = new MongoClient(_settings.ConnectionString);
            if (string.IsNullOrEmpty(_settings.DBName))
            {
                var url = MongoUrl.Create(_settings.ConnectionString);

                _settings.DBName = url.DatabaseName;
            }
        }

        public MongoClient GetClient()
        {
            return client;
        }

        public IMongoDatabase GetDatabase()
        {
            return GetClient().GetDatabase(_settings.DBName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return GetClient().GetDatabase(_settings.DBName).GetCollection<T>(name);
        }
    }
}