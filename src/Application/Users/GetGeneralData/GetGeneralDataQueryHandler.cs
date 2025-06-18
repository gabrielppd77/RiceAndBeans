using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence.Repositories.Users;
using ErrorOr;
using MediatR;

namespace Application.Users.GetGeneralData;

public class GetGeneralDataQueryHandler(
    IGetGeneralDataUserRepository getGeneralDataUserRepository,
    IUserAuthenticated userAuthenticated)
    :
        IRequestHandler<GetGeneralDataQuery, ErrorOr<GeneralDataResult>>
{
    public async Task<ErrorOr<GeneralDataResult>> Handle(GetGeneralDataQuery request,
        CancellationToken cancellationToken)
    {
        var userId = userAuthenticated.GetUserId();

        var user = await getGeneralDataUserRepository.GetUserById(userId);

        return new GeneralDataResult(user.Name, user.UrlImage);
    }
}