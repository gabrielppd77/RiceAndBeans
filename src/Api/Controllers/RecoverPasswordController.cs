using Application.RecoverPassword.RecoverPassword;
using Application.RecoverPassword.ResetPassword;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("recover-password")]
public class RecoverPasswordController : ApiController
{
    [AllowAnonymous]
    [HttpPost("recover")]
    public async Task<IActionResult> RecoverPassword(IRecoverPasswordService service, string email)
    {
        var result = await service.Handle(new RecoverPasswordRequest(email));

        return result.Match(
            _ => NoContent(),
            Problem
        );
    }

    [AllowAnonymous]
    [HttpPost("reset")]
    public async Task<IActionResult> ResetPassword(IResetPasswordService service, ResetPasswordRequest request)
    {
        var result = await service.Handle(request);

        return result.Match(
            _ => NoContent(),
            Problem
        );
    }
}