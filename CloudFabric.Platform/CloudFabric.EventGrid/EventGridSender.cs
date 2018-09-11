using CloudFabric.EventGrid.Events;
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
        public EventGridSender(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendAsync(string topicEndpoint, string sasKey, BaseEvent e)
        {
            await SendAsync(topicEndpoint, sasKey, new List<BaseEvent> { e });
        }
        public async Task SendAsync(string topicEndpoint, string sasKey, List<BaseEvent> events)
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

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            await Task.CompletedTask;
        }

    }
}
