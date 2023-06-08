namespace Common.Events.Teams;

public interface ITeamSwitched
{
    public string UserId { get; set; }

    public string TeamId { get; set; }
}