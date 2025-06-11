using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using MediatR;
using Application.Users.RemoveAccount;

namespace Api.Controllers.Users;

[Route("users")]
public class UsersController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public UsersController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpDelete("remove-account")]
    public async Task<IActionResult> RemoveAccount(string password)
    {
        var command = new RemoveAccountCommand(password);

        var authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => NoContent(),
            Problem
        );
    }
}