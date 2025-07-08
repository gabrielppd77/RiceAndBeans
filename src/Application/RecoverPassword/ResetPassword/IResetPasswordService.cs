using ErrorOr;

namespace Application.RecoverPassword.ResetPassword;

public interface IResetPasswordService
{
    Task<ErrorOr<Success>> Handle(ResetPasswordRequest request);
}