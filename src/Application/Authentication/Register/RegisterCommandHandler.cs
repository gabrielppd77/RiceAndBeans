using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Email;
using Application.Common.Interfaces.Email.Templates;
using Application.Common.Interfaces.Frontend;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Users;
using Domain.Common.Errors;
using Domain.Companies;
using Domain.Users;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Register;

public class RegisterCommandHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IEmailService emailService,
    IFrontendSettingsWrapper frontendSettingsWrapper,
    IConfirmPasswordEmailTemplate confirmPasswordEmailTemplate)
    :
        IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request,
        CancellationToken cancellationToken)
    {
        if (await userRepository.GetByEmail(request.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        var hashedPassword = passwordHasher.HashPassword(request.Password);

        var user = new User(
            request.Name,
            request.Email,
            hashedPassword);

        var company = new Company(user, request.CompanyName);

        user.SetCompany(company);

        await userRepository.Add(user);

        await unitOfWork.SaveChangesAsync();

        var link = $"{frontendSettingsWrapper.ConfirmEmailUrl}?token={user.TokenEmailConfirmation}";

        var body = confirmPasswordEmailTemplate.Generate(user.Name, link);

        await emailService.SendEmailAsync(request.Email, "Confirmação de E-mail", body);

        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}