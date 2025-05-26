using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

using Minio;
using Minio.DataModel.Args;

using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Time;
using Application.Common.Interfaces.FileService;
using Application.Common.Interfaces.Database;
using Application.Common.Interfaces.Persistence.Repositories;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Users;

using Infrastructure.Authentication;
using Infrastructure.FileService;
using Infrastructure.Time;
using Infrastructure.Database;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Users;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddHttpContextAccessor();
        services.AddMinio(configuration);
        services.AddDatabase(configuration);
        services.AddHealthChecks(configuration);

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IUploadFileService, UploadFileService>();
        services.AddScoped<IUserAuthenticated, UserAuthenticated>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICreateUserRepository, UserRepository>();
        services.AddScoped<ILoginUserRepository, UserRepository>();
        services.AddScoped<IDeleteUserRepository, UserRepository>();

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

    public static IServiceCollection AddMinio(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var uploadFileSettings = new UploadFileSettings();
        configuration.Bind(UploadFileSettings.SectionName, uploadFileSettings);

        services.AddMinio(configureClient => configureClient
            .WithEndpoint(uploadFileSettings.BaseUrl)
            .WithCredentials(uploadFileSettings.AccessKey, uploadFileSettings.SecretKey)
            .WithSSL(true)
            .Build());

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks().AddNpgSql(configuration.GetConnectionString("DefaultConnection")!);

        return services;
    }
}