using Identity.Service.Models;

namespace Identity.Service.Contracts.Responses;

public class AuthenticationResult
{
    public bool Success { get; init; }
    public string? Token { get; set; }
    public IEnumerable<string>? Errors { get; init; }
    public User User { get; init; } = null!;
}