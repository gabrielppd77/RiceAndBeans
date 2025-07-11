using Application.Common.ServiceHandler;
using Contracts.Repositories;
using Contracts.Works;
using Domain.Common.Errors;
using ErrorOr;

namespace Application.Categories.RemoveCategory;

public class RemoveCategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : IServiceHandler<RemoveCategoryRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handler(RemoveCategoryRequest request)
    {
        var category = await categoryRepository.GetById(request.CategoryId);

        if (category is null) return Errors.Category.CategoryNotFound;

        categoryRepository.Remove(category);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}