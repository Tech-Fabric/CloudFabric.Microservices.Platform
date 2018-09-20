using System;
using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

namespace CloudFabric.CosmosDb.MongoAPI
{
    public abstract class BaseDocument
    {
        [JsonConverter(typeof(ComplexTypeConverter))]
        [BsonId]
        public virtual ObjectId Id { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        [JsonConverter(typeof(ComplexTypeConverter))]
        public virtual ObjectId CreatedBy { get; set; }

        public virtual DateTime LastUpdatedAt { get; set; }

        [JsonConverter(typeof(ComplexTypeConverter))]
        public virtual ObjectId LastUpdatedBy { get; set; }

        public virtual string DocumentType => "BaseDocument";
        public virtual string PartitionKey { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
