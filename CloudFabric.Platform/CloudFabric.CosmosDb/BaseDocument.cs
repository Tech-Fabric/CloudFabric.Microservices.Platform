using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.CosmosDb
{
    public abstract class BaseDocument
    {
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
