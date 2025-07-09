using Contracts.Repositories;
using Infrastructure.Database;

namespace Infrastructure.Persistence;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task MigrateAsync()
    {
        await context.MigrateAsync();
    }
}