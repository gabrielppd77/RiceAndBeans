using Api;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
	builder.Services.AddPresentation();
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
	app.UseSwaggerDocumentation();	
  
    app.Run();
}