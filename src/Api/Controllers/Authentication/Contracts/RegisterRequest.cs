namespace Api.Controllers.Authentication.Contracts;

public record RegisterRequest(
	string FirstName,
	string Email,
	string Password,
	string CompanyName);