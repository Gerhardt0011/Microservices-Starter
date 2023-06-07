using Common.Events.Identity;

namespace Identity.Service.Events;

public class UserRegistered : IUserRegistered
{
    public string Id { get; set; } = null!;
    
    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public string Email { get; set; } = null!;

    public string CurrentTeam { get; set; } = null!;
}