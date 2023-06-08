using Customers.Service.Models;

namespace Customers.Service.SyncDataServices.Grpc;

public interface ITeamDataClient
{
    IEnumerable<Team>? GetAllTeams();
}