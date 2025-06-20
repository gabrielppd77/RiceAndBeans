using Domain.Users;

namespace Application.Common.Interfaces.Persistence.Repositories.Users;

public interface IUserRepository
{
    Task Add(User user);
    Task<User?> GetByEmail(string email);
    Task<User?> GetById(Guid userId);
    void Remove(User user);
    Task<User?> GetByTokenRecoverPassword(Guid token);
    Task<User?> GetByTokenEmailConfirmation(Guid token);
}