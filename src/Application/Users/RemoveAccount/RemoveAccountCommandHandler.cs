using ErrorOr;
using MediatR;

using Domain.Common.Errors;

using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Users;

namespace Application.Users.RemoveAccount;

public class RemoveAccountCommandHandler :
    IRequestHandler<RemoveAccountCommand, ErrorOr<Unit>>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserAuthenticated _userAuthenticated;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDeleteUserRepository _deleteUserRepository;

    public RemoveAccountCommandHandler(
        IPasswordHasher passwordHasher,
        IUserAuthenticated userAuthenticated,
        IUnitOfWork unitOfWork,
        IDeleteUserRepository deleteUserRepository)
    {
        _passwordHasher = passwordHasher;
        _userAuthenticated = userAuthenticated;
        _unitOfWork = unitOfWork;
        _deleteUserRepository = deleteUserRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(RemoveAccountCommand request, CancellationToken cancellationToken)
    {
        var userId = _userAuthenticated.GetUserId();

        var user = await _deleteUserRepository.GetUserById(userId);

        if (user is null)
        {
            return Errors.User.UserNotFound;
        }

        if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        _deleteUserRepository.RemoveUser(user);

        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}