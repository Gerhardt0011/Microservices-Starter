using System.ComponentModel.DataAnnotations;
using Common.Enums.Teams;

namespace Teams.Service.Dto.Team;

public class TeamUpdateDto
{
    [Required]
    public string UserId { get; set; } = null!;
    
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public TeamType Type { get; set; }
}