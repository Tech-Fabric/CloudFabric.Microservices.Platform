using System;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudFabric.CosmosDb.MongoAPI
{
    public class ComplexTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (typeof(ObjectId).IsAssignableFrom(objectType));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object instance = Activator.CreateInstance(objectType);
            JObject jo = JObject.Load(reader);

            return ObjectId.Parse(jo.Value<string>());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(value.ToString());
        }
    }
}
