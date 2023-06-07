using AutoMapper;
using Identity.Service.Contracts.Responses;
using Identity.Service.Models;
using Identity.Service.Contracts.Requests;
using Identity.Service.Contracts.Services;
using Identity.Service.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Service.Controllers;

[ApiController]
[Route("api/auth/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    
    public RegisterController(IIdentityService identityService, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _identityService = identityService;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<ActionResult<AuthenticationResponse>> Register(RegisterRequest request)
    {
        var result = await _identityService.RegisterAsync(_mapper.Map<User>(request));

        if (!result.Success) return BadRequest(result.Errors);
        
        await _publishEndpoint.Publish(new UserRegistered
        {
            Id = result.User.Id,
            FirstName = result.User.FirstName,
            LastName = result.User.LastName,
            Email = result.User.Email
        });

        return Ok(_mapper.Map<AuthenticationResponse>(result));

    }
}