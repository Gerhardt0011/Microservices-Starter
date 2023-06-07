namespace Identity.Service.Contracts.Responses;

public class AuthenticationResponse
{
    public bool Success { get; init; }
    public string? Token { get; set; }
    public IEnumerable<string>? Errors { get; init; }
}