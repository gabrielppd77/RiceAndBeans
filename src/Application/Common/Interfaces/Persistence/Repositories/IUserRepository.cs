using Domain.Companies;
using Domain.Users;

namespace Application.Common.Interfaces.Persistence.Repositories;

public interface IUserRepository
{
	Task<User?> GetUserByEmail(string email);
	Task AddUser(User user);
    Task AddCompany(Company company);
    Task<User?> GetUserById(Guid userId);
    void RemoveUser(User user);
}