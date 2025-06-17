using Api.Controllers.RecoverPassword.Contracts;
using Application.RecoverPassword.RecoverPassword;
using Application.RecoverPassword.ResetPassword;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.RecoverPassword;

[Route("recover-password")]
public class RecoverPasswordController(ISender mediator, IMapper mapper) : ApiController
{
    [AllowAnonymous]
    [HttpPost("recover")]
    public async Task<IActionResult> RecoverPassword(string email)
    {
        var command = new RecoverPasswordCommand(email);

        var authResult = await mediator.Send(command);

        return authResult.Match(
            _ => NoContent(),
            Problem
        );
    }

    [AllowAnonymous]
    [HttpPost("reset")]
    public async Task<IActionResult> ResetPassword(ResetRequest request)
    {
        var command = mapper.Map<ResetPasswordCommand>(request);

        var authResult = await mediator.Send(command);

        return authResult.Match(
            _ => NoContent(),
            Problem
        );
    }
}