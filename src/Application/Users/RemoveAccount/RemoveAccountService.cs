using Contracts.Services.Authentication;
using Application.Common.Services;
using Domain.Common.Errors;
using Contracts.Repositories;
using Contracts.Works;
using ErrorOr;

namespace Application.Users.RemoveAccount;

public class RemoveAccountService(
    IPasswordHasher passwordHasher,
    IUserAuthenticated userAuthenticated,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository)
    : IServiceHandler<RemoveAccountRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handler(RemoveAccountRequest request)
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

        return Result.Success;
    }
}