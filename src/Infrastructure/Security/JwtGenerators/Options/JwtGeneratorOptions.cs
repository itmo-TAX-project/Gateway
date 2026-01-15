namespace Infrastructure.Security.JwtGenerators.Options;

public class JwtGeneratorOptions
{
    public static string SectionName { get; } = "Security:JwtGenerator";

    public string JwtKey { get; set; } = string.Empty;

    public TimeSpan Expiration { get; set; }
}