using Common.Events.Teams;

namespace Identity.Service.Events.Teams;

public class TeamDeleted : ITeamDeleted
{
    public string Id { get; set; } = null!;
}