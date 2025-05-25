using ErrorOr;
using MediatR;

using Domain.Common.Errors;

using Application.Common.Interfaces.PasswordHash;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence.Repositories;
using Application.Common.Interfaces.Persistence;

namespace Application.Authentication.RemoveAccount;

public class RemoveAccountCommandHandler :
    IRequestHandler<RemoveAccountCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserAuthenticated _userAuthenticated;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveAccountCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUserAuthenticated userAuthenticated,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _userAuthenticated = userAuthenticated;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Unit>> Handle(RemoveAccountCommand request, CancellationToken cancellationToken)
    {
        var userId = _userAuthenticated.GetUserId();

        var user = await _userRepository.GetUserById(userId);

        if (user is null)
        {
            return Errors.User.UserNotFound;
        }

        if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        _userRepository.RemoveUser(user);

        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}