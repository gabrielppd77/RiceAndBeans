namespace Api.Configurations.Cors;

public static class CorsConfiguration
{
    internal static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var corsSettings = new CorsSettings();
        configuration.Bind(CorsSettings.SectionName, corsSettings);

        var allowedOrigins = corsSettings.AllowedOrigins.Split(";");

        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicy.Default, policy =>
            {
                policy.WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }

    public static IApplicationBuilder UseCorsPolicy(this WebApplication app)
    {
        app.UseCors(CorsPolicy.Default);

        return app;
    }
}