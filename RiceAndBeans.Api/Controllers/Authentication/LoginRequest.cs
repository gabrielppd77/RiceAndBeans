namespace RiceAndBeans.Api.Controllers.Authentication;

public record LoginRequest(
	string Email,
	string Password);