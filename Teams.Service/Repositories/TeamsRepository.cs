using MongoDB.Driver;
using Teams.Service.Contracts.Repositories;
using Teams.Service.Models;

namespace Teams.Service.Repositories;

public class TeamsRepository : ITeamsRepository
{
    private readonly IMongoCollection<Team> _teams;

    public TeamsRepository(IMongoCollection<Team> teams)
    {
        _teams = teams;
    }

    public async Task<Team?> GetTeamAsync(string id)
    {
        return await _teams.Find(team => team.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Team>> GetTeamsAsync()
    {
        return await _teams.Find(team => true).ToListAsync();
    }

    public async Task<Team> CreateTeamAsync(Team team)
    {
        if (team == null)
            throw new ArgumentNullException(nameof(team));

        await _teams.InsertOneAsync(team);

        return team;
    }

    public async Task UpdateTeamAsync(Team team)
    {
        if (team == null)
            throw new ArgumentNullException(nameof(team));

        await _teams.ReplaceOneAsync(t => t.Id == team.Id, team);
    }

    public async Task DeleteTeamAsync(string id)
    {
        await _teams.DeleteOneAsync(t => t.Id == id);
    }
}