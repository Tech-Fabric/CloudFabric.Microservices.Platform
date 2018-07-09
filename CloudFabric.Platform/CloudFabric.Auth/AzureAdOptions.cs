namespace CloudFabric.Auth
{
    public class AzureAdOptions
    {
        public string Tenant { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string TenantId { get; set; }
        public string TokenUrl { get; set; }
        public string ClientId { get; set; }
        public string Instance { get; set; }
        public string ClientSecret { get; set; }
    }
}
