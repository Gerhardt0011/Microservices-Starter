using AutoMapper;
using Identity.Service.Events.Teams;
using Identity.Service.Models;

namespace Identity.Service.Profiles;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<Team, TeamCreated>();
        CreateMap<Team, TeamUpdated>();
    }
}