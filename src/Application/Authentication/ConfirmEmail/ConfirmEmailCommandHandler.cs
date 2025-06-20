using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Users;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Authentication.ConfirmEmail;

public class ConfirmEmailCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ConfirmEmailCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByTokenEmailConfirmation(request.Token);

        if (user is null)
            return Errors.ConfirmEmail.InvalidToken;

        user.ConfirmEmail();

        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}