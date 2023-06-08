using Customers.Service.Contracts.Repositories;
using Customers.Service.Models;
using MongoDB.Driver;

namespace Customers.Service.Repositories;

public class TeamsRepository : ITeamsRepository
{
    private readonly IMongoCollection<Team> _teams;

    public TeamsRepository(IMongoCollection<Team> teams)
    {
        _teams = teams;
    }

    public void AddTeam(Team team)
    {
        _teams.InsertOne(team);
    }

    public bool TeamExists(string teamId)
    {
        var existingTeam = _teams.Find(team => team.Id == teamId).FirstOrDefault();

        return existingTeam != null;
    }
}