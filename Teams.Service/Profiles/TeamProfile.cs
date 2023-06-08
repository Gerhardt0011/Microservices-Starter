using AutoMapper;
using Teams.Service.Dto.Team;
using Teams.Service.Events;
using Teams.Service.Models;

namespace Teams.Service.Profiles;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<TeamCreateDto, Team>();
        CreateMap<TeamUpdateDto, Team>();
        CreateMap<Team, TeamReadDto>();
        
        // Event Mappings
        CreateMap<Team, TeamCreated>();
        CreateMap<Team, TeamUpdated>();
    }
}