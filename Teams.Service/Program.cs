using Common;
using Common.Identity;
using Common.MassTransit;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Teams.Service.Contracts.Repositories;
using Teams.Service.Models;
using Teams.Service.Repositories;
using Teams.Service.Settings;
using Teams.Service.SyncDataServices.Grpc;

var builder = WebApplication.CreateBuilder(args);
{
    var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
    var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

    // Add MongoDb
    builder.Services.AddMongo();
    builder.Services.RegisterMongoDbCollection<Team>(mongoDbSettings!.TeamsCollectionName);
    builder.Services.RegisterMongoDbCollection<User>(mongoDbSettings!.UsersCollectionName);

    // Add MassTransit
    builder.Services.AddMassTransitWithRabbitMq();

    // Setup Authentication
    builder.Services.AddJwtBearerAuthentication();

    // Register Automapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Register Repositories
    builder.Services.AddScoped<IUsersRepository, UsersRepository>();
    builder.Services.AddScoped<ITeamsRepository, TeamsRepository>();

    builder.Services.AddGrpc();

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

    app.MapGrpcService<GrpcTeamService>();
    app.MapGet("/protos/teams.proto", async context => await context.Response.WriteAsync(File.ReadAllText("Protos/teams.proto")));

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}