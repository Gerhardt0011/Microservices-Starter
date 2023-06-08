using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace Common.HealthChecks;

public static class Extensions
{
    public static IHealthChecksBuilder AddMongoDbHealthCheck(this IServiceCollection services, string connectionString, TimeSpan? timeout = null)
    {
        return services.AddHealthChecks()
            .Add(new HealthCheckRegistration(
                name: "MongoDb",
                factory: sp => new MongoDbHealthCheck(
                    new MongoClient(connectionString)
                ),
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "ready" },
                timeout: timeout ?? TimeSpan.FromSeconds(5)
            ));
    }

    public static IApplicationBuilder MapServiceHealthChecks(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => false
        });

        app.UseHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("ready"),
        });

        return app;
    }
}