using RiceAndBeans.Domain.Companies;
using RiceAndBeans.Domain.Users;

namespace RiceAndBeans.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
	Task<User?> GetUserByEmail(string email);
	Task AddUser(User user);
    Task AddCompany(Company company);
    Task SaveChanges();
    Task<User?> GetUserById(Guid userId);
    void RemoveUser(User user);
}