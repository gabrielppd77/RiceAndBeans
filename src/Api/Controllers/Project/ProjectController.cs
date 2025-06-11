using Application.Project.ApplyMigration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Project;

[AllowAnonymous]
[Route("")]
public class ProjectController : ApiController
{
    private readonly ISender _mediator;

    public ProjectController(ISender mediator)
    {
        _mediator = mediator;
    }

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
    public async Task<IActionResult> ApplyMigration([FromHeader(Name = "Authorization")] string? authorization)
    {
        var command = new ApplyMigrationCommand(authorization);

        var authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => NoContent(),
            Problem
        );
    }
}