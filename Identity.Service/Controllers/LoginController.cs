using AutoMapper;
using Identity.Service.Contracts.Responses;
using Identity.Service.Contracts.Requests;
using Identity.Service.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Service.Controllers;

[ApiController]
[Route("api/auth/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    
    public LoginController(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<ActionResult<AuthenticationResponse>> Login(LoginRequest request)
    {
        var result = await _identityService.LoginAsync(request.Email, request.Password);
        
        return (result.Success) ? Ok(_mapper.Map<AuthenticationResponse>(result)) : BadRequest(result.Errors);
    }
}