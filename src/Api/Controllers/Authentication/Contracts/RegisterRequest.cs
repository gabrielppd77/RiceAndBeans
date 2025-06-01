namespace Api.Controllers.Authentication.Contracts;

public record RegisterRequest(
	string Name,
	string Email,
	string Password,
	string CompanyName);