using Customers.Service.Contracts.Repositories;
using Customers.Service.Models;
using Customers.Service.SyncDataServices.Grpc;

namespace Customers.Service.Data;

public static class PrepDb
{
    public static void PrepFromGrpc(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var grpcClient = serviceScope.ServiceProvider.GetService<ITeamDataClient>();
        var teams = grpcClient?.GetAllTeams();

        if (teams is null) return;

        var teamsRepository = serviceScope.ServiceProvider.GetService<ITeamsRepository>();
        Seed(teamsRepository, teams);
    }

    private static void Seed(ITeamsRepository? teamsRepository, IEnumerable<Team> teams)
    {
        if (teamsRepository is null) throw new ArgumentNullException(nameof(teamsRepository));

        foreach (var team in teams)
        {
            if (!teamsRepository.TeamExists(team.Id))
            {
                teamsRepository.AddTeam(team);
            }
        }
    }
}