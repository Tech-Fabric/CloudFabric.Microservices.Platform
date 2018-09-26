using CloudFabric.CosmosDb.MongoAPI;
using CloudFabric.EventGrid.Events;
using CloudFabric.Library.Common.Utilities;
using Microsoft.Azure.EventGrid.Models;
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
            await SendAsync(topicEndpoint, sasKey, MapperUtility.Map<List<TEvent>, List<EventGridEvent>>(events));
        }

        public async Task SendAsync(string topicEndpoint, string sasKey, List<EventGridEvent> events)
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
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine($"database connection string: {_databaseConnectionString}");
                Console.WriteLine($"database name: {_databaseName}");
                if (_databaseConnectionString != null && _databaseName != null)
                {
                    var client = new MongoClient(_databaseConnectionString);
                    var database = client.GetDatabase(_databaseName);
                    var collection = database.GetCollection<EventGridEvent>("Events");
                    collection.InsertMany(events);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }

            #endregion




            HttpResponseMessage response = await _httpClient.SendAsync(request);

            await Task.CompletedTask;

        }

    }
}
