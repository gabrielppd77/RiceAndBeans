using ErrorOr;
using MediatR;

namespace RiceAndBeans.Application.Authentication.RemoveAccount
{
    public record RemoveAccountCommand(string Password) : IRequest<ErrorOr<Unit>>;
}