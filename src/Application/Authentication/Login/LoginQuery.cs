using ErrorOr;
using MediatR;
using Application.Authentication.Common;

namespace Application.Authentication.Login;

public record LoginQuery(
	string Email,
	string Password) : IRequest<ErrorOr<AuthenticationResult>>;