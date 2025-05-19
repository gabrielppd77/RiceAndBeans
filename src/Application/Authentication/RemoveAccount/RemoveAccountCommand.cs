using ErrorOr;
using MediatR;

namespace Application.Authentication.RemoveAccount
{
    public record RemoveAccountCommand(string Password) : IRequest<ErrorOr<Unit>>;
}