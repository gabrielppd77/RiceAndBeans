using Contracts.Works;
using Infrastructure.Database;

namespace Infrastructure.Persistence;

public class MigrateWork(ApplicationDbContext context) : IMigrateWork
{
    public async Task MigrateAsync()
    {
        await context.MigrateAsync();
    }
}