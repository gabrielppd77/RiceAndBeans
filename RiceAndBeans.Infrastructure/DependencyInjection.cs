using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using RiceAndBeans.Application.Common.Interfaces.Authentication;
using RiceAndBeans.Application.Common.Interfaces.Persistence;
using RiceAndBeans.Application.Common.Interfaces.Services;
using RiceAndBeans.Application.Common.Interfaces.PasswordHash;

using RiceAndBeans.Infrastructure.Authentication;
using RiceAndBeans.Infrastructure.Persistence.Repositories;
using RiceAndBeans.Infrastructure.Services;
using RiceAndBeans.Infrastructure.PasswordHash;

namespace RiceAndBeans.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		ConfigurationManager configuration)
	{
		services.AddAuth(configuration);
		services.AddHttpContextAccessor();

		services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
		services.AddSingleton<IPasswordHasher, PasswordHasher>();
		services.AddSingleton<IUserAuthenticated, UserAuthenticated>();
		// services.AddSingleton<IUserRepository, UserRepository>();
		services.AddScoped<IUserRepository, UserRepository>();

		return services;
	}

	public static IServiceCollection AddAuth(
		this IServiceCollection services,
		ConfigurationManager configuration)
	{
		var jwtSettings = new JwtSettings();
		configuration.Bind(JwtSettings.SectionName, jwtSettings);

		services.AddSingleton(Options.Create(jwtSettings));
		services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

		services
			.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtSettings.Issuer,
					ValidAudience = jwtSettings.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
				};
			});

		return services;
	}
}