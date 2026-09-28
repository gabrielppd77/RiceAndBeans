using Contracts.Services.Email;
using Contracts.Services.Email.Templates;
using Infrastructure.Email;
using Infrastructure.Email.Templates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations;

public static class EmailConfiguration
{
    internal static IServiceCollection AddEmailConfig(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var emailSettings = configuration
            .GetRequiredSection(EmailSettings.SectionName)
            .Get<EmailSettings>()!;

        services.AddScoped<IEmailService>(_ => new EmailService(emailSettings));
        services.AddScoped<IConfirmPasswordEmailTemplate, ConfirmPasswordEmailTemplate>();
        services.AddScoped<IPasswordRecoveryEmailTemplate, PasswordRecoveryEmailTemplate>();

        return services;
    }
}