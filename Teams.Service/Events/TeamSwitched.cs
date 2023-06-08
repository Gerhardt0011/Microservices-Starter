using Common.Events.Teams;

namespace Teams.Service.Events;

public class TeamSwitched : ITeamSwitched
{
    public string UserId { get; set; } = null!;

    public string TeamId { get; set; } = null!;
}