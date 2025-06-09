using Api.Extensions.Common.CorsConfiguration;

namespace Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerWithUi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }

    public static IApplicationBuilder UseCorsPolicy(this WebApplication app)
    {
        app.UseCors(CorsPolicy.Default);

        return app;
    }
}