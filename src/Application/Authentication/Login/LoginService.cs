using Application.Authentication.Common;
using Application.Common.Services;
using Contracts.Repositories;
using Contracts.Services.Authentication;
using Domain.Common.Errors;
using ErrorOr;

namespace Application.Authentication.Login;

public class LoginService(
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher,
    IUserRepository userRepository)
    : IServiceHandler<LoginRequest, ErrorOr<AuthenticationResponse>>
{
    public async Task<ErrorOr<AuthenticationResponse>> Handler(LoginRequest request)
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