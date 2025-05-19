using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.PasswordHash;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.FileService;
using Infrastructure.Authentication;
using Infrastructure.FileService;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Infrastructure.PasswordHash;

namespace Infrastructure;

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
		services.AddSingleton<IUploadFileService, UploadFileService>();

		// services.AddSingleton<IUserRepository, UserRepository>();
		services.AddScoped<IUserRepository, UserRepository>();

        services.Configure<UploadFileSettings>(configuration.GetRequiredSection(UploadFileSettings.SectionName));

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