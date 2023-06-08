using Teams.Service.Models;

namespace Teams.Service.Contracts.Repositories;

public interface IUsersRepository
{
    public Task<User> GetUserAsync(string userId);
    public Task<User> CreateUserAsync(User user);
    public Task<bool> UserExistsAsync(string userId);
    public Task DeleteUserAsync(string userId);
}