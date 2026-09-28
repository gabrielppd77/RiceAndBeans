using Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuth(configuration);
        services.AddMinio(configuration);
        services.AddDatabase(configuration);
        services.AddEmailConfig(configuration);
        services.AddFileManager(configuration);
        services.AddFrontendSettings(configuration);
        services.AddApplyMigrationSettings(configuration);
        services.AddSimpleServices();
        services.AddRepositories();

        return services;
    }
}