using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace wplanr.DTO.Interfaces
{
    public interface IMongoAdapter
    {
        Task<List<T>> FindManyAsync<T>(Expression<Func<T, bool>> query, string tableName);
        Task<List<T>> FindManyAsync<T>(FilterDefinition<T> filter, ProjectionDefinition<T> projection, string tableName);
        Task<T> FindOneAsync<T>(Expression<Func<T, bool>> query, string tableName);
        Task<bool> InsertOneAsync<T>(T model, string tableName);
        Task<bool> InsertManyAsync<T>(List<T> model, string tableName);
        Task<bool> DeleteManyAsync<T>(Expression<Func<T, bool>> query, string tableName);
        Task<bool> DeleteOneAsync<T>(Expression<Func<T, bool>> query, string tableName);
        Task<long> EstimatedDocumentsCountAsync<T>(string tableName);
        Task<bool> UpdateOneAsync<T>(string tableName, Expression<Func<T, bool>> query, bool upsert, params UpdateDefinition<T>[] updatePairs);
        Task<bool> UpdateManyAsync<T>(string tableName, Expression<Func<T, bool>> query, bool upsert, params UpdateDefinition<T>[] updatePairs);
        Task<bool> ReplaceOneAsync<T>(Expression<Func<T, bool>> query, T model, string tableName, bool insertIfNotExists = false);

    }
}
