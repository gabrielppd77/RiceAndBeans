using Domain.Companies;
using Domain.Users;

namespace Application.Common.Interfaces.Persistence.Repositories.Users;

public interface ICreateUserRepository
{
    Task<User?> GetUserByEmail(string email);
    Task AddUser(User user);
    Task AddCompany(Company company);
}