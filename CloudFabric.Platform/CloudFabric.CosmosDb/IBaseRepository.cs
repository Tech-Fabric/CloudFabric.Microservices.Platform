using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudFabric.CosmosDb
{
    public interface IBaseRepository<T> where T : BaseDocument
    {
        Task<T> CreateAsync(T document);
        Task<T> UpdateAsync(T documet);
        Task DeleteAsync(ObjectId id);
        Task DeleteAsync(List<ObjectId> ids);
        Task<T> GetByIdAsync(ObjectId id);
        IMongoCollection<T> GetCollection();

        FilterDefinition<T> BuildIsDeletedFilter(bool? isDeleted);
        FilterDefinition<T> BuildIdFilter(string id);
    }
}
