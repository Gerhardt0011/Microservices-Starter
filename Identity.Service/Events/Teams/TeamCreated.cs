using Common.Enums.Teams;
using Common.Events.Teams;
using Common.Models;

namespace Identity.Service.Events.Teams;

public class TeamCreated : ITeamCreated
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public List<string> Members { get; set; } = null!;

    public TeamType Type { get; set; }

    List<IModel> ITeamCreated.Members { get; set; } = null!;
}