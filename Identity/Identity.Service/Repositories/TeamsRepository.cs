using AutoMapper;
using Identity.Service.Contracts.Repositories;
using Identity.Service.Events.Teams;
using Identity.Service.Models;
using MassTransit;
using MongoDB.Driver;

namespace Identity.Service.Repositories;

public class TeamsRepository : ITeamsRepository
{
    private readonly IMongoCollection<Team> _teams;
    private readonly IPublishEndpoint _publisher;
    private readonly IMapper _mapper;

    public TeamsRepository(IMongoCollection<Team> teams, IPublishEndpoint publisher, IMapper mapper)
    {
        _teams = teams;
        _publisher = publisher;
        _mapper = mapper;
    }

    public async Task<Team?> FindTeamByOwnerIdAsync(string ownerId)
    {
        return await _teams.Find(team => team.UserId == ownerId).FirstOrDefaultAsync();
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

        await _publisher.Publish(_mapper.Map<TeamCreated>(team));
        
        return team;
    }

    public async Task UpdateTeamAsync(Team team)
    {
        if (team == null)
            throw new ArgumentNullException(nameof(team));
        
        await _teams.ReplaceOneAsync(t => t.Id == team.Id, team);
        
        await _publisher.Publish(_mapper.Map<TeamUpdated>(team));
    }

    public async Task DeleteTeamAsync(string id)
    {
        await _teams.DeleteOneAsync(t => t.Id == id);
        
        await _publisher.Publish(new TeamDeleted
        {
            Id = id
        });
    }

    public async Task AddUserToTeamAsync(string userId, string teamId)
    {
        var team = await _teams.Find(t => t.Id == teamId).FirstOrDefaultAsync();
        
        if (team == null)
            throw new ArgumentException($"Team with id {teamId} not found");

        if (! team.Members.Contains<string>(userId))
        {
            team.Members.Add(userId);
            
            await _teams.ReplaceOneAsync(t => t.Id == teamId, team);
        }
    }

    public async Task RemoveUserFromTeamAsync(string userId, string teamId)
    {
        var team = await _teams.Find(t => t.Id == teamId).FirstOrDefaultAsync();
        
        if (team == null)
            throw new ArgumentException($"Team with id {teamId} not found");

        if (team.Members.Contains<string>(userId))
        {
            team.Members.Remove(userId);
            
            await _teams.ReplaceOneAsync(t => t.Id == teamId, team);
        }
    }
}