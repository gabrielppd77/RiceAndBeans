using RiceAndBeans.Application.Authentication.Commands.Register;
using RiceAndBeans.Application.Authentication.Queries.Login;
using RiceAndBeans.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RiceAndBeans.Api.Controllers;

[AllowAnonymous]
[Route("auth")]
public class AuthenticationController : ApiController
{
	private readonly ISender _mediator;
	private readonly IMapper _mapper;

	public AuthenticationController(ISender mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register(RegisterRequest request)
	{
		var command = _mapper.Map<RegisterCommand>(request);

		var authResult = await _mediator.Send(command);

		return authResult.Match(
			authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
			Problem
		);
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login(LoginRequest request)
	{
		var command = _mapper.Map<LoginQuery>(request);

		var authResult = await _mediator.Send(command);

		return authResult.Match(
			authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
			Problem
		);
	}
}