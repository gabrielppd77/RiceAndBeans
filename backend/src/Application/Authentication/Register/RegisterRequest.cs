namespace Application.Authentication.Register;

public record RegisterRequest(
    string Name,
    string Email,
    string Password,
    string CompanyName);