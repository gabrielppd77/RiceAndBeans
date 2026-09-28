using Domain.Common.Entities;
using Domain.Common.Positions;
using ErrorOr;

namespace Application.Positions.ChangePosition;

public interface IChangePositionService<TEntityType>
    where TEntityType : class, IEntity, IPositionable
{
    Task<ErrorOr<Success>> Handler(IEnumerable<ChangePositionRequest> request);
}