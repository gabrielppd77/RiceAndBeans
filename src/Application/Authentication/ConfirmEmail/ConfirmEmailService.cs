using Domain.Common.Errors;
using Domain.Common.Repositories;
using ErrorOr;

namespace Application.Authentication.ConfirmEmail;

public class ConfirmEmailService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IConfirmEmailService
{
    public async Task<ErrorOr<Success>> Handle(ConfirmEmailRequest request)
    {
        var user = await userRepository.GetByTokenEmailConfirmation(request.Token);

        if (user is null)
            return Errors.ConfirmEmail.InvalidToken;

        user.ConfirmEmail();

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}