using Teams.Service.Models;

namespace Teams.Service.Contracts.Repositories;

public interface ITeamsRepository
{
    public Task<Team?> GetTeamAsync(string id);
    public Task<IEnumerable<Team>> GetTeamsAsync();
    public Task<Team> CreateTeamAsync(Team team);
    public Task UpdateTeamAsync(Team team);
    public Task DeleteTeamAsync(string id);
}