using Application.Common.Services;
using Application.Project.ApplyMigration;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[AllowAnonymous]
[Route("")]
public class ProjectController : ApiController
{
    [HttpGet("")]
    public IActionResult GetHealthCheck()
    {
        return Ok("Server is Living!");
    }

    [HttpGet("version")]
    public IActionResult GetVersion()
    {
        return Ok(new
        {
            Name = Environment.GetEnvironmentVariable("APP_NAME"),
            Version = Environment.GetEnvironmentVariable("APP_VERSION"),
            Commit = Environment.GetEnvironmentVariable("GIT_COMMIT"),
            BuildTime = Environment.GetEnvironmentVariable("BUILD_TIME"),
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
        });
    }

    [HttpPost("apply-migration")]
    public async Task<IActionResult> ApplyMigration(IServiceHandler<ApplyMigrationRequest, ErrorOr<Success>> service,
        [FromHeader(Name = "Authorization")] string? authorization)
    {
        var result = await service.Handler(new ApplyMigrationRequest(authorization));
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }
}