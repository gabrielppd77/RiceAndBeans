using ErrorOr;
using MediatR;

using Application.Common.Interfaces.Email;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Users;
using Application.Common.Interfaces.Frontend;
using Application.Common.Interfaces.Email.Templates;

namespace Application.Users.RecoverPassword;

public class RecoverPasswordCommandHandler : IRequestHandler<RecoverPasswordCommand, ErrorOr<Unit>>
{
    private readonly IEmailService _emailService;
    private readonly IRecoverPasswordUserRepository _recoverPasswordUserRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFrontendSettingsWrapper _frontendSettingsWrapper;
    private readonly IPasswordRecoveryEmailTemplate _passwordRecoveryEmailTemplate;

    public RecoverPasswordCommandHandler(
        IEmailService emailService,
        IRecoverPasswordUserRepository recoverPasswordUserRepository,
        IUnitOfWork unitOfWork,
        IFrontendSettingsWrapper frontendSettingsWrapper,
        IPasswordRecoveryEmailTemplate passwordRecoveryEmailTemplate)
    {
        _emailService = emailService;
        _recoverPasswordUserRepository = recoverPasswordUserRepository;
        _unitOfWork = unitOfWork;
        _frontendSettingsWrapper = frontendSettingsWrapper;
        _passwordRecoveryEmailTemplate = passwordRecoveryEmailTemplate;
    }

    public async Task<ErrorOr<Unit>> Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _recoverPasswordUserRepository.GetUserByEmail(request.Email);

        if(user is null)
        {
            return Unit.Value;
        }

        user.CreateTokenRecoverPassword();

        var link = $"{_frontendSettingsWrapper.PasswordRecoveryUrl}?token={user.TokenRecoverPassword}";

        var body = _passwordRecoveryEmailTemplate.Generate(link);

        await _emailService.SendEmailAsync(request.Email, "Recuperação de senha", body);

        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
