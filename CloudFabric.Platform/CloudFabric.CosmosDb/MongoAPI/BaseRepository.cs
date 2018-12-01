using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CloudFabric.CosmosDb.MongoAPI
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
            _dbContext.CreateOrUpdate(document);

            document.Id = ObjectId.GenerateNewId();
            document.PartitionKey = GetPartitionKey(document.Id.ToString());

            await Collection.InsertOneAsync(document);
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
                var doc = await GetByIdAsync(id);

                if (doc != null)
                {
                    doc.IsDeleted = true;
                    await UpdateAsync(doc);
                }
            }
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
