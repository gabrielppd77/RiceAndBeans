using Domain.Users;

namespace Contracts.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    Task<User?> GetByEmail(string email);
    Task<User?> GetByEmailUntracked(string email);
    Task<User?> GetById(Guid userId);
    Task<User?> GetByIdUntracked(Guid userId);
    void Remove(User user);
    Task<User?> GetByTokenRecoverPassword(Guid token);
    Task<User?> GetByTokenEmailConfirmation(Guid token);
}