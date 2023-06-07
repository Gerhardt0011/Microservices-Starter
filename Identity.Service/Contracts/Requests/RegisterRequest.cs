using System.ComponentModel.DataAnnotations;

namespace Identity.Service.Contracts.Requests;

public class RegisterRequest
{
    [Required] 
    public string FirstName { get; set; } = null!;
    
    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}