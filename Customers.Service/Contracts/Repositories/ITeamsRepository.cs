using Customers.Service.Models;

namespace Customers.Service.Contracts.Repositories;

public interface ITeamsRepository
{
    public void AddTeam(Team team);

    public bool TeamExists(string teamId);
}