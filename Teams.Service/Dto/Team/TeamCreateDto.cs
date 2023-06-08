using System.ComponentModel.DataAnnotations;
using Common.Enums.Teams;

namespace Teams.Service.Dto.Team;

public class TeamCreateDto
{
    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [EnumDataType(typeof(TeamType))]
    public TeamType Type { get; set; }
}