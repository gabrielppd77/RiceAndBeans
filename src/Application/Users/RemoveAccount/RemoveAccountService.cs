using Application.Common.ServiceHandler;
using Application.Picturies.RemovePicture;
using Contracts.Repositories;
using Contracts.Services.Authentication;
using Contracts.Works;
using Domain.Common.Errors;
using Domain.Companies;
using Domain.Users;
using ErrorOr;

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

        var resultRemovePictureUser = await removePictureService.Handler(nameof(User), user.Id);

        if (resultRemovePictureUser.IsError) return resultRemovePictureUser.Errors;

        var resultRemovePictureCompany = await removePictureService.Handler(nameof(Company), companyId);

        if (resultRemovePictureCompany.IsError) return resultRemovePictureCompany.Errors;

        userRepository.Remove(user);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}