using Api;
using Api.Extensions;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.Use(async (context, next) =>
    {
        var host = context.Request.Host;
        Console.WriteLine($"Host request: {host}");
        await next();
    });

    app.UseHttpsRedirection();
    app.MapControllers().RequireAuthorization();
    app.MapGet("/", () => "Server is Living!");
    app.MapGet("/version", () =>
    {
        var version = new
        {
            Name = Environment.GetEnvironmentVariable("APP_NAME"),
            Version = Environment.GetEnvironmentVariable("APP_VERSION"),
            Commit = Environment.GetEnvironmentVariable("GIT_COMMIT"),
            BuildTime = Environment.GetEnvironmentVariable("BUILD_TIME"),
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
        };

        return Results.Ok(version);
    });
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSwaggerWithUi();
    app.ApplyMigrations();
    app.UseCorsPolicy();

    app.Run();
}