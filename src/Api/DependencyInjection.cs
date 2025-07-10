using Microsoft.AspNetCore.Mvc.Infrastructure;
using Api.Common.Errors;
using Api.Common.Exception;
using Api.Extensions;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, ApiProblemDetailsFactory>();
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddSwaggerGenWithAuth();
        services.AddCorsPolicy(configuration);
        services.AddSerilogServices(configuration);

        return services;
    }
}