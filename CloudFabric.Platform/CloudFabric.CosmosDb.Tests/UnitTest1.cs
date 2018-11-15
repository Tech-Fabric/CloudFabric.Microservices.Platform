using CloudFabric.CosmosDb.MongoAPI;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

namespace CloudFabric.CosmosDb.Tests
{
    public class UnitTest1
    {

        private class TestClass : BaseDocument
        {

        }

        private class TestClass2 : BaseDocument
        {
            [JsonConverter(typeof(ObjectIdTypeConverter))]
            public List<ObjectId> Ids { get; set; }
        }

        [Fact]
        public void ValidateObjectId()
        {
            var obj = new TestClass();
            obj.Id = ObjectId.GenerateNewId();

            var json = JsonConvert.SerializeObject(obj);

            var deserialized = JsonConvert.DeserializeObject<TestClass>(json);

            Assert.Equal(obj.Id.ToString(), deserialized.Id.ToString());
        }

        [Fact]
        public void ValidateListObjectIds()
        {
            var obj = new TestClass2();
            obj.Id = ObjectId.GenerateNewId();
            obj.Ids = new List<ObjectId>();
            for (var i = 0; i < 3; i++)
            {
                obj.Ids.Add(ObjectId.GenerateNewId());
            }

            var json = JsonConvert.SerializeObject(obj);

            var deserialized = JsonConvert.DeserializeObject<TestClass2>(json);

        }

        [Fact]
        public void InvalidObjectIdsBecomeEmpty()
        {
            var json = "{\"ids\": [\"s\", \"\", \"Another Invalid\"]}";
            var deserialized = JsonConvert.DeserializeObject<TestClass2>(json);

        }
    }
}
