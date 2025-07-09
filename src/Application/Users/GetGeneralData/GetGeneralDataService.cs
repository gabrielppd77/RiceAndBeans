using Contracts.Services.Authentication;
using Application.Common.Services;
using Domain.Common.Errors;
using Contracts.Repositories;
using ErrorOr;

namespace Application.Users.GetGeneralData;

public class GetGeneralDataService(
    IUserRepository userRepository,
    IUserAuthenticated userAuthenticated)
    : IServiceHandler<Unit, ErrorOr<GeneralDataResponse>>
{
    public async Task<ErrorOr<GeneralDataResponse>> Handler(Unit _)
    {
        var userId = userAuthenticated.GetUserId();

        var user = await userRepository.GetByIdUntracked(userId);

        if (user is null)
            return Errors.User.UserNotFound;

        return new GeneralDataResponse(user.Name, user.UrlImage);
    }
}