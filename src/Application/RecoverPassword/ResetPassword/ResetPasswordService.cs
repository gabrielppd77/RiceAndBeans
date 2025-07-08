using Application.Common.Interfaces.Authentication;
using Domain.Common.Errors;
using Domain.Common.Repositories;
using ErrorOr;

namespace Application.RecoverPassword.ResetPassword;

public class ResetPasswordService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork)
    : IResetPasswordService
{
    public async Task<ErrorOr<Success>> Handle(ResetPasswordRequest request)
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