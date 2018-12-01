using System;
using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

namespace CloudFabric.CosmosDb.MongoAPI
{
    public abstract class BaseDocument
    {
        [BsonId]
        [JsonConverter(typeof(ObjectIdTypeConverter))]
        public virtual ObjectId Id { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        [JsonConverter(typeof(ObjectIdTypeConverter))]
        public virtual ObjectId CreatedBy { get; set; }

        public virtual DateTime LastUpdatedAt { get; set; }

        [JsonConverter(typeof(ObjectIdTypeConverter))]
        public virtual ObjectId LastUpdatedBy { get; set; }

        public virtual string DocumentType { get; set; }
        public virtual string PartitionKey { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
