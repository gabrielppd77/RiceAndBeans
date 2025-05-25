using ErrorOr;
using MediatR;

using Domain.Users;
using Domain.Companies;
using Domain.Common.Errors;

using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.PasswordHash;
using Application.Authentication.Common;
using Application.Common.Interfaces.Persistence.Repositories;
using Application.Common.Interfaces.Persistence;

namespace Application.Authentication.Register;

public class RemoveAccountCommandHandler :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveAccountCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByEmail(request.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        var user = new User(
            request.FirstName,
            request.Email,
            hashedPassword);

        var company = new Company(user.Id, request.CompanyName);

        await _userRepository.AddUser(user);
        await _userRepository.AddCompany(company);

        await _unitOfWork.SaveChangesAsync();

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}