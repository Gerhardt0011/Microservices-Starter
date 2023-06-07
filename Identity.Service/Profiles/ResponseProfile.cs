using AutoMapper;
using Identity.Service.Contracts.Responses;

namespace Identity.Service.Profiles;

public class ResponseProfile : Profile
{
    public ResponseProfile()
    {
        CreateMap<AuthenticationResult, AuthenticationResponse>();
    }
}