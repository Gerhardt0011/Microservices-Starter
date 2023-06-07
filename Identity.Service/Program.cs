using System.Security.Cryptography;
using Common;
using Common.MassTransit;
using Identity.Service.Contracts.Repositories;
using Identity.Service.Contracts.Services;
using Identity.Service.Models;
using Identity.Service.Repositories;
using Identity.Service.Services;
using Identity.Service.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
{
    BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String)); // not sure if this is optimal
    
    var mongoDbSettings = builder.Configuration
        .GetSection(nameof(MongoDbSettings))
        .Get<MongoDbSettings>();
    
    var jwtSettings = builder.Configuration
        .GetSection(nameof(JwtSettings))
        .Get<JwtSettings>();
    builder.Services.AddSingleton(jwtSettings!);

    builder.Services.AddMassTransitWithRabbitMq();

    builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
        .AddMongoDbStores<ApplicationUser, ApplicationRole, ObjectId>(
            mongoDbSettings!.ConnectionString, mongoDbSettings.DatabaseName
        );
    
    var rsa = RSA.Create();
    rsa.ImportRSAPublicKey(
        source: Convert.FromBase64String(jwtSettings!.PublicKey),
        bytesRead: out var _
    );
    
    var tokenValidatorParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new RsaSecurityKey(rsa),
        RequireSignedTokens = true,
        ValidateActor = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };

    builder.Services.AddSingleton(tokenValidatorParameters);

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = tokenValidatorParameters;
        });

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddMongo();
    builder.Services.RegisterMongoDbCollection<RefreshToken>(mongoDbSettings.RefreshTokensCollectionName);
    builder.Services.RegisterMongoDbCollection<Team>(mongoDbSettings.TeamCollectionName);
    
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddScoped<ITeamsRepository, TeamsRepository>();
    builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    builder.Services.AddScoped<IIdentityService, IdentityService>();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the bearer scheme",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        });
        
        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // app.UseHttpsRedirection();

    app.UseAuthentication();
    
    app.UseAuthorization();

    app.MapControllers();
}

app.Run();