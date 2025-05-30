using ErrorOr;
using MediatR;

using Domain.Common.Errors;

using Application.Common.Interfaces.Persistence.Repositories.Users;
using Application.Common.Interfaces.Persistence;

namespace Application.Authentication.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ErrorOr<Unit>>
{
    private readonly IConfirmEmailUserRepository _confirmEmailUserRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmEmailCommandHandler(
        IConfirmEmailUserRepository confirmEmailUserRepository,
        IUnitOfWork unitOfWork)
    {
        _confirmEmailUserRepository = confirmEmailUserRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Unit>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _confirmEmailUserRepository.GetUserByTokenEmailConfirmation(request.Token);

        if (user is null)
            return Errors.ConfirmEmail.InvalidToken;

        user.ConfirmEmailConfirmation();

        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}