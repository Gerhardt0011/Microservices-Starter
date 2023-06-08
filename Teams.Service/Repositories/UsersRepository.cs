using MongoDB.Driver;
using Teams.Service.Contracts.Repositories;
using Teams.Service.Models;

namespace Teams.Service.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly IMongoCollection<User> _users;

    public UsersRepository(IMongoCollection<User> users)
    {
        _users = users;
    }

    public async Task<User> GetUserAsync(string userId)
    {
        return await _users.Find(user => user.Id == userId).SingleOrDefaultAsync();
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _users.InsertOneAsync(user);

        return user;
    }

    public async Task<bool> UserExistsAsync(string userId)
    {
        return await _users.Find(user => user.Id == userId).AnyAsync();
    }

    public async Task DeleteUserAsync(string userId)
    {
        await _users.DeleteOneAsync(user => user.Id == userId);
    }
}