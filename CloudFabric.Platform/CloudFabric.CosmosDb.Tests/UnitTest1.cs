using CloudFabric.CosmosDb.MongoAPI;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using Xunit;

namespace CloudFabric.CosmosDb.Tests
{
    public class UnitTest1
    {

        private class TestClass : BaseDocument
        {

        }

        [Fact]
        public void Test1()
        {
            var obj = new TestClass();
            obj.Id = ObjectId.GenerateNewId();

            var json = JsonConvert.SerializeObject(obj);

            var deserialized = JsonConvert.DeserializeObject<TestClass>(json);

            Assert.Equal(obj.Id.ToString(), deserialized.Id.ToString());
            

        }
    }
}
