using System.Net;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.Email;

namespace Api.Configurations.Serilog;

public static class SerilogConfiguration
{
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