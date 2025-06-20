using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Users;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Users.RemoveAccount;

public class RemoveAccountCommandHandler(
    IPasswordHasher passwordHasher,
    IUserAuthenticated userAuthenticated,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository)
    :
        IRequestHandler<RemoveAccountCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(RemoveAccountCommand request, CancellationToken cancellationToken)
    {
        var userId = userAuthenticated.GetUserId();

        var user = await userRepository.GetById(userId);

        if (user is null)
        {
            return Errors.User.UserNotFound;
        }

        if (!passwordHasher.VerifyPassword(request.Password, user.Password))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        userRepository.Remove(user);

        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}