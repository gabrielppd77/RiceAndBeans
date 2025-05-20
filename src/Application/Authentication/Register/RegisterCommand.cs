using ErrorOr;
using MediatR;
using Application.Authentication.Common;

namespace Application.Authentication.Register;

public record RegisterCommand(
	string FirstName,
	string Email,
	string Password,
	string CompanyName) : IRequest<ErrorOr<AuthenticationResult>>;