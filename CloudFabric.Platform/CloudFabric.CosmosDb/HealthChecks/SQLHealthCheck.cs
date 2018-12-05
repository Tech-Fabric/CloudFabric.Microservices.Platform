using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudFabric.CosmosDb.HealthChecks
{
    public class SQLHealthCheck : IHealthCheck
    {
        string _uri, _sharedAccessKey;

        public string Name => "sql";

        public SQLHealthCheck(string uri, string sharedAccessKey)
        {
            _uri = uri;
            _sharedAccessKey = sharedAccessKey;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var _client = new DocumentClient(new Uri(_uri), _sharedAccessKey); ;
                var _account = await _client.GetDatabaseAccountAsync();

                if(_account == null)
                {
                    return HealthCheckResult.Unhealthy();
                }
            }
            catch (Exception)
            {
                return HealthCheckResult.Unhealthy();
            }

            return HealthCheckResult.Healthy();
        }
    }
}
