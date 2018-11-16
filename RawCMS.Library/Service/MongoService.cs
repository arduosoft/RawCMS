using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RawCMS.Library.DataModel;

namespace RawCMS.Library.Service
{
    public class MongoService
    {
        private readonly MongoSettings _settings;

        public MongoService(IOptions<MongoSettings> settings)
        {
            _settings = settings.Value;
        }

        public MongoClient GetClient()
        {
            string connectionString = "mongodb://" + _settings.Host + ":" + _settings.Port;

            MongoClient client = new MongoClient(connectionString);

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