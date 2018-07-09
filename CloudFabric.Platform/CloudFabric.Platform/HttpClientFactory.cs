namespace CloudFabric.Platform
{
    using System;
    using System.Net.Http;
    using IdentityModel.Client;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;

    public interface IHttpClientFactory
    {
        Task<HttpClient> Create(Uri uri, string requestScope);
    }

    public class HttpClientFactory : IHttpClientFactory
    {
        private readonly string idToken;
        private readonly TokenClient tokenClient;
        private readonly string correlationToken;

        public HttpClientFactory(string tokenUrl, string clientName, string clientSecret, string correlationToken, string idToken)
        {
            this.tokenClient = new TokenClient(tokenUrl, clientName, clientSecret);
            this.correlationToken = correlationToken;
            this.idToken = idToken;
        }

        public async Task<HttpClient> Create(Uri uri, string requestScope)
        {
            var response = await this.tokenClient.RequestClientCredentialsAsync(requestScope).ConfigureAwait(false);
            var client = new HttpClient() { BaseAddress = uri };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.AccessToken);
            client.DefaultRequestHeaders.Add("Correlation-Token", this.correlationToken);
            if (!string.IsNullOrEmpty(this.idToken))
                client.DefaultRequestHeaders.Add("IdToken", this.idToken);
            return client;
        }
    }
}
