using Application.Common.Interfaces.Email;
using Application.Common.Interfaces.Email.Templates;
using Application.Common.Interfaces.Frontend;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Users;
using ErrorOr;
using MediatR;

namespace Application.RecoverPassword.RecoverPassword;

public class RecoverPasswordCommandHandler(
    IEmailService emailService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IFrontendSettingsWrapper frontendSettingsWrapper,
    IPasswordRecoveryEmailTemplate passwordRecoveryEmailTemplate)
    : IRequestHandler<RecoverPasswordCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmail(request.Email);

        if (user is null)
        {
            return Unit.Value;
        }

        user.StartRecoverPassword();

        var link = $"{frontendSettingsWrapper.PasswordRecoveryUrl}?token={user.TokenRecoverPassword}";

        var body = passwordRecoveryEmailTemplate.Generate(link);

        await emailService.SendEmailAsync(request.Email, "Recuperação de senha", body);

        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}