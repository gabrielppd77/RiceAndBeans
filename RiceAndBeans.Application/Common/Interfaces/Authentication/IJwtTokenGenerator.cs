
using RiceAndBeans.Domain.Users;

namespace RiceAndBeans.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
	string GenerateToken(User user);
}