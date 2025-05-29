using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MapsterMapper;
using MediatR;

using Application.RecoverPassword.RecoverPassword;
using Application.RecoverPassword.ResetPassword;

using Api.Controllers.RecoverPassword.Contracts;

namespace Api.Controllers.RecoverPassword;

[Route("recover-password")]
public class RecoverPasswordController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public RecoverPasswordController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("recover")]
    public async Task<IActionResult> RecoverPassword(string email)
    {
        var command = new RecoverPasswordCommand(email);

        var authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(),
            Problem
        );
    }

    [AllowAnonymous]
    [HttpPost("reset")]
    public async Task<IActionResult> ResetPassword(ResetRequest request)
    {
        var command = _mapper.Map<ResetPasswordCommand>(request);

        var authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(),
            Problem
        );
    }
}
