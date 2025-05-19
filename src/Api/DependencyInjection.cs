using Microsoft.AspNetCore.Mvc.Infrastructure;
using Api.Common.Mapping;
using Api.Common.Errors;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, ApiProblemDetailsFactory>();
        services.AddMappings();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocumentation();

        return services;
    }
}