using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Identity.Service.Contracts.Repositories;
using Identity.Service.Contracts.Responses;
using Identity.Service.Contracts.Services;
using Identity.Service.Models;
using Identity.Service.Settings;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Service.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IMapper _mapper;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITeamsRepository _teamsRepository;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<ApplicationRole> roleManager,
        JwtSettings jwtSettings,
        IMapper mapper,
        IRefreshTokenRepository refreshTokenRepository,
        TokenValidationParameters tokenValidationParameters,
        IHttpContextAccessor httpContextAccessor,
        ITeamsRepository teamsRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _jwtSettings = jwtSettings;
        _mapper = mapper;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenValidationParameters = tokenValidationParameters;
        _httpContextAccessor = httpContextAccessor;
        _teamsRepository = teamsRepository;
    }

    public async Task<AuthenticationResult> RegisterAsync(User user)
    {
        var applicationUser = new ApplicationUser
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Name = user.Name,
            UserName = user.Email,
            Email = user.Email
        };

        var result = await _userManager.CreateAsync(applicationUser, user.Password);

        if (!result.Succeeded)
        {
            return new AuthenticationResult
            {
                Errors = result.Errors.Select(x => x.Description)
            };
        }

        var team = await _teamsRepository.CreateTeamAsync(new Team
        {
            UserId = applicationUser.Id.ToString(),
            Name = $"{applicationUser.FirstName}'s Team",
            Members = new List<string> { applicationUser.Id.ToString() }
        });

        applicationUser.CurrentTeam = team.Id;
        await _userManager.UpdateAsync(applicationUser);

        return await GenerateAuthenticationResultForUserAsync(applicationUser);
    }

    public async Task<AuthenticationResult> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new AuthenticationResult
            {
                Errors = new[] { "User does not exist" }
            };
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
        {
            return new AuthenticationResult
            {
                Errors = new[] { "Invalid Login credentials" }
            };
        }

        return await GenerateAuthenticationResultForUserAsync(user);
    }

    public async Task<Role> CreateRoleAsync(string name)
    {
        var newRole = new ApplicationRole
        {
            Name = name
        };

        await _roleManager.CreateAsync(newRole);

        return _mapper.Map<Role>(newRole);
    }

    public IEnumerable<Role> GetRoles()
    {
        return _mapper.Map<IEnumerable<Role>>(_roleManager.Roles);
    }

    public async Task<Role?> GetRoleByNameAsync(string roleName)
    {
        return _mapper.Map<Role>(await _roleManager.FindByNameAsync(roleName));
    }

    public async Task<User?> GetUserByIdAsync(string? id)
    {
        if (id == null) return null;

        var user = await _userManager.FindByIdAsync(id);

        return _mapper.Map<User>(user);
    }

    public async Task AddRoleToUserAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) return;

        await _userManager.AddToRoleAsync(
            user,
            roleName
        );
    }

    public async Task<AuthenticationResult> RefreshTokenAsync(string refreshToken)
    {
        var storedRefreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(
            Encoding.UTF8.GetString(Convert.FromBase64String(refreshToken))
        );

        if (storedRefreshToken == null)
        {
            return new AuthenticationResult { Errors = new[] { "This refresh token doesn't exist" } };
        }

        if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
        {
            return new AuthenticationResult { Errors = new[] { "This refresh token has expired" } };
        }

        if (storedRefreshToken.Invalidated)
        {
            return new AuthenticationResult { Errors = new[] { "This refresh token has been invalidated" } };
        }

        if (storedRefreshToken.Used)
        {
            return new AuthenticationResult { Errors = new[] { "This refresh token has been used" } };
        }

        var user = await _userManager.FindByIdAsync(storedRefreshToken.UserId);

        await _refreshTokenRepository.DeleteAsync(storedRefreshToken.Id);

        if (user == null)
        {
            return new AuthenticationResult { Errors = new[] { "User not found" } };
        }

        return await GenerateAuthenticationResultForUserAsync(user);
    }

    private ClaimsPrincipal? GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);

            if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
            {
                return null;
            }

            return principal;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.RsaSha256, StringComparison.InvariantCultureIgnoreCase);
    }

    private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(ApplicationUser applicationUser)
    {
        var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(
            source: Convert.FromBase64String(_jwtSettings.PrivateKey),
            bytesRead: out var _);

        var tokenHandler = new JwtSecurityTokenHandler();

        var key = new RsaSecurityKey(rsa);
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, applicationUser.UserName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email ?? string.Empty),
            new Claim("userId", applicationUser.Id.ToString()),
            new Claim("teamId", applicationUser.CurrentTeam),
        };

        var userRoles = applicationUser.Roles.ToList();
        foreach (var roleId in userRoles)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role != null)
                claims = claims.Append(new Claim(ClaimTypes.Role, role.Name ?? string.Empty)).ToArray();
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
            SigningCredentials =
                new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        var refreshToken = new RefreshToken
        {
            JwtId = token.Id,
            UserId = applicationUser.Id.ToString(),
            CreationDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddMonths(6)
        };

        await _refreshTokenRepository.CreateRefreshToken(refreshToken);

        SetRefreshToken(refreshToken);

        return new AuthenticationResult
        {
            Success = true,
            Token = tokenString,
            User = _mapper.Map<User>(applicationUser)
        };
    }

    private void SetRefreshToken(RefreshToken refreshToken)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(
            "refreshToken",
            Convert.ToBase64String(Encoding.UTF8.GetBytes(refreshToken.Id)),
            new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiryDate,
                Secure = false,
            }
        );
    }
}