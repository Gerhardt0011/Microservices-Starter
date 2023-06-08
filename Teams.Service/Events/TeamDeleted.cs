using Common.Events.Teams;

namespace Teams.Service.Events;

public class TeamDeleted : ITeamDeleted
{
    public string Id { get; set; } = null!;
}