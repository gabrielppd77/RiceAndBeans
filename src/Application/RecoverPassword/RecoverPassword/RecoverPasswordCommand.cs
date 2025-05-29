using ErrorOr;
using MediatR;

namespace Application.RecoverPassword.RecoverPassword;

public record RecoverPasswordCommand(string Email) : IRequest<ErrorOr<Unit>>;
