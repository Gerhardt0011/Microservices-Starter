using Common.Enums.Teams;
using Common.Models;

namespace Common.Events.Teams;

public interface ITeamCreated
{
    public string Id { get; set; }

    public string UserId { get; set; }

    public string Name { get; set; }

    public List<IModel> Members { get; set; }

    public TeamType Type { get; set; }
}