using System.Security.Cryptography;
using Common.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Common.Identity;

public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly IConfiguration _configuration;

    public ConfigureJwtBearerOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtBearerOptions options)
    {
        Configure(Options.DefaultName, options);
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name == JwtBearerDefaults.AuthenticationScheme)
        {
            var serviceSettings = _configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
            var jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
            
            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(
                source: Convert.FromBase64String(jwtSettings!.PublicKey),
                bytesRead: out var _
            );
            
            options.Authority = serviceSettings?.Authority;
            options.Audience = serviceSettings?.Name;
            options.MapInboundClaims = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = "name",
                RoleClaimType = "role",
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new RsaSecurityKey(rsa),
                RequireSignedTokens = true,
                ValidateActor = false,
                ValidateAudience = false,
                ValidateLifetime = true
            };
        }
    }
}