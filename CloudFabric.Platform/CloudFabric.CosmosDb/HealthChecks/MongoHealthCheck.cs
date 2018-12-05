using System;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CloudFabric.CosmosDb.HealthChecks
{
    public class MongoHealthCheck : IHealthCheck
    {
        string _connectionString;

        public string Name => "mongo";

        public MongoHealthCheck(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var _client = new MongoClient(_connectionString);
                await _client.ListDatabasesAsync();
            }
            catch (Exception)
            {
                return HealthCheckResult.Unhealthy();
            }

            return HealthCheckResult.Healthy();
        }
    }
}
