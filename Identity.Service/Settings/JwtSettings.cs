namespace Identity.Service.Settings;

public class JwtSettings
{
    public string? Secret { get; set; }

    public string PrivateKey { get; set; } = null!;

    public string PublicKey { get; set; } = null!;
    
    public TimeSpan TokenLifetime { get; set; }
}