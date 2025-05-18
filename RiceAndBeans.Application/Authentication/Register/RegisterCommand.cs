using RiceAndBeans.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace RiceAndBeans.Application.Authentication.Register;

public record RegisterCommand(
	string FirstName,
	string LastName,
	string Email,
	string Password,
	string CompanyName) : IRequest<ErrorOr<AuthenticationResult>>;