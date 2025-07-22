using Domain.Common.Entities;

namespace Contracts.Repositories;

public interface IChangePositionRepository<TEntityType>
    where TEntityType : class, IEntity
{
    Task<IEnumerable<TEntityType>> GetAllByIds(IEnumerable<Guid> entitiesIds);
}