using Contracts.Services.Email;
using Contracts.Services.Email.Templates;
using Contracts.Services.Frontend;
using Application.Common.Services;
using Contracts.Repositories;
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