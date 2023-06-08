using AutoMapper;
using Common.Events.Identity;
using Teams.Service.Dto.User;
using Teams.Service.Models;

namespace Teams.Service.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<IUserRegistered, UserCreateDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<User, UserReadDto>();
    }
}