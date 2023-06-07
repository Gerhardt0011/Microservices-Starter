using Identity.Service.Contracts.Requests;
using Identity.Service.Contracts.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Service.Controllers;

[ApiController]
[Route("api/auth/users/{userId}/roles")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserRolesController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public UserRolesController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddRoleToUser(string userId, AddRoleRequest request)
    {
        var role = await _identityService.GetRoleByNameAsync(request.Name);
        if (role == null)
        {
            return BadRequest("Role does not exist");
        }
        
        await _identityService.AddRoleToUserAsync(userId, request.Name);

        return NoContent();
    }
}