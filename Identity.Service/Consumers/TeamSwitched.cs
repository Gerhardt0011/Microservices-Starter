using AutoMapper;
using Common.Events.Teams;
using Identity.Service.Dto.User;
using Identity.Service.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Identity.Service.Consumers;

public class TeamSwitched : IConsumer<ITeamSwitched>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public TeamSwitched(IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task Consume(ConsumeContext<ITeamSwitched> context)
    {
        var userCreateDto = _mapper.Map<TeamSwitchDto>(context.Message);

        var user = await _userManager.FindByIdAsync(userCreateDto.UserId);

        if (user == null)
        {
            Console.WriteLine($"User {userCreateDto.UserId} not found");
            return;
        }

        user.CurrentTeam = userCreateDto.TeamId;
        await _userManager.UpdateAsync(user);
    }
}