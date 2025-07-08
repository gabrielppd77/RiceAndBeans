using System.Text;
using Application.Authentication.ConfirmEmail;
using Application.Authentication.Login;
using Application.Authentication.Register;
using Application.Categories.CreateCategory;
using Application.Categories.ListAllCategories;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Database;
using Application.Common.Interfaces.Email;
using Application.Common.Interfaces.Email.Templates;
using Application.Common.Interfaces.FileManager;
using Application.Common.Interfaces.Frontend;
using Application.Common.Interfaces.Project.ApplyMigration;
using Application.Common.Interfaces.Time;
using Application.RecoverPassword.RecoverPassword;
using Application.RecoverPassword.ResetPassword;
using Domain.Common.Repositories;
using Infrastructure.Authentication;
using Infrastructure.Database;
using Infrastructure.Email;
using Infrastructure.Email.Templates;
using Infrastructure.Exception;
using Infrastructure.FileManager;
using Infrastructure.Frontend;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Categories;
using Infrastructure.Persistence.Repositories.Companies;
using Infrastructure.Persistence.Repositories.Users;
using Infrastructure.Project.ApplyMigration;
using Infrastructure.Time;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();

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

        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IConfirmEmailService, ConfirmEmailService>();

        services.AddScoped<IRecoverPasswordService, RecoverPasswordService>();
        services.AddScoped<IResetPasswordService, ResetPasswordService>();

        services.AddScoped<IListAllCategoriesService, ListAllCategoriesService>();
        services.AddScoped<ICreateCategoryService, CreateCategoryService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }

    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetRequiredSection(EmailSettings.SectionName));
        services.Configure<FileManagerSettings>(configuration.GetRequiredSection(FileManagerSettings.SectionName));
        services.Configure<FrontendSettings>(configuration.GetRequiredSection(FrontendSettings.SectionName));
        services.Configure<ApplyMigrationSettings>(
            configuration.GetRequiredSection(ApplyMigrationSettings.SectionName));

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration
            .GetRequiredSection(JwtSettings.SectionName)
            .Get<JwtSettings>()!;

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

                x.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        var problemDetails = new ProblemDetails
                        {
                            Status = StatusCodes.Status401Unauthorized,
                            Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
                            Title = "Authentication failed",
                            Detail = context.ErrorDescription,
                        };
                        context.Response.StatusCode = problemDetails.Status.Value;
                        context.Response.ContentType = "application/json";
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                        await context.Response.WriteAsJsonAsync(problemDetails);
                    },
                };
            });

        return services;
    }

    public static IServiceCollection AddMinio(this IServiceCollection services, IConfiguration configuration)
    {
        var uploadFileSettings = configuration
            .GetRequiredSection(FileManagerSettings.SectionName)
            .Get<FileManagerSettings>()!;

        services.AddMinio(configureClient => configureClient
            .WithEndpoint(uploadFileSettings.Host)
            .WithCredentials(uploadFileSettings.AccessKey, uploadFileSettings.SecretKey)
            .WithSSL(uploadFileSettings.EnableSsl)
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