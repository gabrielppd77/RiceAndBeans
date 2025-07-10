using Contracts.Services.Frontend;
using Infrastructure.Frontend;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations;

public static class FrontendSettingsConfiguration
{
    public static IServiceCollection AddFrontendSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var frontendSettings = configuration
            .GetRequiredSection(FrontendSettings.SectionName)
            .Get<FrontendSettings>()!;
        services.AddScoped<IFrontendSettings>(_ => frontendSettings);

        return services;
    }
}