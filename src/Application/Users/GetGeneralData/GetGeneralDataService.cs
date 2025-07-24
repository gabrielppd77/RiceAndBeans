using Application.Common.ServiceHandler;
using Contracts.Repositories;
using Contracts.Services.Authentication;
using Contracts.Services.FileManager;
using Domain.Common.Errors;
using Domain.Users;
using ErrorOr;

namespace Application.Users.GetGeneralData;

public class GetGeneralDataService(
    IUserAuthenticated userAuthenticated,
    IUserRepository userRepository,
    IPictureRepository pictureRepository,
    IFileManagerSettings fileManagerSettings)
    : IServiceHandler<Unit, ErrorOr<GeneralDataResponse>>
{
    public async Task<ErrorOr<GeneralDataResponse>> Handler(Unit _)
    {
        var userId = userAuthenticated.GetUserId();

        var user = await userRepository.GetByIdUntracked(userId);

        if (user is null)
            return Errors.User.UserNotFound;

        var picture = await pictureRepository.GetByEntityUntracked(nameof(User), userId);

        return new GeneralDataResponse(user.Name, picture?.GetUrl(fileManagerSettings.BaseUrl));
    }
}