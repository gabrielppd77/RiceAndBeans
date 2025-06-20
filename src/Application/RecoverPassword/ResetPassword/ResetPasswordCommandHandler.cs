using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Users;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.RecoverPassword.ResetPassword;

public class ResetPasswordCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ResetPasswordCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByTokenRecoverPassword(request.Token);

        if (user is null)
            return Errors.RecoverPassword.InvalidToken;

        if (user.TokenRecoverPasswordExpire < DateTime.UtcNow)
            return Errors.RecoverPassword.ExpiredToken;

        var newPasswordHashed = passwordHasher.HashPassword(request.NewPassword);
        user.ResetRecoverPassword(newPasswordHashed);

        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}