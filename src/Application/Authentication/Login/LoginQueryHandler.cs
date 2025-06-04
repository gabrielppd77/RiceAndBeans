using ErrorOr;
using MediatR;

using Domain.Common.Errors;

using Application.Common.Interfaces.Authentication;
using Application.Authentication.Common;
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
		var user = await _loginUserRepository.GetUserByEmail(request.Email);

        if (user is null)
		{
			return Errors.Authentication.InvalidCredentials;
		}

		if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
		{
			return Errors.Authentication.InvalidCredentials;
		}

		if(user.IsEmailConfirmed is false)
		{
			return Errors.Authentication.EmailIsNotConfirmed;
        }

		var token = _jwtTokenGenerator.GenerateToken(user);

		return new AuthenticationResult(user, token);
	}
}