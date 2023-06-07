using Common;
using AutoMapper;
using Identity.Service.Contracts.Services;
using Identity.Service.Dto.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Service.Controllers;

[ApiController]
[Route("api/auth/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsersController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public UsersController(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }

    [HttpGet("me")]
    public async Task<ActionResult<UserReadDto>> GetUser()
    {
        var user = await _identityService.GetUserByIdAsync(HttpContext.GetUserId());

        if (user == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<UserReadDto>(user));
    }
}