using Common.Models;
using Common.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Common;

public static class Extensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
        
        services.AddSingleton(provider =>
        {
            var configuration = provider.GetService<IConfiguration>();
            var mongoDbSettings = configuration!.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            var client = new MongoClient(mongoDbSettings!.ConnectionString);

            return client.GetDatabase(mongoDbSettings.DatabaseName);
        });

        return services;
    }

    public static IServiceCollection RegisterMongoDbCollection<T>(this IServiceCollection services, string collectionName) where T : IModel
    {
        services.AddSingleton<IMongoCollection<T>>(serviceProvider =>
        {
            var database = serviceProvider.GetService<IMongoDatabase>();
            return database!.GetCollection<T>(collectionName);
        });
        
        return services;
    }
    
    public static string? GetUserId(this HttpContext httpContext)
    {
        return httpContext.User?.Claims.Single(c => c.Type == "userId")?.Value;
    }
    
    public static string? GetTeamId(this HttpContext httpContext)
    {
        return httpContext.User?.Claims.Single(c => c.Type == "teamId")?.Value;
    }
}