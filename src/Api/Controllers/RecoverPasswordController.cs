using Api.Controllers.Common;
using Application.Common.ServiceHandler;
using Application.RecoverPassword.RecoverPassword;
using Application.RecoverPassword.ResetPassword;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("recover-password")]
public class RecoverPasswordController : ApiController
{
    [AllowAnonymous]
    [HttpPost("recover")]
    public async Task<IActionResult> RecoverPassword(
        IServiceHandler<RecoverPasswordRequest, ErrorOr<Success>> service,
        string email)
    {
        var result = await service.Handler(new RecoverPasswordRequest(email));
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }

    [AllowAnonymous]
    [HttpPost("reset")]
    public async Task<IActionResult> ResetPassword(
        IServiceHandler<ResetPasswordRequest, ErrorOr<Success>> service,
        ResetPasswordRequest request)
    {
        var result = await service.Handler(request);
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }
}