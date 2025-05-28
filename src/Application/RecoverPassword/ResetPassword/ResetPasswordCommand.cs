using ErrorOr;
using MediatR;

namespace Application.RecoverPassword.ResetPassword;

public record ResetPasswordCommand(Guid Token, string NewPassword) : IRequest<ErrorOr<Unit>>;