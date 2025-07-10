using Contracts.Repositories;
using Infrastructure.Persistence.Repositories.Categories;
using Infrastructure.Persistence.Repositories.Companies;
using Infrastructure.Persistence.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations;

public static class RepositoryConfiguration
{
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}