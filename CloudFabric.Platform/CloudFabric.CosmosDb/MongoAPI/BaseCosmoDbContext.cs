using System;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace CloudFabric.CosmosDb.MongoAPI
{
    public abstract class BaseCosmoDbContext : IDisposable
    {
        protected bool _disposed = false;
        protected readonly MongoClient _client;

        public BaseCosmoDbContext()
        {
            var cosmoConnectionString = (string)Environment.GetEnvironmentVariable("CosmoConnectionString");
            if(cosmoConnectionString == null)
            {
                throw new Exception($"Invalid environment variable [CosmoConnectionString]:{cosmoConnectionString}");
            }

            _client = new MongoClient(cosmoConnectionString);
        }

        public BaseCosmoDbContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception($"Invalid CosmosDb connectionString:{connectionString}");
            }

            _client = new MongoClient(connectionString);
        }

        public IMongoDatabase GetDatabase(string name)
        {
            return _client.GetDatabase(name);
        }

        public void CreateOrUpdate<T>(T obj) where T : BaseDocument
        {
            CreateOrUpdate<T>(new List<T> { obj });
        }

        public void CreateOrUpdate<T>(List<T> obj) where T : BaseDocument
        {
            var userId = GetCurrentUserId();
            obj.ForEach(o =>
            {
                if (o.Id == ObjectId.Empty)
                {
                    o.CreatedBy = userId == null ? ObjectId.Empty : ObjectId.Parse(userId);
                    o.CreatedAt = DateTime.UtcNow;
                }
                o.LastUpdatedBy = userId == null ? ObjectId.Empty : ObjectId.Parse(userId);
                o.LastUpdatedAt = DateTime.UtcNow;
            });
        }

        public string GetPartitionKey(string prefix, string id, int numberOfPartitions)
        {
            var _md5 = MD5.Create();

            var hashedValue = _md5.ComputeHash(Encoding.UTF8.GetBytes(id));
            var asInt = BitConverter.ToInt32(hashedValue, 0);
            asInt = asInt == int.MinValue ? asInt + 1 : asInt;
            return $"{prefix}{Math.Abs(asInt) % numberOfPartitions}";
        }

        public abstract string GetCurrentUserId();

        #region IDisposable
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {

                }
            }
            this._disposed = true;
        }

        #endregion

    }
}
