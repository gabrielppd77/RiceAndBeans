using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Domain.Common.Errors;
using Domain.Common.Repositories;
using ErrorOr;

namespace Application.Authentication.Login;

public class LoginService(
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher,
    IUserRepository userRepository)
    :
        ILoginService
{
    public async Task<ErrorOr<AuthenticationResponse>> Handle(LoginRequest request)
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

        return new AuthenticationResponse(user.Id, user.Name, user.Email, token);
    }
}