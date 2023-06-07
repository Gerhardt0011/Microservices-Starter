using Common.Enums.Teams;

namespace Common.Events.Teams;

public interface ITeamCreated
{
    public string Id { get; set; }
    
    public string UserId { get; set; }
    
    public string Name { get; set; }
    
    public List<string> Members { get; set; }
    
    public TeamType Type { get; set; }
}