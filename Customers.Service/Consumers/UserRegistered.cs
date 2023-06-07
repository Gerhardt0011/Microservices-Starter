using Common.Events.Identity;
using Customers.Service.Contracts.Repositories;
using Customers.Service.Models;
using MassTransit;

namespace Customers.Service.Consumers;

public class UserRegistered : IConsumer<IUserRegistered>
{
    private readonly IUsersRepository _usersRepository;

    public UserRegistered(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Task Consume(ConsumeContext<IUserRegistered> context)
    {
        var existingUser = _usersRepository.UserExists(context.Message.Id);

        if (existingUser)
        {
            return Task.CompletedTask;
        }
        
        _usersRepository.AddUser(new User
        {
            Id = context.Message.Id
        });

        return Task.CompletedTask;
    }
}