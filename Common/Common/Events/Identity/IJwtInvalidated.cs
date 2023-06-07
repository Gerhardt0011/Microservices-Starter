namespace Common.Events.Identity;

public interface IJwtInvalidated
{
    public string JwtId { get; set; }
}