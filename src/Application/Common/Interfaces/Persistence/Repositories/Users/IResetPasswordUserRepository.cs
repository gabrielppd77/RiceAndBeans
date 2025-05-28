using Domain.Users;

namespace Application.Common.Interfaces.Persistence.Repositories.Users;

public interface IResetPasswordUserRepository
{
    Task<User?> GetUserByTokenRecoverPassword(Guid token);
}
