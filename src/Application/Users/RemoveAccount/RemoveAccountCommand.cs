using ErrorOr;
using MediatR;

namespace Application.Users.RemoveAccount;

public record RemoveAccountCommand(string Password) : IRequest<ErrorOr<Unit>>;