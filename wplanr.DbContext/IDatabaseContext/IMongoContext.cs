using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace wplanr.DbContext.IDatabaseContext
{
    public interface IMongoContext
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
