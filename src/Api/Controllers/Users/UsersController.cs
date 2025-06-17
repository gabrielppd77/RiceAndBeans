using Application.Users.RemoveAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Users;

[Route("users")]
public class UsersController(ISender mediator) : ApiController
{
    [HttpDelete("remove-account")]
    public async Task<IActionResult> RemoveAccount(string password)
    {
        var command = new RemoveAccountCommand(password);

        var authResult = await mediator.Send(command);

        return authResult.Match(
            _ => NoContent(),
            Problem
        );
    }
}