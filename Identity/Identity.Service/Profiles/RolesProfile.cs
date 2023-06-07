using AutoMapper;
using Identity.Service.Models;

namespace Identity.Service.Profiles;

public class RolesProfile : Profile
{
    public RolesProfile()
    {
        CreateMap<ApplicationRole, Role>();
    }
}