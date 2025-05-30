using Domain.Users;

namespace Application.Common.Interfaces.Persistence.Repositories.Users;

public interface IConfirmEmailUserRepository
{
    Task<User?> GetUserByTokenEmailConfirmation(Guid token);
}
