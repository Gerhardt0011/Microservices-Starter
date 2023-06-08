using Common.Events.Teams;
using Customers.Service.Contracts.Repositories;
using Customers.Service.Models;
using MassTransit;

namespace Customers.Service.Consumers;

public class TeamCreated : IConsumer<ITeamCreated>
{
    private readonly ITeamsRepository _teamsRepository;

    public TeamCreated(ITeamsRepository teamsRepository)
    {
        _teamsRepository = teamsRepository;
    }

    public Task Consume(ConsumeContext<ITeamCreated> context)
    {
        var existingTeam = _teamsRepository.TeamExists(context.Message.Id);

        if (existingTeam)
        {
            return Task.CompletedTask;
        }

        _teamsRepository.AddTeam(new Team
        {
            Id = context.Message.Id
        });

        return Task.CompletedTask;
    }
}
