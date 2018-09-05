using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CloudFabric.CosmosDb
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseDocument
    {
        protected BaseCosmoDbContext _dbContext;
        public abstract string CollectionName { get; }
        public abstract string DatabaseName { get; }

        protected readonly IMongoDatabase Database;
        public readonly IMongoCollection<T> Collection;


        protected abstract int NumberOfPartitions { get; }
        protected abstract string PartitonPrefix { get; }


        public BaseRepository(BaseCosmoDbContext dbContext)
        {
            _dbContext = dbContext;
            Database = _dbContext.GetDatabase(DatabaseName);
            Collection = Database.GetCollection<T>(CollectionName);
        }
        public async Task<T> CreateAsync(T document)
        {
            var tmpPartitionKey = "tmpPartitionKey";
            document.PartitionKey = tmpPartitionKey;

            _dbContext.CreateOrUpdate(document);
            await Collection.InsertOneAsync(document);

            document.PartitionKey = GetPartitionKey(document.Id.ToString());

            await Collection.ReplaceOneAsync(doc =>
                doc.PartitionKey == tmpPartitionKey
                && doc.Id == document.Id,
                document);
            return document;
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await DeleteAsync(new List<ObjectId> { id });
        }
        public async Task DeleteAsync(List<ObjectId> ids)
        {
            List<T> documents = new List<T>();
            foreach(var id in ids)
            {
                documents.Add(await GetByIdAsync(id));
            }

            documents.ForEach(async doc => {
                doc.IsDeleted = true;
                await UpdateAsync(doc);
            });
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
            return _dbContext.GetPartitionKey(PartitonPrefix, id, NumberOfPartitions);
        }
    }
}
