using Api;
using Api.Extensions;
using Application;
using Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Host.UseSerilog();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers().RequireAuthorization();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSwaggerWithUi();
    app.ApplyMigrations();
    app.UseCorsPolicy();
    app.UseSerilogRequestLogging();
    app.UseExceptionHandler();

    app.Run();
}