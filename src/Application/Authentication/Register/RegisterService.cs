using Application.Authentication.Common;
using Application.Common.ServiceHandler;
using Contracts.Services.Authentication;
using Contracts.Services.Email;
using Contracts.Services.Email.Templates;
using Contracts.Services.Frontend;
using Domain.Common.Errors;
using Contracts.Repositories;
using Contracts.Works;
using Domain.Companies;
using Domain.Users;
using ErrorOr;

namespace Application.Authentication.Register;

public class RegisterService(
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IEmailService emailService,
    IFrontendSettingsWrapper frontendSettingsWrapper,
    IConfirmPasswordEmailTemplate confirmPasswordEmailTemplate)
    : IServiceHandler<RegisterRequest, ErrorOr<AuthenticationResponse>>
{
    public async Task<ErrorOr<AuthenticationResponse>> Handler(RegisterRequest request)
    {
        if (await userRepository.GetByEmailUntracked(request.Email) is not null)
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

        return new AuthenticationResponse(user.Id, user.Name, user.Email, token);
    }
}