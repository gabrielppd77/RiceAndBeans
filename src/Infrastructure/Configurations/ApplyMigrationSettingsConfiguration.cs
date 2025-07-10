using Contracts.Services.Project.ApplyMigration;
using Infrastructure.Project.ApplyMigration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations;

public static class ApplyMigrationSettingsConfiguration
{
    public static IServiceCollection AddApplyMigrationSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var applyMigrationSettings = configuration
            .GetRequiredSection(ApplyMigrationSettings.SectionName)
            .Get<ApplyMigrationSettings>()!;
        services.AddScoped<IApplyMigrationSettings>(_ => applyMigrationSettings);

        return services;
    }
}