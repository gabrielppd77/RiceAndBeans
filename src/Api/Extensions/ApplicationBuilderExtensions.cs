using Api.Extensions.Common;

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
        if (app.Environment.IsDevelopment())
        {
            app.UseCors(CorsPolicy.Development);
        }
        else
        {
            app.UseCors(CorsPolicy.Default);
        }

        return app;
    }
}
