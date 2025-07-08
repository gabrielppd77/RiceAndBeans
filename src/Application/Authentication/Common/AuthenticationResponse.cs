namespace Application.Authentication.Common;

public record AuthenticationResponse(
    Guid Id,
    string Name,
    string Email,
    string Token);