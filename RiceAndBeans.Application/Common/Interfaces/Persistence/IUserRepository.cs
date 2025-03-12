using RiceAndBeans.Domain.Users;

namespace RiceAndBeans.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
	Task<User?> GetUserByEmail(string email);
	Task AddUser(User user);
	Task SaveChanges();
}