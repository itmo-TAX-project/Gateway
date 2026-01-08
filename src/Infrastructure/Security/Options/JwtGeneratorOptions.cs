namespace Infrastructure.Security.Options;

public class JwtGeneratorOptions
{
    public static string SectionName = "Security:JwtGenerator";

    public string JwtKey { get; set; } = String.Empty;

    public TimeSpan Expiration { get; set; }
}