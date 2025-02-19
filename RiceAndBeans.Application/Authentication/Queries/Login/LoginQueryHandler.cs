using RiceAndBeans.Application.Common.Interfaces.Authentication;
using RiceAndBeans.Application.Common.Interfaces.Persistence;
using RiceAndBeans.Application.Authentication.Common;
using RiceAndBeans.Domain.Common.Errors;
using RiceAndBeans.Domain.Users;
using ErrorOr;
using MediatR;

namespace RiceAndBeans.Application.Authentication.Queries.Login;

public class LoginQueryHandler :
	IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
	private readonly IJwtTokenGenerator _jwtTokenGenerator;
	private readonly IUserRepository _userRepository;

	public LoginQueryHandler(
		IJwtTokenGenerator jwtTokenGenerator,
		IUserRepository userRepository)
	{
		_jwtTokenGenerator = jwtTokenGenerator;
		_userRepository = userRepository;
	}

	public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
	{
		await Task.CompletedTask;

		if (_userRepository.GetUserByEmail(request.Email) is not User user)
		{
			return Errors.Authentication.InvalidCredentials;
		}

		if (user.Password != request.Password)
		{
			return Errors.Authentication.InvalidCredentials;
		}

		var token = _jwtTokenGenerator.GenerateToken(user);

		return new AuthenticationResult(user, token);
	}
}