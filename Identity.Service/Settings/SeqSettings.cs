namespace Identity.Service.Settings;

public class SeqSettings
{
    public string? Host { get; init; }

    public string? Port { get; init; }

    public string? ServerUrl => $"http://{Host}:{Port}";
}