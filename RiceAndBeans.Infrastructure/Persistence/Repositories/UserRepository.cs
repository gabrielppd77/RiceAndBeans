using RiceAndBeans.Application.Common.Interfaces.Persistence;
using RiceAndBeans.Domain.Users;

namespace RiceAndBeans.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
	private static readonly List<User> _users = new();

	public async Task AddUser(User user)
	{
        await Task.CompletedTask;
        _users.Add(user);
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
}
