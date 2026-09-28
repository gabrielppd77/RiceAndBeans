using Application.Common.ServiceHandler;
using Application.Picturies.GetPictureUrl;
using Contracts.Repositories;
using Contracts.Services.Authentication;
using Domain.Common.Errors;
using Domain.Users;
using ErrorOr;

namespace Application.Users.GetGeneralData;

public class GetGeneralDataService(
    IUserAuthenticated userAuthenticated,
    IUserRepository userRepository,
    IGetPictureUrlService getPictureUrlService)
    : IServiceHandler<Unit, ErrorOr<GeneralDataResponse>>
{
    public async Task<ErrorOr<GeneralDataResponse>> Handler(Unit _)
    {
        var userId = userAuthenticated.GetUserId();

        var user = await userRepository.GetByIdUntracked(userId);

        if (user is null)
            return Errors.User.UserNotFound;

        var urlImage = await getPictureUrlService.Handler(nameof(User), userId);

        return new GeneralDataResponse(user.Name, urlImage);
    }
}