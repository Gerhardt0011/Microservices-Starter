namespace Common.Events.Identity;

public interface IUserRegistered
{
    public string Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string CurrentTeam { get; set; }
}