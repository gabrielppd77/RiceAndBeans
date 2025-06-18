using System.Text;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Database;
using Application.Common.Interfaces.Email;
using Application.Common.Interfaces.Email.Templates;
using Application.Common.Interfaces.FileManager;
using Application.Common.Interfaces.Frontend;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Companies;
using Application.Common.Interfaces.Persistence.Repositories.Users;
using Application.Common.Interfaces.Project.ApplyMigration;
using Application.Common.Interfaces.Time;
using Infrastructure.Authentication;
using Infrastructure.Database;
using Infrastructure.Email;
using Infrastructure.Email.Templates;
using Infrastructure.FileManager;
using Infrastructure.Frontend;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Companies;
using Infrastructure.Persistence.Repositories.Users;
using Infrastructure.Project.ApplyMigration;
using Infrastructure.Time;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Minio;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuth(configuration);
        services.AddHttpContextAccessor();
        services.AddMinio(configuration);
        services.AddDatabase(configuration);
        services.AddHealthChecks(configuration);
        services.AddServices();
        services.AddSettings(configuration);

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IUploadFileService, UploadFileService>();
        services.AddScoped<IRemoveFileService, RemoveFileService>();
        services.AddScoped<IUserAuthenticated, UserAuthenticated>();
        services.AddScoped<IFrontendSettingsWrapper, FrontendSettingsWrapper>();
        services.AddScoped<IApplyMigrationSettingsWrapper, ApplyMigrationSettingsWrapper>();

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IConfirmPasswordEmailTemplate, ConfirmPasswordEmailTemplate>();
        services.AddScoped<IPasswordRecoveryEmailTemplate, PasswordRecoveryEmailTemplate>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICreateUserRepository, UserRepository>();
        services.AddScoped<ILoginUserRepository, UserRepository>();
        services.AddScoped<IDeleteUserRepository, UserRepository>();
        services.AddScoped<IRecoverPasswordUserRepository, UserRepository>();
        services.AddScoped<IResetPasswordUserRepository, UserRepository>();
        services.AddScoped<IConfirmEmailUserRepository, UserRepository>();
        services.AddScoped<IUploadImageUserRepository, UserRepository>();
        services.AddScoped<IGetGeneralDataUserRepository, UserRepository>();
        services.AddScoped<IUpdateFormDataUserRepository, UserRepository>();

        services.AddScoped<IUploadImageCompanyRepository, CompanyRepository>();
        services.AddScoped<IGetFormDataCompanyRepository, CompanyRepository>();
        services.AddScoped<IUpdateFormDataCompanyRepository, CompanyRepository>();

        return services;
    }

    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));
        services.Configure<FileManagerSettings>(configuration.GetSection(FileManagerSettings.SectionName));
        services.Configure<FrontendSettings>(configuration.GetSection(FrontendSettings.SectionName));
        services.Configure<ApplyMigrationSettings>(configuration.GetSection(ApplyMigrationSettings.SectionName));

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
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

    public static IServiceCollection AddMinio(this IServiceCollection services, IConfiguration configuration)
    {
        var uploadFileSettings = new FileManagerSettings();
        configuration.Bind(FileManagerSettings.SectionName, uploadFileSettings);

        services.AddMinio(configureClient => configureClient
            .WithEndpoint(uploadFileSettings.BaseUrl)
            .WithCredentials(uploadFileSettings.AccessKey, uploadFileSettings.SecretKey)
            .WithSSL()
            .Build());

        return services;
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(connectionString,
                    npgsqlOptions =>
                        npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
    }

    private static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks().AddNpgSql(configuration.GetConnectionString("DefaultConnection")!);
    }
}