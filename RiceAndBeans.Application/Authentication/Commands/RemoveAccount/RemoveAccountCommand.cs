using ErrorOr;
using MediatR;

namespace RiceAndBeans.Application.Authentication.Commands.RemoveAccount
{
    public record RemoveAccountCommand(string Password) : IRequest<ErrorOr<Unit>>;
}