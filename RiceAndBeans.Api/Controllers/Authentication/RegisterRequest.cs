namespace RiceAndBeans.Api.Controllers.Authentication;

public record RegisterRequest(
	string FirstName,
	string LastName,
	string Email,
	string Password,
	string CompanyName);