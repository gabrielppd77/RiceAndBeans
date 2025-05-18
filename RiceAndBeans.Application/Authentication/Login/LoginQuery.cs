using RiceAndBeans.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace RiceAndBeans.Application.Authentication.Login;

public record LoginQuery(
	string Email,
	string Password) : IRequest<ErrorOr<AuthenticationResult>>;