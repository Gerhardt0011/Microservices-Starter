using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace Identity.Service.HealthChecks;

public class MongoDbHealthCheck : IHealthCheck
{
    private readonly MongoClient _mongoClient;

    public MongoDbHealthCheck(MongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await _mongoClient.ListDatabaseNamesAsync();
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message, ex);
        }
    }
}