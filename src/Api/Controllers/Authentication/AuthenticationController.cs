using Api.Controllers.Authentication.Contracts;
using Application.Authentication.ConfirmEmail;
using Application.Authentication.Login;
using Application.Authentication.Register;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Authentication;

[Route("auth")]
public class AuthenticationController(ISender mediator, IMapper mapper) : ApiController
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = mapper.Map<RegisterCommand>(request);

        var authResult = await mediator.Send(command);

        return authResult.Match(
            x => Ok(mapper.Map<AuthenticationResponse>(x)),
            Problem
        );
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = mapper.Map<LoginQuery>(request);

        var authResult = await mediator.Send(command);

        return authResult.Match(
            x => Ok(mapper.Map<AuthenticationResponse>(x)),
            Problem
        );
    }

    [AllowAnonymous]
    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(Guid token)
    {
        var command = new ConfirmEmailCommand(token);

        var authResult = await mediator.Send(command);

        return authResult.Match(
            _ => NoContent(),
            Problem
        );
    }
}