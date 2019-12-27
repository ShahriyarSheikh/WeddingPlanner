using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using wplanr.DbContext.IDatabaseContext;
using wplanr.DTO.Interfaces;

namespace wplanr.Repository.Adapter
{
    public class MongoAdapter : IMongoAdapter
    {
        private readonly IMongoContext _mongoContext;
        private readonly ILogger<MongoAdapter> _logger;

        public MongoAdapter(IMongoContext mongoContext, ILogger<MongoAdapter> logger)
        {
            _mongoContext = mongoContext;
            _logger = logger;
        }

        public async Task<bool> InsertOneAsync<T>(T model, string tableName)
        {
            await RetryPolicyAsync(async () => await _mongoContext.GetCollection<T>(tableName).InsertOneAsync(model));
            return true;
        }

        public async Task<bool> InsertManyAsync<T>(List<T> model, string tableName)
        {
            await RetryPolicyAsync(async () => await _mongoContext.GetCollection<T>(tableName).InsertManyAsync(model));
            return true;
        }

        public async Task<List<T>> FindManyAsync<T>(Expression<Func<T, bool>> query, string tableName)
        {
            var result = default(List<T>);
            var filter = Builders<T>.Filter.Where(query);
            await RetryPolicyAsync(async () => result = (await _mongoContext.GetCollection<T>(tableName).FindAsync(filter)).ToList());
            return result;
        }

        public async Task<List<T>> FindManyAsync<T>(FilterDefinition<T> filter, ProjectionDefinition<T> projection, string tableName)
        {
            var result = default(List<T>);
            await RetryPolicyAsync(async () => result = await _mongoContext.GetCollection<T>(tableName).Find(filter).Project<T>(projection).ToListAsync());
            return result;
        }

        public async Task<T> FindOneAsync<T>(Expression<Func<T, bool>> query, string tableName)
        {
            var result = default(T);
            var filter = Builders<T>.Filter.Where(query);
            await RetryPolicyAsync(async () => result = (await _mongoContext.GetCollection<T>(tableName).FindAsync(filter)).FirstOrDefault());
            return result;
        }

        public async Task<bool> UpdateOneAsync<T>(string tableName, Expression<Func<T, bool>> query, bool upsert, params UpdateDefinition<T>[] updatePairs)
        {
            var result = default(bool);
            var filter = Builders<T>.Filter.Where(query);
            var update = Builders<T>.Update.Combine(updatePairs);
            await RetryPolicyAsync(async () => result = (await _mongoContext.GetCollection<T>(tableName).UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = upsert })).IsModifiedCountAvailable);
            return result;
        }

        public async Task<bool> UpdateManyAsync<T>(string tableName, Expression<Func<T, bool>> query, bool upsert, params UpdateDefinition<T>[] updatePairs)
        {
            var result = default(bool);
            var filter = Builders<T>.Filter.Where(query);
            var update = Builders<T>.Update.Combine(updatePairs);
            await RetryPolicyAsync(async () => result = (await _mongoContext.GetCollection<T>(tableName).UpdateManyAsync(filter, update, new UpdateOptions { IsUpsert = upsert })).IsModifiedCountAvailable);
            return result;
        }

        public async Task<bool> ReplaceOneAsync<T>(Expression<Func<T, bool>> query, T model, string tableName, bool insertIfNotExists = false)
        {
            var result = default(bool);
            var filter = Builders<T>.Filter.Where(query);
            await RetryPolicyAsync(async () => result = (await _mongoContext.GetCollection<T>(tableName).ReplaceOneAsync(filter, model, new UpdateOptions { IsUpsert = insertIfNotExists })).IsModifiedCountAvailable);
            return result;
        }

        public async Task<bool> DeleteManyAsync<T>(Expression<Func<T, bool>> query, string tableName)
        {
            var result = default(bool);
            await RetryPolicyAsync(async () => result = (await _mongoContext.GetCollection<T>(tableName).DeleteManyAsync<T>(query)).DeletedCount > 0);
            return result;
        }

        public async Task<bool> DeleteOneAsync<T>(Expression<Func<T, bool>> query, string tableName)
        {
            var result = default(bool);
            await RetryPolicyAsync(async () => result = (await _mongoContext.GetCollection<T>(tableName).DeleteOneAsync<T>(query)).DeletedCount > 0);
            return result;
        }

        public async Task<long> EstimatedDocumentsCountAsync<T>(string tableName)
        {
            var result = default(long);
            await RetryPolicyAsync(async () => result = await _mongoContext.GetCollection<T>(tableName).EstimatedDocumentCountAsync());
            return result;
        }

        private async Task RetryPolicyAsync(Func<Task> func)
        {
            var retryPolicy = Policy
              .Handle<Exception>(ex => !(ex.HResult == -2146233079/*-2146233088*/) || !(ex.HResult == -2145844839))
              .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, timeSpan) =>
              {
                  _logger.LogCritical(ex, "Failed to execute. Retrying in " + timeSpan.ToString() + "seconds");
              });


            var retryResponse = await retryPolicy.ExecuteAndCaptureAsync(func);

            if (retryResponse.Outcome == OutcomeType.Failure)
            {
                _logger.LogError(retryResponse.FinalException.Message, "Error# " + retryResponse.FinalException);
                throw new Exception(retryResponse.FinalException.Message, retryResponse.FinalException);
            }
        }
    }

    public static class Update<T>
    {
        public static UpdateDefinition<T> Of<V>(Expression<Func<T, V>> field, V value)
        {
            return Builders<T>.Update.Set(field, value);
        }
    }
}
