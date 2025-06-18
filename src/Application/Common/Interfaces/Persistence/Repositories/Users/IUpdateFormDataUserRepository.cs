using Domain.Users;

namespace Application.Common.Interfaces.Persistence.Repositories.Users;

public interface IUpdateFormDataUserRepository
{
    Task<User?> GetUserById(Guid userId);
}