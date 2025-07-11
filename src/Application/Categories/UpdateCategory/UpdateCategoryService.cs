using Application.Common.ServiceHandler;
using Contracts.Repositories;
using Contracts.Works;
using Domain.Common.Errors;
using ErrorOr;

namespace Application.Categories.UpdateCategory;

public class UpdateCategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : IServiceHandler<UpdateCategoryRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handler(UpdateCategoryRequest request)
    {
        var category = await categoryRepository.GetById(request.CategoryId);

        if (category is null) return Errors.Category.CategoryNotFound;

        category.UpdateFormFields(request.Name);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}