namespace Identity.Service.Settings;

public class MongoDbSettings
{
    public string DatabaseName { get; set; } = null!;
    public string ConnectionString { get; set; } = null!;
    public string RefreshTokensCollectionName { get; set; } = null!;

    public string TeamCollectionName { get; set; } = null!;
}