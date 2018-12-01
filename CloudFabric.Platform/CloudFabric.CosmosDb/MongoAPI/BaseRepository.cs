using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;

namespace CloudFabric.CosmosDb.MongoAPI
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseDocument
    {
        ILogger<BaseRepository<T>> _logger;
        protected BaseCosmoDbContext _dbContext;
        public abstract string CollectionName { get; }
        public abstract string DatabaseName { get; }

        protected readonly IMongoDatabase Database;
        public readonly IMongoCollection<T> Collection;


        protected abstract int NumberOfPartitions { get; }
        protected abstract string PartitonPrefix { get; }

        public BaseRepository(BaseCosmoDbContext dbContext, ILogger<BaseRepository<T>> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
            Database = _dbContext.GetDatabase(DatabaseName);
            Collection = Database.GetCollection<T>(CollectionName);
        }
        public async Task<T> CreateAsync(T document)
        {
            _dbContext.CreateOrUpdate(document);

            document.Id = ObjectId.GenerateNewId();
            document.PartitionKey = GetPartitionKey(document.Id.ToString());

            await Collection.InsertOneAsync(document);
            return document;
        }

        public async Task<bool> DeleteAsync(ObjectId id)
        {
            try
            {
                bool success = false;
                var _dictionary =  await DeleteAsync(new List<ObjectId> { id });
                _dictionary.TryGetValue(id, out success);

                return success;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.StackTrace);
                return false;
            }
        }
        public async Task<Dictionary<ObjectId, bool>> DeleteAsync(List<ObjectId> ids)
        {
            var _dictionary = new Dictionary<ObjectId, bool>();

            try
            {
                List<T> documents = new List<T>();

                var disctinctIds = ids.Distinct();

                foreach (var id in disctinctIds)
                {
                    try
                    {
                        var doc = await GetByIdAsync(id);

                        if (doc != null)
                        {
                            if (!doc.IsDeleted)
                            {
                                doc.IsDeleted = true;
                                await UpdateAsync(doc);
                            }
                            _dictionary.Add(id, true);
                        }
                        else
                        {
                            _dictionary.Add(id, false);
                        }
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError("Failed to delete the document with ID : " + id + " " + ex.Message + " " + ex.StackTrace);
                        _dictionary.Add(id, false);
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Deleting documents failed");
                return null;
            }
            return _dictionary;
        }

        public async Task<T> GetByIdAsync(ObjectId id)
        {
            return await Collection.Find(document =>
                document.PartitionKey == GetPartitionKey(id.ToString())
                && document.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(T document)
        {
            _dbContext.CreateOrUpdate(document);
            await Collection.ReplaceOneAsync(doc =>
                doc.PartitionKey == GetPartitionKey(document.Id.ToString())
                && doc.Id == document.Id,
                document);
            return document;
        }

        public IMongoCollection<T> GetCollection()
        {
            return Collection;
        }

        public string GetPartitionKey(string id)
        {
            var _partitionKey = _dbContext.GetPartitionKey(PartitonPrefix, id, NumberOfPartitions);
            return _partitionKey;
        }

        public FilterDefinition<T> BuildIsDeletedFilter(bool? isDeleted)
        {
            return isDeleted == null ?
                null :
                Builders<T>.Filter.Where(doc => doc.IsDeleted == isDeleted);
        }

        public FilterDefinition<T> BuildIdFilter(string id)
        {
            return id == null ?
                null :
                Builders<T>.Filter.Where(doc => doc.PartitionKey == GetPartitionKey(id)) &
                Builders<T>.Filter.Where(doc => doc.Id == ObjectId.Parse(id));
        }
    }
}
