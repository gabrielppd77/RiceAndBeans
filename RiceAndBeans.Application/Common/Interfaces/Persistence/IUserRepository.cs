using RiceAndBeans.Domain.Users;

namespace RiceAndBeans.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
	User? GetUserByEmail(string email);
	void Add(User user);
}