using Contracts.Services.Authentication;
using Contracts.Services.Time;
using Contracts.Works;
using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Infrastructure.Time;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations;

public static class SimpleServicesConfiguration
{
    public static IServiceCollection AddSimpleServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserAuthenticated, UserAuthenticated>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMigrateWork, MigrateWork>();

        return services;
    }
}