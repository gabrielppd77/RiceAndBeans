namespace Api.Controllers.Authentication.Contracts;

public record LoginRequest(
	string Email,
	string Password);