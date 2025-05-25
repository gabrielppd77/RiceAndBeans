using ErrorOr;
using MediatR;

using Domain.Users;
using Domain.Common.Errors;

using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.PasswordHash;
using Application.Authentication.Common;
using Application.Common.Interfaces.Persistence.Repositories;
using Application.Common.Interfaces.Persistence.Repositories.Users;

namespace Application.Authentication.Login;

public class LoginQueryHandler :
	IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
	private readonly IJwtTokenGenerator _jwtTokenGenerator;
	private readonly IPasswordHasher _passwordHasher;
    private readonly ILoginUserRepository _loginUserRepository;

    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher,
        ILoginUserRepository loginUserRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _loginUserRepository = loginUserRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
	{
		if (await _loginUserRepository.GetUserByEmail(request.Email) is not User user)
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