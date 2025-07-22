using System.Reflection;
using Application.Common.ServiceHandler;
using Application.Positions.ChangePosition;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.Scan(scan => scan
            .FromAssemblies(typeof(IServiceHandler<,>).Assembly)
            .AddClasses(c => c.AssignableTo(typeof(IServiceHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        services.Decorate(typeof(IServiceHandler<,>), typeof(ServiceHandlerDecorator<,>));
        services.AddScoped(typeof(IChangePositionService<>), typeof(ChangePositionService<>));

        return services;
    }
}