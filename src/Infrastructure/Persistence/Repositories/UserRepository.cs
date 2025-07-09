using Contracts.Repositories;
using Domain.Users;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Users;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task Add(User user)
    {
        await context.Users.AddAsync(user);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await context.Users.SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetByEmailUntracked(string email)
    {
        return await context.Users
            .AsNoTracking()
            .Include(x => x.Company)
            .SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetById(Guid userId)
    {
        return await context.Users.SingleOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<User?> GetByIdUntracked(Guid userId)
    {
        return await context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Id == userId);
    }

    public void Remove(User user)
    {
        context.Users.Remove(user);
    }

    public async Task<User?> GetByTokenRecoverPassword(Guid token)
    {
        return await context.Users.SingleOrDefaultAsync(x => x.TokenRecoverPassword == token);
    }

    public async Task<User?> GetByTokenEmailConfirmation(Guid token)
    {
        return await context.Users.SingleOrDefaultAsync(x => x.TokenEmailConfirmation == token);
    }
}