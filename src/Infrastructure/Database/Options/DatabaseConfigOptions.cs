namespace Infrastructure.Database.Options;

public class DatabaseConfigOptions
{
    public static string SectionName { get; } = "Persistence:DatabaseConfigOptions";

    public string Host { get; set; } = string.Empty;

    public int Port { get; set; } = 5432;

    public string Database { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string SslMode { get; set; } = string.Empty;
}