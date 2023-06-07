using Customers.Service.Models;

namespace Customers.Service.Contracts.Repositories;

public interface IUsersRepository
{
    public void AddUser(User user);
    
    public bool UserExists(string userId);
}