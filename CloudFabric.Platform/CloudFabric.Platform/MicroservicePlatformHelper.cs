namespace CloudFabric.Platform
{
    using Microsoft.Extensions.Configuration;

    public static class MicroservicePlatform
    {
        private static string TokenUrl;
        private static string ClientName;
        private static string ClientSecret;

        public static void Configure(string tokenUrl, string clientName, string clientSecret)
        {
            TokenUrl = tokenUrl;
            ClientName = clientName;
            ClientSecret = clientSecret;
        }

        public static void Configure(IConfigurationRoot configuration)
        {
            TokenUrl = configuration.GetSection("AzureAd:TokenUrl").Value;
            ClientName = configuration.GetSection("AzureAd:ClientId").Value;
            ClientSecret = configuration.GetSection("AzureAd:ClientSecret").Value;
        }
    }
}
