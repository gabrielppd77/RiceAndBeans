using Domain.Users;

namespace Application.Common.Interfaces.Persistence.Repositories.Users;

public interface IGetGeneralDataUserRepository
{
    Task<User?> GetUserById(Guid userId);
}