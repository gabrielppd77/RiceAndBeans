using RiceAndBeans.Application.Common.Interfaces.Persistence;
using RiceAndBeans.Domain.Companies;
using RiceAndBeans.Domain.Users;

namespace RiceAndBeans.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();
    private static readonly List<Company> _companies = new();

    public async Task AddUser(User user)
    {
        await Task.CompletedTask;
        _users.Add(user);
    }

    public async Task AddCompany(Company company)
    {
        await Task.CompletedTask;
        _companies.Add(company);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        await Task.CompletedTask;
        return _users.SingleOrDefault(x => x.Email == email);
    }

    public async Task SaveChanges()
    {
        await Task.CompletedTask;
    }

    public async Task<User?> GetUserById(Guid userId)
    {
        await Task.CompletedTask;
        return _users.SingleOrDefault(x => x.Id == userId);
    }

    public void RemoveUser(User user)
    {
        _users.Remove(user);
    }
}
