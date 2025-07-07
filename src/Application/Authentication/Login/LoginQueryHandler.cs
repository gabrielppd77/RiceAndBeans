using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Domain.Common.Errors;
using Domain.Common.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Login;

public class LoginQueryHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher,
    IUserRepository userRepository)
    :
        IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailUntracked(request.Email);

        if (user is null)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (!passwordHasher.VerifyPassword(request.Password, user.Password))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (user.IsEmailConfirmed is false)
        {
            return Errors.Authentication.EmailIsNotConfirmed;
        }

        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}