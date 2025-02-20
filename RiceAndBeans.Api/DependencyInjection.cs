using Microsoft.AspNetCore.Mvc.Infrastructure;
using RiceAndBeans.Api.Common.Mapping;
using RiceAndBeans.Api.Common.Errors;

namespace RiceAndBeans.Api;

public static class DependencyInjection
{
	public static IServiceCollection AddPresentation(this IServiceCollection services)
	{
		services.AddControllers();
		services.AddSingleton<ProblemDetailsFactory, ApiProblemDetailsFactory>();
		services.AddMappings();

		return services;
	}
}