using Domain.Users;

namespace Application.Common.Interfaces.Persistence.Repositories.Users;

public interface IRecoverPasswordUserRepository
{
    Task<User?> GetUserByEmail(string email);
}
