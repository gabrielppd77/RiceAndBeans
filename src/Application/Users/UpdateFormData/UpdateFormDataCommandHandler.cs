using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Users;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Users.UpdateFormData;

public class UpdateFormDataCommandHandler(
    IUserAuthenticated userAuthenticated,
    IUnitOfWork unitOfWork,
    IUpdateFormDataUserRepository updateFormDataUserRepository)
    : IRequestHandler<UpdateFormDataCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(UpdateFormDataCommand request, CancellationToken cancellationToken)
    {
        var userId = userAuthenticated.GetUserId();

        var user = await updateFormDataUserRepository.GetUserById(userId);

        if (user is null) return Errors.User.UserNotFound;

        user.UpdateFormFields(request.Name);

        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}