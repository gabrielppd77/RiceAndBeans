namespace Api.Controllers.Authentication.Contracts;

public record AuthenticationResponse(
	Guid Id,
	string FirstName,
	string Email,
	string Token);