using AutoMapper;
using Common.Events.Identity;
using MassTransit;
using Teams.Service.Contracts.Repositories;
using Teams.Service.Dto.User;
using Teams.Service.Models;

namespace Teams.Service.Consumers;

public class UserRegistered : IConsumer<IUserRegistered>
{
    private readonly IMapper _mapper;
    private readonly IUsersRepository _usersRepository;

    public UserRegistered(IMapper mapper, IUsersRepository usersRepository)
    {
        _mapper = mapper;
        _usersRepository = usersRepository;
    }

    public async Task Consume(ConsumeContext<IUserRegistered> context)
    {
        var userCreateDto = _mapper.Map<UserCreateDto>(context.Message);

        var existing = await _usersRepository.UserExistsAsync(context.Message.Id);
        
        if(! existing)
            await _usersRepository.CreateUserAsync(_mapper.Map<User>(userCreateDto));
    }
}