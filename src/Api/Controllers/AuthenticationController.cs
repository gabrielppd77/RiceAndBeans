using Application.Authentication.ConfirmEmail;
using Application.Authentication.Login;
using Application.Authentication.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(IRegisterService service, RegisterRequest request)
    {
        var result = await service.Handle(request);

        return result.Match(
            Ok,
            Problem
        );
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(ILoginService service, LoginRequest request)
    {
        var result = await service.Handle(request);

        return result.Match(
            Ok,
            Problem
        );
    }

    [AllowAnonymous]
    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(IConfirmEmailService service, Guid token)
    {
        var authResult = await service.Handle(new ConfirmEmailRequest(token));

        return authResult.Match(
            _ => NoContent(),
            Problem
        );
    }
}