using Contracts.Repositories;
using Domain.Picturies;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class PictureRepository(ApplicationDbContext context) : IPictureRepository
{
    public async Task Add(Picture picture)
    {
        await context.Pictures.AddAsync(picture);
    }

    public async Task<string?> GetPathByEntityUntracked(string entityType, Guid entityId)
    {
        return await context.Pictures
            .AsNoTracking()
            .Where(x => x.EntityType == entityType)
            .Where(x => x.EntityId == entityId)
            .Select(x => x.Path)
            .FirstOrDefaultAsync();
    }

    public async Task<Picture?> GetByEntityUntracked(string entityType, Guid entityId)
    {
        return await context.Pictures
            .AsNoTracking()
            .Where(x => x.EntityType == entityType)
            .Where(x => x.EntityId == entityId)
            .FirstOrDefaultAsync();
    }
}