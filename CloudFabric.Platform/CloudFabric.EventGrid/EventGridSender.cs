using CloudFabric.CosmosDb.MongoAPI;
using CloudFabric.EventGrid.Events;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudFabric.EventGrid
{
    public class EventGridSender
    {
        private HttpClient _httpClient;
        private string _databaseConnectionString;
        private string _databaseName;

        public EventGridSender(HttpClient httpClient, string databaseConnectionString = null, string databaseName = null)
        {
            _httpClient = httpClient;
            _databaseConnectionString = databaseConnectionString;
            _databaseName = databaseName;
        }

        public async Task SendAsync<TEvent, TEventType>(string topicEndpoint, string sasKey, TEvent e) where TEvent : BaseEvent<TEventType>
        {
            await SendAsync<TEvent, TEventType>(topicEndpoint, sasKey, new List<TEvent> { e });
        }
        public async Task SendAsync<TEvent, TEventType>(string topicEndpoint, string sasKey, List<TEvent> events) where TEvent : BaseEvent<TEventType>
        {
        
            _httpClient.DefaultRequestHeaders.Add("aeg-sas-key", sasKey);

            events.ForEach(e =>
            {
                e.Id = e.Id == null ?
                    Guid.NewGuid().ToString() :
                    e.Id;
            });

            string json = JsonConvert.SerializeObject(events);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, topicEndpoint)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };


            #region cosmosdb
            try
            {
                if (_databaseConnectionString != null && _databaseName != null)
                {
                    var client = new MongoClient(_databaseConnectionString);
                    var database = client.GetDatabase(_databaseName);
                    var collection = database.GetCollection<BaseEvent<TEventType>>("Events");
                    collection.InsertMany(events);
                }
            }
            catch
            {
                throw new Exception("Unable to save event to cosmos db.");
            }
            
            #endregion




            HttpResponseMessage response = await _httpClient.SendAsync(request);

            await Task.CompletedTask;
        }

    }
}
