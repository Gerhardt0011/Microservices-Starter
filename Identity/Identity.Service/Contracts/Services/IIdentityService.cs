using Identity.Service.Contracts.Responses;
using Identity.Service.Models;

namespace Identity.Service.Contracts.Services;

public interface IIdentityService
{
    public Task<AuthenticationResult> RegisterAsync(User user);
    public Task<AuthenticationResult> LoginAsync(string email, string password);
    
    public Task<Role> CreateRoleAsync(string name);
    public IEnumerable<Role> GetRoles();
    public Task<Role?> GetRoleByNameAsync(string roleName);
    
    public Task<User?> GetUserByIdAsync(string? id);
    
    public Task AddRoleToUserAsync(string userId, string roleName);
    Task<AuthenticationResult> RefreshTokenAsync(string refreshToken);
}