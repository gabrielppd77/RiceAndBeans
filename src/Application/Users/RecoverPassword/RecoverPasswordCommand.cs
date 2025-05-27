using ErrorOr;
using MediatR;

namespace Application.Users.RecoverPassword;

public record RecoverPasswordCommand(string Email) : IRequest<ErrorOr<Unit>>;
