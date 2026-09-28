using Domain.Users;

namespace Contracts.Services.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}