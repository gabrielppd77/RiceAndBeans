using ErrorOr;
using MediatR;
using Domain.Users;
using Domain.Companies;
using Domain.Common.Errors;
using Application.Common.Interfaces.Authentication;
using Application.Authentication.Common;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Users;
using Application.Common.Interfaces.Email;
using Application.Common.Interfaces.Email.Templates;
using Application.Common.Interfaces.Frontend;

namespace Application.Authentication.Register;

public class RegisterCommandHandler :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ICreateUserRepository _createUserRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IFrontendSettingsWrapper _frontendSettingsWrapper;
    private readonly IConfirmPasswordEmailTemplate _confirmPasswordEmailTemplate;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork,
        ICreateUserRepository createUserRepository,
        IEmailService emailService,
        IFrontendSettingsWrapper frontendSettingsWrapper,
        IConfirmPasswordEmailTemplate confirmPasswordEmailTemplate)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _createUserRepository = createUserRepository;
        _emailService = emailService;
        _frontendSettingsWrapper = frontendSettingsWrapper;
        _confirmPasswordEmailTemplate = confirmPasswordEmailTemplate;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request,
        CancellationToken cancellationToken)
    {
        if (await _createUserRepository.GetUserByEmail(request.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        var user = new User(
            request.Name,
            request.Email,
            hashedPassword);

        var company = new Company(user.Id, request.CompanyName);

        await _createUserRepository.AddUser(user);
        await _createUserRepository.AddCompany(company);

        await _unitOfWork.SaveChangesAsync();

        var link = $"{_frontendSettingsWrapper.ConfirmEmailUrl}?token={user.TokenEmailConfirmation}";

        var body = _confirmPasswordEmailTemplate.Generate(user.Name, link);

        await _emailService.SendEmailAsync(request.Email, "Confirmação de E-mail", body);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}