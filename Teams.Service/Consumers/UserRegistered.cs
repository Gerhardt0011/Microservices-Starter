using AutoMapper;
using Common.Enums.Teams;
using Common.Events.Identity;
using MassTransit;
using Teams.Service.Contracts.Repositories;
using Teams.Service.Dto.User;
using Teams.Service.Events;
using Teams.Service.Models;

namespace Teams.Service.Consumers;

public class UserRegistered : IConsumer<IUserRegistered>
{
    private readonly IMapper _mapper;
    private readonly IUsersRepository _usersRepository;
    private readonly ITeamsRepository _teamsRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public UserRegistered(
        IMapper mapper,
        IUsersRepository usersRepository,
        ITeamsRepository teamsRepository,
        IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _usersRepository = usersRepository;
        _teamsRepository = teamsRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<IUserRegistered> context)
    {
        var userCreateDto = _mapper.Map<UserCreateDto>(context.Message);

        var existing = await _usersRepository.UserExistsAsync(context.Message.Id);

        if (existing) return;

        var user = await _usersRepository.CreateUserAsync(_mapper.Map<User>(userCreateDto));

        var team = await _teamsRepository.CreateTeamAsync(new Team
        {
            UserId = user.Id.ToString(),
            Name = $"{user.FirstName}'s Team",
            Members = new List<User> { user },
            Type = TeamType.Customer
        });

        // Notify Identity Service that the user has been assigned to a team
        await _publishEndpoint.Publish(new TeamSwitched
        {
            UserId = user.Id.ToString(),
            TeamId = team.Id.ToString()
        });
    }
}