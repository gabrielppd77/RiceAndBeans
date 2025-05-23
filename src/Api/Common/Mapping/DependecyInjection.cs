using System.Reflection;
using Mapster;
using MapsterMapper;

namespace Api.Common.Mapping;

public static class DependecyInjection
{
	public static IServiceCollection AddMappings(this IServiceCollection services)
	{
		var config = TypeAdapterConfig.GlobalSettings;
		config.Scan(Assembly.GetExecutingAssembly());

		services.AddSingleton(config);
		services.AddScoped<IMapper, ServiceMapper>();

		return services;
	}
}