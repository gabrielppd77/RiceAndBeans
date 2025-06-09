namespace Api.Extensions.Common.CorsConfiguration;

public class CorsSettings
{
    public const string SectionName = "CorsSettings";

    public string AllowedOrigins { get; set; } = "";
}