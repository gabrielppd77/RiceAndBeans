using Api.Controllers.Common;
using Application.Authentication.Common;
using Application.Authentication.ConfirmEmail;
using Application.Authentication.Login;
using Application.Authentication.Register;
using Application.Common.ServiceHandler;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        IServiceHandler<RegisterRequest, ErrorOr<AuthenticationResponse>> service,
        RegisterRequest request)
    {
        var result = await service.Handler(request);
        return result.Match(
            Ok,
            Problem
        );
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        IServiceHandler<LoginRequest, ErrorOr<AuthenticationResponse>> service,
        LoginRequest request)
    {
        var result = await service.Handler(request);
        return result.Match(
            Ok,
            Problem
        );
    }

    [AllowAnonymous]
    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(
        IServiceHandler<ConfirmEmailRequest, ErrorOr<Success>> service,
        Guid token)
    {
        var result = await service.Handler(new ConfirmEmailRequest(token));
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }
}