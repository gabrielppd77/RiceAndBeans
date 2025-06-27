using System.Net;
using Api.Extensions.Common.CorsConfiguration;
using Api.Extensions.Common.SerilogConfiguration;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.Email;

namespace Api.Extensions;

public static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = @"Insira o token JWT no formato: **Bearer {seu_token_aqui}**
                              Exemplo: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });

        return services;
    }

    internal static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var corsSettings = new CorsSettings();
        configuration.Bind(CorsSettings.SectionName, corsSettings);

        var allowedOrigins = corsSettings.AllowedOrigins.Split(";");

        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicy.Default, policy =>
            {
                policy.WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }

    internal static IServiceCollection AddSerilogServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var serilogSettings = configuration
            .GetRequiredSection(SerilogSettings.SectionName)
            .Get<SerilogSettings>()!;

        NetworkCredential? credentials = null;

        if (!string.IsNullOrEmpty(serilogSettings.EmailConfigCrendentialUserName))
        {
            credentials = new NetworkCredential()
            {
                UserName = serilogSettings.EmailConfigCrendentialUserName,
                Password = serilogSettings.EmailConfigCrendentialPassword
            };
        }

        var emailConfiguration = new EmailSinkOptions()
        {
            From = serilogSettings.EmailConfigFrom,
            To = [serilogSettings.EmailConfigTo],
            Host = serilogSettings.EmailConfigHost,
            Port = serilogSettings.EmailConfigPort,
            Credentials = credentials,
            Subject = new MessageTemplateTextFormatter("[CRITICAL ERROR] RICE-AND-BEANS-API"),
            Body = new MessageTemplateTextFormatter(
                "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"),
        };

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .WriteTo.Console()
            .WriteTo.Seq(serilogSettings.SeqServerUrl)
            .WriteTo.Email(emailConfiguration, null, LogEventLevel.Fatal)
            .CreateLogger();

        return services;
    }
}