using Common.Enums.Teams;

namespace Teams.Service.Dto.Team;

public class TeamReadDto
{
    public string Id { get; set; } = null!;
    
    public string UserId { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    public TeamType Type { get; set; }
}