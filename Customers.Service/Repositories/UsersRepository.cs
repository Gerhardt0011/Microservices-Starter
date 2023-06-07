using Customers.Service.Contracts.Repositories;
using Customers.Service.Models;
using MongoDB.Driver;

namespace Customers.Service.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly IMongoCollection<User> _users;

    public UsersRepository(IMongoCollection<User> users)
    {
        _users = users;
    }

    public void AddUser(User user)
    {
        _users.InsertOne(user);
    }

    public bool UserExists(string userId)
    {
        var existingUser = _users.Find(user => user.Id == userId).FirstOrDefault();
        
        return existingUser != null;
    }
}