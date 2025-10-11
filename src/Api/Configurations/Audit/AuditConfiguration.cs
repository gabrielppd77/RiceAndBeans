using Audit.Core;
using Audit.WebApi;
using Serilog;
using Serilog.Context;

namespace Api.Configurations.Audit;

public static class AuditConfiguration
{
    internal static IServiceCollection AddAudit(this IServiceCollection services)
    {
        services.AddSingleton<IAuditScopeFactory, CustomAuditScopeFactory>();

        Configuration.Setup()
            .UseDynamicProvider(_ => _.OnInsert(auditEvent =>
            {
                if (auditEvent.Environment.Exception != null)
                {
                    Log.ForContext<AuditEvent>().Error("Audit Exception in {CallingMethodName}: {Exception}", auditEvent.Environment.CallingMethodName, auditEvent.Environment.Exception);
                }
                else
                {
                    using (LogContext.PushProperty("AuditEvent", auditEvent, destructureObjects: true))
                    {
                        Log.ForContext<AuditEvent>().Information(
                            "Audit Event: {EventType} in {Duration}ms",
                            auditEvent.EventType, auditEvent.Duration);
                    }
                }
            }));

        return services;
    }

    public static IApplicationBuilder UseAudit(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            //to audit request body, was enabled to read more times
            context.Request.EnableBuffering();
            await next();
        });

        app.UseAuditMiddleware(_ => _
            .IncludeHeaders()
            .IncludeRequestBody()
            .IncludeResponseBody()
            .WithEventType("HTTP:{verb}:{url}"));

        return app;
    }
}