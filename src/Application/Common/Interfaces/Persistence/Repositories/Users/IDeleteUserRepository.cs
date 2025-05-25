using Domain.Users;

namespace Application.Common.Interfaces.Persistence.Repositories.Users;

public interface IDeleteUserRepository
{
    Task<User?> GetUserById(Guid userId);
    void RemoveUser(User user);
}