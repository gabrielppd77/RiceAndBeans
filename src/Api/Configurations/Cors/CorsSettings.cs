﻿namespace Api.Configurations.Cors;

public class CorsSettings
{
    public const string SectionName = "CorsSettings";

    public string AllowedOrigins { get; set; } = "";
}