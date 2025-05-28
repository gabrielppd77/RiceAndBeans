using ErrorOr;
using MediatR;

using Domain.Common.Errors;

using Application.Common.Interfaces.Persistence.Repositories.Users;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;

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

        user.RemoveTokenRecoverPassword();
        user.Password = _passwordHasher.HashPassword(request.NewPassword);

        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
