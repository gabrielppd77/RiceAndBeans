using RiceAndBeans.Application.Authentication.Common;
using RiceAndBeans.Domain.Common.Errors;
using RiceAndBeans.Domain.Users;
using ErrorOr;
using MediatR;

using RiceAndBeans.Application.Common.Interfaces.Authentication;
using RiceAndBeans.Application.Common.Interfaces.Persistence;
using RiceAndBeans.Application.Common.Interfaces.PasswordHash;

namespace RiceAndBeans.Application.Authentication.Commands.Register;

public class RegisterCommandHandler :
	IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
	private readonly IJwtTokenGenerator _jwtTokenGenerator;
	private readonly IUserRepository _userRepository;
	private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
	{
		if (await _userRepository.GetUserByEmail(request.Email) is not null)
		{
			return Errors.User.DuplicateEmail;
		}

        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        var user = new User()
		{
			FirstName = request.FirstName,
			LastName = request.LastName,
			Email = request.Email,
			Password = hashedPassword
        };

		await _userRepository.AddUser(user);

		await _userRepository.SaveChanges();

        var token = _jwtTokenGenerator.GenerateToken(user);

		return new AuthenticationResult(user, token);
	}
}