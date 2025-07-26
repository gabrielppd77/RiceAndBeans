using Application.Common.ServiceHandler;
using Contracts.Services.Authentication;
using Domain.Common.Errors;
using Contracts.Repositories;
using Contracts.Works;
using ErrorOr;
using Application.Picturies.RemovePicture;
using Domain.Companies;
using Domain.Users;

namespace Application.Users.RemoveAccount;

public class RemoveAccountService(
    IPasswordHasher passwordHasher,
    IUserAuthenticated userAuthenticated,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IRemovePictureService removePictureService)
    : IServiceHandler<RemoveAccountRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handler(RemoveAccountRequest request)
    {
        var userId = userAuthenticated.GetUserId();
        var companyId = userAuthenticated.GetCompanyId();

        var user = await userRepository.GetById(userId);

        if (user is null)
        {
            return Errors.User.UserNotFound;
        }

        if (!passwordHasher.VerifyPassword(request.Password, user.Password))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var resultRemovePictureUser = await removePictureService.Handler(
            new RemovePictureRequest(
                nameof(User),
                user.Id));

        if (resultRemovePictureUser.IsError) return resultRemovePictureUser.Errors;

        var resultRemovePictureCompany = await removePictureService.Handler(
            new RemovePictureRequest(
                nameof(Company),
                companyId));

        if (resultRemovePictureCompany.IsError) return resultRemovePictureCompany.Errors;

        userRepository.Remove(user);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}