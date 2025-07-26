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

    public async Task<Picture?> GetUntracked(string bucket, string entityType, Guid entityId)
    {
        return await context.Pictures
            .AsNoTracking()
            .Where(x => x.Bucket == bucket)
            .Where(x => x.EntityType == entityType)
            .Where(x => x.EntityId == entityId)
            .FirstOrDefaultAsync();
    }

    public async Task<Picture?> Get(string bucket, string entityType, Guid entityId)
    {
        return await context.Pictures
            .Where(x => x.Bucket == bucket)
            .Where(x => x.EntityType == entityType)
            .Where(x => x.EntityId == entityId)
            .FirstOrDefaultAsync();
    }

    public void Remove(Picture picture)
    {
        context.Pictures.Remove(picture);
    }
}