using Identity.Service.Models;

namespace Identity.Service.Contracts.Repositories;

public interface ITeamsRepository
{
    public Task<Team?> FindTeamByOwnerIdAsync(string ownerId);
    public Task<Team?> GetTeamAsync(string id);
    public Task<IEnumerable<Team>> GetTeamsAsync();
    public Task<Team> CreateTeamAsync(Team team);
    public Task UpdateTeamAsync(Team team);
    public Task DeleteTeamAsync(string id);
    
    public Task AddUserToTeamAsync(string userId, string teamId);
    public Task RemoveUserFromTeamAsync(string userId, string teamId);
}