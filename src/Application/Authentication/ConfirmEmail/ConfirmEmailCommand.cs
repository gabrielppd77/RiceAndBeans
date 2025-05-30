using ErrorOr;
using MediatR;

namespace Application.Authentication.ConfirmEmail;

public record ConfirmEmailCommand(Guid Token): IRequest<ErrorOr<Unit>>;