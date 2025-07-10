using Api.Configurations.ApiProblemDetails;
using Api.Configurations.Cors;
using Api.Configurations.GlobalException;
using Api.Configurations.Serilog;
using Api.Configurations.Swagger;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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