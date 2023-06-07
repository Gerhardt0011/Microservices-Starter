using System.ComponentModel.DataAnnotations;

namespace Identity.Service.Contracts.Requests;

public class RefreshTokenRequest
{
    [Required]
    public string Token { get; set; } = null!;
}