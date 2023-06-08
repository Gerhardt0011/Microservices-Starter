using System.ComponentModel.DataAnnotations;

namespace Teams.Service.Dto.User;

public class UserReadDto
{
    [Required]
    public string Id { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string CurrentTeam { get; set; } = null!;
}