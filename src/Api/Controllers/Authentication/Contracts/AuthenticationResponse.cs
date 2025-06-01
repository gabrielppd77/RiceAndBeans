namespace Api.Controllers.Authentication.Contracts;

public record AuthenticationResponse(
	Guid Id,
	string name,
	string Email,
	string Token);