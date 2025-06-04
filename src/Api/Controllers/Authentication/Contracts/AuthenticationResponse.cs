namespace Api.Controllers.Authentication.Contracts;

public record AuthenticationResponse(
	Guid Id,
	string Name,
	string Email,
	string Token);