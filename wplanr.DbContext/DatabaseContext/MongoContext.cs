using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using wplanr.Core.ConfigurationModels;
using wplanr.DbContext.IDatabaseContext;

namespace wplanr.DbContext.MongoContext
{

    /// <summary>
    /// https://www.dotnetcurry.com/aspnet-mvc/1267/using-mongodb-nosql-database-with-aspnet-webapi-core
    /// </summary>
    /// 
    public class MongoContext : IMongoContext
    {
        private readonly MongoClient _client;
        public readonly IMongoDatabase _db;
        private readonly MongoConnectionStrings _mongoConnectionStrings;

        public MongoContext(IOptions<MongoConnectionStrings> mongoConnectionStrings)
        {
            _mongoConnectionStrings = mongoConnectionStrings.Value;
            _client = new MongoClient(_mongoConnectionStrings.MongoConnectionString);
            _db = _client.GetDatabase(_mongoConnectionStrings.MongoDbName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _db.GetCollection<T>(collectionName);
        }
    }
}
