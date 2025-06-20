using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence.Repositories.Users;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Users.GetGeneralData;

public class GetGeneralDataQueryHandler(
    IUserRepository userRepository,
    IUserAuthenticated userAuthenticated)
    :
        IRequestHandler<GetGeneralDataQuery, ErrorOr<GeneralDataResult>>
{
    public async Task<ErrorOr<GeneralDataResult>> Handle(GetGeneralDataQuery request,
        CancellationToken cancellationToken)
    {
        var userId = userAuthenticated.GetUserId();

        var user = await userRepository.GetByIdUntracked(userId);

        if (user is null)
            return Errors.User.UserNotFound;

        return new GeneralDataResult(user.Name, user.UrlImage);
    }
}