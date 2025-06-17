using Domain.Users;

namespace Application.Common.Interfaces.Persistence.Repositories.Users;

public interface IUploadImageUserRepository
{
    Task<User?> GetUserById(Guid userId);
}