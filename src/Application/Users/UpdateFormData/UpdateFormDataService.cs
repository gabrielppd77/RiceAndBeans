using Contracts.Services.Authentication;
using Application.Common.Services;
using Domain.Common.Errors;
using Contracts.Repositories;
using Contracts.Works;
using ErrorOr;

namespace Application.Users.UpdateFormData;

public class UpdateFormDataService(
    IUserAuthenticated userAuthenticated,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository)
    : IServiceHandler<UpdateFormDataRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handler(UpdateFormDataRequest request)
    {
        var userId = userAuthenticated.GetUserId();

        var user = await userRepository.GetById(userId);

        if (user is null) return Errors.User.UserNotFound;

        user.UpdateFormFields(request.Name);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}