using Application.Common.ServiceHandler;
using Contracts.Repositories;
using Contracts.Works;
using ErrorOr;

namespace Application.Categories.ChangeCategoryPosition;

public class ChangeCategoryPositionService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : IServiceHandler<IEnumerable<ChangeCategoryPositionRequest>, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handler(IEnumerable<ChangeCategoryPositionRequest> request)
    {
        var categoriesNewPositions = request.ToList();
        var categories = await categoryRepository.GetAllByIds(categoriesNewPositions.Select(x => x.Id));

        foreach (var category in categories)
        {
            var newPosition = categoriesNewPositions
                .Where(x => x.Id == category.Id)
                .Select(x => x.NewPosition)
                .FirstOrDefault();
            category.ChangePosition(newPosition);
        }

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}