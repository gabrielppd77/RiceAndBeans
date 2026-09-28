using Application.Common.ServiceHandler;
using Domain.Common.Errors;
using Contracts.Repositories;
using Contracts.Works;
using ErrorOr;

namespace Application.Authentication.ConfirmEmail;

public class ConfirmEmailService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IServiceHandler<ConfirmEmailRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handler(ConfirmEmailRequest request)
    {
        var user = await userRepository.GetByTokenEmailConfirmation(request.Token);

        if (user is null)
            return Errors.ConfirmEmail.InvalidToken;

        user.ConfirmEmail();

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}