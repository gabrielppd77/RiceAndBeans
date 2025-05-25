using ErrorOr;
using MediatR;

using Domain.Users;
using Domain.Common.Errors;

using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.PasswordHash;
using Application.Authentication.Common;
using Application.Common.Interfaces.Persistence.Repositories;

namespace Application.Authentication.Login;

public class LoginQueryHandler :
	IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
	private readonly IJwtTokenGenerator _jwtTokenGenerator;
	private readonly IUserRepository _userRepository;
	private readonly IPasswordHasher _passwordHasher;

    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
	{
		if (await _userRepository.GetUserByEmail(request.Email) is not User user)
		{
			return Errors.Authentication.InvalidCredentials;
		}

		if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
		{
			return Errors.Authentication.InvalidCredentials;
		}

		var token = _jwtTokenGenerator.GenerateToken(user);

		return new AuthenticationResult(user, token);
	}
}