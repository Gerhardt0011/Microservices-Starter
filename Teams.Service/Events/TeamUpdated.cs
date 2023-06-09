using Common.Enums.Teams;
using Common.Events.Teams;
using Common.Models;

namespace Teams.Service.Events;

public class TeamUpdated : ITeamUpdated
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public TeamType Type { get; set; }

    public List<IModel> Members { get; set; } = null!;
}