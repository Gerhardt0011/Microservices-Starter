namespace Teams.Service.Settings;

public class MongoDbSettings
{
    public string DatabaseName { get; init; } = null!;
    public string TeamsCollectionName { get; init; } = null!;
    public string UsersCollectionName { get; init; } = null!;
    public string ConnectionString { get; init; } = null!;
}