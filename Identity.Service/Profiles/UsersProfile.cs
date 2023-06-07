using AutoMapper;
using Identity.Service.Models;
using Identity.Service.Contracts.Requests;
using Identity.Service.Dto.User;

namespace Identity.Service.Profiles;

public class UsersProfile: Profile
{
    public UsersProfile()
    {
        CreateMap<ApplicationUser, User>();
        
        CreateMap<User, UserReadDto>();
        
        CreateMap<LoginRequest, User>();
        CreateMap<RegisterRequest, User>();
    }
}