using Identity.Service.Contracts.Requests;
using Identity.Service.Contracts.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Service.Controllers;

[ApiController]
[Route("api/auth/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
public class RolesController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public RolesController(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    [HttpGet]
    public IActionResult GetRoles()
    {
        var roles = _identityService.GetRoles();
        
        return Ok(roles);
    }
    
    [HttpGet("{name}", Name = "GetRoleByNameAsync")]
    public async Task<IActionResult> GetRoleByNameAsync(string name)
    {
        var role = await _identityService.GetRoleByNameAsync(name);

        if (role == null)
        {
            return NotFound();
        }
        
        return Ok(role);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleRequest request)
    {
        var role = await _identityService.CreateRoleAsync(request.Name);
        
        return CreatedAtRoute(nameof(GetRoleByNameAsync), new { name = role.Name }, role);
    }
}