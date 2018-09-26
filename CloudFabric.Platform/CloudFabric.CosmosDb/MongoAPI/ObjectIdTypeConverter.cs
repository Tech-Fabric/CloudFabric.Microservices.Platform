﻿using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudFabric.CosmosDb.MongoAPI
{
    public class ObjectIdTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (typeof(ObjectId).IsAssignableFrom(objectType));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                var listStrings = token.ToObject<List<string>>();
                return listStrings.Select(id => ObjectId.Parse(id)).ToList();
            }
            else
            {
                return ObjectId.Parse(token.Value<string>());
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(value.GetType() == typeof(List<ObjectId>))
            {
                writer.WriteStartArray();
                foreach(var id in ((List<ObjectId>)value))
                {
                    writer.WriteValue(id.ToString());
                }
                writer.WriteEndArray();
            }
            else
            {
                writer.WriteValue(value.ToString());

            }
        }
    }
}
