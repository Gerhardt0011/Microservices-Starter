namespace Customers.Service.Settings;

public class MongoDbSettings
{
    public string ConnectionString { get; init; } = null!;
    public string DatabaseName { get; init; } = null!;
    public string CustomersCollectionName { get; init; } = null!;
    public string TeamsCollectionName { get; init; } = null!;
}