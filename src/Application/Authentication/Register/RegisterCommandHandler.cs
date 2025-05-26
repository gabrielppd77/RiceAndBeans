using ErrorOr;
using MediatR;

using Domain.Users;
using Domain.Companies;
using Domain.Common.Errors;

using Application.Common.Interfaces.Authentication;
using Application.Authentication.Common;
using Application.Common.Interfaces.Persistence.Repositories;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Users;

namespace Application.Authentication.Register;

public class RegisterCommandHandler :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ICreateUserRepository _createUserRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork,
        ICreateUserRepository createUserRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _createUserRepository = createUserRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _createUserRepository.GetUserByEmail(request.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        var user = new User(
            request.FirstName,
            request.Email,
            hashedPassword);

        var company = new Company(user.Id, request.CompanyName);

        await _createUserRepository.AddUser(user);
        await _createUserRepository.AddCompany(company);

        await _unitOfWork.SaveChangesAsync();

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}