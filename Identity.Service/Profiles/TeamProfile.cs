using AutoMapper;
using Common.Events.Teams;
using Identity.Service.Dto.User;
using Identity.Service.Events.Teams;
using Identity.Service.Models;

namespace Identity.Service.Profiles;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<ITeamSwitched, TeamSwitchDto>();
        CreateMap<Team, TeamCreated>();
        CreateMap<Team, TeamUpdated>();
    }
}