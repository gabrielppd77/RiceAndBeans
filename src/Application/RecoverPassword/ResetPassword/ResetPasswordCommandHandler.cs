using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Users;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.RecoverPassword.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ErrorOr<Unit>>
{
    private readonly IResetPasswordUserRepository _resetPasswordByRecoverUserRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public ResetPasswordCommandHandler(
        IResetPasswordUserRepository resetPasswordByRecoverUserRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _resetPasswordByRecoverUserRepository = resetPasswordByRecoverUserRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Unit>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _resetPasswordByRecoverUserRepository.GetUserByTokenRecoverPassword(request.Token);

        if (user is null)
            return Errors.RecoverPassword.InvalidToken;

        if (user.TokenRecoverPasswordExpire < DateTime.UtcNow)
            return Errors.RecoverPassword.ExpiredToken;

        var newPasswordHashed = _passwordHasher.HashPassword(request.NewPassword);
        user.ResetRecoverPassword(newPasswordHashed);

        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}