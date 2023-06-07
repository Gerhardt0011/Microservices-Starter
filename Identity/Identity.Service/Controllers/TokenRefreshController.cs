using AutoMapper;
using Identity.Service.Contracts.Responses;
using Identity.Service.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Service.Controllers;

[ApiController]
[Route("api/auth/refresh")]
public class TokenRefreshController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public TokenRefreshController(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<AuthenticationResponse>> RefreshTokenAsync()
    {
        var refreshToken = HttpContext.Request.Cookies["refreshToken"];

        var result = await _identityService.RefreshTokenAsync(refreshToken ?? string.Empty);

        return (result.Success) ? Ok(_mapper.Map<AuthenticationResponse>(result)) : BadRequest(result.Errors);
    }
}