using Application.Common.Interfaces.Database;
using Application.Common.Interfaces.Persistence;
using Domain.Companies;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _context;

    public UserRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddUser(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task AddCompany(Company company)
    {
        await _context.Companies.AddAsync(company);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserById(Guid userId)
    {
        return await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
    }

    public void RemoveUser(User user)
    {
        _context.Users.Remove(user);
    }
}
