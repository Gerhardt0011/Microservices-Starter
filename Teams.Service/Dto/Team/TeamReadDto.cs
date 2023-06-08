using Common.Enums.Teams;
using Teams.Service.Dto.User;

namespace Teams.Service.Dto.Team;

public class TeamReadDto
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public TeamType Type { get; set; }

    public List<UserReadDto> Members { get; set; } = null!;
}