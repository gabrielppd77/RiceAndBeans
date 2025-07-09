using Application.Common.Interfaces.Email;
using Application.Common.Interfaces.Email.Templates;
using Application.Common.Interfaces.Frontend;
using Application.Common.Services;
using Domain.Common.Repositories;
using ErrorOr;

namespace Application.RecoverPassword.RecoverPassword;

public class RecoverPasswordService(
    IEmailService emailService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IFrontendSettingsWrapper frontendSettingsWrapper,
    IPasswordRecoveryEmailTemplate passwordRecoveryEmailTemplate)
    : IServiceHandler<RecoverPasswordRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handler(RecoverPasswordRequest request)
    {
        var user = await userRepository.GetByEmail(request.Email);

        if (user is null)
        {
            return Result.Success;
        }

        user.StartRecoverPassword();

        var link = $"{frontendSettingsWrapper.PasswordRecoveryUrl}?token={user.TokenRecoverPassword}";

        var body = passwordRecoveryEmailTemplate.Generate(link);

        await emailService.SendEmailAsync(request.Email, "Recuperação de senha", body);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}