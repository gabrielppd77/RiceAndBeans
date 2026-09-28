using Contracts.Repositories;
using Contracts.Works;
using Domain.Common.Entities;
using Domain.Common.Positions;
using ErrorOr;
using FluentValidation;

namespace Application.Positions.ChangePosition;

public class ChangePositionService<TEntityType>(
    IChangePositionRepository<TEntityType> repositorio,
    IUnitOfWork unitOfWork,
    IValidator<IEnumerable<ChangePositionRequest>> validator) : IChangePositionService<TEntityType>
    where TEntityType : class, IEntity, IPositionable
{
    public async Task<ErrorOr<Success>> Handler(IEnumerable<ChangePositionRequest> request)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .ConvertAll(validationFailure =>
                    Error.Validation(
                        validationFailure.PropertyName,
                        validationFailure.ErrorMessage));

            return errors;
        }

        var entitiesNewPositions = request.ToList();
        var entities = await repositorio.GetAllByIds(entitiesNewPositions.Select(x => x.Id));

        foreach (var entity in entities)
        {
            var newPosition = entitiesNewPositions
                .Where(x => x.Id == entity.Id)
                .Select(x => x.NewPosition)
                .FirstOrDefault();
            entity.ChangePosition(newPosition);
        }

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}