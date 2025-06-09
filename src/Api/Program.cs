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
    app.UseHttpsRedirection();
    app.MapControllers().RequireAuthorization();
    app.MapGet("/", () => "Server is Living!");
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSwaggerWithUi();
    app.ApplyMigrations();
    app.UseCorsPolicy();

    app.Run();
}