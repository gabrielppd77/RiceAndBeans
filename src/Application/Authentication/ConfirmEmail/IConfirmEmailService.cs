using ErrorOr;

namespace Application.Authentication.ConfirmEmail;

public interface IConfirmEmailService
{
    Task<ErrorOr<Success>> Handle(ConfirmEmailRequest request);
}