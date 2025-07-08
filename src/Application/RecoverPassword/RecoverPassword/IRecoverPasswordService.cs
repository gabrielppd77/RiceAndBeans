using ErrorOr;

namespace Application.RecoverPassword.RecoverPassword;

public interface IRecoverPasswordService
{
    Task<ErrorOr<Success>> Handle(RecoverPasswordRequest request);
}