using System.ComponentModel.DataAnnotations;
using Common.Enums.Teams;
using Teams.Service.Dto.User;

namespace Teams.Service.Dto.Team;

public class TeamReadDto
{
    [Required]
    public string Id { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public TeamType Type { get; set; }

    [Required]
    public List<UserReadDto> Members { get; set; } = null!;
}