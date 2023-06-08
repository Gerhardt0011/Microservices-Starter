using Common.Enums.Teams;
using Common.Events.Teams;

namespace Teams.Service.Events;

public class TeamCreated : ITeamCreated
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public TeamType Type { get; set; }

    public List<string> Members { get; set; } = null!;
}