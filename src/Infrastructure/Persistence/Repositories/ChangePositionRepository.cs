using Contracts.Repositories;
using Domain.Common.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ChangePositionRepository<TEntityType>(
    ApplicationDbContext context) : IChangePositionRepository<TEntityType> where TEntityType : class, IEntity
{
    async Task<IEnumerable<TEntityType>> IChangePositionRepository<TEntityType>.GetAllByIds(IEnumerable<Guid> entitiesIds)
    {
        return await context.Set<TEntityType>()
            .Where(x => entitiesIds.Contains(x.Id))
            .ToListAsync();
    }
}