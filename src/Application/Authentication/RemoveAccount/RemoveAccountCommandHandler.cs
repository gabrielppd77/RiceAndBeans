using Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Application.Common.Interfaces.PasswordHash;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Authentication;

namespace Application.Authentication.RemoveAccount;

public class RemoveAccountCommandHandler :
    IRequestHandler<RemoveAccountCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserAuthenticated _userAuthenticated;

    public RemoveAccountCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUserAuthenticated userAuthenticated)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _userAuthenticated = userAuthenticated;
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

        await _userRepository.SaveChanges();

        return Unit.Value;
    }
}