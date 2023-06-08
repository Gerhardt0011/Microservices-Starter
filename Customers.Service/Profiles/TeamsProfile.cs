using AutoMapper;
using Customers.Service.Models;
using Teams.Service;

namespace Customers.Service.Profiles;

public class TeamsProfile : Profile
{
    public TeamsProfile()
    {
        // gRPC Mappings
        CreateMap<GrpcTeamModel, Team>();
    }
}