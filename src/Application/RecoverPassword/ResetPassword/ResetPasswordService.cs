using Application.Common.Interfaces.Authentication;
using Application.Common.Services;
using Domain.Common.Errors;
using Domain.Common.Repositories;
using ErrorOr;

namespace Application.RecoverPassword.ResetPassword;

public class ResetPasswordService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork)
    : IServiceHandler<ResetPasswordRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handler(ResetPasswordRequest request)
    {
        var user = await userRepository.GetByTokenRecoverPassword(request.Token);

        if (user is null)
            return Errors.RecoverPassword.InvalidToken;

        if (user.TokenRecoverPasswordExpire < DateTime.UtcNow)
            return Errors.RecoverPassword.ExpiredToken;

        var newPasswordHashed = passwordHasher.HashPassword(request.NewPassword);
        user.ResetRecoverPassword(newPasswordHashed);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}